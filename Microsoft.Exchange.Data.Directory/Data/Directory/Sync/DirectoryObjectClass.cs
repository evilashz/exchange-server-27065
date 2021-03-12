using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008E3 RID: 2275
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum DirectoryObjectClass
	{
		// Token: 0x040047F3 RID: 18419
		Account,
		// Token: 0x040047F4 RID: 18420
		Company,
		// Token: 0x040047F5 RID: 18421
		Contact,
		// Token: 0x040047F6 RID: 18422
		Device,
		// Token: 0x040047F7 RID: 18423
		ForeignPrincipal,
		// Token: 0x040047F8 RID: 18424
		Group,
		// Token: 0x040047F9 RID: 18425
		KeyGroup,
		// Token: 0x040047FA RID: 18426
		ServicePrincipal,
		// Token: 0x040047FB RID: 18427
		SubscribedPlan,
		// Token: 0x040047FC RID: 18428
		User,
		// Token: 0x040047FD RID: 18429
		Subscription,
		// Token: 0x040047FE RID: 18430
		PublicFolder
	}
}
