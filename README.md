# Deep Dive Emailing Microservice

## Overview

The Deep Dive Emailing microservice is designed to handle the sending of emails using Azure Communication Email services. It provides functionality to send plain text and HTML emails with or without attachments. Recipients can be specified in "to", "cc", or "bcc" fields.

## Features

- Send emails using Azure Communication Email.
- Support for plain text and HTML content.
- Attachments support with workaround to handle binary data serialization.
- Logging of email operations and errors for troubleshooting.
