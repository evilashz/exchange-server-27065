using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000EF RID: 239
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ITimer
	{
		// Token: 0x06000747 RID: 1863
		void SetAction(Action newAction, bool startExecution);

		// Token: 0x06000748 RID: 1864
		void WaitExecution();

		// Token: 0x06000749 RID: 1865
		void WaitExecution(TimeSpan timeout);
	}
}
