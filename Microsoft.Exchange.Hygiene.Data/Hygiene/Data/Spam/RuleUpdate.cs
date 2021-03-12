using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F9 RID: 505
	internal class RuleUpdate : ConfigurablePropertyBag
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x000424DC File Offset: 0x000406DC
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ID.ToString());
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00042502 File Offset: 0x00040702
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x00042514 File Offset: 0x00040714
		public Guid? ID
		{
			get
			{
				return (Guid?)this[RuleUpdate.IDProperty];
			}
			set
			{
				this[RuleUpdate.IDProperty] = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00042527 File Offset: 0x00040727
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x00042539 File Offset: 0x00040739
		public long? RuleID
		{
			get
			{
				return (long?)this[RuleUpdate.RuleIDProperty];
			}
			set
			{
				this[RuleUpdate.RuleIDProperty] = value;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0004254C File Offset: 0x0004074C
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x0004255E File Offset: 0x0004075E
		public byte? RuleType
		{
			get
			{
				return (byte?)this[RuleUpdate.RuleTypeProperty];
			}
			set
			{
				this[RuleUpdate.RuleTypeProperty] = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00042571 File Offset: 0x00040771
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x00042583 File Offset: 0x00040783
		public bool? IsPersistent
		{
			get
			{
				return (bool?)this[RuleUpdate.IsPersistentProperty];
			}
			set
			{
				this[RuleUpdate.IsPersistentProperty] = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00042596 File Offset: 0x00040796
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x000425A8 File Offset: 0x000407A8
		public bool? IsActive
		{
			get
			{
				return (bool?)this[RuleUpdate.IsActiveProperty];
			}
			set
			{
				this[RuleUpdate.IsActiveProperty] = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000425BB File Offset: 0x000407BB
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x000425CD File Offset: 0x000407CD
		public byte? State
		{
			get
			{
				return (byte?)this[RuleUpdate.StateProperty];
			}
			set
			{
				this[RuleUpdate.StateProperty] = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x000425E0 File Offset: 0x000407E0
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x000425F2 File Offset: 0x000407F2
		public long? AddedVersion
		{
			get
			{
				return (long?)this[RuleUpdate.AddedVersionProperty];
			}
			set
			{
				this[RuleUpdate.AddedVersionProperty] = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00042605 File Offset: 0x00040805
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x00042617 File Offset: 0x00040817
		public long? RemovedVersion
		{
			get
			{
				return (long?)this[RuleUpdate.RemovedVersionProperty];
			}
			set
			{
				this[RuleUpdate.RemovedVersionProperty] = value;
			}
		}

		// Token: 0x04000A91 RID: 2705
		public static readonly HygienePropertyDefinition IDProperty = new HygienePropertyDefinition("id_RuleId", typeof(Guid?));

		// Token: 0x04000A92 RID: 2706
		public static readonly HygienePropertyDefinition RuleIDProperty = new HygienePropertyDefinition("bi_RuleId", typeof(long?));

		// Token: 0x04000A93 RID: 2707
		public static readonly HygienePropertyDefinition RuleTypeProperty = new HygienePropertyDefinition("ti_RuleType", typeof(byte?));

		// Token: 0x04000A94 RID: 2708
		public static readonly HygienePropertyDefinition IsPersistentProperty = new HygienePropertyDefinition("f_IsPersistent", typeof(bool?));

		// Token: 0x04000A95 RID: 2709
		public static readonly HygienePropertyDefinition IsActiveProperty = new HygienePropertyDefinition("f_IsActive", typeof(bool?));

		// Token: 0x04000A96 RID: 2710
		public static readonly HygienePropertyDefinition StateProperty = new HygienePropertyDefinition("ti_State", typeof(byte?));

		// Token: 0x04000A97 RID: 2711
		public static readonly HygienePropertyDefinition AddedVersionProperty = new HygienePropertyDefinition("bi_AddedVersion", typeof(long?));

		// Token: 0x04000A98 RID: 2712
		public static readonly HygienePropertyDefinition RemovedVersionProperty = new HygienePropertyDefinition("bi_RemovedVersion", typeof(long?));
	}
}
