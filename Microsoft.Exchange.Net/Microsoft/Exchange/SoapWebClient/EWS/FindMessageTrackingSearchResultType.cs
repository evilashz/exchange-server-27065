using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002A1 RID: 673
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FindMessageTrackingSearchResultType
	{
		// Token: 0x040011AF RID: 4527
		public string Subject;

		// Token: 0x040011B0 RID: 4528
		public EmailAddressType Sender;

		// Token: 0x040011B1 RID: 4529
		public EmailAddressType PurportedSender;

		// Token: 0x040011B2 RID: 4530
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] Recipients;

		// Token: 0x040011B3 RID: 4531
		public DateTime SubmittedTime;

		// Token: 0x040011B4 RID: 4532
		public string MessageTrackingReportId;

		// Token: 0x040011B5 RID: 4533
		public string PreviousHopServer;

		// Token: 0x040011B6 RID: 4534
		public string FirstHopServer;

		// Token: 0x040011B7 RID: 4535
		[XmlArrayItem(IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
