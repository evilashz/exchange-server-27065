using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000A8 RID: 168
	internal sealed class BatchLookup : IDataProcessor
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x00018514 File Offset: 0x00016714
		internal BatchLookup(IDataProcessor next, PropertyCache propertyCache)
		{
			this.next = next;
			this.propertyCache = propertyCache;
			this.dataObjects = new List<PropertyBag>();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00018538 File Offset: 0x00016738
		public void Process(PropertyBag propertyBag)
		{
			this.dataObjects.Add(propertyBag);
			IEnumerable<ADObjectId> objectIds = this.propertyCache.GetObjectIds(propertyBag);
			foreach (ADObjectId objectId in objectIds)
			{
				this.propertyCache.Enqueue(objectId);
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000185A0 File Offset: 0x000167A0
		public void Flush(Func<byte[]> getCookieDelegate, bool moreData)
		{
			this.propertyCache.LookupData();
			this.PropagateData();
			this.next.Flush(getCookieDelegate, moreData);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000185C0 File Offset: 0x000167C0
		private void PropagateData()
		{
			foreach (PropertyBag propertyBag in this.dataObjects)
			{
				this.next.Process(propertyBag);
			}
		}

		// Token: 0x040002C8 RID: 712
		private readonly IDataProcessor next;

		// Token: 0x040002C9 RID: 713
		private readonly List<PropertyBag> dataObjects;

		// Token: 0x040002CA RID: 714
		private readonly PropertyCache propertyCache;
	}
}
