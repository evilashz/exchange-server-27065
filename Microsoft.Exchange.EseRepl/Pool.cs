using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000015 RID: 21
	internal class Pool<T> : IPool<T> where T : class, IPoolableObject
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000365E File Offset: 0x0000185E
		public Pool(int expectedSize)
		{
			this.freeStack = new Stack<T>();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000367C File Offset: 0x0000187C
		public virtual int FreeObjectCount
		{
			get
			{
				return this.freeStack.Count;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000368C File Offset: 0x0000188C
		public virtual bool TryReturnObject(T o)
		{
			bool result;
			lock (this.lockObj)
			{
				if (this.freeStack.Count == 0)
				{
					this.freeStack.Push(o);
				}
				else
				{
					if (!o.Preallocated)
					{
						return false;
					}
					IPoolableObject poolableObject = this.freeStack.Peek();
					if (!poolableObject.Preallocated)
					{
						T t = this.freeStack.Pop();
						IDisposable disposable = t as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					this.freeStack.Push(o);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003744 File Offset: 0x00001944
		public virtual T TryGetObject()
		{
			lock (this.lockObj)
			{
				if (this.freeStack.Count > 0)
				{
					return this.freeStack.Pop();
				}
			}
			return default(T);
		}

		// Token: 0x0400004A RID: 74
		private object lockObj = new object();

		// Token: 0x0400004B RID: 75
		private Stack<T> freeStack;
	}
}
