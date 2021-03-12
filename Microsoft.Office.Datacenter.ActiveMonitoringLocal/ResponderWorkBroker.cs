using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200002C RID: 44
	public class ResponderWorkBroker<TDataAccess> : TypedWorkBroker<ResponderDefinition, ResponderWorkItem, ResponderResult, TDataAccess>, IResponderWorkBroker, IWorkBrokerBase where TDataAccess : DataAccess, new()
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000D2B3 File Offset: 0x0000B4B3
		public ResponderWorkBroker(WorkItemFactory factory) : base(factory)
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		public IDataAccessQuery<ResponderResult> GetResponderResults(ResponderDefinition definition, DateTime startTime)
		{
			return base.GetResultsQuery(definition, startTime);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		public IDataAccessQuery<MonitorResult> GetMonitorResults(string alertMask, DateTime startTime, DateTime endTime)
		{
			IEnumerable<MonitorResult> query = from r in base.GetResultsQuery<MonitorResult>(alertMask, startTime)
			where r.ExecutionEndTime <= endTime
			select r;
			return this.AsDataAccessQuery<MonitorResult>(query);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000D31F File Offset: 0x0000B51F
		public IDataAccessQuery<ProbeResult> GetProbeResult(int probeId, int resultId)
		{
			Activator.CreateInstance<TDataAccess>();
			return base.GetSingleResultQuery<ProbeResult>(probeId, resultId);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000D34C File Offset: 0x0000B54C
		public IDataAccessQuery<ProbeResult> GetProbeResults(string sampleMask, DateTime startTime, DateTime endTime)
		{
			IEnumerable<ProbeResult> query = from r in base.GetResultsQuery<ProbeResult>(sampleMask, startTime)
			where r.ExecutionEndTime <= endTime
			select r;
			return this.AsDataAccessQuery<ProbeResult>(query);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public IDataAccessQuery<ProbeResult> GetProbeResults(string scopeName, DateTime startTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IOrderedEnumerable<ProbeResult> query = from r in tdataAccess.GetTable<ProbeResult, string>(ProbeResultIndex.ScopeNameAndExecutionEndTime(scopeName, startTime))
			where r.DeploymentId == Settings.DeploymentId
			orderby r.ExecutionStartTime descending
			select r;
			return tdataAccess.AsDataAccessQuery<ProbeResult>(query);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000D438 File Offset: 0x0000B638
		public IDataAccessQuery<MonitorResult> GetLastSuccessfulMonitorResult(string alertMask, DateTime startTime, DateTime endTime)
		{
			IEnumerable<MonitorResult> query = from r in base.GetLastSuccessfulResultQuery<MonitorResult>(alertMask, startTime)
			where r.ExecutionEndTime <= endTime
			select r;
			return this.AsDataAccessQuery<MonitorResult>(query);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000D474 File Offset: 0x0000B674
		public IDataAccessQuery<MonitorResult> GetLastSuccessfulMonitorResult(int workItemId)
		{
			return base.GetLastSuccessfulResultQuery<MonitorResult>(workItemId, SqlDateTime.MinValue.Value);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000D498 File Offset: 0x0000B698
		public IDataAccessQuery<ResponderResult> GetLastSuccessfulResponderResult(ResponderDefinition definition)
		{
			return base.GetLastSuccessfulResultQuery(definition, SqlDateTime.MinValue.Value);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000D4B9 File Offset: 0x0000B6B9
		public IDataAccessQuery<ResponderResult> GetLastSuccessfulResponderResult(ResponderDefinition definition, TimeSpan searchWindow)
		{
			return base.GetLastSuccessfulResultQuery(definition, searchWindow);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		public IDataAccessQuery<ResponderResult> GetLastSuccessfulRecoveryAttemptedResponderResult(ResponderDefinition definition, TimeSpan searchWindow)
		{
			IEnumerable<ResponderResult> source = from r in base.GetResultsQuery(definition, DateTime.UtcNow - searchWindow)
			where r.ResultType == ResultType.Succeeded && r.IsRecoveryAttempted
			select r;
			return this.AsDataAccessQuery<ResponderResult>(source.Take(1));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000D53C File Offset: 0x0000B73C
		public IDataAccessQuery<ResponderResult> GetLastSuccessfulRecoveryAttemptedResponderResultByName(ResponderDefinition definition, TimeSpan searchWindow)
		{
			IEnumerable<ResponderResult> source = from r in base.GetResultsQuery<ResponderResult>(definition.ConstructWorkItemResultName(), DateTime.UtcNow - searchWindow)
			where r.ResultType == ResultType.Succeeded && r.IsRecoveryAttempted
			select r;
			return this.AsDataAccessQuery<ResponderResult>(source.Take(1));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		public IDataAccessQuery<MonitorResult> GetSuccessfulMonitorResults(DateTime startTime, DateTime endTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IOrderedEnumerable<MonitorResult> query = from r in tdataAccess.GetTable<MonitorResult, DateTime>(MonitorResultIndex.ExecutionEndTime(startTime))
			where r.ResultType == ResultType.Succeeded && r.DeploymentId == Settings.DeploymentId && r.ExecutionEndTime <= endTime
			orderby r.ExecutionStartTime descending
			select r;
			return this.AsDataAccessQuery<MonitorResult>(query);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000D64C File Offset: 0x0000B84C
		public IDataAccessQuery<MonitorDefinition> GetMonitorDefinitions(DateTime startTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<MonitorDefinition> query = from d in tdataAccess.GetTable<MonitorDefinition, DateTime>(WorkDefinitionIndex<MonitorDefinition>.StartTime(startTime))
			where d.DeploymentId == Settings.DeploymentId
			select d;
			return this.AsDataAccessQuery<MonitorDefinition>(query);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		public IDataAccessQuery<MonitorDefinition> GetMonitorDefinition(int workItemId)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<MonitorDefinition> source = from d in tdataAccess.GetTable<MonitorDefinition, int>(WorkDefinitionIndex<MonitorDefinition>.Id(workItemId))
			where d.DeploymentId == Settings.DeploymentId
			select d;
			return this.AsDataAccessQuery<MonitorDefinition>(source.Take(1));
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000D702 File Offset: 0x0000B902
		bool IWorkBrokerBase.IsLocal()
		{
			return base.IsLocal();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000D70A File Offset: 0x0000B90A
		TimeSpan IWorkBrokerBase.get_DefaultResultWindow()
		{
			return base.DefaultResultWindow;
		}
	}
}
