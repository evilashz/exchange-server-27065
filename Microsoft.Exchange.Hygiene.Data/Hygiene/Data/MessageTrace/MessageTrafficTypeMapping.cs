using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000184 RID: 388
	internal class MessageTrafficTypeMapping : ConfigurablePropertyBag
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00031BC8 File Offset: 0x0002FDC8
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00031BED File Offset: 0x0002FDED
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00031BFF File Offset: 0x0002FDFF
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[CommonReportingSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[CommonReportingSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00031C12 File Offset: 0x0002FE12
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x00031C24 File Offset: 0x0002FE24
		public string DataSource
		{
			get
			{
				return this[CommonReportingSchema.DataSourceProperty] as string;
			}
			set
			{
				this[CommonReportingSchema.DataSourceProperty] = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00031C32 File Offset: 0x0002FE32
		// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x00031C44 File Offset: 0x0002FE44
		public string TrafficType
		{
			get
			{
				return (string)this[CommonReportingSchema.TrafficTypeProperty];
			}
			set
			{
				this[CommonReportingSchema.TrafficTypeProperty] = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00031C52 File Offset: 0x0002FE52
		// (set) Token: 0x06000FAB RID: 4011 RVA: 0x00031C64 File Offset: 0x0002FE64
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[MessageTrafficTypeMapping.ExMessageIdProperty];
			}
			set
			{
				this[MessageTrafficTypeMapping.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00031C77 File Offset: 0x0002FE77
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x00031C89 File Offset: 0x0002FE89
		public int DateKey
		{
			get
			{
				return (int)this[CommonReportingSchema.DateKeyProperty];
			}
			set
			{
				this[CommonReportingSchema.DateKeyProperty] = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00031C9C File Offset: 0x0002FE9C
		// (set) Token: 0x06000FAF RID: 4015 RVA: 0x00031CAE File Offset: 0x0002FEAE
		public short HourKey
		{
			get
			{
				return (short)this[CommonReportingSchema.HourKeyProperty];
			}
			set
			{
				this[CommonReportingSchema.HourKeyProperty] = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00031CC1 File Offset: 0x0002FEC1
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x00031CC9 File Offset: 0x0002FEC9
		public string TenantDomain { get; set; }

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00031CD2 File Offset: 0x0002FED2
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return MessageTrafficTypeMapping.propertydefinitions;
		}

		// Token: 0x04000738 RID: 1848
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = new HygienePropertyDefinition("ExMessageId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000739 RID: 1849
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			CommonReportingSchema.OrganizationalUnitRootProperty,
			CommonReportingSchema.DataSourceProperty,
			CommonReportingSchema.TrafficTypeProperty,
			MessageTrafficTypeMapping.ExMessageIdProperty,
			CommonReportingSchema.DateKeyProperty,
			CommonReportingSchema.HourKeyProperty
		};
	}
}
