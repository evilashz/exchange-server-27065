using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000AF RID: 175
	[DataContract(Name = "OfferType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum OfferType
	{
		// Token: 0x04000279 RID: 633
		[EnumMember]
		Invalid,
		// Token: 0x0400027A RID: 634
		[EnumMember]
		ExchangePlan1Edu,
		// Token: 0x0400027B RID: 635
		[EnumMember]
		E3,
		// Token: 0x0400027C RID: 636
		[EnumMember]
		E1,
		// Token: 0x0400027D RID: 637
		[EnumMember]
		P1,
		// Token: 0x0400027E RID: 638
		[EnumMember]
		K1,
		// Token: 0x0400027F RID: 639
		[EnumMember]
		EopEnterprise,
		// Token: 0x04000280 RID: 640
		[EnumMember]
		EopEnterprisePreminum
	}
}
