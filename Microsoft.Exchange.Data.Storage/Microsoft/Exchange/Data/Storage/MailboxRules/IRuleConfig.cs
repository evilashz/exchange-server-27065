using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BF3 RID: 3059
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRuleConfig
	{
		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x06006D25 RID: 27941
		object SCLJunkThreshold { get; }
	}
}
