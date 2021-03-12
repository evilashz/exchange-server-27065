using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C5 RID: 197
	internal class ExternalSharingCalendarType : ExternalSharingType
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0003A008 File Offset: 0x00038208
		public override string FolderTypeName
		{
			get
			{
				return "ExternalSharingCalendar";
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0003A00F File Offset: 0x0003820F
		public override PropertyDefinition CounterProperty
		{
			get
			{
				return MailboxSchema.ExternalSharingCalendarSubscriptionCount;
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0003A016 File Offset: 0x00038216
		protected override bool MatchesContainerClass(string containerClass)
		{
			return ObjectClass.IsCalendarFolder(containerClass);
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0003A01E File Offset: 0x0003821E
		public override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.CalendarFolder;
			}
		}
	}
}
