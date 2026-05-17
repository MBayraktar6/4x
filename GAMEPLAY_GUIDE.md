# 4X Strategy Game - Oyun Rehberi

## 🎮 Oyunun Amacı

Bu 4X (Keşfet, Genişlet, İşlet, Çıkar) stratejik oyunda:

```
🏆 Hedef: Klanınızı geliştirin ve haritada bölge kapın
```

---

## 📚 Oyun Sistemleri

### 1️⃣ **Kaynak Yönetimi**

#### Beş Kaynak Türü:

```
💰 Gold (Altın)        - Genel para, her şeyde kullanılır
🪵 Wood (Ahşap)       - Bina inşaatı
🪨 Stone (Taş)        - İleri bina inşaatı
🌾 Food (Yiyecek)     - Asker eğitimi
⚒️  Iron (Demir)       - Güçlü yapılar
```

#### Başlangıç Kaynakları:
```
💰 50,000 Gold
🪵 50,000 Wood
🪨 50,000 Stone
🌾 50,000 Food
⚒️  10,000 Iron
```

#### Kaynak Üretimi:
```
Bina          Saat Başına Üretim
────────────────────────────────
Farm          500 Food
Lumbermill    600 Wood
Stone Mine    550 Stone
Iron Mine     300 Iron
Town Hall     100 Gold
```

---

### 2️⃣ **Bina Sistemi**

#### Bina Türleri & Işlevi:

```
🏰 ÜRETIM BİNALARI
├── Town Hall (1.000 Gold)
│   └─ Saatlik 100 Gold üretir
├── Farm (300 Gold)
│   └─ Saatlik 500 Food üretir
├── Lumbermill (400 Gold)
│   └─ Saatlik 600 Wood üretir
├── Stone Mine (450 Gold)
│   └─ Saatlik 550 Stone üretir
└── Iron Mine (600 Gold)
    └─ Saatlik 300 Iron üretir

⚔️  SAVUNMA BİNALARI
├── Wall (500 Gold)
│   └─ Düşük hasar azaltma
└── Tower (700 Gold)
    └─ Yüksek hasar, otomatik ateş

🎖️  ÖZELLİK BİNALARI
├── Barracks (800 Gold)
│   └─ Askerleri eğit
├── Laboratory (1.200 Gold)
│   └─ Araştırmalar yap (gelecek)
└── Market (600 Gold)
    └─ Kaynakları ticaret et
```

#### İnşaat Mekanikler:

```
1. Bina Seç → Maliyet Göster
2. Kaynakları Çıkar
3. İnşaat Başla (Zaman: 60-120 sn)
4. İlerleme Göster (%)  
5. Tamamlandı → Üretim Başla
```

#### Bina Yükseltme:

```
Level    Maliyet Artışı    Build Time Artışı
──────────────────────────────────────────────
1→2      1.2x             1.2x (80 sn)
2→3      1.2x             1.2x (96 sn)
3→4      1.2x             1.2x (115 sn)
```

---

### 3️⃣ **Asker Eğitimi**

#### Birim Türleri (6 Tip):

```
⚔️  WARRIOR (Savaşçı)
├─ Cost: 100 Gold + 50 Food + 20 Iron
├─ Health: 100 | Attack: 15 | Defense: 5
├─ Speed: 3.5 | Role: Balanced
└─ Best For: Genel savaş

🏹 ARCHER (Okçu)
├─ Cost: 80 Gold + 40 Food + 10 Iron
├─ Health: 60 | Attack: 20 | Defense: 2
├─ Speed: 4.0 | Role: Ranged Damage
└─ Best For: Uzaktan hasar

🛡️  KNIGHT (Şövalye)
├─ Cost: 200 Gold + 80 Food + 80 Iron
├─ Health: 150 | Attack: 20 | Defense: 12
├─ Speed: 2.5 | Role: Tank
└─ Best For: Direnç ve ön cephe

🔮 MAGE (Sihirci)
├─ Cost: 150 Gold + 30 Food + 30 Iron
├─ Health: 50 | Attack: 25 | Defense: 3
├─ Speed: 3.0 | Role: High Damage
└─ Best For: Hasar çıkarmak

🚀 SCOUT (Keşfeci)
├─ Cost: 50 Gold + 20 Food + 5 Iron
├─ Health: 30 | Attack: 8 | Defense: 1
├─ Speed: 5.5 | Role: Reconnaissance
└─ Best For: Hız, keşif

✨ HEALER (İyileştirici)
├─ Cost: 120 Gold + 60 Food + 15 Iron
├─ Health: 70 | Attack: 5 | Defense: 4
├─ Speed: 3.0 | Role: Support
└─ Best For: Destek ve iyilendirme
```

#### Askeri Strateji:

```
Dengeleme Ordusu (Başlangıç)
├─ 2 Warriors     (Denge)
├─ 1 Archer       (Hasar)
└─ 1 Scout        (Hız)

Defensif Konfigürasyon
├─ 3 Knights      (Duvar)
└─ 2 Healers      (Destek)

Ağır Saldırı
├─ 2 Mages        (Hasar)
├─ 2 Archers      (Ek Hasar)
└─ 1 Knight       (Tank)
```

---

### 4️⃣ **Klan Sistemi**

#### Klan Oluşturma:

```
1. "Clan" menüsüne tıkla
2. "Create Clan" seç
3. Klan adı gir (3-20 karakter)
4. Klan oluştur ✓
5. Başlangıç üyeleri: Sen + 4 NPC
```

#### Klan Seviyeleri:

```
Level   Deneyim Gerekli   Avantajlar
─────────────────────────────────────
1       0                 Bölge kapma başla
2       10.000            +50 üye sınırı
3       20.000            +100 üye sınırı
4       30.000            Bazı maliyetler -10%
5       40.000            Askeri güç +20%
```

#### Bölge Kontrolü:

```
✅ Klan Bölgeleri
├─ Harita üzerindeki yeşil alanlar
├─ Başlangıç: 5x5 kare (25 tile)
├─ Genişletme: Sınırda yeni bölge iddia et
└─ Değer: Kaynak üretimi +10% / tile

🎯 Bölge Savunması
├─ Düşman klanlar bölgeleri saldırabilir
├─ Savunma kuleleri otomatik ateş eder
├─ Kaybı engelleme: Kuvvetli askerler tutun
└─ Stratejik: Haritalanmış defensif noktalar
```

#### Klan Ödülleri:

```
Milyondan Sonra:
├─ Bölge Kontrolü: 50 Gold/saat / tile
├─ Klan Altını Havuzu: +1.000/hafta
├─ Üye Bonusları: +5% Üretim / 50 üyede
└─ Savaş Ödülleri: +50% Deneyim
```

---

### 5️⃣ **Savaş Sistemi**

#### Savaş Başlatma:

```
1. Haritada düşman bölgesini seç
2. "Attack" butonuna tıkla
3. Ordu seç (min 5 asker)
4. "Confirm" tıkla
5. Savaş başlıyor! ⚔️
```

#### Savaş Mekaniği:

```
Her Tur:
1. En hızlı birimler saldırır ilk
2. Hedef = Rastgele düşman seç
3. Hasar = Saldırı - Savunma
4. Düşük HP → Ölüm
5. Savaş bittiyse Kazanan Seç

Savaş Skoru:
- Saldırgan: 1000 Deneyim (kazanırsa)
- Savunucu: 500 Deneyim (tutuştursa)
```

#### Stratejik İpuçları:

```
💡 İyi İçin
✓ Knights'ı önde tut
✓ Archer'ları arka sırada
✓ Mages'i erken açma
✓ Healers ile büyük savaşları sür

❌ Kaçınılacak
✗ Çok Scout gönderme
✗ Healers olmadan uzun savaş
✗ Savunmasız bölgeyi saldır
✗ Birlikte Mage kalabalığı
```

---

### 6️⃣ **Harita Sistemi**

#### Harita Özellikleri:

```
Boyut: 100 x 100 tile
Arazi Türleri:
├─ 🟩 Grass (50%) - Normal arazi
├─ 🌲 Forest (20%) - Orman
├─ ⛰️  Mountain (15%) - Dağ
├─ 💧 Water (10%) - Su (bina yapılamaz)
└─ 🏜️  Desert (5%) - Çöl
```

#### Minimap:

```
- Tüm haritayı küçültülmüş göster
- Klanları renkle işaretle
- Savaş yerleri kırmızı nokta
- Zoom: +/- tuşları veya slider
```

#### Stratejik Yerleşim:

```
İdeal Setup:
├─ Town Hall: Merkez
├─ Üretim Binaları: Etrafında
├─ Savunma: Sınırlarda
└─ Barracks: Kuzey ucunda
```

---

### 7️⃣ **Ekonomi & Ticaret**

#### Pazar Fiyatları (Dinamik):

```
Kaynak    Fiyat Aralığı    Değişim
─────────────────────────────────
Gold      1 (Sabit)       -
Wood      30-100          ±5 /dakika
Stone     30-100          ±5 /dakika
Food      20-80           ±5 /dakika
Iron      50-150          ±8 /dakika
```

#### Ticaret Stratejileri:

```
💼 Al-Sat Oyunu
1. Düşük fiyattan al (30 Wood)
2. Yüksek fiyattan sat (100 Wood)
3. Kar: 70 Wood değerinde Gold

📈 Uzun Vadeli
1. Düşük demlerde bikin
2. Market yüksekken sat
3. Kar: Milyonlar
```

---

### 8️⃣ **Video Reklam Sistemi**

#### Ödüllü Videolar:

```
📺 Reklamı İzle → 100 Gold Al

Kısıtlamalar:
├─ Cooldown: 15-30 dakika arası
├─ Limite: Oyuncu başına max 5/gün
├─ Gerekli: İnternet bağlantısı
└─ Ödül: 100 Gold (ayarlanabilir)

Kullanım Zamanı:
✓ Kızgın olunca hızlı Gold al
✓ Başlangıç önyükleme
✓ Premium özellikler deneme
```

---

## 🏅 Başarılar & Ödüller

```
🎖️  BAŞARILAR (5+)

1. "Builder"
   └─ Herhangi bir bina inşa et → 500 Gold

2. "Commander"  
   └─ 100 asker eğit → 2.000 Gold

3. "Veteran"
   └─ Level 10'a ulaş → 1.000 Gold

4. "Warrior"
   └─ 10 savaş kazan → 1.500 Gold

5. "Ad Watcher"
   └─ 50 reklam izle → 3.000 Gold
```

---

## 💾 Otomatik Kaydetme

```
✅ Sistem her 5 dakikada bir otomatik kaydeder
✅ Oyundan çıkışta kaydeder
✅ Offline ilerlemesi hesaba eklenir
✅ Veri bulutta senkronize edilir (gelecek)
```

---

## 🎮 Başlangıç Rehberi (İlk 30 dakika)

```
ADIM 1 (0-5 dakika): Kurulum
├─ Oyuncu adınızı girin
├─ Town Hall'u yükseltin
└─ İlk Farm'ı inşa edin

ADIM 2 (5-15 dakika): Bina Genişletmesi
├─ 3 üretim binası ekleyin
├─ Kaynakları biriktirin
└─ Barracks inşa edin

ADIM 3 (15-25 dakika): Asker Eğitimi
├─ İlk 10 askeri eğitin
├─ Savunma yoluyla duvar ekleyin
└─ İlk bölgeyi iddia edin

ADIM 4 (25-30 dakika): Klan Kurma
├─ Klan oluşturun
├─ Arkadaşlarınızı davet edin
└─ İlk savaş planlayın
```

---

## 🎯 Uzun Vadeli Hedefler

```
🌟 HAFTASAL
├─ Level 15'e ulaş
├─ 3 savaş kazan
├─ Bölgesini 10x10'a genişlet
└─ 500 askeri bir araya getir

🌟 AYLIK
├─ Level 50'ye ulaş
├─ Klanını 50 üyeye getir
├─ Başarıların 50%'sini aç
└─ 1 milyondan fazla Gold biriştir

🌟 SEZONLuk (3 ay)
├─ Level 100'e ulaş
├─ Klanını Top 10'a getir
├─ Tüm başarıları aç
└─ Liderlik tahtasında ilkse!
```

---

## ⚙️ Ayarlar & Özelleştirme

```
🔧 SES
├─ Master Volume (0-100%)
├─ Müzik (0-100%)
├─ SFX (0-100%)
└─ Bildirimler (On/Off)

🖼️  GRAFİKLER
├─ Kalite: Düşük/Orta/Yüksek
├─ FPS Sınırı: 30/60/120
├─ Gölgeler: On/Off
└─ Partiküller: On/Off

⏰ OYUN
├─ Oyun Hızı: 0.5x - 2x
├─ Otomatik Kaydetme: On/Off
├─ Push Notifications: On/Off
└─ Offline İlerleme: On/Off
```

---

## 🆘 SSS (Sıkça Sorulan Sorular)

**S: Askerleri kayır mı?**
C: Hayır, eğitildi eğitildi durur.

**S: Bina yok edebilir miyim?**
C: Hayır, sadece yükseltebilir/inşa edebilirsiniz.

**S: Bir klandan diğerine geçebilir miyim?**
C: Evet, "Clan" menüsünde "Leave Clan" tıklayın.

**S: Savaş kaybedersem ne olur?**
C: Sadece askerleri kaybedersiniz, binalar güvenledir.

**S: Offline para kazanabilir miyim?**
C: Evet! Binalar offline saat başına 50% üretir (max 12 saat).

---

**İyi Oyunlar! 🎮**

Son Güncelleme: 2026-05-17
