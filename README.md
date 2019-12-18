
# Checkout Tech Test
## Overview
My aim was to create a PaymentGateway API with minimal dependencies and the structure for a basic but comprehensive testing environment.

The API runs on `ASP.NET Core 3.x` and depends on `Amazon DynamoDB` for storage in its mock acquiring bank and the payment submission endpoint has a small unit test suite.

A very basic end-to-end test project is aimed at testing the API at large and ensuring components and dependecies are integrated.

The API and end-to-end tests are brought together by `docker-compose`. This enables a minimal but reproducible development and testing environment that will work on both development machines, CI/CD tooling and also result in a production image for the API.

## Design
The Models project, which exposes common types, includes POCOs with interfaces. The interfaces do not include setters for the properties enabling consuming code to treat them as immutable whilst retaining the ability to create them with easily (i.e. with object initializer syntax).

## Areas for improvement

## Usage
`docker-compose up`