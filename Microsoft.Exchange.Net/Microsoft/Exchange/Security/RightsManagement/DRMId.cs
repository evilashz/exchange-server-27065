using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009A2 RID: 2466
	[StructLayout(LayoutKind.Sequential)]
	internal class DRMId
	{
		// Token: 0x06003572 RID: 13682 RVA: 0x000873D5 File Offset: 0x000855D5
		public DRMId()
		{
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000873DD File Offset: 0x000855DD
		public DRMId(string idType, string id)
		{
			this.IdType = idType;
			this.Id = id;
		}

		// Token: 0x04002DBE RID: 11710
		public uint Version;

		// Token: 0x04002DBF RID: 11711
		[MarshalAs(UnmanagedType.LPWStr)]
		public string IdType;

		// Token: 0x04002DC0 RID: 11712
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Id;
	}
}
