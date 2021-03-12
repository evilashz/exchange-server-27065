using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF4 RID: 2804
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReadonlyMemoryPropertyBag : MemoryPropertyBag
	{
		// Token: 0x060065DE RID: 26078 RVA: 0x001B0D5C File Offset: 0x001AEF5C
		public ReadonlyMemoryPropertyBag(IList<StorePropertyDefinition> propertyDefs, object[] propertyValues)
		{
			base.PreLoadStoreProperty<StorePropertyDefinition>(propertyDefs, propertyValues);
			this.locked = !this.locked;
		}

		// Token: 0x060065DF RID: 26079 RVA: 0x001B0D7B File Offset: 0x001AEF7B
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			if (this.locked)
			{
				throw new InvalidOperationException("Can't modify ReadonlyPropertyBag");
			}
			base.DeleteStoreProperty(propertyDefinition);
		}

		// Token: 0x060065E0 RID: 26080 RVA: 0x001B0D97 File Offset: 0x001AEF97
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			if (this.locked)
			{
				throw new InvalidOperationException("Can't modify ReadonlyPropertyBag");
			}
			base.SetValidatedStoreProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x001B0DB4 File Offset: 0x001AEFB4
		public override void Load(ICollection<PropertyDefinition> properties)
		{
			if (this.locked)
			{
				throw new InvalidOperationException("Can't modify ReadonlyPropertyBag");
			}
			base.Load(properties);
		}

		// Token: 0x040039E3 RID: 14819
		private readonly bool locked;
	}
}
