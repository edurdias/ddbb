> # The content presented below is a draft only. Changes will mostly likely happen.

##<a name="CodeOfConduct"></a>Code of Conduct
As contributors and maintainers of the **ddbb** project, we pledge to respect everyone who contributes by posting issues, updating documentation, submitting pull requests, providing feedback in comments, and any other activities.

Communication must be constructive and never resort to personal attacks, trolling, public or private harrassment, insults, or other unprofessional conduct.

We promise to extend courtesy and respect to everyone involved in this project regardless of gender, gender identity, sexual orientation, disability, age, race, ethnicity, religion, or level of experience. We expect anyone contributing to the project to do the same.

If any member of the community violates this code of conduct, the maintainers of the project may take action, removing issues, comments, and PRs or blocking accounts as deemed appropriate.


##<a name="QuestionsOrProblems"></a>Questions or Problems
If you have questions about how to use **ddbb**, please direct these to the [StackOverflow][stackOverflow].

##<a name="IssuesAndBugs"></a>Issues and Bugs

Issues and feature requests are submitted through the project's Issues section on GitHub. Please use the following guidelines when you submit issues and feature requests:

- Make sure the issue is not already reported by searching through the list of issues
- Provide detailed description of the issue including the following information:
	* Which feature the issue appears in
	* Under what circumstances the issue appears
	* What is desired behavior
	* What is breaking
	* What is the impact (things like loss or corruption of data, compromizing security, disruption of service etc.)
	* Any code that will be helpful to reproduce the issue


Issues are regularly reviewed and updated with additional information by the core team. Sometimes the core team may have questions about particular issue that might need clarifications so, please be ready to provide additional information.

##<a name="FeatureRequests"></a>Feature Requests

You can request a new feature by submitting an issue to our [GitHub Repository][repository]. When opening any feature requests, consider including as much information as possible, including:

* Detailed scenarios enabled by the feature or DCR.
* Information about your use case or additional value your business or site will see from the feature.
* Any design tips or estimation ideas you may have considered already.
* Make note of whether you are opening an issue you would like the Microsoft team or another community member to work on or if you are looking to design & develop the feature yourself.
* Any potential caveats or concerns you may have already thought about.
* A miniature test plan or list of test scenarios is always helpful.

If you would like to implement a new feature then consider what kind of change it is:

* **Major Changes** that you wish to contribute to the project should be discussed first so that we can better coordinate our efforts, prevent duplication of work, and help you to craft the change so that it is successfully accepted into the project.

* **Small Changes** can be crafted and submitted to the [GitHub Repository][repository] as a Pull Request.

##<a name="SubmissionGuidelines"></a>Submission Guidelines

###<a name="SubmittingAnIssue"></a>Submitting an Issue

Before you submit your issue search the archive, maybe your question was already answered.

If your issue appears to be a bug, and hasn't been reported, open a new issue. Help us to maximize the effort we can spend fixing issues and adding new features, by not reporting duplicate issues. 

**If you get help, help others.**

###<a name="SubmittingAPullRequest"></a>Submitting a Pull Request
You can then start to make modifications to the code in your local Git repository. This can be done in your local dev branch or, if you prefer, in a branch out of dev. In the simplest scenario, working directly on dev, you can commit your work with following commands:


1. Add and commit your local changes
<br/>`git commit -a`
2. Push your changes from your local repository to your github fork
<br/>`git push`

Once your code is in your github fork, you can then submit a pull request for the team's review. You can do so with the following commands:

1. In GitHub click on the Pull Request button ![](https://azure.github.io/images/button-pull-request.jpg)
2. In the pull request select your fork as source and edurdias/ddbb as destination for the request
3. Write detailed message describing the changes in the pull request
4. Submit the pull request for consideration by the Core Team


If there are conflicts between your fork and the main project one, github will warn you that the pull request cannot be merged. If that's the case, you can do the following:


1. Add remote to your local repository using the following Git commands
<br/>`git remote add upstream -f git@github.com:edurdias/ddbb`

2. Update your local repository with the changes from the remote repository by using the following Git commands (make sure you're in the branch you're submitting the code from)
<br/>`git merge upstream/dev`

3. Resolve any conflicts locally and finally do another push with the command
<br/>`git push`

Please keep in mind that not all requests will be approved. Requests are reviewed by the Core Team on a regular basis and will be updated with the status at each review. If your request is accepted you will receive information about the next steps and when the request will be integrated in the main branch. If your request is rejected you will receive information about the reasons why it was rejected.

##<a name="BranchingModel"></a>Branching Model


##<a name="CodingRules"></a>Coding Rules

##<a name="CommitMessageGuidelines"></a>Commit Message Guidelines


[repository]: https://github.com/edurdias/ddbb
[stackOverflow]: https://stackoverflow.com/questions/tagged/ddbb