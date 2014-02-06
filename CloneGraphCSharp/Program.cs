using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneGraphCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Node original = new Node(1);
            original.Children.Add(new Node(2));
            original.Children.Add(new Node(3));

            Node commonChild = new Node(4);

            original.Children[0].Children.Add(commonChild);
            original.Children[1].Children.Add(commonChild);

            Node copiedNode = Clone(original);

            copiedNode.Display();
        }

        static Dictionary<Node, Node> cache = new Dictionary<Node, Node>();

        static Node Clone(Node n)
        {
            Node cloned = GetClonedNode(n);

            foreach (Node child in n.Children)
            {
                Node clonedChild = Clone(child);
                cloned.Children.Add(clonedChild);
            }

            return cloned;
        }

       static Node GetClonedNode(Node n)
        {
            Node clonedNode = null;
            if (cache.ContainsKey(n))
            {
                clonedNode = cache[n];
            }
            else
            {
                clonedNode = CloneNodeWithValue(n);
                cache.Add(n, clonedNode);
            }

            return clonedNode;
        }

        static private Node CloneNodeWithValue(Node n)
        {
            Node clonedNode = new Node(n.Data);
            clonedNode.IsVisited = n.IsVisited;
            return clonedNode;
        }
    }

    class Node
    {
        public Node(int data)
        {
            Children = new List<Node>();
            Data = data;
        }

        public int Data { get; set; }

        public bool IsVisited { get; set; }

        public List<Node> Children { get; set; }

        // do bfs
        public void Display()
        {
            Queue<Node> queue = new Queue<Node>();

            this.IsVisited = true;

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Node node = queue.Peek();

                queue.Dequeue();

                Console.WriteLine(node.Data);

                foreach (var child in node.Children)
                {
                    if (!child.IsVisited)
                    {
                        queue.Enqueue(child);
                        child.IsVisited = true;
                    }
                }
            }
        }
    }
}
