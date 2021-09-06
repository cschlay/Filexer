# Filexer

_Filexer_ is developer centric file system indexer which enables quick backups on important files.
It ensures that

- Git repositories are up-to-date and pushed to remote
- PDFs and Office documents are collected in one place

The backup layout is quite flat since it is developer-centric backup so that migration between operating systems are simple.

| Source | Target |
| ------ | ------ |
| `~/.ssh` | `dev/.ssh` |
| `~/.bashrc` | `dev/.bashrc` |
| `.pem`| `dev/secrets` |
| `.pdf` | `documents/*.pdf` |
| Git Repository | `projects/`
| Office 365 File | `office/` |
| Everything else | `misc/` |

Large files such as (images, videos, executables), installed software, and binaries are not indexed.

Each back up will output a `.zip` as `YYYY-MM-DD-Thhmm.filexer.zip`. The time stamp is a variant of ISO 8601 standard.
