using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessageComposer
	{
		// Token: 0x060001A7 RID: 423
		void WriteToMessage(MessageItem message);
	}
}
