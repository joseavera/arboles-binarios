using System;

// ── CLASE NODO ────────────────────────────────────────────────
// Representa un elemento del árbol.
// Cada nodo guarda un valor y dos referencias a otros nodos.
class Nodo
{
    public int  Valor;       // el dato que almacena este nodo
    public Nodo Izquierdo;   // referencia al hijo izquierdo (valores menores)
    public Nodo Derecho;     // referencia al hijo derecho (valores mayores)

    // Constructor: crea el nodo con su valor.
    // Izquierdo y Derecho quedan en null automáticamente (sin hijos).
    public Nodo(int v) { Valor = v; }
}

// ── CLASE BST ─────────────────────────────────────────────────
// Árbol Binario de Búsqueda.
// Regla: izquierda < nodo < derecha, en CADA nodo del árbol.
class BST
{
    private Nodo raiz; // punto de entrada del árbol; null = árbol vacío

    // ── INSERTAR ──────────────────────────────────────────────
    // Método público: el usuario llama Insertar(valor).
    // Delega el trabajo recursivo a InsRec, actualizando la raíz.
    public void Insertar(int v) => raiz = InsRec(raiz, v);

    // Método privado recursivo.
    // Recibe el nodo actual y el valor a insertar.
    private Nodo InsRec(Nodo n, int v)
    {
        // Caso base: llegué a una posición vacía → crear el nodo aquí
        if (n == null) return new Nodo(v);

        // Si el valor es menor → bajar al subárbol izquierdo
        if (v < n.Valor) n.Izquierdo = InsRec(n.Izquierdo, v);

        // Si el valor es mayor → bajar al subárbol derecho
        else if (v > n.Valor) n.Derecho = InsRec(n.Derecho, v);

        // Si v == n.Valor: el valor ya existe, no se insertan duplicados

        // Devolver el nodo actual (con su subárbol ya actualizado)
        return n;
    }

    // ── BUSCAR ────────────────────────────────────────────────
    // Método público: devuelve true si el valor existe, false si no.
    public bool Buscar(int v) => BusRec(raiz, v);

    // Método privado recursivo.
    private bool BusRec(Nodo n, int v)
    {
        // Caso base: llegué a null → el valor no existe en el árbol
        if (n == null)    return false;

        // Caso base: encontré el valor
        if (v == n.Valor) return true;

        // Decisión binaria: si es menor busco a la izquierda,
        // si es mayor busco a la derecha.
        // Cada comparación descarta la mitad del árbol restante → O(log n)
        return v < n.Valor
            ? BusRec(n.Izquierdo, v)  // menor → izquierda
            : BusRec(n.Derecho,   v); // mayor → derecha
    }

    // ── RECORRIDOS ────────────────────────────────────────────
    // Los tres recorridos visitan los mismos nodos.
    // Lo que cambia es CUÁNDO se procesa el nodo actual:
    // antes, entre, o después de sus hijos.

    // InOrder: Izquierdo → Nodo → Derecho
    // Produce los valores en orden ASCENDENTE en un BST.
    public void InOrder()   { InRec(raiz);   Console.WriteLine(); }

    // PreOrder: Nodo → Izquierdo → Derecho
    // La raíz siempre aparece primero.
    // Útil para copiar o serializar el árbol.
    public void PreOrder()  { PreRec(raiz);  Console.WriteLine(); }

    // PostOrder: Izquierdo → Derecho → Nodo
    // La raíz siempre aparece al último.
    // Útil para eliminar nodos de forma segura.
    public void PostOrder() { PostRec(raiz); Console.WriteLine(); }

    // ── InOrder recursivo ──
    private void InRec(Nodo n)
    {
        if (n == null) return;          // caso base: nodo vacío, no hacer nada
        InRec(n.Izquierdo);            // 1. visitar subárbol izquierdo (menores)
        Console.Write(n.Valor + " ");  // 2. imprimir el nodo actual
        InRec(n.Derecho);             // 3. visitar subárbol derecho (mayores)
    }

    // ── PreOrder recursivo ──
    private void PreRec(Nodo n)
    {
        if (n == null) return;          // caso base
        Console.Write(n.Valor + " ");  // 1. imprimir el nodo actual PRIMERO
        PreRec(n.Izquierdo);           // 2. visitar subárbol izquierdo
        PreRec(n.Derecho);            // 3. visitar subárbol derecho
    }

    // ── PostOrder recursivo ──
    private void PostRec(Nodo n)
    {
        if (n == null) return;          // caso base
        PostRec(n.Izquierdo);          // 1. visitar subárbol izquierdo
        PostRec(n.Derecho);           // 2. visitar subárbol derecho
        Console.Write(n.Valor + " "); // 3. imprimir el nodo actual AL FINAL
    }
}

// ── PROGRAMA PRINCIPAL ────────────────────────────────────────
class Program
{
    static void Main()
    {
        // Crear un árbol vacío
        BST arbol = new BST();

        // Insertar los valores uno por uno.
        // El orden de inserción determina la forma del árbol:
        // 50 → raíz
        // 30 → izq de 50   (30 < 50)
        // 70 → der de 50   (70 > 50)
        // 20 → izq de 30   (20 < 50, 20 < 30)
        // 40 → der de 30   (40 < 50, 40 > 30)
        // 60 → izq de 70   (60 > 50, 60 < 70)
        // 80 → der de 70   (80 > 50, 80 > 70)
        foreach (int v in new[] { 50, 30, 70, 20, 40, 60, 80 })
            arbol.Insertar(v);

        // InOrder → siempre produce orden ascendente en BST
        Console.Write("InOrder:   "); arbol.InOrder();
        // Salida: 20 30 40 50 60 70 80

        // PreOrder → raíz primero, luego subárbol izq, luego der
        Console.Write("PreOrder:  "); arbol.PreOrder();
        // Salida: 50 30 20 40 70 60 80

        // PostOrder → hojas primero, raíz al final
        Console.Write("PostOrder: "); arbol.PostOrder();
        // Salida: 20 40 30 60 80 70 50

        // Buscar(40): existe en el árbol → True
        Console.WriteLine("Buscar(40): " + arbol.Buscar(40));

        // Buscar(99): no existe en el árbol → False
        // Recorre: 99>50→der; 99>70→der; 99>80→der; null → false
        Console.WriteLine("Buscar(99): " + arbol.Buscar(99));
    }
}
