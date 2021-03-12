using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004A4 RID: 1188
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct COAUTHINFO
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x000A67A6 File Offset: 0x000A49A6
		public COAUTHINFO(Authn authnSvc, Authz authzSvc, AuthnLevel AuthnLevel, ImpLevel ImpersonationLevel)
		{
			this.dwAuthnSvc = authnSvc;
			this.dwAuthzSvc = authzSvc;
			this.pwszServerPrincName = null;
			this.dwAuthnLevel = AuthnLevel;
			this.dwImpersonationLevel = ImpersonationLevel;
			this.pAuthIdentityData = IntPtr.Zero;
			this.dwCapabilities = 0;
		}

		// Token: 0x04001EC1 RID: 7873
		public Authn dwAuthnSvc;

		// Token: 0x04001EC2 RID: 7874
		public Authz dwAuthzSvc;

		// Token: 0x04001EC3 RID: 7875
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pwszServerPrincName;

		// Token: 0x04001EC4 RID: 7876
		public AuthnLevel dwAuthnLevel;

		// Token: 0x04001EC5 RID: 7877
		public ImpLevel dwImpersonationLevel;

		// Token: 0x04001EC6 RID: 7878
		public IntPtr pAuthIdentityData;

		// Token: 0x04001EC7 RID: 7879
		public int dwCapabilities;
	}
}
