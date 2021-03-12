using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000030 RID: 48
	internal interface ILocalCalendarReference : ICalendarReference, IEntityReference<Calendar>
	{
		// Token: 0x06000115 RID: 277
		StoreId GetCalendarFolderId();
	}
}
