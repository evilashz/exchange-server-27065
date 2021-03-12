using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000809 RID: 2057
	[XmlType(TypeName = "MessageTrackingReportType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class MessageTrackingReport
	{
		// Token: 0x04002137 RID: 8503
		public EmailAddressWrapper Sender;

		// Token: 0x04002138 RID: 8504
		public EmailAddressWrapper PurportedSender;

		// Token: 0x04002139 RID: 8505
		public string Subject;

		// Token: 0x0400213A RID: 8506
		public DateTime SubmitTime;

		// Token: 0x0400213B RID: 8507
		[XmlArrayItem("Address")]
		public EmailAddressWrapper[] OriginalRecipients;

		// Token: 0x0400213C RID: 8508
		[XmlArrayItem("RecipientTrackingEvent", IsNullable = false)]
		public RecipientTrackingEvent[] RecipientTrackingEvents;

		// Token: 0x0400213D RID: 8509
		[XmlArrayItem("TrackingPropertyType", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
