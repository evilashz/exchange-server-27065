using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000295 RID: 661
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum FlaggedForActionType
	{
		// Token: 0x04001166 RID: 4454
		Any,
		// Token: 0x04001167 RID: 4455
		Call,
		// Token: 0x04001168 RID: 4456
		DoNotForward,
		// Token: 0x04001169 RID: 4457
		FollowUp,
		// Token: 0x0400116A RID: 4458
		FYI,
		// Token: 0x0400116B RID: 4459
		Forward,
		// Token: 0x0400116C RID: 4460
		NoResponseNecessary,
		// Token: 0x0400116D RID: 4461
		Read,
		// Token: 0x0400116E RID: 4462
		Reply,
		// Token: 0x0400116F RID: 4463
		ReplyToAll,
		// Token: 0x04001170 RID: 4464
		Review
	}
}
