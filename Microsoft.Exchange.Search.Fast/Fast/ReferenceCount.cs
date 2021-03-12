using System;
using System.Threading;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000021 RID: 33
	internal class ReferenceCount<T> where T : IDisposable
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		internal ReferenceCount(T referencedObject)
		{
			if (referencedObject == null)
			{
				throw new ArgumentNullException("referencedObject");
			}
			this.referencedObject = referencedObject;
			this.referenceCount = 1;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000C529 File Offset: 0x0000A729
		public T ReferencedObject
		{
			get
			{
				return this.referencedObject;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000C531 File Offset: 0x0000A731
		internal int AddRef()
		{
			if (this.referenceCount <= 0)
			{
				throw new InvalidOperationException("AddRef");
			}
			return Interlocked.Increment(ref this.referenceCount);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C554 File Offset: 0x0000A754
		internal int Release()
		{
			if (this.referenceCount <= 0)
			{
				throw new InvalidOperationException("Release");
			}
			int num = Interlocked.Decrement(ref this.referenceCount);
			if (num == 0)
			{
				this.referencedObject.Dispose();
			}
			return num;
		}

		// Token: 0x040000E0 RID: 224
		private int referenceCount;

		// Token: 0x040000E1 RID: 225
		private T referencedObject = default(T);
	}
}
