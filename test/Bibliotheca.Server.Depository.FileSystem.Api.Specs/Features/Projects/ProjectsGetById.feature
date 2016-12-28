Feature: Project details

    As a user I want to download specific project details
    so that I can easly view information about project.

Scenario: Project details must be available
Given system contains project "project-a"
When user wants to see details of project "project-a"
Then system returns status code Ok
    And project name is equal to "Project A"
    And description is equal to "Test project A"
    And default branch is eqaul to "Latest"
    And group is equal to "Group"
    And visible branches contains only "Latest", "Release 1.0"
    And tags contains only "TagA", "TagB", "TagC"

Scenario: System have to return proper status code when project not exists
Given system not contains projects "project-x"
When user wants to see details of project "project-x"
Then system returns status code NotFound