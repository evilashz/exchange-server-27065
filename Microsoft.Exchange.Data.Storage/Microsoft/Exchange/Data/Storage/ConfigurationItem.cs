using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000876 RID: 2166
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConfigurationItem : Item, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x060051A2 RID: 20898 RVA: 0x00154CD8 File Offset: 0x00152ED8
		internal ConfigurationItem(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x00154CE2 File Offset: 0x00152EE2
		internal new static ConfigurationItem Bind(StoreSession session, StoreId id)
		{
			return ItemBuilder.ItemBind<ConfigurationItem>(session, id, ConfigurationItemSchema.Instance, null);
		}
	}
}
