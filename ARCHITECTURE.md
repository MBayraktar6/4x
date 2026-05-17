# 4X Strategy Game - Sistem Mimarisi

## 📐 Genel Mimarim

```
┌─────────────────────────────────────────────────────────────────┐
│                       Game Manager (Singleton)                  │
│  Merkezi koordinator - Tüm sistemleri yönetir ve senkronize eder│
└─────────────────────────────────────────────────────────────────┘
                              │
        ┌─────────────────────┼─────────────────────┐
        │                     │                     │
        ▼                     ▼                     ▼
┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│  Player Data     │  │ Building Manager │  │  Unit Manager    │
│  (Kaynaklar)     │  │  (İnşaat Sistemi)│  │  (Asker Sistemi) │
└──────────────────┘  └──────────────────┘  └──────────────────┘
        │                     │                     │
        ▼                     ▼                     ▼
┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│ Clan Manager     │  │  Map Manager     │  │  Economy Manager │
│  (Klan Sistemi)  │  │  (Harita Sistemi)│  │  (Ticaret)       │
└──────────────────┘  └──────────────────┘  └──────────────────┘
        │
        ▼
┌──────────────────┐
│ Combat System    │
│  (Savaş Sistemi) │
└──────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                   Support Systems                               │
├──────────────┬──────────────┬──────────────┬───────────────────┤
│ Ads Manager  │ UI Manager   │ Notification │ Achievement       │
│  (Reklam)    │  (Arayüz)    │   System     │  System           │
└──────────────┴──────────────┴──────────────┴───────────────────┘
```

---

## 🔄 Sistem Açıklaması

### 1. **GameManager** (Merkezi Koordinatör)

```csharp
public class GameManager : MonoBehaviour
{
    // Singleton Pattern
    public static GameManager Instance { get; private set; }
    
    // Tüm Yöneticiler
    public PlayerData playerData;
    public BuildingManager buildingManager;
    public UnitManager unitManager;
    public MapManager mapManager;
    public ClanManager clanManager;
    public EconomyManager economyManager;
    public AdsManager adsManager;
    public UIManager uiManager;
}
```

**Sorumluluklar:**
- Oyun başlatma
- Tüm sistemleri başlatma
- Oyun döngüsü (Game Loop)
- Kaydet/Yükle işlemi
- Oyun durumu yönetimi

---

### 2. **PlayerData** (Oyuncu Profili)

```csharp
public class PlayerData : MonoBehaviour
{
    public PlayerInfo playerInfo;  // Ad, seviye, deneyim
    public Resources resources;     // 5 kaynak türü
    public long totalGamesPlayed;  // İstatistikler
}
```

**Özellikler:**
- Seviye ve deneyim sistemi
- Kaynak yönetimi (Al/Çıkar)
- Verilerini otomatik kaydet
- Offline veri senkronizasyonu

---

### 3. **BuildingManager** (Bina Sistemi)

```
Bina Türleri:
├── Üretim Binaları
│   ├── Farm (Food)
│   ├── Lumbermill (Wood)
│   ├── Stone Mine (Stone)
│   └── Iron Mine (Iron)
├── Savunma Binaları
│   ├── Wall
│   └── Tower
└── Özel Binalar
    ├── Town Hall (merkez)
    ├── Barracks (asker eğit)
    └── Laboratory (araştırma)
```

**Mekanikler:**
- Real-time inşaat süresi
- Otomatik kaynak üretimi
- Bina seviyesi ve yükseltme
- Bina kuyruğu sistemi

---

### 4. **UnitManager** (Asker Sistemi)

```
Birim Türleri (6 tip):
├── Warrior      → Dengeli (HP:100, ATK:15, DEF:5)
├── Archer       → Uzun Menzil (HP:60, ATK:20, DEF:2)
├── Knight       → Ağır Zırh (HP:150, ATK:20, DEF:12)
├── Mage         → Sihirli (HP:50, ATK:25, DEF:3)
├── Scout        → Hızlı (HP:30, ATK:8, DEF:1)
└── Healer       → Destek (HP:70, ATK:5, DEF:4)
```

**Sistemler:**
- Asker eğitimi (Food tüketimi)
- Hareket ve konumlandırma
- Savaş istatistikleri
- Grup yönetimi

---

### 5. **MapManager** (Harita Sistemi)

```
100x100 Tile Harita
├── Arazi Türleri:
│   ├── Grass (⊙) - Normal
│   ├── Forest (🌲) - Orman
│   ├── Mountain (⛏) - Dağ
│   ├── Water (🌊) - Su
│   └── Desert (🏜) - Çöl
├── Tile Özellikleri:
│   ├── Bina
│   ├── Askerler
│   └── Klan Sahipliği
└── Kontrol:
    ├── Yer placement doğrula
    ├── Bölge iddia et
    └── Territory göster
```

**Özellikler:**
- Rastgele harita üretimi
- Klan bölge kontrol sistemi
- Minimap sistemi
- Pathfinding

---

### 6. **ClanManager** (Klan Sistemi)

```csharp
public class Clan
{
    public int clanId;              // Klan ID
    public string clanName;         // Klan Adı
    public int leaderId;            // Lider
    public List<int> memberIds;     // Üyeler
    public long clanGold;           // Klan Altını
    public int level;               // Seviye (1-100)
    public long experience;         // Deneyim
    public Vector3 clanHallPosition; // Merkez
    public List<Vector2Int> ownedTerritories; // Kontrol edilen bölgeler
}
```

**İşlevler:**
- Klan oluştur/sil
- Üye yönet (ekle/çıkar)
- Klan leveling
- Bölge sahipliği

---

### 7. **EconomyManager** (Ekonomi Sistemi)

```
Kaynak Fiyatları (Dinamik):
├── Gold     = 1 (Temel)
├── Wood     = 30-100 (Dalgalanır)
├── Stone    = 30-100 (Dalgalanır)
├── Food     = 20-80 (Dalgalanır)
└── Iron     = 50-150 (Dalgalanır)

Ticaret Sistemi:
├── Kaynak Sat (Kahramanlar için)
├── Kaynak Al (Alışveriş)
├── Ticaret Rotaları
└── Kardan Marj Hesapla
```

**Mekanikler:**
- Market fiyat dalgalanması (her dakika)
- Al/Satış sistemi
- Ticaret analitiği
- Ekokont dengeleme

---

### 8. **CombatSystem** (Savaş Sistemi)

```
Savaş Akışı:
1. İçeri Başlat (2 Klan)
2. Unit Raundu (Hızlı birimler önce)
3. Hasar Hesapla (ATK - DEF)
4. Sağlık Güncelle
5. Ölü Birimler Kaldır
6. Savaş Bittiyse Sonlandır
7. Ödül Ver (Deneyim/Gold)

Savaş Ödülleri:
├── Kazanan: 1000 Deneyim
└── Kaybeden: 500 Deneyim
```

**Özellikler:**
- Real-time savaş
- Dinamik harita savaş yeri
- Savaş geçmişi
- Otomatik yönetim

---

### 9. **AdsManager** (Reklam Sistemi)

```
Reklam Türleri:
├── Banner Ads (Üst/Alt)
├── Interstitial Ads (Geçiş)
└── Rewarded Video Ads ⭐ (Ödüllü)

Ödüllü Video Sistemi:
├── Cooldown: 15-30 dakika
├── Limit: Oyuncu başına max 5/gün
├── Ödül: 100 Gold
└── Takip: AnalyticsSystem ile
```

**Yapılandırma:**
- Google AdMob entegrasyonu
- Test ID'leri (geliştirme)
- Üretim ID'leri (release)

---

### 10. **Destek Sistemleri**

#### UIManager
```csharp
- Kaynak gösterimi (HUD)
- Buton yönetimi
- Dinamik UI updates
- Screen adaptasyonu
```

#### NotificationSystem
```csharp
- Pop-up bildirimler
- Sıra yönetimi (max 3)
- Otomatik kaybolma
- Renk komutu (tip başına)
```

#### AchievementSystem
```csharp
Başarılar (5+ adet):
├── "Builder" - 1 bina inşa et
├── "Commander" - 100 asker eğit
├── "Veteran" - Level 10 ol
├── "Warrior" - 10 savaş kazan
└── "Ad Watcher" - 50 reklam izle
```

#### TutorialSystem
```csharp
- İnteraktif oyun rehberi
- 5 adım (kaynaklar, binalar, askerler, klanlar, savaşlar)
- Skip seçeneği
- Bir kez göster
```

#### OfflineProgressSystem
```csharp
- Offline kazanç (50% normal üretim)
- Max 12 saat offline ilerleme
- Otomatik uygulama
- Bildirim göster
```

#### AnalyticsSystem
```csharp
Takip Edilen Olaylar:
├── building_constructed
├── unit_trained
├── battle_completed
├── ad_watched
└── achievement_unlocked
```

---

## 🔐 Veri Yapısı

### PlayerData Serileştirme

```json
{
  "playerInfo": {
    "playerName": "Player_1234",
    "level": 5,
    "experience": 2500,
    "clanId": 1,
    "villagePosition": {"x": 50, "y": 50}
  },
  "resources": {
    "gold": 10000,
    "wood": 15000,
    "stone": 12000,
    "food": 20000,
    "iron": 5000
  },
  "lastSaveTime": 1684368240
}
```

### Building Serileştirme

```json
{
  "buildingId": "farm_001",
  "buildingName": "Farm",
  "level": 3,
  "buildTime": 60,
  "buildProgress": 0,
  "isBuilding": false,
  "position": {"x": 10, "y": 15},
  "buildingType": 2
}
```

---

## 🔄 Oyun Döngüsü (Update Cycle)

```
Frame'de Çalıştırılan Sıra:

1. InputSystem
   └─ Oyuncu girişini oku

2. GameManager.Update()
   ├─ EconomyManager.Update()      → Kaynak üret
   ├─ BuildingManager.Update()     → İnşaat ilerle
   ├─ UnitManager.Update()         → Asker hareket
   ├─ ClanManager.Update()         → Klan deneyimi
   └─ AdsManager.Update()          → Reklam cooldown

3. UIManager.Update()
   ├─ HUD güncellenme
   ├─ Kaynak gösterimi
   └─ Buton states

4. CombatSystem (aktif savaş varsa)
   ├─ Savaş turunun hesaplanması
   └─ UI güncelleme

5. Physics2D.Simulate() / Physics.Simulate()
   └─ Fizik simulasyonu

6. Render
   └─ Ekrana çiz
```

---

## 📊 Performans Hedefleri

```
Mobile Cihazlar:
├── FPS: 60 FPS (hedef)
├── RAM: <300MB oyun içi
├── Storage: <200MB (APK boyutu)
└── Battery: <5% saat başına

Network:
├── Ping: <100ms ideal
├── Bant genişliği: Minimal
└── Senkronizasyon: 30 saniye aralığında
```

---

## 🔐 Güvenlik Düşünceleri

```
Veri Koruması:
├── ✅ PlayerPrefs şifreleme
├── ✅ Offline veri senkronizasyonu
├── ✅ Hile önleme mekanizmaları
└── ✅ Server-side validasyon (gelecek)

Özel Tavsiyeler:
├── Büyük değişiklikleri sunucuda doğrula
├── Hız hile yapılabilir değerleri izle
├── Yapı maliyetlerini iki kez kontrol et
└── Transaction logging ekle
```

---

**Son Güncelleme:** 2026-05-17
