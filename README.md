# Daenet.Common.Logging

This is a wrapper for the .net core ILogger. The `LogManager` class wraps the ILogger interface and has all the functions `ILogger` provides and more. See **LogManager**.

## Installation

Install the Daenet.Common.Logging [Nuget package](https://www.nuget.org/packages/Daenet.Common.Logging/).

## LogManager Features

LogManager wraps the `ILogger` interface and provides additional features.

### Additional Parameters

LogManager has the ability to add *Additional Parameters* to the every LogMessage.
This can be done by using the `void AddAdditionalParams(string paramName, string paramValue);` method.

LogManager has an internal dictionary, which handles those parameters.
Before every trace the LogManager adds the *Additional Parameters* dictionary to the parameters list.
A custom implementation of *ILogger* can then handle the dictionary.
For example: An EventHubLogger could write the dictionary to a own JSON property.
The *Additional Parameters* dictionary is stored under the key `scope_d7aeb2f369664dfeac94ff5af0207efb`.

Remarks: Additional Parameters can be added, removed and cleared.