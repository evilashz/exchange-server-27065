using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x02000304 RID: 772
	internal class AgentErrorHandlingAllowAction : IErrorHandlingAction
	{
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0007FD03 File Offset: 0x0007DF03
		public ErrorHandlingActionType ActionType
		{
			get
			{
				return ErrorHandlingActionType.Allow;
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0007FD06 File Offset: 0x0007DF06
		public void TakeAction(QueuedMessageEventSource source, MailItem mailItem)
		{
		}
	}
}
