using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000465 RID: 1125
	[DebuggerTypeProxy(typeof(Stack.StackDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Stack : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06003702 RID: 14082 RVA: 0x000D2E51 File Offset: 0x000D1051
		public Stack()
		{
			this._array = new object[10];
			this._size = 0;
			this._version = 0;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000D2E74 File Offset: 0x000D1074
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (initialCapacity < 10)
			{
				initialCapacity = 10;
			}
			this._array = new object[initialCapacity];
			this._size = 0;
			this._version = 0;
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000D2EC4 File Offset: 0x000D10C4
		public Stack(ICollection col) : this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Push(obj);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000D2F0F File Offset: 0x000D110F
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000D2F17 File Offset: 0x000D1117
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000D2F1A File Offset: 0x000D111A
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000D2F3C File Offset: 0x000D113C
		public virtual void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000D2F68 File Offset: 0x000D1168
		public virtual object Clone()
		{
			Stack stack = new Stack(this._size);
			stack._size = this._size;
			Array.Copy(this._array, 0, stack._array, 0, this._size);
			stack._version = this._version;
			return stack;
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000D2FB4 File Offset: 0x000D11B4
		public virtual bool Contains(object obj)
		{
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && this._array[size].Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000D3000 File Offset: 0x000D1200
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int i = 0;
			if (array is object[])
			{
				object[] array2 = (object[])array;
				while (i < this._size)
				{
					array2[i + index] = this._array[this._size - i - 1];
					i++;
				}
				return;
			}
			while (i < this._size)
			{
				array.SetValue(this._array[this._size - i - 1], i + index);
				i++;
			}
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000D30CB File Offset: 0x000D12CB
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.StackEnumerator(this);
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000D30D3 File Offset: 0x000D12D3
		public virtual object Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			return this._array[this._size - 1];
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000D30FC File Offset: 0x000D12FC
		public virtual object Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			this._version++;
			object[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			object result = array[num];
			this._array[this._size] = null;
			return result;
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000D3158 File Offset: 0x000D1358
		public virtual void Push(object obj)
		{
			if (this._size == this._array.Length)
			{
				object[] array = new object[2 * this._array.Length];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			object[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = obj;
			this._version++;
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000D31C7 File Offset: 0x000D13C7
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Stack Synchronized(Stack stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			return new Stack.SyncStack(stack);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000D31E0 File Offset: 0x000D13E0
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x04001817 RID: 6167
		private object[] _array;

		// Token: 0x04001818 RID: 6168
		private int _size;

		// Token: 0x04001819 RID: 6169
		private int _version;

		// Token: 0x0400181A RID: 6170
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0400181B RID: 6171
		private const int _defaultCapacity = 10;

		// Token: 0x02000B79 RID: 2937
		[Serializable]
		private class SyncStack : Stack
		{
			// Token: 0x06006C9E RID: 27806 RVA: 0x0017686C File Offset: 0x00174A6C
			internal SyncStack(Stack stack)
			{
				this._s = stack;
				this._root = stack.SyncRoot;
			}

			// Token: 0x17001285 RID: 4741
			// (get) Token: 0x06006C9F RID: 27807 RVA: 0x00176887 File Offset: 0x00174A87
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001286 RID: 4742
			// (get) Token: 0x06006CA0 RID: 27808 RVA: 0x0017688A File Offset: 0x00174A8A
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17001287 RID: 4743
			// (get) Token: 0x06006CA1 RID: 27809 RVA: 0x00176894 File Offset: 0x00174A94
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._s.Count;
					}
					return count;
				}
			}

			// Token: 0x06006CA2 RID: 27810 RVA: 0x001768DC File Offset: 0x00174ADC
			public override bool Contains(object obj)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._s.Contains(obj);
				}
				return result;
			}

			// Token: 0x06006CA3 RID: 27811 RVA: 0x00176924 File Offset: 0x00174B24
			public override object Clone()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = new Stack.SyncStack((Stack)this._s.Clone());
				}
				return result;
			}

			// Token: 0x06006CA4 RID: 27812 RVA: 0x00176978 File Offset: 0x00174B78
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._s.Clear();
				}
			}

			// Token: 0x06006CA5 RID: 27813 RVA: 0x001769C0 File Offset: 0x00174BC0
			public override void CopyTo(Array array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._s.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006CA6 RID: 27814 RVA: 0x00176A08 File Offset: 0x00174C08
			public override void Push(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._s.Push(value);
				}
			}

			// Token: 0x06006CA7 RID: 27815 RVA: 0x00176A50 File Offset: 0x00174C50
			public override object Pop()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = this._s.Pop();
				}
				return result;
			}

			// Token: 0x06006CA8 RID: 27816 RVA: 0x00176A98 File Offset: 0x00174C98
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._s.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006CA9 RID: 27817 RVA: 0x00176AE0 File Offset: 0x00174CE0
			public override object Peek()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = this._s.Peek();
				}
				return result;
			}

			// Token: 0x06006CAA RID: 27818 RVA: 0x00176B28 File Offset: 0x00174D28
			public override object[] ToArray()
			{
				object root = this._root;
				object[] result;
				lock (root)
				{
					result = this._s.ToArray();
				}
				return result;
			}

			// Token: 0x04003473 RID: 13427
			private Stack _s;

			// Token: 0x04003474 RID: 13428
			private object _root;
		}

		// Token: 0x02000B7A RID: 2938
		[Serializable]
		private class StackEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006CAB RID: 27819 RVA: 0x00176B70 File Offset: 0x00174D70
			internal StackEnumerator(Stack stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this.currentElement = null;
			}

			// Token: 0x06006CAC RID: 27820 RVA: 0x00176B9F File Offset: 0x00174D9F
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006CAD RID: 27821 RVA: 0x00176BA8 File Offset: 0x00174DA8
			public virtual bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				bool flag;
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					flag = (this._index >= 0);
					if (flag)
					{
						this.currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				flag = (num >= 0);
				if (flag)
				{
					this.currentElement = this._stack._array[this._index];
				}
				else
				{
					this.currentElement = null;
				}
				return flag;
			}

			// Token: 0x17001288 RID: 4744
			// (get) Token: 0x06006CAE RID: 27822 RVA: 0x00176C67 File Offset: 0x00174E67
			public virtual object Current
			{
				get
				{
					if (this._index == -2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006CAF RID: 27823 RVA: 0x00176CA2 File Offset: 0x00174EA2
			public virtual void Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this._index = -2;
				this.currentElement = null;
			}

			// Token: 0x04003475 RID: 13429
			private Stack _stack;

			// Token: 0x04003476 RID: 13430
			private int _index;

			// Token: 0x04003477 RID: 13431
			private int _version;

			// Token: 0x04003478 RID: 13432
			private object currentElement;
		}

		// Token: 0x02000B7B RID: 2939
		internal class StackDebugView
		{
			// Token: 0x06006CB0 RID: 27824 RVA: 0x00176CD6 File Offset: 0x00174ED6
			public StackDebugView(Stack stack)
			{
				if (stack == null)
				{
					throw new ArgumentNullException("stack");
				}
				this.stack = stack;
			}

			// Token: 0x17001289 RID: 4745
			// (get) Token: 0x06006CB1 RID: 27825 RVA: 0x00176CF3 File Offset: 0x00174EF3
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this.stack.ToArray();
				}
			}

			// Token: 0x04003479 RID: 13433
			private Stack stack;
		}
	}
}
