Feature: Rule
	Get, Update and Delete a Rule

@mytag
Scenario: 0. Initialize some Rule Intances
	When rules required attributes provided to initialize instances
		| Title     | Description			  | 
		| Norma1	| Normas de convivencia 1 |
		| Norma2	| Normas de convivencia 2 |
		| Norma3    | Normas de convivencia 3 |


#1 - Update a group
Scenario: 1. The landlord wants to update a rule
	When the landlord complete the form to update the rule with Id 3 and click the Update button
		| Title     | Description			    |  
		| Norma - 3	| Normas de convivencia - 3 |


#2 - Get rule by id
Scenario: 2. The landlord wants to see a rule details
	When the landlord select rule with id 3
	Then rule details should be
		| Id | Title		| Description			    |  
		| 3  | Norma - 3	| Normas de convivencia - 3 |


#3 - Get all rules 
Scenario: 3. The landlord wants to see all rules
	When the landlord goes to Rules Page, rule list should return
		| Id | Title     | Description			    | 
		|  1 | Norma1	| Normas de convivencia 1   |
		|  2 | Norma2	| Normas de convivencia 2   |
		|  3 | Norma - 3 | Normas de convivencia - 3 |


#4 - Delete a rule
Scenario: 4. The landlord wants to delete his rule
	When the landlord with id 3 click the Delete Rule button
	Then the landlord with id 1 is removed and removed rule details should be
		| Id | Title		| Description			    |  
		| 3  | Norma - 3	| Normas de convivencia - 3 |