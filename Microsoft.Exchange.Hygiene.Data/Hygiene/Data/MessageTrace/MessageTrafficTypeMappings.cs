using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000185 RID: 389
	internal class MessageTrafficTypeMappings : ConfigurablePropertyBag
	{
		// Token: 0x06000FB5 RID: 4021 RVA: 0x00031D5B File Offset: 0x0002FF5B
		public MessageTrafficTypeMappings(IEnumerable<MessageTrafficTypeMapping> batch)
		{
			this.TvpMessageTrafficTypeMapping = DalHelper.CreateDataTable("MessageTrafficTypeMappingDt", MessageTrafficTypeMappings.DataTableProperties, batch);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00031D79 File Offset: 0x0002FF79
		internal MessageTrafficTypeMappings(Guid organizationalUnitRoot, DataTable items)
		{
			this.OrganizationalUnitRoot = organizationalUnitRoot;
			this.TvpMessageTrafficTypeMapping = items;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00031D90 File Offset: 0x0002FF90
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00031DB5 File Offset: 0x0002FFB5
		// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x00031DC7 File Offset: 0x0002FFC7
		public DataTable TvpMessageTrafficTypeMapping
		{
			get
			{
				return (DataTable)this[MessageTrafficTypeMappings.TvpMessageTrafficTypeMappingProp];
			}
			private set
			{
				this[MessageTrafficTypeMappings.TvpMessageTrafficTypeMappingProp] = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00031DD5 File Offset: 0x0002FFD5
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x00031DDD File Offset: 0x0002FFDD
		internal Guid OrganizationalUnitRoot { get; set; }

		// Token: 0x0400073B RID: 1851
		internal const string MessageTrafficTypeMappingTable = "MessageTrafficTypeMappingDt";

		// Token: 0x0400073C RID: 1852
		internal static readonly HygienePropertyDefinition TvpMessageTrafficTypeMappingProp = new HygienePropertyDefinition("tvp_MessageTrafficTypeMapping", typeof(DataTable));

		// Token: 0x0400073D RID: 1853
		private static readonly HygienePropertyDefinition[] DataTableProperties = new HygienePropertyDefinition[]
		{
			CommonReportingSchema.OrganizationalUnitRootProperty,
			CommonReportingSchema.DataSourceProperty,
			CommonReportingSchema.DomainHashKeyProp,
			CommonReportingSchema.TrafficTypeProperty,
			MessageTrafficTypeMapping.ExMessageIdProperty,
			CommonReportingSchema.DateKeyProperty,
			CommonReportingSchema.HourKeyProperty,
			DalHelper.HashBucketProp
		};
	}
}
