using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000049 RID: 73
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct RecipientInfo
	{
		// Token: 0x040000F3 RID: 243
		public string CommonName;

		// Token: 0x040000F4 RID: 244
		public string FirstName;

		// Token: 0x040000F5 RID: 245
		public string LastName;

		// Token: 0x040000F6 RID: 246
		public string DisplayName;

		// Token: 0x040000F7 RID: 247
		public string Initials;
	}
}
