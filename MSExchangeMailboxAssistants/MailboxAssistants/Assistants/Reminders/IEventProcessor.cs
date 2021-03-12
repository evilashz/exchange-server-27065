using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000242 RID: 578
	internal interface IEventProcessor
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060015B9 RID: 5561
		string Name { get; }

		// Token: 0x060015BA RID: 5562
		bool IsEnabled(VariantConfigurationSnapshot snapshot);

		// Token: 0x060015BB RID: 5563
		bool IsEventInteresting(IMapiEvent mapiEvent);

		// Token: 0x060015BC RID: 5564
		void HandleEvent(IMapiEvent mapiEvent, IMailboxSession itemStore, IStoreObject item, bool isItemInDumpster, List<KeyValuePair<string, object>> customDataToLog);
	}
}
