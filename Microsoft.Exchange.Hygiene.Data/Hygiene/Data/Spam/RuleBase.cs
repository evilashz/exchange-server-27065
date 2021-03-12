using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001EE RID: 494
	internal class RuleBase : ConfigurablePropertyBag
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00041910 File Offset: 0x0003FB10
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ID.ToString());
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00041936 File Offset: 0x0003FB36
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00041948 File Offset: 0x0003FB48
		public Guid ID
		{
			get
			{
				return (Guid)this[RuleBase.IDProperty];
			}
			set
			{
				this[RuleBase.IDProperty] = value;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0004195B File Offset: 0x0003FB5B
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x0004196D File Offset: 0x0003FB6D
		public long? RuleID
		{
			get
			{
				return (long?)this[RuleBase.RuleIDProperty];
			}
			set
			{
				this[RuleBase.RuleIDProperty] = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00041980 File Offset: 0x0003FB80
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00041992 File Offset: 0x0003FB92
		public byte? ScopeID
		{
			get
			{
				return (byte?)this[RuleBase.ScopeIDProperty];
			}
			set
			{
				this[RuleBase.ScopeIDProperty] = value;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x000419A5 File Offset: 0x0003FBA5
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x000419B7 File Offset: 0x0003FBB7
		public byte? RuleType
		{
			get
			{
				return (byte?)this[RuleBase.RuleTypeProperty];
			}
			set
			{
				this[RuleBase.RuleTypeProperty] = value;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x000419CA File Offset: 0x0003FBCA
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x000419DC File Offset: 0x0003FBDC
		public long? GroupID
		{
			get
			{
				return (long?)this[RuleBase.GroupIDProperty];
			}
			set
			{
				this[RuleBase.GroupIDProperty] = value;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x000419EF File Offset: 0x0003FBEF
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x00041A01 File Offset: 0x0003FC01
		public decimal? Sequence
		{
			get
			{
				return (decimal?)this[RuleBase.SequenceProperty];
			}
			set
			{
				this[RuleBase.SequenceProperty] = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00041A14 File Offset: 0x0003FC14
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x00041A26 File Offset: 0x0003FC26
		public DateTime? CreatedDatetime
		{
			get
			{
				return (DateTime?)this[RuleBase.CreatedDatetimeProperty];
			}
			set
			{
				this[RuleBase.CreatedDatetimeProperty] = value;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00041A39 File Offset: 0x0003FC39
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00041A4B File Offset: 0x0003FC4B
		public DateTime? ChangeDatetime
		{
			get
			{
				return (DateTime?)this[RuleBase.ChangedDatetimeProperty];
			}
			set
			{
				this[RuleBase.ChangedDatetimeProperty] = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00041A5E File Offset: 0x0003FC5E
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x00041A70 File Offset: 0x0003FC70
		public DateTime? DeletedDatetime
		{
			get
			{
				return (DateTime?)this[RuleBase.DeletedDatetimeProperty];
			}
			set
			{
				this[RuleBase.DeletedDatetimeProperty] = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00041A83 File Offset: 0x0003FC83
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x00041A95 File Offset: 0x0003FC95
		public bool? IsPersistent
		{
			get
			{
				return (bool?)this[RuleBase.IsPersistentProperty];
			}
			set
			{
				this[RuleBase.IsPersistentProperty] = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00041AA8 File Offset: 0x0003FCA8
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00041ABA File Offset: 0x0003FCBA
		public bool? IsActive
		{
			get
			{
				return (bool?)this[RuleBase.IsActiveProperty];
			}
			set
			{
				this[RuleBase.IsActiveProperty] = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00041ACD File Offset: 0x0003FCCD
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x00041ADF File Offset: 0x0003FCDF
		public byte? State
		{
			get
			{
				return (byte?)this[RuleBase.StateProperty];
			}
			set
			{
				this[RuleBase.StateProperty] = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00041AF2 File Offset: 0x0003FCF2
		// (set) Token: 0x060014DC RID: 5340 RVA: 0x00041B04 File Offset: 0x0003FD04
		public long? AddedVersion
		{
			get
			{
				return (long?)this[RuleBase.AddedVersionProperty];
			}
			set
			{
				this[RuleBase.AddedVersionProperty] = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00041B17 File Offset: 0x0003FD17
		// (set) Token: 0x060014DE RID: 5342 RVA: 0x00041B29 File Offset: 0x0003FD29
		public long? RemovedVersion
		{
			get
			{
				return (long?)this[RuleBase.RemovedVersionProperty];
			}
			set
			{
				this[RuleBase.RemovedVersionProperty] = value;
			}
		}

		// Token: 0x04000A52 RID: 2642
		public static readonly HygienePropertyDefinition IDProperty = new HygienePropertyDefinition("id_RuleId", typeof(Guid));

		// Token: 0x04000A53 RID: 2643
		public static readonly HygienePropertyDefinition RuleIDProperty = new HygienePropertyDefinition("bi_RuleId", typeof(long?));

		// Token: 0x04000A54 RID: 2644
		public static readonly HygienePropertyDefinition RuleTypeProperty = new HygienePropertyDefinition("ti_RuleType", typeof(byte?));

		// Token: 0x04000A55 RID: 2645
		public static readonly HygienePropertyDefinition ScopeIDProperty = new HygienePropertyDefinition("ti_ScopeId", typeof(byte?));

		// Token: 0x04000A56 RID: 2646
		public static readonly HygienePropertyDefinition GroupIDProperty = new HygienePropertyDefinition("bi_GroupId", typeof(long?));

		// Token: 0x04000A57 RID: 2647
		public static readonly HygienePropertyDefinition SequenceProperty = new HygienePropertyDefinition("d_Sequence", typeof(decimal?));

		// Token: 0x04000A58 RID: 2648
		public static readonly HygienePropertyDefinition CreatedDatetimeProperty = new HygienePropertyDefinition("dt_CreatedDatetime", typeof(DateTime?));

		// Token: 0x04000A59 RID: 2649
		public static readonly HygienePropertyDefinition ChangedDatetimeProperty = new HygienePropertyDefinition("dt_ChangedDatetime", typeof(DateTime?));

		// Token: 0x04000A5A RID: 2650
		public static readonly HygienePropertyDefinition DeletedDatetimeProperty = new HygienePropertyDefinition("dt_DeletedDatetime", typeof(DateTime?));

		// Token: 0x04000A5B RID: 2651
		public static readonly HygienePropertyDefinition IsPersistentProperty = new HygienePropertyDefinition("f_IsPersistent", typeof(bool?));

		// Token: 0x04000A5C RID: 2652
		public static readonly HygienePropertyDefinition IsActiveProperty = new HygienePropertyDefinition("f_IsActive", typeof(bool?));

		// Token: 0x04000A5D RID: 2653
		public static readonly HygienePropertyDefinition StateProperty = new HygienePropertyDefinition("ti_State", typeof(byte?));

		// Token: 0x04000A5E RID: 2654
		public static readonly HygienePropertyDefinition AddedVersionProperty = new HygienePropertyDefinition("bi_AddedVersion", typeof(long?));

		// Token: 0x04000A5F RID: 2655
		public static readonly HygienePropertyDefinition RemovedVersionProperty = new HygienePropertyDefinition("bi_RemovedVersion", typeof(long?));
	}
}
