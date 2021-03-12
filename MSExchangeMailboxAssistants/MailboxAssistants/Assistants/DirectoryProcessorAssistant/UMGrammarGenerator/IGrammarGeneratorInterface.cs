using System;
using System.Collections.Generic;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001B7 RID: 439
	internal interface IGrammarGeneratorInterface
	{
		// Token: 0x06001117 RID: 4375
		List<DirectoryGrammar> GetGrammarList();

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001118 RID: 4376
		string ADEntriesFileName { get; }
	}
}
