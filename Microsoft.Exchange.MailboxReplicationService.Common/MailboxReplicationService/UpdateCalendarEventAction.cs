using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000091 RID: 145
	[DataContract]
	internal sealed class UpdateCalendarEventAction : CalendarEventContentAction
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0000AEA8 File Offset: 0x000090A8
		public UpdateCalendarEventAction()
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000AEB0 File Offset: 0x000090B0
		public UpdateCalendarEventAction(byte[] itemId, byte[] folderId, string watermark, Event theEvent, IList<Event> exceptionalOccurrences = null, IList<string> deletedOccurrences = null) : base(itemId, folderId, watermark, theEvent, exceptionalOccurrences, deletedOccurrences)
		{
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000AEC1 File Offset: 0x000090C1
		internal override ActionId Id
		{
			get
			{
				return ActionId.UpdateCalendarEvent;
			}
		}
	}
}
