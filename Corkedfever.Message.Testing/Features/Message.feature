Feature: Message

A feature to test submitting messages and retrieving messages
also cleans up all left over messages so that they aren't in the database anymore

@tag1
Scenario: I can submit a message
	Given I have a message
	When I submit the message
	Then the message is in the database

Scenarios: I can retrieve all messages for an email
	Given I have a message
	And I have another message
	When I retrieve all messages for an email
	Then I get all the messages

Scenarios: I can retrieve a message by id
	Given I have a message
	When I retrieve the message by id
	Then I get the message

