using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200013C RID: 316
	internal class AggTrafficDatas : ConfigurablePropertyBag
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x0002670E File Offset: 0x0002490E
		public AggTrafficDatas(IEnumerable<AggTrafficData> batch)
		{
			this.TvpAggTrafficData = DalHelper.CreateDataTable("AggTrafficDataDt", AggTrafficDatas.DataTableProperties, batch);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002672C File Offset: 0x0002492C
		internal AggTrafficDatas(Guid organizationalUnitRoot, DataTable items)
		{
			this.OrganizationalUnitRoot = organizationalUnitRoot;
			this.TvpAggTrafficData = items;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00026744 File Offset: 0x00024944
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00026769 File Offset: 0x00024969
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x0002677B File Offset: 0x0002497B
		public DataTable TvpAggTrafficData
		{
			get
			{
				return (DataTable)this[AggTrafficDatas.TvpAggTrafficDataProp];
			}
			private set
			{
				this[AggTrafficDatas.TvpAggTrafficDataProp] = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00026789 File Offset: 0x00024989
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x00026791 File Offset: 0x00024991
		internal Guid OrganizationalUnitRoot { get; set; }

		// Token: 0x04000610 RID: 1552
		internal const string AggTrafficDataTable = "AggTrafficDataDt";

		// Token: 0x04000611 RID: 1553
		internal static readonly HygienePropertyDefinition TvpAggTrafficDataProp = new HygienePropertyDefinition("tvp_AggTrafficData", typeof(DataTable));

		// Token: 0x04000612 RID: 1554
		private static readonly HygienePropertyDefinition[] DataTableProperties = new HygienePropertyDefinition[]
		{
			CommonReportingSchema.OrganizationalUnitRootProperty,
			CommonReportingSchema.DomainHashKeyProp,
			CommonReportingSchema.TrafficTypeProperty,
			CommonReportingSchema.DateKeyProperty,
			CommonReportingSchema.HourKeyProperty,
			CommonReportingSchema.TenantDomainProperty,
			CommonReportingSchema.MessageCountProperty,
			CommonReportingSchema.RecipientCountProperty,
			DalHelper.HashBucketProp
		};
	}
}
