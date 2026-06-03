# 🏥 Hastane Otomasyon Sistemi

Modern arayüze sahip, hastalar ve doktorlar arasındaki yönetim süreçlerini kolaylaştıran bir masaüstü otomasyon projesidir. Bu proje **C# Windows Forms**, **Entity Framework** ve **MSSQL Server** kullanılarak geliştirilmiştir.

## 📌 Proje Hakkında
Bu sistem; hastane yönetimindeki hasta kayıtları, doktor listeleri ve bu iki birim arasındaki eşleştirme süreçlerini tek bir merkezden dijital olarak yönetmek amacıyla kurgulanmıştır.

## 🚀 Özellikler

### 👤 Hasta Yönetimi
* Sisteme yeni hasta kaydı ekleme
* Mevcut hasta bilgilerini güncelleme ve silme
* Detaylı hasta listeleme

### 🩺 Doktor Yönetimi
* Doktor branş ve kişisel bilgilerinin sisteme tanımlanması
* Doktor kayıtlarının güncellenmesi ve takibi

### 🔗 Hasta-Doktor Eşleştirme (İşlemler)
* Form üzerinde seçilen hastanın, ilgili doktora dinamik olarak atanması
* Arka planda ilişkisel veri tabanı (Foreign Key) mimarisi ile veri tutarlılığı

## 🛠️ Kullanılan Teknolojiler
* **Dil:** C# (.NET Framework)
* **Veri Tabanı:** MSSQL (SQL Server)
* **ORM:** Entity Framework
* **Arayüz Tasarımı:** Temiz ve Profesyonel Tasarım (Turkuaz ve Gri Tonları)

## 🗄️ Veri Tabanı Mimarisi
Proje, birbiriyle ilişkili 3 temel tablodan oluşmaktadır:
1. `tbl_hastalar` (Hasta Bilgileri)
2. `tbl_doktorlar` (Doktor Bilgileri)
3. `tbl_islemler` (Hasta ve Doktorları birbirine bağlayan ilişkisel ara tablo)
4.
