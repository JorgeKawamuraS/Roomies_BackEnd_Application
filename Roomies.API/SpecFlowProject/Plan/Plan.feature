Feature: Plan
	Get a Plan

@mytag
Scenario: 0. Initialize some Group Intances
	When plans required attributes provided to initialize instances
		| Price | Name | Description  |
		| 39.0	| AA   | null		  |
		| 50.0	| BB   | null		  |

#1 - Get all plans
Scenario: 1. The administrator wants to see all the plans
	When the administrator goes to see all the plans, administrator list should return
		| Id | Price | Name | Description  |
		| 1	 | 39.0	 | AA   | null		   |
		| 2	 | 50.0	 | BB   | null		   |

		
#2 - Get plan by id
Scenario: 2. The user wants to see his plan
	When the user goes to Plan Page and click on plan with id 1
	Then user details should be
		| Id | Price | Name | Description  |
		| 1	 | 39.0	 | AA   | null		   |