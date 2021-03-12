using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000013 RID: 19
	public class MonitorWorkBroker<TDataAccess> : TypedWorkBroker<MonitorDefinition, MonitorWorkItem, MonitorResult, TDataAccess>, IMonitorWorkBroker, IWorkBrokerBase where TDataAccess : DataAccess, new()
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00006F3C File Offset: 0x0000513C
		public MonitorWorkBroker(WorkItemFactory factory) : base(factory)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006F60 File Offset: 0x00005160
		public IDataAccessQuery<ProbeResult> GetProbeResults(string sampleMask, DateTime startTime, DateTime endTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<ProbeResult> query = from r in base.GetResultsQuery<ProbeResult>(sampleMask, startTime)
			where r.ExecutionEndTime <= endTime
			select r;
			return tdataAccess.AsDataAccessQuery<ProbeResult>(query);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006FA8 File Offset: 0x000051A8
		public IDataAccessQuery<ProbeResult> GetProbeResult(int probeId, int resultId)
		{
			Activator.CreateInstance<TDataAccess>();
			return base.GetSingleResultQuery<ProbeResult>(probeId, resultId);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006FC4 File Offset: 0x000051C4
		public IDataAccessQuery<MonitorResult> GetSuccessfulMonitorResults(MonitorDefinition definition, DateTime startTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<MonitorResult> query = from r in base.GetResultsQuery<MonitorResult>(definition.ConstructWorkItemResultName(), startTime)
			where r.ResultType == ResultType.Succeeded
			select r;
			return tdataAccess.AsDataAccessQuery<MonitorResult>(query);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007038 File Offset: 0x00005238
		public IDataAccessQuery<MonitorResult> GetSuccessfulMonitorResults(Component component, DateTime startTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IOrderedEnumerable<MonitorResult> query = from r in tdataAccess.GetTable<MonitorResult, string>(MonitorResultIndex.ComponentNameAndExecutionEndTime(component.ToString(), startTime))
			where r.DeploymentId == Settings.DeploymentId && r.ResultType == ResultType.Succeeded
			orderby r.ExecutionStartTime descending
			select r;
			return tdataAccess.AsDataAccessQuery<MonitorResult>(query);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000070F4 File Offset: 0x000052F4
		public IDataAccessQuery<MonitorResult> GetSuccessfulMonitorResults(DateTime startTime, DateTime endTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IOrderedEnumerable<MonitorResult> query = from r in tdataAccess.GetTable<MonitorResult, DateTime>(MonitorResultIndex.ExecutionEndTime(startTime))
			where r.ResultType == ResultType.Succeeded && r.DeploymentId == Settings.DeploymentId && r.ExecutionEndTime <= endTime
			orderby r.ExecutionStartTime descending
			select r;
			return this.AsDataAccessQuery<MonitorResult>(query);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007164 File Offset: 0x00005364
		public IDataAccessQuery<MonitorResult> GetLastSuccessfulMonitorResult(MonitorDefinition definition)
		{
			return base.GetLastSuccessfulResultQuery(definition, SqlDateTime.MinValue.Value);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007185 File Offset: 0x00005385
		public IDataAccessQuery<MonitorResult> GetLastMonitorResult(MonitorDefinition definition, TimeSpan searchWindow)
		{
			return base.GetLastResultQuery(definition, searchWindow);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000071A0 File Offset: 0x000053A0
		public IDataAccessQuery<MonitorDefinition> GetMonitorDefinitions(DateTime startTime)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<MonitorDefinition> query = from d in tdataAccess.GetTable<MonitorDefinition, DateTime>(WorkDefinitionIndex<MonitorDefinition>.StartTime(startTime))
			where d.DeploymentId == Settings.DeploymentId
			select d;
			return tdataAccess.AsDataAccessQuery<MonitorDefinition>(query);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000071F7 File Offset: 0x000053F7
		bool IWorkBrokerBase.IsLocal()
		{
			return base.IsLocal();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000071FF File Offset: 0x000053FF
		TimeSpan IWorkBrokerBase.get_DefaultResultWindow()
		{
			return base.DefaultResultWindow;
		}
	}
}
