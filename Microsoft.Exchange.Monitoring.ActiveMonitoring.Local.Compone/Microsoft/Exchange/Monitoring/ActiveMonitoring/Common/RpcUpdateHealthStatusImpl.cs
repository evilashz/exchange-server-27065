using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200057A RID: 1402
	internal static class RpcUpdateHealthStatusImpl
	{
		// Token: 0x060022FD RID: 8957 RVA: 0x000D2F34 File Offset: 0x000D1134
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcUpdateHealthStatusImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcUpdateHealthStatusImpl.Request>(requestInfo, 1, 2);
			DateTime lastUpdateTime = MonitorResultCacheManager.Instance.UpdateHealthEntries(request.Definitions, request.Results, request.DefinitionHeadTimeStamp, request.DefinitionTailTimeStamp, request.IsFullUpdate);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, new RpcUpdateHealthStatusImpl.Reply
			{
				LastUpdateTime = lastUpdateTime
			}, 1, 2);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000D2F8C File Offset: 0x000D118C
		public static DateTime SendRequest(string serverName, RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry[] monitorDefinitions, RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[] monitorResults, DateTime definitionHeadTime, DateTime definitionTailTime, bool isFullUpdate, int timeoutInMSec = 30000)
		{
			WTFDiagnostics.TraceDebug<int, int, DateTime, DateTime, bool>(ExTraceGlobals.GenericRpcTracer, RpcUpdateHealthStatusImpl.traceContext, "UpdateHealthStatus.SendRequest: DefinitionsCount:{0} ResultsCount:{1} HeadTime:{2} TailTime:{3} IsFullUpdate:{4}", monitorDefinitions.Length, monitorResults.Length, definitionHeadTime, definitionTailTime, isFullUpdate, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcUpdateHealthStatusImpl.cs", 101);
			RpcGenericRequestInfo rpcGenericRequestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(new RpcUpdateHealthStatusImpl.Request
			{
				Definitions = monitorDefinitions,
				Results = monitorResults,
				DefinitionHeadTimeStamp = definitionHeadTime,
				DefinitionTailTimeStamp = definitionTailTime,
				IsFullUpdate = isFullUpdate
			}, ActiveMonitoringGenericRpcCommandId.UpdateHealthStatus, 1, 2);
			WTFDiagnostics.TraceDebug<int>(ExTraceGlobals.ResultCacheTracer, RpcUpdateHealthStatusImpl.traceContext, "Invoking RpcUpdateHealthStatus rpc with {0} bytes of data", rpcGenericRequestInfo.AttachedData.Length, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcUpdateHealthStatusImpl.cs", 127);
			RpcUpdateHealthStatusImpl.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcUpdateHealthStatusImpl.Reply>(rpcGenericRequestInfo, serverName, timeoutInMSec);
			DateTime lastUpdateTime = reply.LastUpdateTime;
			WTFDiagnostics.TraceDebug<string, DateTime>(ExTraceGlobals.GenericRpcTracer, RpcUpdateHealthStatusImpl.traceContext, "RpcUpdateHealthStatusImpl.SendRequest() returned. (serverName:{0} LastUpdateTime: {1})", serverName, lastUpdateTime, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcUpdateHealthStatusImpl.cs", 141);
			return lastUpdateTime;
		}

		// Token: 0x0400191D RID: 6429
		public const int MajorVersion = 1;

		// Token: 0x0400191E RID: 6430
		public const int MinorVersion = 2;

		// Token: 0x0400191F RID: 6431
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.UpdateHealthStatus;

		// Token: 0x04001920 RID: 6432
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x0200057B RID: 1403
		[Serializable]
		internal class Request
		{
			// Token: 0x1700074C RID: 1868
			// (get) Token: 0x06002300 RID: 8960 RVA: 0x000D3067 File Offset: 0x000D1267
			// (set) Token: 0x06002301 RID: 8961 RVA: 0x000D306F File Offset: 0x000D126F
			public RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry[] Definitions { get; set; }

			// Token: 0x1700074D RID: 1869
			// (get) Token: 0x06002302 RID: 8962 RVA: 0x000D3078 File Offset: 0x000D1278
			// (set) Token: 0x06002303 RID: 8963 RVA: 0x000D3080 File Offset: 0x000D1280
			public RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[] Results { get; set; }

			// Token: 0x1700074E RID: 1870
			// (get) Token: 0x06002304 RID: 8964 RVA: 0x000D3089 File Offset: 0x000D1289
			// (set) Token: 0x06002305 RID: 8965 RVA: 0x000D3091 File Offset: 0x000D1291
			public DateTime DefinitionHeadTimeStamp { get; set; }

			// Token: 0x1700074F RID: 1871
			// (get) Token: 0x06002306 RID: 8966 RVA: 0x000D309A File Offset: 0x000D129A
			// (set) Token: 0x06002307 RID: 8967 RVA: 0x000D30A2 File Offset: 0x000D12A2
			public DateTime DefinitionTailTimeStamp { get; set; }

			// Token: 0x17000750 RID: 1872
			// (get) Token: 0x06002308 RID: 8968 RVA: 0x000D30AB File Offset: 0x000D12AB
			// (set) Token: 0x06002309 RID: 8969 RVA: 0x000D30B3 File Offset: 0x000D12B3
			public bool IsFullUpdate { get; set; }
		}

		// Token: 0x0200057C RID: 1404
		[Serializable]
		internal class Reply
		{
			// Token: 0x17000751 RID: 1873
			// (get) Token: 0x0600230B RID: 8971 RVA: 0x000D30C4 File Offset: 0x000D12C4
			// (set) Token: 0x0600230C RID: 8972 RVA: 0x000D30CC File Offset: 0x000D12CC
			public DateTime LastUpdateTime { get; set; }
		}

		// Token: 0x0200057D RID: 1405
		[Serializable]
		internal class RpcShortMonitorDefinitionEntry
		{
			// Token: 0x0600230E RID: 8974 RVA: 0x000D30E0 File Offset: 0x000D12E0
			public RpcShortMonitorDefinitionEntry(MonitorDefinition definition)
			{
				this.Id = definition.Id;
				this.Name = definition.Name;
				this.TargetResource = definition.TargetResource;
				this.Description = string.Empty;
				this.IsHaImpacting = definition.IsHaImpacting;
				this.RecurranceInterval = definition.RecurrenceIntervalSeconds;
				this.TimeoutSeconds = definition.TimeoutSeconds;
				this.CreatedTimeUtc = definition.CreatedTime.ToUniversalTime();
				this.HealthSetName = definition.Component.Name;
				this.HealthSetDescription = string.Empty;
				this.HealthGroupName = definition.Component.HealthGroup.ToString();
				this.ServerComponentName = definition.Component.ServerComponent.ToString();
				this.Enabled = definition.Enabled;
				this.ServicePriority = definition.ServicePriority;
			}

			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x0600230F RID: 8975 RVA: 0x000D31C4 File Offset: 0x000D13C4
			// (set) Token: 0x06002310 RID: 8976 RVA: 0x000D31CC File Offset: 0x000D13CC
			public int Id { get; private set; }

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x06002311 RID: 8977 RVA: 0x000D31D5 File Offset: 0x000D13D5
			// (set) Token: 0x06002312 RID: 8978 RVA: 0x000D31DD File Offset: 0x000D13DD
			public string Name { get; private set; }

			// Token: 0x17000754 RID: 1876
			// (get) Token: 0x06002313 RID: 8979 RVA: 0x000D31E6 File Offset: 0x000D13E6
			// (set) Token: 0x06002314 RID: 8980 RVA: 0x000D31EE File Offset: 0x000D13EE
			public string TargetResource { get; private set; }

			// Token: 0x17000755 RID: 1877
			// (get) Token: 0x06002315 RID: 8981 RVA: 0x000D31F7 File Offset: 0x000D13F7
			// (set) Token: 0x06002316 RID: 8982 RVA: 0x000D31FF File Offset: 0x000D13FF
			public string Description { get; private set; }

			// Token: 0x17000756 RID: 1878
			// (get) Token: 0x06002317 RID: 8983 RVA: 0x000D3208 File Offset: 0x000D1408
			// (set) Token: 0x06002318 RID: 8984 RVA: 0x000D3210 File Offset: 0x000D1410
			public bool IsHaImpacting { get; private set; }

			// Token: 0x17000757 RID: 1879
			// (get) Token: 0x06002319 RID: 8985 RVA: 0x000D3219 File Offset: 0x000D1419
			// (set) Token: 0x0600231A RID: 8986 RVA: 0x000D3221 File Offset: 0x000D1421
			public int RecurranceInterval { get; private set; }

			// Token: 0x17000758 RID: 1880
			// (get) Token: 0x0600231B RID: 8987 RVA: 0x000D322A File Offset: 0x000D142A
			// (set) Token: 0x0600231C RID: 8988 RVA: 0x000D3232 File Offset: 0x000D1432
			public DateTime CreatedTimeUtc { get; private set; }

			// Token: 0x17000759 RID: 1881
			// (get) Token: 0x0600231D RID: 8989 RVA: 0x000D323C File Offset: 0x000D143C
			public DateTime CreatedTime
			{
				get
				{
					if (this.createdTimeLocal == null)
					{
						this.createdTimeLocal = new DateTime?(this.CreatedTimeUtc.ToLocalTime());
					}
					return this.createdTimeLocal.Value;
				}
			}

			// Token: 0x1700075A RID: 1882
			// (get) Token: 0x0600231E RID: 8990 RVA: 0x000D327A File Offset: 0x000D147A
			// (set) Token: 0x0600231F RID: 8991 RVA: 0x000D3282 File Offset: 0x000D1482
			public string HealthSetName { get; private set; }

			// Token: 0x1700075B RID: 1883
			// (get) Token: 0x06002320 RID: 8992 RVA: 0x000D328B File Offset: 0x000D148B
			// (set) Token: 0x06002321 RID: 8993 RVA: 0x000D3293 File Offset: 0x000D1493
			public string HealthSetDescription { get; private set; }

			// Token: 0x1700075C RID: 1884
			// (get) Token: 0x06002322 RID: 8994 RVA: 0x000D329C File Offset: 0x000D149C
			// (set) Token: 0x06002323 RID: 8995 RVA: 0x000D32A4 File Offset: 0x000D14A4
			public string HealthGroupName { get; private set; }

			// Token: 0x1700075D RID: 1885
			// (get) Token: 0x06002324 RID: 8996 RVA: 0x000D32AD File Offset: 0x000D14AD
			// (set) Token: 0x06002325 RID: 8997 RVA: 0x000D32B5 File Offset: 0x000D14B5
			public string ServerComponentName { get; private set; }

			// Token: 0x1700075E RID: 1886
			// (get) Token: 0x06002326 RID: 8998 RVA: 0x000D32BE File Offset: 0x000D14BE
			// (set) Token: 0x06002327 RID: 8999 RVA: 0x000D32C6 File Offset: 0x000D14C6
			public int TimeoutSeconds { get; private set; }

			// Token: 0x1700075F RID: 1887
			// (get) Token: 0x06002328 RID: 9000 RVA: 0x000D32CF File Offset: 0x000D14CF
			// (set) Token: 0x06002329 RID: 9001 RVA: 0x000D32D7 File Offset: 0x000D14D7
			public bool Enabled { get; private set; }

			// Token: 0x17000760 RID: 1888
			// (get) Token: 0x0600232A RID: 9002 RVA: 0x000D32E0 File Offset: 0x000D14E0
			// (set) Token: 0x0600232B RID: 9003 RVA: 0x000D32E8 File Offset: 0x000D14E8
			public int ServicePriority { get; private set; }

			// Token: 0x04001927 RID: 6439
			private DateTime? createdTimeLocal;
		}

		// Token: 0x0200057E RID: 1406
		[Serializable]
		internal class RpcShortMonitorResultEntry
		{
			// Token: 0x0600232C RID: 9004 RVA: 0x000D32F4 File Offset: 0x000D14F4
			public RpcShortMonitorResultEntry(MonitorResult result)
			{
				this.FirstAlertObservedTimeUtc = this.GetUniversalTime(result.FirstAlertObservedTime);
				this.LastTransitionTimeUtc = this.GetUniversalTime(result.HealthStateChangedTime);
				this.LastExecutionTimeUtc = result.ExecutionEndTime.ToUniversalTime();
				this.LastExecutionResult = result.ResultType.ToString();
				this.ResultId = result.ResultId;
				this.WorkItemId = result.WorkItemId;
				this.IsAlert = result.IsAlert;
				this.Error = result.Error;
				this.Exception = result.Exception;
				this.IsNotified = result.IsNotified;
				this.LastFailedProbeId = result.LastFailedProbeId;
				this.LastFailedProbeResultId = result.LastFailedProbeResultId;
			}

			// Token: 0x0600232D RID: 9005 RVA: 0x000D33B8 File Offset: 0x000D15B8
			private DateTime GetUniversalTime(DateTime? time)
			{
				return ((time != null) ? time.Value : DateTime.MinValue).ToUniversalTime();
			}

			// Token: 0x17000761 RID: 1889
			// (get) Token: 0x0600232E RID: 9006 RVA: 0x000D33E4 File Offset: 0x000D15E4
			// (set) Token: 0x0600232F RID: 9007 RVA: 0x000D33EC File Offset: 0x000D15EC
			public DateTime FirstAlertObservedTimeUtc { get; private set; }

			// Token: 0x17000762 RID: 1890
			// (get) Token: 0x06002330 RID: 9008 RVA: 0x000D33F8 File Offset: 0x000D15F8
			public DateTime FirstAlertObservedTime
			{
				get
				{
					if (this.firstAlertObservedTimeLocal == null)
					{
						this.firstAlertObservedTimeLocal = new DateTime?(this.FirstAlertObservedTimeUtc.ToLocalTime());
					}
					return this.firstAlertObservedTimeLocal.Value;
				}
			}

			// Token: 0x17000763 RID: 1891
			// (get) Token: 0x06002331 RID: 9009 RVA: 0x000D3436 File Offset: 0x000D1636
			// (set) Token: 0x06002332 RID: 9010 RVA: 0x000D343E File Offset: 0x000D163E
			public DateTime LastTransitionTimeUtc { get; private set; }

			// Token: 0x17000764 RID: 1892
			// (get) Token: 0x06002333 RID: 9011 RVA: 0x000D3448 File Offset: 0x000D1648
			public DateTime LastTransitionTime
			{
				get
				{
					if (this.lastTransitionTimeLocal == null)
					{
						this.lastTransitionTimeLocal = new DateTime?(this.LastTransitionTimeUtc.ToLocalTime());
					}
					return this.lastTransitionTimeLocal.Value;
				}
			}

			// Token: 0x17000765 RID: 1893
			// (get) Token: 0x06002334 RID: 9012 RVA: 0x000D3486 File Offset: 0x000D1686
			// (set) Token: 0x06002335 RID: 9013 RVA: 0x000D348E File Offset: 0x000D168E
			public DateTime LastExecutionTimeUtc { get; private set; }

			// Token: 0x17000766 RID: 1894
			// (get) Token: 0x06002336 RID: 9014 RVA: 0x000D3498 File Offset: 0x000D1698
			public DateTime LastExecutionTime
			{
				get
				{
					if (this.lastExecutionTimeLocal == null)
					{
						this.lastExecutionTimeLocal = new DateTime?(this.LastExecutionTimeUtc.ToLocalTime());
					}
					return this.lastExecutionTimeLocal.Value;
				}
			}

			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x06002337 RID: 9015 RVA: 0x000D34D6 File Offset: 0x000D16D6
			// (set) Token: 0x06002338 RID: 9016 RVA: 0x000D34DE File Offset: 0x000D16DE
			public string LastExecutionResult { get; private set; }

			// Token: 0x17000768 RID: 1896
			// (get) Token: 0x06002339 RID: 9017 RVA: 0x000D34E7 File Offset: 0x000D16E7
			// (set) Token: 0x0600233A RID: 9018 RVA: 0x000D34EF File Offset: 0x000D16EF
			public int ResultId { get; private set; }

			// Token: 0x17000769 RID: 1897
			// (get) Token: 0x0600233B RID: 9019 RVA: 0x000D34F8 File Offset: 0x000D16F8
			// (set) Token: 0x0600233C RID: 9020 RVA: 0x000D3500 File Offset: 0x000D1700
			public int WorkItemId { get; private set; }

			// Token: 0x1700076A RID: 1898
			// (get) Token: 0x0600233D RID: 9021 RVA: 0x000D3509 File Offset: 0x000D1709
			// (set) Token: 0x0600233E RID: 9022 RVA: 0x000D3511 File Offset: 0x000D1711
			public bool IsAlert { get; private set; }

			// Token: 0x1700076B RID: 1899
			// (get) Token: 0x0600233F RID: 9023 RVA: 0x000D351A File Offset: 0x000D171A
			// (set) Token: 0x06002340 RID: 9024 RVA: 0x000D3522 File Offset: 0x000D1722
			public string Error { get; private set; }

			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x06002341 RID: 9025 RVA: 0x000D352B File Offset: 0x000D172B
			// (set) Token: 0x06002342 RID: 9026 RVA: 0x000D3533 File Offset: 0x000D1733
			public string Exception { get; private set; }

			// Token: 0x1700076D RID: 1901
			// (get) Token: 0x06002343 RID: 9027 RVA: 0x000D353C File Offset: 0x000D173C
			// (set) Token: 0x06002344 RID: 9028 RVA: 0x000D3544 File Offset: 0x000D1744
			public bool IsNotified { get; private set; }

			// Token: 0x1700076E RID: 1902
			// (get) Token: 0x06002345 RID: 9029 RVA: 0x000D354D File Offset: 0x000D174D
			// (set) Token: 0x06002346 RID: 9030 RVA: 0x000D3555 File Offset: 0x000D1755
			public int LastFailedProbeId { get; private set; }

			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x06002347 RID: 9031 RVA: 0x000D355E File Offset: 0x000D175E
			// (set) Token: 0x06002348 RID: 9032 RVA: 0x000D3566 File Offset: 0x000D1766
			public int LastFailedProbeResultId { get; private set; }

			// Token: 0x04001936 RID: 6454
			private DateTime? firstAlertObservedTimeLocal;

			// Token: 0x04001937 RID: 6455
			public DateTime? lastTransitionTimeLocal;

			// Token: 0x04001938 RID: 6456
			public DateTime? lastExecutionTimeLocal;
		}
	}
}
