using System;
using System.Collections.Generic;//работа с коллекциями 
using System.Windows.Forms;
using Xceed.Document.NET;//для работы с Word
using Xceed.Words.NET;
using System.IO;
using System.Linq;

namespace Генератор_задач
{
    public partial class Form1 : Form
    {
        public List<string> paths = new List<string>();
        string filename1 = "";
        bool[] tasksList = new bool[18];
        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\path_list.txt"))//определяем существует ли файл
            {
                using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\path_list.txt"))
                    while (!reader.EndOfStream) paths.Add(reader.ReadLine());
                foreach (string str in paths) filepath.Items.Add(str);
            }
        }

        void put(DocX tasks, DocX answers, string task, string answer)
        {
            Paragraph p = tasks.InsertParagraph(task);
            p.SpacingAfter(10);//устанавливаем интервал после указанных абзацев. 
            p = answers.InsertParagraph(task);
            p.SpacingAfter(5);
            p = answers.InsertParagraph("Ответ: " + answer + ".");
            p.SpacingAfter(10);
        }

        void generate(DocX tasks, DocX answers)//ГЕНЕРАЦИЯ ВОПРОСОВ
        {


            long combinations(int k, int n)//Сочетание 
            {
                long result = 1;
                if (k > n - k)
                {
                    for (int i = n; i > k; i--) result *= i;
                    for (int i = 1; i <= n - k; i++) result /= i;
                }
                else
                {
                    for (int i = n; i > n - k; i--) result *= i;
                    for (int i = 1; i <= k; i++) result /= i;
                }
                return result;
            }
            //позволяет указать блок кода, который можно развернуть или свернуть при использовании
            #region first task
            {
                if (tasksList[0] == true)
                {
                    int total = rand.Next(15, 30) * 10;
                    int defective = rand.Next(2, 5) * 10;
                    int taken = rand.Next(2, 10);

                    string task =
                        $"1. На завод привезли партию из {total} подшипников, в которую случайно попало {defective} бракованных. " +
                        $"Определить вероятность того, что из {taken}-х взятых наугад подшипников оба окажутся годными.";

                    double solution = Math.Round(Convert.ToDouble(combinations(taken, total - defective)) / combinations(taken, total), 3);


                    put(tasks, answers, task, solution.ToString());
                }
            }
            #endregion

            #region second task
            {
                if (tasksList[1] == true)
                {
                    int total_white = rand.Next(3, 6) * 5;
                    int total_black = rand.Next(1, 4) * 5;
                    int taken = rand.Next(5, 10);
                    int taken_white = rand.Next(2, 4);

                    string task =
                        $"2. В урне {total_white} белых и {total_black} черных шаров. Наудачу отобраны {taken} шаров. Найти вероятность того, что среди них окажется ровно {taken_white} белых шара.";

                    double solution = Math.Round((double)combinations(taken_white, total_white) * combinations(taken - taken_white, total_black) / combinations(taken, total_black + total_white), 3);

                    put(tasks, answers, task, solution.ToString());
                }
            }
            #endregion

            #region third task
            {
                int taken = rand.Next(5, 10);

                string task =
                    $"3. В колоде 32 карты. Наугад вынимают {taken} карт. Найти вероятность того, что среди них окажется хотя бы одна дама.";

                double solution = 1 - Math.Round((double)combinations(taken, 28) / combinations(taken, 32), 3);
                if (tasksList[2] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region fourth task
            {
                int taken = rand.Next(2, 4);

                string task =
                    $"4. Из колоды в 52 карты вынимают одновременно {taken} карты. Событие А - среди вынутых карт хотя бы одна бубновая, В - хотя бы одна червонная. Найти Р(А+В).";

                double solution = Math.Round(1 - combinations(taken, 26) / (double)combinations(taken, 52), 3);
                if (tasksList[3] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region fifth task
            {
                double p1 = rand.Next(1, 10) * 0.01;
                double p2 = rand.Next(1, 10) * 0.01;
                double p3 = rand.Next(1, 10) * 0.01;
                double p4 = rand.Next(1, 10) * 0.01;

                string task =
                    $"5. Узел автомашины состоит из 4 деталей. Вероятности выхода из строя этих деталей соответственно равны: р1 = {p1}; р2 = {p2}; р3 = {p3}; p4 = {p4}. " +
                    $"Узел выходит из строя, если выходит из строя хотя бы одна деталь. Найти вероятность того, что узел не выйдет из строя, если детали выходят из строя независимо друг от друга.";

                double solution = 1 - Math.Round((1 - p1) * (1 - p2) * (1 - p3) * (1 - p4), 3);
                if (tasksList[4] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region sixth task
            {
                double p1 = rand.Next(4, 7) * 0.1;
                double p2 = rand.Next(3, 5) * 0.1;
                double p3 = rand.Next(5, 8) * 0.01;

                string task =
                    $"6. Для разрушения моста достаточно попадания одной авиационной бомбы. " +
                    $"Найти вероятность того, что мост будет разрушен, если на него сбросить 3 бомбы, вероятности попадания которых соответственно равны: р1 = {p1}, р2 = {p2}, р3 = {p3}";

                double solution = Math.Round(1 - (1 - p1) * (1 - p2) * (1 - p3), 3);
                if (tasksList[5] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region seventh task
            {
                int total = rand.Next(2, 5);

                string task =
                    $"7. Опыт заключается в последовательном бросании {total} монет. Событие А - выпадение хотя бы одного орла, событие B - выпадения хотя бы одной решки. Определить Р(А/В).";

                double solution = 0;
                for (int i = 1; i < total; i++) solution += combinations(i, total);
                solution = Math.Round(Math.Pow(1.0 / 2, total) * solution / (Math.Pow(1.0 / 2, total) * solution + Math.Pow(1.0 / 2, total)), 3);
                if (tasksList[6] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region eigth task
            {
                int box1 = rand.Next(2, 3) * 5;
                int box2 = rand.Next(3, 5) * 5;
                int white1 = rand.Next(6, 9);
                int white2 = rand.Next(3, 7);

                string task =
                    $"8. В первой урне содержится {box1} шаров, из них {white1} белых, во второй {box2} шаров, из них {white2} белых. " +
                    $"Из каждой урны наудачу извлекли по одному шару, а затем из этих двух шаров наудачу взят один шар. Найти вероятность того, что взят белый шар.";

                double solution = Math.Round(0.5 * (Convert.ToDouble(white1) / box1 + Convert.ToDouble(white1) / box2), 3);
                if (tasksList[7] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region ninth task
            {
                double p1 = rand.Next(4, 7) * 0.1;
                double p2 = rand.Next(3, 5) * 0.1;
                double p3 = rand.Next(5, 8) * 0.1;

                string task =
                    $"9. Батарея из трех орудий произвела залп, причем два снаряда попали в цель. " +
                    $"Найти вероятность того, что первое орудие дало попадание, если вероятности попадания в цель первым, вторым и третьим соответственно равны p1 = {p1}, p2 = {p2}, p3 = {p3}.";

                double solution = Math.Round(p1 * (p2 * (1 - p3) + (1 - p2) * p3) / (p1 * (p2 * (1 - p3) + (1 - p2) * p3) + (1 - p1) * p2 * p3), 3);
                if (tasksList[8] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region tenth task
            {
                int total = rand.Next(5, 7);
                int fallen = rand.Next(2, 4);
                string task =
                    $"10. Игральная кость бросается {total} раз. Найти вероятность того, что три очка выпадут {fallen} раза.";
                double solution = Math.Round(combinations(fallen, total) * Math.Pow(1.0 / 6, fallen) * Math.Pow(5.0 / 6, total - fallen), 3);
                if (tasksList[9] == true)
                {
                    put(tasks, answers, task, solution.ToString());
                    //if (tasksList[10] == true)
                    //{
                    //    //tasks.InsertSectionPageBreak();
                    //    //answers.InsertSectionPageBreak();
                    //}
                }
            }
            #endregion

            #region eleventh and twelth tasks
            {
                if (tasksList[10] == true || tasksList[11] == true)
                {
                    double p1 = rand.Next(2, 4) * 0.05, x1 = -rand.Next(9, 10) * 0.1;
                    double p2 = rand.Next(3, 6) * 0.05, x2 = -rand.Next(7, 8) * 0.1;
                    double p3 = rand.Next(2, 5) * 0.05, x3 = -rand.Next(5, 6) * 0.1;
                    double p4 = rand.Next(1, 3) * 0.05, x4 = -rand.Next(3, 4) * 0.1;
                    double p5 = 1 - p1 - p2 - p3 - p4, x5 = -rand.Next(1, 2) * 0.1;

                    Paragraph p = tasks.InsertParagraph("11. Случайная величина имеет следующее распределение вероятностей:");
                    p.SpacingAfter(5);
                    answers.InsertParagraph(p);

                    var table = tasks.AddTable(2, 6);
                    table.Rows[0].Cells[0].Paragraphs[0].Append("x");
                    table.Rows[1].Cells[0].Paragraphs[0].Append("p(x)");
                    table.Rows[0].Cells[1].Paragraphs[0].Append(x1.ToString());
                    table.Rows[1].Cells[1].Paragraphs[0].Append(p1.ToString());
                    table.Rows[0].Cells[2].Paragraphs[0].Append(x2.ToString());
                    table.Rows[1].Cells[2].Paragraphs[0].Append(p2.ToString());
                    table.Rows[0].Cells[3].Paragraphs[0].Append(x3.ToString());
                    table.Rows[1].Cells[3].Paragraphs[0].Append(p3.ToString());
                    table.Rows[0].Cells[4].Paragraphs[0].Append(x4.ToString());
                    table.Rows[1].Cells[4].Paragraphs[0].Append(p4.ToString());
                    table.Rows[0].Cells[5].Paragraphs[0].Append(x5.ToString());
                    table.Rows[1].Cells[5].Paragraphs[0].Append(p5.ToString());
                    table.SetWidths(new float[] { 40F, 40F, 40F, 40F, 40F, 40F });

                    string solution_11 = $"\n\tF(x) = 0 при x <= {x1};\n" +
                        $"\tF(x) = {p1} при {x1} < x <= {x2};\n" +
                        $"\tF(x) = {p1 + p2} при {x2} < x <= {x3};\n" +
                        $"\tF(x) = {p1 + p2 + p3} при {x3} < x <= {x4};\n" +
                        $"\tF(x) = {p1 + p2 + p3 + p4} при {x4} < x <= {x5};\n" +
                        $"\tF(x) = 1 при x > {x5}.";

                    string task_12 =
                        $"12. Найти математическое ожидание, дисперсию и среднеквадратичное отклонение случайной величины x примера 11.";
                    double mat = Math.Round(x1 * p1 + x2 * p2 + x3 * p3 + x4 * p4 + x5 * p5, 3);
                    double dis = Math.Round(x1 * x1 * p1 + x2 * x2 * p2 + x3 * x3 * p3 + x4 * x4 * p4 + x5 * x5 * p5 - mat * mat, 3);

                    string solution_12 = $"M(x)={mat}, D(x)={dis}, σ(x)={Math.Round(Math.Sqrt(dis), 3)}";

                    tasks.InsertTable(table);
                    answers.InsertTable(table);

                    p = tasks.InsertParagraph("Построить многоугольник распределения и найти функцию распределения F(x).");
                    p.SpacingBefore(5);
                    p.SpacingAfter(10);
                    p = answers.InsertParagraph("Построить многоугольник распределения и найти функцию распределения F(x).");
                    p.SpacingBefore(5);
                    p.SpacingAfter(5);
                    p = answers.InsertParagraph("Ответ: " + solution_11);
                    p.SpacingAfter(10);

                    if (tasksList[11] == true)
                        put(tasks, answers, task_12, solution_12);
                }

            }
            #endregion

            #region thirteenth and forteenth tasks
            {
                if (tasksList[12] == true || tasksList[13] == true)
                {
                    int bound = 0;
                    int pow = 0;
                    string pow_string1 = "";
                    string pow_string2 = "";
                    int var1 = rand.Next(1, 4);
                    int var2 = rand.Next(1, 3);

                    switch (var1)
                    {
                        case 1: { pow = 1; pow_string1 = ""; pow_string2 = "\u00B2"; break; }
                        case 2: { pow = 2; pow_string1 = "\u00B2"; pow_string2 = "\u00B3"; break; }
                        case 3: { pow = 3; pow_string1 = "\u00B3"; pow_string2 = "\u2074"; break; }
                    }
                    switch (var2)
                    {
                        case 1: { bound = 1; break; }
                        case 2: { bound = 2; break; }
                    }

                    string task_13 =
                        $"13. x - непрерывная случайная величина с плотностью распределения f(x), заданной функцией: " +
                        $"f(x) = -Ax{pow_string1}, если x принадлежит промежутку (0; {bound}], и f(x) = 0 в противном случае.";

                    pow++;
                    double A = pow / Math.Pow(bound, pow);

                    string solution_13 = $"\n\tF(x) = 0 при x <= 0;\n" +
                        $"\tF(x) = {A / pow}x{pow_string2} при 0 < x <= {bound};\n" +
                        $"\tF(x) = {bound * A / pow} при x > {bound}";

                    string task_14 =
                        $"14. x - непрерывная величина из примера 13. Найти математическое ожидание, дисперсию и среднеквадратичное отклонение.";

                    double mat = Math.Round(A / (pow + 1) * Math.Pow(bound, pow + 1), 3);
                    double dis = Math.Round(A / (pow + 2) * Math.Pow(bound, pow + 2) - mat * mat, 3);
                    string solution_14 =
                        $"M(x) = {mat}, " +
                        $"D(x) = {dis}, " +
                        $"σ(x) = {Math.Round(Math.Sqrt(dis), 3)}";

                    put(tasks, answers, task_13, solution_13);
                    if (tasksList[13] == true)
                        put(tasks, answers, task_14, solution_14);
                }

            }
            #endregion

            #region fifteenth task
            {
                int number = rand.Next(2, 5) * 10;

                string task =
                    $"15. Вероятность рождения мальчика равна 0,51. Найти вероятность того, что среди 100 новорожденных окажется {number} мальчиков.";

                string solution = $"0.2ϕ({0.2 * (number - 51)})";

                if (tasksList[14] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region sixteenth task
            {
                double sigma = rand.Next(1, 2) * 0.1;
                double e = rand.Next(1, 4) * 0.1;

                string task =
                    $"16. x - нормально распределенная случайная величина с параметрами а = 1, σ = {sigma}.\nНайти Р(|x - 1|< {e}).";

                string solution = $"2Φ({Math.Round(e / sigma, 3)})";
                if (tasksList[15] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region seventeenth task
            {
                int total = rand.Next(5, 8) * 25;
                int number = rand.Next(2, 4) * 25;

                string task =
                    $"17. Цех выпускает в среднем 80% продукции высшего качества. Какова вероятность того, что в партии из {total} изделий будет больше {number} изделий высшего качества.";

                string solution = $"Φ({Math.Round((total - total * 0.8) / (0.4 * Math.Sqrt(total)), 3)}) - Φ({Math.Round((number - total * 0.8) / (0.4 * Math.Sqrt(total)), 3)})";
                if (tasksList[16] == true)
                    put(tasks, answers, task, solution.ToString());
            }
            #endregion

            #region eighteenth task
            {
                if (tasksList[17] == true)
                {
                    double a1 = rand.Next(0, 2) * 0.1;
                    double a2 = rand.Next(1, 3) * 0.1;
                    double a3 = rand.Next(1, 2) * 0.1;
                    double a4 = rand.Next(0, 1) * 0.1;
                    double a5 = rand.Next(1, 2) * 0.1;
                    double a6 = 1 - a1 - a2 - a3 - a4 - a5;

                    Paragraph p = tasks.InsertParagraph("18. Дана таблица распределения вероятностей двумерной случайной величины (x,y):");
                    p.SpacingAfter(5);
                    answers.InsertParagraph(p);

                    var table = tasks.AddTable(3, 4);
                    table.Rows[0].Cells[0].Paragraphs[0].Append("x\\y");
                    table.Rows[1].Cells[0].Paragraphs[0].Append("0");
                    table.Rows[2].Cells[0].Paragraphs[0].Append("1");
                    table.Rows[0].Cells[1].Paragraphs[0].Append("-1");
                    table.Rows[1].Cells[1].Paragraphs[0].Append(a1.ToString());
                    table.Rows[2].Cells[1].Paragraphs[0].Append(a4.ToString());
                    table.Rows[0].Cells[2].Paragraphs[0].Append("0");
                    table.Rows[1].Cells[2].Paragraphs[0].Append(a2.ToString());
                    table.Rows[2].Cells[2].Paragraphs[0].Append(a5.ToString());
                    table.Rows[0].Cells[3].Paragraphs[0].Append("1");
                    table.Rows[1].Cells[3].Paragraphs[0].Append(a3.ToString());
                    table.Rows[2].Cells[3].Paragraphs[0].Append(a6.ToString());
                    table.SetWidths(new float[] { 40F, 40F, 40F, 40F });

                    double Me = a4 + a5 + a6;
                    double De = Me - Me * Me;
                    double Mn = -1 * (a1 + a4) + (a3 + a6);
                    double Dn = a1 + a4 + a3 + a6;
                    double Men = -a4 + a6;
                    double Den = a4 + a6 - Men * Men;

                    string solution = $"M(x) = {Me}, M(y) = {Mn}, D(x) = {De}, D(y) = {Dn}, M(xy) = {Men}, D(xy) = {Den}";

                    tasks.InsertTable(table);
                    answers.InsertTable(table);
                    p = tasks.InsertParagraph("Найти M(x), M(y), D(x), D(y), M(xy), D(xy).");
                    p.SpacingBefore(5);
                    p.SpacingAfter(10);
                    p = answers.InsertParagraph("Найти M(x), M(y), D(x), D(y), M(xy), D(xy).");
                    p.SpacingBefore(5);
                    p.SpacingAfter(5);

                    p = answers.InsertParagraph("Ответ: " + solution + ".");
                    p.SpacingAfter(10);
                }
            }
            #endregion

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (numberVars.Text == "" || filename.Text == "" || filepath.Text == "" || filename1 == "" || numericUpDown2.Value - numericUpDown1.Value < 0)
            {
                MessageBox.Show("Введите данные");//если пользователь забыл ввести данные
                return;
            }
            int number = Convert.ToInt32(numberVars.Text);
            if (number > 100)
            {
                MessageBox.Show("Максимальное количество вариантов 100");//если пользователь захочет более 100 вариантов выводим ему надпись 
                return;
            }
            if (!Directory.Exists(filepath.Text))//определяет указывает ли данный путь на существующий каталог на диске
            {
                MessageBox.Show("Директория " + filepath.Text + " не существует");
                return;
            }
            if (File.Exists(filepath.Text + "\\" + filename.Text + ".docx"))//Определяем существует ли файл с таким же названием 
            {
                DialogResult result = MessageBox.Show("Файл с таким названием уже существует.\nПерезаписать?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            }

            if (!filepath.Items.Contains(filepath.Text))
            {
                filepath.Items.Add(filepath.Text);
                paths.Add(filepath.Text);
            }
            for (int i = 0; i < 18; i++)
            {
                if (i + 1 >= numericUpDown1.Value && i + 1 <= numericUpDown2.Value)
                    tasksList[i] = true;
                else tasksList[i] = false;
            }

            // создаём документ с заданиями и ответами 
            DocX task_document = DocX.Create(filepath.Text + "\\" + filename.Text + ".docx");
            DocX answer_document = DocX.Create(filepath.Text + "\\" + filename.Text + "_ответы.docx");
            int var_number = 0;
            string[] lines = File.ReadAllLines(filename1);
            foreach (string s in lines)
            {

                if (var_number <= number)
                {
                    var_number++;
                    Paragraph p = task_document.InsertParagraph(var_number.ToString() + " вариант " + s);//вывод номера варианта 

                    p.FontSize(16);
                    p.SpacingAfter(20);
                    answer_document.InsertParagraph(p);
                    generate(task_document, answer_document);
                    if (var_number != number)
                    {
                        task_document.InsertSectionPageBreak();
                        answer_document.InsertSectionPageBreak();
                    }
                }
            }
            // сохраняем документ
            task_document.Save();
            answer_document.Save();

            MessageBox.Show(" Генерация успешно выполнена ");

        }

        private void filepathButton_Click(object sender, EventArgs e)
        {
            string filepath_string = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                filepath_string = folderBrowserDialog1.SelectedPath;
            }

            filepath.Text = filepath_string;
        }

        private void filepath_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filepath.SelectedItem != null) return;
            filepath.Items.Clear();
            foreach (string str in paths)
            {
                if (str.ToUpper().Contains(filepath.Text.ToUpper()))
                    filepath.Items.Add(str);
            }
            filepath.DroppedDown = true;
            Cursor.Current = Cursors.Default;
            filepath.SelectionStart = filepath.Text.Length;
        }

        private void item2_Click(object sender, EventArgs e)
        {
            string message = "Введите в соотвествующие поля необходимые данные и нажмите <Сгенерировать>.\n" +
                "В выбранную вами папку будут сохранены два файла: имя_файла.docx, содержащий условия задач, и имя_файла_ответы.docx, содержащий условия задач с ответами.";
            MessageBox.Show(message, "Справка", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void item3_Click(object sender, EventArgs e)
        {
            string message = "Условия задач взяты из 30 варианта типового расчета.\n" +
                           "Авторы: 29/2 группа\n" +
                           "Турдакова О. Д.\n" +
                           "Игнатьева М.А.\n"
                           + "Шешунов М.И.\n" +
                           "Константинов В.Д. \n" +
                           "2021 год";
            MessageBox.Show(message, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
               filename1 = openFileDialog1.FileName;  
        }

       
    }
}
