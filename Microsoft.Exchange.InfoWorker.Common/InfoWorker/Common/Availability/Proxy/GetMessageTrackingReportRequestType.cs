using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002ED RID: 749
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[Serializable]
	public class GetMessageTrackingReportRequestType : BaseRequestType
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x000670C5 File Offset: 0x000652C5
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x000670CD File Offset: 0x000652CD
		public string Scope
		{
			get
			{
				return this.scopeField;
			}
			set
			{
				this.scopeField = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x000670D6 File Offset: 0x000652D6
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x000670DE File Offset: 0x000652DE
		public MessageTrackingReportTemplateType ReportTemplate
		{
			get
			{
				return this.reportTemplateField;
			}
			set
			{
				this.reportTemplateField = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x000670E7 File Offset: 0x000652E7
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x000670EF File Offset: 0x000652EF
		public EmailAddressType RecipientFilter
		{
			get
			{
				return this.recipientFilterField;
			}
			set
			{
				this.recipientFilterField = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x000670F8 File Offset: 0x000652F8
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x00067100 File Offset: 0x00065300
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

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00067109 File Offset: 0x00065309
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x00067111 File Offset: 0x00065311
		public bool ReturnQueueEvents
		{
			get
			{
				return this.returnQueueEventsField;
			}
			set
			{
				this.returnQueueEventsField = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0006711A File Offset: 0x0006531A
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x00067122 File Offset: 0x00065322
		[XmlIgnore]
		public bool ReturnQueueEventsSpecified
		{
			get
			{
				return this.returnQueueEventsFieldSpecified;
			}
			set
			{
				this.returnQueueEventsFieldSpecified = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0006712B File Offset: 0x0006532B
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x00067133 File Offset: 0x00065333
		public string DiagnosticsLevel
		{
			get
			{
				return this.diagnosticsLevelField;
			}
			set
			{
				this.diagnosticsLevelField = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0006713C File Offset: 0x0006533C
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x00067144 File Offset: 0x00065344
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
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

		// Token: 0x04000E5A RID: 3674
		private string scopeField;

		// Token: 0x04000E5B RID: 3675
		private MessageTrackingReportTemplateType reportTemplateField;

		// Token: 0x04000E5C RID: 3676
		private EmailAddressType recipientFilterField;

		// Token: 0x04000E5D RID: 3677
		private string messageTrackingReportIdField;

		// Token: 0x04000E5E RID: 3678
		private bool returnQueueEventsField;

		// Token: 0x04000E5F RID: 3679
		private bool returnQueueEventsFieldSpecified;

		// Token: 0x04000E60 RID: 3680
		private string diagnosticsLevelField;

		// Token: 0x04000E61 RID: 3681
		private TrackingPropertyType[] propertiesField;
	}
}
