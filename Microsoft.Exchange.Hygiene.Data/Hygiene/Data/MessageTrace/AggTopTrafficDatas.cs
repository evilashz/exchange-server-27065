using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200013A RID: 314
	internal class AggTopTrafficDatas : ConfigurablePropertyBag
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x0002647A File Offset: 0x0002467A
		public AggTopTrafficDatas(IEnumerable<AggTopTrafficData> batch)
		{
			this.TvpAggTopTrafficData = DalHelper.CreateDataTable("AggTopTrafficDataDt", AggTopTrafficDatas.DataTableProperties, batch);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00026498 File Offset: 0x00024698
		internal AggTopTrafficDatas(Guid organizationalUnitRoot, DataTable items)
		{
			this.OrganizationalUnitRoot = organizationalUnitRoot;
			this.TvpAggTopTrafficData = items;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x000264B0 File Offset: 0x000246B0
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000264D5 File Offset: 0x000246D5
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x000264E7 File Offset: 0x000246E7
		public DataTable TvpAggTopTrafficData
		{
			get
			{
				return (DataTable)this[AggTopTrafficDatas.TvpAggTopTrafficDataProp];
			}
			private set
			{
				this[AggTopTrafficDatas.TvpAggTopTrafficDataProp] = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x000264F5 File Offset: 0x000246F5
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x000264FD File Offset: 0x000246FD
		internal Guid OrganizationalUnitRoot { get; set; }

		// Token: 0x0400060B RID: 1547
		internal const string AggTopTrafficDataTable = "AggTopTrafficDataDt";

		// Token: 0x0400060C RID: 1548
		internal static readonly HygienePropertyDefinition TvpAggTopTrafficDataProp = new HygienePropertyDefinition("tvp_AggTopTrafficData", typeof(DataTable));

		// Token: 0x0400060D RID: 1549
		private static readonly HygienePropertyDefinition[] DataTableProperties = new HygienePropertyDefinition[]
		{
			CommonReportingSchema.OrganizationalUnitRootProperty,
			CommonReportingSchema.DomainHashKeyProp,
			CommonReportingSchema.TrafficTypeProperty,
			CommonReportingSchema.DateKeyProperty,
			CommonReportingSchema.HourKeyProperty,
			AggTopTrafficData.AttributeValueProperty,
			CommonReportingSchema.TenantDomainProperty,
			CommonReportingSchema.MessageCountProperty,
			DalHelper.HashBucketProp
		};
	}
}
