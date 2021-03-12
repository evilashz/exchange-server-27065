using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C63 RID: 3171
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class FolderItemIdProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FAB RID: 28587 RVA: 0x001E0F10 File Offset: 0x001DF110
		internal FolderItemIdProperty(NativeStorePropertyDefinition propertyDefinition, string displayName) : base(displayName, typeof(StoreObjectId), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(propertyDefinition, PropertyDependencyType.NeedForRead)
		})
		{
			this.enclosedPropertyDefinition = propertyDefinition;
			if (propertyDefinition.Type != typeof(byte[]))
			{
				ExDiagnostics.FailFast("Can't create FolderItemIdProperty on non byte[] typed properties", false);
			}
		}

		// Token: 0x06006FAC RID: 28588 RVA: 0x001E0F70 File Offset: 0x001DF170
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(this.enclosedPropertyDefinition);
			if (value is byte[])
			{
				return StoreObjectId.FromProviderSpecificId((byte[])value, StoreObjectType.Unknown);
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006FAD RID: 28589 RVA: 0x001E0FA7 File Offset: 0x001DF1A7
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return IdProperty.SmartIdFilterToNativeIdFilter(filter, this, this.enclosedPropertyDefinition);
		}

		// Token: 0x06006FAE RID: 28590 RVA: 0x001E0FB6 File Offset: 0x001DF1B6
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return IdProperty.NativeIdFilterToSmartIdFilter(filter, this, this.enclosedPropertyDefinition);
		}

		// Token: 0x06006FAF RID: 28591 RVA: 0x001E0FC5 File Offset: 0x001DF1C5
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
		}

		// Token: 0x040043B1 RID: 17329
		private NativeStorePropertyDefinition enclosedPropertyDefinition;
	}
}
