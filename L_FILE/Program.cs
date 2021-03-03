using System;
using System.IO;



//Тема: Взаимодействие с файловой системой

//Задание 1.
//Написать программу «Записная книжка» для возможности записи/чтения файла.

//Требования к программе:

//При написании программы предоставить пользователю два интерфейса
//для реализации в своем классе «Записная книжка» методов чтения/записи,
//для взаимодействия с этими методами в классе реализовать метод «меню»




public class Notebook
{
    //создаем папку под названием блокнот
    public void Create_directory()
    {
        string name_directory = "Notebook";
        DirectoryInfo directory = new DirectoryInfo(name_directory);

        directory.Create();

    }

    //запись в файл с созданрием файла
    private void Write_in_recording(string path) 
    {
        Console.Write("Введите текст\n >>");
        string text = Console.ReadLine();
        StreamWriter streamWriter = File.CreateText(path);

            using (streamWriter)
            {
                streamWriter.WriteLine(text);
                Console.WriteLine("Запись создана");
            }
        
    }


    //создаем файлы которые будут хранить записи по датам
    public void Creat_recording()
    {
        string date =" ";
        string path= @"Notebook\";
        Console.Write("Введите дату записи : ");
        date = Console.ReadLine();

        FileInfo fileInfo = new FileInfo(path+date+".txt");
        bool file=fileInfo.Exists;//проверяем существует ли запись с такой датой

        
        //отлавливаем существование файла если нет, то создаем файл
        try
        {
            if (file)
            {

                throw new Exception("Запись с такой датой уже существует");
            }
            else
            {

               string  path_to_the_file =path+date + ".txt";
                this.Write_in_recording(path_to_the_file);
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("Error "+ex.Message);
        }

        

    }



    //вывести все txt файлы
  public void Show_all_recordings()
    {
        string path = @"Notebook";


        try
        {
            //делаем проверку на существование папки
            if (Directory.Exists(path))
            {
                //заносим названия всех файлов в массив строк
                string[] all_recordings = Directory.GetFiles(path, "*.txt");//добавляем патерн txt для поиска только текстовых файлов

                //если папка пуста сообщаем об этом пользователю
                if (all_recordings.Length == 0)
                {
                    Console.WriteLine("В папке отсутсвуют файлы");
                }
                else
                {
                    foreach (string item in all_recordings)
                    {
                        Console.WriteLine(item);
                        using (StreamReader stream = File.OpenText(item))
                        {

                            Console.WriteLine(stream.ReadToEnd());
                        }
                    }
                }

            }
            else
            {

               throw new Exception ("Ошибка папка записная книжка не обнаружена");
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
       
    }

    public void Menu(Notebook notebook)
    {

        int menu_notebook = 0;

        do
        {

            Console.WriteLine("\t\tЗаписная книжка");
            Console.WriteLine("Сделайте выбор");
            Console.WriteLine("1 - Просмотреть все записи в записной книжке");
            Console.WriteLine("2 - Создать запись в записной книжке");
            // Console.WriteLine("3 - Редактировать запись в записной книжке");
            Console.WriteLine("0-EXIT");
            Console.Write(">>");

            menu_notebook = int.Parse(Console.ReadLine());


            switch (menu_notebook)
            {
                //Просмотр всех записей
                case 1:
                    {
                        Console.WriteLine("\t\tВывод дат с существующими записями");
                        notebook.Show_all_recordings();
                        Console.WriteLine();


                    }
                    break;
                //создание записи в записной книжке
                case 2:
                    {
                        notebook.Creat_recording();

                    }
                    break;
                //пока не активно сделано для дз и экзамена  
                //case 3:
                //    {

                //    }
                //    break;
                default:
                    if (menu_notebook > 2 || menu_notebook < 0)
                    {
                        Console.WriteLine("Ошибка ввода выбора меню");
                    }
                    break;
            }

        } while (menu_notebook != 0);
    }

}

//фитча для концовки программы

namespace L_FILE
{
    class Program
    {
        static void Main(string[] args)
        {
            Notebook notebook = new Notebook();
            notebook.Create_directory();

            notebook.Menu(notebook);

            Console.WriteLine("\t\tTHE END");
           
        }
    }
}
