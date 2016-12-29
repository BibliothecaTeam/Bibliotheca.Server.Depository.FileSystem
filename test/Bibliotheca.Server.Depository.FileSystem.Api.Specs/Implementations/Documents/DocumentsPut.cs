using FluentBehave;
using System;

namespace test
{
    [Feature("DocumentsPut", "Updating existing document")]
    public class DocumentsPut
    {
        [Scenario("Document should be successfully modified")]
        public void DocumentShouldBeSuccessfullyModified()
        {
            GivenSystemContainsDocumentInBranchInProject("docs/updated-index.md", "Latest", "project-a");
            WhenUserUpdatesDocumentInBranchInProject("docs/updated-index.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeOk();
        }

        [Scenario("System have to return proper status code when project not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectNotExists()
        {
            GivenSystemNotContainsProject("project-not-exists");
            WhenUserUpdatesDocumentInBranchInProject("docs/index.md", "Latest", "project-not-exists");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when branch not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNotExists()
        {
            GivenSystemNotContainsBranchInProject("branch-not-exists", "project-a");
            WhenUserUpdatesDocumentInBranchInProject("docs/index.md", "branch-not-exists", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when document not exists")]
        public void SystemHaveToReturnProperStatusCodeWhenDocumentNotExists()
        {
            GivenSystemNotContainsDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            WhenUserUpdatesDocumentInBranchInProject("docs/index-not-exists.md", "Latest", "project-a");
            ThenSystemReturnsStatusCodeNotFound();
        }

        [Scenario("System have to return proper status code when project id not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenProjectIdNotSpecified()
        {
            GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            WhenUserUpdatesDocumentInBranchInProject("docs/index.md", "Latest", "");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when branch name not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenBranchNameNotSpecified()
        {
            GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            WhenUserUpdatesDocumentInBranchInProject("docs/index.md", "", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Scenario("System have to return proper status code when document uri not specified")]
        public void SystemHaveToReturnProperStatusCodeWhenDocumentUriNotSpecified()
        {
            GivenSystemContainsDocumentInBranchInProject("docs/index.md", "Latest", "project-a");
            WhenUserUpdatesDocumentInBranchInProject("", "Latest", "project-a");
            ThenSystemReturnsStatusCodeBadRequest();
        }

        [Given("System contains document in branch in project")]
        private void GivenSystemContainsDocumentInBranchInProject(string p0, string p1, string p2)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("User updates document in branch in project")]
        private void WhenUserUpdatesDocumentInBranchInProject(string p0, string p1, string p2)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("System returns status code Ok")]
        private void ThenSystemReturnsStatusCodeOk()
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

        [Given("System not contains document in branch in project")]
        private void GivenSystemNotContainsDocumentInBranchInProject(string p0, string p1, string p2)
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