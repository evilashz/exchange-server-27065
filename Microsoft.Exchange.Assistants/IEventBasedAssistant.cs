using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200005F RID: 95
	internal interface IEventBasedAssistant : IAssistantBase
	{
		// Token: 0x060002CF RID: 719
		void OnStart(EventBasedStartInfo startInfo);

		// Token: 0x060002D0 RID: 720
		bool IsEventInteresting(MapiEvent mapiEvent);

		// Token: 0x060002D1 RID: 721
		void HandleEvent(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item);
	}
}
