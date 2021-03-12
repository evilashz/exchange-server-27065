using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ServiceContextProvider;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x0200010D RID: 269
	public class OnlineResponder : ResponderWorkItem
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x000306D4 File Offset: 0x0002E8D4
		public static ResponderDefinition CreateDefinition()
		{
			return new ResponderDefinition
			{
				AssemblyPath = OnlineResponder.AssemblyPath,
				TypeName = OnlineResponder.TypeName,
				Name = "OnlineResponder",
				ServiceName = ExchangeComponent.DataProtection.Name,
				AlertTypeId = "*",
				AlertMask = "*",
				RecurrenceIntervalSeconds = 300,
				WaitIntervalSeconds = 30,
				TimeoutSeconds = 300,
				MaxRetryAttempts = 3,
				Enabled = true,
				StartTime = DateTime.UtcNow
			};
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0003097C File Offset: 0x0002EB7C
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			ServerComponentStateManager.SyncAdState();
			if (DatacenterRegistry.IsForefrontForOffice())
			{
				ServiceContextProvider.Instance.NotifyRecoveryCompletion(ServerComponentEnum.ServerWideOffline.ToString(), true, "");
			}
			IDataAccessQuery<ResponderResult> lastSuccessfulResponderResult = base.Broker.GetLastSuccessfulResponderResult(base.Definition);
			Task<ResponderResult> task = lastSuccessfulResponderResult.ExecuteAsync(cancellationToken, base.TraceContext);
			task.Continue(delegate(ResponderResult lastResponderResult)
			{
				DateTime startTime = DateTime.MinValue;
				if (lastResponderResult != null)
				{
					startTime = lastResponderResult.ExecutionStartTime;
				}
				IDataAccessQuery<MonitorResult> successfulMonitorResults = this.Broker.GetSuccessfulMonitorResults(startTime, this.Result.ExecutionStartTime);
				Dictionary<ServerComponentEnum, OnlineResponder.RedGreenRecord> results = new Dictionary<ServerComponentEnum, OnlineResponder.RedGreenRecord>();
				this.Broker.AsDataAccessQuery<MonitorResult>(successfulMonitorResults).ExecuteAsync(delegate(MonitorResult monitorResult)
				{
					ServerComponentEnum key2;
					if (this.MonitorAffectsComponent(monitorResult, out key2))
					{
						bool isAlert = monitorResult.IsAlert;
						Dictionary<ServerComponentEnum, OnlineResponder.RedGreenRecord> results;
						lock (results)
						{
							OnlineResponder.RedGreenRecord redGreenRecord;
							if (!results.TryGetValue(key2, out redGreenRecord))
							{
								redGreenRecord = new OnlineResponder.RedGreenRecord();
								results.Add(key2, redGreenRecord);
							}
							if (isAlert)
							{
								redGreenRecord.RedCount++;
							}
							else
							{
								redGreenRecord.GreenCount++;
							}
						}
					}
				}, cancellationToken, this.TraceContext);
				foreach (KeyValuePair<ServerComponentEnum, OnlineResponder.RedGreenRecord> keyValuePair in results)
				{
					ServerComponentEnum key = keyValuePair.Key;
					if (keyValuePair.Value.RedCount == 0 && keyValuePair.Value.GreenCount > 0)
					{
						string text = key.ToString();
						ServerComponentStateManager.SetOnline(key);
						if (DatacenterRegistry.IsForefrontForOffice())
						{
							ServiceContextProvider.Instance.NotifyRecoveryCompletion(text, true, "");
						}
						ManagedAvailabilityCrimsonEvents.ComponentSetOnline.LogPeriodic<string, string, string, string, string, string>(text, OnlineResponder.defaultEventSuppression, RecoveryActionId.TakeComponentOnline.ToString(), text, this.Definition.Name, string.Empty, string.Empty, string.Empty);
					}
				}
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00030A03 File Offset: 0x0002EC03
		private bool MonitorAffectsComponent(MonitorResult result, out ServerComponentEnum componentId)
		{
			componentId = ServerComponentEnum.None;
			if (result != null && result.IsHaImpacting)
			{
				componentId = result.Component.ServerComponent;
			}
			return ServerComponentEnum.None != componentId;
		}

		// Token: 0x04000584 RID: 1412
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000585 RID: 1413
		private static readonly string TypeName = typeof(OnlineResponder).FullName;

		// Token: 0x04000586 RID: 1414
		private static readonly TimeSpan defaultEventSuppression = TimeSpan.FromMinutes(5.0);

		// Token: 0x0200010E RID: 270
		private class RedGreenRecord
		{
			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000821 RID: 2081 RVA: 0x00030A60 File Offset: 0x0002EC60
			// (set) Token: 0x06000822 RID: 2082 RVA: 0x00030A68 File Offset: 0x0002EC68
			public int RedCount { get; set; }

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000823 RID: 2083 RVA: 0x00030A71 File Offset: 0x0002EC71
			// (set) Token: 0x06000824 RID: 2084 RVA: 0x00030A79 File Offset: 0x0002EC79
			public int GreenCount { get; set; }
		}
	}
}
