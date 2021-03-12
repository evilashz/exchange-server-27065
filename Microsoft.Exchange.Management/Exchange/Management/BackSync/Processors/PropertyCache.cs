using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000B0 RID: 176
	internal abstract class PropertyCache
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x00018D0C File Offset: 0x00016F0C
		protected PropertyCache(Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]> getProperties, PropertyDefinition[] properties) : this(getProperties, properties, new Dictionary<ADObjectId, ADRawEntry>())
		{
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018D1B File Offset: 0x00016F1B
		protected PropertyCache(Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]> getProperties, PropertyDefinition[] properties, IDictionary<ADObjectId, ADRawEntry> propertyCache)
		{
			this.properties = properties;
			this.getProperties = getProperties;
			this.propertyCache = propertyCache;
			this.lookupQueue = new HashSet<ADObjectId>();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00018D44 File Offset: 0x00016F44
		public ADRawEntry GetProperties(ADObjectId objectId)
		{
			if (!this.propertyCache.ContainsKey(objectId))
			{
				this.LookupData(new ADObjectId[]
				{
					objectId
				});
			}
			return this.propertyCache[objectId];
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00018D7D File Offset: 0x00016F7D
		public void Enqueue(ADObjectId objectId)
		{
			if (!this.IsInCache(objectId) && !this.IsQueuedForLookup(objectId))
			{
				this.EnqueueForLookup(objectId);
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00018D98 File Offset: 0x00016F98
		public void LookupData()
		{
			ADObjectId[] ids = this.lookupQueue.ToArray<ADObjectId>();
			this.LookupData(ids);
		}

		// Token: 0x060005C5 RID: 1477
		public abstract IEnumerable<ADObjectId> GetObjectIds(PropertyBag propertyBag);

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018DB8 File Offset: 0x00016FB8
		protected virtual bool MeetsAdditionalCriteria(ADRawEntry entry)
		{
			return true;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00018DBC File Offset: 0x00016FBC
		private void LookupData(ADObjectId[] ids)
		{
			Result<ADRawEntry>[] array = this.getProperties(ids, this.properties);
			for (int i = 0; i < ids.Length; i++)
			{
				ADRawEntry adrawEntry = array[i].Data;
				if (adrawEntry != null && !this.MeetsAdditionalCriteria(adrawEntry))
				{
					adrawEntry = null;
				}
				this.propertyCache[ids[i]] = adrawEntry;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00018E14 File Offset: 0x00017014
		private void EnqueueForLookup(ADObjectId objectId)
		{
			this.lookupQueue.Add(objectId);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00018E23 File Offset: 0x00017023
		private bool IsInCache(ADObjectId objectId)
		{
			return this.propertyCache.ContainsKey(objectId);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00018E31 File Offset: 0x00017031
		private bool IsQueuedForLookup(ADObjectId objectId)
		{
			return this.lookupQueue.Contains(objectId);
		}

		// Token: 0x040002D2 RID: 722
		private readonly Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]> getProperties;

		// Token: 0x040002D3 RID: 723
		private readonly IDictionary<ADObjectId, ADRawEntry> propertyCache;

		// Token: 0x040002D4 RID: 724
		private readonly HashSet<ADObjectId> lookupQueue;

		// Token: 0x040002D5 RID: 725
		private readonly PropertyDefinition[] properties;
	}
}
