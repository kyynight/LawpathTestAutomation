Feature: NewUser
	In order to automate sign up process
	As a new user
	I want to be able to sign up successfuly

@mytag
Scenario: User successfully creates a Lawpath account
	Given John is on Lawpath registration page
	When he enters all the required registration information for new user "<firstName>" , "<lastName>", "<phone>" and "<password>"
	And he hits Sign Up
	Then his Lawpath account is created and is redirected to the welcome page

	Examples: 
	| firstName | lastName | phone      |  password   |
	| John      | Wick     | 0412345678 |  HelenDaisy |