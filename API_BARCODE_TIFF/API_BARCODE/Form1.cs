using System;
using System.Net.Mail;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Spire.Barcode;
using System.Drawing;
using iTextSharp.text.pdf;


namespace API_BARCODE
{
    public partial class Form1 : Form
    {
        //variavel que vai conter o email do utilizador que está a digitalizar     
        string Email = "";
        //varivel que armazena os códigos de barras    
        string[] Barcode;
        //mensagem de erro 
        string Mensagem_Erro = "";
        //variavel que armazena os ficheiros lidos na pasta de deposito
        string[] Ficheiros_Retornados;
        //variavel que armazena o ultimo ficheiro lido
        string Ultimo_Ficheiro_Executado = "";
        //localização do ficheiro txt dos logs
        string Localizacao_Logs_Sucesso = ".\\logs.txt";
        string Localizacao_Logs_Erro_invalido = ".\\logs.txt";
        string Localizacao_Logs_Erro_Geral = ".\\logs.txt";
        //declaraçao da variavel e do tipo de data do timestamp
        string Data_Hora = DateTime.Now.ToString("dd_MM_yyyy__hh_mm_ss_fff_tt");
        //conta os erros ao ler os ficheiros
        int Conta_OS_Erros = 0;
        //conta os ficheiros lidos com sucesso 
        int Conta_OS_Sucesso = 0;
        //conta o total de ficheiros processados 
        int Conta_Total_Processados = 0;
        //inicializa a 0 o quantidade de vezes que é efetuado o click no butão de pausa
        public static int numero_clicks_pausa = 0;
        //verifica se foi acionado o click do butão 
        public bool click_botao_pausa = false;
        //posição em que o array de logs se encontra
        public int posicao_logs = 0;
        public int posicao_logs_erro = 0;
        //linha temporal para permitir a paraguem da aplicação quando é efetuado o click do butão
        public Thread thread;
        string outputFilePath;
        string inputFilePath;
        public string[] array_mensagens_logs = new string[500];
        public string[] array_mensagens_logs_erro = new string[500];


        public Form1()
        {
            InitializeComponent();

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            MSG_LOGS.AppendText(Environment.NewLine);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Update();
            this.Iniciar_Processo_Click(sender, e);
        }


        public void Iniciar_Processo_Click(object sender, EventArgs e)
        {
            //invoca o ficheiro .ini para leitura da pasta de origem, destino, tipo de ficheiro e pasta de erros
            var MyIni = new INIFILE(@".\ini_teste.ini");
            var Pasta_Origem = MyIni.Read("Pasta_Origem");
            var Pasta_Destino = MyIni.Read("Pasta_Destino");
            var Tipo_Ficheiro = MyIni.Read("Tipo_Ficheiro");
            var Pasta_Erros = MyIni.Read("Pasta_Erros");

            //começa um linha de execução que pode ser parada para análise de erros 
            thread = new Thread(() =>
            {
                //vai buscar todo o tipo de ficheiro
                Ficheiros_Retornados = Directory.GetFiles(Pasta_Origem);
                //le os ficheiros da pasta de depósito
                foreach (var Ler_ficheiros in Ficheiros_Retornados)
                {
                    try
                    {//validação do tipo de ficheiro (tif)
                        if (Path.GetExtension(Ler_ficheiros.ToString()) == Tipo_Ficheiro)
                        {
                            //retira o nome do ultimo ficheiro lido 
                            Ultimo_Ficheiro_Executado = Ler_ficheiros.Substring(Ler_ficheiros.LastIndexOf("Deposito\\") + 9);
                            //invoca a dll com passaguem de ficheiro por parametro, sendo devolvido o code bar do ficheiro(code 39)
                            Barcode = BarcodeScanner.Scan(Ler_ficheiros.ToString(), BarCodeType.Code39);
                            //só move o ficheiro se o barcode for diferente de null
                            if (Barcode[0] != null)
                            {
                                //recolhe o timestamp no momento em que o ficheiro é processado 
                                Data_Hora = DateTime.Now.ToString("dd_MM_yyyy__hh_mm_ss_fff_tt");
                                posicao_logs++;
                                array_mensagens_logs[posicao_logs] = "Date: " + Data_Hora + " | Move file: " + Ler_ficheiros.Substring(Ler_ficheiros.LastIndexOf("Deposito\\") + 9) + " | Barcode: " + Barcode[0].ToString() + ". - SUCCESS";
                                //move o ficheiro para a pasta de destino colocando o nome o número da OS
                                File.Move(Ler_ficheiros.ToString(), Pasta_Destino + "\\" + Barcode[0].ToString() + "_OS" + Tipo_Ficheiro);

                            }

                            Conta_OS_Sucesso++;
                            //apaga o ficheiro tif que foi digitalizado 
                            File.Delete(Ler_ficheiros);
                            //apaga o ficheiro pdf com as marcas de texto da api SautinSoft que vai servir para converter o ficheiro tif
                            File.Delete(Pasta_Destino + "\\" + Barcode[0].ToString() + ".pdf");
                        }
                        else
                        {//caso não existam ficheiros com extensão tif saí da aplicação
                            break;
                        }
                    }
                    catch (ArgumentException exception_barcode_invalido)
                    {
                        //caso exista algum erro no processo será despultada uma mensagem de aviso com o erro
                        Mensagem_Erro = "Erro: " + exception_barcode_invalido.Message + " Barcode com formato incorreto, ficheiro: " + Ultimo_Ficheiro_Executado + ".";
                        //recolhe o timestamp no momento em que o erro ocorre 
                        Data_Hora = DateTime.Now.ToString("dd_MM_yyyy__hh_mm_ss_fff_tt");
                        posicao_logs_erro++;
                        array_mensagens_logs_erro[posicao_logs_erro] = "Date: " + Data_Hora + " | Error: " + exception_barcode_invalido.Message + " | Barcode com formato incorreto, no ficheiro " + Ultimo_Ficheiro_Executado + ". - ERROR";
                        if (File.Exists(Ler_ficheiros))
                        {
                            //move o ficheiro para a pasta de erros colocando o timestamp seguido do tipo de ficheiro
                            File.Move(Ler_ficheiros.ToString(), Pasta_Erros + "\\" + Data_Hora + Tipo_Ficheiro);
                        }
                        //envia email para helpdesk
                        // Send_Email(Mensagem_Erro, Pasta_Erros, Data_Hora);
                        //escrita do erro nos logs com a mensaguem de erro e o nome do ficheiro 
                        StreamWriter writer_Logs_Erro_invalido = new StreamWriter(Localizacao_Logs_Erro_invalido, true);
                        writer_Logs_Erro_invalido.WriteLine("Date: " + Data_Hora + " | Error: " + exception_barcode_invalido.Message + " | Barcode com formato incorreto, no ficheiro " + Ultimo_Ficheiro_Executado + ". - ERROR");
                        //limpa da memória os dados escritos    
                        writer_Logs_Erro_invalido.Close();
                        //permite escrever no objeto (textbox dentro da Thread atual)
                        MSG_LOGS.Invoke((MethodInvoker)delegate
                        {
                            MSG_LOGS.AppendText(Environment.NewLine);
                            //escreve a mensagem na textbox
                            MSG_LOGS.AppendText(Mensagem_Erro);
                            //adiciona uma nova linha na textbox
                            MSG_LOGS.AppendText(Environment.NewLine);
                        });
                        Conta_OS_Erros++;
                    }
                    catch (Exception Execao_Geral)
                    {
                        //caso exista algum erro no processo será despultada uma mensagem de aviso com o erro
                        Mensagem_Erro = "Erro: " + Execao_Geral.Message + " | Execao_Geral | Ficheiro: " + Ultimo_Ficheiro_Executado + ".";
                        //recolhe o timestamp atual no momento em que o erro ocorre 
                        Data_Hora = DateTime.Now.ToString("dd_MM_yyyy__hh_mm_ss_fff_tt");
                        posicao_logs_erro++;
                        array_mensagens_logs_erro[posicao_logs_erro] = "Date: " + Data_Hora + " | Error: " + Execao_Geral.Message + " | Barcode com formato incorreto, no ficheiro " + Ultimo_Ficheiro_Executado + ". - ERROR";
                        if (File.Exists(Ler_ficheiros))
                        {
                            //move o ficheiro para a pasta de erros colocando o timestamp seguido do tipo de ficheiro
                            File.Move(Ler_ficheiros, Pasta_Erros + "\\" + Data_Hora + Tipo_Ficheiro);
                        }
                        //envia email para helpdesk
                        // Send_Email(Mensagem_Erro, Pasta_Erros, Data_Hora);
                        //escrita do erro nos logs 
                        StreamWriter writer_Logs_Erro_Geral = new StreamWriter(Localizacao_Logs_Erro_Geral, true);
                        writer_Logs_Erro_Geral.WriteLine("Date: " + Data_Hora + " | Error: " + Execao_Geral.Message + " | Ficheiro: " + Ultimo_Ficheiro_Executado + ". - ERROR");
                        //limpa da memória os dados escritos   
                        writer_Logs_Erro_Geral.Close();
                        //permite escrever no objeto (textbox dentro da Thread atual)    
                        MSG_LOGS.Invoke((MethodInvoker)delegate
                        {
                            MSG_LOGS.AppendText(Environment.NewLine);
                            //escreve na textbox a mensagem de erro
                            MSG_LOGS.AppendText(Mensagem_Erro);
                            //adiciona uma linha na textbox 
                            MSG_LOGS.AppendText(Environment.NewLine);
                        });
                        Conta_OS_Erros++;
                    }

                }
                for (int i = 0; i <= posicao_logs; i++)
                {
                    //StreamWriter writer_Logs_Sucesso = new StreamWriter(Localizacao_Logs_Sucesso, true);
                    //writer_Logs_Sucesso.WriteLine(array_mensagens_logs[i]);
                    //writer_Logs_Sucesso.Dispose();
                    File.AppendAllText(Localizacao_Logs_Sucesso, array_mensagens_logs[i]);
                    File.AppendAllText(Localizacao_Logs_Sucesso, Environment.NewLine);
                }
                for (int i = 0; i <= posicao_logs_erro; i++)
                {
                    //StreamWriter writer_Logs_Sucesso = new StreamWriter(Localizacao_Logs_Sucesso, true);
                    //writer_Logs_Sucesso.WriteLine(array_mensagens_logs[i]);
                    //writer_Logs_Sucesso.Dispose();
                    File.AppendAllText(Localizacao_Logs_Erro_Geral, array_mensagens_logs_erro[i]);
                    File.AppendAllText(Localizacao_Logs_Erro_Geral, Environment.NewLine);
                }

                //permite escrever no objeto (textbox dentro da Thread atual)    
                MSG_LOGS.Invoke((MethodInvoker)delegate
                  {
                      MSG_LOGS.AppendText(Environment.NewLine);
                      //mensagem de estado com número de ficheiros corretamente processados 
                      MSG_LOGS.AppendText("Foram processados " + Conta_OS_Sucesso.ToString() + " ficheiros com sucesso");
                      MSG_LOGS.AppendText(Environment.NewLine);
                      //mensagem de estado com número de erros 
                      MSG_LOGS.AppendText("Foram processados " + Conta_OS_Erros.ToString() + " ficheiros com erro");
                      MSG_LOGS.AppendText(Environment.NewLine);
                      //mensagem de estado com o total de erros processados 
                      Conta_Total_Processados = Conta_OS_Erros + Conta_OS_Sucesso;
                      MSG_LOGS.AppendText("Foram processados " + Conta_Total_Processados.ToString() + " ficheiros no total");
                      MSG_LOGS.AppendText(Environment.NewLine);
                      Pausa.Visible = true;
                      label_Encerrar.Visible = true;
                  });
                Thread.Sleep(5000);
                Application.Exit();
            });
            thread.Start();
        }
        private void Pausa_Click(object sender, EventArgs e)
        {
            thread.Abort();

        }


    }
}