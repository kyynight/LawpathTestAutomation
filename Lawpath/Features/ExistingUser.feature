Feature: ExistingUser
	In order to avoid duplicate sign up
	As an existing user
	I want the system to throw an error when there's a duplication in sign up

@mytag
Scenario: User sign up with a duplicated account
	Given John is on Lawpath registration page
	When he enters registration information of an existing account "<firstName>" , "<lastName>", "<phone>", "<email>" and "<password>"
	And he hits Sign Up
	Then an error of account duplication should be thrown

	Examples: 
	| firstName | lastName | phone      | email                     | password   |
	| John      | Wick     | 124356     | 123@333.com               | HelenDaisy |