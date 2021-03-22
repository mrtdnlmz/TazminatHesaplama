using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TazminatHesaplama
{
    public partial class Hesaplama : Form
    {
        DateTime iseGiris, istenCikis;
        int kacGun, ihbarSure, ihbarGun, yil, ay, gun;
        double netUcret, brutUcret, ikramiye, yol = 0.0, yemek = 0.0, bayram = 0.0, kidemBrutu, toplamKidem, tk1, tk2, tk3, ihbarGV, KidemDV, kidemNet, toplamTutar, adet, ikramiyeBrut,
            bayramBrut, ihbarBrut, ihbarDV, ihbarNet, kidemtavan = 6730.15,kidemtavan2=7116.56, gidaGiyim, yakacak, aile, cocuk, egitim, devam, yipranma;

        double fark1, fark2, kgvm, ihbargv1, ihbargv2, toplamGV;



        public void btnHesapla_Click(object sender, EventArgs e)
        {

            GunHesap();
            CalismaGunHesap();
            Tazminat();
            ihbarGVHesap();



        }
        public void CalismaGunHesap()
        {
            iseGiris = DateTime.Parse(txtiseGiris.Text);
            istenCikis = DateTime.Parse(txtistenCikis.Text);
            kacGun = (int)(istenCikis - iseGiris).TotalDays + 1;
            txtGun.Text = Convert.ToString(kacGun + " " + "Gün");
            if (kacGun <= 42)
            {
                ihbarSure = 2;
            }
            else if (kacGun > 43 && kacGun <= 546)
            {
                ihbarSure = 4;
            }
            else if (kacGun > 547 && kacGun <= 1092)
            {
                ihbarSure = 6;
            }
            else if (kacGun >= 1093)
            {
                ihbarSure = 8;
            }

        }
        private void Tazminat()
        {

            netUcret = Convert.ToInt32(txtNetUcret.Text);
            brutUcret = netUcret / 0.7149;
            txtBrut.Text = Convert.ToString(Math.Round(brutUcret, 2));
            ikramiye = Convert.ToInt32(txtikramiye.Text);
            adet = Convert.ToInt32(txtAdet.Text);
            ikramiyeBrut = (ikramiye / 0.7149) * adet / 12;
            txtikramiyeBrut.Text = Convert.ToString(Math.Round(ikramiyeBrut, 2));
            yol = Convert.ToDouble(txtYol.Text);
            yemek = Convert.ToDouble(txtYemek.Text);
            bayram = Convert.ToDouble(txtBayram.Text) / 12;
            gidaGiyim = Convert.ToDouble(txtGidaGiyim.Text) / 12;
            yakacak = Convert.ToDouble(txtYakacak.Text) / 12;
            egitim = Convert.ToDouble(txtEgitim.Text) / 12;
            aile = Convert.ToDouble(txtAileYard.Text);
            cocuk = Convert.ToDouble(txtCocuk.Text);
            devam = Convert.ToDouble(txtDevam.Text);
            yipranma = Convert.ToDouble(txtYipranma.Text);
            toplamKidem = (brutUcret + ikramiyeBrut + yol + yemek + bayram + gidaGiyim
                             + yakacak + egitim + aile + cocuk + devam + yipranma);
            ihbarGun = ihbarSure * 7;
            ihbarBrut = (brutUcret + ikramiyeBrut + yol + yemek + bayram + gidaGiyim
                         + yakacak + egitim + aile + cocuk + devam + yipranma) / 30 * ihbarGun;


            if (toplamKidem <= kidemtavan)
            {
                kidemBrutu = toplamKidem;
            }
            else
            {
                kidemBrutu = kidemtavan;
                lblUyari.Text = Convert.ToString("Kıdem Tavanı!");
            }

            txtEsasKBrut.Text = Convert.ToString(Math.Round(kidemBrutu, 2));
            tk1 = kidemBrutu * yil;
            tk2 = kidemBrutu / 12 * ay;
            tk3 = kidemBrutu / 365 * gun;
            toplamKidem = tk1 + tk2 + tk3;
            txtHspKidem.Text = Convert.ToString(Math.Round(toplamKidem, 2));
            KidemDV = toplamKidem * 0.00759;
            txtDV.Text = Convert.ToString(Math.Round(KidemDV, 2));
            kidemNet = toplamKidem - KidemDV;
            txtKidemNet.Text = Convert.ToString(Math.Round(kidemNet, 2));
            txtToplamİhbar.Text = Convert.ToString(ihbarSure + " " + "Hafta");

       

        }

        public Hesaplama()
        {
            InitializeComponent();
        }

        private void Hesaplama_Load(object sender, EventArgs e)
        {
            txtGun.Enabled = false;
            txtBrut.Enabled = false;
            txtikramiyeBrut.Enabled = false;
            txtEsasKBrut.Enabled = false;
            txtDV.Enabled = false;
            txtKidemNet.Enabled = false;
            txtHspKidem.Enabled = false;
            txtihbarGV1.Enabled = false;
            txtihbarGV2.Enabled = false;
            txtihbarBrut.Enabled = false;
            txtihbarDV.Enabled = false;
            txtToplamİhbar.Enabled = false;
            txtihbarNet.Enabled = false;
        }
        public void GunHesap()
        {
            GunHesap hsp = new GunHesap();
            hsp.ikiTarihFarki(DateTime.Parse(txtistenCikis.Text), DateTime.Parse(txtiseGiris.Text));
            lbl_gobel.Text = hsp.Yil3.ToString() + " Yıl " + hsp.Ay3.ToString() + " Ay " + hsp.Gun3.ToString() + " Gün ";
            yil = hsp.Yil3;
            ay = hsp.Ay3;
            gun = hsp.Gun3;

        }

        public void ihbarGVHesap()
        {
            kgvm = Convert.ToInt32(txtKGVM.Text);

            if (kgvm > 0)
            {
                if (kgvm <= 22000)
                {
                    fark1 = 22000 - kgvm;
                    if (fark1 > ihbarBrut)
                    {
                        ihbargv1 = ihbarBrut * 15 / 100;
                    }
                    else if (fark1 < ihbarBrut)
                    {
                        ihbargv1 = fark1 * 15 / 100;
                        fark2 = ihbarBrut - fark1;
                        ihbargv2 = fark2 * 20 / 100;
                    }

                    else if (fark1 < ihbarBrut)
                    {
                        fark2 = ihbarBrut - fark1;
                        ihbargv2 = fark2 * 20 / 100;
                    }
                }
                else if (kgvm > 22000 && kgvm <= 49000)
                {
                    fark1 = 49000 - kgvm;
                    if (fark1 > ihbarBrut)
                    {
                        ihbargv1 = ihbarBrut * 20 / 100;
                    }
                    else if (fark1 < ihbarBrut)
                    {
                        fark2 = ihbarBrut - fark1;
                        ihbargv2 = fark2 * 27 / 100;
                    }
                }
                else if (kgvm > 49000 && kgvm <= 120000)
                {
                    fark1 = 49000 - kgvm;
                    if (fark1 > ihbarBrut)
                    {
                        ihbargv1 = ihbarBrut * 27 / 100;
                    }
                    else if (fark1 < ihbarBrut)
                    {
                        fark2 = ihbarBrut - fark1;
                        ihbargv2 = fark2 * 35 / 100;
                    }
                }
                else if (kgvm > 120000 && kgvm <= 600000)
                {
                    fark1 = 49000 - kgvm;
                    if (fark1 > ihbarBrut)
                    {
                        ihbargv1 = ihbarBrut * 35 / 100;
                    }
                    else if (fark1 < ihbarBrut)
                    {
                        fark2 = ihbarBrut - fark1;
                        ihbargv2 = fark2 * 40 / 100;
                    }
                }
                else if (kgvm > 600000)
                {

                    ihbargv1 = ihbarBrut * 40 / 100;

                }
            }
            else
            {
                ihbarGV = ihbarBrut * 15 / 100;
            }

            txtihbarBrut.Text = Convert.ToString(Math.Round(ihbarBrut, 2));
            ihbarDV = ihbarBrut * 0.00759;
            ihbarNet = ihbarBrut - (ihbargv1 + ihbargv2 + ihbarDV);
            txtihbarDV.Text = Convert.ToString(Math.Round(ihbarDV, 2));
            txtihbarGV1.Text = Convert.ToString(Math.Round(ihbargv1, 2));
            txtihbarGV2.Text = Convert.ToString(Math.Round(ihbargv2, 2));
            txtihbarNet.Text = Convert.ToString(Math.Round(ihbarNet, 2));
            toplamTutar = kidemNet + ihbarNet;
            txtToplamNet.Text = Convert.ToString(Math.Round(toplamTutar, 2));
        }

        //public void hesap()
        //{
        //    int sonuc;
        //    sonuc = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text);
        //    MessageBox.Show(sonuc.ToString());
        //}

        // örnek tab tuşunun enter tuşuna atanması
        //private void txtiseGiris_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == 13)
        //    {
        //        SendKeys.Send("{TAB}");
        //    }
        //}


    }
}
// Arayüz Katmanı: textler-griedviewler-datasource
// İş Katmanı (business): if'lerin yazıldığı katman.
// Veri Erişim Katmanı: insert-delete-select-update komutlarının yazıldığı katman.
// Yardımcı Katman Entity Katmanı: Veri tabanındaki nesneleri içeren katman
