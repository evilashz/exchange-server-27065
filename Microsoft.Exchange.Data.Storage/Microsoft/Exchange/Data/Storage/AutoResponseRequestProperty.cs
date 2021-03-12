using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C18 RID: 3096
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class AutoResponseRequestProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E51 RID: 28241 RVA: 0x001DA77C File Offset: 0x001D897C
		internal AutoResponseRequestProperty(string displayName, NativeStorePropertyDefinition nativeProp) : base(displayName, nativeProp.Type, PropertyFlags.Sortable, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(nativeProp, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.AutoResponseSuppressInternal, PropertyDependencyType.NeedToReadForWrite)
		})
		{
			this.nativeProp = nativeProp;
			this.suppressMask = AutoResponseSuppressProperty.SuppressionMapping[nativeProp];
		}

		// Token: 0x06006E52 RID: 28242 RVA: 0x001DA7D4 File Offset: 0x001D89D4
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(this.nativeProp);
		}

		// Token: 0x06006E53 RID: 28243 RVA: 0x001DA7E3 File Offset: 0x001D89E3
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValueWithFixup(this.nativeProp, (bool)value && !this.IsSuppressed(propertyBag));
		}

		// Token: 0x06006E54 RID: 28244 RVA: 0x001DA80C File Offset: 0x001D8A0C
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return this.nativeProp;
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x06006E55 RID: 28245 RVA: 0x001DA814 File Offset: 0x001D8A14
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006E56 RID: 28246 RVA: 0x001DA817 File Offset: 0x001D8A17
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, this.nativeProp);
		}

		// Token: 0x06006E57 RID: 28247 RVA: 0x001DA828 File Offset: 0x001D8A28
		private bool IsSuppressed(PropertyBag.BasicPropertyStore propertyBag)
		{
			int num;
			return Util.TryConvertTo<int>(propertyBag.GetValue(InternalSchema.AutoResponseSuppressInternal), out num) && (num & (int)this.suppressMask) == (int)this.suppressMask;
		}

		// Token: 0x04004097 RID: 16535
		private readonly NativeStorePropertyDefinition nativeProp;

		// Token: 0x04004098 RID: 16536
		private readonly AutoResponseSuppress suppressMask;
	}
}
