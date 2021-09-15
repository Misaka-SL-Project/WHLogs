[![Github All Releases](https://img.shields.io/github/downloads/xRoier/WHLogs/total?color=blueviolet&style=for-the-badge)]()
# WebhookLogs
A "simple" plugin that logs game events (and commands) through discord webhooks.

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

# Installation

**[EXILED 2.14.0](https://github.com/Exiled-Team/EXILED/releases/tag/2.14.0) must be installed for this to work.**

Place the "WHLogs.dll" file in your Plugins folder.
Windows: ``%appdata%/EXILED/Plugins``.
Linux: ``.config/EXILED/Plugins``.
