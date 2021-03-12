using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000347 RID: 839
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetMessageTrackingReportRequestType : BaseRequestType
	{
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x000294C2 File Offset: 0x000276C2
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x000294CA File Offset: 0x000276CA
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

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x000294D3 File Offset: 0x000276D3
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x000294DB File Offset: 0x000276DB
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

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x000294E4 File Offset: 0x000276E4
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x000294EC File Offset: 0x000276EC
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

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x000294F5 File Offset: 0x000276F5
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x000294FD File Offset: 0x000276FD
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

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00029506 File Offset: 0x00027706
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x0002950E File Offset: 0x0002770E
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

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x00029517 File Offset: 0x00027717
		// (set) Token: 0x06001B30 RID: 6960 RVA: 0x0002951F File Offset: 0x0002771F
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

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00029528 File Offset: 0x00027728
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x00029530 File Offset: 0x00027730
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

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x00029539 File Offset: 0x00027739
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x00029541 File Offset: 0x00027741
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

		// Token: 0x0400121F RID: 4639
		private string scopeField;

		// Token: 0x04001220 RID: 4640
		private MessageTrackingReportTemplateType reportTemplateField;

		// Token: 0x04001221 RID: 4641
		private EmailAddressType recipientFilterField;

		// Token: 0x04001222 RID: 4642
		private string messageTrackingReportIdField;

		// Token: 0x04001223 RID: 4643
		private bool returnQueueEventsField;

		// Token: 0x04001224 RID: 4644
		private bool returnQueueEventsFieldSpecified;

		// Token: 0x04001225 RID: 4645
		private string diagnosticsLevelField;

		// Token: 0x04001226 RID: 4646
		private TrackingPropertyType[] propertiesField;
	}
}
