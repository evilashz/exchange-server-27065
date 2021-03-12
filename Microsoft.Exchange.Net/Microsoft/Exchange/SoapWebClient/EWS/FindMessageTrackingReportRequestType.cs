using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200042A RID: 1066
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindMessageTrackingReportRequestType : BaseRequestType
	{
		// Token: 0x0400167C RID: 5756
		public string Scope;

		// Token: 0x0400167D RID: 5757
		public string Domain;

		// Token: 0x0400167E RID: 5758
		public EmailAddressType Sender;

		// Token: 0x0400167F RID: 5759
		public EmailAddressType PurportedSender;

		// Token: 0x04001680 RID: 5760
		public EmailAddressType Recipient;

		// Token: 0x04001681 RID: 5761
		public string Subject;

		// Token: 0x04001682 RID: 5762
		public DateTime StartDateTime;

		// Token: 0x04001683 RID: 5763
		[XmlIgnore]
		public bool StartDateTimeSpecified;

		// Token: 0x04001684 RID: 5764
		public DateTime EndDateTime;

		// Token: 0x04001685 RID: 5765
		[XmlIgnore]
		public bool EndDateTimeSpecified;

		// Token: 0x04001686 RID: 5766
		public string MessageId;

		// Token: 0x04001687 RID: 5767
		public EmailAddressType FederatedDeliveryMailbox;

		// Token: 0x04001688 RID: 5768
		public string DiagnosticsLevel;

		// Token: 0x04001689 RID: 5769
		public string ServerHint;

		// Token: 0x0400168A RID: 5770
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
