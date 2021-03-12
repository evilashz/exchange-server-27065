using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors
{
	// Token: 0x0200006C RID: 108
	internal class DelegatedStoragePropertyAccessor<TStoreObject, TValue> : DelegatedPropertyAccessor<TStoreObject, TValue>, IStoragePropertyAccessor<TStoreObject, TValue>, IPropertyValueCollectionAccessor<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, TValue>, IPropertyAccessor<TStoreObject, TValue>
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00007FE6 File Offset: 0x000061E6
		public DelegatedStoragePropertyAccessor(DelegatedPropertyAccessor<TStoreObject, TValue>.TryGetValueFunc getterDelegate, Action<TStoreObject, TValue> setterDelegate = null, DelegatedStoragePropertyAccessor<TStoreObject, TValue>.TryGetValueFromCollectionFunc propertyValueCollectionGetterDelegate = null, PropertyChangeMetadata.PropertyGroup propertyChangeMetadataGroup = null, params Microsoft.Exchange.Data.PropertyDefinition[] dependencies) : base(getterDelegate, setterDelegate)
		{
			this.propertyValueCollectionGetterDelegate = propertyValueCollectionGetterDelegate;
			this.PropertyChangeMetadataGroup = propertyChangeMetadataGroup;
			this.Dependencies = ((dependencies.Length == 0) ? ((IEnumerable<Microsoft.Exchange.Data.PropertyDefinition>)propertyChangeMetadataGroup) : dependencies);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00008016 File Offset: 0x00006216
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000801E File Offset: 0x0000621E
		public PropertyChangeMetadata.PropertyGroup PropertyChangeMetadataGroup { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00008027 File Offset: 0x00006227
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000802F File Offset: 0x0000622F
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> Dependencies { get; private set; }

		// Token: 0x06000256 RID: 598 RVA: 0x00008038 File Offset: 0x00006238
		public bool TryGetValue(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, out TValue value)
		{
			value = default(TValue);
			return this.propertyValueCollectionGetterDelegate != null && this.propertyValueCollectionGetterDelegate(propertyIndices, values, out value);
		}

		// Token: 0x040000C0 RID: 192
		private readonly DelegatedStoragePropertyAccessor<TStoreObject, TValue>.TryGetValueFromCollectionFunc propertyValueCollectionGetterDelegate;

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x06000258 RID: 600
		public delegate bool TryGetValueFromCollectionFunc(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, out TValue value);
	}
}
