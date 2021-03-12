using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CD9 RID: 3289
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class SubjectPortionProperty : SmartPropertyDefinition
	{
		// Token: 0x060071E9 RID: 29161 RVA: 0x001F8A58 File Offset: 0x001F6C58
		internal SubjectPortionProperty(string displayName, NativeStorePropertyDefinition nativeProperty) : base(displayName, typeof(string), PropertyFlags.None, Array<PropertyDefinitionConstraint>.Empty, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiSubject, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.NormalizedSubjectInternal, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.SubjectPrefixInternal, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(nativeProperty, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedToReadForWrite)
		})
		{
			this.nativeProperty = nativeProperty;
		}

		// Token: 0x060071EA RID: 29162 RVA: 0x001F8ACD File Offset: 0x001F6CCD
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(this.nativeProperty);
		}

		// Token: 0x060071EB RID: 29163 RVA: 0x001F8ADC File Offset: 0x001F6CDC
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentNullException("value");
			}
			SubjectProperty.ModifySubjectProperty(propertyBag, this.nativeProperty, text);
		}

		// Token: 0x060071EC RID: 29164 RVA: 0x001F8B0B File Offset: 0x001F6D0B
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(this.nativeProperty);
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x001F8B1A File Offset: 0x001F6D1A
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, this.nativeProperty);
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x001F8B29 File Offset: 0x001F6D29
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, this.nativeProperty);
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x001F8B38 File Offset: 0x001F6D38
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(TextFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x060071F0 RID: 29168 RVA: 0x001F8B6A File Offset: 0x001F6D6A
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x001F8B6D File Offset: 0x001F6D6D
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return this.nativeProperty;
		}

		// Token: 0x04004F13 RID: 20243
		private readonly NativeStorePropertyDefinition nativeProperty;
	}
}
