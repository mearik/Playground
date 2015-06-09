using System;
using System.Collections.Generic;

namespace mearik.Fun.DataStructures
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//DemoQueue();
			DemoBST();
		}

		static void DemoQueue()
		{
			QueueWith2Stacks<int> q = new QueueWith2Stacks<int>();

			Console.WriteLine("Initialized the queue");
			Console.WriteLine("Size: " + q.Count);

			q.Enqueue(123);
			Console.WriteLine("Enqueue 123");
			Console.WriteLine("Size: " + q.Count);

			q.Enqueue(2);
			Console.WriteLine("Enqueue 2");
			Console.WriteLine("Size: " + q.Count);

			Console.WriteLine("Dequeued: " + q.Dequeue());
			Console.WriteLine("Size: " + q.Count);

		}

		static void DemoBST()
		{
			BST tree = new BST();

			tree.Insert(3);
			tree.Insert(2);
			tree.Insert(5);
			tree.Insert(6);
			tree.Insert(31);
			tree.Insert(34);
			tree.Insert(53);
			tree.Insert(9);
			tree.Insert(8);
			tree.Insert(19);
			tree.Insert(18);
			tree.Insert(1);
			tree.Insert(4);

			tree.InOrderTraversal();
			Console.WriteLine("Height: " + tree.Height());
			Console.WriteLine("Leaves: " + tree.LeafCount());
			Console.WriteLine("Diameter: " + tree.Diameter());

			tree.PrintLevelOrder();
			Console.WriteLine("IsBalanced: " + tree.IsBalanced());

			Console.WriteLine("Deleting 31");
			tree.Delete(31);

			tree.PrintLevelOrder();
			Console.ReadLine();
		}
	}
}
