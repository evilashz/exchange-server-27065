using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors
{
	// Token: 0x02000069 RID: 105
	internal abstract class StoragePropertyAccessor<TStoreObject, TValue> : PropertyAccessor<TStoreObject, TValue>, IStoragePropertyAccessor<TStoreObject, TValue>, IPropertyAccessor<TStoreObject, TValue> where TStoreObject : IStorePropertyBag
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00007D54 File Offset: 0x00005F54
		protected StoragePropertyAccessor(bool readOnly, PropertyChangeMetadata.PropertyGroup propertyChangeMetadataGroup = null, IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> dependencies = null) : base(readOnly)
		{
			this.PropertyChangeMetadataGroup = propertyChangeMetadataGroup;
			this.Dependencies = (dependencies ?? ((IEnumerable<Microsoft.Exchange.Data.PropertyDefinition>)propertyChangeMetadataGroup));
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007D75 File Offset: 0x00005F75
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007D7D File Offset: 0x00005F7D
		public PropertyChangeMetadata.PropertyGroup PropertyChangeMetadataGroup { get; protected set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00007D86 File Offset: 0x00005F86
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00007D8E File Offset: 0x00005F8E
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> Dependencies { get; private set; }
	}
}
