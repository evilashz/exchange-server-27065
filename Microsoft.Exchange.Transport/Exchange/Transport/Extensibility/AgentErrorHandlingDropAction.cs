using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x02000303 RID: 771
	internal class AgentErrorHandlingDropAction : IErrorHandlingAction
	{
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0007FCF0 File Offset: 0x0007DEF0
		public ErrorHandlingActionType ActionType
		{
			get
			{
				return ErrorHandlingActionType.Drop;
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0007FCF3 File Offset: 0x0007DEF3
		public void TakeAction(QueuedMessageEventSource source, MailItem mailItem)
		{
			source.Delete();
		}
	}
}
