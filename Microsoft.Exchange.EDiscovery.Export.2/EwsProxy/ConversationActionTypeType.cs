using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002E2 RID: 738
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ConversationActionTypeType
	{
		// Token: 0x040010EC RID: 4332
		AlwaysCategorize,
		// Token: 0x040010ED RID: 4333
		AlwaysDelete,
		// Token: 0x040010EE RID: 4334
		AlwaysMove,
		// Token: 0x040010EF RID: 4335
		Delete,
		// Token: 0x040010F0 RID: 4336
		Move,
		// Token: 0x040010F1 RID: 4337
		Copy,
		// Token: 0x040010F2 RID: 4338
		SetReadState,
		// Token: 0x040010F3 RID: 4339
		SetRetentionPolicy,
		// Token: 0x040010F4 RID: 4340
		Flag
	}
}
