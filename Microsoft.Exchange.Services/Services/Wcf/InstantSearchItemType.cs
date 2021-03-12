using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B2A RID: 2858
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "InstantSearchItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum InstantSearchItemType
	{
		// Token: 0x04002D2C RID: 11564
		None,
		// Token: 0x04002D2D RID: 11565
		MailItem,
		// Token: 0x04002D2E RID: 11566
		MailConversation,
		// Token: 0x04002D2F RID: 11567
		CalendarItem,
		// Token: 0x04002D30 RID: 11568
		Persona
	}
}
