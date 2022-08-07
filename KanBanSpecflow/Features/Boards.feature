Feature: Boards

Scenario Outline: Check List of Workspaces
	Given I have workspaces in the system
	| UUID                                 | Name        |
	| d9092be9-555d-4d9d-a8d3-8ac3b181d291 | Workspace_A |
	| ff685cb0-447e-416e-ba1d-bcc0586ff0d1 | Workspace_B |

	Then I receive a list of workspaces
	| UUID                                 | Name        |
	| d9092be9-555d-4d9d-a8d3-8ac3b181d291 | Workspace_A |
	| ff685cb0-447e-416e-ba1d-bcc0586ff0d1 | Workspace_B |
	
Scenario Outline: Check Boards within Workspaces
	Given I have boards in the system
	| Workspace_UUID | UUID | Name |
	| d9092be9-555d-4d9d-a8d3-8ac3b181d291 | a9092be9-555d-4d9d-a8d3-8ac3b181d290 | Board_A |
	| ff685cb0-447e-416e-ba1d-bcc0586ff0d1 | bf685cb0-447e-416e-ba1d-bcc0586ff0d0 | Board_B |
	| ff685cb0-447e-416e-ba1d-bcc0586ff0d1 | cf685cb0-447e-416e-ba1d-bcc0586ff0d1 | Board_C |
	| ff685cb0-447e-416e-ba1d-bcc0586ff0d1 | df685cb0-447e-416e-ba1d-bcc0586ff0d1 | Board_D |

	Then I receive a list of boards specific to workspace 'd9092be9-555d-4d9d-a8d3-8ac3b181d291'
	| Workspace_UUID | UUID | Name |
	| d9092be9-555d-4d9d-a8d3-8ac3b181d291 | a9092be9-555d-4d9d-a8d3-8ac3b181d290 | Board_A |

	Then I receive a list of boards specific to workspace 'ff685cb0-447e-416e-ba1d-bcc0586ff0d1'
	| Workspace_UUID | UUID | Name |
	| ff685cb0-447e-416e-ba1d-bcc0586ff0d1 | bf685cb0-447e-416e-ba1d-bcc0586ff0d0 | Board_B |
	| ff685cb0-447e-416e-ba1d-bcc0586ff0d1 | cf685cb0-447e-416e-ba1d-bcc0586ff0d1 | Board_C |
	| ff685cb0-447e-416e-ba1d-bcc0586ff0d1 | df685cb0-447e-416e-ba1d-bcc0586ff0d1 | Board_D |
	
Scenario Outline: Check Boards details
	Given I have board details in the system
	| Board_UUID                           | Name    | Author     | CreatedDate |
	| a9092be9-555d-4d9d-a8d3-8ac3b181d290 | Board_A | ScriptPunk | 5/10/2022   |
	| bf685cb0-447e-416e-ba1d-bcc0586ff0d0 | Board_B | ScriptPunk | 5/10/2022   |

	Then I receive details specific to board 'a9092be9-555d-4d9d-a8d3-8ac3b181d290' and ''
	| Board_UUID                           | Name    | Author     | CreatedDate |
	| a9092be9-555d-4d9d-a8d3-8ac3b181d290 | Board_A | ScriptPunk | 5/10/2022   |

	Then I receive details specific to board 'bf685cb0-447e-416e-ba1d-bcc0586ff0d0' and ''
	| Board_UUID                           | Name    | Author     | CreatedDate |
	| bf685cb0-447e-416e-ba1d-bcc0586ff0d0 | Board_B | ScriptPunk | 5/10/2022   |

	Then I receive details specific to board 'a9092be9-555d-4d9d-a8d3-8ac3b181d290' and 'bf685cb0-447e-416e-ba1d-bcc0586ff0d0'
	| Board_UUID                           | Name    | Author     | CreatedDate |
	| a9092be9-555d-4d9d-a8d3-8ac3b181d290 | Board_A | ScriptPunk | 5/10/2022   |
	| bf685cb0-447e-416e-ba1d-bcc0586ff0d0 | Board_B | ScriptPunk | 5/10/2022   |
