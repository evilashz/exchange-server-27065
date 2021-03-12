using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessageComposerBuilder
	{
		// Token: 0x060001CA RID: 458
		IMessageComposer Build(ADUser recipient);
	}
}
