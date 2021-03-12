using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IStorePropertyBag : IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000022 RID: 34
		bool IsDirty { get; }

		// Token: 0x06000023 RID: 35
		bool IsPropertyDirty(PropertyDefinition propertyDefinition);

		// Token: 0x06000024 RID: 36
		void Load();

		// Token: 0x06000025 RID: 37
		void Load(ICollection<PropertyDefinition> propertyDefinitions);

		// Token: 0x06000026 RID: 38
		Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode);

		// Token: 0x06000027 RID: 39
		object TryGetProperty(PropertyDefinition propertyDefinition);

		// Token: 0x06000028 RID: 40
		void Delete(PropertyDefinition propertyDefinition);

		// Token: 0x06000029 RID: 41
		T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue);

		// Token: 0x0600002A RID: 42
		void SetOrDeleteProperty(PropertyDefinition propertyDefinition, object propertyValue);
	}
}
