Feature: Projects list

    As a user I want to have list of projects
    so that I can easly view all existing projects.

Scenario: List of projects must be available
Given system contains projects
When user wants to see all projects
Then system returns status code Ok
    And system returns all available projects
    And projects are sorted alpabetically

Scenario: List must contains only projects with correct configuration
Given system contains projects
    And project "empty-project" does not have configuration file
When user wants to see all projects
Then system returns status code Ok
    And system returns projects without "empty-project"