
# Checkout Tech Test
## Overview
My aim was to create a PaymentGateway API with minimal dependencies and the structure for a basic but comprehensive testing environment.

The API runs on `ASP.NET Core 3.x` and depends on `Amazon DynamoDB` for storage in its mock acquiring bank and the payment submission endpoint has a small unit test suite.

A very basic end-to-end test project is aimed at testing the API at large and ensuring components and dependecies are integrated.

The API and end-to-end tests are brought together by `docker-compose`. This enables a minimal but reproducible development and testing environment that will work on both development machines, CI/CD tooling and also result in a production image for the API.

## Requirements
The project should run with docker and docker-compose alone however development will probably be a bit quicker with the .NET Core SDK (3.0+). 

## Design
The Models project, which exposes common types, includes POCOs with interfaces. The interfaces do not include setters for the properties enabling consuming code to treat them as immutable whilst retaining the ability to create them with easily (i.e. with object initializer syntax).

## Areas for improvement
There are some very basic retry policies included to ensure services don't start until their dependencies are ready, notably; the API waiting for DynamoDB to successfully respond to CreateTable and end-to-end tests waiting for the API health check to respond successfully before beginning tests. Although in principle I would likely retain those waits they could be better implemented and integrated. For example the end-to-end tests would probably have some kind of pre-suite wait rather than waiting within a test.
With the API project you'll find a ConfigureDynamoService which runs at startup to create the DynamoDB table for Payments. Whilst there is some value to this bootstrapping method it would ideally go in to a resource template i.e. Terraform, CloudFormation or ARM.

## Usage
`docker-compose up`
