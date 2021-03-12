using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x02000300 RID: 768
	internal interface IErrorHandlingAction
	{
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060021A6 RID: 8614
		ErrorHandlingActionType ActionType { get; }

		// Token: 0x060021A7 RID: 8615
		void TakeAction(QueuedMessageEventSource source, MailItem mailItem);
	}
}
