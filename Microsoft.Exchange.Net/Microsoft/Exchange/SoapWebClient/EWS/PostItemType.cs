using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000241 RID: 577
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class PostItemType : ItemType
	{
		// Token: 0x04000F40 RID: 3904
		[XmlElement(DataType = "base64Binary")]
		public byte[] ConversationIndex;

		// Token: 0x04000F41 RID: 3905
		public string ConversationTopic;

		// Token: 0x04000F42 RID: 3906
		public SingleRecipientType From;

		// Token: 0x04000F43 RID: 3907
		public string InternetMessageId;

		// Token: 0x04000F44 RID: 3908
		public bool IsRead;

		// Token: 0x04000F45 RID: 3909
		[XmlIgnore]
		public bool IsReadSpecified;

		// Token: 0x04000F46 RID: 3910
		public DateTime PostedTime;

		// Token: 0x04000F47 RID: 3911
		[XmlIgnore]
		public bool PostedTimeSpecified;

		// Token: 0x04000F48 RID: 3912
		public string References;

		// Token: 0x04000F49 RID: 3913
		public SingleRecipientType Sender;
	}
}
