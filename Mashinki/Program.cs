using Mashinki;

internal class Program
{
    static void Main()
    {
        List<Car> cars = new List<Car>(); //список автомобилей
        Car car = new Car(); //объект для взаимодействия
        car.Starting(cars);
    }
}