using System;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000078 RID: 120
	internal interface IDispatcher
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003A9 RID: 937
		// (remove) Token: 0x060003AA RID: 938
		event AgentInvokeStartHandler OnAgentInvokeStart;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060003AB RID: 939
		// (remove) Token: 0x060003AC RID: 940
		event AgentInvokeReturnsHandler OnAgentInvokeReturns;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060003AD RID: 941
		// (remove) Token: 0x060003AE RID: 942
		event AgentInvokeEndHandler OnAgentInvokeEnd;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060003AF RID: 943
		// (remove) Token: 0x060003B0 RID: 944
		event AgentInvokeScheduledHandler OnAgentInvokeScheduled;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060003B1 RID: 945
		// (remove) Token: 0x060003B2 RID: 946
		event AgentInvokeResumedHandler OnAgentInvokeResumed;

		// Token: 0x060003B3 RID: 947
		void Invoke(MExSession session);

		// Token: 0x060003B4 RID: 948
		void AgentInvokeCompleted(MExSession session);

		// Token: 0x060003B5 RID: 949
		void AgentInvokeScheduled(MExSession session);

		// Token: 0x060003B6 RID: 950
		void AgentInvokeResumed(MExSession session);

		// Token: 0x060003B7 RID: 951
		void Shutdown();

		// Token: 0x060003B8 RID: 952
		bool HasHandler(string eventTopic);

		// Token: 0x060003B9 RID: 953
		void SetCloneState(string eventTopic, int firstAgentIndex);

		// Token: 0x060003BA RID: 954
		int GetAgentIndex(AgentRecord agentEntry);
	}
}
