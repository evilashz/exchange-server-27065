using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200008C RID: 140
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IDirectPropertyBag
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060009AF RID: 2479
		PropertyBagContext Context { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060009B0 RID: 2480
		bool IsNew { get; }

		// Token: 0x060009B1 RID: 2481
		void SetValue(StorePropertyDefinition propertyDefinition, object propertyValue);

		// Token: 0x060009B2 RID: 2482
		object GetValue(StorePropertyDefinition propertyDefinition);

		// Token: 0x060009B3 RID: 2483
		void Delete(StorePropertyDefinition propertyDefinition);

		// Token: 0x060009B4 RID: 2484
		bool IsLoaded(NativeStorePropertyDefinition propertyDefinition);

		// Token: 0x060009B5 RID: 2485
		bool IsDirty(AtomicStorePropertyDefinition propertyDefinition);
	}
}
