using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop
{
	// Token: 0x02000019 RID: 25
	internal class PinHelper : IDisposable
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00003B4B File Offset: 0x00001D4B
		public PinHelper(object o)
		{
			this._handle = GCHandle.Alloc(o, GCHandleType.Pinned);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003B60 File Offset: 0x00001D60
		public IntPtr Addr
		{
			get
			{
				return this._handle.AddrOfPinnedObject();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003B70 File Offset: 0x00001D70
		~PinHelper()
		{
			this.Dispose(false);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003BAF File Offset: 0x00001DAF
		protected virtual void Dispose(bool disposing)
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
		}

		// Token: 0x04000078 RID: 120
		private GCHandle _handle;
	}
}
