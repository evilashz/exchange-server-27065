using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Responders
{
	// Token: 0x020001C6 RID: 454
	public class PotentialOneCopyRemoteServerRestartResponder : RemoteServerRestartResponder
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x000538D8 File Offset: 0x00051AD8
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, string alertMask, ServiceHealthStatus responderTargetState, string componentName, string serviceName, bool enabled = true)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = alertMask;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.TimeoutSeconds = 600;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 120;
			responderDefinition.Enabled = enabled;
			responderDefinition.Attributes["ComponentName"] = componentName;
			responderDefinition.AssemblyPath = PotentialOneCopyRemoteServerRestartResponder.AssemblyPath;
			responderDefinition.TypeName = PotentialOneCopyRemoteServerRestartResponder.TypeName;
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.PotentialOneCopyRemoteServerRestartResponder, Environment.MachineName, null);
			return responderDefinition;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000539A0 File Offset: 0x00051BA0
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			ProbeResult lastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
			if (lastFailedProbeResult == null || string.IsNullOrWhiteSpace(lastFailedProbeResult.StateAttribute1))
			{
				throw new HighAvailabilityMAResponderException("Unable to get Target server from last failed probe result.");
			}
			string failedServer = lastFailedProbeResult.StateAttribute1;
			base.Result.StateAttribute1 = failedServer;
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.PotentialOneCopyRemoteServerRestartResponder, string.Empty, this, true, cancellationToken, null);
			recoveryActionRunner.IsIgnoreResourceName = true;
			string componentname = base.Definition.Attributes["ComponentName"];
			base.Result.StateAttribute2 = componentname;
			this.errorMessage = null;
			recoveryActionRunner.Execute(delegate()
			{
				this.PerformRemoteForceReboot(componentname, failedServer);
			});
			if (this.errorMessage != null)
			{
				throw new HighAvailabilityMAResponderException(string.Format("Error occurred inside DoWork: {0}", string.Join(Environment.NewLine, new string[]
				{
					this.errorMessage
				})));
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00053BE8 File Offset: 0x00051DE8
		private void PerformRemoteForceReboot(string componentName, string failedServer)
		{
			RecoveryActionHelper.RunAndMeasure(string.Format("RemoteForceReboot(WorkitemId={0}, ResultId={1}, Component={2}, TargetServer={3})", new object[]
			{
				base.Id,
				base.Result.ResultId,
				componentName,
				failedServer
			}), false, ManagedAvailabilityCrimsonEvents.MeasureOperation, delegate
			{
				this.CreateRunspace();
				string failedServer2 = failedServer;
				if (!string.IsNullOrWhiteSpace(failedServer2))
				{
					PSCommand pscommand = new PSCommand();
					pscommand.AddScript(string.Format("Request-SetMachinePowerState.ps1 -MachineName {0} -Action Restart -Reason 'Active Monitoring RemoteServerRestartResponder from host {1}'", failedServer2), false);
					this.Result.StateAttribute5 = string.Format("Request-SetMachinePowerState.ps1 -MachineName {0} -Action Restart -Reason 'Active Monitoring RemoteServerRestartResponder from host {1}'", failedServer2);
					try
					{
						Collection<PSObject> collection = this.RemotePowerShell.InvokePSCommand(pscommand);
						StringBuilder stringBuilder = new StringBuilder();
						if (collection != null)
						{
							foreach (PSObject psobject in collection)
							{
								stringBuilder.AppendLine(psobject.ToString());
							}
						}
						this.Result.StateAttribute5 = "Execution Result=" + stringBuilder.ToString();
					}
					catch (Exception arg)
					{
						this.errorMessage = string.Format("Powershell Invoke exception: Command={0}, Ex={1}", pscommand.Commands[0].CommandText, arg);
						this.Result.StateAttribute5 = string.Format("Powershell Invoke exception: Command={0}, Ex={1}", pscommand.Commands[0].CommandText, arg);
					}
				}
				return string.Empty;
			});
		}

		// Token: 0x04000982 RID: 2434
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000983 RID: 2435
		private static readonly string TypeName = typeof(PotentialOneCopyRemoteServerRestartResponder).FullName;

		// Token: 0x04000984 RID: 2436
		private string errorMessage;
	}
}
