using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000C2 RID: 194
	internal struct ServiceFailureActions
	{
		// Token: 0x040001E4 RID: 484
		public uint resetPeriod;

		// Token: 0x040001E5 RID: 485
		[MarshalAs(UnmanagedType.LPTStr)]
		public string rebootMessage;

		// Token: 0x040001E6 RID: 486
		[MarshalAs(UnmanagedType.LPTStr)]
		public string command;

		// Token: 0x040001E7 RID: 487
		public uint actionCount;

		// Token: 0x040001E8 RID: 488
		public IntPtr actions;
	}
}
