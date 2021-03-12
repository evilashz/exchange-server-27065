using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000057 RID: 87
	internal class CachePrimingInfo : ConfigurablePropertyBag
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000A1C0 File Offset: 0x000083C0
		public CachePrimingInfo()
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A1C8 File Offset: 0x000083C8
		internal CachePrimingInfo(string cacheName, CachePrimingState primingState, CacheEntityState entityState = null)
		{
			this.CacheName = cacheName;
			this.PrimingState = primingState;
			if (entityState != null)
			{
				this.LastSyncTime = entityState.LastSyncTime;
				this.SyncWatermark = entityState.LastFullSyncTime;
				this.LastTracerSyncTime = entityState.LastTracerSyncTime;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000A205 File Offset: 0x00008405
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.CacheIdentity);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000A212 File Offset: 0x00008412
		public string CacheIdentity
		{
			get
			{
				return this.CacheName;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000A21A File Offset: 0x0000841A
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000A22C File Offset: 0x0000842C
		public string CacheName
		{
			get
			{
				return this[CachePrimingInfo.CacheNameProp] as string;
			}
			set
			{
				this[CachePrimingInfo.CacheNameProp] = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000A23A File Offset: 0x0000843A
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000A24C File Offset: 0x0000844C
		public CachePrimingState PrimingState
		{
			get
			{
				return (CachePrimingState)this[CachePrimingInfo.PrimingStateProp];
			}
			set
			{
				this[CachePrimingInfo.PrimingStateProp] = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000A25F File Offset: 0x0000845F
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000A271 File Offset: 0x00008471
		public DateTime LastSyncTime
		{
			get
			{
				return (DateTime)this[CachePrimingInfo.LastSyncTimeProp];
			}
			set
			{
				this[CachePrimingInfo.LastSyncTimeProp] = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000A284 File Offset: 0x00008484
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000A296 File Offset: 0x00008496
		public DateTime SyncWatermark
		{
			get
			{
				return (DateTime)this[CachePrimingInfo.SyncWatermarkProp];
			}
			set
			{
				this[CachePrimingInfo.SyncWatermarkProp] = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000A2A9 File Offset: 0x000084A9
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000A2BB File Offset: 0x000084BB
		public DateTime LastTracerSyncTime
		{
			get
			{
				return (DateTime)this[CachePrimingInfo.LastTracerSyncTimeProp];
			}
			set
			{
				this[CachePrimingInfo.LastTracerSyncTimeProp] = value;
			}
		}

		// Token: 0x04000219 RID: 537
		private static readonly HygienePropertyDefinition CacheNameProp = CacheObjectSchema.EntityNameProp;

		// Token: 0x0400021A RID: 538
		private static readonly HygienePropertyDefinition LastSyncTimeProp = CacheObjectSchema.LastSyncTimeProp;

		// Token: 0x0400021B RID: 539
		private static readonly HygienePropertyDefinition SyncWatermarkProp = CacheObjectSchema.LastFullSyncTimeProp;

		// Token: 0x0400021C RID: 540
		private static readonly HygienePropertyDefinition LastTracerSyncTimeProp = CacheObjectSchema.LastTracerSyncTimeProp;

		// Token: 0x0400021D RID: 541
		private static readonly HygienePropertyDefinition PrimingStateProp = new HygienePropertyDefinition("PrimingState", typeof(CachePrimingState), CachePrimingState.Unknown, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
