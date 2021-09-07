# Filexer

_Filexer_ is developer centric file system indexer which enables quick backups on important files.
It may be used in Servers or WSL to ease how files are taken out.

It ensures that

- Git repositories are up-to-date and pushed to remote
- PDFs and Office documents are collected in one place
- Important files are collected


The backup layout is quite flat since it is developer-centric backup so that migration between operating systems are simple.

Each back up will output a `.zip` as `YYYYMMDDThhmm.filexer.zip`. The time stamp is a variant of ISO 8601 standard.

Avoid indexing large files and images, this is supposed to be used as instant backup tool.
It supplements already existing full backup systems.

## Configuration

The default location for configuration file is at home directory `~/.filexer.json`.
It can be overriden with environment variable e.g. `FILEXER_CONFIG_FILE=~/custom-folder/config.json`.

The supported structure properties are defined below, you may copy it as starting point of configuration

```json5
{
  // You can use e.g. USB stick's mount path.
  "backupLocation": "",
  "cloneGitRepositories": true,
  // The roots locations that are no indexed by default such as "C:\Projects".
  // The default directory is user home i.e "/home/<username>
  "includedRootDirectories": [],
  // Exclude directories by pattern, by default everything is included
  "ignoreDirectories": [
    "downloads",
    "node_modules"
  ],
  // Recommended to use, e.g. Dockerfile doesn't have one
  "includeFilesWithoutExtension": true,
  // Explicitly state the extensions you want
  "includedExtensions": {
    "development": [
      ".md",
      ".json",
      ".sh",
      ".yaml",
    ],
    "documents": [
      ".pdf"
    ],
    "office": [
      ".docx",  // MS Word
      ".odt",   // OpenDocument Text
      ".ods",   // OpenDocument Spreadsheet
      ".xlsx"   // MS Excel
    ],
    "secrets": [
      ".pem",
      ".pub"
    ],
    "misc": [
      ".txt"
    ]
  }
}
```
