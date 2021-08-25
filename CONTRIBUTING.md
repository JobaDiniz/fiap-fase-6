# Contributing

## Commit message guidelines
Each commit message consists of a **header** and a **body**. The header has a special format that includes a **type**, a **scope** and a **subject**:

```
<type>(<scope>): <subject>
<BLANK LINE>
<body>
```
The **header** is mandatory and the **scope** of the header is optional. The **body** is optional; you can use the body to explain in further detail the work you've done.
Any line of the commit message (header) cannot be longer 80 characters. This allows the message to be easier to read on Github as well as in various git tools.

#### Do not capitalize the message
- **Good**: `fix(review): add validation messages`
- **Bad**: `Fix(Review): Add validation messages`

### Type
Must be one of the following:

- `build`: Changes that affect the build system
- `config`: Changes to any of the configurations files
- `docs`: Documentation only changes
- `resource`: Changes to the resources files
- `feat`: A new feature
- `fix`: A bug fix
- `perf`: A code change that improves performance
- `refactor`: A code change that neither fixes a bug nor adds a feature
- `style`: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc). Or copyright headers.
- `test`: Adding missing tests or correcting existing tests

### Scope
The scope should be the name of the module affected (as perceived by the person reading the changelog generated from commit messages).

### Subject
The subject contains a succinct description of the change:

- Use the imperative, present tense: "change" not "changed" nor "changes"
- Don't capitalize the first letter
- No dot (.) at the end

### Body
Just as in the subject, use the imperative, present tense: "change" not "changed" nor "changes". The body should include the motivation for the change and contrast this with previous behavior.

*The body is not required.*

## Development setup
- Install Visual Studio or Visual Studio Code
- Install [Docker Desktop](https://docs.docker.com/desktop/)
  - [Windows](https://docs.docker.com/desktop/windows/install/) 
  - [Mac](https://docs.docker.com/desktop/mac/install/)  

Within the *solution* file (`iFood.sln`), set the `docker-compose` **project** as a startup project. The `docker-compose` project will build all the required containers to run the application in debug mode.

![image](https://user-images.githubusercontent.com/165290/130785309-65aeb2af-6d55-41eb-92e6-bc0ce6a29045.png)

After running the application, go to `https://localhost:62773/swagger/index.html` to test the API.

## Integration tests
Before running integration tests within Visual Studio, you need to compose the docker image **for tests**. These integration docker images are different from the docker-compose project mentioned before.

In the `iFood.Reviews.Data.Tests` integration test project, there is a file `docker-compose.yml`.

### Steps
- Run `docker-compose -f src/infrastructure/ifood.Reviews.Data.Tests/docker-compose.yml up`
- Run the `iFood.Reviews.Data.Tests` tests wthin Visual Studio

Those tests also run automatically using *github actions* no every *pull request*. This was configured using the [CI workflow](./.github/workflows/ci.yml)

## Links
- [Apache Kafka CLI commands cheat sheet](https://medium.com/@TimvanBaarsen/apache-kafka-cli-commands-cheat-sheet-a6f06eac01b#a1a2)
