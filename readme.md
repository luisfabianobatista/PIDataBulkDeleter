**PI Analysis Output Bulk Deleter tool**

Data backfilling and recalculations are very similar operations. For PI Analysis, backfilling means that the analyses will be calculated for the entered time range, assuming that the output tags do not contain any data for the same time range. For the current PI Analysis version (2016 SP1), recalculation is a backfilling operation assuming the output tag values need to be deleted prior to backfilling operation. In other words, since PI Analysis 2016 SP1 does not support recalculation (i.e., it cannot replace output values for PI tags), the PI Administrator needs to first delete existing values for all calculation output tags, before executing the backfilling operation. Right now, PI Analysis recalculation is a 2 step operation: deleting values and backfilling analysis after.
 
**Deleting values for analyses recalculation purposes**

In order to delete PI tag values, there are several methods already available. They are not difficult if you already know the tag names holding the data to be deleted. However, for my recent project, determining the all output tags used for analysis would be a laborious process if done manually. For this reason, since I was using PI Analysis 2016 SP1 (version that does not support recalculations) and that I could not wait the release of the next PI Analysis version (which will support recalculations), I had to quickly come up with a solution to help me deleting tags of my analysis very easily prior to recalculate the analysis outputs. This is why I decided to create the tool called “PI Analysis Output Bulk Deleter”. 
 
**PI Analysis Output Bulk Deleter**

The PI Analysis Output Bulk Deleter was designed to simplify the process of deleting analysis output values and event frames, all in context with the AF hierarchy. It will search check all Analysis output tags of a target element (and child elements, if selected) and delete their respective values for the desired time range. 
Although the existing event frames are deleted by PI Analysis service during a recalculation operation, there may be events that won’t be deleted by the PI Analysis service (ex: if analysis that created them has been deleted). The PI Analysis Output Bulk delete can delete all events belonging to a target AF element and their child elements.
The application is multithreaded, and it uses a traditional method where it has to retrieve from the PI Data Archive all values before deleting them. A new method (much faster) that does not require reading values prior to their deletion is supported in PI Server 2015 and beyond, but since my last project’s did not use PI Data Archive 2015, the support for the new method was not implemented in the current version of PI Analysis Output Bulk Deleter. But you can easily replace a couple of lines in the application, in case you want support for it.

**Instructions**

For instructions, check [here](PIDataBulkDeleter-Instructions.docx)

**Licensing**

Copyright 2016 Luis Fabiano Batista, OSIsoft, LLC.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

Please see the file named [LICENSE](license.txt).
