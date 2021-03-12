using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001E3 RID: 483
	internal class PredicateExtendedProperty : ConfigurablePropertyTable
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00040E38 File Offset: 0x0003F038
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00040E5D File Offset: 0x0003F05D
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x00040E6F File Offset: 0x0003F06F
		public Guid ID
		{
			get
			{
				return (Guid)this[PredicateExtendedProperty.IDProperty];
			}
			set
			{
				this[PredicateExtendedProperty.IDProperty] = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00040E82 File Offset: 0x0003F082
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x00040E94 File Offset: 0x0003F094
		public Guid PredicateID
		{
			get
			{
				return (Guid)this[PredicateExtendedProperty.PredicateIDProperty];
			}
			set
			{
				this[PredicateExtendedProperty.PredicateIDProperty] = value;
			}
		}

		// Token: 0x04000A12 RID: 2578
		public static readonly HygienePropertyDefinition IDProperty = new HygienePropertyDefinition("id_RuleId", typeof(Guid));

		// Token: 0x04000A13 RID: 2579
		public static readonly HygienePropertyDefinition PredicateIDProperty = new HygienePropertyDefinition("id_PredicateId", typeof(Guid));
	}
}
