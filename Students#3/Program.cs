using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

class BD
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            string selectedTable = "";
            bool returnToTableSelection = false;
            string conectBD = "Server=DESKTOP-5BD88QO\\SQLEXPRESS;Database=Students;Trusted_Connection=True;TrustServerCertificate=true";

            if (!returnToTableSelection)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Выберите таблицу для дальнейших действий");
                Console.ResetColor();
                Console.WriteLine("1. Student");
                Console.WriteLine("2. Subjects\n");
                int chosenTable = 0;
                while (true)
                {
                    // Пытаемся преобразовать ввод пользователя в число
                    if (!int.TryParse(Console.ReadLine(), out chosenTable))
                    {
                        // Если ввод не является числом, выводим сообщение об ошибке и повторяем запрос
                        Console.WriteLine("Ошибка: Выберите номер таблицы.");
                    }
                    else if (chosenTable != 1 && chosenTable != 2)
                    {
                        // Если введено число отличное от 1 и 2, выводим сообщение об ошибке и повторяем запрос
                        Console.WriteLine("Ошибка: Пожалуйста, введите 1 или 2.");
                    }
                    else
                    {
                        // Ввод корректен, выходим из цикла
                        selectedTable = (chosenTable == 1) ? "Student" : "Subjects";
                        break;
                    }
                }

                Console.Clear();
                Console.WriteLine("Обработка вашего выбора...");
                await Task.Delay(2000); // Имитация обработки выбора в течение 2 секунд


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вы выбрали таблицу: " + selectedTable);
                Console.ResetColor();

                if (returnToTableSelection)
                {
                    returnToTableSelection = false;
                    continue; // Переход в начало цикла для выбора таблицы
                }

                switch (chosenTable)
                {
                    //Действия с таблицей студента
                    case 1:
                        using (SqlConnection conect = new SqlConnection(conectBD))
                        {
                            conect.Open();
                            string sql = $"SELECT * FROM Student;";
                            using (SqlCommand command = new SqlCommand(sql, conect))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("\nStudent\n");
                                Console.ResetColor(); using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"{reader["student_id"]} {reader["student_name"]}  {reader["student_age"]} {reader["student_major"]} ");
                                    }
                                }
                            }
                            if (!returnToTableSelection)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\nВыберите действие");
                                Console.ResetColor();
                                Console.WriteLine("1. Добавить студента");
                                Console.WriteLine("2. Обновить данные студента");
                                Console.WriteLine("3. Удалить данные");
                                Console.WriteLine("4. Средняя оценка студентов");
                                Console.WriteLine("5. Отличники");
                                Console.WriteLine("6. Студенты изучающие предмет");
                                Console.WriteLine("7. В обратном порядке");
                                Console.WriteLine("8. Количество изучающих предмет");
                                Console.WriteLine("9. Выбор таблицы\n");


                                int action;
                                while (true)
                                {
                                    // Пытаемся преобразовать ввод пользователя в число
                                    if (!int.TryParse(Console.ReadLine(), out action))
                                    {
                                        // Если ввод не является числом, выводим сообщение об ошибке и повторяем запрос
                                        Console.WriteLine("Ошибка: Введите номер действия.");
                                    }
                                    else if (action < 1 || action > 9)
                                    {
                                        // Если введено число, не находящееся в диапазоне от 1 до 9, выводим сообщение об ошибке и повторяем запрос
                                        Console.WriteLine("Ошибка: Выберите действие от 1 до 9.");
                                    }
                                    else
                                    {
                                        // Ввод корректен, выходим из цикла
                                        break;
                                    }
                                }
                                // Вывод сообщения о выбранном действии
                                switch (action)
                                {
                                    case 1:
                                        try
                                        {
                                            using (SqlCommand createUser = new SqlCommand(sql, conect))
                                            {
                                                int num = await createUser.ExecuteNonQueryAsync();
                                              
                                                Console.WriteLine("\nВведите Фамилию Имя"); 
                                                string lastname = Console.ReadLine(); 
                                                Console.WriteLine("\nВведите возраст:");
                                                 string secondname = Console.ReadLine(); 
                                                Console.WriteLine("\nВведите предмет:");
                                                string book = Console.ReadLine(); 

                                                sql = $"INSERT INTO Students (student_name,student_age,student_major) VALUES ('{lastname}','{secondname}','{book}')"; createUser.CommandText = sql;
                                                num = await createUser.ExecuteNonQueryAsync(); Console.WriteLine($"\nДобавлено объектов: {num}");
                                            }
                                        }
                                        catch
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Error");
                                            Console.ResetColor();
                                        }
                                        break;
                                    //Обновление данных с возможностью выбрать,что обновить
                                    case 2:
                                        try
                                        {
                                            using (SqlCommand updateUser = new SqlCommand(sql, conect))
                                            {
                                                int up = await updateUser.ExecuteNonQueryAsync(); Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("\nВыберите действие"); Console.ResetColor();
                                                Console.WriteLine("1. Изменить Фамилию Имя");
                                                Console.WriteLine("2. Изменить Возраст");
                                                Console.WriteLine("3. Изменить Предмет\n");
                                                //Выбор что обновить
                                                int obnova = Convert.ToInt16(Console.ReadLine());
                                                switch (obnova)
                                                {
                                                    case 1:
                                                        Console.WriteLine("Введите ID пользователя для изменения");
                                                        string userID = Console.ReadLine(); Console.WriteLine("\nВведите новые Фамилию Имя:");
                                                        string firstname = Console.ReadLine(); sql = $"UPDATE Student set student_name ='{firstname}' WHERE ID={userID}";
                                                        break;
                                                    case 2:
                                                        Console.WriteLine("Введите ID пользователя для изменения");
                                                        string usID = Console.ReadLine(); Console.WriteLine("\nВведите новый Возраст:");
                                                        string last = Console.ReadLine(); sql = $"UPDATE People set student_age='{last}' WHERE ID={usID}";
                                                        break;
                                                    case 4:
                                                        Console.WriteLine("Введите ID пользователя для изменения");
                                                        string uID = Console.ReadLine(); Console.WriteLine("\nВведите новую Книгу:");
                                                        string lastу = Console.ReadLine(); sql = $"UPDATE Users set student_major='{lastу}' WHERE ID={uID}";
                                                        break;
                                                }
                                                updateUser.CommandText = sql; up = await updateUser.ExecuteNonQueryAsync();
                                            }
                                        }
                                        catch
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Error");
                                            Console.ResetColor();
                                        }
                                        break;
                                    //Удаление пользователя
                                    case 3:
                                        try
                                        {
                                            using (SqlCommand deleteUser = new SqlCommand(sql, conect))
                                            {
                                                int del = await deleteUser.ExecuteNonQueryAsync(); Console.WriteLine("Введите ID пользователя для удаления");
                                                string delID = Console.ReadLine(); 
                                                sql = $"use Students delete from Student where student_id={delID}";
                                                deleteUser.CommandText = sql; del = await deleteUser.ExecuteNonQueryAsync();
                                            }
                                        }
                                        catch
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Error");
                                            Console.ResetColor();
                                        }
                                        break;

                                    case 4:
                                        try
                                        {
                                            sql = "SELECT AVG(CAST(student_estimation AS decimal)) AS AverageEstimation FROM Student";

                                            using (SqlCommand displayAverageEstimation = new SqlCommand(sql, conect))
                                            {
                                                object result = await displayAverageEstimation.ExecuteScalarAsync();
                                                if (result != DBNull.Value)
                                                {
                                                    Console.WriteLine($"Средняя оценка: {result}");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Нет доступных данных");
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Error: {ex.Message}");
                                            Console.ResetColor();
                                        }
                                        break;

                                    case 5:
                                        try
                                        {
                                            sql = "SELECT student_name, student_age, student_major FROM Student WHERE student_estimation = '5'";

                                            using (SqlCommand displayStudents = new SqlCommand(sql, conect))
                                            {
                                                using SqlDataReader reader = await displayStudents.ExecuteReaderAsync();
                                                while (await reader.ReadAsync())
                                                {
                                                    Console.WriteLine($"{reader["student_name"]} {reader["student_age"]} {reader["student_major"]}");
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Error: {ex.Message}");
                                            Console.ResetColor();
                                        }
                                        break;

                                    case 6:
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Введите предмет для отображения студентов:\n");
                                            Console.ResetColor();
                                            string subject = Console.ReadLine();

                                            sql = $"SELECT student_name, student_age, student_major, student_estimation FROM Student WHERE student_major = @Subject";

                                            using (SqlCommand displayStudentsBySubject = new SqlCommand(sql, conect))
                                            {
                                                displayStudentsBySubject.Parameters.AddWithValue("@Subject", subject);

                                                using (SqlDataReader reader = await displayStudentsBySubject.ExecuteReaderAsync())
                                                {
                                                    while (await reader.ReadAsync())
                                                    {
                                                      Console.WriteLine($"{reader["student_name"]} {reader["student_age"]} {reader["student_major"]} {reader["student_estimation"]}");
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Error: {ex.Message}");
                                            Console.ResetColor();
                                        }

                                        break;

                                    case 7:
                                        try
                                        {
                                            Console.WriteLine("Выберите способ сортировки:");
                                            Console.WriteLine("1. Вывести в обратном алфавитном порядке");
                                            Console.WriteLine("2. Вывести в алфавитном порядке");
                                            Console.WriteLine("3. Вывести от последнего ID к первому");

                                            int sortOption = Convert.ToInt32(Console.ReadLine());

                                            string orderBy = "";

                                            switch (sortOption)
                                            {
                                                case 1:
                                                    orderBy = "student_name DESC";
                                                    break;
                                                case 2:
                                                    orderBy = "student_name ASC";
                                                    break;
                                                case 3:
                                                    orderBy = "student_id DESC";
                                                    break;
                                                default:
                                                    Console.WriteLine("Неверный вариант сортировки.");
                                                    break;
                                            }

                                            if (!string.IsNullOrEmpty(orderBy))
                                            {
                                                sql = $"SELECT student_name, student_age, student_major FROM Student ORDER BY {orderBy}";

                                                using (SqlCommand displayStudentsOrdered = new SqlCommand(sql, conect))
                                                {
                                                    using (SqlDataReader reader = await displayStudentsOrdered.ExecuteReaderAsync())
                                                    {
                                                        while (await reader.ReadAsync())
                                                        {
                                                            Console.WriteLine($"{reader["student_name"]} {reader["student_age"]} {reader["student_major"]} {reader["student_estimation"]}");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Error: {ex.Message}");
                                            Console.ResetColor();
                                        }

                                        break;

                                    case 8:
                                        try
                                        {
                                            Console.WriteLine("Введите предмет для отображения студентов:\n");
                                            string subject = Console.ReadLine();

                                            int studentCounter = 0;  // Переменная для подсчета выведенных студентов

                                            sql = $"SELECT student_name, student_age, student_major, student_estimation FROM Student WHERE student_major = @Subject";

                                            using (SqlCommand displayStudentsBySubject = new SqlCommand(sql, conect))
                                            {
                                                displayStudentsBySubject.Parameters.AddWithValue("@Subject", subject);

                                                using (SqlDataReader reader = await displayStudentsBySubject.ExecuteReaderAsync())
                                                {
                                                    while (await reader.ReadAsync())
                                                    {
                                                        // Увеличиваем счетчик студентов при выводе каждого студента
                                                        studentCounter++;
                                                        Console.WriteLine($"{reader["student_name"]} {reader["student_age"]} {reader["student_major"]} {reader["student_estimation"]}\n");
                                                    }
                                                }

                                                // После завершения цикла выводим общее количество выведенных студентов
                                                Console.WriteLine($"Всего выведено студентов: {studentCounter}");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Error: {ex.Message}");
                                            Console.ResetColor();
                                        }

                                        break;

                                    case 9:
                                        try
                                        {
                                            returnToTableSelection = true;
                                        }
                                        catch
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Error");
                                            Console.ResetColor();
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Выбрано неверное действие.");
                                    break;
                                }
                            }
                      break;
                    }





                    //Действия с таблицей предметов       
                    case 2:
                        using (SqlConnection conect = new SqlConnection(conectBD))
                        {
                            conect.Open();
                            string sql = $"SELECT * FROM Subjects;";



                            using (SqlCommand command = new SqlCommand(sql, conect))
                            {
                              
                                using (SqlDataReader read = command.ExecuteReader())
                                {
                                    while (read.Read())
                                    {
                                        Console.WriteLine($"{read["subject_id"]} {read["subject_name"]} {read["subject_description"]}");
                                    }

                                }
                            }
                            if (!returnToTableSelection)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow; 
                                Console.WriteLine("\nВыберите действие\n");
                                Console.ResetColor();
                                Console.WriteLine("1. Добавить предмет");
                                Console.WriteLine("2. Обновить данные предмета");
                                Console.WriteLine("3. Удалить");
                                Console.WriteLine("4. В обратном порядке");
                                Console.WriteLine("5. Выбор таблицы\n");
                                int M;
                                while (true)
                                {
                                    // Пытаемся преобразовать ввод пользователя в число
                                    if (!int.TryParse(Console.ReadLine(), out M))
                                    {
                                        // Если ввод не является числом, выводим сообщение об ошибке и повторяем запрос
                                        Console.WriteLine("Ошибка: Введите номер действия.");
                                    }
                                    else if (M < 1 || M > 4)
                                    {
                                        // Если введено число, не находящееся в диапазоне от 1 до 4, выводим сообщение об ошибке и повторяем запрос
                                        Console.WriteLine("Ошибка: Выберите действие от 1 до 4.");
                                    }
                                    else
                                    {
                                        // Ввод корректен, выходим из цикла
                                        break;
                                    }
                                }
                                switch (M)
                                {
                                    case 1:
                                        Console.WriteLine("Новый предмет...");
                                        using (SqlCommand createOrder = new SqlCommand(sql, conect))
                                        {
                                            int crt = await createOrder.ExecuteNonQueryAsync();
                                            Console.WriteLine("Введите ID"); string crtID = Console.ReadLine();
                                            Console.WriteLine("Введите Название"); string dateOrder = Console.ReadLine();
                                            Console.WriteLine("Введите Полное наименование"); string crtID1 = Console.ReadLine();
                          

                                            sql = $"insert into Subjects (subject_id,subject_name,subject_description) values ('{crtID}','{dateOrder}','{crtID1}')"; createOrder.CommandText = sql;
                                            crt = await createOrder.ExecuteNonQueryAsync();
                                            Console.WriteLine("Обновлено");
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("Редактирование предмета...");
                                        using (SqlCommand updateOrder = new SqlCommand(sql, conect))
                                        {
                                            int upOrd = await updateOrder.ExecuteNonQueryAsync();
                                            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("\nВыберите действие");
                                            Console.ResetColor();
                                            Console.WriteLine("1. Изменить название");
                                            Console.WriteLine("2. Изменить полное наименование\n");
                                            int O = Convert.ToInt16(Console.ReadLine()); switch (O)
                                            {
                                                case 1:
                                                    Console.WriteLine("Введите ID для редактирования"); 
                                                    string OID = Console.ReadLine();
                                                    Console.WriteLine("Введите название для изменения"); 
                                                    string userID = Console.ReadLine();
                                                    sql = $"update Subjects set subject_name='{userID}' where ID='{OID}'"; break;
                                                case 2:
                                                    Console.WriteLine("Введите ID для редактирования");
                                                    string OrID = Console.ReadLine(); 
                                                    Console.WriteLine("Введите полное наименование");
                                                    string dateOr = Console.ReadLine(); 
                                                    sql = $"update Subjects set subject_description='{dateOr}' where ID='{OrID}'";
                                                    break;
                                            }
                                            updateOrder.CommandText = sql; upOrd = await updateOrder.ExecuteNonQueryAsync();
                                            Console.WriteLine("Обновлено");
                                        }
                                        break;

                                    case 3:
                                        try
                                        {
                                            Console.WriteLine("Удаление книги...");
                                            using (SqlCommand deleteUser = new SqlCommand(sql, conect))
                                            {
                                                int del = await deleteUser.ExecuteNonQueryAsync(); Console.WriteLine("Введите ID для удаления");
                                                string delID = Console.ReadLine(); sql = $"use Students delete from Subjects where subject_id={delID}";
                                                deleteUser.CommandText = sql; del = await deleteUser.ExecuteNonQueryAsync();
                                                Console.WriteLine("Обновлено");
                                            }
                                        }
                                        catch
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Error");
                                            Console.ResetColor();
                                        }
                                        break;

                                    case 4:
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Выберите способ сортировки:\n");
                                            Console.ResetColor();
                                            Console.WriteLine("1. Вывести в обратном алфавитном порядке");
                                            Console.WriteLine("2. Вывести в алфавитном порядке");
                                            Console.WriteLine("3. Вывести от последнего ID к первому\n");

                                            int sortOption = Convert.ToInt32(Console.ReadLine());

                                            string orderBy = "";

                                            switch (sortOption)
                                            {
                                                case 1:
                                                    orderBy = "subject_name DESC";
                                                    break;
                                                case 2:
                                                    orderBy = "subject_name ASC";
                                                    break;
                                                case 3:
                                                    orderBy = "subject_id DESC";
                                                    break;
                                                default:
                                                    Console.WriteLine("Неверный вариант сортировки.");
                                                    break;
                                            }

                                            if (!string.IsNullOrEmpty(orderBy))
                                            {
                                                sql = $"SELECT subject_name, subject_description FROM Subjects ORDER BY {orderBy}";

                                                using (SqlCommand displayStudentsOrdered = new SqlCommand(sql, conect))
                                                {
                                                    using (SqlDataReader reader = await displayStudentsOrdered.ExecuteReaderAsync())
                                                    {
                                                        while (await reader.ReadAsync())
                                                        {
                                                            Console.WriteLine($"{reader["subject_name"]} {reader["subject_description\n"]}");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Error: {ex.Message}");
                                            Console.ResetColor();
                                        }

                                        break;

                                    case 5:
                                        try
                                        {
                                            Console.WriteLine("Выбор таблицы");
                                            returnToTableSelection = true;
                                        }
                                        catch
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Error");
                                            Console.ResetColor();
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Выбрано неверное действие.");
                                  break;
                                }
                            }
                        }
                   break;
                
                }
            }
        }
    }
}
