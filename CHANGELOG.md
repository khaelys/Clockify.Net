# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



## [1.3.0]
### Added
- Support for query parameter in `FindAllWorkspacesAsync()` (PR #17)

## [1.2.0]
### Added
 - Support for deleting and updating tasks. (PR #16)
 - `Note` field to ProjectRequest and ProjectDtoImpl (PR #15)
 - `FindAllHydratedTimeEntriesForUserAsync` method to client (PR #12)
### Changed
- JsonSerializer to Json.Net
### Fixed
- `EstimateRequest` should now work properly. 

## [1.1.4]
### Fixed
- `Estimate` deserialization (PR #10)

## [1.1.3]
### Changed
- `SetActiveWorkspaceFor` is now obsolete due to removal from experimental 
- `DeleteWorkspaceAsync` is now obsolete because of removal SetActiveWorkspaceFor endpoint from API.
### Fixed
- `LongRunning` is `UserSettingsDto` is now a bool as it should be (Issue #8)


## [1.1.2]
### Fixed
- `projectRequired` and `considerDurationFormat` were mixed
- Clockify api requires ISO-8601 format for dates otherwise PM part 12-24 will be lost


## [1.1.1]
### Changed
- Change paramater name in `DeleteTimeEntryAsync` to timeEntryId
### Fixed
- DataOffset formating in `FindAllTimeEntriesForUserAsync()`

## [1.1.0]
### Added
- Support for POST /users/{userId}/activeWorkspace/{workspaceId}
### Changed
- Updated RestSharp
- Code to stopped using obselete methods.
### Removed
- Removed Q&A dependencies

## [1.0.0]
### Added
- Support for all endpoints form https://clockify.me/developers-api
	- Workspace support
	- User support
	- Client support
	- Projects support
	- Tag support
	- Task support
	- Time Entry support
	- Template support

<!-- 
===== Template =====
## [0.0.0]
### Added
### Changed
### Removed 
### Fixed
-->
