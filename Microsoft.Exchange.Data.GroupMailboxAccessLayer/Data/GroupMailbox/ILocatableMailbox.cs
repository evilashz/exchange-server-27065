using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ILocatableMailbox
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000235 RID: 565
		IMailboxLocator Locator { get; }
	}
}
