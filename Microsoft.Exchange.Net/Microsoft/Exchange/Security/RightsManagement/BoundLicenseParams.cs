using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009A0 RID: 2464
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[StructLayout(LayoutKind.Sequential)]
	internal class BoundLicenseParams
	{
		// Token: 0x04002DAA RID: 11690
		internal uint Version;

		// Token: 0x04002DAB RID: 11691
		internal uint EnablingPrincipalHandle;

		// Token: 0x04002DAC RID: 11692
		internal uint SecureStoreHandle;

		// Token: 0x04002DAD RID: 11693
		[MarshalAs(UnmanagedType.LPWStr)]
		public string WszRightsRequested;

		// Token: 0x04002DAE RID: 11694
		[MarshalAs(UnmanagedType.LPWStr)]
		public string WszRightsGroup;

		// Token: 0x04002DAF RID: 11695
		internal uint DRMIDuVersion;

		// Token: 0x04002DB0 RID: 11696
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DRMIDIdType;

		// Token: 0x04002DB1 RID: 11697
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DRMIDId;

		// Token: 0x04002DB2 RID: 11698
		internal uint AuthenticatorCount;

		// Token: 0x04002DB3 RID: 11699
		internal IntPtr RghAuthenticators;

		// Token: 0x04002DB4 RID: 11700
		[MarshalAs(UnmanagedType.LPWStr)]
		public string WszDefaultEnablingPrincipalCredentials;

		// Token: 0x04002DB5 RID: 11701
		internal uint DwFlags;
	}
}
