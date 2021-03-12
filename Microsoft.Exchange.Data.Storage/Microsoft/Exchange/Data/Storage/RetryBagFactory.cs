using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AFC RID: 2812
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RetryBagFactory : IPropertyBagFactory
	{
		// Token: 0x06006624 RID: 26148 RVA: 0x001B1543 File Offset: 0x001AF743
		internal RetryBagFactory(StoreSession storeSession)
		{
			this.storeSession = storeSession;
			this.exTimeZone = storeSession.ExTimeZone;
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x001B1560 File Offset: 0x001AF760
		public PersistablePropertyBag CreateStorePropertyBag(PropertyBag propertyBag, ICollection<PropertyDefinition> prefetchProperties)
		{
			byte[] entryId = propertyBag.TryGetProperty(InternalSchema.EntryId) as byte[];
			StoreObjectId id = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Unknown);
			StoreObjectPropertyBag storeObjectPropertyBag = propertyBag as StoreObjectPropertyBag;
			ICollection<PropertyDefinition> prefetchPropertyArray;
			if (storeObjectPropertyBag != null)
			{
				prefetchPropertyArray = ((prefetchProperties != null) ? prefetchProperties.Union(storeObjectPropertyBag.PrefetchPropertyArray) : storeObjectPropertyBag.PrefetchPropertyArray);
			}
			else
			{
				prefetchPropertyArray = prefetchProperties;
			}
			PersistablePropertyBag persistablePropertyBag = ItemBagFactory.CreatePropertyBag(this.storeSession, id, prefetchPropertyArray);
			persistablePropertyBag.ExTimeZone = this.exTimeZone;
			return persistablePropertyBag;
		}

		// Token: 0x04003A07 RID: 14855
		private readonly StoreSession storeSession;

		// Token: 0x04003A08 RID: 14856
		private readonly ExTimeZone exTimeZone;
	}
}
