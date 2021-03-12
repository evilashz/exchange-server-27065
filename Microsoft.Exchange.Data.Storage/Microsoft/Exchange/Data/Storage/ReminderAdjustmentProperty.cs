using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CB6 RID: 3254
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ReminderAdjustmentProperty : SmartPropertyDefinition
	{
		// Token: 0x06007144 RID: 28996 RVA: 0x001F6660 File Offset: 0x001F4860
		internal ReminderAdjustmentProperty(string displayName, NativeStorePropertyDefinition nativeProp) : base(displayName, nativeProp.Type, PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(nativeProp, PropertyDependencyType.NeedForRead)
		})
		{
			this.nativeProp = nativeProp;
		}

		// Token: 0x06007145 RID: 28997 RVA: 0x001F6699 File Offset: 0x001F4899
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(this.nativeProp);
		}

		// Token: 0x06007146 RID: 28998 RVA: 0x001F66A8 File Offset: 0x001F48A8
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValueWithFixup(this.nativeProp, value);
			Reminder.Adjust(propertyBag.Context.StoreObject);
		}

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x06007147 RID: 28999 RVA: 0x001F66C9 File Offset: 0x001F48C9
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06007148 RID: 29000 RVA: 0x001F66CC File Offset: 0x001F48CC
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return this.nativeProp;
		}

		// Token: 0x06007149 RID: 29001 RVA: 0x001F66D4 File Offset: 0x001F48D4
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, this.nativeProp);
		}

		// Token: 0x0600714A RID: 29002 RVA: 0x001F66E3 File Offset: 0x001F48E3
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, this.nativeProp);
		}

		// Token: 0x04004EA4 RID: 20132
		private readonly NativeStorePropertyDefinition nativeProp;
	}
}
