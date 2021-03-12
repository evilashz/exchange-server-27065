using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000019 RID: 25
	public class SecureArray<T> : DisposeTrackableBase
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003825 File Offset: 0x00001A25
		public SecureArray(T[] array)
		{
			this.array = array;
			this.gcHandle = GCHandle.Alloc(this.array, GCHandleType.Pinned);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003846 File Offset: 0x00001A46
		public SecureArray(int arraySize) : this(new T[arraySize])
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003854 File Offset: 0x00001A54
		~SecureArray()
		{
			base.Dispose(false);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003884 File Offset: 0x00001A84
		public T[] ArrayValue
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000388C File Offset: 0x00001A8C
		public int Length()
		{
			return this.array.Length;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003896 File Offset: 0x00001A96
		protected override void InternalDispose(bool isDisposing)
		{
			Array.Clear(this.array, 0, this.array.Length);
			this.gcHandle.Free();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000038B7 File Offset: 0x00001AB7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SecureArray<T>>(this);
		}

		// Token: 0x0400006C RID: 108
		private readonly T[] array;

		// Token: 0x0400006D RID: 109
		private GCHandle gcHandle;
	}
}
