using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004A3 RID: 1187
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct COSERVERINFO : IDisposable
	{
		// Token: 0x060029E9 RID: 10729 RVA: 0x000A6726 File Offset: 0x000A4926
		public COSERVERINFO(string serverName)
		{
			this.pAuthInfo = IntPtr.Zero;
			this.pwszName = serverName;
			this.dwReserved1 = 0U;
			this.dwReserved2 = 0U;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000A6748 File Offset: 0x000A4948
		public void SetAuthInfo(COAUTHINFO authInfo)
		{
			this.pAuthInfo = Marshal.AllocCoTaskMem(Marshal.SizeOf(authInfo));
			Marshal.StructureToPtr(authInfo, this.pAuthInfo, false);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000A6772 File Offset: 0x000A4972
		public void Dispose()
		{
			if (this.pAuthInfo != IntPtr.Zero)
			{
				Marshal.DestroyStructure(this.pAuthInfo, typeof(COAUTHINFO));
				Marshal.FreeCoTaskMem(this.pAuthInfo);
			}
		}

		// Token: 0x04001EBD RID: 7869
		private uint dwReserved1;

		// Token: 0x04001EBE RID: 7870
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pwszName;

		// Token: 0x04001EBF RID: 7871
		private IntPtr pAuthInfo;

		// Token: 0x04001EC0 RID: 7872
		private uint dwReserved2;
	}
}
