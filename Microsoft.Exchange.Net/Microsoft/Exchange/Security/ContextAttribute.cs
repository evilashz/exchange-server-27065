using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C70 RID: 3184
	internal enum ContextAttribute
	{
		// Token: 0x04003AED RID: 15085
		Sizes,
		// Token: 0x04003AEE RID: 15086
		Names,
		// Token: 0x04003AEF RID: 15087
		Lifespan,
		// Token: 0x04003AF0 RID: 15088
		DceInfo,
		// Token: 0x04003AF1 RID: 15089
		StreamSizes,
		// Token: 0x04003AF2 RID: 15090
		Authority = 6,
		// Token: 0x04003AF3 RID: 15091
		SessionKey = 9,
		// Token: 0x04003AF4 RID: 15092
		PackageInfo,
		// Token: 0x04003AF5 RID: 15093
		NegotiationInfo = 12,
		// Token: 0x04003AF6 RID: 15094
		UniqueBindings = 25,
		// Token: 0x04003AF7 RID: 15095
		EndpointBindings,
		// Token: 0x04003AF8 RID: 15096
		ClientSpecifiedTarget,
		// Token: 0x04003AF9 RID: 15097
		RemoteCertificate = 83,
		// Token: 0x04003AFA RID: 15098
		LocalCertificate,
		// Token: 0x04003AFB RID: 15099
		RootStore,
		// Token: 0x04003AFC RID: 15100
		IssuerListInfoEx = 89,
		// Token: 0x04003AFD RID: 15101
		ConnectionInfo,
		// Token: 0x04003AFE RID: 15102
		EapKeyBlock
	}
}
