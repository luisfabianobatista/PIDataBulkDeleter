PI Analysis Output Bulk Deleter tool
(Posted by Fabiano Batista  in Fabiano Batista's Blog on 8-Aug-2016 4:38:00 PM)

Data backfilling and recalculations are very similar operations. For PI Analysis, backfilling means that the analyses will be calculated for the entered time range, assuming that the output tags do not contain any data for the same time range. For the current PI Analysis version (2016 SP1), recalculation is a backfilling operation assuming the output tag values need to be deleted prior to backfilling operation. In other words, since PI Analysis 2016 SP1 does not support recalculation (i.e., it cannot replace output values for PI tags), the PI Administrator needs to first delete existing values for all calculation output tags, before executing the backfilling operation. Right now, PI Analysis recalculation is a 2 step operation: deleting values and backfilling analysis after.
 
Deleting values for analyses recalculation purposes
In order to delete PI tag values, there are several methods already available. They are not difficult if you already know the tag names holding the data to be deleted. However, for my recent project, determining the all output tags used for analysis would be a laborious process if done manually. For this reason, since I was using PI Analysis 2016 SP1 (version that does not support recalculations) and that I could not wait the release of the next PI Analysis version (which will support recalculations), I had to quickly come up with a solution to help me deleting tags of my analysis very easily prior to recalculate the analysis outputs. This is why I decided to create the tool called “PI Analysis Output Bulk Deleter”. 
 
PI Analysis Output Bulk Deleter
The PI Analysis Output Bulk Deleter was designed to simplify the process of deleting analysis output values and event frames, all in context with the AF hierarchy. It will search check all Analysis output tags of a target element (and child elements, if selected) and delete their respective values for the desired time range. 
Although the existing event frames are deleted by PI Analysis service during a recalculation operation, there may be events that won’t be deleted by the PI Analysis service (ex: if analysis that created them has been deleted). The PI Analysis Output Bulk delete can delete all events belonging to a target AF element and their child elements.
The application is multithreaded, and it uses a traditional method where it has to retrieve from the PI Data Archive all values before deleting them. A new method (much faster) that does not require reading values prior to their deletion is supported in PI Server 2015 and beyond, but since my last project’s did not use PI Data Archive 2015, the support for the new method was not implemented in the current version of PI Analysis Output Bulk Deleter. But you can easily replace a couple of lines in the application, in case you want support for it.
 
The tool source code can be obtained from this GitHub repository: PI Analysis Output Bulk Deleter
 
Let’s see now how to use it.
Instructions
 
1. Pre-requisite:
•	Enable PI Buffer Subsystem in the client machine, buffering to the server that contains the tags which values will be deleted (optional)
2. Compile the application and Run the “PIDataBulkDeleter.exe”.
3. Select the desired PI AF server and database (see Figure 1).
 
 
Figure 1 - Selecting the AF Server and database of interest
4. Open the “Element Brower” window, and select the target element, which is the element for which you want to delete the analyses (see Figure 2). The name of the target element will be written into the “Target Element” field.
 
 
Figure 2 - Selecting the target AF element
 
 
5. If you want to delete analyses for the target element and his child elements, select the option “Include Child Elements” (see Figure 3).
•	This option disables the “Selected attributes only” option.
 
 
 
Figure 3 - Including the analyses outputs from child elements
 
6. If you do not want to touch the child elements, but want to delete only process a few output attributes from the target element, select the option “Selected Attributes Only”, then chose the attributes of interest. The “Select Attribute” window will appear (see Figure 6), where you will be able to select the attribute of interest from the target element only. You cannot use this option if the “Include Child Elements” option is selected.
7. If the “Selected Attributes Only” option is not selected, you can type a filter expression for the attribute path, using the known PI wildcards (*,?). It is not case-sensitive. If you want to consider all analysis outputs, leave it blank.
8. Enter the start time and end time of the deletion time range, using PI time format (see Figure 4).
 
 
Figure 4 - Entering the time range (PI time format supported)
9. Select one of the 3 deletion types (see Figure 5).
 
 
 
 
Figure 5 - Deletion types supported
 
 
Figure 6 - Selecting the attributes of interest
10. Edit the “Max Search Results” and “Max Thread” values (optional).
11. Click on “Delete Data” button and confirm to proceed with the data deletion.’
12. If the buffer was not enabled, click on the “PI Server” button, right-click on the collective name where the data to be deleted is located, and choose “Switch Member”. Then click on the “Delete Data” button to delete the same values from the other member of the collective (if applicable).
 
 
If the buffer was enabled, make sure all delete calls were processed by the PI Data Archive server before proceeding with the data backfill.
 
Example
 
Let’s assume that the feeder elements are child of the substation element, and you want to process all attributes starting with the string “Energy” from all feeder elements, for the time range between ‘*-1d’ and ‘*’. Also assume all feeder elements start with the string “Feeder”. To make sure only the attributes start with the string “Energy” from the feeders at a given site will be processed, select the substation element as the target (which is parent of the feeders), select the option “Include Child Elements” and type “*Feeder*|Energy* in the Attribute Path Filter field. Enter the time range for the date deletion, the deletion type and click on “Delete Data”. See Figure 7 for details.
 
It will then process the target all of their child elements. Only the attributes that match the Attribute Path Filter will be considered (if this field is not empty, otherwise all analyses outputs based on PI points will be considered). 
The tool will show the name of the attributes that match the filter criteria, and the number of values deleted for each one (if applicable), like in the example below:
 
Searching elements for data deletion...10 elements found.
Processing element 1 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Substation\Feeder 1...
Deleted 24 values from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Substation\Feeder 1|Energy Produced Last Hour.
Deleted 24 values from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Substation\Feeder 1|Energy Expected Breaker Open Last Hour.
Deleted 24 values from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Substation\Feeder 1|Energy Expected Breaker Comm Failure Last Hour.
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Substation\Feeder 1.
 
Also noticed that, for the target elements and turbine elements, no attribute was considered, since they did not match the filter criteria:
 
 
Processing element 3 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.001...
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.001.
Processing element 4 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.002...
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.002.
Processing element 5 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.003...
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.003.
Processing element 6 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.004...
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.004.
Processing element 7 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.005...
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.005.
Processing element 8 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.006...
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.006.
Processing element 9 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.007...
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Turbines\TST:WTG.007.
Processing element 10 of 10...
Deleting analyses output attributes from \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Substation...
Finished processing PI values deletion for element \\MYAFSERVER\User_FBatista_Test\Generation Fleet\Test Site\Substation.
Operation completed.
Operation duration: 0.03 minutes.
 
 
 
Figure 7 - Example of utilization

