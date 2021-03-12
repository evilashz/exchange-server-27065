using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x0200011E RID: 286
	internal class DICommon : ConfigurablePropertyBag
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00021882 File Offset: 0x0001FA82
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.Identifier);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0002188F File Offset: 0x0001FA8F
		public Guid Identifier
		{
			get
			{
				return (Guid)this[DomainSchema.Identifier];
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000218A1 File Offset: 0x0001FAA1
		public Guid TenantId
		{
			get
			{
				return DomainSchema.GetGuidEmptyIfNull(this[DomainSchema.TenantId]);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000218B4 File Offset: 0x0001FAB4
		public DataDatetimeGroup EntityTimeStamp
		{
			get
			{
				return new DataDatetimeGroup
				{
					createdDatetime = (DateTime?)this[DomainSchema.CreatedDatetime],
					changedDatetime = (DateTime?)this[DomainSchema.ChangedDatetime],
					deletedDatetime = (DateTime?)this[DomainSchema.DeletedDatetime]
				};
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00021910 File Offset: 0x0001FB10
		public DataDatetimeGroup PropertyTimeStamp
		{
			get
			{
				return new DataDatetimeGroup
				{
					createdDatetime = (DateTime?)this[DomainSchema.PropertyCreatedDatetime],
					changedDatetime = (DateTime?)this[DomainSchema.PropertyChangedDatetime],
					deletedDatetime = (DateTime?)this[DomainSchema.PropertyDeletedDatetime]
				};
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002196B File Offset: 0x0001FB6B
		public int? PropertyId
		{
			get
			{
				return (int?)this[DomainSchema.PropertyId];
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002197D File Offset: 0x0001FB7D
		public int? EntityId
		{
			get
			{
				return (int?)this[DomainSchema.EntityId];
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002198F File Offset: 0x0001FB8F
		public string PropertyValue
		{
			get
			{
				return this[DomainSchema.PropertyValue] as string;
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000219A1 File Offset: 0x0001FBA1
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return DICommon.propertydefinitions;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000219B3 File Offset: 0x0001FBB3
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x04000596 RID: 1430
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.TenantId,
			DomainSchema.PropertyValue,
			DomainSchema.EntityId,
			DomainSchema.PropertyId,
			DomainSchema.CreatedDatetime,
			DomainSchema.ChangedDatetime,
			DomainSchema.DeletedDatetime,
			DomainSchema.PropertyCreatedDatetime,
			DomainSchema.PropertyChangedDatetime,
			DomainSchema.PropertyDeletedDatetime
		};
	}
}
