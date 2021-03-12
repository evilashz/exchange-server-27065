using System;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000085 RID: 133
	internal interface IRuntimeSettings
	{
		// Token: 0x06000413 RID: 1043
		AgentRecord[] CreateDefaultAgentOrder();

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000414 RID: 1044
		AgentRecord[] PublicAgentsInDefaultOrder { get; }

		// Token: 0x06000415 RID: 1045
		void SaveAgentSubscription(AgentRecord[] agentRecords);

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000416 RID: 1046
		bool DisposeAgents { get; }

		// Token: 0x06000417 RID: 1047
		void AddSessionRef();

		// Token: 0x06000418 RID: 1048
		void ReleaseSessionRef();

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000419 RID: 1049
		FactoryTable AgentFactories { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600041A RID: 1050
		MonitoringOptions MonitoringOptions { get; }
	}
}
