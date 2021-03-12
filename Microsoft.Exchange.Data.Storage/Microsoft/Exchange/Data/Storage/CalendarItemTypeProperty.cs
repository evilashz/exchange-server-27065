using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C2A RID: 3114
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class CalendarItemTypeProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E9F RID: 28319 RVA: 0x001DBFA0 File Offset: 0x001DA1A0
		internal CalendarItemTypeProperty() : base("CalendarItemType", typeof(CalendarItemType), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.AppointmentRecurring, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.IsException, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006EA0 RID: 28320 RVA: 0x001DBFFC File Offset: 0x001DA1FC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (ObjectClass.IsOfClass(valueOrDefault, "IPM.Appointment.Occurrence") || ObjectClass.IsOfClass(valueOrDefault, "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}"))
			{
				bool valueOrDefault2 = propertyBag.GetValueOrDefault<bool>(InternalSchema.IsException);
				if (valueOrDefault2)
				{
					return CalendarItemType.Exception;
				}
				return CalendarItemType.Occurrence;
			}
			else
			{
				bool valueOrDefault3 = propertyBag.GetValueOrDefault<bool>(InternalSchema.AppointmentRecurring);
				if (valueOrDefault3)
				{
					return CalendarItemType.RecurringMaster;
				}
				return CalendarItemType.Single;
			}
		}
	}
}
