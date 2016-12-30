Feature: Branches list

    As a user I want to have list of branches from project
    so that I can easly view all existing branches.

Scenario: List of branches must be available
Given system contains project "project-a" with branches "Latest", "Release 1.0"
When user wants to see all branches from project "project-a"
Then system returns status code Ok
    And system returns branches "Latest", "Release 1.0" from project "project-a"
    And branches are sorted alpabetically

Scenario: List must contains only branches with correct configuration
Given system contains project "project-a" with  branches "Latest", "Release 1.0"
    And branch "empty-branch" in project "project-a" does not have configuration file
When user wants to see all branches from project "project-a"
Then system returns status code Ok
    And system returns branches from project "project-a" without "empty-branch"

Scenario: System have to return proper status code during getting list of branches when project not exists
Given system not contains project "project-not-exists"
When user wants to see all branches from project "project-not-exists"
Then system returns status code NotFound

Scenario: System have to return proper status code during getting list of branches when project id not specified
Given system contains project "project-a" with  branches "Latest", "Release 1.0"
When user wants to see all branches from project ""
Then system returns status code BadRequest