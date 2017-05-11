# aspnet-core-demo
This is a demo project to understand the concepts of Asp.Net core alongwith EntityFramework core.   
Following is the stack used:
- [ASP.Net core 1.1.1](https://docs.microsoft.com/en-us/aspnet/core/)   
- [EntityFramework core 1.1.0](https://docs.microsoft.com/en-us/ef/core/index)   
- [Serilog for Asp.Net core 1.0.1](https://serilog.net/)   

The solution contains an IoTDemo.API project which is of type Web API in Asp.Net core.

## Setup guide
- clone the repository on your machine
- you will need Visual Studio 2017 to run this project
- clean the solution, this shall restore all the nuget packages used in the solution.
- goto appsettings.json file in IoTDemo.API project and use the appropriate connectionstring (localdb/SQLServer), whichever you want.

## Running the project
- Run the IotDemo.API project in IIS Express configuration
- project uses code first migration, the database will be created and seeded on startup if one does not exist already.
- since there is no UI, you will have to test the api's from a tool like Postman/SOAP UI.
- Following are the methods
  - iotdata/getall : Gets all the data from table IoTData
  - iotdata/get?id={integer_id} :Gets a particular record if id is a valid & existing integer
  - iotdata/write : This is a POST method to insert record 
    - i. name : name of the data to be inserted, if this name already exists, that will be used else a new record will be inserted in IotDataName
    - ii. value: should be a valid floating point value
    - iii. date: this is an optional field, if blank, current datetime will be used
    - iv. key: Every insertion/deletion is validated with a key, keys are stored in IoTKeys table and initially, 2 keys will be seeded in db.
- API's will return a 422 status code and a customised error message if validation fails.
   
## Hosting in local IIS
- publish the IoTDemo.API project to a directory on your machine (Use folder publish method, published package will contain all the required dll's to run aspnet core application).
- You will need to install AspNetCoreModule in IIS modules, use web platform installer to install the same.
- Reset the IIS once this module is installed.
- create a new website in IIS
  - Set the App Pool's .NET CLR version to No Managed Code
  - Set the virtual directory to the published folder
  - Add record to the site in your hosts file
  - Browse the site from IIS and test the API's from the tool (Postman/ SOAP UI)
  
   
## Author
Sagar Yerva
