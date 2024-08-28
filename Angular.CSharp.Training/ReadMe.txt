1. Need to fix Adding Functionality - Done
2. Live changing of table (fetching from Database) on page - Done
3. Fixing Update Function - Done

4. Add login functionality, first with only some hardcoded values, With SPA, Header should be static and Logout functionality
5. User Preferences - Authorisation (based on this view and permission will be provided)

6. Popups should be there - using alerts it's done, will modify more
7. Page Summary (About Us, Contact Us, Privacy Policy, Terms & Conditions) - static page - Done
8. Templates for each section - Done

9. Validation in Model/View and Controller
10. Filters (based on user access level)
11. Dashboard using graphs, charts
12. AddEmployee button will should have popup then it will add data
13. Edit Employee should also have a popup

14. Create custom error messages for each validation (age, email, name)
15. Getting all messages to the console

Code-First vs Database-First approach to develop





Employee : EmpId (primary key), EmpName, EmpEmail, EmpAge, EmpDesignation, EmpSalary, EmpLocation, EmpStatus, EmpManagerID (Foreign Key - can be null), EmpDepartmentID (Foreign Key)

Project : ProjectID (PK), ProjectName, ProjectManagerID (FK), ProjectStatus

Department : DeptID (PK), DeptName, 

Manager : ManagerID (PK), ManagerName


1. RoleId in table (1 -admin, 2 - manager, 3 - guest)
2. Professional colours in Main page. (light colours) - done
3. Sizes and other things for button (Add (+ icon) and Edit (pencil icon))) - done
4. Authentication (Login, Logout, Session) - done
5. User Preferences (based on role) - Done
6. Filter on excel table - Done / more to customize it
7. pagination - done
8. Sorting - done
9. Add more charts, graphs using queries, trends (Dashboard dybnamic) - In progress
10. Send email/ attachment - In progress (as a second option side of reporting with charts as attachments)
11. Export to excel/csv/pdf - done
12. Once user got idle user got logout automatically, password expiry/reset/compliance
13. Login through 3rd party integration (Normal/ social identity provider)
14. roles should be 4-5
15. Redis cache implemented
16. Integration with non relational databases (MongoDB)
17. Azure/AWS storage account integration (PowerBI dashboard)
18. Other modules (Finance, Tax, Management)
19. Forgot the password/ security question
20. Host the site in IIS
21. Add User option in template for admin
22. Admin can make user Active or InActive
23. Identifier for user like a user is leaving
24. Within a grid we can have nested grid (under manager after employee)



Role in User table
Take input in Register page for role or any other page which only one admin user can access and then he can make other users as admin from the user table.
First admin user we can create in the Database itself.


issues: 
1. Dynamic filtering based on Department and designation
2. ManagerID not becoming null if we change the designation
3. ProjectID is coming in the table despite of filling the ProjectName