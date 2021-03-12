using System;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Responders
{
	// Token: 0x02000467 RID: 1127
	public class RestartHostControllerServiceResponder2 : RestartServiceResponder
	{
		// Token: 0x06001C94 RID: 7316 RVA: 0x000A7C60 File Offset: 0x000A5E60
		internal static ResponderDefinition CreateDefinition(string responderName, string alertMask, ServiceHealthStatus responderTargetState, int serviceStopTimeoutInSeconds = 15, int serviceStartTimeoutInSeconds = 120, int serviceStartDelayInSeconds = 0, DumpMode dumpOnRestartMode = DumpMode.None, string dumpPath = null, double minimumFreeDiskPercent = 15.0, int maximumDumpDurationInSeconds = 0, string throttleGroupName = null)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = RestartHostControllerServiceResponder2.AssemblyPath;
			responderDefinition.TypeName = RestartHostControllerServiceResponder2.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = ExchangeComponent.Search.Name;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = alertMask;
			responderDefinition.RecurrenceIntervalSeconds = 0;
			responderDefinition.TimeoutSeconds = 600;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = true;
			if (string.IsNullOrEmpty(dumpPath))
			{
				dumpPath = RestartServiceResponder.DefaultValues.DumpPath;
			}
			responderDefinition.Attributes["WindowsServiceName"] = "HostControllerService";
			responderDefinition.Attributes["ServiceStopTimeout"] = TimeSpan.FromSeconds((double)serviceStopTimeoutInSeconds).ToString();
			responderDefinition.Attributes["ServiceStartTimeout"] = TimeSpan.FromSeconds((double)serviceStartTimeoutInSeconds).ToString();
			responderDefinition.Attributes["ServiceStartDelay"] = TimeSpan.FromSeconds((double)serviceStartDelayInSeconds).ToString();
			responderDefinition.Attributes["IsMasterAndWorker"] = true.ToString();
			responderDefinition.Attributes["DumpOnRestart"] = dumpOnRestartMode.ToString();
			responderDefinition.Attributes["DumpPath"] = dumpPath;
			responderDefinition.Attributes["MinimumFreeDiskPercent"] = minimumFreeDiskPercent.ToString();
			responderDefinition.Attributes["MaximumDumpDurationInSeconds"] = maximumDumpDurationInSeconds.ToString();
			responderDefinition.Attributes["AdditionalProcessNameToKill"] = "NodeRunner";
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.RestartService, "HostControllerService", null);
			return responderDefinition;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000A7E30 File Offset: 0x000A6030
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			SearchMonitoringHelper.LogRecoveryAction(this, "Invoked.", new object[0]);
			this.InitializeAttributes();
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.RestartService, base.WindowsServiceName, this, true, cancellationToken, null);
			try
			{
				recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
				{
					this.InternalRestartService(startEntry, cancellationToken);
				});
			}
			catch (ThrottlingRejectedOperationException)
			{
				SearchMonitoringHelper.LogRecoveryAction(this, "Throttled.", new object[0]);
				throw;
			}
			SearchMonitoringHelper.LogRecoveryAction(this, "Completed.", new object[0]);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000A7ED0 File Offset: 0x000A60D0
		protected override void InitializeAttributes()
		{
			base.InitializeAttributes();
			new AttributeHelper(base.Definition);
		}

		// Token: 0x040013B6 RID: 5046
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040013B7 RID: 5047
		private static readonly string TypeName = typeof(RestartHostControllerServiceResponder2).FullName;
	}
}
