using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF5 RID: 2805
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OccurrenceBagFactory : IPropertyBagFactory
	{
		// Token: 0x060065E2 RID: 26082 RVA: 0x001B0DD0 File Offset: 0x001AEFD0
		internal OccurrenceBagFactory(StoreSession storeSession, OccurrenceStoreObjectId occurrenceUniqueItemId)
		{
			this.storeSession = storeSession;
			this.exTimeZone = storeSession.ExTimeZone;
			this.occurrenceUniqueItemId = occurrenceUniqueItemId;
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x001B0DF4 File Offset: 0x001AEFF4
		public PersistablePropertyBag CreateStorePropertyBag(PropertyBag propertyBag, ICollection<PropertyDefinition> propsToReturn)
		{
			OccurrencePropertyBag occurrencePropertyBag = Item.CreateOccurrencePropertyBag(this.storeSession, this.occurrenceUniqueItemId, propsToReturn);
			occurrencePropertyBag.ExTimeZone = this.exTimeZone;
			return occurrencePropertyBag;
		}

		// Token: 0x040039E4 RID: 14820
		private readonly StoreSession storeSession;

		// Token: 0x040039E5 RID: 14821
		private readonly OccurrenceStoreObjectId occurrenceUniqueItemId;

		// Token: 0x040039E6 RID: 14822
		private readonly ExTimeZone exTimeZone;
	}
}
