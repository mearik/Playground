using System;
using System.Collections.Generic;

namespace mearik.Fun.DataStructures
{
	public class BST
	{
		private BinaryTreeNode _root;

		public BST() { }

		/// <summary>
		/// Insert the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		public void Insert(int key) 
		{
			_root = Insert(_root, key);
		}

		private BinaryTreeNode Insert(BinaryTreeNode node, int key) 
		{
			if (node == null)
			{
				return new BinaryTreeNode(key);
			}

			if(key == node.Key) 
			{
				return node;
			}

			if(key < node.Key) 
			{
				node.Left = Insert(node.Left, key);
			}
			else 
			{
				node.Right = Insert(node.Right, key);
			}
			return node;
		}

		//BUG: Deleting the root node will lose the whole tree 
		public void Delete(int key) 
		{
			if (_root == null) 
			{
				return;
			}

			if (_root.Key == key && _root.Left == null && _root.Right == null) 
			{
				_root = null;
				return;
			}

			BinaryTreeNode parent = new BinaryTreeNode();
			BinaryTreeNode node = FindNode(_root, key, ref parent);

			DeleteNode(node, parent);
		}

		private void DeleteNode(BinaryTreeNode node, BinaryTreeNode parent)
		{
			if (node == null)
			{
				return;
			}

			//case 1: no children
			if (node.Left == null && node.Right == null)
			{
				if (parent.Left == node)
					parent.Left = null;
				else
					parent.Right = null;
			}
			//case 2: one child
			else if (node.Left == null ^ node.Right == null)
			{
				BinaryTreeNode childNode = node.Left;
				if (childNode == null)
					childNode = node.Right;

				if (parent.Left != null && parent.Left == node)
					parent.Left = childNode;
				else 
					parent.Right = childNode;
			}
			else 
			{
				//case 3: two children
				BinaryTreeNode minParent;
				BinaryTreeNode minNode = FindMinimumNode(node.Right, out minParent);

				int? temp = node.Key;
				node.Key = minNode.Key;
				minNode.Key = temp;

				DeleteNode(minNode, node ?? minParent);
			}
		}

		/// <summary>
		/// Finds the minimum node from a given node
		/// </summary>
		/// <returns>The minimum node.</returns>
		/// <param name="node">root node</param>
		/// <param name="parentNode">Parent node of the minimum node.</param>
		private BinaryTreeNode FindMinimumNode(BinaryTreeNode node, out BinaryTreeNode parentNode)
		{
			parentNode = null;

			while (node != null && node.Left != null) 
			{
				parentNode = node;
				node = node.Left;
			}
			return node;
		}

	
		public bool Lookup(int key) 
		{
			BinaryTreeNode parentNode = new BinaryTreeNode();
			return (FindNode(_root, key, ref parentNode) != null);
		}

		private BinaryTreeNode FindNode(BinaryTreeNode node, int key, ref BinaryTreeNode parentNode)
		{
			if (node == null) 
			{
				parentNode = null;
				return null;
			}

			if (key == node.Key) 
			{
				if (!parentNode.Key.HasValue)
					parentNode = null;
				return node;
			}

			parentNode = node;
			if (key < node.Key)
			{
				return FindNode(node.Left, key, ref parentNode);
			}
			else 
			{
				return FindNode(node.Right, key, ref parentNode);
			}
		}

		#region Traversals

		public void PrintLevelOrder() 
		{
			Queue<BinaryTreeNode> q = new Queue<BinaryTreeNode>();

			int currentLevelCnt = 0;
			int nextLevelCnt = 0;
			if (_root != null) 
			{
				q.Enqueue(_root);
				currentLevelCnt = 1;
			}

			while (q.Count > 0) 
			{
				BinaryTreeNode node = q.Dequeue();
				Console.Write(" " + node.Key);

				if (node.Left != null) 
				{
					q.Enqueue(node.Left);
					nextLevelCnt++;
				}
				if (node.Right != null)
				{
					q.Enqueue(node.Right);
					nextLevelCnt++;
				}

				currentLevelCnt--;
				if (currentLevelCnt == 0) 
				{
					Console.WriteLine();
					currentLevelCnt = nextLevelCnt;
					nextLevelCnt = 0;
				}

			}


		}

		public void PreOrderTraversal()
		{
			PreOrderTraversal(_root);
		}


		private void PreOrderTraversal(BinaryTreeNode node) 
		{
			if (node == null) 
			{
				return;
			}
			System.Console.WriteLine(node.Key);
			PreOrderTraversal(node.Left);
			PreOrderTraversal(node.Right);
		}

		public void InOrderTraversal()
		{
			InOrderTraversal(_root);
		}

		private void InOrderTraversal(BinaryTreeNode node) 
		{
			if (node == null) 
			{
				return;
			}
			InOrderTraversal(node.Left);
			System.Console.WriteLine(node.Key);
			InOrderTraversal(node.Right);
		}



		public void PostOrderTraversal()
		{
			PostOrderTraversal(_root);
		}

		private void PostOrderTraversal(BinaryTreeNode node) 
		{
			if (node == null) 
			{
				return;
			}
			PostOrderTraversal(node.Left);
			PostOrderTraversal(node.Right);
			System.Console.WriteLine(node.Key);
		}
		#endregion

		public bool IsBalanced() 
		{
			int height = 0 ;
			return IsBalanced(_root, ref height);
		}


		private bool IsBalanced(BinaryTreeNode node, ref int height)
		{
			if (node == null) 
			{
				return true;
			}

			//int curHeight = height+1;
			int leftHeight = 0;
			int rightHeight = 0;
			bool leftBalanced = IsBalanced(node.Left, ref leftHeight);
			bool rightBalanced = IsBalanced(node.Right, ref rightHeight);
			height = Math.Max(leftHeight, rightHeight) + 1;

			if (Math.Abs(leftHeight - rightHeight) > 1) 
			{
				return false;
			}
			else {
				return (leftBalanced && rightBalanced);
			}
		}

		public int Height()
		{
			return Height(_root);	
		}

		private int Height(BinaryTreeNode node)
		{
			if (node == null)
			{
				return 0;
			}

			return 1 + Math.Max(Height(node.Left), Height(node.Right));
		}

		public int LeafCount()
		{
			Queue<BinaryTreeNode> q = new Queue<BinaryTreeNode>();
			int cnt = 0;

			if (_root != null) 
			{
				q.Enqueue(_root);
			}

			while (q.Count > 0) 
			{
				BinaryTreeNode node = q.Dequeue();
				if (node.Left == null && node.Right == null)
				{
					cnt++;	
				}
				if (node.Left != null)
				{
					q.Enqueue(node.Left);
				}
				if (node.Right != null)
				{
					q.Enqueue(node.Right);
				}
			}
			return cnt;
		}


		/// <summary>
		/// Diameter of the tree.
		/// </summary>
		public int Diameter()
		{
			return Diameter(_root);
		}

		/// <summary>
		/// Diameter the specified node. O(n^2) complexity. This can be improved.
		/// </summary>
		/// <param name="node">Node.</param>
		private int Diameter(BinaryTreeNode node)
		{
			if (node == null)
				return 0;

			int rootDiameter = this.Height(node.Left) + this.Height(node.Right) + 1;
			int leftDiameter = this.Diameter(node.Left);
			int rightDiameter = this.Diameter(node.Right);

			return Math.Max(rootDiameter, Math.Max(leftDiameter, rightDiameter));
		}
	}

	public class BinaryTreeNode 
	{
		public int? Key { get; set;}
		public BinaryTreeNode Left { get; set;}
		public BinaryTreeNode Right { get; set;}


		public BinaryTreeNode() { }

		public BinaryTreeNode(int key)
		{
			this.Key = key;
		}
	}
}

