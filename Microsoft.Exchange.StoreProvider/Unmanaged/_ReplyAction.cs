using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000262 RID: 610
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _ReplyAction
	{
		// Token: 0x040010C9 RID: 4297
		internal int cbMessageEntryID;

		// Token: 0x040010CA RID: 4298
		internal unsafe byte* lpbMessageEntryID;

		// Token: 0x040010CB RID: 4299
		internal Guid guidReplyTemplate;
	}
}
