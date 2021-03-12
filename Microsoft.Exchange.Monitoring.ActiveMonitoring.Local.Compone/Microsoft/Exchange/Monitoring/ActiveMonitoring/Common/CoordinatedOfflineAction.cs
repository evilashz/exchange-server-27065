using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000110 RID: 272
	internal class CoordinatedOfflineAction : CoordinatedRecoveryAction
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x00030EEC File Offset: 0x0002F0EC
		internal CoordinatedOfflineAction(RecoveryActionId actionId, TimeSpan duration, ServerComponentEnum serverComponent, string requester, int minimumRequiredTobeInReadyState, string[] servers) : base(actionId, requester, minimumRequiredTobeInReadyState, 1, servers)
		{
			this.duration = duration;
			this.serverComponent = serverComponent;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00030F17 File Offset: 0x0002F117
		internal void InvokeOfflineOnMajority(TimeSpan arbitrationTimeout)
		{
			base.Execute(arbitrationTimeout, delegate(CoordinatedRecoveryAction.ResourceAvailabilityStatistics stats)
			{
				ServerComponentStateManager.SetOffline(this.serverComponent);
			});
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00030F2C File Offset: 0x0002F12C
		protected override ResourceAvailabilityStatus RunCheck(string serverName, out string debugInfo)
		{
			debugInfo = string.Empty;
			if (RecoveryActionHelper.IsLocalServerName(serverName))
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.RecoveryActionTracer, base.TraceContext, "Avoding rpc loop back to local machine since we are already in the Arbitration phase, return directly status of 'Arbitrating'", null, "RunCheck", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\RecoveryAction\\CoordinatedOfflineAction.cs", 95);
				return ResourceAvailabilityStatus.Arbitrating;
			}
			DateTime localTime = ExDateTime.Now.LocalTime;
			DateTime queryStartTime = localTime - this.duration;
			bool flag = false;
			DateTime dateTime;
			DateTime dateTime2;
			RpcGetServerComponentStatusImpl.SendRequest(serverName, this.serverComponent, queryStartTime, localTime, out flag, out dateTime, out dateTime2, 30000);
			ResourceAvailabilityStatus result;
			if (dateTime2 < ExDateTime.Now.LocalTime)
			{
				if (flag)
				{
					result = ResourceAvailabilityStatus.Ready;
				}
				else
				{
					result = ResourceAvailabilityStatus.Offline;
				}
			}
			else
			{
				result = ResourceAvailabilityStatus.Arbitrating;
			}
			debugInfo = string.Format("[serverComponent={0}, isOnline = {1}, lastOfflineRequestStartTime = {2}, lastOfflineRequestEndTime = {3}]", new object[]
			{
				this.serverComponent,
				flag,
				dateTime,
				dateTime2
			});
			return result;
		}

		// Token: 0x04000594 RID: 1428
		private readonly TimeSpan duration;

		// Token: 0x04000595 RID: 1429
		private ServerComponentEnum serverComponent;
	}
}
