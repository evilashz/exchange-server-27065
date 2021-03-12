using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B2B RID: 2859
	[XmlType(TypeName = "InstantSearchPerfKey", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public enum InstantSearchPerfKey
	{
		// Token: 0x04002D32 RID: 11570
		Unknown,
		// Token: 0x04002D33 RID: 11571
		ServiceCommandInvocationTimeStamp,
		// Token: 0x04002D34 RID: 11572
		InstantSearchAPIMethodInvocationTimeStamp,
		// Token: 0x04002D35 RID: 11573
		InstantSearchAPICallback,
		// Token: 0x04002D36 RID: 11574
		NotificationHandlerPayloadDeliveryTimeStamp,
		// Token: 0x04002D37 RID: 11575
		NotificationQueuedTime,
		// Token: 0x04002D38 RID: 11576
		NotificationPickupFromQueueTime,
		// Token: 0x04002D39 RID: 11577
		NotificationSerializationTime
	}
}
