using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.DataProviders
{
	// Token: 0x02000023 RID: 35
	internal class SeriesEventDataProvider : EventDataProvider
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00004EC1 File Offset: 0x000030C1
		public SeriesEventDataProvider(IStorageEntitySetScope<IStoreSession> scope, StoreId calendarFolderId) : base(scope, calendarFolderId)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004ECB File Offset: 0x000030CB
		protected override ICalendarItemBase CreateNewStoreObject()
		{
			return base.XsoFactory.CreateCalendarItemSeries(base.Session, base.ContainerFolderId);
		}
	}
}
