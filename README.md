# Filexer

_Filexer_ is developer centric file system indexer which enables quick backups on important files.
It ensures that

- Git repositories are up-to-date and pushed to remote
- PDFs and Office documents are collected in one place
- Warns about unsafe credential storage

The backup layout is as follows

| Source | Target |
| ------ | ------ |
| `~/.ssh` | `User/.ssh` |
| `~/.bashrc` | `User/.bashrc` |

Large files and binaries (images, videos, executables) are not indexed.

Each back up will output a `.zip` as `YYYY-MM-DD-Thhmm.filexer.zip`. The time stamp is a variant of ISO 8601 standard.
