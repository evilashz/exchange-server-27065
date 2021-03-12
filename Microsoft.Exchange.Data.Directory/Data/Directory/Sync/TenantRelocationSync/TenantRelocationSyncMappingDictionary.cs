using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x02000800 RID: 2048
	internal class TenantRelocationSyncMappingDictionary
	{
		// Token: 0x06006507 RID: 25863 RVA: 0x0016020E File Offset: 0x0015E40E
		public TenantRelocationSyncMappingDictionary()
		{
			this.InitializeCache();
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0016021C File Offset: 0x0015E41C
		public void Insert(ADObjectId source, ADObjectId target, Guid correlationId)
		{
			DistinguishedNameMapItem value = new DistinguishedNameMapItem(source, target, correlationId);
			if (source != null && !string.IsNullOrEmpty(source.DistinguishedName))
			{
				this.sourceDnDictionary[source.DistinguishedName] = value;
			}
			if (target != null && !string.IsNullOrEmpty(target.DistinguishedName))
			{
				this.targetDnDictionary[target.DistinguishedName] = value;
			}
			if (!correlationId.Equals(Guid.Empty))
			{
				this.correlationGuidDictionary[correlationId] = value;
			}
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x00160294 File Offset: 0x0015E494
		public void Remove(DistinguishedNameMapItem item)
		{
			if (item.SourceDN != null && !string.IsNullOrEmpty(item.SourceDN.DistinguishedName))
			{
				this.sourceDnDictionary[item.SourceDN.DistinguishedName] = null;
			}
			if (item.TargetDN != null && !string.IsNullOrEmpty(item.TargetDN.DistinguishedName))
			{
				this.targetDnDictionary[item.TargetDN.DistinguishedName] = null;
			}
			if (!item.CorrelationId.Equals(Guid.Empty))
			{
				this.correlationGuidDictionary[item.CorrelationId] = null;
			}
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x0016032C File Offset: 0x0015E52C
		public DistinguishedNameMapItem LookupBySourceDn(string source)
		{
			DistinguishedNameMapItem result;
			if (this.sourceDnDictionary.TryGetValue(source, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x0016034C File Offset: 0x0015E54C
		public DistinguishedNameMapItem LookupBySourceADObjectId(ADObjectId id)
		{
			if (!string.IsNullOrEmpty(id.DistinguishedName))
			{
				return this.LookupBySourceDn(id.DistinguishedName);
			}
			if (!Guid.Empty.Equals(id.ObjectGuid))
			{
				return this.LookupByCorrelationGuid(id.ObjectGuid);
			}
			throw new ArgumentException("invalid paramter id");
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x001603A0 File Offset: 0x0015E5A0
		public DistinguishedNameMapItem LookupByTargetDn(string target)
		{
			DistinguishedNameMapItem result;
			if (this.targetDnDictionary.TryGetValue(target, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x001603C0 File Offset: 0x0015E5C0
		public DistinguishedNameMapItem LookupByCorrelationGuid(Guid cId)
		{
			DistinguishedNameMapItem result;
			if (this.correlationGuidDictionary.TryGetValue(cId, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x001603E0 File Offset: 0x0015E5E0
		public void ClearCacheIfNecessary(bool fForce)
		{
			if (fForce || this.correlationGuidDictionary.Count > TenantRelocationSyncMappingDictionary.MaxCacheSize)
			{
				this.InitializeCache();
			}
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x00160400 File Offset: 0x0015E600
		private void InitializeCache()
		{
			lock (this)
			{
				this.sourceDnDictionary = new Dictionary<string, DistinguishedNameMapItem>();
				this.targetDnDictionary = new Dictionary<string, DistinguishedNameMapItem>();
				this.correlationGuidDictionary = new Dictionary<Guid, DistinguishedNameMapItem>();
			}
		}

		// Token: 0x0400431E RID: 17182
		private static readonly int MaxCacheSize = 100000;

		// Token: 0x0400431F RID: 17183
		private Dictionary<string, DistinguishedNameMapItem> sourceDnDictionary;

		// Token: 0x04004320 RID: 17184
		private Dictionary<string, DistinguishedNameMapItem> targetDnDictionary;

		// Token: 0x04004321 RID: 17185
		private Dictionary<Guid, DistinguishedNameMapItem> correlationGuidDictionary;
	}
}
