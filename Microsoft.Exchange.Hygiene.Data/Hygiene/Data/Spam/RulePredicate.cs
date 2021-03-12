using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F7 RID: 503
	internal class RulePredicate : ConfigurablePropertyBag
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00042204 File Offset: 0x00040404
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.PredicateID.ToString());
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0004222A File Offset: 0x0004042A
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x0004223C File Offset: 0x0004043C
		public Guid ID
		{
			get
			{
				return (Guid)this[RulePredicate.IDProperty];
			}
			set
			{
				this[RulePredicate.IDProperty] = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0004224F File Offset: 0x0004044F
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x00042261 File Offset: 0x00040461
		public Guid PredicateID
		{
			get
			{
				return (Guid)this[RulePredicate.PredicateIDProperty];
			}
			set
			{
				this[RulePredicate.PredicateIDProperty] = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00042274 File Offset: 0x00040474
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x00042286 File Offset: 0x00040486
		public decimal? Sequence
		{
			get
			{
				return (decimal?)this[RulePredicate.SequenceProperty];
			}
			set
			{
				this[RulePredicate.SequenceProperty] = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00042299 File Offset: 0x00040499
		// (set) Token: 0x0600151C RID: 5404 RVA: 0x000422AB File Offset: 0x000404AB
		public Guid? ParentID
		{
			get
			{
				return (Guid?)this[RulePredicate.ParentIDProperty];
			}
			set
			{
				this[RulePredicate.ParentIDProperty] = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x000422BE File Offset: 0x000404BE
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x000422D0 File Offset: 0x000404D0
		public byte? PredicateType
		{
			get
			{
				return (byte?)this[RulePredicate.PredicateTypeProperty];
			}
			set
			{
				this[RulePredicate.PredicateTypeProperty] = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000422E3 File Offset: 0x000404E3
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x000422F5 File Offset: 0x000404F5
		public long? ProcessorID
		{
			get
			{
				return (long?)this[RulePredicate.ProcessorIdProperty];
			}
			set
			{
				this[RulePredicate.ProcessorIdProperty] = value;
			}
		}

		// Token: 0x04000A8B RID: 2699
		public static readonly HygienePropertyDefinition IDProperty = new HygienePropertyDefinition("id_RuleId", typeof(Guid));

		// Token: 0x04000A8C RID: 2700
		public static readonly HygienePropertyDefinition PredicateIDProperty = new HygienePropertyDefinition("id_PredicateId", typeof(Guid));

		// Token: 0x04000A8D RID: 2701
		public static readonly HygienePropertyDefinition ParentIDProperty = new HygienePropertyDefinition("id_ParentId", typeof(Guid?));

		// Token: 0x04000A8E RID: 2702
		public static readonly HygienePropertyDefinition ProcessorIdProperty = new HygienePropertyDefinition("bi_ProcessorId", typeof(long?));

		// Token: 0x04000A8F RID: 2703
		public static readonly HygienePropertyDefinition PredicateTypeProperty = new HygienePropertyDefinition("ti_PredicateType", typeof(byte?));

		// Token: 0x04000A90 RID: 2704
		public static readonly HygienePropertyDefinition SequenceProperty = new HygienePropertyDefinition("d_Sequence", typeof(decimal?));
	}
}
