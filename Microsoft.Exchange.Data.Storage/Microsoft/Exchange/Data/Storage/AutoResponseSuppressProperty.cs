using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C19 RID: 3097
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class AutoResponseSuppressProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E58 RID: 28248 RVA: 0x001DA85C File Offset: 0x001D8A5C
		internal AutoResponseSuppressProperty() : base("AutoResponseSuppress", InternalSchema.AutoResponseSuppressInternal.Type, PropertyFlags.Sortable, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.AutoResponseSuppressInternal, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006E59 RID: 28249 RVA: 0x001DA89A File Offset: 0x001D8A9A
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(InternalSchema.AutoResponseSuppressInternal);
		}

		// Token: 0x06006E5A RID: 28250 RVA: 0x001DA8A8 File Offset: 0x001D8AA8
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			AutoResponseSuppress autoResponseSuppress = (AutoResponseSuppress)value;
			foreach (KeyValuePair<NativeStorePropertyDefinition, AutoResponseSuppress> keyValuePair in AutoResponseSuppressProperty.SuppressionMapping)
			{
				if ((autoResponseSuppress & keyValuePair.Value) == keyValuePair.Value)
				{
					propertyBag.SetValueWithFixup(keyValuePair.Key, false);
				}
			}
			propertyBag.SetValueWithFixup(InternalSchema.AutoResponseSuppressInternal, (int)value);
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x06006E5B RID: 28251 RVA: 0x001DA938 File Offset: 0x001D8B38
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.CanSortBy | StorePropertyCapabilities.CanGroupBy;
			}
		}

		// Token: 0x06006E5C RID: 28252 RVA: 0x001DA93B File Offset: 0x001D8B3B
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.AutoResponseSuppressInternal;
		}

		// Token: 0x04004099 RID: 16537
		internal static readonly Dictionary<NativeStorePropertyDefinition, AutoResponseSuppress> SuppressionMapping = Util.AddElements<Dictionary<NativeStorePropertyDefinition, AutoResponseSuppress>, KeyValuePair<NativeStorePropertyDefinition, AutoResponseSuppress>>(new Dictionary<NativeStorePropertyDefinition, AutoResponseSuppress>(), new KeyValuePair<NativeStorePropertyDefinition, AutoResponseSuppress>[]
		{
			new KeyValuePair<NativeStorePropertyDefinition, AutoResponseSuppress>(InternalSchema.IsDeliveryReceiptRequestedInternal, AutoResponseSuppress.DR),
			new KeyValuePair<NativeStorePropertyDefinition, AutoResponseSuppress>(InternalSchema.IsNonDeliveryReceiptRequestedInternal, AutoResponseSuppress.NDR),
			new KeyValuePair<NativeStorePropertyDefinition, AutoResponseSuppress>(InternalSchema.IsReadReceiptRequestedInternal, AutoResponseSuppress.RN),
			new KeyValuePair<NativeStorePropertyDefinition, AutoResponseSuppress>(InternalSchema.IsNotReadReceiptRequestedInternal, AutoResponseSuppress.NRN)
		});
	}
}
