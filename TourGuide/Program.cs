using System;
using System.Collections.Generic;
using TourGuide;

class Program
{
    private static List<Attraction> attractions;

    static void Main()
    {
        Console.WriteLine("Программа Тургид");

        // Загрузка данных о достопримечательностях при запуске
        attractions = AttractionFileHandler.LoadData();

        while (true)
        {
            Console.WriteLine("Выберите опцию:");
            Console.WriteLine("1. Добавить новую достопримечательность");
            Console.WriteLine("2. Просмотреть все достопримечательности");
            Console.WriteLine("3. Фильтр по рейтингу");
            Console.WriteLine("4. Поиск по названию");
            Console.WriteLine("5. Редактировать или удалить достопримечательность");
            Console.WriteLine("6. Сохранить данные");
            Console.WriteLine("7. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Добавление новой достопримечательности
                    AddAttraction();
                    break;
                case "2":
                    // Просмотр всех достопримечательностей
                    DisplayAttractions();
                    break;
                case "3":
                    // Просмотр конкретной достопримечательности
                    FilterByRating();
                    break;
                case "4":
                    // Фильтрация по рейтингу
                    SearchByName();
                    break;
                case "5":
                    // Поиск по названию
                    EditOrDeleteAttraction();
                    break;
                case "6":
                     // Редактирование информации
                    AttractionFileHandler.SaveData(attractions);
                    break;
                case "7":
                    AttractionFileHandler.SaveData(attractions);
                    Console.WriteLine("Данные сохранены успешно!");
                    Console.WriteLine("Нажмите Enter, чтобы закрыть программу.");
                    Console.ReadLine(); // Добавленная строка для ожидания ввода пользователя
                    Environment.Exit(0);
                    break;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    private static void AddAttraction()
    {
        Attraction newAttraction = new Attraction();

        Console.Write("Введите название достопримечательности: ");
        newAttraction.Name = Console.ReadLine();

        Console.Write("Введите описание: ");
        newAttraction.Description = Console.ReadLine();

        Console.Write("Введите местоположение: ");
        newAttraction.Location = Console.ReadLine();

        Console.Write("Введите рейтинг: ");
        if (double.TryParse(Console.ReadLine(), out double rating))
        {
            newAttraction.Rating = rating;
        }
        else
        {
            Console.WriteLine("Некорректный формат рейтинга. Рейтинг установлен в 0.");
            newAttraction.Rating = 0;
        }

        Console.Write("Введите часы работы: ");
        newAttraction.OpeningHours = Console.ReadLine();

        attractions.Add(newAttraction);
        Console.WriteLine("Достопримечательность добавлена успешно!");
    }

    private static void DisplayAttractions()
    {
        if (attractions.Count > 0)
        {
            Console.WriteLine("Список достопримечательностей:");
            foreach (var attraction in attractions)
            {
                Console.WriteLine($"Название: {attraction.Name}, Рейтинг: {attraction.Rating}");
            }
        }
        else
        {
            Console.WriteLine("Список достопримечательностей пуст.");
        }
    }

    private static void FilterByRating()
    {
        Console.Write("Введите минимальный рейтинг: ");
        if (double.TryParse(Console.ReadLine(), out double minRating))
        {
            var filteredAttractions = attractions.FindAll(a => a.Rating >= minRating);
            DisplayAttractions(filteredAttractions);
        }
        else
        {
            Console.WriteLine("Некорректный формат рейтинга.");
        }
    }

    private static void SearchByName()
    {
        Console.Write("Введите часть названия для поиска: ");
        string searchQuery = Console.ReadLine().ToLower();

        var searchResults = attractions.FindAll(a => a.Name.ToLower().Contains(searchQuery));

        DisplayAttractions(searchResults);
    }

    private static void EditOrDeleteAttraction()
    {
        Console.Write("Введите название достопримечательности для редактирования или удаления: ");
        string attractionName = Console.ReadLine();

        var attraction = attractions.Find(a => a.Name.Equals(attractionName, StringComparison.OrdinalIgnoreCase));

        if (attraction != null)
        {
            Console.WriteLine("Выберите опцию:");
            Console.WriteLine("1. Редактировать");
            Console.WriteLine("2. Удалить");
            Console.WriteLine("3. Отмена");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    EditAttraction(attraction);
                    break;
                case "2":
                    attractions.Remove(attraction);
                    Console.WriteLine("Достопримечательность удалена успешно!");
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Достопримечательность не найдена.");
        }
    }

    private static void EditAttraction(Attraction attraction)
    {
        Console.Write("Введите новое название достопримечательности: ");
        attraction.Name = Console.ReadLine();

        Console.Write("Введите новое описание: ");
        attraction.Description = Console.ReadLine();

        Console.Write("Введите новое местоположение: ");
        attraction.Location = Console.ReadLine();

        double newRating;
        Console.Write("Введите новый рейтинг: ");
        if (double.TryParse(Console.ReadLine(), out newRating))
        {
            attraction.Rating = newRating;
        }
        else
        {
            Console.WriteLine("Некорректный формат рейтинга. Рейтинг остается без изменений.");
        }

        Console.Write("Введите новые часы работы: ");
        attraction.OpeningHours = Console.ReadLine();

        Console.WriteLine("Достопримечательность отредактирована успешно!");
    }

    private static void DisplayAttractions(List<Attraction> attractionsToDisplay)
    {
        if (attractionsToDisplay.Count > 0)
        {
            Console.WriteLine("Список достопримечательностей:");
            foreach (var attraction in attractionsToDisplay)
            {
                Console.WriteLine($"Название: {attraction.Name}, Рейтинг: {attraction.Rating}");
            }
        }
        else
        {
            Console.WriteLine("Нет результатов для отображения.");
        }
    }
    

}
