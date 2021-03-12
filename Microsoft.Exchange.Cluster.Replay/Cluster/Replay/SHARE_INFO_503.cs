using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200036E RID: 878
	internal struct SHARE_INFO_503
	{
		// Token: 0x04000EEB RID: 3819
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string Netname;

		// Token: 0x04000EEC RID: 3820
		internal uint Type;

		// Token: 0x04000EED RID: 3821
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string Remark;

		// Token: 0x04000EEE RID: 3822
		internal int Permissions;

		// Token: 0x04000EEF RID: 3823
		internal int Max_uses;

		// Token: 0x04000EF0 RID: 3824
		internal int Current_uses;

		// Token: 0x04000EF1 RID: 3825
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string Path;

		// Token: 0x04000EF2 RID: 3826
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string Passwd;

		// Token: 0x04000EF3 RID: 3827
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string Servername;

		// Token: 0x04000EF4 RID: 3828
		internal int Reserved;

		// Token: 0x04000EF5 RID: 3829
		internal IntPtr Security_descriptor;
	}
}
