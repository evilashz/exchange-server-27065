using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003C3 RID: 963
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ConversationActionTypeType
	{
		// Token: 0x0400153E RID: 5438
		AlwaysCategorize,
		// Token: 0x0400153F RID: 5439
		AlwaysDelete,
		// Token: 0x04001540 RID: 5440
		AlwaysMove,
		// Token: 0x04001541 RID: 5441
		Delete,
		// Token: 0x04001542 RID: 5442
		Move,
		// Token: 0x04001543 RID: 5443
		Copy,
		// Token: 0x04001544 RID: 5444
		SetReadState,
		// Token: 0x04001545 RID: 5445
		SetRetentionPolicy,
		// Token: 0x04001546 RID: 5446
		Flag
	}
}
