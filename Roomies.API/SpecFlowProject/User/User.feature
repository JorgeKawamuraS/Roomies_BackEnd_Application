Feature: User
	Create and Get an User

@mytag
Scenario: 0. Initialize some User Intances
	When users required attributes provided to initialize instances 
		| FirstName   | LastName  | UserName | ProfileId  | 
		| Cristiano   | Ronaldo   | ElBicho  |     1      | 


#1 - Create an user
Scenario: 1. The user wants to register
	When the user complete the form with the required fields and click the Register button
		| FirstName   | LastName  | UserName    | ProfileId  | 
		| Daniel	  | Peredo    | EraHoyRamon |     1	     |


#2 - Get all users
Scenario: 2. The administrator wants to see all users
	When the administrator goes to Users Page, user list should return
		| FirstName   | LastName  | UserName    | ProfileId  | 
		| Cristiano   | Ronaldo   | ElBicho     |     1      | 
		| Daniel	  | Peredo    | EraHoyRamon |     1	     |
