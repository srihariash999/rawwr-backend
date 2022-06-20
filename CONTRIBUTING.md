# Contribution guidelines

First of all, thanks for thinking of contributing to this project. :smile:

Before sending a Pull Request, please make sure that you're assigned the task on a GitHub issue.

- If a relevant issue already exists, discuss on the issue and get it assigned to yourself on GitHub.
- If no relevant issue exists, open a new issue and get it assigned to yourself on GitHub.

Please proceed with a Pull Request only after you're assigned. It'd be sad if your Pull Request (and your hardwork) isn't accepted just because it isn't ideologically compatible.

# Developing rawwr

1. Install with

    ```sh
    git clone https://github.com/srihariash999/rawwr-backend
    cd rawwr-backend
    dotnet restore
    dotnet run
    ```

2. Make your changes in a different git branch (say, `brand-new-branch`).

3. (Optional) To test whether the project is working as you expect after your changes, run the project with your dev environment setup and test the APIs using swagger UI.


4. NOTE: while creating a PR remove any local setup you have configured in appsettings.json or otherwise.

5. Give a detailed description in the PR's description mentioning all the changes you have done and the files you have changed. Also link all relevant issues that your PR deals with. 
