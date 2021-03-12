using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000014 RID: 20
	internal class TypeSynchronizer
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000293C File Offset: 0x00000B3C
		public TypeSynchronizer(Filter filterDelegate, PreDecorate preDecorateDelegate, PostDecorate postDecorateDelegate, LoadTargetCache loadTargetCacheDelegate, TargetCacheLookup targetCacheLookupDelegate, TargetCacheRemoveTargetOnlyEntries targetCacheCleanupDelegate, string name, string sourceQueryFilter, string targetQueryFilter, SearchScope searchScope, string[] copyAttributes, string[] filterAttributes, bool skipDeletedEntriesInFullSyncMode)
		{
			int num = 0;
			if (loadTargetCacheDelegate != null)
			{
				num++;
			}
			if (targetCacheLookupDelegate != null)
			{
				num++;
			}
			if (targetCacheCleanupDelegate != null)
			{
				num++;
			}
			if (num != 0 && num != 3)
			{
				throw new ArgumentException("loadTargetCacheDelegate, targetCacheLookupDelegate, and targetCacheCleanupDelegate must be all be set");
			}
			this.filterDelegate = filterDelegate;
			this.preDecorateDelegate = preDecorateDelegate;
			this.postDecorateDelegate = postDecorateDelegate;
			this.loadTargetCacheDelegate = loadTargetCacheDelegate;
			this.targetCacheLookupDelegate = targetCacheLookupDelegate;
			this.targetCacheCleanupDelegate = targetCacheCleanupDelegate;
			this.name = name;
			this.sourceQueryFilter = sourceQueryFilter;
			this.targetQueryFilter = targetQueryFilter;
			this.searchScope = searchScope;
			this.copyAttributes = copyAttributes;
			this.filterAttributes = filterAttributes;
			this.skipDeletedEntriesInFullSyncMode = skipDeletedEntriesInFullSyncMode;
			this.targetCache = new Dictionary<byte[], byte[]>(ArrayComparer<byte>.Comparer);
			this.targetCacheEnabled = false;
			this.targetCacheFullyLoaded = false;
			this.hasTargetCacheFullSyncError = false;
			if (loadTargetCacheDelegate != null)
			{
				this.targetCacheEnabled = true;
			}
			if (!this.skipDeletedEntriesInFullSyncMode && this.targetCacheEnabled)
			{
				throw new InvalidOperationException("Can't set skipDeletedEntriesInFullSyncMode to false when target cache is enabled");
			}
			int num2 = (copyAttributes == null) ? 0 : copyAttributes.Length;
			int num3 = (filterAttributes == null) ? 0 : filterAttributes.Length;
			this.readSourceAttributes = new string[num2 + num3 + TypeSynchronizer.srcDeltaAttributes.Length];
			if (num2 > 0)
			{
				copyAttributes.CopyTo(this.readSourceAttributes, 0);
			}
			if (num3 > 0)
			{
				filterAttributes.CopyTo(this.readSourceAttributes, num2);
			}
			TypeSynchronizer.srcDeltaAttributes.CopyTo(this.readSourceAttributes, num2 + num3);
			this.readTargetAttributes = new string[num2 + TypeSynchronizer.targetDeltaAttributes.Length];
			if (num2 > 0)
			{
				copyAttributes.CopyTo(this.readTargetAttributes, 0);
			}
			TypeSynchronizer.targetDeltaAttributes.CopyTo(this.readTargetAttributes, num2);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002AC7 File Offset: 0x00000CC7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002ACF File Offset: 0x00000CCF
		public string SourceQueryFilter
		{
			get
			{
				return this.sourceQueryFilter;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public string TargetQueryFilter
		{
			get
			{
				return this.targetQueryFilter;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002ADF File Offset: 0x00000CDF
		public string[] CopyAttributes
		{
			get
			{
				return this.copyAttributes;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002AE7 File Offset: 0x00000CE7
		public string[] FilterAttributes
		{
			get
			{
				return this.filterAttributes;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002AEF File Offset: 0x00000CEF
		public bool SkipDeletedEntriesInFullSyncMode
		{
			get
			{
				return this.skipDeletedEntriesInFullSyncMode;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002AF7 File Offset: 0x00000CF7
		public string[] ReadSourceAttributes
		{
			get
			{
				return this.readSourceAttributes;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002AFF File Offset: 0x00000CFF
		public string[] ReadTargetAttributes
		{
			get
			{
				return this.readTargetAttributes;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002B07 File Offset: 0x00000D07
		public SearchScope SearchScope
		{
			get
			{
				return this.searchScope;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002B0F File Offset: 0x00000D0F
		public Dictionary<byte[], byte[]> TargetCache
		{
			get
			{
				return this.targetCache;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002B17 File Offset: 0x00000D17
		public bool TargetCacheEnabled
		{
			get
			{
				return this.targetCacheEnabled;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002B1F File Offset: 0x00000D1F
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002B27 File Offset: 0x00000D27
		public bool TargetCacheFullyLoaded
		{
			get
			{
				return this.targetCacheFullyLoaded;
			}
			set
			{
				this.targetCacheFullyLoaded = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002B30 File Offset: 0x00000D30
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002B38 File Offset: 0x00000D38
		public bool HasTargetCacheFullSyncError
		{
			get
			{
				return this.hasTargetCacheFullSyncError;
			}
			set
			{
				this.hasTargetCacheFullSyncError = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002B41 File Offset: 0x00000D41
		public Filter Filter
		{
			get
			{
				return this.filterDelegate;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002B49 File Offset: 0x00000D49
		public PreDecorate PreDecorate
		{
			get
			{
				return this.preDecorateDelegate;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002B51 File Offset: 0x00000D51
		public PostDecorate PostDecorate
		{
			get
			{
				return this.postDecorateDelegate;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002B59 File Offset: 0x00000D59
		public LoadTargetCache LoadTargetCache
		{
			get
			{
				return this.loadTargetCacheDelegate;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002B61 File Offset: 0x00000D61
		public TargetCacheLookup TargetCacheLookup
		{
			get
			{
				return this.targetCacheLookupDelegate;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002B69 File Offset: 0x00000D69
		public TargetCacheRemoveTargetOnlyEntries TargetCacheRemoveTargetOnlyEntries
		{
			get
			{
				return this.targetCacheCleanupDelegate;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002B71 File Offset: 0x00000D71
		public void ResetTargetCacheState(bool hasTargetCacheFullSyncError)
		{
			this.targetCache.Clear();
			this.targetCacheFullyLoaded = false;
			this.hasTargetCacheFullSyncError = hasTargetCacheFullSyncError;
		}

		// Token: 0x0400001F RID: 31
		private static readonly string[] srcDeltaAttributes = new string[]
		{
			"objectGUID",
			"objectClass",
			"whenCreated",
			"parentGUID",
			"name"
		};

		// Token: 0x04000020 RID: 32
		private static readonly string[] targetDeltaAttributes = new string[]
		{
			"msExchEdgeSyncSourceGuid",
			"name"
		};

		// Token: 0x04000021 RID: 33
		private readonly bool skipDeletedEntriesInFullSyncMode;

		// Token: 0x04000022 RID: 34
		private readonly string name;

		// Token: 0x04000023 RID: 35
		private string sourceQueryFilter;

		// Token: 0x04000024 RID: 36
		private string targetQueryFilter;

		// Token: 0x04000025 RID: 37
		private string[] copyAttributes;

		// Token: 0x04000026 RID: 38
		private string[] filterAttributes;

		// Token: 0x04000027 RID: 39
		private string[] readSourceAttributes;

		// Token: 0x04000028 RID: 40
		private string[] readTargetAttributes;

		// Token: 0x04000029 RID: 41
		private SearchScope searchScope;

		// Token: 0x0400002A RID: 42
		private Dictionary<byte[], byte[]> targetCache;

		// Token: 0x0400002B RID: 43
		private bool targetCacheEnabled;

		// Token: 0x0400002C RID: 44
		private bool targetCacheFullyLoaded;

		// Token: 0x0400002D RID: 45
		private bool hasTargetCacheFullSyncError;

		// Token: 0x0400002E RID: 46
		private Filter filterDelegate;

		// Token: 0x0400002F RID: 47
		private PreDecorate preDecorateDelegate;

		// Token: 0x04000030 RID: 48
		private PostDecorate postDecorateDelegate;

		// Token: 0x04000031 RID: 49
		private LoadTargetCache loadTargetCacheDelegate;

		// Token: 0x04000032 RID: 50
		private TargetCacheLookup targetCacheLookupDelegate;

		// Token: 0x04000033 RID: 51
		private TargetCacheRemoveTargetOnlyEntries targetCacheCleanupDelegate;
	}
}
