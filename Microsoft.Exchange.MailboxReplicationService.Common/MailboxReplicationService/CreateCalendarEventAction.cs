using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000085 RID: 133
	[DataContract]
	internal sealed class CreateCalendarEventAction : CalendarEventContentAction
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x0000AA19 File Offset: 0x00008C19
		public CreateCalendarEventAction()
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000AA21 File Offset: 0x00008C21
		public CreateCalendarEventAction(byte[] itemId, byte[] folderId, string watermark, Event theEvent, IList<Event> exceptionalOccurrences = null, IList<string> deletedOccurrences = null) : base(itemId, folderId, watermark, theEvent, exceptionalOccurrences, deletedOccurrences)
		{
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000AA32 File Offset: 0x00008C32
		internal override ActionId Id
		{
			get
			{
				return ActionId.CreateCalendarEvent;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000AA36 File Offset: 0x00008C36
		internal override void TranslateEntryIds(IEntryIdTranslator translator)
		{
			base.TranslateFolderId(translator);
			base.OriginalItemId = base.ItemId;
		}
	}
}
