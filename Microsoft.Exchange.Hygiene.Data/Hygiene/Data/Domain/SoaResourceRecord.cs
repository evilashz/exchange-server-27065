using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x0200012A RID: 298
	[Serializable]
	internal class SoaResourceRecord : ConfigurablePropertyBag
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00024E66 File Offset: 0x00023066
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.ResourceRecordId);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00024E73 File Offset: 0x00023073
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x00024E85 File Offset: 0x00023085
		public Guid ResourceRecordId
		{
			get
			{
				return (Guid)this[DomainSchema.ResourceRecordId];
			}
			set
			{
				this[DomainSchema.ResourceRecordId] = value;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00024E98 File Offset: 0x00023098
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x00024EAA File Offset: 0x000230AA
		public string DomainName
		{
			get
			{
				return (string)this[DomainSchema.DomainName];
			}
			set
			{
				this[DomainSchema.DomainName] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00024EBD File Offset: 0x000230BD
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x00024ECF File Offset: 0x000230CF
		public string PrimaryNameServer
		{
			get
			{
				return (string)this[DomainSchema.PrimaryNameServer];
			}
			set
			{
				this[DomainSchema.PrimaryNameServer] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00024EE2 File Offset: 0x000230E2
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x00024EF4 File Offset: 0x000230F4
		public string ResponsibleMailServer
		{
			get
			{
				return (string)this[DomainSchema.ResponsibleMailServer];
			}
			set
			{
				this[DomainSchema.ResponsibleMailServer] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00024F07 File Offset: 0x00023107
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x00024F19 File Offset: 0x00023119
		public int Refresh
		{
			get
			{
				return (int)this[DomainSchema.Refresh];
			}
			set
			{
				this[DomainSchema.Refresh] = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00024F2C File Offset: 0x0002312C
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x00024F3E File Offset: 0x0002313E
		public int Retry
		{
			get
			{
				return (int)this[DomainSchema.Retry];
			}
			set
			{
				this[DomainSchema.Retry] = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00024F51 File Offset: 0x00023151
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x00024F63 File Offset: 0x00023163
		public int Expire
		{
			get
			{
				return (int)this[DomainSchema.Expire];
			}
			set
			{
				this[DomainSchema.Expire] = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00024F76 File Offset: 0x00023176
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x00024F88 File Offset: 0x00023188
		public int Serial
		{
			get
			{
				return (int)this[DomainSchema.Serial];
			}
			set
			{
				this[DomainSchema.Serial] = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00024F9B File Offset: 0x0002319B
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x00024FAD File Offset: 0x000231AD
		public int DefaultTtl
		{
			get
			{
				return (int)this[DomainSchema.DefaultTtl];
			}
			set
			{
				this[DomainSchema.DefaultTtl] = value;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00024FC0 File Offset: 0x000231C0
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return SoaResourceRecord.propertydefinitions;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00024FD2 File Offset: 0x000231D2
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x040005F3 RID: 1523
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.ResourceRecordId,
			DomainSchema.DomainName,
			DomainSchema.PrimaryNameServer,
			DomainSchema.ResponsibleMailServer,
			DomainSchema.Refresh,
			DomainSchema.Retry,
			DomainSchema.Expire,
			DomainSchema.Serial,
			DomainSchema.DefaultTtl
		};
	}
}
