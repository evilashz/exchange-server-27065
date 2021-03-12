using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000091 RID: 145
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICorePropertyBag : ILocationIdentifierSetter
	{
		// Token: 0x06000A13 RID: 2579
		object TryGetProperty(PropertyDefinition propertyDefinition);

		// Token: 0x06000A14 RID: 2580
		void SetProperty(PropertyDefinition propertyDefinition, object value);

		// Token: 0x06000A15 RID: 2581
		void Delete(PropertyDefinition propertyDefinition);

		// Token: 0x06000A16 RID: 2582
		Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode);

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A17 RID: 2583
		ICollection<PropertyDefinition> AllFoundProperties { get; }

		// Token: 0x170001EC RID: 492
		object this[PropertyDefinition propertyDefinition]
		{
			get;
			set;
		}

		// Token: 0x06000A1A RID: 2586
		T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition);

		// Token: 0x06000A1B RID: 2587
		T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue);

		// Token: 0x06000A1C RID: 2588
		T? GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition) where T : struct;

		// Token: 0x06000A1D RID: 2589
		bool IsPropertyDirty(PropertyDefinition propertyDefinition);

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A1E RID: 2590
		bool IsDirty { get; }

		// Token: 0x06000A1F RID: 2591
		void Load(ICollection<PropertyDefinition> propsToLoad);

		// Token: 0x06000A20 RID: 2592
		void Reload();

		// Token: 0x06000A21 RID: 2593
		void Clear();
	}
}
