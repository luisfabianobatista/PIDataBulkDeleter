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
    public partial class AttributeSelectionForm : Form
    {

        private Dictionary<string, bool> _SelectedAttributeNames;
        //private OSIsoft.AF.Asset.AFElement _TargetElement;
        public AttributeSelectionForm(ref Dictionary<string, bool> SelectedAttributeNames)
        {
            
            InitializeComponent();
            _SelectedAttributeNames = SelectedAttributeNames;
            
            //_TargetElement = targetElement;

           
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
         
            //Mark all check boxes
            for (int i = 0; i < checkedListBoxAttributeSelection.Items.Count; i++)
            {

                checkedListBoxAttributeSelection.SetItemChecked(i, true);

            }

        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            
            checkedListBoxAttributeSelection.ClearSelected();

            //Clear all check boxes
            for (int i = 0; i < checkedListBoxAttributeSelection.Items.Count; i++)
            {

                checkedListBoxAttributeSelection.SetItemChecked(i, false);
                
             }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxAttributeSelection.Items.Count; i++)
            {

                _SelectedAttributeNames[checkedListBoxAttributeSelection.Items[i].ToString()] = 
                    checkedListBoxAttributeSelection.GetItemChecked(i);
                               
            }

            this.Close();

        }

        private void AttributeSelectionForm_Load(object sender, EventArgs e)
        { 
            //Loads all the items from the dictionary onto the checkListBoxSelectedItems, and check the boxes accordingly
            foreach (var item in _SelectedAttributeNames)
            {
                checkedListBoxAttributeSelection.Items.Add(item.Key, item.Value);
            }
            
          }
    }
}
