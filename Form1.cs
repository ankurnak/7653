using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace lab24_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private byte[] GenerateRandomKey(int v)
        {
            throw new NotImplementedException();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string inputFile = textBox1.Text;
            string outputFile = textBox2.Text;

            await Task.Run(() =>
            {
                try
                {
                    byte[] key = GenerateRandomKey(8); // Генеруємо випадковий ключ довжиною 8 байт

                    using (FileStream inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                    {
                        using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                        {
                            ESIGNEngine cipher = new ESIGNEngine();
                            KeyParameter keyParam = new KeyParameter(key);

                            cipher.Init(true, keyParam); // Ініціалізуємо шифр з ключем

                            byte[] inputBuffer = new byte[cipher.GetBlockSize()];
                            byte[] outputBuffer = new byte[cipher.GetBlockSize()];

                            int inputLength = 0;
                            int outputLength = 0;

                            while ((inputLength = inputStream.Read(inputBuffer, 0, inputBuffer.Length)) > 0)
                            {
                                outputLength = cipher.ProcessBlock(inputBuffer, 0, inputLength, outputBuffer, 0);
                                outputStream.Write(outputBuffer, 0, outputLength);
                            }
                        }
                    }

                    MessageBox.Show("Шифрування ESIGN завершено успішно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка під час шифрування: " + ex.Message);
                }
            });
        }
        
        private async void button2_Click(object sender, EventArgs e)
        {
            string inputFile = textBox1.Text;
            string outputFile = textBox2.Text;

            await Task.Run(() =>
            {
                try
                {
                    byte[] key = GenerateRandomKey(16); // Генеруємо випадковий ключ довжиною 16 байт

                    using (FileStream inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                    {
                        using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                        {
                            Mdc2Digest digest = new Mdc2Digest();
                            BufferedBlockCipher cipher = new BufferedBlockCipher(new CfbBlockCipher(new AesEngine(), 128));
                            KeyParameter keyParam = new KeyParameter(key);

                            byte[] inputBuffer = new byte[cipher.GetBlockSize()];
                            byte[] outputBuffer = new byte[cipher.GetOutputSize(inputBuffer.Length)];

                            int inputLength = 0;
                            int outputLength = 0;

                            while ((inputLength = inputStream.Read(inputBuffer, 0, inputBuffer.Length)) > 0)
                            {
                                digest.BlockUpdate(inputBuffer, 0, inputLength); // Обчислюємо хеш від вхідного буфера
                                outputLength = cipher.ProcessBytes(inputBuffer, 0, inputLength, outputBuffer, 0);
                                outputStream.Write(outputBuffer, 0, outputLength);
                            }

                            outputLength = cipher.DoFinal(outputBuffer, 0);
                            outputStream.Write(outputBuffer, 0, outputLength);

                            byte[] hash = new byte[digest.GetDigestSize()];
                            digest.DoFinal(hash, 0); // Отримуємо фінальний хеш

                            // Зберігаємо хеш у файл з тим же ім'ям, що й вихідний файл, але з розширенням ".mdc2"
                            string hashFile = Path.ChangeExtension(outputFile, "mdc2");
                            File.WriteAllBytes(hashFile, hash);
                        }
                    }

                    MessageBox.Show("Шифрування MDC-2 завершено успішно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка під час шифрування: " + ex.Message);
                }
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string inputFile = textBox1.Text;
            string outputFile = textBox2.Text;

            await Task.Run(() =>
            {
                try
                {
                    byte[] key = GenerateRandomKey(16); // Генеруємо випадковий ключ довжиною 16 байт

                    using (FileStream inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                    {
                        using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                        {
                            RC2Engine cipher = new RC2Engine();
                            KeyParameter keyParam = new KeyParameter(key);

                            cipher.Init(true, keyParam); // Ініціалізуємо шифр з ключем

                            byte[] inputBuffer = new byte[cipher.GetBlockSize()];
                            byte[] outputBuffer = new byte[cipher.GetBlockSize()];

                            int inputLength = 0;
                            int outputLength = 0;

                            while ((inputLength = inputStream.Read(inputBuffer, 0, inputBuffer.Length)) > 0)
                            {
                                outputLength = cipher.ProcessBlock(inputBuffer, 0, inputLength, outputBuffer, 0);
                                outputStream.Write(outputBuffer, 0, outputLength);
                            }
                        }
                    }

                    MessageBox.Show("Шифрування RC2 завершено успішно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка під час шифрування: " + ex.Message);
                }
            });
        }
    }

    class KeyParameter
    {
        private byte[] key;

        public KeyParameter(byte[] key)
        {
            this.key = key;
        }
    }

    class RC2Engine
    {
        internal int GetBlockSize()
        {
            throw new NotImplementedException();
        }

        internal void Init(bool v, KeyParameter keyParam)
        {
            throw new NotImplementedException();
        }

        internal int ProcessBlock(byte[] inputBuffer, int v1, int inputLength, byte[] outputBuffer, int v2)
        {
            throw new NotImplementedException();
        }
    }

    class Mdc2Digest
    {
        internal void BlockUpdate(byte[] inputBuffer, int v, int inputLength)
        {
            throw new NotImplementedException();
        }

        internal void DoFinal(byte[] hash, int v)
        {
            throw new NotImplementedException();
        }

        internal int GetDigestSize()
        {
            throw new NotImplementedException();
        }
    }

    class AesEngine
    {
        public AesEngine()
        {
        }
    }

    class BufferedBlockCipher
    {
        public BufferedBlockCipher(CfbBlockCipher cfbBlockCipher)
        {
        }

        internal int DoFinal(byte[] outputBuffer, int v)
        {
            throw new NotImplementedException();
        }

        internal int GetBlockSize()
        {
            throw new NotImplementedException();
        }

        internal int GetOutputSize(int length)
        {
            throw new NotImplementedException();
        }

        internal int ProcessBytes(byte[] inputBuffer, int v1, int inputLength, byte[] outputBuffer, int v2)
        {
            throw new NotImplementedException();
        }
    }

    class CfbBlockCipher
    {
        private AesEngine aesEngine;
        private int v;

        public CfbBlockCipher(AesEngine aesEngine, int v)
        {
            this.aesEngine = aesEngine;
            this.v = v;
        }
    }

    class ESIGNEngine
    {
        internal int GetBlockSize()
        {
            throw new NotImplementedException();
        }

        internal void Init(bool v, KeyParameter keyParam)
        {
            throw new NotImplementedException();
        }

        internal int ProcessBlock(byte[] inputBuffer, int v1, int inputLength, byte[] outputBuffer, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
