Feature: Registering new Users

Scenario: Non-existing users should be created as they post their first message
	Given Non-existing user "John"
	When Post message "hello"
	Then in storage is registered new user with username "John"
	And user "John" has a message "hello" on his timeline
