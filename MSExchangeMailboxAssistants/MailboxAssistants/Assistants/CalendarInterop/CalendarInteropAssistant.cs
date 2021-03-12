using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarInterop
{
	// Token: 0x02000260 RID: 608
	internal class CalendarInteropAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06001696 RID: 5782 RVA: 0x00080212 File Offset: 0x0007E412
		public CalendarInteropAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.actionProcessor = new CalendarInteropProcessor();
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00080228 File Offset: 0x0007E428
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && ObjectClass.IsCalendarItemSeries(mapiEvent.ObjectClass) && mapiEvent.EventMask.Contains(MapiEventTypeFlags.ObjectModified);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0008024F File Offset: 0x0007E44F
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			this.actionProcessor.ProcessPendingActions(mailboxSession, item);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0008025E File Offset: 0x0007E45E
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00080266 File Offset: 0x0007E466
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0008026E File Offset: 0x0007E46E
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000D40 RID: 3392
		private readonly CalendarInteropProcessor actionProcessor;
	}
}
