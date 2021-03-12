using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000025 RID: 37
	internal interface IFlowManager
	{
		// Token: 0x060000CB RID: 203
		void EnsureIndexingFlow();

		// Token: 0x060000CC RID: 204
		void EnsureQueryFlows(string indexSystemName);

		// Token: 0x060000CD RID: 205
		void EnsureTransportFlow();

		// Token: 0x060000CE RID: 206
		XElement GetFlowDiagnostics();

		// Token: 0x060000CF RID: 207
		IEnumerable<string> GetFlows();

		// Token: 0x060000D0 RID: 208
		void RemoveFlowsForIndexSystem(string indexSystemName);

		// Token: 0x060000D1 RID: 209
		void AddCtsFlow(string flowName, string flowXML);

		// Token: 0x060000D2 RID: 210
		bool RemoveCTSFlow(string flowName);

		// Token: 0x060000D3 RID: 211
		ICollection<FlowDescriptor> GetExpectedFlowsForIndexSystem(string indexSystemName);

		// Token: 0x060000D4 RID: 212
		string GetFlow(string flowName);
	}
}
