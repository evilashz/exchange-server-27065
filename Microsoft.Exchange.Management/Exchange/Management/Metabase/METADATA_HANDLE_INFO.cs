using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004C4 RID: 1220
	[StructLayout(LayoutKind.Sequential)]
	internal class METADATA_HANDLE_INFO
	{
		// Token: 0x06002A78 RID: 10872 RVA: 0x000AABD3 File Offset: 0x000A8DD3
		private METADATA_HANDLE_INFO()
		{
			this.dwMDPermissions = 0;
			this.dwMDSystemChangeNumber = 0;
		}

		// Token: 0x04001FB5 RID: 8117
		internal int dwMDPermissions;

		// Token: 0x04001FB6 RID: 8118
		internal int dwMDSystemChangeNumber;
	}
}
