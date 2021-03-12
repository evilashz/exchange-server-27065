using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200013B RID: 315
	internal class AggTrafficData : ConfigurablePropertyBag
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00026584 File Offset: 0x00024784
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x000265A9 File Offset: 0x000247A9
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x000265BB File Offset: 0x000247BB
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000265CE File Offset: 0x000247CE
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x000265E0 File Offset: 0x000247E0
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

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000265EE File Offset: 0x000247EE
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x00026600 File Offset: 0x00024800
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

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00026613 File Offset: 0x00024813
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x00026625 File Offset: 0x00024825
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00026638 File Offset: 0x00024838
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x0002664A File Offset: 0x0002484A
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

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00026658 File Offset: 0x00024858
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x0002666A File Offset: 0x0002486A
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

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0002667D File Offset: 0x0002487D
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x0002668F File Offset: 0x0002488F
		public long RecipientCount
		{
			get
			{
				return (long)this[CommonReportingSchema.RecipientCountProperty];
			}
			set
			{
				this[CommonReportingSchema.RecipientCountProperty] = value;
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000266A2 File Offset: 0x000248A2
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return AggTrafficData.propertydefinitions;
		}

		// Token: 0x0400060F RID: 1551
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			CommonReportingSchema.OrganizationalUnitRootProperty,
			CommonReportingSchema.TrafficTypeProperty,
			CommonReportingSchema.DateKeyProperty,
			CommonReportingSchema.HourKeyProperty,
			CommonReportingSchema.TenantDomainProperty,
			CommonReportingSchema.MessageCountProperty,
			CommonReportingSchema.RecipientCountProperty
		};
	}
}
