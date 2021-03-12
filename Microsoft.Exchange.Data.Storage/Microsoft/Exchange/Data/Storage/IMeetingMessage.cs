using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C3 RID: 963
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMeetingMessage : IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06002BC7 RID: 11207
		bool IsArchiveMigratedMessage { get; }

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06002BC8 RID: 11208
		Participant ReceivedRepresenting { get; }

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06002BC9 RID: 11209
		string CalendarOriginatorId { get; }

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06002BCA RID: 11210
		bool IsRepairUpdateMessage { get; }

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06002BCB RID: 11211
		// (set) Token: 0x06002BCC RID: 11212
		bool CalendarProcessed { get; set; }

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06002BCD RID: 11213
		bool IsRecurringMaster { get; }

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06002BCE RID: 11214
		GlobalObjectId GlobalObjectId { get; }
	}
}
