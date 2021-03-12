using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Implementation
{
	// Token: 0x02000227 RID: 551
	[StructLayout(LayoutKind.Auto)]
	internal struct GCHandleCollection : IDisposable
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x000151EC File Offset: 0x000133EC
		public void Dispose()
		{
			if (this.handles != null)
			{
				foreach (GCHandle gchandle in this.handles)
				{
					gchandle.Free();
				}
				this.handles = null;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00015250 File Offset: 0x00013450
		public IntPtr Add(object value)
		{
			if (value == null)
			{
				return IntPtr.Zero;
			}
			if (this.handles == null)
			{
				this.handles = new List<GCHandle>();
			}
			GCHandle item = GCHandle.Alloc(value, GCHandleType.Pinned);
			this.handles.Add(item);
			return item.AddrOfPinnedObject();
		}

		// Token: 0x0400033F RID: 831
		private List<GCHandle> handles;
	}
}
