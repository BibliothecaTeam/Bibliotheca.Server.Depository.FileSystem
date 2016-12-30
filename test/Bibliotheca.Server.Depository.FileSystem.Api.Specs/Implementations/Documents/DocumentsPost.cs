using FluentBehave;
using System;

namespace test
{
    [Feature("DocumentsPost", "Creating a new documents")]
    public class DocumentsPost
    {
        //[Scenario("Documents should be successfully added")]
        public void DocumentsShouldBeSuccessfullyAdded()
        {
            GivenSystemNotContainsDocumentInBranchInProject("docs/new-document.md", "Latest", "project-a");
            WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeOk();
            ThenNewDocumentExistsInBranchInProject("", "Latest", "project-a");
        }

        //[Scenario("System have to return proper status code when project not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProject("project-not-exists");
            WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "Latest", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        //[Scenario("System have to return proper status code when branch not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNotExists()
        {
            GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        //[Scenario("System have to return proper status code when project id not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemNotContainsDocumentInBranchInProject("docs/new-document.md", "Latest", "project-a");
            WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "Latest", "");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        //[Scenario("System have to return proper status code when branch name not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            GivenSystemNotContainsDocumentInBranchInProject("docs/new-document.md", "Latest", "project-a");
            WhenUserAddsDocumentToBranchInProject("docs/new-document.md", "", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        //[Scenario("System have to return proper status code when document uri not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenDocumentUriNotSpecified()
        {
            GivenSystemNotContainsDocumentInBranchInProject("docs/new-document.md", "Latest", "project-a");
            WhenUserAddsDocumentToBranchInProject("", "Latest", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System not contains document in branch in project")]
        private void GivenSystemNotContainsDocumentInBranchInProject(string p0, string p1, string p2)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("User adds document to branch in project")]
        private void WhenUserAddsDocumentToBranchInProject(string p0, string p1, string p2)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("New document exists in branch in project")]
        private void ThenNewDocumentExistsInBranchInProject(string p0, string p1, string p2)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System not contains project")]
        private void GivenSystemNotContainsProject(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code NotFound")]
        private void ThenSystemReturnsStatusCodeNotFound()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("System not contains branch in project")]
        private void GivenSystemNotContainsBranchInProject(string p0, string p1)
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