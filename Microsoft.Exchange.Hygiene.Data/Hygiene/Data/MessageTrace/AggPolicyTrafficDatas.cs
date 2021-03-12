using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000138 RID: 312
	internal class AggPolicyTrafficDatas : ConfigurablePropertyBag
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x000261C0 File Offset: 0x000243C0
		public AggPolicyTrafficDatas(IEnumerable<AggPolicyTrafficData> batch)
		{
			this.TvpAggPolicyTrafficData = DalHelper.CreateDataTable("AggPolicyTrafficDt", AggPolicyTrafficDatas.DataTableProperties, batch);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000261DE File Offset: 0x000243DE
		internal AggPolicyTrafficDatas(Guid organizationalUnitRoot, DataTable items)
		{
			this.OrganizationalUnitRoot = organizationalUnitRoot;
			this.TvpAggPolicyTrafficData = items;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x000261F4 File Offset: 0x000243F4
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00026219 File Offset: 0x00024419
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x0002622B File Offset: 0x0002442B
		public DataTable TvpAggPolicyTrafficData
		{
			get
			{
				return (DataTable)this[AggPolicyTrafficDatas.TvpAggPolicyTrafficDataProp];
			}
			private set
			{
				this[AggPolicyTrafficDatas.TvpAggPolicyTrafficDataProp] = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00026239 File Offset: 0x00024439
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00026241 File Offset: 0x00024441
		internal Guid OrganizationalUnitRoot { get; set; }

		// Token: 0x04000605 RID: 1541
		internal const string AggPolicyTrafficDataTable = "AggPolicyTrafficDt";

		// Token: 0x04000606 RID: 1542
		internal static readonly HygienePropertyDefinition TvpAggPolicyTrafficDataProp = new HygienePropertyDefinition("tvp_AggPolicyTrafficData", typeof(DataTable));

		// Token: 0x04000607 RID: 1543
		private static readonly HygienePropertyDefinition[] DataTableProperties = new HygienePropertyDefinition[]
		{
			CommonReportingSchema.OrganizationalUnitRootProperty,
			CommonReportingSchema.DataSourceProperty,
			AggPolicyTrafficData.DLPIdProperty,
			AggPolicyTrafficData.RuleIdProperty,
			CommonReportingSchema.DomainHashKeyProp,
			CommonReportingSchema.TrafficTypeProperty,
			CommonReportingSchema.DateKeyProperty,
			CommonReportingSchema.HourKeyProperty,
			CommonReportingSchema.MessageCountProperty,
			DalHelper.HashBucketProp
		};
	}
}
