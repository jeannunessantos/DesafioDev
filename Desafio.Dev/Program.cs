using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Desafio.Dev
{
    class Program
    {
        static void Main(string[] args)
        {
            ConstruirArvore();
        }

        private static void ConstruirArvore()
        {
            var source = "[[\"A\", \"C\"], [\"B\", \"D\"], [\"B\", \"G\"], [\"C\", \"E\"], [\"C\", \"H\"], [\"E\", \"F\"],[\"A\", \"B\"]]";
            source = Regex.Replace(source, @"\s+", "");

            var list = new List<Item>();
            var arrayItems = source
                .Replace("[[", "")
                .Replace("]]", "")
                .Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in arrayItems)
            {
                var partes = item.Split(new[] { ",\"" }, StringSplitOptions.RemoveEmptyEntries);
                var chave = partes.First().Replace("\"", "");
                var valor = partes.Last().Replace("\"", "");
                list.Add(new Item { Pai = chave, Filho = valor });
            }

            var listGroup = list
                .OrderBy(p => p.Pai)
                .ThenBy(f => f.Filho)
                .GroupBy(p => p.Pai)
                .ToList();

            ValidarRegras(listGroup);

            var tree = new Arvore();
            foreach (var data in listGroup)
            {
                foreach (var item in data)
                {
                    if (tree.Raiz == null)
                    {
                        tree.AddNode(item.Pai);
                        tree.AddNode(item.Filho);
                    }
                    else
                    {
                        tree.AddNode(item.Filho);
                    }
                }
            }
            Console.WriteLine(JsonConvert.SerializeObject(tree, Formatting.Indented));
            Console.ReadKey();
        }

        private static void ValidarRegras(List<IGrouping<string, Item>> lstGroup)
        {
            var contemMaisDeDoisFilhos = lstGroup.Select(g => g.Count()).Any(c => c > 2);
            if (contemMaisDeDoisFilhos) throw new Exception("Erro: Mais de 2 filhos");
            if (false) throw new Exception("Erro: Ciclo presente");         //ToDo:Implementar regras
            if (false) throw new Exception("Erro: Raízes múltiplas");       //ToDo:Implementar regras
            if (false) throw new Exception("Erro: Qualquer outro erro");    //ToDo:Implementar regras
        }
    }
}