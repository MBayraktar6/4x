# Wolf Game 4X - Geliştirme Rehberi

## Proje Yapısı

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs      # Ana oyun yöneticisi
│   │   ├── GameConfig.cs       # Oyun ayarları
│   │   └── PlayerData.cs       # Oyuncu verileri
│   ├── Village/
│   │   ├── VillageManager.cs   # Köy yönetimi
│   │   ├── VillageData.cs      # Köy verileri
│   │   └── BuildingDatabase.cs # Bina konfigürasyonları
│   ├── Clan/
│   │   ├── ClanManager.cs      # Klan yönetimi
│   │   └── ClanData.cs         # Klan verileri
│   ├── Map/
│   │   └── MapManager.cs       # Harita yönetimi
│   ├── Ads/
│   │   └── AdManager.cs        # AdMob entegrasyonu
│   ├── Managers/
│   │   ├── UIManager.cs        # UI yönetimi
│   │   └── SaveManager.cs      # Veri kaydetme
│   └── UI/
│       └── ResourceDisplay.cs  # Kaynak göstergeleri
├── Resources/
├── Scenes/
├── Prefabs/
├── Sprites/
└── Audio/
```

## Başlangıç

### 1. Temel Kurulum
- Unity 2022 LTS+ yükleyin
- Google Mobile Ads SDK'sını Unity'ye kurun
- Repository'yi klonlayın

### 2. GameConfig Ayarları
1. Assets/Resources klasöründe "GameConfig" ScriptableObject oluşturun
2. Oyun ayarlarını yapılandırın
3. GameManager'da referansını ayarlayın

### 3. Harita Oluşturma
- MapManager otomatik olarak harita oluşturur
- Harita boyutunu GameConfig'te ayarlayın
- Bölgeler otomatik olarak 10x10 tile olarak oluşturulur

## Başlıca Sistemler

### Kaynak Sistemi
- **Altın (Gold)**: Temel para birimi
- **Kereste (Wood)**: İnşaat için
- **Yemek (Food)**: Birim eğitimi için
- **Mücevher (Gems)**: Premium para birimi

### Bina Sistemi
- Her köyde farklı bina türleri
- Her bina seviyeye göre daha güçlü
- İnşaa süresi ayarlanabilir
- Üretim hızları bina türüne göre değişir

### Klan Sistemi
- Oyuncular klanlar kurabilir
- Klan üyeleri bölgeleri birlikte kontrol ederler
- Klan hazinesine kaynak katkısı
- Klan teknolojileri genel bonuslar sağlar

### Reklam Sistemi
- AdMob entegrasyonu
- 15 dakika cooldown
- Günlük 48 reklam limiti
- Her reklam: 100 altın + 50 kereste + 75 yemek

## Genişletme Noktaları

### 1. Savaş Sistemi
```csharp
public class BattleManager : MonoBehaviour
{
    // Birim üretimi
    // Savaş hesaplaması
    // Zarar sistemi
}
```

### 2. Ticaret Sistemi
```csharp
public class MarketManager : MonoBehaviour
{
    // Oyuncu ticaret
    // NPC ticaret
    // Fiyat dinamikleri
}
```

### 3. Araştırma Sistemi
```csharp
public class ResearchManager : MonoBehaviour
{
    // Teknoloji ağacı
    // Araştırma süresi
    // Bonusları
}
```

### 4. Etkinlikler/Quests
```csharp
public class QuestManager : MonoBehaviour
{
    // Günlük görevler
    // Kampanya
    // Ödüller
}
```

## Veri Kaydetme

Veriler `Application.persistentDataPath` konumunda `gamedata.json` olarak kaydedilir.

```csharp
// Kaydetme
GameManager.Instance.SaveGame();

// Yükleme
PlayerData data = SaveManager.LoadPlayerData();
```

## AdMob Kurulumu

1. [Google AdMob](https://admob.google.com) açılan bir hesap oluşturun
2. App ID ve Ad Unit ID'lerinizi alın
3. `AdManager.cs` dosyasında `rewardedAdUnitId` güncelleyin
4. Test modu için AdMob test ID'lerini kullanın

## Derleme & Dağıtım

### Android
1. File > Build Settings
2. Platform: Android seçin
3. Package Name: com.yourcompany.wolfgame4x
4. Build

### iOS
1. File > Build Settings
2. Platform: iOS seçin
3. Build
4. Xcode'da açın ve derleme yapın

## Performans Optimizasyonu

- Object Pooling kullanın
- Sprite atlasları kullanın
- Parçacık efektleri sınırlayın
- UI Canvas optimizasyonu

## Debug İşaretleri

Tüm sistemler debug loglarına sahiptir:
```
[GameManager] - Ana oyun logları
[VillageManager] - Köy işlemleri
[ClanManager] - Klan işlemleri
[MapManager] - Harita işlemleri
[AdManager] - Reklam işlemleri
[SaveManager] - Kaydetme işlemleri
```

## Sık Sorulan Sorular

**S: Harita büyüklüğünü nasıl değiştirim?**
C: GameConfig.cs'te `mapWidth` ve `mapHeight` değerlerini değiştirin.

**S: Yeni bir bina türü nasıl eklerim?**
C: BuildingType enum'una ekleyin, BuildingConfig oluşturun, BuildingDatabase'ye ekleyin.

**S: Reklam cooldown süresini nasıl değiştirim?**
C: GameConfig.cs'te `videoAdCooldownMinutes` değerini değiştirin.

**S: Veriler nerede saklanır?**
C: `Application.persistentDataPath` klasöründe `gamedata.json` olarak.

## İletişim

Sorular veya öneriler için issue açın!
