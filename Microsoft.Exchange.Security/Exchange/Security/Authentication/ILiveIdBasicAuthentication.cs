using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200005F RID: 95
	internal interface ILiveIdBasicAuthentication
	{
		// Token: 0x060002DD RID: 733
		LiveIdAuthResult SyncADPassword(string puid, byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, bool syncHrd);

		// Token: 0x060002DE RID: 734
		SecurityStatus GetWindowsIdentity(byte[] userBytes, byte[] passBytes, out WindowsIdentity identity, out IAccountValidationContext accountValidationContext);

		// Token: 0x060002DF RID: 735
		SecurityStatus GetCommonAccessToken(byte[] userBytes, byte[] passBytes, Guid requestId, out string commonAccessToken, out IAccountValidationContext accountValidationContext);

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002E0 RID: 736
		string LastRequestErrorMessage { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002E1 RID: 737
		LiveIdAuthResult LastAuthResult { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002E2 RID: 738
		// (set) Token: 0x060002E3 RID: 739
		string ApplicationName { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002E4 RID: 740
		// (set) Token: 0x060002E5 RID: 741
		string UserIpAddress { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002E6 RID: 742
		// (set) Token: 0x060002E7 RID: 743
		bool AllowLiveIDOnlyAuth { get; set; }
	}
}
