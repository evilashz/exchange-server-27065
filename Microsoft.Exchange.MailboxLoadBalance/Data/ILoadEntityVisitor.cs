using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ILoadEntityVisitor
	{
		// Token: 0x060000BC RID: 188
		bool Visit(LoadContainer container);

		// Token: 0x060000BD RID: 189
		bool Visit(LoadEntity entity);
	}
}
