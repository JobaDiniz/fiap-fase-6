# Contributing

## Code
TODO

## Development setup
TODO

## Integration tests
Before running integration tests within Visual Studio, you need to compose the docker image.

In the `Data` integration tests, there is a file `docker-compose.yml`. You need to run ` docker-compose -f src/infrastructure/ifood.Reviews.Data.Tests/docker-compose.yml up
` before running Visual Studio tests, otherwise the _Mongo_ database will not be available.