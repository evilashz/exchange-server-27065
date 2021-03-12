using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	internal class CacheWorkSliceCookie : ConfigurablePropertyBag
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000A370 File Offset: 0x00008570
		public CacheWorkSliceCookie()
		{
			this.LastSyncTime = DateTime.MinValue;
			this.LastFullSyncTime = DateTime.MinValue;
			this.CookieOffset = -1L;
			this.DBRead = false;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public CacheWorkSliceCookie(string entityName, int partitionIndex, CachePrimingMode primingMode)
		{
			this.EntityName = entityName;
			this.PartitionIndex = partitionIndex;
			this.PrimingMode = primingMode;
			this.LastSyncTime = DateTime.MinValue;
			this.LastFullSyncTime = DateTime.MinValue;
			this.CookieOffset = -1L;
			this.DBRead = false;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000A3ED File Offset: 0x000085ED
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.CacheIdentity);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000A3FA File Offset: 0x000085FA
		public string CacheIdentity
		{
			get
			{
				return CacheWorkSliceCookie.ConstructCacheIdentity(this.EntityName, this.PartitionIndex, this.PrimingMode);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000A413 File Offset: 0x00008613
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000A425 File Offset: 0x00008625
		public string EntityName
		{
			get
			{
				return this[CacheWorkSliceCookie.EntityNameProp] as string;
			}
			set
			{
				this[CacheWorkSliceCookie.EntityNameProp] = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000A433 File Offset: 0x00008633
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000A445 File Offset: 0x00008645
		public int PartitionIndex
		{
			get
			{
				return (int)this[CacheWorkSliceCookie.PartitionIndexProp];
			}
			set
			{
				this[CacheWorkSliceCookie.PartitionIndexProp] = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000A458 File Offset: 0x00008658
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000A46A File Offset: 0x0000866A
		public CachePrimingMode PrimingMode
		{
			get
			{
				return (CachePrimingMode)this[CacheWorkSliceCookie.PrimingModeProp];
			}
			set
			{
				this[CacheWorkSliceCookie.PrimingModeProp] = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000A47D File Offset: 0x0000867D
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000A48F File Offset: 0x0000868F
		public string Cookie
		{
			get
			{
				return this[CacheWorkSliceCookie.CookieProp] as string;
			}
			set
			{
				this[CacheWorkSliceCookie.CookieProp] = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000A49D File Offset: 0x0000869D
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000A4AF File Offset: 0x000086AF
		public DateTime LastSyncTime
		{
			get
			{
				return (DateTime)this[CacheWorkSliceCookie.LastSyncTimeProp];
			}
			set
			{
				this[CacheWorkSliceCookie.LastSyncTimeProp] = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000A4C2 File Offset: 0x000086C2
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000A4D4 File Offset: 0x000086D4
		public DateTime LastFullSyncTime
		{
			get
			{
				return (DateTime)this[CacheWorkSliceCookie.LastFullSyncTimeProp];
			}
			set
			{
				this[CacheWorkSliceCookie.LastFullSyncTimeProp] = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000A4E7 File Offset: 0x000086E7
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000A4F9 File Offset: 0x000086F9
		public string CookieFile
		{
			get
			{
				return this[CacheWorkSliceCookie.CookieFileProp] as string;
			}
			set
			{
				this[CacheWorkSliceCookie.CookieFileProp] = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000A507 File Offset: 0x00008707
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000A519 File Offset: 0x00008719
		public long CookieOffset
		{
			get
			{
				return (long)this[CacheWorkSliceCookie.CookieOffsetProp];
			}
			set
			{
				this[CacheWorkSliceCookie.CookieOffsetProp] = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000A52C File Offset: 0x0000872C
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000A53E File Offset: 0x0000873E
		public bool DBRead
		{
			get
			{
				return (bool)this[CacheWorkSliceCookie.DBReadProp];
			}
			set
			{
				this[CacheWorkSliceCookie.DBReadProp] = value;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000A551 File Offset: 0x00008751
		public static string ConstructCacheIdentity(string entityName, int partitionIndex, CachePrimingMode primingMode)
		{
			return string.Format("{0}:{1}:{2}", entityName, partitionIndex, primingMode);
		}

		// Token: 0x04000229 RID: 553
		protected static readonly HygienePropertyDefinition EntityNameProp = CacheObjectSchema.EntityNameProp;

		// Token: 0x0400022A RID: 554
		protected static readonly HygienePropertyDefinition LastSyncTimeProp = CacheObjectSchema.LastSyncTimeProp;

		// Token: 0x0400022B RID: 555
		protected static readonly HygienePropertyDefinition LastFullSyncTimeProp = CacheObjectSchema.LastFullSyncTimeProp;

		// Token: 0x0400022C RID: 556
		protected static readonly HygienePropertyDefinition PartitionIndexProp = new HygienePropertyDefinition("PartitionIndex", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400022D RID: 557
		protected static readonly HygienePropertyDefinition PrimingModeProp = new HygienePropertyDefinition("PrimingMode", typeof(CachePrimingMode));

		// Token: 0x0400022E RID: 558
		protected static readonly HygienePropertyDefinition CookieProp = new HygienePropertyDefinition("Cookie", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400022F RID: 559
		protected static readonly HygienePropertyDefinition CookieFileProp = new HygienePropertyDefinition("CookieFile", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000230 RID: 560
		protected static readonly HygienePropertyDefinition CookieOffsetProp = new HygienePropertyDefinition("CookieOffset", typeof(long), -1L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000231 RID: 561
		protected static readonly HygienePropertyDefinition DBReadProp = new HygienePropertyDefinition("DBRead", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
