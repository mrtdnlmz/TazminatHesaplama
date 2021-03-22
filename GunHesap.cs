using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TazminatHesaplama
{
    public class GunHesap
    {

        public static int[] sonuc = new int[3];
        public int[] ikiTarihFarki(DateTime sonTarih, DateTime ilkTarih)

        {

            int ilkGun, ilkAy, ilkYil;

            int sonGun, sonAy, sonYil;

            int farkYil, farkAy, farkGun;

            farkYil = 0; farkAy = 0; farkGun = 0;



            ilkYil = ilkTarih.Year;

            ilkAy = ilkTarih.Month;

            ilkGun = ilkTarih.Day;



            sonGun = sonTarih.Day;

            sonAy = sonTarih.Month;

            sonYil = sonTarih.Year;



            if (sonGun < ilkGun)

            {

                sonGun += DateTime.DaysInMonth(sonYil, sonAy);

                farkGun = sonGun - ilkGun;

                sonAy--;

                if (sonAy < ilkAy)

                {

                    sonAy += 12;

                    sonYil--;

                    farkAy = sonAy - ilkAy;

                    farkYil = sonYil - ilkYil;

                }

                else

                {

                    farkAy = sonAy - ilkAy;

                    farkYil = sonYil - ilkYil;

                }

            }

            else

            {

                farkGun = sonGun - ilkGun;

                if (sonAy < ilkAy)

                {

                    sonAy += 12;

                    sonYil--;

                    farkAy = sonAy - ilkAy;

                    farkYil = sonYil - ilkYil;

                }

                else

                {

                    farkAy = sonAy - ilkAy;

                    farkYil = sonYil - ilkYil;

                }

            }



       

            sonuc[0] = farkYil;
            Yil3= farkYil;

            sonuc[1] = farkAy;
            Ay3 = farkAy;

            sonuc[2] = farkGun+1;
            Gun3 = farkGun+1;

            return sonuc;
        }

        public int Yil3;
        public int Yil 
        {
            get
            {
                return Yil3;
            }
            set
            {
                Yil3 = value;
            }
        }
        public int Ay3;
        public int Ay 
        {
            get
            {
                return Yil3;
            }
            set
            {
                Yil3 = value;
            }
        }
        public int Gun3;
        public int Gun 
        {
            get
            {
                return Yil3;
            }
            set
            {
                Yil3 = value;
            }
        }

    }
}
