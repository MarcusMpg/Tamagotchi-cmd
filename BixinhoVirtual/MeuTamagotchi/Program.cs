using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Xml.Schema;



namespace MeuTamagotchi
{
    internal class Program
    {
        //inicio da imagem
        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            // Cria um novo Bitmap com a largura e altura desejadas
            Bitmap resizedImage = new Bitmap(width, height);

            // Desenha a imagem original no novo Bitmap usando as dimensões desejadas
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        static string ConvertToAscii(Bitmap image)
        {
            // Caracteres ASCII usados para representar a imagem
            char[] asciiChars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '@' };

            StringBuilder asciiArt = new StringBuilder();

            // Percorre os pixels da imagem e converte cada um em um caractere ASCII correspondente
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayScale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int asciiIndex = grayScale * (asciiChars.Length - 1) / 255;
                    char asciiChar = asciiChars[asciiIndex];
                    asciiArt.Append(asciiChar);
                }
                asciiArt.Append(Environment.NewLine);
            }

            return asciiArt.ToString();
        }

        static void ExibirImagem(string imagePath, int width, int height)
        {
            // Caminho para a imagem que deseja exibir
            //string imagePath = @""C:\Users\marcu\Downloads\Gato.jpg"";

            // Carrega a imagem
            Bitmap image = new Bitmap(imagePath);

            // Redimensiona a imagem para a largura e altura desejadas
            int consoleWidth = width;
            int consoleHeight = height;
            Bitmap resizedImage = ResizeImage(image, consoleWidth, consoleHeight);

            // Converte a imagem em texto ASCII
            string asciiArt = ConvertToAscii(resizedImage);

            // Exibe o texto ASCII no console
            Console.WriteLine(asciiArt);
        }
            //fim da imagem
        static void GravarArquivoTextto(string nome, string nomeDono, float alimentado, float limpo, float feliz)
        {
            string fileContent = nome + Environment.NewLine;
            fileContent += nomeDono + Environment.NewLine;
            fileContent += alimentado + Environment.NewLine;
            fileContent += limpo + Environment.NewLine;
            fileContent += feliz + Environment.NewLine;
            string dir = Environment.CurrentDirectory + "\\";
            string file = dir + nome + nomeDono + ".txt";
            File.WriteAllText(file, fileContent);

        }
        static void LerArquivoTexto(string nome, string nomeDono, ref float alimentado, ref float limpo, ref float feliz)
        {
            string dir = Environment.CurrentDirectory + "\\";
            string file = dir + nome + nomeDono + ".txt";
            if (File.Exists(file))
            {
                string[] dados = File.ReadAllLines(file);
                alimentado = float.Parse(dados[2]);
                limpo = float.Parse(dados[3]);
                feliz = float.Parse(dados[4]);
                if (alimentado <= 0 || limpo <= 0 || feliz <= 0)
                {
                    Console.WriteLine("Seu bichinho morreu anteriormente!!!");
                    Console.WriteLine("Criando novo PET:!");
                    alimentado = 100;
                    limpo = 100;
                    feliz = 100;
                }
            }
        }
        
        static string Falar()
        {
            string[] conversa = new string[10];
            conversa[0] = "       Nossa o dia foi muito legal!, comi muito!!";
            conversa[1] = "       Que saudades passei o dia todo esperando você!, quase morri!";
            conversa[2] = "       Nem fiz nada hj!";
            conversa[3] = "       Porque os passaros conseguem voar?";
            conversa[4] = "       Aqueles que riem por último sempre pensam mais devagar.";
            conversa[5] = "       Um pessimista é um otimista com experiência.";
            conversa[6] = "       Estou ficando sem sentimentos, acho qque tô virando homem!";
            conversa[7] = "       Já dizia Péricles: Eu não to legal.";
            conversa[8] = "       Parece que eu to pagando pelos pecados que eu nem cometi ainda.";
            conversa[9] = "       A voz dentro da minha cabeça não calou a boca hj, to pistola.!";

            Random rand = new Random();
            return conversa[rand.Next(conversa.Length)];
        }

        static void LerDados(ref string nome, ref string nomeDono)
        {
            Console.Write("     Qual é o seu nome: ");
            nomeDono = Console.ReadLine();
            Console.Clear();
            Console.Write("     Qual o nome do seu PET virtual:");
            nome = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("      Legal, estava com muita saldade de vc {0}", nomeDono);
            Console.WriteLine("");
        }

        static string Interagir(string nomeDono, ref float alimentado, ref float limpo, ref float feliz)
        {
            string entrada = "";
            Random rand = new Random();
            Console.WriteLine("        O que vamos fazer hoje?");
            Console.Write("       Brincar/ Comer/ Banho/ Nada: ");
            entrada = Console.ReadLine().ToLower();
            Console.Clear();
            switch (entrada)
            {
                case "comer": alimentado += rand.Next(30); break;
                case "banho": limpo += rand.Next(30); break;
                case "brincar": feliz += rand.Next(30); break;
            }
            if (feliz > 100)
            {
                Console.WriteLine("------ Cansei de brincar");
                feliz = 100;
            }
            if (alimentado > 100)
            {
                Console.WriteLine("------ Estou cheio, Obirgado!");
                alimentado = 100;
            }
            if (limpo > 100)
            {
                Console.WriteLine("------ Já estou cherosinho!!!");
                limpo = 100;
            }
            return entrada;
        }
        static void AtualizarStatus(ref float alimentado, ref float limpo, ref float feliz)
        {
            //diminuir os  valores das caracteristicas do animal
            //0 - alimento; 1 - limpo; 2 - feliz;
            int caracteristicas = 0;
            Random rand = new Random();
            caracteristicas = rand.Next(3);
            switch (caracteristicas)
            {
                case 0: alimentado -= rand.Next(10); break;
                case 1: limpo -= rand.Next(10); break;
                case 2: feliz -= rand.Next(10); break;
            }
        }
        static void ExibirStatus(float alimentado, float limpo, float feliz, int tipo)
        {
            if (tipo == 0)
            {
                Console.WriteLine("Status do meu bichinho");
                Console.WriteLine("Alimentado: {0}", alimentado);
                Console.WriteLine("Limpo: {0}", limpo);
                Console.WriteLine("Feliz: {0}", feliz); 
            }
            if (tipo == 1)
            {
                    string fotoFome = Environment.CurrentDirectory + "\\pandaDeitado.jpg";
                if (alimentado > 30 && alimentado < 75)
                {
                    Console.WriteLine("");
                    Console.WriteLine("       Estou faminto !!!");
                    Console.WriteLine("       Bota um prato de comida ai pra mim!");
                    Console.WriteLine("");
                    ExibirImagem(fotoFome, 60, 25);
                }
                if (limpo > 30 && limpo < 75)
                {
                    Console.WriteLine("");
                    Console.WriteLine("       Estou podrinho !!!");
                    Console.WriteLine("       me dá banho!");
                    Console.WriteLine("");
                    ExibirImagem(fotoFome, 60, 25);
                }
                if (feliz > 30 && feliz < 75)
                {
                    Console.WriteLine("");
                    Console.WriteLine("       que vida triste !!!");
                    Console.WriteLine("       SAD! quero colo");
                    Console.WriteLine("");
                    ExibirImagem(fotoFome, 60, 25);
                }
            }
            if (tipo == 2)
            {
                Console.WriteLine("Status do meu bichinho");
                Console.WriteLine("Alimentado: {0}", alimentado);
                Console.WriteLine("Limpo: {0}", limpo);
                Console.WriteLine("Feliz: {0}", feliz);
                Console.WriteLine("Nivel de felicidade");
                if (alimentado > 30 && alimentado < 75)
                {
                    Console.WriteLine("");
                    Console.WriteLine("       Estou faminto !!!");
                    Console.WriteLine("       Bota um prato de comida ai pra mim!");
                   // Console.WriteLine("");
                   
                }
                if (limpo > 30 && limpo < 75)
                {
                    Console.WriteLine("");
                    Console.WriteLine("       Estou podrinho !!!");
                    Console.WriteLine("       me dá banho!");
                    Console.WriteLine("");
                }
                if (feliz > 30 && feliz < 75)
                {
                    Console.WriteLine("");
                    Console.WriteLine("       que vida triste !!!");
                    Console.WriteLine("       SAD! quero colo");
                    Console.WriteLine("");
                }
            }
        }

        
        static void Main(string[] args)
        {
            //Dados do jogo: 
            string entrada = "";  //entrada de dados
            string fotoinicial = Environment.CurrentDirectory + "\\panda.jpg"; //foto
            string fotoMorto = Environment.CurrentDirectory + "\\pandaMorto.jpg"; //foto
            string nomeDono = ""; //nome do jogador
            //Dados do bichinho
            string nome = ""; //nome do bichinho
            //status do bichinho
            float alimentado = 100;
            float limpo = 100;
            float feliz = 100;

            ExibirImagem(fotoinicial, 60, 25);
           Console.WriteLine("              Meu Tamagotchi");
            //Entrada de dados
            LerDados(ref nome, ref nomeDono);
            //Coleta os dados do bichinho no arquivo de texto
            LerArquivoTexto( nome,  nomeDono, ref alimentado, ref limpo, ref feliz);
            Console.WriteLine("Olá, {0}", nomeDono);

            // PROGGRAMA principal.
            while (entrada.ToLower() != "nada" && alimentado > 0 && limpo > 0 && feliz > 0)
            {
                //Console.WriteLine("alimentado: {0} %  / limpo: {1} %  / feliz: {2} % ", alimentado, limpo, feliz);
                //Console.ReadKey(); 
                
                //Contato inicial
                Console.Clear();
                Console.WriteLine(Falar());
                Thread.Sleep(3000);
                //Atualizo as caracterisdticas do animal.
                AtualizarStatus(ref alimentado, ref limpo, ref feliz);
                Console.Clear();
                //Exibo o status do bichinho
                ExibirStatus(alimentado, limpo, feliz, 1);
                Thread.Sleep(3000);
                Console.Clear();
                //Resposta para o animal
                entrada = Interagir(nomeDono, ref alimentado, ref limpo, ref feliz);

               

                
            }

            //Siu do jogo
            Console.Clear();
            if (alimentado <= 0 || limpo <= 0 || feliz <= 0)
            {
               
                Console.WriteLine("       A vida é tão fragio e pode se esvair em um sopro.");
                Console.WriteLine("       Descanse em paz {0}", nome);
                Console.WriteLine("       GAME OVER!");
                Console.WriteLine("       Inicie um novo jogo!.");
                ExibirImagem(fotoMorto ,60, 25);
            }
            else
            {
                
                Console.WriteLine("     Até aproxima {0} ansioso para a brincar mais depois!!!", nomeDono);
                Console.WriteLine("     Até outro dia.");
                ExibirImagem(fotoinicial, 60, 25);
            }

            GravarArquivoTextto( nome, nomeDono, alimentado, limpo, feliz);
            Console.ReadKey();
        }
    }
}
