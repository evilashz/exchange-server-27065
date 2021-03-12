using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000139 RID: 313
	internal class AggTopTrafficData : ConfigurablePropertyBag
	{
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x000262D4 File Offset: 0x000244D4
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000262F9 File Offset: 0x000244F9
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0002630B File Offset: 0x0002450B
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

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0002631E File Offset: 0x0002451E
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00026330 File Offset: 0x00024530
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

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0002633E File Offset: 0x0002453E
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00026350 File Offset: 0x00024550
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

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00026363 File Offset: 0x00024563
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00026375 File Offset: 0x00024575
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00026388 File Offset: 0x00024588
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0002639A File Offset: 0x0002459A
		public string TenantDomain
		{
			get
			{
				return (string)this[CommonReportingSchema.TenantDomainProperty];
			}
			set
			{
				this[CommonReportingSchema.TenantDomainProperty] = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x000263A8 File Offset: 0x000245A8
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000263BA File Offset: 0x000245BA
		public string AttributeValue
		{
			get
			{
				return (string)this[AggTopTrafficData.AttributeValueProperty];
			}
			set
			{
				this[AggTopTrafficData.AttributeValueProperty] = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000263C8 File Offset: 0x000245C8
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x000263DA File Offset: 0x000245DA
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

		// Token: 0x06000C2E RID: 3118 RVA: 0x000263ED File Offset: 0x000245ED
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return AggTopTrafficData.propertydefinitions;
		}

		// Token: 0x04000609 RID: 1545
		internal static readonly HygienePropertyDefinition AttributeValueProperty = new HygienePropertyDefinition("AttributeValue", typeof(string), string.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x0400060A RID: 1546
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			CommonReportingSchema.OrganizationalUnitRootProperty,
			CommonReportingSchema.TrafficTypeProperty,
			CommonReportingSchema.DateKeyProperty,
			CommonReportingSchema.HourKeyProperty,
			AggTopTrafficData.AttributeValueProperty,
			CommonReportingSchema.TenantDomainProperty,
			CommonReportingSchema.MessageCountProperty
		};
	}
}
