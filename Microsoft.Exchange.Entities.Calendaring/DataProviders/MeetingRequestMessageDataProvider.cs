using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.DataProviders
{
	// Token: 0x02000022 RID: 34
	internal class MeetingRequestMessageDataProvider : StorageItemDataProvider<IMailboxSession, MeetingRequestMessage, IMeetingRequest>
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004E0F File Offset: 0x0000300F
		public MeetingRequestMessageDataProvider(IStorageEntitySetScope<IMailboxSession> parentScope) : base(parentScope, null, ExTraceGlobals.MeetingRequestMessageDataProviderTracer)
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004E1E File Offset: 0x0000301E
		protected override IStorageTranslator<IMeetingRequest, MeetingRequestMessage> Translator
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004E28 File Offset: 0x00003028
		public virtual StoreId GetCorrelatedItemId(IMeetingRequest meetingRequest)
		{
			StoreObjectId defaultFolderId = base.Scope.StoreSession.GetDefaultFolderId(DefaultFolderType.Calendar);
			StoreId result;
			using (CalendarFolder calendarFolder = (CalendarFolder)base.XsoFactory.BindToCalendarFolder(base.Session, defaultFolderId))
			{
				IEnumerable<VersionedId> enumerable;
				result = meetingRequest.FetchCorrelatedItemId(calendarFolder, false, out enumerable);
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004E88 File Offset: 0x00003088
		internal void SaveMeetingRequest(IMeetingRequest meetingRequest, CommandContext commandContext)
		{
			SaveMode saveMode = base.GetSaveMode(null, commandContext);
			meetingRequest.Save(saveMode);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004EA6 File Offset: 0x000030A6
		protected internal override IMeetingRequest BindToStoreObject(StoreId id)
		{
			return base.XsoFactory.BindToMeetingRequestMessage(base.Session, id);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004EBA File Offset: 0x000030BA
		protected override IMeetingRequest CreateNewStoreObject()
		{
			throw new NotImplementedException();
		}
	}
}
