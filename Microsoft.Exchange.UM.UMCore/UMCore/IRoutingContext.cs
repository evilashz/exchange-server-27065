using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200005C RID: 92
	internal interface IRoutingContext
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003AB RID: 939
		string CallId { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003AC RID: 940
		SipRoutingHelper RoutingHelper { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003AD RID: 941
		PlatformSipUri RequestUriOfCall { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003AE RID: 942
		bool IsSecuredCall { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003AF RID: 943
		UMDialPlan DialPlan { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003B0 RID: 944
		Guid TenantGuid { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003B1 RID: 945
		string UMPodRedirectTemplate { get; }
	}
}
