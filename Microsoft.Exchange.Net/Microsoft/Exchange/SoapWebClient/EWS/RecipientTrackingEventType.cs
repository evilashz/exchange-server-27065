using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200029D RID: 669
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RecipientTrackingEventType
	{
		// Token: 0x04001199 RID: 4505
		public DateTime Date;

		// Token: 0x0400119A RID: 4506
		public EmailAddressType Recipient;

		// Token: 0x0400119B RID: 4507
		public string DeliveryStatus;

		// Token: 0x0400119C RID: 4508
		public string EventDescription;

		// Token: 0x0400119D RID: 4509
		[XmlArrayItem("String", IsNullable = false)]
		public string[] EventData;

		// Token: 0x0400119E RID: 4510
		public string Server;

		// Token: 0x0400119F RID: 4511
		[XmlElement(DataType = "nonNegativeInteger")]
		public string InternalId;

		// Token: 0x040011A0 RID: 4512
		public bool BccRecipient;

		// Token: 0x040011A1 RID: 4513
		[XmlIgnore]
		public bool BccRecipientSpecified;

		// Token: 0x040011A2 RID: 4514
		public bool HiddenRecipient;

		// Token: 0x040011A3 RID: 4515
		[XmlIgnore]
		public bool HiddenRecipientSpecified;

		// Token: 0x040011A4 RID: 4516
		public string UniquePathId;

		// Token: 0x040011A5 RID: 4517
		public string RootAddress;

		// Token: 0x040011A6 RID: 4518
		[XmlArrayItem(IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
