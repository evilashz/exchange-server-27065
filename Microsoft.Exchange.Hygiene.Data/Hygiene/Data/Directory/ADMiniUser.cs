using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C1 RID: 193
	[Serializable]
	internal class ADMiniUser : ConfigurablePropertyBag
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00014B53 File Offset: 0x00012D53
		public override ObjectId Identity
		{
			get
			{
				return this.UserId;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00014B5B File Offset: 0x00012D5B
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00014B6D File Offset: 0x00012D6D
		internal ADObjectId UserId
		{
			get
			{
				return this[ADMiniUserSchema.UserIdProp] as ADObjectId;
			}
			set
			{
				this[ADMiniUserSchema.UserIdProp] = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00014B7B File Offset: 0x00012D7B
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00014B8D File Offset: 0x00012D8D
		internal ADObjectId TenantId
		{
			get
			{
				return this[ADMiniUserSchema.TenantIdProp] as ADObjectId;
			}
			set
			{
				this[ADMiniUserSchema.TenantIdProp] = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00014B9B File Offset: 0x00012D9B
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x00014BAD File Offset: 0x00012DAD
		internal ADObjectId ConfigurationId
		{
			get
			{
				return this[ADMiniUserSchema.ConfigurationIdProp] as ADObjectId;
			}
			set
			{
				this[ADMiniUserSchema.ConfigurationIdProp] = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00014BBB File Offset: 0x00012DBB
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00014BCD File Offset: 0x00012DCD
		internal NetID NetId
		{
			get
			{
				return (NetID)this[ADMiniUserSchema.NetIdProp];
			}
			set
			{
				this[ADMiniUserSchema.NetIdProp] = value;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00014BDB File Offset: 0x00012DDB
		public override Type GetSchemaType()
		{
			return typeof(ADMiniUserSchema);
		}
	}
}
