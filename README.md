# FoxDen

FoxDen is an application suite designed to centralize convention management into a convenient, easy to use, and extensible platform.

## Features

### Authentication

Authentication is provided by a free and open source Zitadel integration. This integration can either point to a hosted on-prem instance or a free or paid Cloud instance.
Zitadel allows for integration with existing [IDP providers](https://zitadel.com/docs/guides/integrate/identity-providers/introduction) such as Google Workspaces,
Microsoft Entra ID (formerly Azure Active Directory), OpenLDAP, and more to provide a seamless Single Sign-On experience for your staff powered by OAuth2. Zitadel also
allows for customizing the login portal so you can brand the portal in any way you like.

For attendees and guests, they are able to create accounts which are stored within Zitadel. This allows them to manage the information stored about them, streamline the
registration process, and sign documents such as when agreeing to convention rules or signing contracts.

Review the [Zitadel Documentation](https://zitadel.com/docs/guides/start/quickstart) for more information about its features.

### Staff Applications

FoxDen integrates with [JotForm](https://www.jotform.com/) to provide advanced form features and workflows. These workflows include:

- Modifying collected form data [^1].
- Flagged Applicants. [^2]
  - Some applicants may be prohibited from staffing for specific teams or for the convention as a whole. If a flagged applicant submits an application, HR will receive
	a notification and any relevant workflows will be fired.
- Send review emails, add additional checks, require approvals, and more.

### JSON REST API

Further automations can be handled by integrating with the JSON REST API, which is OpenAPI compliant.

### 

[^1] https://github.com/LuzFaltex/FoxDen/issues/2
[^2] https://github.com/LuzFaltex/FoxDen/issues/3