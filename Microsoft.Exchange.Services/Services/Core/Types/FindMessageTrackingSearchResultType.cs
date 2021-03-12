using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000807 RID: 2055
	[XmlType(TypeName = "FindMessageTrackingSearchResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class FindMessageTrackingSearchResultType
	{
		// Token: 0x06003BFA RID: 15354 RVA: 0x000D4F86 File Offset: 0x000D3186
		internal FindMessageTrackingSearchResultType()
		{
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x000D4F90 File Offset: 0x000D3190
		internal FindMessageTrackingSearchResultType(string subject, EmailAddressWrapper sender, EmailAddressWrapper[] recipients, DateTime submittedTime, string messageTrackingReportId, string previousHopServer, string firstHopServer, TrackingPropertyType[] properties)
		{
			this.Subject = subject;
			this.Sender = sender;
			this.Recipients = recipients;
			this.SubmittedTime = submittedTime;
			this.MessageTrackingReportId = messageTrackingReportId;
			this.PreviousHopServer = (string.IsNullOrEmpty(previousHopServer) ? null : previousHopServer);
			this.FirstHopServer = (string.IsNullOrEmpty(firstHopServer) ? null : firstHopServer);
			this.Properties = properties;
		}

		// Token: 0x04002122 RID: 8482
		public string Subject;

		// Token: 0x04002123 RID: 8483
		public EmailAddressWrapper Sender;

		// Token: 0x04002124 RID: 8484
		public EmailAddressWrapper PurportedSender;

		// Token: 0x04002125 RID: 8485
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressWrapper[] Recipients;

		// Token: 0x04002126 RID: 8486
		public DateTime SubmittedTime;

		// Token: 0x04002127 RID: 8487
		public string MessageTrackingReportId;

		// Token: 0x04002128 RID: 8488
		public string PreviousHopServer;

		// Token: 0x04002129 RID: 8489
		public string FirstHopServer;

		// Token: 0x0400212A RID: 8490
		[XmlArrayItem("TrackingPropertyType", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
