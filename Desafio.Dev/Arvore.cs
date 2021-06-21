namespace Desafio.Dev
{
    public class Arvore
    {
        public NoArvore Raiz { get; private set; }
        private NoArvore Atual;

        public void AddNode(string data)
        {
            if (Atual == null)
            {
                Raiz = new NoArvore(data);
                Atual = Raiz;
            }
            else
            {
                Atual = Atual.AddFilho(data);
            }
        }
    }
}
