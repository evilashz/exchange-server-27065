using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001B3 RID: 435
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum RetentionActionType
	{
		// Token: 0x04000A26 RID: 2598
		None,
		// Token: 0x04000A27 RID: 2599
		MoveToDeletedItems,
		// Token: 0x04000A28 RID: 2600
		MoveToFolder,
		// Token: 0x04000A29 RID: 2601
		DeleteAndAllowRecovery,
		// Token: 0x04000A2A RID: 2602
		PermanentlyDelete,
		// Token: 0x04000A2B RID: 2603
		MarkAsPastRetentionLimit,
		// Token: 0x04000A2C RID: 2604
		MoveToArchive
	}
}
