using FluentBehave;
using System;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Implementations.Branches
{
    [Feature("BranchesDelete", "Deleting branch")]
    public class BranchesDelete
    {
        [Scenario("Branch should be successfully deleted")]
        public void BranchShouldBeSuccessfullyDeleted()
        {
            GivenSystemContainsProjectWithBranch("project-a", "branch-to-delete");
            WhenUserDeletesBranchFromProject("branch-to-delete", "project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenBrachInProjectNotExists("branch-to-delete", "project-a");
        }

        [Scenario("System have to return proper status code when branch not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNotExists()
        {
            GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            WhenUserDeletesBranchFromProject("branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProject("project-not-exists");
            WhenUserDeletesBranchFromProject("branch-not-exists", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when branch name not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            GivenSystemContainsProject("project-a");
            WhenUserDeletesBranchFromProject("", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemContainsProjectWithBranch("project-a", "branch-to-delete");
            WhenUserDeletesBranchFromProject("branch-to-delete", "");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains project with branch")]
        private void GivenSystemContainsProjectWithBranch(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("User deletes branch from project")]
        private void WhenUserDeletesBranchFromProject(string p0, string p1)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("Brach in project not exists")]
        private void ThenBrachInProjectNotExists(string p0, string p1)
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