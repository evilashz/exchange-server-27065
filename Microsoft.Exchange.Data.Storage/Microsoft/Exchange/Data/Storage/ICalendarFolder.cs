using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200038E RID: 910
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarFolder : IFolder, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060027FC RID: 10236
		// (set) Token: 0x060027FD RID: 10237
		Guid ConsumerCalendarGuid { get; set; }

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060027FE RID: 10238
		// (set) Token: 0x060027FF RID: 10239
		long ConsumerCalendarOwnerId { get; set; }

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002800 RID: 10240
		// (set) Token: 0x06002801 RID: 10241
		Guid ConsumerCalendarPrivateFreeBusyId { get; set; }

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002802 RID: 10242
		// (set) Token: 0x06002803 RID: 10243
		Guid ConsumerCalendarPrivateDetailId { get; set; }

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002804 RID: 10244
		// (set) Token: 0x06002805 RID: 10245
		PublishVisibility ConsumerCalendarPublishVisibility { get; set; }

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002806 RID: 10246
		// (set) Token: 0x06002807 RID: 10247
		string ConsumerCalendarSharingInvitations { get; set; }

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002808 RID: 10248
		// (set) Token: 0x06002809 RID: 10249
		SharingPermissionLevel ConsumerCalendarPermissionLevel { get; set; }

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600280A RID: 10250
		// (set) Token: 0x0600280B RID: 10251
		string ConsumerCalendarSynchronizationState { get; set; }

		// Token: 0x0600280C RID: 10252
		object[][] GetCalendarView(ExDateTime startTime, ExDateTime endTime, params PropertyDefinition[] dataColumns);

		// Token: 0x0600280D RID: 10253
		object[][] GetSyncView(ExDateTime startTime, ExDateTime endTime, CalendarViewBatchingStrategy batchingStrategy, PropertyDefinition[] dataColumns, bool includeNprMasters);
	}
}
