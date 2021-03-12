using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000066 RID: 102
	internal interface ITimeBasedAssistant : IAssistantBase
	{
		// Token: 0x060002DB RID: 731
		void OnStart();

		// Token: 0x060002DC RID: 732
		void OnWorkCycleCheckpoint();

		// Token: 0x060002DD RID: 733
		AssistantTaskContext InitialStep(AssistantTaskContext context);

		// Token: 0x060002DE RID: 734
		AssistantTaskContext InitializeContext(MailboxData data, TimeBasedDatabaseJob job);

		// Token: 0x060002DF RID: 735
		void Invoke(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog = null);

		// Token: 0x060002E0 RID: 736
		List<MailboxData> GetMailboxesToProcess();

		// Token: 0x060002E1 RID: 737
		List<ResourceKey> GetResourceDependencies();

		// Token: 0x060002E2 RID: 738
		MailboxData CreateOnDemandMailboxData(Guid itemGuid, string parameters);
	}
}
