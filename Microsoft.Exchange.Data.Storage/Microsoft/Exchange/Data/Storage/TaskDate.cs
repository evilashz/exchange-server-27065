using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CDA RID: 3290
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal abstract class TaskDate : SmartPropertyDefinition
	{
		// Token: 0x060071F2 RID: 29170 RVA: 0x001F8B78 File Offset: 0x001F6D78
		internal TaskDate(string displayName, NativeStorePropertyDefinition firstProperty, NativeStorePropertyDefinition secondProperty) : base(displayName, typeof(ExDateTime), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(firstProperty, PropertyDependencyType.NeedForRead),
			new PropertyDependency(secondProperty, PropertyDependencyType.NeedForRead)
		})
		{
			this.first = firstProperty;
			this.second = secondProperty;
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x001F8BC8 File Offset: 0x001F6DC8
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			ExDateTime exDateTime = (ExDateTime)value;
			propertyBag.SetValueWithFixup(this.first, exDateTime);
			if (propertyBag.TimeZone != null)
			{
				propertyBag.SetValueWithFixup(this.second, TaskDate.PersistentLocalTime(new ExDateTime?(exDateTime)));
			}
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x001F8C18 File Offset: 0x001F6E18
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(this.first);
			if (value is ExDateTime)
			{
				return (ExDateTime)value;
			}
			return value;
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x001F8C48 File Offset: 0x001F6E48
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(this.first);
			propertyBag.Delete(this.second);
		}

		// Token: 0x060071F6 RID: 29174 RVA: 0x001F8C64 File Offset: 0x001F6E64
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, this.first);
		}

		// Token: 0x060071F7 RID: 29175 RVA: 0x001F8C73 File Offset: 0x001F6E73
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, this.first);
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x001F8C82 File Offset: 0x001F6E82
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x060071F9 RID: 29177 RVA: 0x001F8CA4 File Offset: 0x001F6EA4
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x060071FA RID: 29178 RVA: 0x001F8CA7 File Offset: 0x001F6EA7
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return this.first;
		}

		// Token: 0x060071FB RID: 29179 RVA: 0x001F8CB0 File Offset: 0x001F6EB0
		internal static ExDateTime? PersistentLocalTime(ExDateTime? time)
		{
			if (time == null)
			{
				return null;
			}
			ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, time.Value.LocalTime.Ticks);
			return new ExDateTime?(ExTimeZone.UtcTimeZone.ConvertDateTime(exDateTime));
		}

		// Token: 0x04004F14 RID: 20244
		private NativeStorePropertyDefinition first;

		// Token: 0x04004F15 RID: 20245
		private NativeStorePropertyDefinition second;
	}
}
