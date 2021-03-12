using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors
{
	// Token: 0x0200008F RID: 143
	internal static class CalendarFolderAccessors
	{
		// Token: 0x040000FC RID: 252
		public static readonly IStoragePropertyAccessor<ICalendarFolder, string> DisplayName = new DefaultStoragePropertyAccessor<ICalendarFolder, string>(FolderSchema.DisplayName, false);
	}
}
