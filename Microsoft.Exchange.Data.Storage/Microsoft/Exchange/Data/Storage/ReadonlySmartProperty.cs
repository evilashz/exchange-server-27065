using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CB1 RID: 3249
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ReadonlySmartProperty : SmartPropertyDefinition
	{
		// Token: 0x0600712C RID: 28972 RVA: 0x001F5ED4 File Offset: 0x001F40D4
		internal ReadonlySmartProperty(NativeStorePropertyDefinition propertyDefinition) : base(propertyDefinition.Name, propertyDefinition.Type, PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(propertyDefinition, PropertyDependencyType.NeedForRead)
		})
		{
			this.enclosedProperty = propertyDefinition;
		}

		// Token: 0x0600712D RID: 28973 RVA: 0x001F5F12 File Offset: 0x001F4112
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(this.enclosedProperty);
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x001F5F21 File Offset: 0x001F4121
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, this.enclosedProperty);
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x001F5F30 File Offset: 0x001F4130
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, this.enclosedProperty);
		}

		// Token: 0x06007130 RID: 28976 RVA: 0x001F5F3F File Offset: 0x001F413F
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return this.enclosedProperty;
		}

		// Token: 0x06007131 RID: 28977 RVA: 0x001F5F47 File Offset: 0x001F4147
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(SinglePropertyFilter));
		}

		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x06007132 RID: 28978 RVA: 0x001F5F59 File Offset: 0x001F4159
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return this.enclosedProperty.Capabilities;
			}
		}

		// Token: 0x04004EA0 RID: 20128
		private readonly NativeStorePropertyDefinition enclosedProperty;
	}
}
