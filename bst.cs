using System;

class Nodo
{
    public int  Valor;
    public Nodo Izquierdo, Derecho;
    public Nodo(int v) { Valor = v; }
}

class BST
{
    private Nodo raiz;

    public void Insertar(int v) => raiz = Ins(raiz, v);

    private Nodo Ins(Nodo n, int v)
    {
        if (n == null) return new Nodo(v);
        if (v < n.Valor) n.Izquierdo = Ins(n.Izquierdo, v);
        else if (v > n.Valor) n.Derecho = Ins(n.Derecho, v);
        return n;
    }

    public void InOrder()
    {
        IO(raiz);
        Console.WriteLine();
    }

    private void IO(Nodo n)
    {
        if (n == null) return;
        IO(n.Izquierdo);
        Console.Write(n.Valor + " ");
        IO(n.Derecho);
    }
}

class Program
{
    static void Main()
    {
        BST arbol = new BST();

        int[] valores = { 50, 30, 70, 20, 40, 60, 80 };

        Console.WriteLine("Insertando: 50, 30, 70, 20, 40, 60, 80");
        foreach (int v in valores)
            arbol.Insertar(v);

        Console.Write("InOrder: ");
        arbol.InOrder();
        // Salida: 20 30 40 50 60 70 80  (orden ascendente)
    }
}
