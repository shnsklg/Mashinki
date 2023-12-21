using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashinki
{
    internal class Car
    {
        private string numbr; //номер
        private double maxVol; //объём бака (л)
        private double curVol; //объём топлива в баке (л)
        private double curSpeed; //скорость движения (км/ч)
        private double maxSpeed; //максимальная скорость движения (км/ч)
        private double fuelCons; //расход топлива на 100 км (л)
        private double distance; //расстояние (км)
        private double mileage; //пробег (км)
        private List<string> trajectory = new List<string>(); //траектория движения

        /// <summary>
        /// Создание автомобиля
        /// </summary>
        public Car()
        {
            this.numbr = "";
            this.maxSpeed = 200;
            this.fuelCons = 13;
        }

        /// <summary>
        /// Выбор приватного метода
        /// </summary>
        /// <param name="cars">Список автомобилей</param>
        public void Starting(List<Car> cars)
        {
            this.CreateCar(cars);
        }

        /// <summary>
        /// Создание автомобиля
        /// </summary>
        /// <param name="cars">Список автомобилей</param>
        private void CreateCar(List<Car> cars)
        {
            if (cars.Count < 1)
            {
                Console.WriteLine("Для начала создайте хотя бы один автомобиль.\n");
            }
            cars.Add(new Car());
            Car last1 = cars.Last();
            last1.InfoInput(cars);
            Console.WriteLine();
            Console.WriteLine("Чтобы начать работу, нажмите Enter. Если хотите создать ещё автомобиль, введите '+' и нажмите Enter.");
            string answ = Console.ReadLine();
            Console.WriteLine();
            if (answ == "+")
            {
                this.CreateCar(cars);
            }
            else
            {
                this.ChooseCar(cars);
            }
        }

        /// <summary>
        /// Заполнение информации об автомобиле
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        private void InfoInput(List<Car> cars)
        {
            this.NumberInput(cars);
            this.VolumeInput();
            this.MileageInput();
        }

        /// <summary>
        /// Заполнение номера автомобиля
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        private void NumberInput(List<Car> cars)
        {
            Console.Write("Введите номер (обязательно): ");
            string numb = Console.ReadLine();
            if (numb == "")
            {
                this.NumberInput(cars);
            }
            int a = 0;
            foreach (Car car in cars)
            {
                if (numb == car.numbr)
                {
                    Console.WriteLine("Автомобиль с таким номером уже есть в базе.");
                    a++;
                    this.NumberInput(cars);
                }
            }
            if (a == 0)
            {
                this.numbr = numb;
            }
        }

        /// <summary>
        /// Заполнение объёма бака автомобиля
        /// </summary>
        private void VolumeInput()
        {
            Console.Write("Введите объём бака (должен быть > 0): ");
            double vol = Convert.ToDouble(Console.ReadLine());
            if (vol <= 0)
            {
                this.VolumeInput();
            }
            else
            {
                this.maxVol = vol;
            }
        }

        /// <summary>
        /// Заполнение пробега автомобиля
        /// </summary>
        private void MileageInput()
        {
            Console.Write("Введите пробег (должен быть не < 0): ");
            double run = Convert.ToDouble(Console.ReadLine());
            if (run < 0)
            {
                this.MileageInput();
            }
            else
            {
                this.mileage = run;
            }
        }

        /// <summary>
        /// Выбор автомобиля
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        private void ChooseCar(List<Car> cars)
        {
            Console.WriteLine("Выберите автомобиль:\n");
            foreach (Car car in cars)
            {
                Console.WriteLine((cars.IndexOf(car) + 1) + ".");
                car.InfoOutput();
                Console.WriteLine();
            }
            int carChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            foreach (Car car in cars)
            {
                if (carChoice == cars.IndexOf(car))
                {
                    car.ChooseAction(cars);
                    break;
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Вывод информации об автомобиле
        /// </summary>
        private void InfoOutput()
        {
            Console.WriteLine("Тип: легковой");
            Console.WriteLine("Номер: " + this.numbr);
            Console.WriteLine("Бак: " + this.curVol + "/" + this.maxVol + " л.");
            Console.WriteLine("Пробег: " + this.mileage + " км.");
        }

        /// <summary>
        /// Выбор действия
        /// </summary>
        /// <param name="cars">список автомобилей</param>
        private void ChooseAction(List<Car> cars)
        {
            Console.WriteLine("\nВыберите действие:\n1 - Информация об автомобиле\n2 - Спланировать маршрут\n3 - Заправка\n4 - Расчёт количества возможных аварий\n5 - Поездка\n6 - Выбрать другой автомобиль\n7 - Добавить автомобиль\nEnter - выход\n");
            string actChoice = Console.ReadLine();
            Console.WriteLine();
            switch (actChoice)
            {
                case "1":
                    this.InfoOutput();
                    this.ChooseAction(cars);
                    break;

                case "2":
                    this.Way();
                    this.ChooseAction(cars);
                    break;

                case "3":
                    this.Refill();
                    this.ChooseAction(cars);
                    break;

                case "4":
                    this.Search(cars);
                    break;

                case "5":
                    this.Drive();
                    this.ChooseAction(cars);
                    break;

                case "6":
                    this.ChooseCar(cars);
                    break;

                case "7":
                    this.CreateCar(cars);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Планирование пути
        /// </summary>
        private void Way()
        {
            Console.WriteLine("Введите координаты начала пути:");
            Console.Write("x1: ");
            double x1 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            double x0 = x1;
            Console.Write("y1: ");
            double y1 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            Console.WriteLine("Введите координаты конца пути:");
            Console.Write("x2: ");
            double x2 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            Console.Write("y2: ");
            double y2 = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            this.distance = Math.Round(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)), 2);
            if (x1 == x2)
            {
                this.Way();
            }
            else
            {
                if (x1 < x2)
                {
                    while (x1 <= x2)
                    {
                        this.trajectory.Add(Convert.ToString(x1));
                        x1++;
                    }
                }
                else if (x1 > x2)
                {
                    while (x1 >= x2)
                    {
                        this.trajectory.Add(Convert.ToString(x1));
                        x1--;
                    }
                }
                x1 = x0;
                for (int i = 0; i < this.trajectory.Count; i++)
                {
                    this.trajectory[i] += ";" + Convert.ToString(y1 + ((double)(y2 - y1) * (Convert.ToDouble(this.trajectory[i]) - x1) / (x2 - x1)));
                }
                Console.WriteLine("Маршрут спланирован.");
            }
        }

        /// <summary>
        /// Заправка
        /// </summary>
        private void Refill()
        {
            Console.WriteLine("Объём Вашего бака: " + this.maxVol + " л.");
            Console.Write("Введите объём топлива: ");
            double vol = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            if (vol == 0)
            {
                Console.WriteLine("Вы не можете заправиться на 0 л. топлива.");
                this.Refill();
            }
            else
            {
                if (vol <= Math.Round(this.maxVol - this.curVol, 2))
                {
                    this.curVol += vol;
                    Console.WriteLine("Бак заправлен.");
                }
                else
                {
                    Console.WriteLine("Вы не можете заправить объём топлива, превышающий объём бака.");
                    this.Refill();
                }
            }
        }

        /// <summary>
        /// Поездка
        /// </summary>
        private void Drive()
        {
            if (this.curVol == 0)
            {
                Console.WriteLine("Заправьтесь.");
                this.Refill();
                this.Drive();
            }
            else if (this.trajectory.Count == 0)
            {
                Console.WriteLine("Спланируйте маршрут.");
                this.Way();
                this.Drive();
            }
            else
            {
                Console.WriteLine("Общее расстояние: " + this.distance + " км.");
                while (this.distance > 0)
                {
                    if (this.curSpeed == 0)
                    {
                        this.SpeedUpIn();
                    }
                    if (Math.Round((double)this.fuelCons / 100 * this.distance, 2) <= this.curVol)
                    {
                        this.mileage += this.distance;
                        this.curVol -= Math.Round((double)this.fuelCons / 100 * this.distance, 2);
                        this.distance = 0;
                        this.Stop();
                        Console.WriteLine("Вы доехали. Ваш текущий пробег " + this.mileage + " км.");
                    }
                    else
                    {
                        this.mileage += Math.Round((double)this.curVol / this.fuelCons * 100, 2);
                        Console.WriteLine("Вы проехали " + Math.Round((double)this.curVol / this.fuelCons * 100, 2) + " км. Ваш текущий пробег: " + this.mileage + " км. Для дальнейшей поездки всего необходимо " + FuelLack() + " л топлива. Поедете дальше?");
                        string answ = Console.ReadLine();
                        if (answ == "нет"||answ=="ytn"||answ=="net"||answ=="no")
                        {
                            this.curVol = 0;
                            this.Stop();
                            break;
                        }
                        else if (answ == "да"||answ =="lf"||answ=="da"||answ=="yes")
                        {
                            this.distance -= Math.Round((double)this.curVol / this.fuelCons * 100, 2);
                            this.curVol = 0;
                            this.Refill();
                        }
                    }
                }
                this.trajectory.Clear();
            }
        }

        /// <summary>
        /// Ввод данных для разгона и изменение расхода топлива
        /// </summary>
        private void SpeedUpIn()
        {
            Console.WriteLine("Необходимо разогнаться.");
            Console.Write("Разоганться на (введите число больше нуля): ");
            double p = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);
            if (p <= 0)
            {
                this.SpeedUpIn();
            }
            else if (p > this.maxSpeed)
            {
                Console.WriteLine("Невозможно ехать быстрее " + this.maxSpeed + " км/ч.");
                this.SpeedUpIn();
            }
            else
            {
                this.SpeedUp(p);
                Console.WriteLine("Ваша скорость: " + this.curSpeed + " км/ч.");
                if (this.curSpeed >= 0 && this.curSpeed <= 45)
                {
                    this.fuelCons = 12;
                }
                else if (this.curSpeed >= 46 && this.curSpeed <= 100)
                {
                    this.fuelCons = 9;
                }
                else if (this.curSpeed >= 101 && this.curSpeed <= 180)
                {
                    this.fuelCons = 12.5;
                }
            }
        }

        /// <summary>
        /// Разгон
        /// </summary>
        /// <param name="p">Величина разгона (км/ч)</param>
        private void SpeedUp(double p)
        {
            this.curSpeed = p;
        }

        /// <summary>
        /// Остановка
        /// </summary>
        protected void Stop()
        {
            this.curSpeed = 0;
            this.fuelCons = 13;
        }

        /// <summary>
        /// Расчёт недостающего объёма топлива
        /// </summary>
        /// <returns></returns>
        private double FuelLack()
        {
            return Math.Round((((double)this.fuelCons / 100) * this.distance) - this.curVol, 2);
        }

        /// <summary>
        /// Выбор автомобиля для сверки траектории
        /// </summary>
        /// <param name="cars">Список автомобилей</param>
        private void Search(List<Car> cars)
        {
            if (this.trajectory.Count == 0)
            {
                Console.WriteLine("Спланируйте поездку.");
                Way();
                Search(cars);
            }
            else
            {
                int comp = cars.Count(c => c.numbr != this.numbr && c.trajectory.Count != 0);
                if (comp > 0)
                {
                    Console.WriteLine("Выберите автомобиль для сверки траекторий: ");
                    int i = 1;
                    foreach (Car car in cars)
                    {
                        if (car.numbr != this.numbr && car.trajectory.Count != 0)
                        {
                            Console.WriteLine(i + ". " + car.numbr);
                            i++;
                        }
                    }
                    int index = Convert.ToInt32(Console.ReadLine()) - 1;
                    this.Crash(cars[index], cars);
                }
                else
                {
                    Console.WriteLine("Нет автомобилей, доступных для сверки траектории.");
                    this.ChooseAction(cars);
                }
            }
        }

        /// <summary>
        /// Расчёт возможного количества аварий
        /// </summary>
        /// <param name="cars">Список автомобилей</param>
       private void Crash(Car car, List<Car> cars)
        {
            int acc = 0;
            string place = "";
            for (int i = 1; i <= this.trajectory.Count() - 1; i++)
            {
                for (int j = 1; j <= this.trajectory.Count() - 1; j++)
                {
                    if (this.trajectory[i] == car.trajectory[j])
                    {
                        acc++;
                        place = this.trajectory[i];
                    }
                }
            }
            Console.WriteLine("Автомобили: " + this.numbr + ", " + car.numbr);
            if (acc == 0)
            {
                Console.WriteLine("Траектории движения автомобилей не пересекаются.");
            }
            else if (acc == 1)
            {
                Console.WriteLine("Возможный аварийный участок: " + place);
            }
            else
            {
                string start = "";
                string fin = "";
                if (this.trajectory[1] == car.trajectory[1])
                {
                    start = this.trajectory[1];
                }
                else if (Convert.ToDouble(this.trajectory[1].Remove(this.trajectory[1].IndexOf(";"), 1)) > Convert.ToDouble(car.trajectory[1].Remove(car.trajectory[1].IndexOf(";"), 1)))
                {
                    start = this.trajectory[1];
                }
                else
                {
                    start = car.trajectory[1];
                }
                if (Convert.ToDouble(this.trajectory.Last().Remove(this.trajectory.Last().IndexOf(";"), 1)) == Convert.ToDouble(this.trajectory.Last().Remove(this.trajectory.Last().IndexOf(";"), 1)))
                {
                    fin = this.trajectory.Last();
                }
                else if (Convert.ToDouble(this.trajectory.Last().Remove(this.trajectory.Last().IndexOf(";"), 1)) > Convert.ToDouble(this.trajectory.Last().Remove(this.trajectory.Last().IndexOf(";"), 1)))
                {
                    fin = this.trajectory.Last();
                }
                else
                {
                    fin = this.trajectory.Last();
                }
                Console.WriteLine("Возможный аварийный участок: [" + start + " - " + fin + "]");
            }
            Console.WriteLine("Количество возможных аварий: " + acc);
            this.ChooseAction(cars);
        }
    }
}

