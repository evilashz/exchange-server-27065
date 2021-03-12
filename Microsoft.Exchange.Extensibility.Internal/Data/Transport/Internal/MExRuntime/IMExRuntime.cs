using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000083 RID: 131
	internal interface IMExRuntime
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003FA RID: 1018
		int AgentCount { get; }

		// Token: 0x060003FB RID: 1019
		void Initialize(string configFile, string agentGroup, ProcessTransportRole processTransportRole, string installPath, FactoryInitializer factoryInitializer = null);

		// Token: 0x060003FC RID: 1020
		IMExSession CreateSession(ICloneableInternal state, string name);

		// Token: 0x060003FD RID: 1021
		IMExSession CreateSession(ICloneableInternal state, string name, Func<bool> resumeAgentCallback);

		// Token: 0x060003FE RID: 1022
		IMExSession CreateSession(ICloneableInternal state, string name, Action startAsyncAgentCallback, Action completeAsyncAgentCallback, Func<bool> resumeAgentCallback);

		// Token: 0x060003FF RID: 1023
		void Shutdown();

		// Token: 0x06000400 RID: 1024
		string GetAgentName(int agentSequenceNumber);

		// Token: 0x06000401 RID: 1025
		XElement[] GetDiagnosticInfo(DiagnosableParameters parameters, string agentType);
	}
}
