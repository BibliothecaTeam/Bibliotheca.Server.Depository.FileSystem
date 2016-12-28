Feature: Deleting project

    As a user I want to delete specific project
    so that I can easly remove unecessary projects.

Scenario: Project should be successfully deleted
Given system contains project "project-to-delete"
When user deletes project "project-to-delete"
Then system returns status code Ok
    And project "project-to-delete" not exists

Scenario: System have to return proper status code when project not exists
Given system not contains projects "project-x"
When user deletes project "project-x"
Then system returns status code NotFound