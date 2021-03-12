using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.TypeConversion
{
	// Token: 0x02000066 RID: 102
	internal interface IStorageTranslationRule<in TLeft, in TRight> : ITranslationRule<TLeft, TRight>
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000237 RID: 567
		IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> StorageDependencies { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000238 RID: 568
		PropertyChangeMetadata.PropertyGroup StoragePropertyGroup { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000239 RID: 569
		IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> EntityProperties { get; }
	}
}
