using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors
{
	// Token: 0x02000068 RID: 104
	internal interface IStoragePropertyAccessor<in TStoreObject, TValue> : IPropertyAccessor<TStoreObject, TValue>
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000240 RID: 576
		PropertyChangeMetadata.PropertyGroup PropertyChangeMetadataGroup { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000241 RID: 577
		IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> Dependencies { get; }
	}
}
