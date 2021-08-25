using System;
using System.Collections.Generic;
using System.IO;

namespace ExercicioFixacao214
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string caminhoArquivo = SolicitarCaminhoArquivo();
                ValidarExtensao(caminhoArquivo);
                var votosProcessados = ProcessarArquivo(caminhoArquivo);
                ImprimirVotos(votosProcessados);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Ocorreu uma ApplicationException: {ex.Message}, {(ex.InnerException != null ? ex.InnerException.Message : "")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma Exception: {ex.Message}, {(ex.InnerException != null ? ex.InnerException.Message : "")}");
            }
        }
        private static string SolicitarCaminhoArquivo()
        {
            try
            {
                Console.Write("Enter file full path: ");
                return Console.ReadLine();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Método: SolicitarCaminhoArquivo, erro: {ex.Message} - {(ex.InnerException != null ? ex.InnerException.Message : "")}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Método: SolicitarCaminhoArquivo, erro: { ex.Message } - { (ex.InnerException != null ? ex.InnerException.Message : "")}");
            }
        }

        private static void ValidarExtensao(string caminhoArquivo)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(caminhoArquivo);
            if (directoryInfo.Extension != ".txt")
                throw new IOException("Informe apenas arquivos com a extensão .txt!");
        }
        private static Dictionary<string, int> ProcessarArquivo(string caminhoArquivo)
        {
            try
            {
                Dictionary<string, int> votos = new Dictionary<string, int>();

                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Open))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string linha = streamReader.ReadLine();
                            var dadosVoto = linha.Split(',');
                            var candidato = dadosVoto[0];
                            var quantidadeVotos = int.Parse(dadosVoto[1]);

                            if (dadosVoto != null)
                            {
                                if (votos.ContainsKey(dadosVoto[0]))
                                {
                                    votos[candidato] += quantidadeVotos;
                                }
                                else
                                {
                                    votos[candidato] = quantidadeVotos;
                                }
                            }
                        }
                    };
                };

                return votos;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Método: ProcessarArquivo, erro: {ex.Message} - {(ex.InnerException != null ? ex.InnerException.Message : "")}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Método: ProcessarArquivo, erro: { ex.Message } - { (ex.InnerException != null ? ex.InnerException.Message : "")}");
            }
        }

        private static void ImprimirVotos(Dictionary<string, int> votosProcessados)
        {
            try
            {
                foreach (var voto in votosProcessados)
                {
                    Console.WriteLine($"{voto.Key}: {voto.Value}");
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Método: ImprimirVotos, erro: {ex.Message} - {(ex.InnerException != null ? ex.InnerException.Message : "")}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Método: ImprimirVotos, erro: { ex.Message } - { (ex.InnerException != null ? ex.InnerException.Message : "")}");
            }
        }
    }
}
