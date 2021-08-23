# Containerization Strategy
![continuous integration](https://github.com/JobaDiniz/fiap-fase-6/actions/workflows/ci.yml/badge.svg)

Phase 6 of Fiap MBA Solutions Architecture course.

Imagine that you are the Solution Architect of `iFood` and your mission is:

- Research and describe about the company's Core Business
- Propose an architecture model through an application diagram (using TOGAF or another free diagram, preferably), composed by the applications that implement Core Business.
- Describe which technologies you would use in the architecture and why.
- What problems present to solve with the architecture.
- What are the challenges to deploy the architecture.
- **Implement a Microservice in Docker** of an application present in the proposed architectural diagram. The Microservice code should be considered in a public GitHub and contain as layers Domain, Repository, Service and Controller.

## Architecture
This repository shows a *Reviews* microservice of `iFood`. When an order is finalized, the customer can review the store. In the mobile app or website, customers can view the reviews about the store.

### Implementation
The implementation exposes an API where the mobile app or website can query reviews for a given store.

This service is responsible to
- Listen to store created events and create store in its own database
- Listen to reviews created events and store reviews in its own database
- Exposes an REST API to query reviews given a store

### Technologies
- `Kafka` was used to listen to events from other microservices
- `Asp.net Core` was used to expose a REST API
- `MongoDb` was used to store data
- `Docker` was used in development, continuous integration tests, and deployment

<img width="836" alt="diagram" src="https://user-images.githubusercontent.com/165290/130526478-94ef31ba-8ac6-4d33-a3db-c70984f06fc6.png">


## Contributing
If you want to contribute, start by reading [CONTRIBUTING.md](CONTRIBUTING.md).
