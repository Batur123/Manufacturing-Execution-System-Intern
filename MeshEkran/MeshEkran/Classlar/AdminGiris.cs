﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasDLL;
using System.Data.SqlClient;

namespace MeshEkran.Classlar
{
    public class AdminGiris
    {

        #region AdminGirişKontrol
        public static string KullaniciAdi, Sifre;
        
        public KullaniciDLL.Admin AdminKontrol(string KullaniciAdi, string Sifre)
        {
            KullaniciDLL.Admin adminuser = null;
            using (var connection = MeshEkran.Veritabani.Database.GetConnection())
            {
                var command = new SqlCommand("SELECT *FROM Admin WHERE KullaniciAdi='" + KullaniciAdi + "'and Sifre='" + Sifre + "'");
                command.Connection = connection;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        adminuser = new KullaniciDLL.Admin();

                        adminuser.KullaniciID = reader.GetInt32(0);
                        adminuser.KullaniciAdi = reader.GetString(1);
                        adminuser.Sifre = reader.GetString(2);
                        adminuser.Isim = reader.GetString(3);
                        adminuser.Soyisim = reader.GetString(4);

                    }
                }
                connection.Close();
            }
            return adminuser;
        }
        #endregion

        #region AdminKayitKontrol
        private bool AdminVarsa(KullaniciDLL.Admin adminuser)
        {
            bool result = false;
            using (var connection = MeshEkran.Veritabani.Database.GetConnection())
            {
                var command = new SqlCommand("SELECT *FROM Admin WHERE KullaniciAdi='" + adminuser.KullaniciAdi + "'");
                command.Connection = connection;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = true;
                    }
                }
                connection.Close();
            }
            return result;
        }
        #endregion

        #region AdminKayıtYapma
        public bool AdminInsert(KullaniciDLL.Admin adminuser)
        {
            bool result = false;
            if (!AdminVarsa(adminuser))
            {
                using (var connection = MeshEkran.Veritabani.Database.GetConnection())
                {
                    var command = new SqlCommand("INSERT INTO Admin(KullaniciAdi,Sifre,Ad,Soyad) VALUES('" + adminuser.KullaniciAdi + "','" + adminuser.Sifre + "','" + adminuser.Isim + "','" + adminuser.Soyisim + "')");
                    command.Connection = connection;
                    connection.Open();
                    if (command.ExecuteNonQuery() != -1)
                    {
                        result = true;
                    }
                    connection.Close();
                }
            }
            return result;
        }
        #endregion

    }
}

