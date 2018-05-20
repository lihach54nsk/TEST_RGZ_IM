using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_RGZ_IM
{
    public class Program
    {
        static void Main(string[] args)
        {
            double time = 0;
            for (; ; )
            {
                Console.WriteLine("Введите текущее время");
                time = Convert.ToDouble(Console.ReadLine());
                if (time == 0) break;
                Console.WriteLine("Время прихода следующей заявки");
                Console.WriteLine(GetNextPeopleTime(false));
                Console.WriteLine("Количество заявок, пришедших в очередь за 1 раз");
                Console.WriteLine(GetNextPeopleCount(false));
                Console.WriteLine("Время до возникновения следующей волны");
                Console.WriteLine(GetWave());
                Console.WriteLine("Продолжительность волны");
                Console.WriteLine(GetWaveLength());
                Console.WriteLine("Время обслуживания заявки");
                Console.WriteLine(GetServiceTime(time));
                Console.WriteLine("Время совершения заказа");
                Console.WriteLine(GetOrderTime());
                Console.ReadLine();
            }
            // Console.WriteLine("Время совершения заказа");
            // Console.ReadLine();
        }

        readonly static Random random = new Random();

        static double R => (double)random.Next(int.MaxValue) / int.MaxValue;

        /// <summary>
        /// Время прихода следующей заявки
        /// Равномерный закон распределения
        /// </summary>
        /// <param name="isWave">Режим волны</param>
        public static double GetNextPeopleTime(bool isWave)
        {
            if (isWave == false) return 1.5 + PlusMinus(); // 0.67 человека в минуту
            else return 0.75 + PlusMinus();
        }

        /// <summary>
        /// Количество заявок, пришедших в очередь за 1 раз
        /// Нормальный закон распределения
        /// </summary>
        /// <param name="isWave">Режим волны</param>
        public static int GetNextPeopleCount(bool isWave)
        {
            if (isWave == false) return random.Next(1, 4);
            else return random.Next(1, 4);
        }

        /// <summary>
        /// Время до возникновения следующей волны
        /// Экспоненциальный закон распределения
        /// </summary>
        public static double GetWave() => 360 + PlusMinusWave();
      
        /// <summary>
        /// Продолжительность волны
        /// </summary>
        /// <returns></returns>
        public static double GetWaveLength()
        {
            if (random.Next(0, 1) == 0) return 90 + 30 * random.NextDouble();
            else return 90 - 30 * random.NextDouble();
        }

        /// <summary>
        /// Время обслуживания заявки
        /// Равномерный закон распределения
        /// </summary>
        /// <returns></returns>
        public static double GetServiceTime(double time)
        {
            if (time < 960) return 6.5 + PlusMinusWork(time);
            else return 11 + PlusMinusWork(time);
        }

        /// <summary>
        /// Время совершения заказа
        /// Равномерный закон распределения
        /// </summary>
        /// <returns></returns>
        public static double GetOrderTime() => 0.75 + PlusMinusOrder() / 2;

        /// <summary>
        /// Генерация времени разброса заказа заявки
        /// </summary>
        /// <returns></returns>
        static double PlusMinusOrder()
        {
            if (random.Next(0, 1) == 0) return random.NextDouble();
            else return -random.NextDouble();
        }

        static double PlusMinusWave()
        {
            if (random.Next(0, 1) == 0) return 30 * random.NextDouble();
            else return -30 * random.NextDouble();
        }

        /// <summary>
        /// Генерация разброса для времени обслуживания заявки в зависимости от времени суток
        /// </summary>
        /// <param Время суток="time"></param>
        /// <returns></returns>
        static double PlusMinusWork(double time)
        {
            if (time < 960)
            {
                if (random.Next(0, 1) == 0) return random.NextDouble();
                else return -random.NextDouble();
            }
            else
            {
                if (random.Next(0, 1) == 0) return 2 * random.NextDouble();
                else return 2 * random.NextDouble();
            }
        }

        static double PlusMinus()
        {
            if (random.Next(0, 1) == 0) return 0.322 * random.NextDouble();
            else return -0.5 * random.NextDouble();
        }
    }
}