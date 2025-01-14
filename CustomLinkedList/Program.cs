using System.Collections;

var linkedList = new CustomLinkedList<string>();

linkedList.AddToFront("a");
linkedList.AddToEnd("b");
linkedList.AddToEnd("c");

var str = new String[3];
linkedList.CopyTo(str, 0);

foreach (var item in linkedList)
    Console.WriteLine(item);


Console.ReadKey();


public interface ILinkedList<T> : ICollection<T>
{
    void AddToFront(T? item);
    void AddToEnd(T? item);
}

public class CustomLinkedList<T> : ILinkedList<T?>
{
    private Node? _head;
    private int _count;

    public int Count => _count;
    public bool IsReadOnly => false;

    public void Add(T? item)
    {
        AddToEnd(item);
    }

    public void AddToEnd(T? item)
    {
        if (_head is null)
        {
            _head = new Node(item, null);
        }
        else
        {
            var newNode = new Node(item, null);

            var LastNode = GetNodes().Last();

            LastNode.NextNode = newNode;
        }
        _count++;
    }

    public void AddToFront(T? item)
    {

        var _newHead = new Node(item, nextNode: _head);

        _head = _newHead;
        _count++;

    }

    public bool Remove(T? item)
    {
        Node? predecessor = null;

        foreach (var node in GetNodes())
        {
            if ((node.Value is null && item is null) ||
                (node.Value is not null && node.Value.Equals(item)))
            {
                if (predecessor is null)
                {
                    _head = node.NextNode;
                }
                else
                {
                    predecessor.NextNode = node.NextNode;
                }

                --_count;
                return true;
            }
            predecessor = node;
        }
        return false;
    }

    public void Clear()
    {
        var current = _head;

        while (current is not null)
        {
            Node temporary = current;
            current = current.NextNode;
            temporary.NextNode = null;
        }

        _head = null;
        _count = 0;
    }

    public bool Contains(T? item)
    {
        if (item is null)
        {
            return GetNodes().Any(node => node.Value is null);
        }
        return GetNodes().Any(node => item.Equals(node.Value));
    }

    public void CopyTo(T?[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0 || arrayIndex > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }
        if (array.Length - arrayIndex < _count)
        {
            throw new ArgumentException("Array is not long enought to store list's Values");
        }

        foreach (var node in GetNodes())
        {
            array[arrayIndex] = node.Value;
            arrayIndex++;
        }
    }

    public IEnumerator<T?> GetEnumerator()
    {
        foreach (var node in GetNodes())
            yield return node.Value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<Node> GetNodes()
    {
        Node? currNode = _head;
        while (currNode is not null)
        {
            yield return currNode;
            currNode = currNode.NextNode;
        }
    }



    private class Node
    {
        public T? Value { get; }
        public Node? NextNode { get; set; }

        public Node(T? value, Node? nextNode)
        {
            Value = value;
            NextNode = nextNode;
        }

        public override string ToString()
        {
            return $"Value is {Value}, \n" +
                $"Next node is {(NextNode is null ? "null" : NextNode.Value)}";
        }
    }
}