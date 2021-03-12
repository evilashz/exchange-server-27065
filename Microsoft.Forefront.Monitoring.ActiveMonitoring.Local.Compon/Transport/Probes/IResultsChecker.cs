using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000276 RID: 630
	internal interface IResultsChecker
	{
		// Token: 0x060014CE RID: 5326
		bool LastSendMailFailed(CancellationToken cancellationToken, int deploymentId, long previousSequenceNumber, int numofMinutesToLookBack, string resultName, int workItemId, TracingContext traceContext);

		// Token: 0x060014CF RID: 5327
		List<ProbeResult> GetPreviousResults(CancellationToken cancellationToken, int deploymentId, string previousRunResultName, int numofMinutesToLookBack, TracingContext traceContext);

		// Token: 0x060014D0 RID: 5328
		List<ProbeResult> GetPreviousNSpecificStageResults(CancellationToken cancellationToken, int deploymentId, string lamResultNameprefix, int numofMinutesToLookBack, int numOfResultsToReturn, string searchStringInExtensionXml, TracingContext traceContext);

		// Token: 0x060014D1 RID: 5329
		List<ProbeResult> GetPreviousProbeResults(CancellationToken cancellationToken, int numofMinutesToLookBack, string resultName, TracingContext traceContext);
	}
}
