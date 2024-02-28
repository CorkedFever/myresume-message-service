Feature: Message

A feature to test submitting messages and retrieving messages
also cleans up all left over messages so that they aren't in the database anymore

Scenario: I can submit a message
	Given I have a message <message>
	When I submit the message
	Then the message is in the database
	Examples: 
		| message |
		|'{"EmailAddress": "test@test2.com", "FirstName": "John", "LastName": "Smith", "Title": "Submit Test Title", "Message":"This is a message for the Submit Test"}'  |

Scenario: I can retrieve all messages for an email
	Given I have a message <message>
	And I have another message <message2>
	When I submit all the messages
	Then Then I can retrieve all the messages for the email
	Examples: 
		| message | message2 |
		|'{"EmailAddress": "test@test3.com", "FirstName": "John", "LastName": "Smith", "Title": "Submit Test Title", "Message":"This is a message for the Submit Test"}'  |'{"EmailAddress": "test@test3.com", "FirstName": "John", "LastName": "Smith", "Title": "Submit Test Title", "Message":"This is a message for the Submit Test"}'  |
