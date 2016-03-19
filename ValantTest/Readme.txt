::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:::::::::::::::::::::::::::VALANT TEST::::::::::::::::::::::::::::::::::
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

How to run
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

Open the solution in Visual Studio, then left-click on Presentation.Api

Choose Debug -> Start New Instance,

Open your preferred browser and navigate to "http://localhost:42733/swagger/" 
(I set this by default).
(Usually visual studio will open a browser window pointing to the correct url)

Please do the same operation after, for Presentation.Web and you will be 
directed to the homepage of the web site.

Here you will have to panels one for managing the Inventory and other for the 
notifications.
Presentation.Web is a simple website that lets you interact with the API in a 
simple and transparent way.

Design
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
I've chosen a DDD (Domain Drive Design), I know that this kind of design 
can come at a relatively high cost,
However it's a great way for demonstrating maintainability, creative 
collaboration, isolation and encapsulation.

Other stuff
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
Unit Testing was done only on Application services, mainly because all 
logic is there.
Front-End was made just for testing purposes.
I've made a tweak to the requirements, so when you add an existing item 
the count is increased and when you take an item 
The count is decreased and the item is only remove from the repository if the 
count is zero!

signalR was used for notifications streaming.

:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

Thank you
Amândio Ribeiro

