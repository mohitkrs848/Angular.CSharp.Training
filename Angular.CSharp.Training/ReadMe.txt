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
25. guest user/ other user (4-5 user roles) and then authrising the parts based on that
26. Redis cache for performin loging/storing session/ maintain the user data (store the data untill token expires)
27. Uploading the media (let's it for user data)/ Bulk upload
28. Data processing (flat files to read/write the data)
29. logs store in databases
30. fetch the data from sql server and convert it into json and store it into the mongoDB  (write a service for the functionality)
31. to work with email, try to work with templates (create a template in database and then send the email)


Role in User table
Take input in Register page for role or any other page which only one admin user can access and then he can make other users as admin from the user table.
First admin user we can create in the Database itself.


issues: 
1. Dynamic filtering based on Department and designation
2. ManagerID not becoming null if we change the designation
3. ProjectID is coming in the table despite of filling the ProjectName


Azure- using solera.com ( deploy in app service in azure/ adn database in azure))
sandboz account fo message / or email api (smtp port/rest api) - try to use rest api from 3rd party (Microsoft smtp service try someone else)
sql express deployed in aws ec2/azure vm with minimal configuration and connect with local point


either to use auth (3rd party identity provider) or Auth0 (provide account)/ OpenId
for gmail or facebook (need one developer account for client id)
Azure AD, b2c (can enable MFA)
we can utilise b2c (better approach) - for social identity provider

seq for database logging (for audit trail) but requires hosting on server

Business layer will be hided to logic layer - need to implement concrete class (Automapper)

Task: Shivansh
- Logout after apopup and delete employee/project after popup
- Employees table (project Id) is coming inplace of that we need project name
- right up corner there should be the User name and also a small point for showing if user is admin or not
- my dashboard should show trends by default
- Dynamic filtering on Add employees ( like a Software engineer should be from Enginerring department only)

Mohit: 
- Backend layers structure
- synronized methods
- Dashboard updation with dynamic x,y parameters
- logs store in databases
- search functionality (combine all the fields in single search input)