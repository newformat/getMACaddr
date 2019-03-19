using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Реализовано - 
 * v0.1 Общий вид приложения
 * v0.3 Поиск МАС адресов под шаблон "MAC адрес:" (портал)
 * v0.4 Поиск МАС адресов под шаблон "DiscoverNotification" (shell)
 * v0.5 Поиск МАС адресов под шаблон "UnknownDeviceNotification" (shell)
 * v0.6 Поиск серийного номера под шаблон "Серийный номер:"
 * v0.7 Исключения на обработку ошибки копирования пустого поля
 * v0.8 Копирование МАС адресов с кнопки
 * v0.9 Вставка информации в основной текстовой блок с кнопки
 * v1.0 Чиста основного текстового блока с кнопки
 * v1.2 (12.03.19) Выбор типа данных
 * v1.3 (14.03.19) Поиск МАС адресов по шаблону "МАС адрес" без двоеточий, сам адрес на след. строке.
 * v1.3 (16.03.19) Установить кнопку "О программе" в верхнее встроенное меню
 * v1.5 (20.03.19) Исправлена ошибка с шаблоном "Упрощенная сортировка,"; -
 * Запрет на вторичный запуск(Program.cs); Поле МАС адреса можно редактировать
 */

namespace parseGetMAC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox2.Text = "";

            // создать объект типа данных массива со строками.
            String[] s = textBox1.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            bool id = false;
            foreach (String ch in s)
            {
                /* может пригодится, пока оставлю
                if (id) {textBox2.Text = textBox2.Text + ch.Trim() + "\r\n";id = false;continue;} */

                if (ch.IndexOf("MAC адрес:") != -1 && comboBox1.SelectedIndex == 0)
                {
                    String[] MACAddr = ch.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    textBox2.Text = textBox2.Text + MACAddr[1] + "\r\n";
                }
                else if (ch.IndexOf("Серийный номер:") != -1 && comboBox1.SelectedIndex == 1)
                {
                    String[] SerialNumber = ch.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    textBox2.Text = textBox2.Text + SerialNumber[1] + "\r\n";
                }
                else if (ch.IndexOf("DiscoverNotification") != -1 || ch.IndexOf("UnknownDeviceNotification") != -1 && comboBox1.SelectedIndex == 1 )
                {
                    String[] data = ch.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    textBox2.Text = textBox2.Text + data[6].Substring(15, 16) + "\r\n";
                }
                else if (ch.IndexOf("MAC адрес") != -1 && comboBox1.SelectedIndex == 0)
                {
                    String[] MACAddr = ch.Split(new char[] { 'с' }, StringSplitOptions.RemoveEmptyEntries);
                    textBox2.Text = textBox2.Text + MACAddr[1] + "\r\n";
                }
                /*else if(ch.IndexOf("MAC адрес") != -1 && comboBox1.SelectedIndex == 0){id = true;continue;}*/
                else
                    continue;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = Clipboard.GetText(); // получить текст из буфера обмена
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
                Clipboard.SetText(textBox2.Text); // записать текст  в буфер обмена
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false; // отключить "развернуть окно"
        }



        private void оПрограммеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Последнее обновление 20.03.19 -\n" +
                "- Исправлена ошибка с шаблоном \"Упрощенная сортировка, \";\n" +
                "- Запрет на вторичный запуск(Program.cs);\n" 
                + "- Поле МАС адреса можно редактировать\n", 
                "О программе", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

    }
}
