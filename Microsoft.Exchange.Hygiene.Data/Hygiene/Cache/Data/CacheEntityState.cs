using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	internal class CacheEntityState : ConfigurablePropertyBag
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000972A File Offset: 0x0000792A
		public CacheEntityState()
		{
			this.LastSyncTime = DateTime.MinValue;
			this.LastFullSyncTime = DateTime.MinValue;
			this.LastTracerSyncTime = DateTime.MinValue;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009753 File Offset: 0x00007953
		public CacheEntityState(string entityName) : this()
		{
			this.EntityName = entityName;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009762 File Offset: 0x00007962
		public CacheEntityState(string entityName, CachePrimingAction action) : this(entityName)
		{
			this.PrimingAction = action;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00009772 File Offset: 0x00007972
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.CacheIdentity);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000977F File Offset: 0x0000797F
		public string CacheIdentity
		{
			get
			{
				return CacheEntityState.ConstructCacheIdentity(this.EntityName);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000978C File Offset: 0x0000798C
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000979E File Offset: 0x0000799E
		public string EntityName
		{
			get
			{
				return this[CacheEntityState.EntityNameProp] as string;
			}
			set
			{
				this[CacheEntityState.EntityNameProp] = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000097AC File Offset: 0x000079AC
		// (set) Token: 0x06000314 RID: 788 RVA: 0x000097BE File Offset: 0x000079BE
		public DateTime LastSyncTime
		{
			get
			{
				return (DateTime)this[CacheEntityState.LastSyncTimeProp];
			}
			set
			{
				this[CacheEntityState.LastSyncTimeProp] = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000097D1 File Offset: 0x000079D1
		// (set) Token: 0x06000316 RID: 790 RVA: 0x000097E3 File Offset: 0x000079E3
		public DateTime LastFullSyncTime
		{
			get
			{
				return (DateTime)this[CacheEntityState.LastFullSyncTimeProp];
			}
			set
			{
				this[CacheEntityState.LastFullSyncTimeProp] = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000097F6 File Offset: 0x000079F6
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00009808 File Offset: 0x00007A08
		public DateTime LastTracerSyncTime
		{
			get
			{
				return (DateTime)this[CacheEntityState.LastTracerSyncTimeProp];
			}
			set
			{
				this[CacheEntityState.LastTracerSyncTimeProp] = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000981B File Offset: 0x00007A1B
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000982D File Offset: 0x00007A2D
		public CachePrimingAction PrimingAction
		{
			get
			{
				return (CachePrimingAction)this[CacheEntityState.PrimingActionProp];
			}
			set
			{
				this[CacheEntityState.PrimingActionProp] = value;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00009840 File Offset: 0x00007A40
		public static string ConstructCacheIdentity(string entityName)
		{
			return string.Format("{0}", entityName);
		}

		// Token: 0x040001FA RID: 506
		private static readonly HygienePropertyDefinition EntityNameProp = CacheObjectSchema.EntityNameProp;

		// Token: 0x040001FB RID: 507
		private static readonly HygienePropertyDefinition LastSyncTimeProp = CacheObjectSchema.LastSyncTimeProp;

		// Token: 0x040001FC RID: 508
		private static readonly HygienePropertyDefinition LastFullSyncTimeProp = CacheObjectSchema.LastFullSyncTimeProp;

		// Token: 0x040001FD RID: 509
		private static readonly HygienePropertyDefinition LastTracerSyncTimeProp = CacheObjectSchema.LastTracerSyncTimeProp;

		// Token: 0x040001FE RID: 510
		private static readonly HygienePropertyDefinition PrimingActionProp = new HygienePropertyDefinition("PrimingAction", typeof(CachePrimingAction), CachePrimingAction.Priming, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
