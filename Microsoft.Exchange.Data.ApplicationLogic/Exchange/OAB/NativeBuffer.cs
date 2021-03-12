using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000146 RID: 326
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NativeBuffer : DisposeTrackableBase
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x00038087 File Offset: 0x00036287
		public NativeBuffer(int size)
		{
			if (size < 0)
			{
				throw new ArgumentException("size");
			}
			this.size = size;
			this.buffer = Marshal.AllocHGlobal(size);
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000380B1 File Offset: 0x000362B1
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x000380B9 File Offset: 0x000362B9
		public IntPtr Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000380C1 File Offset: 0x000362C1
		public void CopyIn(byte[] data)
		{
			if (data.Length > this.size)
			{
				throw new ArgumentException("inputData > bufferSize");
			}
			Marshal.Copy(data, 0, this.buffer, data.Length);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000380EC File Offset: 0x000362EC
		public byte[] CopyOut(int dataSize)
		{
			if (dataSize > this.size)
			{
				throw new ArgumentException("dataSize > bufferSize");
			}
			byte[] array = new byte[dataSize];
			Marshal.Copy(this.buffer, array, 0, dataSize);
			return array;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00038123 File Offset: 0x00036323
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.buffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.buffer);
				this.buffer = IntPtr.Zero;
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00038150 File Offset: 0x00036350
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NativeBuffer>(this);
		}

		// Token: 0x0400070F RID: 1807
		private readonly int size;

		// Token: 0x04000710 RID: 1808
		private IntPtr buffer;
	}
}
