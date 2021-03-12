using System;
using System.Net;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001BC RID: 444
	internal class AggInboundSpamDataHistory : ConfigurablePropertyBag
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00038C7F File Offset: 0x00036E7F
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x00038C91 File Offset: 0x00036E91
		public IPAddress IPAddress
		{
			get
			{
				return (IPAddress)this[AggInboundIPSchema.IPAddressProperty];
			}
			set
			{
				this[AggInboundIPSchema.IPAddressProperty] = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00038C9F File Offset: 0x00036E9F
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00038CB1 File Offset: 0x00036EB1
		public DateTime AggregationDate
		{
			get
			{
				return (DateTime)this[AggInboundIPSchema.AggregationDateProperty];
			}
			set
			{
				this[AggInboundIPSchema.AggregationDateProperty] = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00038CC4 File Offset: 0x00036EC4
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00038CD6 File Offset: 0x00036ED6
		public double SpamPercentage
		{
			get
			{
				return (double)this[AggInboundIPSchema.SpamPercentageProperty];
			}
			set
			{
				this[AggInboundIPSchema.SpamPercentageProperty] = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00038CE9 File Offset: 0x00036EE9
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x00038CFB File Offset: 0x00036EFB
		public long TotalSpam
		{
			get
			{
				return (long)this[AggInboundIPSchema.SpamMessageCountProperty];
			}
			set
			{
				this[AggInboundIPSchema.SpamMessageCountProperty] = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00038D10 File Offset: 0x00036F10
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.identity.ToString());
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00038D36 File Offset: 0x00036F36
		public override Type GetSchemaType()
		{
			return typeof(AggInboundIPSchema);
		}

		// Token: 0x040008EF RID: 2287
		private readonly Guid identity = ReportingSession.GenerateNewId();
	}
}
