## Hello User

This is a Task Management API 
Built using Dot Net Core , Entity Framework Core , Sql Server as DB.

## Steps to Start Using This Application 
1.) Open package manager console and run migration using 
command "Add-Migration '<name as per convinience first migartion is usually named as initial>'"

2.) Run command 'update-database' for updating the database 

3.) Build the Appliaction 

3.) Now run the application , seed data will automatically be added in the DB 

## General Overview Of Application

1.) JWT Token based Authentication is being used as Authentication Mechanism .
2.) You can generate a token via login API with test cred (username:admin,password:admin)
3.) Then Authorize the swagger APis using 'Bearer <xxxxx-xxxxx-xxxxx(token)>'
4.) Now You Can Access the Apis freely
