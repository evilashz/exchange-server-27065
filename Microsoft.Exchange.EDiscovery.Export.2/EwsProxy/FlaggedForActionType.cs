using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B4 RID: 436
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum FlaggedForActionType
	{
		// Token: 0x04000D14 RID: 3348
		Any,
		// Token: 0x04000D15 RID: 3349
		Call,
		// Token: 0x04000D16 RID: 3350
		DoNotForward,
		// Token: 0x04000D17 RID: 3351
		FollowUp,
		// Token: 0x04000D18 RID: 3352
		FYI,
		// Token: 0x04000D19 RID: 3353
		Forward,
		// Token: 0x04000D1A RID: 3354
		NoResponseNecessary,
		// Token: 0x04000D1B RID: 3355
		Read,
		// Token: 0x04000D1C RID: 3356
		Reply,
		// Token: 0x04000D1D RID: 3357
		ReplyToAll,
		// Token: 0x04000D1E RID: 3358
		Review
	}
}
