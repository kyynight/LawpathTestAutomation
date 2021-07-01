Feature: IncorrectInput
	In order to check for errors in input field
	As a product owner
	I want the application to validate for a proper email input field

@mytag
Scenario: User sign up with a badly formatted email
	Given John is on Lawpath registration page
	When he enters registration information with a badly formatted email "<firstName>" , "<lastName>", "<phone>", "<email>" and "<password>"
	And he hits Sign Up
	Then an error of badly formatted email should be thrown

	Examples: 
	| firstName | lastName | phone  | email | password   |
	| John      | Wick     | 124356 | 123   | HelenDaisy |