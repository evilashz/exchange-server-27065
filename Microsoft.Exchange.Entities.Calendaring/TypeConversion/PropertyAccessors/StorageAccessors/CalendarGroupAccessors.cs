using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors
{
	// Token: 0x02000090 RID: 144
	internal static class CalendarGroupAccessors
	{
		// Token: 0x040000FD RID: 253
		public static readonly IStoragePropertyAccessor<ICalendarGroup, Guid> GroupClassId = new DelegatedStoragePropertyAccessor<ICalendarGroup, Guid>(delegate(ICalendarGroup container, out Guid value)
		{
			value = container.GroupClassId;
			return true;
		}, null, null, null, new PropertyDefinition[0]);

		// Token: 0x040000FE RID: 254
		public static readonly IStoragePropertyAccessor<ICalendarGroup, string> GroupName = new DelegatedStoragePropertyAccessor<ICalendarGroup, string>(delegate(ICalendarGroup container, out string value)
		{
			value = container.GroupName;
			return true;
		}, delegate(ICalendarGroup calendarGroup, string name)
		{
			calendarGroup.GroupName = name;
		}, null, null, new PropertyDefinition[0]);
	}
}
