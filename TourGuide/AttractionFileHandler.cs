using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourGuide
{
    public class AttractionFileHandler
    {
        private static string FilePath = "attractions.txt";

        /// Загружает данные о достопримечательностях из файла.
        public static List<Attraction> LoadData()
        {
            List<Attraction> attractions = new List<Attraction>();

            try
            {
                // Проверка существования файла перед чтением
                if (File.Exists(FilePath))
                {
                    // Чтение строк из файла
                    string[] lines = File.ReadAllLines(FilePath);

                    foreach (string line in lines)
                    {
                        // Разделение строки на части
                        string[] parts = line.Split('|');

                        // Проверка правильности формата строки
                        if (parts.Length == 5)
                        {
                            // Создание объекта Attraction и добавление в список
                            Attraction attraction = new Attraction
                            {
                                Name = parts[0],
                                Description = parts[1],
                                Location = parts[2],
                                Rating = double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double rating) ? rating : 0.0,
                                OpeningHours = parts[4]
                            };

                            attractions.Add(attraction);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Файл с данными не существует. Создан новый файл.");
                }
            }
            catch (Exception ex)
            {
                // Вывод ошибки в консоль и логирование
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                Trace.TraceError($"Error loading data: {ex}");
            }

            return attractions;
        }

           /// Сохраняет данные о достопримечательностях в файл.
        public static void SaveData(List<Attraction> attractions)
        {
            try
            {
                // Запись данных в файл
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    foreach (Attraction attraction in attractions)
                    {
                        // Запись каждой достопримечательности в новую строку
                        writer.WriteLine($"{attraction.Name}|{attraction.Description}|{attraction.Location}|{attraction.Rating.ToString(CultureInfo.InvariantCulture)}|{attraction.OpeningHours}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Вывод ошибки в консоль и логирование
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
                Trace.TraceError($"Error saving data: {ex}");
            }
        }       
    }

}
