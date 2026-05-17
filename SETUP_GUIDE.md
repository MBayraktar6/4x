# 4X Strategy Game - Kurulum ve Konfigürasyon Rehberi

## 📋 İçindekiler

1. [Ön Gereksinimler](#ön-gereksinimler)
2. [Proje Kurulumu](#proje-kurulumu)
3. [Unity Ayarları](#unity-ayarları)
4. [AdMob Konfigürasyonu](#admob-konfigürasyonu)
5. [Grafikleri Eklemek](#grafikleri-eklemek)
6. [Build İşlemi](#build-işlemi)
7. [Sorun Giderme](#sorun-giderme)

---

## 🔧 Ön Gereksinimler

### Yazılım Gereksinimleri

```
✅ Unity 2022.3 LTS veya üzeri
✅ Android SDK (API Level 24+)
✅ Xcode 14+ (iOS için)
✅ Git
✅ Visual Studio Code veya Rider
```

### Hardware Gereksinimleri

```
✅ Minimum 8GB RAM
✅ 50GB boş disk alanı
✅ Intel i5 / M1 veya üzeri işlemci
```

---

## 🚀 Proje Kurulumu

### 1. Repository'yi Clone Edin

```bash
git clone https://github.com/MBayraktar6/4x.git
cd 4x
```

### 2. Unity'de Açın

```
1. Unity Hub açın
2. "Add" butonuna tıklayın
3. Proje klasörünü seçin
4. Unity 2022.3 LTS seçin
5. "Open" tıklayın
```

### 3. Gerekli Paketleri Yükleyin

```
Window > TextMesh Pro > Import TMP Essentials
Window > TextMesh Pro > Import Examples and Extras
```

### 4. Google Mobile Ads SDK Yükleyin

```
1. Window > Google Mobile Ads > Settings
2. "Get Started" tıklayın
3. AdMob hesapları bağlayın
```

---

## ⚙️ Unity Ayarları

### Player Settings (Android & iOS)

#### Android Ayarları

```
File > Build Settings > Select Android

Player Settings:
├── Company Name: "YourCompanyName"
├── Product Name: "4X Strategy Game"
├── Package Name: "com.company.strategygame"
├── Minimum API Level: 24
├── Target API Level: 33+
├── Version: 1.0
└── Bundle Version Code: 1
```

#### iOS Ayarları

```
File > Build Settings > Select iOS

Player Settings:
├── Company Name: "YourCompanyName"
├── Product Name: "4X Strategy Game"
├── Bundle Identifier: "com.company.strategygame"
├── Minimum OS Version: 14.0
├── Supported Device Orientations: Portrait
└── App Icons and Launch Images: Yapılandırın
```

### Graphics Settings

```
Edit > Project Settings > Graphics

├── Color Space: Linear (daha iyi görsel kalite)
├── Rendering Path: 2D (Forward Rendering)
└── Instancing: Enable
```

### Audio Settings

```
Edit > Project Settings > Audio

├── Default Speaker Mode: Stereo
├── DSP Buffer Size: Best Latency
└── Volume: 1.0
```

---

## 📱 AdMob Konfigürasyonu

### 1. AdMob Hesabı Oluşturun

```
1. https://admob.google.com adresine gidin
2. Google hesabınızla giriş yapın
3. "Create App" tıklayın
4. Platform olarak "Android" veya "iOS" seçin
```

### 2. Ad Unit ID'lerini Alın

```
Android:
├── App ID: ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy
├── Rewarded Ad Unit: ca-app-pub-3940256099942544/5224354917
└── Banner Ad Unit: ca-app-pub-3940256099942544/6300978111

iOS:
├── App ID: ca-app-pub-xxxxxxxxxxxxxxxx~zzzzzzzzzz
├── Rewarded Ad Unit: ca-app-pub-3940256099942544/1712485313
└── Banner Ad Unit: ca-app-pub-3940256099942544/2934735716
```

### 3. AdsManager.cs'i Yapılandırın

```csharp
// Assets/Scripts/Ads/AdsManager.cs

public class AdsManager : MonoBehaviour
{
    // Android ID'lerini yapıştırın
    public string appId = "ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy";
    public string rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917";
    public string bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
    
    // iOS ID'lerini yapıştırın (isteğe bağlı)
    #if UNITY_IOS
    public string appId = "ca-app-pub-xxxxxxxxxxxxxxxx~zzzzzzzzzz";
    #endif
}
```

### 4. Test Modunda İlk Test

```csharp
// Development sırasında test ID'lerini kullanın
// (yukarıda zaten yapılandırıldı)

// Test cihazı olarak kendi cihazınızı kaydedin:
// AdMob > Settings > Test devices > Add test device
```

---

## 🎨 Grafikleri Eklemek

### Ücretsiz Grafik Kaynakları

#### 1. Kenney.nl (En İyi)

```
Website: https://kenney.nl

Download:
├── 2D Game Assets - Fantasy
├── 2D Game Assets - Medieval
├── 2D Game Assets - UI
└── 2D Game Assets - Isometric

Kullanım:
1. İndirin ve zip'i açın
2. Assets/Sprites/ klasörüne yapıştırın
3. Sprites'ı import ayarlarıyla yapılandırın
```

#### 2. Itch.io

```
Website: https://itch.io

Aramalar:
├── "2D Game Assets Free"
├── "Fantasy Tileset"
├── "Character Sprites"
└── "UI Pack Free"
```

#### 3. OpenGameArt.org

```
Website: https://opengameart.org

Kategori:
├── Graphics > 2D
├── Fantasy
└── Medieval
```

### Sprite'ları Unity'ye Eklemek

```
1. PNG/JPG dosyasını Assets/Sprites/ klasörüne sürükleyin
2. Import Settings ayarlayın:
   ├── Texture Type: Sprite (2D and UI)
   ├── Sprite Mode: Single (tek sprite) / Multiple (sheet)
   ├── Pixels Per Unit: 100
   └── Filter Mode: Point (No Filter)
3. Apply tıklayın
```

### Prefab'lara Sprite'ları Atamak

```
1. Assets/Prefabs/ klasöründeki prefab'ı açın
2. SpriteRenderer bileşenini bulun
3. Sprite alanına sprite'ı sürükleyin
4. Save tıklayın
```

---

## 🔨 Build İşlemi

### Android APK Oluşturma

```
1. File > Build Settings
2. "Android" platform seçin
3. "Switch Platform" tıklayın
4. Scenes ekleyin:
   - Assets/Scenes/MainScene.unity
5. "Build" tıklayın
6. Dosya adını girin (örn: game.apk)
7. Build bekleyin (5-10 dakika)
```

### Android AAB Oluşturma (Play Store için)

```
1. File > Build Settings > Android seçin
2. Edit > Project Settings > Player
3. "Split Application Binary" etkinleştirin
4. File > Build Settings > "Build"
5. Çıkış klasörü seçin
6. .aab dosyası oluşturulacak
```

### iOS Build Oluşturma

```
1. File > Build Settings
2. "iOS" platform seçin
3. "Switch Platform" tıklayın
4. Scenes ekleyin
5. "Build" tıklayın
6. Xcode projesini seçin
7. Xcode'da açılacak
8. Signing sertifikasını yapılandırın
9. Build Archive
```

### APK Cihaza Yükleme

```bash
# Android SDK'nin platform-tools klasöründe
adb install game.apk

# veya çift tıkla (Windows)
# veya sürükle (Mac/Linux Android File Transfer)
```

---

## 🐛 Sorun Giderme

### Problem: Reklamlar Gösterilmiyor

```
✅ Çözümler:
1. AdMob ID'lerinin doğru olduğunu kontrol edin
2. Test cihazını AdMob'da kaydetmiş olun
3. Uygulamanın internete bağlı olduğundan emin olun
4. Google Mobile Ads SDK'nin en son sürümünü yükleyin
5. BuildSettings'deki "Internet Access" izni etkinleştirildi
```

### Problem: Oyun Kırmızı Hata Gösteriyor

```
✅ Çözümler:
1. Console penceresinde hatayı okuyun
2. Unity Package Manager'da bağımlılıkları kontrol edin
3. TextMesh Pro'yu import edin
4. Assets > Reimport All tıklayın
```

### Problem: Build Başarısız Oluyor

```
✅ Kontrol Listesi:
☐ Android SDK ve NDK yüklü mü? (Build Settings > Preferences)
☐ Java Development Kit (JDK) yüklü mü?
☐ API Level doğru mu? (Minimum 24)
☐ All scenes added to build?
☐ Yeterli disk alanı?
```

### Problem: Performans Düşük

```
✅ Optimizasyonlar:
1. Graphics Settings'de "High Performance" seçin
2. Sprite'ları atlastürüne dönüştürün
3. Gereksiz shadow'ları kapatın
4. Frame rate limitini 60'a ayarlayın
5. Profiler'ı açarak병목noktası bulun (Window > Analysis > Profiler)
```

### Problem: Oyuncu Verileri Kaydedilmiyor

```
✅ Çözümler:
1. Application.persistentDataPath denetleyin
2. PlayerPrefs.HasKey() ile kontrol edin
3. File IO izinlerini kontrol edin
4. SaveLoadHelper.cs'deki hataları kontrol edin
```

---

## 📊 Kontrol Listesi

### Geliştirme Öncesi

- [ ] Unity 2022.3+ yüklü
- [ ] Android SDK yüklü
- [ ] Git kurulu ve repository cloned
- [ ] Tüm paketler import edildi
- [ ] TextMesh Pro essentials yüklü

### Build Öncesi (Android)

- [ ] Package name yapılandırıldı
- [ ] AdMob ID'leri yapıştırıldı
- [ ] Tüm sahneler build settings'e eklendi
- [ ] Minimum API Level 24+
- [ ] Internet izni etkinleştirildi

### Release Öncesi

- [ ] Tüm sistemler test edildi
- [ ] Hata mesajları kaldırıldı
- [ ] Grafikleri optimize edildi
- [ ] AdMob üretim ID'leri kullanılıyor
- [ ] Sürüm numarası ayarlandı

---

## 📞 Destek

Sorularınız varsa:

```
1. GitHub Issues: https://github.com/MBayraktar6/4x/issues
2. Unity Forum: forum.unity.com
3. AdMob Support: support.google.com/admob
```

---

**Son Güncelleme:** 2026-05-17
