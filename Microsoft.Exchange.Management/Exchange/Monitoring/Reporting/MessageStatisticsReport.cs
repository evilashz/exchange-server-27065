using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x020005A5 RID: 1445
	[Serializable]
	public sealed class MessageStatisticsReport : ConfigurableObject
	{
		// Token: 0x060032D3 RID: 13011 RVA: 0x000CF86D File Offset: 0x000CDA6D
		public MessageStatisticsReport() : base(new MessageStatisticsReportPropertyBag())
		{
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x000CF87A File Offset: 0x000CDA7A
		// (set) Token: 0x060032D5 RID: 13013 RVA: 0x000CF891 File Offset: 0x000CDA91
		public new ObjectId Identity
		{
			get
			{
				return (ObjectId)this.propertyBag[TransportReportSchema.Identity];
			}
			internal set
			{
				this.propertyBag[TransportReportSchema.Identity] = value;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000CF8A4 File Offset: 0x000CDAA4
		// (set) Token: 0x060032D7 RID: 13015 RVA: 0x000CF8BB File Offset: 0x000CDABB
		public ExDateTime StartDate
		{
			get
			{
				return (ExDateTime)this.propertyBag[TransportReportSchema.StartDate];
			}
			internal set
			{
				this.propertyBag[TransportReportSchema.StartDate] = value;
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000CF8D3 File Offset: 0x000CDAD3
		// (set) Token: 0x060032D9 RID: 13017 RVA: 0x000CF8EA File Offset: 0x000CDAEA
		public ExDateTime EndDate
		{
			get
			{
				return (ExDateTime)this.propertyBag[TransportReportSchema.EndDate];
			}
			internal set
			{
				this.propertyBag[TransportReportSchema.EndDate] = value;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000CF902 File Offset: 0x000CDB02
		// (set) Token: 0x060032DB RID: 13019 RVA: 0x000CF919 File Offset: 0x000CDB19
		public int TotalMessagesSent
		{
			get
			{
				return (int)this.propertyBag[MessageStatisticsReportSchema.TotalMessagesSent];
			}
			internal set
			{
				this.propertyBag[MessageStatisticsReportSchema.TotalMessagesSent] = value;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x000CF931 File Offset: 0x000CDB31
		// (set) Token: 0x060032DD RID: 13021 RVA: 0x000CF948 File Offset: 0x000CDB48
		public int TotalMessagesReceived
		{
			get
			{
				return (int)this.propertyBag[MessageStatisticsReportSchema.TotalMessagesReceived];
			}
			internal set
			{
				this.propertyBag[MessageStatisticsReportSchema.TotalMessagesReceived] = value;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x000CF960 File Offset: 0x000CDB60
		// (set) Token: 0x060032DF RID: 13023 RVA: 0x000CF977 File Offset: 0x000CDB77
		public int TotalMessagesSentToForeign
		{
			get
			{
				return (int)this.propertyBag[MessageStatisticsReportSchema.TotalMessagesSentToForeign];
			}
			internal set
			{
				this.propertyBag[MessageStatisticsReportSchema.TotalMessagesSentToForeign] = value;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x000CF98F File Offset: 0x000CDB8F
		// (set) Token: 0x060032E1 RID: 13025 RVA: 0x000CF9A6 File Offset: 0x000CDBA6
		public int TotalMessagesReceivedFromForeign
		{
			get
			{
				return (int)this.propertyBag[MessageStatisticsReportSchema.TotalMessagesReceivedFromForeign];
			}
			internal set
			{
				this.propertyBag[MessageStatisticsReportSchema.TotalMessagesReceivedFromForeign] = value;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000CF9BE File Offset: 0x000CDBBE
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MessageStatisticsReport.Schema;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000CF9C5 File Offset: 0x000CDBC5
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000CF9C8 File Offset: 0x000CDBC8
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04002380 RID: 9088
		private static MessageStatisticsReportSchema Schema = ObjectSchema.GetInstance<MessageStatisticsReportSchema>();
	}
}
