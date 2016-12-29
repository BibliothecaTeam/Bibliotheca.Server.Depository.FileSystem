using FluentBehave;
using System;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesGetById", "Branch details")]
    public class BranchesGetById
    {
        [Scenario("Branch details must be available")]
        public void BranchDetailsMustBeAvailable()
        {
            GivenSystemContainsProjectWithBranch("project-a", "Latest");
            WhenUserWantsToSeeDetailsOfBranchFromProject("Latest", "project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenBranchNameIsEqualTo("Latest");
            ThenConfigurationInformationIsAvailable();
        }

        [Scenario("System have to return proper status code when branch not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNotExists()
        {
            GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            WhenUserWantsToSeeDetailsOfBranchFromProject("branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProject("project-not-exists");
            WhenUserWantsToSeeDetailsOfBranchFromProject("branch-not-exists", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when branch name not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            GivenSystemContainsProject("project-a");
            WhenUserWantsToSeeDetailsOfBranchFromProject("", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemContainsProjectWithBranch("project-a", "Latest");
            WhenUserWantsToSeeDetailsOfBranchFromProject("Latest", "");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains project with branch")]
        private void GivenSystemContainsProjectWithBranch(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("User wants to see details of branch from project")]
        private void WhenUserWantsToSeeDetailsOfBranchFromProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("Branch name is equal to")]
        private void ThenBranchNameIsEqualTo(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("Configuration information is available")]
        private void ThenConfigurationInformationIsAvailable()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System not contains branch in project")]
        private void GivenSystemNotContainsBranchInProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code NotFound")]
        private void ThenSystemReturnsStatusCodeNotFound()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System not contains project")]
        private void GivenSystemNotContainsProject(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System contains project")]
        private void GivenSystemContainsProject(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code BadRequest")]
        private void ThenSystemReturnsStatusCodeBadRequest()
        {
            throw new NotImplementedException("Implement me!");
        }

    }
}