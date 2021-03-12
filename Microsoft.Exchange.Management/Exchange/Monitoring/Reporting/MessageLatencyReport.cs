using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x020005A2 RID: 1442
	[Serializable]
	public sealed class MessageLatencyReport : ConfigurableObject
	{
		// Token: 0x060032BD RID: 12989 RVA: 0x000CF65E File Offset: 0x000CD85E
		public MessageLatencyReport() : base(new MessageLatencyReportPropertyBag())
		{
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000CF66B File Offset: 0x000CD86B
		// (set) Token: 0x060032BF RID: 12991 RVA: 0x000CF682 File Offset: 0x000CD882
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

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000CF695 File Offset: 0x000CD895
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x000CF6AC File Offset: 0x000CD8AC
		public short SlaTargetInSeconds
		{
			get
			{
				return (short)this.propertyBag[MessageLatencyReportSchema.SlaTargetInSeconds];
			}
			internal set
			{
				this.propertyBag[MessageLatencyReportSchema.SlaTargetInSeconds] = value;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000CF6C4 File Offset: 0x000CD8C4
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x000CF6DB File Offset: 0x000CD8DB
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

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000CF6F3 File Offset: 0x000CD8F3
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x000CF70A File Offset: 0x000CD90A
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

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000CF722 File Offset: 0x000CD922
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x000CF739 File Offset: 0x000CD939
		public decimal PercentOfMessageInGivenSla
		{
			get
			{
				return (decimal)this.propertyBag[MessageLatencyReportSchema.PercentOfMessageInGivenSla];
			}
			internal set
			{
				this.propertyBag[MessageLatencyReportSchema.PercentOfMessageInGivenSla] = value;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000CF751 File Offset: 0x000CD951
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MessageLatencyReport.Schema;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x000CF758 File Offset: 0x000CD958
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000CF75B File Offset: 0x000CD95B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x0400237B RID: 9083
		private static MessageLatencyReportSchema Schema = ObjectSchema.GetInstance<MessageLatencyReportSchema>();
	}
}
