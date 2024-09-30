# PDSBuddy
A companion container for self-hosted ATProto PDS instances

This first version of PDSBuddy does one thing: PDS backup. It generates a .car file from your PDS and stores it in a GitHub repo. That's it.

Currently backups only contain posts, likes, and follows. They DO NOT include pictures/video or any other blob data due to the limitations of the dotnet GitHub SDK.

# docker compose
Notes:
* [Look up](https://internect.info/) your DID
* [Create](https://github.com/settings/tokens) a Github access token
* Set NOTIFICATIONS_LEVEL to `Off` to disable all emails
* Notifications settings for Gmail are included below but you can use any SMTP server
* [Create](https://myaccount.google.com/apppasswords) a Gmail app-specific password 
```
pdsbuddy: 
  container_name: pdsbuddy 
  image: ghcr.io/timburga/pdsbuddy:main 
  restart: unless-stopped 
  environment: 
    - PDS_URL=https://your.pds.address 
    - DID=your-full-did (eg did:plc:c54hcflqn6rv53qumfnxc5mj) 
    - GITHUB_REPO=repo-name 
    - GITHUB_TOKEN=github-access-token
    - NOTIFICATIONS_LEVEL=[Off|Errors|All]
    - NOTIFICATIONS_FROM_ADDRESS=user@gmail.com 
    - NOTIFICATIONS_TO_ADDRESS=user@gmail.com 
    - NOTIFICATIONS_SUBJECT=Message from PDSBuddy 
    - NOTIFICATIONS_SERVER=smtp.gmail.com 
    - NOTIFICATIONS_SERVER_PORT=587 
    - NOTIFICATIONS_SERVER_USER=user@gmail.com 
    - NOTIFICATIONS_SERVER_PASSWORD=gmail-app-specific-password
```
