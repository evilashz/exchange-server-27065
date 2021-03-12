using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Monitors
{
	// Token: 0x020000AD RID: 173
	public class HealthStateCollectionMonitor : MonitorWorkItem
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00023988 File Offset: 0x00021B88
		public static MonitorDefinition CreateDefinition(string name)
		{
			return new MonitorDefinition
			{
				AssemblyPath = HealthStateCollectionMonitor.AssemblyPath,
				TypeName = HealthStateCollectionMonitor.TypeName,
				Name = name,
				Component = ExchangeComponent.Monitoring,
				ServiceName = ExchangeComponent.Monitoring.Name,
				ServicePriority = 0,
				TargetResource = Environment.MachineName,
				RecurrenceIntervalSeconds = 30,
				MonitoringIntervalSeconds = 30,
				TimeoutSeconds = 30,
				MaxRetryAttempts = 3,
				Enabled = true,
				SampleMask = "*"
			};
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00023A98 File Offset: 0x00021C98
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = base.Broker.GetLastSuccessfulMonitorResult(base.Definition);
			Task<MonitorResult> task = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, base.TraceContext);
			task.Continue(delegate(MonitorResult lastMonitorResult)
			{
				DateTime startTime = SqlDateTime.MinValue.Value;
				if (lastMonitorResult != null)
				{
					startTime = lastMonitorResult.ExecutionStartTime;
				}
				DateTime d = this.UpdateEntries(startTime, false, cancellationToken);
				if (d == DateTime.MinValue)
				{
					DateTime startTime2 = DateTime.UtcNow - TimeSpan.FromMinutes(60.0);
					this.UpdateEntries(startTime2, true, cancellationToken);
				}
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00023BF0 File Offset: 0x00021DF0
		private RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry[] GetDefinitions(bool isFullInfo, DateTime lastSuccessfulUpdateTime, RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[] results, CancellationToken cancellationToken, out DateTime definitionHeadTime, out DateTime definitionTailTime)
		{
			int totalCount = 0;
			MonitorDefinition definitionHead = null;
			MonitorDefinition definitionTail = null;
			Dictionary<int, bool> workItemIdLookup = (from x in results
			select x.WorkItemId).Distinct<int>().ToDictionary((int a) => a, (int b) => true);
			Dictionary<int, MonitorDefinition> definitions = new Dictionary<int, MonitorDefinition>();
			IDataAccessQuery<MonitorDefinition> monitorDefinitions = base.Broker.GetMonitorDefinitions(DateTime.MaxValue);
			Task<int> task = base.Broker.AsDataAccessQuery<MonitorDefinition>(monitorDefinitions).ExecuteAsync(delegate(MonitorDefinition monitorDefinition)
			{
				if (monitorDefinition != null)
				{
					if (definitionHead == null || monitorDefinition.CreatedTime < definitionHead.CreatedTime)
					{
						definitionHead = monitorDefinition;
					}
					if (definitionTail == null || monitorDefinition.CreatedTime > definitionTail.CreatedTime)
					{
						definitionTail = monitorDefinition;
					}
					bool flag = false;
					if (isFullInfo || workItemIdLookup.TryGetValue(monitorDefinition.Id, out flag) || monitorDefinition.CreatedTime >= lastSuccessfulUpdateTime)
					{
						MonitorDefinition monitorDefinition2 = null;
						if (!definitions.TryGetValue(monitorDefinition.Id, out monitorDefinition2) || monitorDefinition.CreatedTime > monitorDefinition2.CreatedTime)
						{
							definitions[monitorDefinition.Id] = monitorDefinition;
						}
					}
					totalCount++;
				}
			}, cancellationToken, base.TraceContext);
			task.Wait(cancellationToken);
			Dictionary<int, MonitorDefinition>.ValueCollection values = definitions.Values;
			int num = 0;
			RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry[] array = new RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry[values.Count];
			foreach (MonitorDefinition definition in values)
			{
				array[num++] = new RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry(definition);
			}
			definitionHeadTime = MonitorResultCacheManager.GetDefinitionCreationTime(definitionHead);
			definitionTailTime = MonitorResultCacheManager.GetDefinitionCreationTime(definitionTail);
			this.TraceDebug("Total Definitions: {0}, Unique: {1}, HeadTime: {2}, TailTime: {3}", new object[]
			{
				totalCount,
				definitions.Count,
				definitionHeadTime,
				definitionTailTime
			});
			return array;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00023E30 File Offset: 0x00022030
		private RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[] GetResults(DateTime startTime, CancellationToken cancellationToken)
		{
			Dictionary<int, MonitorResult> results = new Dictionary<int, MonitorResult>();
			int totalCount = 0;
			IDataAccessQuery<MonitorResult> successfulMonitorResults = base.Broker.GetSuccessfulMonitorResults(startTime, base.Result.ExecutionStartTime);
			Task<int> task = base.Broker.AsDataAccessQuery<MonitorResult>(successfulMonitorResults).ExecuteAsync(delegate(MonitorResult monitorResult)
			{
				MonitorResult monitorResult2 = null;
				if (!results.TryGetValue(monitorResult.WorkItemId, out monitorResult2) || monitorResult.ExecutionStartTime > monitorResult2.ExecutionStartTime)
				{
					results[monitorResult.WorkItemId] = monitorResult;
				}
				totalCount++;
			}, cancellationToken, base.TraceContext);
			task.Wait(cancellationToken);
			Dictionary<int, MonitorResult>.ValueCollection values = results.Values;
			int num = 0;
			RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[] array = new RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[values.Count];
			foreach (MonitorResult result in values)
			{
				array[num++] = new RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry(result);
			}
			this.TraceDebug("Total Results found: {0}, Unique results found: {1}", new object[]
			{
				totalCount,
				results.Count
			});
			return array;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00023F40 File Offset: 0x00022140
		private DateTime UpdateEntries(DateTime startTime, bool isFullInfo, CancellationToken cancellationToken)
		{
			RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[] results = this.GetResults(startTime, cancellationToken);
			DateTime definitionHeadTime;
			DateTime definitionTailTime;
			RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry[] definitions = this.GetDefinitions(isFullInfo, startTime, results, cancellationToken, out definitionHeadTime, out definitionTailTime);
			return RpcUpdateHealthStatusImpl.SendRequest(Environment.MachineName, definitions, results, definitionHeadTime, definitionTailTime, isFullInfo, 30000);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00023F7D File Offset: 0x0002217D
		private void TraceDebug(string formatString, params object[] args)
		{
			WTFDiagnostics.TraceDebug(ExTraceGlobals.ResultCacheTracer, base.TraceContext, string.Format(formatString, args), null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Monitors\\HealthStateCollectionMonitor.cs", 309);
		}

		// Token: 0x040003C3 RID: 963
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040003C4 RID: 964
		private static readonly string TypeName = typeof(HealthStateCollectionMonitor).FullName;
	}
}
