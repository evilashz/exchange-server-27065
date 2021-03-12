using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C0 RID: 448
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindMessageTrackingSearchResultType
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0002523E File Offset: 0x0002343E
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x00025246 File Offset: 0x00023446
		public string Subject
		{
			get
			{
				return this.subjectField;
			}
			set
			{
				this.subjectField = value;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0002524F File Offset: 0x0002344F
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x00025257 File Offset: 0x00023457
		public EmailAddressType Sender
		{
			get
			{
				return this.senderField;
			}
			set
			{
				this.senderField = value;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00025260 File Offset: 0x00023460
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x00025268 File Offset: 0x00023468
		public EmailAddressType PurportedSender
		{
			get
			{
				return this.purportedSenderField;
			}
			set
			{
				this.purportedSenderField = value;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00025271 File Offset: 0x00023471
		// (set) Token: 0x06001345 RID: 4933 RVA: 0x00025279 File Offset: 0x00023479
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] Recipients
		{
			get
			{
				return this.recipientsField;
			}
			set
			{
				this.recipientsField = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00025282 File Offset: 0x00023482
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x0002528A File Offset: 0x0002348A
		public DateTime SubmittedTime
		{
			get
			{
				return this.submittedTimeField;
			}
			set
			{
				this.submittedTimeField = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00025293 File Offset: 0x00023493
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x0002529B File Offset: 0x0002349B
		public string MessageTrackingReportId
		{
			get
			{
				return this.messageTrackingReportIdField;
			}
			set
			{
				this.messageTrackingReportIdField = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000252A4 File Offset: 0x000234A4
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x000252AC File Offset: 0x000234AC
		public string PreviousHopServer
		{
			get
			{
				return this.previousHopServerField;
			}
			set
			{
				this.previousHopServerField = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x000252B5 File Offset: 0x000234B5
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x000252BD File Offset: 0x000234BD
		public string FirstHopServer
		{
			get
			{
				return this.firstHopServerField;
			}
			set
			{
				this.firstHopServerField = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x000252C6 File Offset: 0x000234C6
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x000252CE File Offset: 0x000234CE
		[XmlArrayItem(IsNullable = false)]
		public TrackingPropertyType[] Properties
		{
			get
			{
				return this.propertiesField;
			}
			set
			{
				this.propertiesField = value;
			}
		}

		// Token: 0x04000D5D RID: 3421
		private string subjectField;

		// Token: 0x04000D5E RID: 3422
		private EmailAddressType senderField;

		// Token: 0x04000D5F RID: 3423
		private EmailAddressType purportedSenderField;

		// Token: 0x04000D60 RID: 3424
		private EmailAddressType[] recipientsField;

		// Token: 0x04000D61 RID: 3425
		private DateTime submittedTimeField;

		// Token: 0x04000D62 RID: 3426
		private string messageTrackingReportIdField;

		// Token: 0x04000D63 RID: 3427
		private string previousHopServerField;

		// Token: 0x04000D64 RID: 3428
		private string firstHopServerField;

		// Token: 0x04000D65 RID: 3429
		private TrackingPropertyType[] propertiesField;
	}
}
