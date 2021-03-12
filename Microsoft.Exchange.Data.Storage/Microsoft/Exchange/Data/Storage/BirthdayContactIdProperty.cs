using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C1E RID: 3102
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class BirthdayContactIdProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E67 RID: 28263 RVA: 0x001DABF8 File Offset: 0x001D8DF8
		public BirthdayContactIdProperty() : base("BirthdayContactId", typeof(StoreObjectId), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(BirthdayContactIdProperty.EnclosedPropertyDefinition, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006E68 RID: 28264 RVA: 0x001DAC36 File Offset: 0x001D8E36
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return IdProperty.NativeIdFilterToSmartIdFilter(filter, this, BirthdayContactIdProperty.EnclosedPropertyDefinition);
		}

		// Token: 0x06006E69 RID: 28265 RVA: 0x001DAC44 File Offset: 0x001D8E44
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
		}

		// Token: 0x06006E6A RID: 28266 RVA: 0x001DAC56 File Offset: 0x001D8E56
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return IdProperty.SmartIdFilterToNativeIdFilter(filter, this, BirthdayContactIdProperty.EnclosedPropertyDefinition);
		}

		// Token: 0x06006E6B RID: 28267 RVA: 0x001DAC64 File Offset: 0x001D8E64
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			StoreObjectId storeObjectId = value as StoreObjectId;
			if (storeObjectId == null)
			{
				throw new ArgumentException("value");
			}
			propertyBag.SetValue(BirthdayContactIdProperty.EnclosedPropertyDefinition, storeObjectId.ProviderLevelItemId);
		}

		// Token: 0x06006E6C RID: 28268 RVA: 0x001DAC98 File Offset: 0x001D8E98
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(BirthdayContactIdProperty.EnclosedPropertyDefinition);
			if (value is byte[])
			{
				return StoreObjectId.FromProviderSpecificId(value as byte[], StoreObjectType.Contact);
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x001DACCF File Offset: 0x001D8ECF
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return BirthdayContactIdProperty.EnclosedPropertyDefinition;
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x06006E6E RID: 28270 RVA: 0x001DACD6 File Offset: 0x001D8ED6
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x040040BB RID: 16571
		private static readonly NativeStorePropertyDefinition EnclosedPropertyDefinition = InternalSchema.BirthdayContactEntryId;
	}
}
