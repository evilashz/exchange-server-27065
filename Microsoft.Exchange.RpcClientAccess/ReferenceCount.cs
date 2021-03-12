using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ReferenceCount<T> where T : IDisposable
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00003CA7 File Offset: 0x00001EA7
		internal ReferenceCount(T referencedObject)
		{
			if (referencedObject == null)
			{
				throw new ArgumentNullException("referencedObject");
			}
			this.referencedObject = referencedObject;
			this.referenceCount = 1;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003CDC File Offset: 0x00001EDC
		public T ReferencedObject
		{
			get
			{
				this.CheckReleased();
				return this.referencedObject;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003CEC File Offset: 0x00001EEC
		internal static ReferenceCount<T> Assign(T referencedObject)
		{
			bool flag = false;
			ReferenceCount<T> result;
			try
			{
				ReferenceCount<T> referenceCount = new ReferenceCount<T>(referencedObject);
				flag = true;
				result = referenceCount;
			}
			finally
			{
				if (!flag)
				{
					referencedObject.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003D2C File Offset: 0x00001F2C
		internal static void ReleaseIfPresent(ReferenceCount<T> referenceCountInstanceOrNull)
		{
			if (referenceCountInstanceOrNull != null)
			{
				referenceCountInstanceOrNull.Release();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003D38 File Offset: 0x00001F38
		internal int AddRef()
		{
			this.CheckReleased();
			return Interlocked.Increment(ref this.referenceCount);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003D4C File Offset: 0x00001F4C
		internal int Release()
		{
			this.CheckReleased();
			int num = Interlocked.Decrement(ref this.referenceCount);
			if (num == 0)
			{
				this.referencedObject.Dispose();
			}
			return num;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003D80 File Offset: 0x00001F80
		internal bool HasReleased
		{
			get
			{
				return this.referenceCount <= 0;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003D8E File Offset: 0x00001F8E
		private void CheckReleased()
		{
			if (this.referenceCount <= 0)
			{
				throw new ObjectDisposedException(base.GetType().ToString() + " has already been released.");
			}
		}

		// Token: 0x04000051 RID: 81
		private int referenceCount;

		// Token: 0x04000052 RID: 82
		private T referencedObject = default(T);
	}
}
