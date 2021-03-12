using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorStoreObject : IDisposable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001FE RID: 510
		StoreObjectId Id { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001FF RID: 511
		ExDateTime CreationTime { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000200 RID: 512
		string Name { get; }

		// Token: 0x06000201 RID: 513
		void Delete(PropertyDefinition propertyDefinition);

		// Token: 0x06000202 RID: 514
		T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue);

		// Token: 0x06000203 RID: 515
		void Load(ICollection<PropertyDefinition> properties);

		// Token: 0x06000204 RID: 516
		void OpenAsReadWrite();

		// Token: 0x06000205 RID: 517
		void LoadMessageIdProperties();

		// Token: 0x06000206 RID: 518
		void Save(SaveMode saveMode);
	}
}
