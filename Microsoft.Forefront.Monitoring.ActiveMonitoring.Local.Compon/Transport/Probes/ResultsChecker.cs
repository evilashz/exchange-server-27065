using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000277 RID: 631
	internal class ResultsChecker : IResultsChecker
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x0003EA80 File Offset: 0x0003CC80
		public bool LastSendMailFailed(CancellationToken cancellationToken, int deploymentId, long previousSequenceNumber, int numofMinutesToLookBack, string resultName, int workItemId, TracingContext traceContext)
		{
			DateTime startTime = DateTime.UtcNow.AddMinutes((double)(-(double)numofMinutesToLookBack));
			List<ProbeResult> results = new List<ProbeResult>();
			LocalDataAccess localDataAccess = new LocalDataAccess();
			IOrderedEnumerable<ProbeResult> query = from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(resultName, startTime))
			where r.DeploymentId == deploymentId && r.ResultName.StartsWith(resultName) && r.ExecutionEndTime >= startTime && r.WorkItemId == workItemId && r.StateAttribute25 == previousSequenceNumber.ToString()
			orderby r.ExecutionStartTime
			select r;
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				if (r.StateAttribute9 != 1.0)
				{
					results.Add(r);
				}
			}, cancellationToken, traceContext);
			return results.Count != 0;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0003EBD4 File Offset: 0x0003CDD4
		public List<ProbeResult> GetPreviousResults(CancellationToken cancellationToken, int deploymentId, string previousRunResultName, int numofMinutesToLookBack, TracingContext traceContext)
		{
			DateTime startTime = DateTime.UtcNow.AddMinutes((double)(-(double)numofMinutesToLookBack));
			List<ProbeResult> results = new List<ProbeResult>();
			LocalDataAccess localDataAccess = new LocalDataAccess();
			IOrderedEnumerable<ProbeResult> query = from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime("MBTSubmission/StoreDriverSubmission", startTime))
			where r.DeploymentId == deploymentId && r.ResultName.StartsWith("MBTSubmission/StoreDriverSubmission") && r.ExecutionEndTime >= startTime && r.StateAttribute1 == previousRunResultName
			orderby r.ExecutionStartTime
			select r;
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				results.Add(r);
			}, cancellationToken, traceContext);
			return results;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0003ED08 File Offset: 0x0003CF08
		public List<ProbeResult> GetPreviousNSpecificStageResults(CancellationToken cancellationToken, int deploymentId, string lamResultNameprefix, int numofMinutesToLookBack, int numOfResultsToReturn, string searchStringInExtensionXml, TracingContext traceContext)
		{
			DateTime startTime = DateTime.UtcNow.AddMinutes((double)(-(double)numofMinutesToLookBack));
			List<ProbeResult> results = new List<ProbeResult>();
			LocalDataAccess localDataAccess = new LocalDataAccess();
			IEnumerable<ProbeResult> query = (from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(lamResultNameprefix, startTime))
			where r.DeploymentId == deploymentId && r.ResultName.StartsWith(lamResultNameprefix) && r.ExecutionEndTime >= startTime && r.ExtensionXml.Contains(searchStringInExtensionXml)
			orderby r.ExecutionStartTime descending
			select r).Take(numOfResultsToReturn);
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				results.Add(r);
			}, cancellationToken, traceContext);
			return results;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0003EE20 File Offset: 0x0003D020
		public List<ProbeResult> GetPreviousProbeResults(CancellationToken cancellationToken, int numofMinutesToLookBack, string resultName, TracingContext traceContext)
		{
			LocalDataAccess localDataAccess = new LocalDataAccess();
			DateTime startTime = DateTime.UtcNow.AddMinutes((double)(-(double)numofMinutesToLookBack));
			List<ProbeResult> results = new List<ProbeResult>();
			IOrderedEnumerable<ProbeResult> query = from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(resultName, startTime))
			where r.ResultName.Contains(resultName) && r.ExecutionEndTime >= startTime
			orderby r.ExecutionStartTime descending
			select r;
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				results.Add(r);
			}, cancellationToken, traceContext);
			return results;
		}
	}
}
