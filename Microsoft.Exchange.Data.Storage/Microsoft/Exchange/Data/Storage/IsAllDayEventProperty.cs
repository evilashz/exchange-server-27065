using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C2F RID: 3119
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class IsAllDayEventProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EC4 RID: 28356 RVA: 0x001DC9C8 File Offset: 0x001DABC8
		internal IsAllDayEventProperty() : base("IsAllDayEvent", typeof(bool), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiIsAllDayEvent, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiStartTime, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiEndTime, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiPRStartDate, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiPREndDate, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x001DCA40 File Offset: 0x001DAC40
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			CalendarItemBase calendarItemBase = propertyBag.Context.StoreObject as CalendarItemBase;
			if (calendarItemBase != null && calendarItemBase.IsAllDayEventCache != null)
			{
				return calendarItemBase.IsAllDayEventCache.Value;
			}
			return IsAllDayEventProperty.CalculateIsAllDayEvent(propertyBag);
		}

		// Token: 0x06006EC6 RID: 28358 RVA: 0x001DCA8C File Offset: 0x001DAC8C
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (!(value is bool))
			{
				string message = ServerStrings.ObjectMustBeOfType("bool");
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), message);
				throw new ArgumentException(message);
			}
			CalendarItemBase calendarItemBase = propertyBag.Context.StoreObject as CalendarItemBase;
			if (calendarItemBase != null && calendarItemBase.PropertyBag.ExTimeZone != null)
			{
				calendarItemBase.IsAllDayEventCache = new bool?((bool)value);
			}
			propertyBag.SetValueWithFixup(InternalSchema.MapiIsAllDayEvent, value);
		}

		// Token: 0x06006EC7 RID: 28359 RVA: 0x001DCB0A File Offset: 0x001DAD0A
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, InternalSchema.MapiIsAllDayEvent);
		}

		// Token: 0x17001DF4 RID: 7668
		// (get) Token: 0x06006EC8 RID: 28360 RVA: 0x001DCB18 File Offset: 0x001DAD18
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.CanQuery;
			}
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x001DCB1C File Offset: 0x001DAD1C
		internal static object CalculateIsAllDayEvent(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.MapiIsAllDayEvent);
			if (propertyBag.TimeZone == null)
			{
				return value;
			}
			object value2 = propertyBag.GetValue(InternalSchema.MapiStartTime);
			object value3 = propertyBag.GetValue(InternalSchema.MapiEndTime);
			PropertyError propertyError = value2 as PropertyError;
			if (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
			{
				value2 = propertyBag.GetValue(InternalSchema.MapiPRStartDate);
			}
			propertyError = (value3 as PropertyError);
			if (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
			{
				value3 = propertyBag.GetValue(InternalSchema.MapiPREndDate);
			}
			if (!(value is bool) || !(value2 is ExDateTime) || !(value3 is ExDateTime))
			{
				return value;
			}
			ExDateTime exDateTime = (ExDateTime)value2;
			ExDateTime exDateTime2 = (ExDateTime)value3;
			if ((bool)value && exDateTime == exDateTime.Date && exDateTime2 == exDateTime2.Date && exDateTime2 > exDateTime)
			{
				return true;
			}
			return false;
		}
	}
}
