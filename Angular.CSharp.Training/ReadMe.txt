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
2. Professional colours in MAin page. (light colours)
3. Sizes and other things for button (Add (+ icon) and Edit (pencil icon)))
4. Authentication (Login, Logout, Session)
5. User Preferences (based on role)