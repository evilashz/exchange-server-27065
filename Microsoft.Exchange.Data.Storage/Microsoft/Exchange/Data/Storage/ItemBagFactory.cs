using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200084D RID: 2125
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ItemBagFactory : IPropertyBagFactory
	{
		// Token: 0x06004F24 RID: 20260 RVA: 0x0014B7B2 File Offset: 0x001499B2
		internal ItemBagFactory(StoreSession storeSession, StoreObjectId id)
		{
			this.storeSession = storeSession;
			this.exTimeZone = storeSession.ExTimeZone;
			this.id = id;
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x0014B7D4 File Offset: 0x001499D4
		public PersistablePropertyBag CreateStorePropertyBag(PropertyBag propertyBag, ICollection<PropertyDefinition> prefetchPropertyArray)
		{
			PersistablePropertyBag persistablePropertyBag = ItemBagFactory.CreatePropertyBag(this.storeSession, this.id, prefetchPropertyArray);
			persistablePropertyBag.ExTimeZone = this.exTimeZone;
			return persistablePropertyBag;
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x0014B804 File Offset: 0x00149A04
		internal static StoreObjectPropertyBag CreatePropertyBag(StoreSession storeSession, StoreObjectId id, ICollection<PropertyDefinition> prefetchPropertyArray)
		{
			MapiProp mapiProp = null;
			StoreObjectPropertyBag storeObjectPropertyBag = null;
			bool flag = false;
			StoreObjectPropertyBag result;
			try
			{
				mapiProp = storeSession.GetMapiProp(id);
				storeObjectPropertyBag = new StoreObjectPropertyBag(storeSession, mapiProp, prefetchPropertyArray);
				flag = true;
				result = storeObjectPropertyBag;
			}
			finally
			{
				if (!flag)
				{
					if (storeObjectPropertyBag != null)
					{
						storeObjectPropertyBag.Dispose();
						storeObjectPropertyBag = null;
					}
					if (mapiProp != null)
					{
						mapiProp.Dispose();
						mapiProp = null;
					}
				}
			}
			return result;
		}

		// Token: 0x04002B2A RID: 11050
		private readonly StoreSession storeSession;

		// Token: 0x04002B2B RID: 11051
		private readonly ExTimeZone exTimeZone;

		// Token: 0x04002B2C RID: 11052
		private StoreObjectId id;
	}
}
