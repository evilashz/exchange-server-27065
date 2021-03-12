using System;
using System.Threading;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200001C RID: 28
	internal sealed class ConcurrentLookAside<T> where T : class
	{
		// Token: 0x06000267 RID: 615 RVA: 0x000057C7 File Offset: 0x000039C7
		private ConcurrentLookAside()
		{
			this.lookAsideList = new T[64];
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000057DC File Offset: 0x000039DC
		public T Get()
		{
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			for (int i = 0; i < this.lookAsideList.Length; i++)
			{
				int num = (i + currentManagedThreadId) % this.lookAsideList.Length;
				T t = this.lookAsideList[num];
				if (t != null && object.ReferenceEquals(t, Interlocked.CompareExchange<T>(ref this.lookAsideList[num], default(T), t)))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00005860 File Offset: 0x00003A60
		public bool Put(T unusedObject)
		{
			if (unusedObject != null)
			{
				int currentManagedThreadId = Environment.CurrentManagedThreadId;
				for (int i = 0; i < this.lookAsideList.Length; i++)
				{
					int num = (i + currentManagedThreadId) % this.lookAsideList.Length;
					if (this.lookAsideList[num] == null && Interlocked.CompareExchange<T>(ref this.lookAsideList[num], unusedObject, default(T)) == null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000307 RID: 775
		internal const int DefaultLookAsideSize = 64;

		// Token: 0x04000308 RID: 776
		private readonly T[] lookAsideList;

		// Token: 0x04000309 RID: 777
		internal static readonly ConcurrentLookAside<T> Pool = new ConcurrentLookAside<T>();
	}
}
