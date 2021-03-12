using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000105 RID: 261
	internal class TenantIPInfo : ConfigurablePropertyBag
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0001ED89 File Offset: 0x0001CF89
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x0001ED9B File Offset: 0x0001CF9B
		public IPRange IPRange
		{
			get
			{
				return (IPRange)this[TenantIPInfo.IPRangeProp];
			}
			set
			{
				this[TenantIPInfo.IPRangeProp] = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0001EDA9 File Offset: 0x0001CFA9
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x0001EDBB File Offset: 0x0001CFBB
		public IPListType IPListType
		{
			get
			{
				return (IPListType)this[TenantIPInfo.IPListTypeProp];
			}
			set
			{
				this[TenantIPInfo.IPListTypeProp] = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0001EDCE File Offset: 0x0001CFCE
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		public bool IsRemoved
		{
			get
			{
				return (bool)this[TenantIPInfo.IsRemovedProp];
			}
			set
			{
				this[TenantIPInfo.IsRemovedProp] = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0001EDF3 File Offset: 0x0001CFF3
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0001EE05 File Offset: 0x0001D005
		public DateTime ChangeDatetime
		{
			get
			{
				return (DateTime)this[TenantIPInfo.ChangedDatetimeProp];
			}
			set
			{
				this[TenantIPInfo.ChangedDatetimeProp] = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0001EE18 File Offset: 0x0001D018
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0001EE2A File Offset: 0x0001D02A
		public Guid TenantId
		{
			get
			{
				return (Guid)this[TenantIPInfo.TenantIdProp];
			}
			set
			{
				this[TenantIPInfo.TenantIdProp] = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001EE3D File Offset: 0x0001D03D
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ToString());
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001EE4A File Offset: 0x0001D04A
		public override string ToString()
		{
			return this.IPRange.ToString() + this.IPListType + this.IsRemoved;
		}

		// Token: 0x04000545 RID: 1349
		public static readonly HygienePropertyDefinition IPListTypeProp = new HygienePropertyDefinition("IPListType", typeof(IPListType), IPListType.TenantAllowList, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000546 RID: 1350
		public static readonly HygienePropertyDefinition IPRangeProp = new HygienePropertyDefinition("IPRange", typeof(IPRange));

		// Token: 0x04000547 RID: 1351
		public static readonly HygienePropertyDefinition IsRemovedProp = new HygienePropertyDefinition("IsRemoved", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000548 RID: 1352
		public static readonly HygienePropertyDefinition ChangedDatetimeProp = new HygienePropertyDefinition("dt_ChangedDatetime", typeof(DateTime?));

		// Token: 0x04000549 RID: 1353
		public static readonly HygienePropertyDefinition TenantIdProp = new HygienePropertyDefinition("id_TenantId", typeof(Guid?));
	}
}
