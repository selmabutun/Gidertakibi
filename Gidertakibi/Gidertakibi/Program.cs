using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExpenseTracker
{
	class Program
	{
		// Harcamaları tutacak liste
		private static readonly List<Expense> expenses = new List<Expense>();

		static void Main(string[] args)
		{
			Console.Title = "💸 Mini Gider Takibi";
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			while (true)
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("\n==== GİDER TAKİP MENÜSÜ ====");
				Console.ResetColor();

				Console.WriteLine("1) Yeni Harcama Ekle");
				Console.WriteLine("2) Harcamaları Listele");
				Console.WriteLine("3) Toplam Tutarı Göster");
				Console.WriteLine("0) Çıkış Yap");
				Console.Write("Seçimin: ");

				string secim = Console.ReadLine();
				Console.WriteLine();

				switch (secim)
				{
					case "1":
						HarcamaEkle();
						break;
					case "2":
						HarcamalariListele();
						break;
					case "3":
						ToplamiGoster();
						break;
					case "0":
						return; // Programdan çık
					default:
						HataYaz("Geçersiz seçim yaptınız!");
						break;
				}
			}
		}

		private static void HarcamaEkle()
		{
			Console.Write("Açıklama: ");
			string aciklama = Console.ReadLine().Trim();

			Console.Write("Tutar (₺): ");
			decimal tutar;
			bool basariliMi = decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out tutar);

			if (!basariliMi || tutar <= 0)
			{
				HataYaz("Geçerli bir tutar girin (0'dan büyük sayı).");
				return;
			}

			Expense yeniHarcama = new Expense();
			yeniHarcama.Date = DateTime.Now;
			yeniHarcama.Description = aciklama;
			yeniHarcama.Amount = tutar;

			expenses.Add(yeniHarcama);

			BasariYaz("✅ Harcama başarıyla eklendi!");
		}

		private static void HarcamalariListele()
		{
			if (expenses.Count == 0)
			{
				Console.WriteLine("📭 Henüz hiç harcama eklenmedi.");
				return;
			}

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Tarih\t\t       Açıklama\t\tTutar (₺)");
			Console.ResetColor();

			foreach (Expense harcama in expenses)
			{
				Console.WriteLine("{0:yyyy-MM-dd HH:mm}\t{1}\t\t{2:F2}", harcama.Date, harcama.Description, harcama.Amount);
			}
		}

		private static void ToplamiGoster()
		{
			decimal toplam = 0;
			foreach (Expense harcama in expenses)
			{
				toplam += harcama.Amount;
			}

			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("🔢 Toplam Harcama: {0:F2} ₺", toplam);
			Console.ResetColor();
		}

		private static void HataYaz(string mesaj)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("⚠️  " + mesaj);
			Console.ResetColor();
		}

		private static void BasariYaz(string mesaj)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(mesaj);
			Console.ResetColor();
		}
	}

	class Expense
	{
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
