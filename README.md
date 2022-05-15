[![Github All Releases](https://img.shields.io/github/downloads/xRoier/WHLogs/total?color=blueviolet&style=for-the-badge)]()
# WebhookLogs
A dirty and simple plugin that logs game events (and commands) through discord webhooks.

# Config
```
webhooklogs:
# Is the plugin enabled?
  is_enabled: true
  # Set the delay between log messages [This is the minimum, if this number is lower the plugin will not load to avoid discord ratelimit]
  log_queue_delay: 1.2
  # Set the webhook username
  username: Logs
  # Set the webhook avatar url
  avatar_url: https://i.imgur.com/SaqRzfU.png
  # Set the webhook url for game events logs
  game_events_logs_webhook_url: fill me
  # Set the webhook url for command logs
  command_logs_webhook_url: fill me
  # Set the webhook url for pvp events logs
  pvp_events_logs_webhook_url: fill me
```

**[EXILED 5.2.1](https://github.com/Exiled-Team/EXILED/releases/tag/5.2.1) must be installed for this to work.**

# Installation - Linux

Place the "WHLogs.dll" file in your plugins folder (``.config/EXILED/Plugins``)

# Installation - Windows

Place the "WHLogs.dll" file in your plugins folder (``%appdata%/EXILED/Plugins``)
