using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000153 RID: 339
	internal class SafeBuffer : DisposableBase.Finalizable
	{
		// Token: 0x06000AF2 RID: 2802 RVA: 0x00029237 File Offset: 0x00027437
		internal SafeBuffer(int cb)
		{
			this.buf = new byte[cb];
			if (cb >= 4)
			{
				this.gc = GCHandle.Alloc(this.buf, GCHandleType.Pinned);
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00029261 File Offset: 0x00027461
		internal SafeBuffer(byte[] b)
		{
			this.buf = b;
			if (b.Length >= 4)
			{
				this.gc = GCHandle.Alloc(this.buf, GCHandleType.Pinned);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00029288 File Offset: 0x00027488
		internal byte[] Buffer
		{
			get
			{
				return this.buf;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00029290 File Offset: 0x00027490
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Array.Clear(this.buf, 0, this.buf.Length);
			}
			if (this.gc.IsAllocated)
			{
				this.gc.Free();
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000292C1 File Offset: 0x000274C1
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SafeBuffer>(this);
		}

		// Token: 0x040005DB RID: 1499
		private byte[] buf;

		// Token: 0x040005DC RID: 1500
		private GCHandle gc;
	}
}
