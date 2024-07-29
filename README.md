![DevAccelerate Logo](https://github.com/ejyle/devaccelerate-dotnet/blob/dev/assets/da_logo_sm.png?raw=true)
# DevAccelerate for .NET

**DevAccelerate for .NET** is a comprehensive and well-designed development accelerator for Microsoft .NET Framework. It is free and open source. It contains common components and functionalities that allow developers to build real-world .NET apps rapidly. It is broken up into individual modules and therefore allows developers to use only what is needed. The usage of the framework is governed by the terms and conditions of its [License](https://github.com/devaccelerate/DevAccelerateNet/blob/master/LICENSE).

## 8.0.0 Preview Changes
DevAccelerate for .NET 8.0 adds support for .NET 8 and the primary objective of this release is to simplify the overall framework. The following are the highlights:
* Added support for .NET 8.
* Removed Facades, Social, and Tools from the solution to simplify the overall framework and remove the .
* Renamed MTP (Managed Tenant Provider) as MSP (Managed Service Provider) in the Multi-Tenancy libraries.
* Added DaMSPTenant and MSPTenantMember classes to manage MSP tenants and their members in a simplified entity model.

## 7.0.1 Preview Changes
DevAccelerate for .NET 7.0 is a major release and has new many new APIs and some of the older APIs have been removed. The following are the highlights:
* Added object types, object instances, object history and object dependencies to the core module.
* Removed caching, exception management, and logging APIs.
* Moved tenant, address profile and organization profile related types to a new module called multiTenancy.
* Moved subscription and subscription related types to a new module called subscriptions.
* Moved apps and features related types to a new module called platform.
* Moved user profile related types to identity module.
* Removed enterprise security and profiles modules.
  Migrated the new messages module back to notifications.
* General design and code improvements.

## 6.5.0 Changes
Some new modules have been added and few older modules have also been migrated to the latest code base. The following are the highlights:
* Added Comments module to create and manage comments.
* Added Files module to create and manage logical file objects.
* Migrated the older Notifications library and added it as Messages module.
* Added Tasks module to create and manage user-defined tasks.

## 6.1.0 Changes
The code base has been updated in a significant way to provide better and more optimised functionality. The following are the highlights:
* The current code base is now fully migrated to .NET 6 (latest .NET version) and it includes the support for .NET 5 and .NET Core 3.1. The support for .NET Framework has been discontinued.
* Support for Microsoft Enterprise Library (MSEL) has been removed. The development of MSEL was discontinued by Microsoft long time ago and therefore it was reasonable to discontinue its inclusion in DevAccelerate.
* The use of application configurations (app.config / web.config) in DevAccelerate libraries has been discontinued. This has been replaced with the IOptions pattern.
* The DevAccelerateNetTools console app has been migrated as a .NET tool.

## Modules

DevAccelerate for .NET is broken down into modules and each module has one or more NuGet package. A DevAccelerate NuGet package automatically installs required dependencies.

The following is the list of DevAccelerate modules with corresponding NuGet packages:
#### Core
* [DevAccelerateCore](https://www.nuget.org/packages/DevAccelerateCore)
* [DevAccelerateCoreEF](https://www.nuget.org/packages/DevAccelerateCoreEF)
#### Comments
* [DevAccelerateComments](https://www.nuget.org/packages/DevAccelerateComments)
* [DevAccelerateCommentsEF](https://www.nuget.org/packages/DevAccelerateCommentsEF)
#### Files
* [DevAccelerateFiles](https://www.nuget.org/packages/DevAccelerateFiles)
* [DevAccelerateFilesEF](https://www.nuget.org/packages/DevAccelerateFilesEF)
#### Lists
* [DevAccelerateLists](https://www.nuget.org/packages/DevAccelerateLists/)
* [DevAccelerateListsEF](https://www.nuget.org/packages/DevAccelerateListsEF/)
#### Identity
* [DevAccelerateIdentity](https://www.nuget.org/packages/DevAccelerateIdentity/)
* [DevAccelerateIdentityEF](https://www.nuget.org/packages/DevAccelerateIdentityEF/)
#### Platform
* [DevAcceleratePlatform](https://www.nuget.org/packages/DevAcceleratePlatform)
* [DevAcceleratePlatformEF](https://www.nuget.org/packages/DevAcceleratePlatformEF)
#### MultiTenancy
* [DevAccelerateMultiTenancy](https://www.nuget.org/packages/DevAccelerateMultiTenancy/)
* [DevAccelerateMultiTenancyEF](https://www.nuget.org/packages/DevAccelerateMultiTenancyEF/)
#### Subscriptions
* [DevAccelerateSubscriptions](https://www.nuget.org/packages/DevAccelerateSubscriptions/)
* [DevAccelerateSubscriptionsEF](https://www.nuget.org/packages/DevAccelerateSubscriptionsEF/)
#### Notifications
* [DevAccelerateNotifications](https://www.nuget.org/packages/DevAccelerateNotifications)
* [DevAccelerateNotificationsEF](https://www.nuget.org/packages/DevAccelerateNotificationsEF)
#### Mail and SMS
* [DevAccelerateMail](https://www.nuget.org/packages/DevAccelerateMail/)
* [DevAccelerateMailSendGrid](https://www.nuget.org/packages/DevAccelerateMailSendGrid/)
* [DevAccelerateSms](https://www.nuget.org/packages/DevAccelerateSms/)
* [DevAccelerateSmsTextlocal](https://www.nuget.org/packages/DevAccelerateSmsTextlocal/)
* [DevAccelerateSmsTwilio](https://www.nuget.org/packages/DevAccelerateSmsTwilio/)
#### System Tasks
* [DevAccelerateSystemTasks](https://www.nuget.org/packages/DevAccelerateSystemTasks/)
* [DevAccelerateSystemTasksEF](https://www.nuget.org/packages/DevAccelerateSystemTasksEF/)
#### Tasks
* [DevAccelerateTasks](https://www.nuget.org/packages/DevAccelerateTasks)
* [DevAccelerateTasksEF](https://www.nuget.org/packages/DevAccelerateTasksEF)

## Getting Started
### Installation
DevAccelerate modules are available on NuGet. Each module has its own package(s). Use ```dotnet add package``` command to install DevAccelerate modules. Here are some of the examples:
```
dotnet add package DevAccelerateCoreEF
dotnet add package DevAccelerateCommentsEF
dotnet add package DevAccelerateFilesEF
dotnet add package DevAccelerateListsEF
dotnet add package DevAccelerateIdentityEF
dotnet add package DevAcceleratePlatformEF
dotnet add package DevAccelerateMultiTenancyEF
dotnet add package DevAccelerateSubscriptionsEF
dotnet add package DevAccelerateTasksEF
dotnet add package DevAccelerateNotificationsEF
dotnet add package DevAccelerateMailSendGrid
dotnet add package DevAccelerateSmsTextlocal
dotnet add package DevAccelerateSmsTwilio
```
To install all the DevAccelerate modules (libraries) in one go, the DevAccelerateAll metapackage can be quite handy:
```
dotnet add package DevAccelerateAll
```
## Issues

If you find a bug in the library or you have an idea about a new feature, please try to search in the existing list of [issues](https://github.com/devaccelerate/DevAccelerateNet/issues). If the bug or idea is not listed and addressed there, please [open a new issue](https://github.com/devaccelerate/DevAccelerateNet/issues/new).
