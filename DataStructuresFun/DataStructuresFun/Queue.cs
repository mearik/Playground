using System;
using System.Collections;

namespace mearik.Fun.DataStructures
{
	/// <summary>
	/// Queue implementation using 2 stacks.
	/// </summary>
	public class QueueWith2Stacks<T>
	{
		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count {get; private set;}

		private Stack _tailStack;
		private Stack _headStack;

		/// <summary>
		/// Initializes a new instance of the <see cref="mearik.Fun.DataStructures.QueueWith2Stacks`1"/> class.
		/// </summary>
		public QueueWith2Stacks()
		{
			_tailStack = new Stack();
			_headStack = new Stack();
			this.Count = 0;
		}

		/// <summary>
		/// Enqueue the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Enqueue(T item)
		{
			_tailStack.Push(item);
			this.Count++;
		}

		/// <summary>
		/// Dequeues the first item in the queue.
		/// Throws an InvalidOperationException if the queue is empty.
		/// </summary>
		public T Dequeue() 
		{
			if (this.Count == 0) 
			{
				throw new InvalidOperationException("Queue is empty!");
			}

			if (_headStack.Count == 0) 
			{
				while (_tailStack.Count > 0)
				{
					_headStack.Push(_tailStack.Pop());
				}
			}

			this.Count--;

			return (T)_headStack.Pop();
		}
	}
}

