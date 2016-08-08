/****************************************************************************
*                    PI Analysis Output Bulk Deleter                        *
*               Copyright 2016 - Luis Fabiano de Campos Batista             *
*                                                                           *
* Licensed under the Apache License, Version 2.0 (the "License");           *
* you may not use this file except in compliance with the License.          *
* You may obtain a copy of the License at                                   *
*                                                                           *
*                http://www.apache.org/licenses/LICENSE-2.0                 *
*                                                                           *
* Unless required by applicable law or agreed to in writing, software       *
* distributed under the License is distributed on an "AS IS" BASIS,         *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  *
* See the License for the specific language governing permissions and       *
* limitations under the License.                                            *
*                                                                           *
/****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PIDataBulkDeleter
{
   
    public partial class MainForm : Form
    {
        //Initial max number of objects to be retrieved from a search
        int iMaxRetrievedObjects = 1000000;
        int iAFThreadCount = 10;

        //Global variables
        OSIsoft.AF.PISystem             _AFServer;
        OSIsoft.AF.AFDatabase           _AFDB;
        OSIsoft.AF.Asset.AFElement      _TargetElement;
        bool                            _AttributeListFilterIsActive;
        Dictionary<string, bool>        _AttributeFilter;
        OSIsoft.AF.PI.PIServers         _PIServers;
        OSIsoft.AF.PI.PIServer          _PiServer;
         

        System.Diagnostics.Stopwatch _OpDuration = new System.Diagnostics.Stopwatch();
        //Int32 _NumberEventsProcessed;
        //String _EventTimeCursor;
        

        public MainForm()
        {
            InitializeComponent();

            DialogResult myDlgResult;

            //Gets the AF database and PI System instances
            try
            {
                _AFDB = OSIsoft.AF.UI.AFOperations.ConnectToDatabase(this, "", "", true, out myDlgResult);
                _AFServer = _AFDB.PISystem;
                afElementFindCtrl1.Database = _AFDB;
                
            } catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            List<object> args = (List<object>)e.Argument;
            OSIsoft.AF.Asset.AFValues afValues = new OSIsoft.AF.Asset.AFValues();

            //Parsing arguments from object sender
            DateTime dtStartTime = (DateTime)args[0]; //start time
            DateTime dtEndTime = (DateTime)args[1]; //end time
            OSIsoft.AF.Asset.AFElement elTargetElement = (OSIsoft.AF.Asset.AFElement)args[2]; //target AF element
            int opCode = ((int)args[3]); //operation code (delete pi tags only, EF only or both)
            string attrPathFilter = ((string)args[4]); //filter by attribute name using wildcards (*, ?)

            DateTime writeTime = dtStartTime;

            OSIsoft.AF.Time.AFTimeRange timeRange = new OSIsoft.AF.Time.AFTimeRange(dtStartTime, dtEndTime);

            backgroundWorker1.ReportProgress(0, "Searching elements for data deletion...");

            OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Asset.AFElement> AnalysesElements;

            if (ckbIncludeChildElements.Checked) //Searchs for child elements
            {
                AnalysesElements = OSIsoft.AF.Asset.AFElement.FindElements(_AFDB, _TargetElement, "*", OSIsoft.AF.AFSearchField.Name, true, OSIsoft.AF.AFSortField.Name, OSIsoft.AF.AFSortOrder.Ascending, iMaxRetrievedObjects);
            } else //just creates an empty list
            {
                AnalysesElements = new OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Asset.AFElement>();
            }
            
            //Add the target element
            if (_TargetElement !=null)
                AnalysesElements.Add(_TargetElement); //Including the root element to be processed too

            backgroundWorker1.ReportProgress(0, AnalysesElements.Count.ToString() + " elements found.\n");

            double elementCount = AnalysesElements.Count;
            double operationsCount = 1;
            int processingElementCount = 1;
            //Searching all elements in the list to obtain the attributes used as analyses outputs

            foreach (var element in AnalysesElements)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    backgroundWorker1.ReportProgress(100, "Delete operation cancelled.\n");
                    return;
                }
                //Used to track operation progress - ##### not being used because I changed the progress bar style to marquee #####
                int progressPercent = (int)(operationsCount / elementCount * 100);
                //int numOfEventsDeleted = 0;

                //Reporting the order of the element being processed
                backgroundWorker1.ReportProgress(processingElementCount, "Processing element " + processingElementCount.ToString() + " of " + elementCount.ToString() + "...\n");
                processingElementCount++;

                //Obtaining the list of analysis associated with the current element
                OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> elAnalyses = element.GetAnalyses(OSIsoft.AF.AFSortField.Name, OSIsoft.AF.AFSortOrder.Ascending, element.Analyses.Count + 1);

                if (opCode == 0 || opCode == 2) //To be executed if asked to delete PI Values only or (PI Values + EFs)
                {

                    backgroundWorker1.ReportProgress(progressPercent, "Deleting analyses output attributes from " + element.GetPath() + "...\n");


                    
                    //Obtaining the list of attributes that are PI Point DRs associated with the analyses
                    List<OSIsoft.AF.Asset.AFAttribute> attrOutputs = GetAnalysesOutputPointDR(elAnalyses);

                    //Using parallelism to speed up the deletes
                    Parallel.ForEach(attrOutputs, new ParallelOptions() { MaxDegreeOfParallelism = iAFThreadCount }, (attr) =>
                    //foreach (var attr in attrOutputs)
                    {
                        
                        OSIsoft.AF.Asset.AFValues values = null;
                        int valueCount = 0;

                        if (_AttributeListFilterIsActive)
                        {
                            bool bValueFound; //Flag to inform if the attr is selected in the filter list
                            bool IsSelected = _AttributeFilter.TryGetValue(attr.GetPath(), out bValueFound);
                            
                            if (bValueFound)
                            {
                                //Perform the tasks

                                //Search values if the attribute is selected in the attribute list filter
                                if (IsSelected) 
                                {
                                    backgroundWorker1.ReportProgress(progressPercent, "Deleting values from attribute " + attr.GetPath() + "...\n");
                                    values = attr.Data.RecordedValues(timeRange, OSIsoft.AF.Data.AFBoundaryType.Inside, null, null, true);
                                    valueCount = values.Count();
                                    backgroundWorker1.ReportProgress(progressPercent, "Deleting " + valueCount.ToString() + " values from attribute " + attr.GetPath() + "...\n");
                                } 

                            } else
                            {
                                //backgroundWorker1.ReportProgress(progressPercent, "The attribute " + attr.GetPath() + " was not processed (filtered out). \n");
                                //values = attr.Data.RecordedValues(timeRange, OSIsoft.AF.Data.AFBoundaryType.Inside, null, null, true);
                            }

                        } else
                        {
                            //Search values only for those attributes included by the attribute path filter
                            // of if the filter is empty (not set)
                            //This will already return everything in upper case and convert the wild cards expressions to regular expressions
                            string attrNameFilterRegExpression = WildcardToRegex(attrPathFilter);
                            
                            if (attrPathFilter== String.Empty || 
                                System.Text.RegularExpressions.Regex.IsMatch(attr.GetPath().ToUpper(), attrNameFilterRegExpression)) 
                            {
                                values = attr.Data.RecordedValues(timeRange, OSIsoft.AF.Data.AFBoundaryType.Inside, null, null, true);
                                valueCount = values.Count();



                            } else
                            {
                                //backgroundWorker1.ReportProgress(progressPercent, "The attribute " + attr.Name + " from element " + element.Name + " was not processed (filtered out). \n");
                            }



                            
                        }


                        
                        if (valueCount > 0)
                        {

                            try
                            {
                                //This is the new method supported only in PI Data Archive 2015 and above (don't need to retrieve all values - just pass the range)
                                //attr.Data.ReplaceValues(timeRange, new OSIsoft.AF.Asset.AFValues(), OSIsoft.AF.Data.AFBufferOption.BufferIfPossible);

                                
                                
                                //This is the only method supported for PI Data Archive 2015 and below (need to retrieve all values before deleting it)
                                attr.Data.UpdateValues(values, OSIsoft.AF.Data.AFUpdateOption.Remove, OSIsoft.AF.Data.AFBufferOption.BufferIfPossible);

                                backgroundWorker1.ReportProgress(progressPercent, "Deleted " + valueCount.ToString() + " values from " + attr.GetPath() + ".\n");


                            }
                            catch
                            {
                                backgroundWorker1.ReportProgress(progressPercent, "Error deleting tag events from " + attr.GetPath() + ".\n");
                            }

                        }

                        if (backgroundWorker1.CancellationPending)
                        {
                            backgroundWorker1.ReportProgress(100, "Delete operation cancelled.\n");
                            return;
                        }

                        //numOfEventsDeleted += values.Count; --> Can't use this "as is" inside the paralle foreach loop, otherwise we lose parallelism efficiency

                    });

                    
                    int numAttrFilteredOut = GetAttributeFilteredCount();
                    int numAttrProcessed = attrOutputs.Count - numAttrFilteredOut;

                    //backgroundWorker1.ReportProgress(progressPercent, numOfEventsDeleted.ToString() + " tag events were deleted from " + attrOutputs.Count.ToString() + " analysis output(s)\n");
                    backgroundWorker1.ReportProgress(progressPercent, "Finished processing PI values deletion for element " + element.GetPath() +".\n");
                }

                if (opCode == 1 || opCode == 2) //To be executed if asked to delete EFs only or (PI Values + EFs)
                {
                    /*
                    foreach (var analysis in elAnalyses)
                    {
                        // var analysesEventFrameList = OSIsoft.AF.EventFrame.AFEventFrame.FindEventFramesByAnalysis(analysis, OSIsoft.AF.Asset.AFSearchMode.Overlapped,
                        //    timeRange.StartTime, timeRange.EndTime, OSIsoft.AF.AFSortField.Name, OSIsoft.AF.AFSortOrder.Ascending, 0, iMaxRetrievedObjects);
                    }
                    */


                    backgroundWorker1.ReportProgress(progressPercent, "Searching event frames for the " + element.Name + " element.\n");
                    var analysesEventFrameList = element.GetEventFrames(OSIsoft.AF.Asset.AFSearchMode.StartInclusive, timeRange.StartTime, timeRange.EndTime, "*", null, null, OSIsoft.AF.AFSortField.StartTime, OSIsoft.AF.AFSortOrder.Ascending, 0, iMaxRetrievedObjects);

                    int efCount = analysesEventFrameList.Count;
                    //Deleting each individual event frame from the current element
                    Parallel.ForEach(analysesEventFrameList, new ParallelOptions() { MaxDegreeOfParallelism = iAFThreadCount }, (ef) =>
                    //foreach (var ef in analysesEventFrameList)
                    {
                        try
                        {
                            ef.Delete();

                        }
                        catch
                        {
                            backgroundWorker1.ReportProgress(progressPercent, "Error deleting the following event: " + ef.Name);
                        }

                    });

                    backgroundWorker1.ReportProgress(progressPercent, "Deleted " + efCount.ToString() + " event frames from the  " + element.Name + " element.\n");

                    //Commiting changes to database
                    _AFDB.CheckIn();
                }

                //Update counter used to calculate the operation progress
                operationsCount++;

            }
           

            backgroundWorker1.ReportProgress(100,"Operation completed.\n");

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
            progressBar1.Visible = false;

            _OpDuration.Stop();

            lblMessage.Text= String.Empty;

            rtxtbMsgLog.AppendText("Operation duration: " + _OpDuration.Elapsed.TotalMinutes.ToString("0.00") + " minutes.\n");
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {

                backgroundWorker1.CancelAsync();
                rtxtbMsgLog.AppendText("Aborting delete operation...\n");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            rtxtbMsgLog.Clear();

           DialogResult dgResult;


            if (_AFServer!=null)
            {

                if (_TargetElement != null)
                {


                    dgResult = MessageBox.Show("PI Data is going to be deleted. Proceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);



                } else
                {
                    dgResult = MessageBox.Show("No root element was explicitly selected, and all elements from " + _AFDB.Name + " database will be considered for value deletion. \n\nProceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }

               

                if (dgResult == DialogResult.Yes)
                {



                    if (rbtnTagValuesOnly.Checked)
                    {
                        ProcessData(0);
                    }
                    else if (rbtnEFOnly.Checked)
                    {
                        ProcessData(1);
                    }
                    else
                    {
                        ProcessData(2);
                    }

                }
            } else
            {

                MessageBox.Show("Connect to the PI AF Server first.");
            }
            
        }
        

       

     
        /// <summary>
        /// Reports progress changes from the Background Worker to the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            var progressMsg = e.UserState as string;
            rtxtbMsgLog.AppendText(progressMsg);
            rtxtbMsgLog.ScrollToCaret();
        }

        
        private void MainForm_Load(object sender, EventArgs e)
        {

            this.Text = _AFDB.GetPath() + " - PI Analysis Output Bulk Deleter";
            numUpDownMaxSearchResults.Value = iMaxRetrievedObjects;
            numUpDownThreadCount.Value = iAFThreadCount;

            //Setting attribute filter initial state
            _AttributeFilter = new Dictionary<string, bool>();
            _AttributeListFilterIsActive = false;


            //Allowing to choose the proper PI Server

            _PIServers = new OSIsoft.AF.PI.PIServers();

            //Tries to connect to the primary first

            _PiServer = _PIServers.DefaultPIServer;

            

        }


        
        /// <summary>
        /// Helper method to encapsulate the parameters for the background worker
        /// </summary>
        /// <param name="opCode">Operation code: 0 - delete PI tag values only; 1 - delete EFs only; 2 - delete both PI tag values and EFs</param>
        private void ProcessData(int opCode)
        {
            List<object> bWorkArguments = new List<object>();

            OSIsoft.AF.Time.AFTime st, et;

           if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Please wait the current operation to be completed or cancel it first.");

            } else
            {

                //First checks if time range fields are valid and not empty
                if (txtbStartTime.Text!=String.Empty && txtbEndTime.Text!=String.Empty && OSIsoft.AF.Time.AFTime.TryParse(txtbStartTime.Text, out st) 
                    && OSIsoft.AF.Time.AFTime.TryParse(txtbEndTime.Text, out et))
                {
                    //Starts timer used to report the operation total duration
                    _OpDuration.Restart();

                    //Build object to pass to background worker
                    bWorkArguments.Add((DateTime)st);
                    bWorkArguments.Add((DateTime)et);
                    bWorkArguments.Add(_TargetElement);
                    bWorkArguments.Add(opCode);
                    bWorkArguments.Add(txtAttrFilter.Text); //The attr filter field

                    progressBar1.Visible = true;
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    //progressBar1.Style = ProgressBarStyle.Blocks;
                    progressBar1.MarqueeAnimationSpeed = 30;
                    backgroundWorker1.RunWorkerAsync(bWorkArguments);


                }
                else
                {

                    MessageBox.Show("Start time / end time fields are in the wrong format.");
                }
            }        

        }


        /// <summary>
        /// Gets the list of attributes used as PI Point DR outputs from all the analyses of a given element
        /// </summary>
        /// <param name="afelement">The input element</param>
        /// <returns></returns>
        private List<OSIsoft.AF.Asset.AFAttribute> GetAnalysesOutputPointDR(OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> elAnalyses)
        {

            //This list will hold all attributes used as Analyses outputs
            var analysisOutputs = new List<OSIsoft.AF.Asset.AFAttribute>();

            //Finding all analyses for a given element passed as parameter (afelement)
           // OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> elAnalyses = afelement.GetAnalyses(OSIsoft.AF.AFSortField.Name, OSIsoft.AF.AFSortOrder.Ascending, afelement.Analyses.Count + 1);
            foreach (var analysis in elAnalyses)
            {
                var configuration = analysis.AnalysisRule.GetConfiguration();
                foreach (var output in configuration.ResolvedOutputs)
                {

                    var attr = (OSIsoft.AF.Asset.AFAttribute)output.Attribute;

                    //Need to do all the checks first before trying to find if it's associated with a PI Point
                    if (attr != null)
                    {
                        try
                        {
                            if (attr.PIPoint != null)
                            {
                                //Since it's a PI Point DR, we add it to the list
                                analysisOutputs.Add(attr);

                            }
                        } catch
                        {
                            //do nothing
                        }
                        
                    }
                    
    

                }
            }

            return analysisOutputs;

        }

        private OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.EventFrame.AFEventFrame> 
            GetEFAnalyses(OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> elAnalyses)
        {

            return null;
        }

        private void btnPISystem_Click(object sender, EventArgs e)
        {

        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            OSIsoft.AF.AFDatabase dbSelected = OSIsoft.AF.UI.AFOperations.SelectDatabase(this, _AFServer, _AFDB);

            if (dbSelected == null)
            {
                MessageBox.Show("Database was not changed",  "Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                _AFDB = dbSelected;

                afElementFindCtrl1.Text = "";
                _TargetElement = null;
                afElementFindCtrl1.Database = _AFDB;
                
                this.Text = _AFDB.GetPath() + " - PI Analysis Output Bulk Deleter";

            }
            
        }

        private void numUpDownMaxSearchResults_ValueChanged(object sender, EventArgs e)
        {
            iMaxRetrievedObjects = (int) numUpDownMaxSearchResults.Value;
        }

        private void numUpDownThreadCount_ValueChanged(object sender, EventArgs e)
        {
            iAFThreadCount = (int) numUpDownThreadCount.Value;
        }

        private void ckbSelectedAttributesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSelectedAttributesOnly.Checked)
            {

                //Finding the attributes from target element

                if (_TargetElement == null)
                {
                    MessageBox.Show(this, "Please select the target element first.", "Target element not selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    ckbSelectedAttributesOnly.Checked = false;
                    
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    //Obtaining the list of analysis associated with the target element
                    OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> elAnalyses = _TargetElement.GetAnalyses(OSIsoft.AF.AFSortField.Name, OSIsoft.AF.AFSortOrder.Ascending, _TargetElement.Analyses.Count + 1);

                    List<OSIsoft.AF.Asset.AFAttribute> attributesForFilter = GetAnalysesOutputPointDR(elAnalyses);

                    //Populating the filter with all the attribute used as analyses outputs
                    foreach(var attr in attributesForFilter)
                    {
                        _AttributeFilter.Add(attr.GetPath(), false);

                    }

                    this.Cursor = Cursors.Default;

                    if (_AttributeFilter.Count == 0)
                    {
                        MessageBox.Show(this, "The target element does not contain any PI Point DR attribute used as analysis output", "No valid analysis outputs", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        //Disable single attribute filter textbox

                        txtAttrFilter.Clear();
                        txtAttrFilter.Enabled = false;

                        _AttributeListFilterIsActive = true;

                        Form myAttrSelection = new AttributeSelectionForm(ref _AttributeFilter);
                        var result = myAttrSelection.ShowDialog();

                        //Uncheck the selection if the user has cancelled the operation
                        if (result == DialogResult.Cancel)
                        {
                            ckbSelectedAttributesOnly.Checked = false;
                        }


                    }

                }

               
                
               

                Console.WriteLine(" test");

            }
            else
            {
                ReinitializeAttributeFilter();


            }
        }

        private void ckbIncludeChildElements_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIncludeChildElements.Checked)
            {
                ckbSelectedAttributesOnly.Enabled = false;
                ckbSelectedAttributesOnly.Checked = false;
                _AttributeListFilterIsActive = false;
            } else
            {
                ckbSelectedAttributesOnly.Enabled = true;
                _AttributeListFilterIsActive = true;
            }
        }

        /// <summary>
        /// Resets attribute filter to initial state (empty) and disable attribute filter check
        /// </summary>
        private void ReinitializeAttributeFilter()
        {

            _AttributeListFilterIsActive = false;
            _AttributeFilter.Clear();


            //Disable single attribute filter textbox

            txtAttrFilter.Clear();
            txtAttrFilter.Enabled = true;
        }

        private void afElementFindCtrl1_AFElementUpdated(object sender, CancelEventArgs e)
        {
            _TargetElement = afElementFindCtrl1.AFElement;

            //Resets attribute filter state
            ReinitializeAttributeFilter();
        }


        /// <summary>
        /// Returns the number of attributes that were filtered out
        /// </summary>
        /// <returns></returns>
        private int GetAttributeFilteredCount()

        {
            if (_AttributeFilter !=null)
            {
                var keysWithMatchingValues = _AttributeFilter.Where(kpv => !kpv.Value);
                return keysWithMatchingValues.Count();
            } else
            {
                return 0;
            }
            
                      

        }


        /// <summary>
        /// Allowing the user to select other members of the PI Server collective
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPIServers_Click(object sender, EventArgs e)
        {
            OSIsoft.AF.UI.AFOperations.ShowPIServers(this, _PiServer, false);
        }

        public static string WildcardToRegex(string pattern)
        {
            return "^" + System.Text.RegularExpressions.Regex.Escape(pattern.ToUpper())
                              .Replace(@"\*", ".*")
                              .Replace(@"\?", ".")
                       + "$";
        }

        
    }
}
