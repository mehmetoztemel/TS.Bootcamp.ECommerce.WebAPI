# TS.Bootcamp.ECommerce.WebAPI

🚀 Bu proje, e-ticaret işlemleri için bir WebAPI sağlar. Kullanıcı yönetimi, ürün işlemleri, sepet yönetimi ve sipariş takibi gibi temel özellikler içerir.

## Özellikler
- **Kullanıcı Yönetimi:** Kullanıcı modeli ve Register/Login metoduyla güvenli giriş sağlar.
- **Ürün İşlemleri:** Ürün modeliyle birlikte Create ve GetAll metodlarıyla ürünleri yönetir.
- **Sepet İşlemleri:** Sepet modeli, Create, GetAll ve Pay metodlarıyla alışveriş sepetini yönetir.
- **Sipariş Yönetimi:** Sipariş modeliyle GetAll metoduyla tüm siparişleri takip eder.

## Güvenlik ve Performans
- Veri işlemleri için Model ve DTO kullanır.
- Rate Limiting ile istekleri sınırlar.
- Detaylı CORS ayarlarıyla güvenliği maksimum seviyeye çıkarır.
- Health Check ile sistem sağlığını düzenli olarak kontrol eder.

## HTTP Metodları ve Parametreler
- GET, POST, PUT, DELETE metodları desteklenir.
- Query Params ve Route Params ile istekler yönetilir.

## Teknolojiler
- C#, ASP.NET Core, Entity Framework Core, Swagger

## Nasıl Başlarım?
1. Projeyi klonlayın: `git clone https://github.com/mehmetoztemel/TS.Bootcamp.ECommerce.WebAPI.git`
2. Proje dizinine gidin: `cd TS.Bootcamp.ECommerce.WebAPI`
3. Proje bağımlılıklarını yükleyin: `dotnet restore`
4. Uygulamayı başlatın: `dotnet run`

## Katkıda Bulunma
1. Fork projesi
2. Yeni özellikler ekleyin veya hataları düzeltin
3. Değişiklikleri commit edin: `git commit -am 'Yeni özellik: ...'`
4. Fork'a push yapın: `git push origin master`
5. Pull Request gönderin

Sorularınız veya geri bildirimleriniz varsa çekinmeden paylaşabilirsiniz!

