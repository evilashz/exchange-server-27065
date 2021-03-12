using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200029C RID: 668
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MessageTrackingReportType
	{
		// Token: 0x04001191 RID: 4497
		public EmailAddressType Sender;

		// Token: 0x04001192 RID: 4498
		public EmailAddressType PurportedSender;

		// Token: 0x04001193 RID: 4499
		public string Subject;

		// Token: 0x04001194 RID: 4500
		public DateTime SubmitTime;

		// Token: 0x04001195 RID: 4501
		[XmlIgnore]
		public bool SubmitTimeSpecified;

		// Token: 0x04001196 RID: 4502
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] OriginalRecipients;

		// Token: 0x04001197 RID: 4503
		[XmlArrayItem("RecipientTrackingEvent", IsNullable = false)]
		public RecipientTrackingEventType[] RecipientTrackingEvents;

		// Token: 0x04001198 RID: 4504
		[XmlArrayItem(IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
