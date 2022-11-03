# ChannelEngineWrapperAPI
A wrapper API for existing channel api to verify certain business case with two client App.
This is first fetching all Inprogress Orders , then from that it filters out top 5 In progress orders and then it is updating the stock for one of the product to 25.

- Console App
- Web App

These are the projects and their purpose - 
- ChannelEngine.Clinets.ConsoleApp -> Console application to see the run the business logics and see the output in the window, which displays all the cases.
- ChannelEngine.Clients.WebApp -> Web Application (MVC) to see the test cases in a web page in a tabular format if the three scenario was sucessfull or not and displaying as checked or unchecked.
- ChannelEngineWrapperAPI.BusinessLogics -> Class Library Containing all the business logics as mentioned in the problem statement.
- ChannelEngineApiClient -> Class library containing logics to call the channel engine api using httpclient.
- ChannelEngineContracts -> Class library containing all the project contracts.
- ChannelEngineApiModels -> Class library containing all the models.
- ChannelEngineWrapperAPI.BusineesLogics.Tests -> Unit Test for the Business Logic Class.
- ChannelEngineWrapperAPI.ChannelEngineAPIClient.Tests -> Unit Test for the API Client.

# Business Logics For Reference : 
> 1. Fetch all orders with status IN_PROGRESS from the API
 
> 2. From these orders, compile a list of the top 5 products sold (product name, GTIN
and total quantity), order these by the total quantity sold in descending order

> 3. Pick one of the products from these orders and use the API to set the stock of
this product to 25.


