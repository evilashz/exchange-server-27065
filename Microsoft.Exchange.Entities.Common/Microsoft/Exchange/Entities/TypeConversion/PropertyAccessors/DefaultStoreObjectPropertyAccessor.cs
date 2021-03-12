using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors
{
	// Token: 0x0200006B RID: 107
	internal class DefaultStoreObjectPropertyAccessor<TStoreObject, TValue> : DefaultStoragePropertyAccessor<TStoreObject, TValue> where TStoreObject : IStoreObject
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00007FA0 File Offset: 0x000061A0
		public DefaultStoreObjectPropertyAccessor(uint locationIdentifier, StorePropertyDefinition property, bool forceReadonly = false) : base(property, forceReadonly)
		{
			this.locationIdentifier = locationIdentifier;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007FB4 File Offset: 0x000061B4
		protected override void PerformSet(TStoreObject container, TValue value)
		{
			LocationIdentifierHelper locationIdentifierHelperInstance = container.LocationIdentifierHelperInstance;
			if (locationIdentifierHelperInstance != null)
			{
				locationIdentifierHelperInstance.SetLocationIdentifier(this.locationIdentifier);
			}
			base.PerformSet(container, value);
		}

		// Token: 0x040000BF RID: 191
		private readonly uint locationIdentifier;
	}
}
