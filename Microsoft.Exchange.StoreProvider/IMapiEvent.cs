using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000064 RID: 100
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMapiEvent
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002BC RID: 700
		MapiEventTypeFlags EventMask { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002BD RID: 701
		Guid MailboxGuid { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002BE RID: 702
		string ObjectClass { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002BF RID: 703
		ObjectType ItemType { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002C0 RID: 704
		byte[] ItemEntryId { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002C1 RID: 705
		byte[] ParentEntryId { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002C2 RID: 706
		MapiEventFlags EventFlags { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002C3 RID: 707
		MapiExtendedEventFlags ExtendedEventFlags { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002C4 RID: 708
		long ItemCount { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002C5 RID: 709
		long UnreadItemCount { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002C6 RID: 710
		long EventCounter { get; }
	}
}
