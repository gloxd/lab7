using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab7
{
    public class Toy
    {
        private string name;
        private double price;
        private int minAge;
        private int maxAge;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int MinAge
        {
            get { return minAge; }
            set { minAge = value; }
        }

        public int MaxAge
        {
            get { return maxAge; }
            set { maxAge = value; }
        }

        public Toy() { }

        public Toy(string name, double price, int minAge, int maxAge)
        {
            this.name = name;
            this.price = price;
            this.minAge = minAge;
            this.maxAge = maxAge;
        }
    }

    public struct Participant
    {
        private string lastName;
        private string firstName;
        private int grade;
        private int score;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public int Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Participant(string lastName, string firstName, int grade, int score)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.grade = grade;
            this.score = score;
        }
    }

    public static class FileTasks
    {
        // --- Задание 1 ---
        public static void GenerateTask1File(string path, int count)
        {
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < count; i++)
                {
                    sw.WriteLine(rand.Next(0, count * 2));
                }
            }
        }

        public static int RunTask1(string path)
        {
            int sum = 0;
            int index = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int number;
                    if (int.TryParse(line, out number))
                    {
                        if (number == index)
                        {
                            sum += number;
                        }
                    }
                    index++;
                }
            }
            return sum;
        }

        // --- Задание 2 ---
        public static void GenerateTask2File(string path, int rows, int cols)
        {
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < rows; i++)
                {
                    string rowStr = "";
                    for (int j = 0; j < cols; j++)
                    {
                        rowStr += rand.Next(1, 50) + " ";
                    }
                    sw.WriteLine(rowStr.Trim());
                }
            }
        }

        public static long RunTask2(string path, int k)
        {
            long product = 1;
            bool found = false;

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    foreach (string part in parts)
                    {
                        int number;
                        if (int.TryParse(part, out number))
                        {
                            if (number % k == 0)
                            {
                                product *= number;
                                found = true;
                            }
                        }
                    }
                }
            }
            return found ? product : 0;
        }

        // --- Задание 3 ---
        public static void RunTask3(string inputPath, string outputPath)
        {
            string russianAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

            using (StreamReader sr = new StreamReader(inputPath))
            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    bool hasRussian = false;
                    foreach (char c in line)
                    {
                        if (russianAlphabet.IndexOf(c) != -1)
                        {
                            hasRussian = true;
                            break;
                        }
                    }
                    if (!hasRussian)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        // --- Задание 4 ---
        public static void GenerateTask4BinaryFile(string path, int count)
        {
            Random rand = new Random();
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    bw.Write(rand.Next(1, 100));
                }
            }
        }

        public static void RunTask4(string inputPath, string outputPath, int k)
        {
            using (BinaryReader br = new BinaryReader(File.Open(inputPath, FileMode.Open)))
            using (BinaryWriter bw = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    int number = br.ReadInt32();
                    if (number % k == 0)
                    {
                        bw.Write(number);
                    }
                }
            }
        }

        // --- Задание 5 ---
        public static void RunTask5Save(string path, Toy[] toys)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, toys);
            }
        }

        public static double RunTask5LoadAndFind(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            Toy[] toys;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                toys = (Toy[])serializer.Deserialize(fs);
            }

            double maxPrice = -1;
            foreach (Toy toy in toys)
            {
                if (toy.Name.ToLower().IndexOf("конструктор") != -1)
                {
                    if (toy.Price > maxPrice)
                    {
                        maxPrice = toy.Price;
                    }
                }
            }
            return maxPrice;
        }
    }

    public static class CollectionTasks
    {
        // --- Задание 6 ---
        public static bool RunTask6(System.Collections.Generic.List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i] == list[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // --- Задание 7 ---
        public static void RunTask7(System.Collections.Generic.LinkedList<int> list, int valueToRemove)
        {
            System.Collections.Generic.LinkedListNode<int> current = list.First;
            while (current != null)
            {
                if (current.Value == valueToRemove)
                {
                    list.Remove(current);
                    break;
                }
                current = current.Next;
            }
        }

        // --- Задание 8 ---
        public static void RunTask8(System.Collections.Generic.HashSet<string>[] melomanPreferences, string[] allSongs)
        {
            int n = melomanPreferences.Length;

            System.Collections.Generic.HashSet<string> likesAll = new System.Collections.Generic.HashSet<string>();
            System.Collections.Generic.HashSet<string> likesSome = new System.Collections.Generic.HashSet<string>();
            System.Collections.Generic.HashSet<string> likesNone = new System.Collections.Generic.HashSet<string>();

            foreach (string song in allSongs)
            {
                int count = 0;
                foreach (System.Collections.Generic.HashSet<string> pref in melomanPreferences)
                {
                    if (pref.Contains(song))
                    {
                        count++;
                    }
                }

                if (count == n)
                {
                    likesAll.Add(song);
                }
                else if (count > 0)
                {
                    likesSome.Add(song);
                }
                else
                {
                    likesNone.Add(song);
                }
            }

            Console.WriteLine("Нравятся ВСЕМ меломанам:");
            foreach (string s in likesAll) Console.WriteLine("- " + s);

            Console.WriteLine("Нравятся НЕКОТОРЫМ меломанам:");
            foreach (string s in likesSome) Console.WriteLine("- " + s);

            Console.WriteLine("Не нравятся НИКОМУ:");
            foreach (string s in likesNone) Console.WriteLine("- " + s);
        }

        // --- Задание 9 ---
        public static void RunTask9(string textFilePath)
        {
            string content = File.ReadAllText(textFilePath).ToLower();
            string vowels = "аеёиоуыэюя";

            char[] separators = { ' ', ',', '.', '!', '?', '-', '\n', '\r', '\t' };
            string[] words = content.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            System.Collections.Generic.Dictionary<char, System.Collections.Generic.HashSet<string>> vowelWordMap =
                new System.Collections.Generic.Dictionary<char, System.Collections.Generic.HashSet<string>>();

            foreach (char v in vowels)
            {
                vowelWordMap.Add(v, new System.Collections.Generic.HashSet<string>());
            }

            foreach (string word in words)
            {
                foreach (char c in word)
                {
                    if (vowelWordMap.ContainsKey(c))
                    {
                        vowelWordMap[c].Add(word);
                    }
                }
            }

            System.Collections.Generic.List<char> resultVowels = new System.Collections.Generic.List<char>();
            foreach (char v in vowels)
            {
                if (vowelWordMap[v].Count <= 1 && vowelWordMap[v].Count > 0)
                {
                    resultVowels.Add(v);
                }
            }

            for (int i = 0; i < resultVowels.Count; i++)
            {
                for (int j = i + 1; j < resultVowels.Count; j++)
                {
                    if (resultVowels[i] > resultVowels[j])
                    {
                        char temp = resultVowels[i];
                        resultVowels[i] = resultVowels[j];
                        resultVowels[j] = temp;
                    }
                }
            }

            Console.WriteLine("Гласные, входящие не более чем в одно слово:");
            foreach (char v in resultVowels)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();
        }

        // --- Задание 10 ---
        public static void RunTask10(Participant[] participants)
        {
            int n = participants.Length;
            if (n == 0) return;

            System.Collections.Generic.SortedList<int, int> scoreCounts = new System.Collections.Generic.SortedList<int, int>();
            foreach (Participant p in participants)
            {
                if (scoreCounts.ContainsKey(p.Score))
                {
                    scoreCounts[p.Score]++;
                }
                else
                {
                    scoreCounts.Add(p.Score, 1);
                }
            }

            System.Collections.Generic.List<int> uniqueScores = new System.Collections.Generic.List<int>();
            foreach (int score in scoreCounts.Keys)
            {
                uniqueScores.Add(score);
            }

            int maxScore = uniqueScores[uniqueScores.Count - 1];
            int winnersCount = scoreCounts[maxScore];

            bool hasWinners = false;
            double limit = n * 0.20;

            if (maxScore > 200 && winnersCount <= limit)
            {
                hasWinners = true;
            }

            int targetScore = -1;

            if (hasWinners)
            {
                if (uniqueScores.Count > 1)
                {
                    targetScore = uniqueScores[uniqueScores.Count - 2];
                }
            }
            else
            {
                targetScore = maxScore;
            }

            if (targetScore == -1)
            {
                Console.WriteLine("Искомых участников не найдено.");
                return;
            }

            int countOfTarget = scoreCounts[targetScore];

            if (countOfTarget == 1)
            {
                foreach (Participant p in participants)
                {
                    if (p.Score == targetScore)
                    {
                        Console.WriteLine(p.LastName + " " + p.FirstName);
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine(countOfTarget);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Автоматическое создание исходного файла для Задания 10
            string t10FilePath = "participants.txt";
            CreateDefaultOlympiadFile(t10FilePath);

            Console.WriteLine("====== ТЕСТИРОВАНИЕ КЛАССА FileTasks ======");

            // 1. Тест Задания 1
            string t1Path = "task1.txt";
            int t1Count = ReadInt("Задание 1. Введите количество случайных чисел для файла: ", 1, 100);
            FileTasks.GenerateTask1File(t1Path, t1Count);
            int sumT1 = FileTasks.RunTask1(t1Path);
            Console.WriteLine("Сумма элементов, равных своему индексу: " + sumT1);

            // 2. Тест Задания 2
            string t2Path = "task2.txt";
            int t2K = ReadInt("Задание 2. Введите делитель k: ", 1, 100);
            FileTasks.GenerateTask2File(t2Path, 5, 4);
            long productT2 = FileTasks.RunTask2(t2Path, t2K);
            Console.WriteLine("Произведение элементов, кратных " + t2K + ": " + productT2);

            // 3. Тест Задания 3
            string t3Input = "task3_in.txt";
            string t3Output = "task3_out.txt";
            File.WriteAllText(t3Input, "Hello World\nПривет мир\nC# Project\nФайлы и Коллекции");
            FileTasks.RunTask3(t3Input, t3Output);
            Console.WriteLine("Задание 3 выполнено. Строки без русских букв переписаны в " + t3Output);

            // 4. Тест Задания 4
            string t4Input = "task4_in.bin";
            string t4Output = "task4_out.bin";
            int t4K = ReadInt("Задание 4. Введите делитель k для бинарного файла: ", 1, 100);
            FileTasks.GenerateTask4BinaryFile(t4Input, 10);
            FileTasks.RunTask4(t4Input, t4Output, t4K);
            Console.WriteLine("Задание 4 выполнено. Кратные элементы сохранены.");

            // 5. Тест Задания 5
            string t5Path = "toys.xml";
            int toysCount = ReadInt("Задание 5. Сколько игрушек вы хотите ввести с клавиатуры? ", 1, 10);
            Toy[] inputToys = new Toy[toysCount];
            for (int i = 0; i < toysCount; i++)
            {
                Console.WriteLine($"\nВвод данных об игрушке #{i + 1}:");
                Console.Write("Название: ");
                string name = Console.ReadLine();
                double price = ReadDouble("Стоимость (руб): ");
                int minAge = ReadInt("Минимальный возраст: ", 0, 18);
                int maxAge = ReadInt("Максимальный возраст: ", minAge, 99);
                inputToys[i] = new Toy(name, price, minAge, maxAge);
            }
            FileTasks.RunTask5Save(t5Path, inputToys);
            double maxPrice = FileTasks.RunTask5LoadAndFind(t5Path);
            Console.WriteLine("\nСтоимость самого дорогого конструктора из XML: " + maxPrice);


            Console.WriteLine("\n====== ТЕСТИРОВАНИЕ КЛАССА CollectionTasks ======");

            // 6. Интерактивный тест Задания 6 (List)
            Console.WriteLine("\n--- Задание 6 (Работа с List) ---");
            int list6Size = ReadInt("Введите размер списка List: ", 1, 20);
            System.Collections.Generic.List<int> list6 = new System.Collections.Generic.List<int>();
            for (int i = 0; i < list6Size; i++)
            {
                list6.Add(ReadInt($"Введите элемент #{i + 1}: ", -1000, 1000));
            }
            Console.WriteLine("Есть ли в списке хотя бы два одинаковых элемента? " + CollectionTasks.RunTask6(list6));

            // 7. Интерактивный тест Задания 7 (LinkedList)
            Console.WriteLine("\n--- Задание 7 (Работа с LinkedList) ---");
            int list7Size = ReadInt("Введите количество элементов для LinkedList: ", 1, 20);
            System.Collections.Generic.LinkedList<int> list7 = new System.Collections.Generic.LinkedList<int>();
            for (int i = 0; i < list7Size; i++)
            {
                list7.AddLast(ReadInt($"Введите элемент #{i + 1}: ", -1000, 1000));
            }
            int remVal = ReadInt("Введите число для удаления его первого вхождения: ", -1000, 1000);
            CollectionTasks.RunTask7(list7, remVal);
            Console.Write("Результат LinkedList после удаления: ");
            foreach (int val in list7) Console.Write(val + " ");
            Console.WriteLine();

            // 8. Интерактивный тест Задания 8 (HashSet)
            Console.WriteLine("\n--- Задание 8 (Работа с HashSet - Меломаны) ---");
            int melomansCount = ReadInt("Введите количество меломанов: ", 1, 10);

            Console.Write("Введите ВСЕ существующие музыкальные произведения через запятую:\n> ");
            string allSongsRaw = Console.ReadLine();
            string[] allSongs = allSongsRaw.Split(',');
            for (int i = 0; i < allSongs.Length; i++) allSongs[i] = allSongs[i].Trim();

            System.Collections.Generic.HashSet<string>[] prefs = new System.Collections.Generic.HashSet<string>[melomansCount];
            for (int i = 0; i < melomansCount; i++)
            {
                prefs[i] = new System.Collections.Generic.HashSet<string>();
                Console.WriteLine($"Меломан #{i + 1}. Введите названия любимых песен из списка через запятую:");
                Console.Write("> ");
                string favSongsRaw = Console.ReadLine();
                string[] favSongs = favSongsRaw.Split(',');
                foreach (string song in favSongs)
                {
                    string cleaned = song.Trim();
                    if (cleaned != "") prefs[i].Add(cleaned);
                }
            }
            CollectionTasks.RunTask8(prefs, allSongs);

            // 9. Тест Задания 9
            Console.WriteLine("\n--- Задание 9 (Поиск уникальных гласных в тексте) ---");
            string t9Path = "task9.txt";
            File.WriteAllText(t9Path, "арбуз игла улей окно ананас");
            Console.WriteLine("Анализируется созданный текстовый файл со строкой: 'арбуз игла улей окно ананас'");
            CollectionTasks.RunTask9(t9Path);

            // 10. Тест Задания 10 (Чтение из созданного файла)
            Console.WriteLine("\n--- Задание 10 (Результаты олимпиады из файла) ---");
            Console.WriteLine($"Чтение данных олимпиадников из файла: {t10FilePath}...");

            if (File.Exists(t10FilePath))
            {
                string[] lines = File.ReadAllLines(t10FilePath);
                int n = int.Parse(lines[0].Trim());
                Participant[] olympiad = new Participant[n];

                for (int i = 0; i < n; i++)
                {
                    string[] parts = lines[i + 1].Split(' ');
                    string lName = parts[0];
                    string fName = parts[1];
                    int cls = int.Parse(parts[2]);
                    int scr = int.Parse(parts[3]);
                    olympiad[i] = new Participant(lName, fName, cls, scr);
                }

                Console.Write("Результат обработки олимпиады: ");
                CollectionTasks.RunTask10(olympiad);
            }
            else
            {
                Console.WriteLine("Файл с исходными данными олимпиады не найден.");
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения программы...");
            Console.ReadKey();
        }

        static int ReadInt(string message, int min, int max)
        {
            int result;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max)
            {
                Console.Write($"Ошибка! Введите целое число в диапазоне от {min} до {max}: ");
            }
            return result;
        }

        static double ReadDouble(string message)
        {
            double result;
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), out result) || result < 0)
            {
                Console.Write("Ошибка! Введите корректное положительное вещественное число: ");
            }
            return result;
        }

        static void CreateDefaultOlympiadFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("5"); // Количество участников N
                sw.WriteLine("Иванов Иван 11 230");  
                sw.WriteLine("Семенов Егор 11 225");    
                sw.WriteLine("Сидоров Сидор 9 180");
                sw.WriteLine("Яковлев Яков 8 150");
                sw.WriteLine("Николаев Коля 7 120");
            }
        }

    }
}