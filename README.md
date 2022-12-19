# Multiplatform Github Action

[![Multiplatform Test](https://github.com/Tilation/multiplatform-commit-tagger/actions/workflows/multiplatform-test.yml/badge.svg)](https://github.com/Tilation/multiplatform-commit-tagger/actions/workflows/multiplatform-test.yml)

Compatible with: `macos`, `ubuntu` and `windows` runners.

Parameter | Required | Description
---|---|---
owner | true | The repository owner's username
repo | true | The repository name
token | true | A github PAT that has write access to the repository, depending on the type of token, this action CAN TRIGGER `CREATE` WORKFLOWS
tag | true | The name of the tag
refsha | true | The commit SHA to tag
tag_author | true | The username of the tag author
tag_author_email | true | The email of the tag author
tag_message | false | The tag message

## Example
```yaml
name: Tagger

on:
  pull_request:
    branches:
      - 'main'
    types:
      - closed

jobs:
  tag:
    if: github.event.pull_request.merged == true
    runs-on: ubuntu-latest
   
    steps:
    - uses: tilation/multiplatform-commit-tagger@v1
      with:
        owner: tilation
        repo: multiplatform-commit-tagger
        token: ${{ secrets.GITHUB_TOKEN }}
        tag: 'v6.7.9'
        refsha: ${{ github.sha }}
        tag_author: ${{ github.actor }}
        tag_author_email: ${{ github.actor }}
```
