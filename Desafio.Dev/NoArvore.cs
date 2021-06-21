using static Desafio.Dev.Enumerador;

namespace Desafio.Dev
{
    public class NoArvore
    {
        private readonly NoArvore parent;
        private Direcao ultimoCaminho;

        public string Data { get; set; }
        public NoArvore Esquerda { get; set; }
        public NoArvore Direita { get; set; }

        public NoArvore(string data, NoArvore parent = null)
        {
            Data = data;
            this.parent = parent;
        }

        public NoArvore AddFilho(string data)
        {
            if (Esquerda == null)
            {
                ultimoCaminho = Direcao.Esquerda;
                Esquerda = new NoArvore(data, this);
                return this;
            }
            if (Direita == null)
            {
                ultimoCaminho = Direcao.Direita;
                Direita = new NoArvore(data, this);
                return parent ?? this;
            }

            if (ultimoCaminho == Direcao.Parent || parent == null && ultimoCaminho == Direcao.Direita)
            {
                ultimoCaminho = Direcao.Esquerda;
                return Esquerda.AddFilho(data);
            }

            if (ultimoCaminho == Direcao.Esquerda)
            {
                ultimoCaminho = Direcao.Direita;
                return Direita.AddFilho(data);
            }

            ultimoCaminho = Direcao.Parent;
            return parent?.AddFilho(data);
        }
    }
}
