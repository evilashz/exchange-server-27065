using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000060 RID: 96
	internal interface IEventSkipNotification
	{
		// Token: 0x060002D2 RID: 722
		void OnSkipEvent(MapiEvent mapiEvent, Exception exception);
	}
}
