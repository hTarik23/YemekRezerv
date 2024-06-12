using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Rezervasyon.Controllers;
using Rezervasyon.Models;
using System.Data;

namespace Rezervasyon.Insfracture;

public class DataAccess
{
    public bool AddReservation(int customerId, int restaurantId, DateTime date, TimeSpan time, int numberOfPeople)
    {
        try
        {
            using (var context = new AppDbContext())
            {
                var customerIdParameter = new SqlParameter("@MusteriID", customerId);
                var restaurantIdParameter = new SqlParameter("@RestoranID", restaurantId);
                var dateParameter = new SqlParameter("@Tarih", date);
                var timeParameter = new SqlParameter("@Saat", time);
                var numberOfPeopleParameter = new SqlParameter("@KisiSayisi", numberOfPeople);

                context.Database.ExecuteSqlRaw("EXEC RezervasyonEkle @MusteriID, @RestoranID, @Tarih, @Saat, @KisiSayisi", customerIdParameter, restaurantIdParameter, dateParameter, timeParameter, numberOfPeopleParameter);

                return true;
            }
        }
        catch (Exception ex)
        {
            // Hata günlüğü ve hata işleme yapılabilir.
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool AddMusteri(string ad, string soyad, string telefon)
    {
        try
        {
            using (var context = new AppDbContext())
            {
                var adParameter = new SqlParameter("@Adı", ad);
                var soyadParameter = new SqlParameter("@Soyadı", soyad);
                var telefonParameter = new SqlParameter("@TelefonNumarası", telefon);

                context.Database.ExecuteSqlRaw("EXEC MusteriEkle @Adı, @Soyadı, @TelefonNumarası", adParameter, soyadParameter, telefonParameter);

                return true;
            }
        }
        catch (Exception ex)
        {
            // Hata günlüğü ve hata işleme yapılabilir.
            Console.WriteLine(ex.Message);
            return false;
        }
    }
	public bool AddRestorant(string ad, string adress, string telefon)
	{
		try
		{
			using (var context = new AppDbContext())
			{
				var adParameter = new SqlParameter("@Adı", ad);
				var adressParameter = new SqlParameter("@Adres", adress);
				var telefonParameter = new SqlParameter("@TelefonNumarası", telefon);

				context.Database.ExecuteSqlRaw("EXEC RestoranEkle @Adı, @Adres, @TelefonNumarası", adParameter, adressParameter, telefonParameter);

				return true;
			}
		}
		catch (Exception ex)
		{
			// Hata günlüğü ve hata işleme yapılabilir.
			Console.WriteLine(ex.Message);
			return false;
		}
	}
	public async Task<List<Musteriler>> TumMusterileriGetir()
    {
        try
        {
            List<Musteriler> musteriler = new List<Musteriler>();

            using (var context = new AppDbContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "TumMusterileriGetir";
                    command.CommandType = CommandType.StoredProcedure;

                    context.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Musteriler musteri = new Musteriler();
                            musteri.MusteriID = int.Parse(reader["MusteriID"].ToString());
                            musteri.Ad = reader["Adı"].ToString();
                            musteri.Soyad = reader["Soyadı"].ToString();
                            musteri.Telefon = reader["TelefonNumarası"].ToString();
                            musteriler.Add(musteri);
                        }
                    }
                }
            }

            return musteriler;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
	public async Task<List<Restorantlar>> TumRestoratlariGetir()
	{
		try
		{
			List<Restorantlar> restorantlardizi = new List<Restorantlar>();

			using (var context = new AppDbContext())
			{
				using (var command = context.Database.GetDbConnection().CreateCommand())
				{
					command.CommandText = "RestoranGetir";
					command.CommandType = CommandType.StoredProcedure;

					context.Database.OpenConnection();

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Restorantlar restorantlar = new Restorantlar();
							restorantlar.RestoranID = int.Parse(reader["RestoranID"].ToString());
							restorantlar.Ad = reader["Adı"].ToString();
							restorantlar.Adress = reader["Adres"].ToString();
							restorantlar.Telefon = reader["TelefonNumarası"].ToString();
							restorantlardizi.Add(restorantlar);
						}
					}
				}
			}

			return restorantlardizi;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return null;
		}
	}

	public bool MusteriSil(int musteriID)
	{
		try
		{
			using (var context = new AppDbContext())
			{
				var musteriIDParameter = new SqlParameter("@MusteriID", musteriID);

				context.Database.ExecuteSqlRaw("EXEC MusteriSil @MusteriID", musteriIDParameter);

				return true;
			}
		}
		catch (Exception ex)
		{
			// Hata günlüğü ve hata işleme yapılabilir.
			Console.WriteLine(ex.Message);
			return false;
		}
	}
	public bool RestorantSil(int Restorantd)
	{
		try
		{
			using (var context = new AppDbContext())
			{
				var restorantIdpara = new SqlParameter("@RestoranID", Restorantd);

				context.Database.ExecuteSqlRaw("EXEC RestoranSil @RestoranID", restorantIdpara);

				return true;
			}
		}
		catch (Exception ex)
		{
			// Hata günlüğü ve hata işleme yapılabilir.
			Console.WriteLine(ex.Message);
			return false;
		}
	}
	public bool UpdateMusteri(int musteriId, string ad, string soyad, string telefon)
	{
		try
		{
			using (var context = new AppDbContext())
			{
				var musteriIdParameter = new SqlParameter("@MusteriID", musteriId);
				var adParameter = new SqlParameter("@Ad", ad);
				var soyadParameter = new SqlParameter("@Soyad", soyad);
				var telefonParameter = new SqlParameter("@TelefonNumarası", telefon);

				context.Database.ExecuteSqlRaw("EXEC MusteriGuncelle @MusteriID, @Ad, @Soyad, @TelefonNumarası",
											   musteriIdParameter, adParameter, soyadParameter, telefonParameter);

				return true;
			}
		}
		catch (Exception ex)
		{
			// Hata günlüğü ve hata işleme yapılabilir.
			Console.WriteLine(ex.Message);
			return false;
		}
	}
	public bool UpdateRestorant(int Id, string ad, string adress, string telefon)
	{
		try
		{
			using (var context = new AppDbContext())
			{
				var restIdParameter = new SqlParameter("@RestoranID", Id);
				var adParameter = new SqlParameter("@Ad", ad);
				var adresParameter = new SqlParameter("@Adres", adress);
				var telefonParameter = new SqlParameter("@TelefonNumarası", telefon);

				context.Database.ExecuteSqlRaw("EXEC RestoranGuncelle @RestoranID, @Ad, @Adres, @TelefonNumarası",
											   restIdParameter, adParameter, adresParameter, telefonParameter);

				return true;
			}
		}
		catch (Exception ex)
		{
			// Hata günlüğü ve hata işleme yapılabilir.
			Console.WriteLine(ex.Message);
			return false;
		}
	}
    public async Task<List<Randevular>> TumRandevulariGetir()
    {
        try
        {
            List<Randevular> randevularList = new List<Randevular>();

            using (var context = new AppDbContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "RandevuGetir";
                    command.CommandType = CommandType.StoredProcedure;

                    context.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Randevular Randevular = new Randevular();
                            Randevular.ReservationID = int.Parse(reader["RezervasyonID"].ToString());
                            Randevular.RestaurantID = int.Parse(reader["RestoranID"].ToString());
                            Randevular.CustomerID = int.Parse(reader["MusteriID"].ToString());
                            Randevular.Date = DateTime.Parse(reader["Tarih"].ToString());
                            Randevular.Time = TimeSpan.Parse(reader["Saat"].ToString());
                            randevularList.Add(Randevular);
                        }
                    }
                }
            }

            return randevularList;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<Rezervasyonlar>> TumRezervasyonlariGetir()
    {
        try
        {
            List<Rezervasyonlar> rezervasyonlar = new List<Rezervasyonlar>();

            using (var context = new AppDbContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "TumRezervasyonlariGetir";
                    command.CommandType = CommandType.StoredProcedure;

                    await context.Database.OpenConnectionAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Rezervasyonlar rezervasyon = new Rezervasyonlar
                            {
                                RezervasyonID = reader.GetInt32(reader.GetOrdinal("RezervasyonID")),
                                MusteriID = reader.GetInt32(reader.GetOrdinal("MusteriID")),
                                MusteriAdi = reader.GetString(reader.GetOrdinal("MusteriAdi")),
                                MusteriSoyadi = reader.GetString(reader.GetOrdinal("MusteriSoyadi")),
                                RestoranID = reader.GetInt32(reader.GetOrdinal("RestoranID")),
                                RestoranAdi = reader.GetString(reader.GetOrdinal("RestoranAdi")),
                                Tarih = reader.GetDateTime(reader.GetOrdinal("Tarih")),
                                Saat = TimeSpan.Parse(reader["Saat"].ToString()),
                                KisiSayisi = reader.GetInt32(reader.GetOrdinal("KişiSayısı"))
                            };

                            rezervasyonlar.Add(rezervasyon);
                        }
                    }
                }
            }

            return rezervasyonlar;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
