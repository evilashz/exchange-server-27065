using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000B5 RID: 181
	internal class ObjectPropertyLookup : PropertyCache, IPropertyLookup
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x00019504 File Offset: 0x00017704
		public ObjectPropertyLookup(Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]> getProperties, IDictionary<ADObjectId, ADRawEntry> propertyCache) : base(getProperties, SyncObject.BackSyncProperties.Cast<PropertyDefinition>().ToArray<PropertyDefinition>(), propertyCache)
		{
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001951D File Offset: 0x0001771D
		public ObjectPropertyLookup(Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]> getProperties) : this(getProperties, new Dictionary<ADObjectId, ADRawEntry>())
		{
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001952C File Offset: 0x0001772C
		public override IEnumerable<ADObjectId> GetObjectIds(PropertyBag propertyBag)
		{
			return new ADObjectId[]
			{
				(ADObjectId)propertyBag[ADObjectSchema.Id]
			};
		}
	}
}
