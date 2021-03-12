using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CD3 RID: 3283
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class StoreObjectIdCollectionProperty : SmartPropertyDefinition
	{
		// Token: 0x060071BD RID: 29117 RVA: 0x001F7D8C File Offset: 0x001F5F8C
		internal StoreObjectIdCollectionProperty(NativeStorePropertyDefinition propertyDefinition, PropertyFlags propertyFlags, string displayName) : base(displayName, typeof(StoreObjectId[]), propertyFlags, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(propertyDefinition, PropertyDependencyType.NeedForRead)
		})
		{
			this.enclosedPropertyDefinition = propertyDefinition;
			if (propertyDefinition.Type != typeof(byte[][]))
			{
				ExDiagnostics.FailFast("Can't create StoreObjectIdCollectionProperty on non byte[][] typed properties", false);
			}
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x001F7DEC File Offset: 0x001F5FEC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			byte[][] array = propertyBag.GetValue(this.enclosedPropertyDefinition) as byte[][];
			if (array != null)
			{
				StoreObjectId[] array2 = new StoreObjectId[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = StoreObjectId.FromProviderSpecificId(array[i], StoreObjectType.Unknown);
				}
				return array2;
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x060071BF RID: 29119 RVA: 0x001F7E3C File Offset: 0x001F603C
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (value != null)
			{
				StoreObjectId[] array = value as StoreObjectId[];
				ArgumentValidator.ThrowIfNull("value", array);
				byte[][] array2 = new byte[array.Length][];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i].ProviderLevelItemId;
				}
				propertyBag.SetValue(this.enclosedPropertyDefinition, array2);
				return;
			}
			propertyBag.Delete(this.enclosedPropertyDefinition);
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x001F7E9C File Offset: 0x001F609C
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				return base.SinglePropertySmartFilterToNativeFilter(filter, this.enclosedPropertyDefinition);
			}
			StoreObjectId storeObjectId = comparisonFilter.PropertyValue as StoreObjectId;
			if (storeObjectId == null)
			{
				throw new NotSupportedException("FolderItemIdCollectionProperty only supports StoreObjectId in filters");
			}
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, this.enclosedPropertyDefinition, storeObjectId.ProviderLevelItemId);
		}

		// Token: 0x17001E67 RID: 7783
		// (get) Token: 0x060071C1 RID: 29121 RVA: 0x001F7EF2 File Offset: 0x001F60F2
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return base.Capabilities | StorePropertyCapabilities.CanQuery;
			}
		}

		// Token: 0x04004F07 RID: 20231
		private readonly NativeStorePropertyDefinition enclosedPropertyDefinition;
	}
}
