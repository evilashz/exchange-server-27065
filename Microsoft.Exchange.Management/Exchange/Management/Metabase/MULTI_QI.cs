using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004A2 RID: 1186
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct MULTI_QI : IDisposable
	{
		// Token: 0x060029E7 RID: 10727 RVA: 0x000A66C8 File Offset: 0x000A48C8
		public void SetIID(Guid IID)
		{
			this.pIID = Marshal.AllocCoTaskMem(Marshal.SizeOf(IID));
			Marshal.StructureToPtr(IID, this.pIID, false);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000A66F2 File Offset: 0x000A48F2
		public void Dispose()
		{
			if (this.pIID != IntPtr.Zero)
			{
				Marshal.DestroyStructure(this.pIID, typeof(Guid));
				Marshal.FreeCoTaskMem(this.pIID);
			}
		}

		// Token: 0x04001EBA RID: 7866
		private IntPtr pIID;

		// Token: 0x04001EBB RID: 7867
		public IntPtr pItf;

		// Token: 0x04001EBC RID: 7868
		public ulong hr;
	}
}
