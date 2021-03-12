using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000137 RID: 311
	internal class AggPolicyTrafficData : ConfigurablePropertyBag
	{
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00025FAC File Offset: 0x000241AC
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00025FD1 File Offset: 0x000241D1
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00025FE3 File Offset: 0x000241E3
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

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00025FF6 File Offset: 0x000241F6
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00026008 File Offset: 0x00024208
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

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00026016 File Offset: 0x00024216
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00026028 File Offset: 0x00024228
		public Guid DLPId
		{
			get
			{
				return (Guid)this[AggPolicyTrafficData.DLPIdProperty];
			}
			set
			{
				this[AggPolicyTrafficData.DLPIdProperty] = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002603B File Offset: 0x0002423B
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0002604D File Offset: 0x0002424D
		public Guid RuleId
		{
			get
			{
				return (Guid)this[AggPolicyTrafficData.RuleIdProperty];
			}
			set
			{
				this[AggPolicyTrafficData.RuleIdProperty] = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00026060 File Offset: 0x00024260
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00026072 File Offset: 0x00024272
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

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00026080 File Offset: 0x00024280
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00026092 File Offset: 0x00024292
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

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x000260A5 File Offset: 0x000242A5
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x000260B7 File Offset: 0x000242B7
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

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000260CA File Offset: 0x000242CA
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x000260D2 File Offset: 0x000242D2
		public string TenantDomain { get; set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000260DB File Offset: 0x000242DB
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x000260ED File Offset: 0x000242ED
		public long MessageCount
		{
			get
			{
				return (long)this[CommonReportingSchema.MessageCountProperty];
			}
			set
			{
				this[CommonReportingSchema.MessageCountProperty] = value;
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00026100 File Offset: 0x00024300
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return AggPolicyTrafficData.propertydefinitions;
		}

		// Token: 0x04000601 RID: 1537
		internal static readonly HygienePropertyDefinition DLPIdProperty = new HygienePropertyDefinition("DLPId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000602 RID: 1538
		internal static readonly HygienePropertyDefinition RuleIdProperty = new HygienePropertyDefinition("RuleId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000603 RID: 1539
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			CommonReportingSchema.OrganizationalUnitRootProperty,
			CommonReportingSchema.DataSourceProperty,
			CommonReportingSchema.TrafficTypeProperty,
			CommonReportingSchema.DateKeyProperty,
			CommonReportingSchema.HourKeyProperty,
			AggPolicyTrafficData.DLPIdProperty,
			AggPolicyTrafficData.RuleIdProperty,
			CommonReportingSchema.MessageCountProperty
		};
	}
}
