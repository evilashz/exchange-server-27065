using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005E4 RID: 1508
	[XmlType(TypeName = "TransportSettingsConfigXml")]
	[Serializable]
	public sealed class TransportSettingsConfigXml : XMLSerializableBase
	{
		// Token: 0x06004639 RID: 17977 RVA: 0x001050C8 File Offset: 0x001032C8
		public TransportSettingsConfigXml()
		{
			this.QueueAggregationIntervalTicks = TransportSettingsConfigXml.DefaultQueueAggregationIntervalTicks;
			this.DiagnosticsAggregationServicePort = TransportSettingsConfigXml.DefaultDiagnosticsAggregationServicePort;
			this.AgentGeneratedMessageLoopDetectionInSubmissionEnabled = TransportSettingsConfigXml.DefaultAgentGeneratedMessageLoopDetectionInSubmissionEnabled;
			this.AgentGeneratedMessageLoopDetectionInSmtpEnabled = TransportSettingsConfigXml.DefaultAgentGeneratedMessageLoopDetectionInSmtpEnabled;
			this.MaxAllowedAgentGeneratedMessageDepth = TransportSettingsConfigXml.DefaultMaxAllowedAgentGeneratedMessageDepth;
			this.MaxAllowedAgentGeneratedMessageDepthPerAgent = TransportSettingsConfigXml.DefaultMaxAllowedAgentGeneratedMessageDepthPerAgent;
		}

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x0010511D File Offset: 0x0010331D
		// (set) Token: 0x0600463B RID: 17979 RVA: 0x00105125 File Offset: 0x00103325
		[XmlElement(ElementName = "QAIT")]
		public long QueueAggregationIntervalTicks { get; set; }

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x0010512E File Offset: 0x0010332E
		// (set) Token: 0x0600463D RID: 17981 RVA: 0x00105136 File Offset: 0x00103336
		[XmlElement(ElementName = "DASP")]
		public int DiagnosticsAggregationServicePort { get; set; }

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x0010513F File Offset: 0x0010333F
		// (set) Token: 0x0600463F RID: 17983 RVA: 0x00105147 File Offset: 0x00103347
		[XmlElement(ElementName = "AGMLDE")]
		public bool AgentGeneratedMessageLoopDetectionInSubmissionEnabled { get; set; }

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06004640 RID: 17984 RVA: 0x00105150 File Offset: 0x00103350
		// (set) Token: 0x06004641 RID: 17985 RVA: 0x00105158 File Offset: 0x00103358
		[XmlElement(ElementName = "AGMLDSMTPE")]
		public bool AgentGeneratedMessageLoopDetectionInSmtpEnabled { get; set; }

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x00105161 File Offset: 0x00103361
		// (set) Token: 0x06004643 RID: 17987 RVA: 0x00105169 File Offset: 0x00103369
		[XmlElement(ElementName = "MAAGMD")]
		public uint MaxAllowedAgentGeneratedMessageDepth { get; set; }

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x00105172 File Offset: 0x00103372
		// (set) Token: 0x06004645 RID: 17989 RVA: 0x0010517A File Offset: 0x0010337A
		[XmlElement(ElementName = "MAAGMDPA")]
		public uint MaxAllowedAgentGeneratedMessageDepthPerAgent { get; set; }

		// Token: 0x04003034 RID: 12340
		internal static readonly int DefaultDiagnosticsAggregationServicePort = 9710;

		// Token: 0x04003035 RID: 12341
		internal static readonly long DefaultQueueAggregationIntervalTicks = EnhancedTimeSpan.FromMinutes(1.0).Ticks;

		// Token: 0x04003036 RID: 12342
		internal static readonly bool DefaultAgentGeneratedMessageLoopDetectionInSubmissionEnabled = true;

		// Token: 0x04003037 RID: 12343
		internal static readonly bool DefaultAgentGeneratedMessageLoopDetectionInSmtpEnabled = true;

		// Token: 0x04003038 RID: 12344
		internal static readonly uint DefaultMaxAllowedAgentGeneratedMessageDepth = 3U;

		// Token: 0x04003039 RID: 12345
		internal static readonly uint DefaultMaxAllowedAgentGeneratedMessageDepthPerAgent = 2U;
	}
}
