# Development Guide

## Project Setup

### Prerequisites
- Unity 2022.3 LTS or higher
- Android SDK (for Android builds)
- Xcode (for iOS builds)
- Google Mobile Ads SDK for Unity

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/MBayraktar6/4x.git
   cd 4x
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Add" and select the project folder
   - Open the project with Unity 2022.3+

3. **Install Dependencies**
   - Window > TextMesh Pro > Import TMP Essentials
   - Window > Google Mobile Ads > Settings (configure AdMob IDs)

### Project Structure

```
Assets/
├── Scripts/
│   ├── Core/              # Core game systems
│   │   ├── GameManager.cs
│   │   └── PlayerData.cs
│   ├── Buildings/         # Building system
│   │   ├── Building.cs
│   │   └── BuildingManager.cs
│   ├── Units/             # Unit system
│   │   ├── Unit.cs
│   │   └── UnitManager.cs
│   ├── Map/               # Map system
│   │   └── MapManager.cs
│   ├── Clan/              # Clan system
│   │   └── ClanManager.cs
│   ├── Economy/           # Economy system
│   │   └── EconomyManager.cs
│   ├── Combat/            # Combat system
│   │   └── CombatSystem.cs
│   ├── Ads/               # Advertisement system
│   │   └── AdsManager.cs
│   ├── UI/                # User interface
│   │   └── UIManager.cs
│   └── Helpers/           # Utility functions
│       ├── SaveLoadHelper.cs
│       └── DebugHelper.cs
├── Prefabs/               # Game prefabs
├── Scenes/
│   └── MainScene.unity
├── Resources/
│   ├── BuildingConfigs.json
│   └── UnitConfigs.json
├── Sprites/               # 2D graphics
├── Animations/            # Character animations
└── Audio/                 # Sound effects & music
```

## Building Systems

### Game Manager
- Central game state management
- Initializes all managers
- Handles pause/resume
- Save/load game data

### Player Data
- Player profile information
- Resource management
- Level and experience system
- Persistent data storage

### Building System
- Building placement and construction
- Resource production
- Building upgrades
- Building queue

### Unit System
- Unit creation and management
- Movement system
- Combat stats
- Unit types (Warrior, Archer, etc.)

### Map System
- Tile-based map generation
- Territory control
- Clan territory management
- Building placement validation

### Clan System
- Clan creation and management
- Member management
- Clan experience and leveling
- Territory ownership

### Economy System
- Resource trading
- Market prices with fluctuation
- Trade routes
- Resource production balancing

### Combat System
- Battle initialization
- Combat rounds
- Unit health/damage
- Battle rewards

### Ads System
- AdMob integration
- Rewarded ads
- Ad cooldown system
- Reward distribution

## AdMob Setup

1. **Create AdMob Account**
   - Go to [Google AdMob](https://admob.google.com)
   - Sign in with Google account
   - Create new app

2. **Get Ad Unit IDs**
   - Rewarded ad unit ID
   - Banner ad unit ID
   - App ID

3. **Configure in AdsManager.cs**
   ```csharp
   public string appId = "ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy";
   public string rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917";
   public string bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
   ```

## Building for Android

1. **Configure Player Settings**
   - File > Build Settings
   - Select Android platform
   - Player Settings:
     - Package name: com.company.gamename
     - Minimum API level: 24
     - Target API level: 33+

2. **Build APK**
   - File > Build Settings > Build
   - Select output folder

3. **Build AAB (for Play Store)**
   - Edit > Project Settings > Player
   - Enable "Split Application Binary"
   - Build as AAB

## Building for iOS

1. **Configure Player Settings**
   - File > Build Settings
   - Select iOS platform
   - Player Settings:
     - Bundle Identifier: com.company.gamename
     - Minimum OS Version: 14.0

2. **Build Xcode Project**
   - File > Build Settings > Build
   - Open generated Xcode project
   - Configure signing
   - Build and archive

## Debugging

### Enable Debug Logs
```csharp
DebugHelper.LogGameState();      // Print current game state
DebugHelper.AddResourcesToPlayer(10000); // Add test resources
```

### Test Ads
Use test ad unit IDs provided in code (already configured)

## Common Issues

### Ads not showing
- Check AdMob IDs are correct
- Ensure app is properly configured in AdMob console
- Check network connectivity
- Use test IDs during development

### Game not saving
- Check persistent data path
- Ensure write permissions for Android
- Check for JSON serialization issues

### Performance issues
- Reduce map size
- Optimize sprite usage
- Use object pooling for units
- Profile with Unity Profiler

## Version History

### v0.1.0 - Initial Release
- Core game systems implemented
- Basic building and unit systems
- Clan system
- AdMob integration
- Cross-platform support

## Contributing

1. Create feature branch: `git checkout -b feature/new-feature`
2. Commit changes: `git commit -am 'Add new feature'`
3. Push to branch: `git push origin feature/new-feature`
4. Create Pull Request

## License

MIT License - Feel free to use this for personal or commercial projects.
