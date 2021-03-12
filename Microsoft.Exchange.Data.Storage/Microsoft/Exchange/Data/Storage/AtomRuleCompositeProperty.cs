using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C13 RID: 3091
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal abstract class AtomRuleCompositeProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E38 RID: 28216 RVA: 0x001DA01A File Offset: 0x001D821A
		protected AtomRuleCompositeProperty(string displayName, NativeStorePropertyDefinition compositeProperty, IList<NativeStorePropertyDefinition> atomAndRuleProperties) : base(displayName, typeof(string), PropertyFlags.None, PropertyDefinitionConstraint.None, AtomRuleCompositeProperty.GetDependencies(compositeProperty, atomAndRuleProperties))
		{
			this.CompositeProperty = compositeProperty;
			this.atomAndRuleProperties = new ReadOnlyCollection<NativeStorePropertyDefinition>(atomAndRuleProperties);
			this.RegisterFilterTranslation();
		}

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x06006E39 RID: 28217 RVA: 0x001DA053 File Offset: 0x001D8253
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return base.Capabilities | StorePropertyCapabilities.CanQuery | StorePropertyCapabilities.CanSortBy | StorePropertyCapabilities.CanGroupBy;
			}
		}

		// Token: 0x06006E3A RID: 28218 RVA: 0x001DA061 File Offset: 0x001D8261
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return this.CompositeProperty;
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x001DA069 File Offset: 0x001D8269
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(this.CompositeProperty);
		}

		// Token: 0x06006E3C RID: 28220 RVA: 0x001DA078 File Offset: 0x001D8278
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (!propertyBag.CanIgnoreUnchangedProperties || (this.CompositeProperty.PropertyFlags & PropertyFlags.SetIfNotChanged) == PropertyFlags.SetIfNotChanged || !propertyBag.IsLoaded(this.CompositeProperty) || !object.Equals(this.InternalTryGetValue(propertyBag), value))
			{
				propertyBag.SetValueWithFixup(this.CompositeProperty, value);
			}
		}

		// Token: 0x06006E3D RID: 28221 RVA: 0x001DA0CC File Offset: 0x001D82CC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			string text = propertyBag.GetValueOrDefault<string>(this.CompositeProperty);
			if (!propertyBag.IsDirty(this.CompositeProperty) && this.IsAtomOrRulePropertyDirty(propertyBag))
			{
				text = this.GenerateCompositePropertyValue(propertyBag);
			}
			return text ?? new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006E3E RID: 28222 RVA: 0x001DA113 File Offset: 0x001D8313
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, this.CompositeProperty);
		}

		// Token: 0x06006E3F RID: 28223 RVA: 0x001DA122 File Offset: 0x001D8322
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, this.CompositeProperty);
		}

		// Token: 0x06006E40 RID: 28224 RVA: 0x001DA131 File Offset: 0x001D8331
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(TextFilter));
		}

		// Token: 0x06006E41 RID: 28225 RVA: 0x001DA164 File Offset: 0x001D8364
		internal void UpdateCompositePropertyValue(PropertyBag propertyBag)
		{
			PropertyBag.BasicPropertyStore propertyBag2 = (PropertyBag.BasicPropertyStore)propertyBag;
			if (!propertyBag.IsPropertyDirty(this.CompositeProperty) && this.IsAtomOrRulePropertyDirty(propertyBag2))
			{
				string text = this.GenerateCompositePropertyValue(propertyBag2);
				if (text != null)
				{
					propertyBag[this.CompositeProperty] = text;
					return;
				}
				propertyBag.Delete(this.CompositeProperty);
			}
		}

		// Token: 0x06006E42 RID: 28226
		protected abstract string GenerateCompositePropertyValue(PropertyBag.BasicPropertyStore propertyBag);

		// Token: 0x06006E43 RID: 28227 RVA: 0x001DA1B4 File Offset: 0x001D83B4
		protected bool IsAtomOrRulePropertyDirty(PropertyBag.BasicPropertyStore propertyBag)
		{
			foreach (NativeStorePropertyDefinition propertyDefinition in this.atomAndRuleProperties)
			{
				if (propertyBag.IsDirty(propertyDefinition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006E44 RID: 28228 RVA: 0x001DA20C File Offset: 0x001D840C
		private static PropertyDependency[] GetDependencies(NativeStorePropertyDefinition compositeProperty, IList<NativeStorePropertyDefinition> atomOrRuleProperties)
		{
			List<PropertyDependency> list = new List<PropertyDependency>(atomOrRuleProperties.Count + 1);
			foreach (NativeStorePropertyDefinition property in atomOrRuleProperties)
			{
				list.Add(new PropertyDependency(property, PropertyDependencyType.AllRead));
			}
			list.Add(new PropertyDependency(compositeProperty, PropertyDependencyType.AllRead));
			return list.ToArray();
		}

		// Token: 0x0400404C RID: 16460
		protected readonly NativeStorePropertyDefinition CompositeProperty;

		// Token: 0x0400404D RID: 16461
		private readonly IList<NativeStorePropertyDefinition> atomAndRuleProperties;

		// Token: 0x02000C14 RID: 3092
		protected class FormattedSentenceContext : FormattedSentence.Context
		{
			// Token: 0x06006E45 RID: 28229 RVA: 0x001DA27C File Offset: 0x001D847C
			public FormattedSentenceContext(PropertyBag.BasicPropertyStore propertyBag, Dictionary<string, NativeStorePropertyDefinition> placeholderCodeToPropDef)
			{
				this.propertyBag = propertyBag;
				this.placeholderCodeToPropDef = placeholderCodeToPropDef;
			}

			// Token: 0x06006E46 RID: 28230 RVA: 0x001DA294 File Offset: 0x001D8494
			public override string ResolvePlaceholder(string code)
			{
				NativeStorePropertyDefinition propertyDefinition;
				if (!this.placeholderCodeToPropDef.TryGetValue(code, out propertyDefinition))
				{
					throw new FormatException("Invalid placeholder code: " + code);
				}
				return this.propertyBag.GetValue(propertyDefinition) as string;
			}

			// Token: 0x0400404E RID: 16462
			private readonly PropertyBag.BasicPropertyStore propertyBag;

			// Token: 0x0400404F RID: 16463
			private readonly Dictionary<string, NativeStorePropertyDefinition> placeholderCodeToPropDef;
		}
	}
}
