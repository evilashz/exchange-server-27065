using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.HA.ManagedAvailability;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000F5 RID: 245
	public class SystemFailoverResponder : ResponderWorkItem
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0002D57C File Offset: 0x0002B77C
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0002D584 File Offset: 0x0002B784
		internal string ComponentName { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0002D58D File Offset: 0x0002B78D
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0002D595 File Offset: 0x0002B795
		internal string[] ServersInGroup { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0002D59E File Offset: 0x0002B79E
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0002D5A6 File Offset: 0x0002B7A6
		internal int MinimumRequiredServers { get; set; }

		// Token: 0x060007A0 RID: 1952 RVA: 0x0002D5B0 File Offset: 0x0002B7B0
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, ServiceHealthStatus responderTargetState, string componentName, string serviceName = "Exchange", bool enabled = true)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = SystemFailoverResponder.AssemblyPath;
			responderDefinition.TypeName = SystemFailoverResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = monitorName;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = enabled;
			responderDefinition.Attributes["ComponentName"] = componentName;
			responderDefinition.Attributes["MinimumRequiredServers"] = -1.ToString();
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.ServerFailover, Environment.MachineName, null);
			return responderDefinition;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0002D66C File Offset: 0x0002B86C
		protected virtual void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			if (attributeHelper == null)
			{
				attributeHelper = new AttributeHelper(base.Definition);
			}
			this.ComponentName = attributeHelper.GetString("ComponentName", true, null);
			this.ServersInGroup = Dependencies.ThrottleHelper.Settings.GetServersInGroup("Dag");
			this.MinimumRequiredServers = attributeHelper.GetInt("MinimumRequiredServers", false, -1, null, null);
			if (this.MinimumRequiredServers == -1)
			{
				this.MinimumRequiredServers = this.ServersInGroup.Length / 2 + 1;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002D74C File Offset: 0x0002B94C
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes(null);
			if (!RegistryHelper.GetPropertyIntBool("IsSystemFailoverEnabled", true, null, null, false))
			{
				base.Result.StateAttribute1 = "System failover disabled";
				return;
			}
			Component component = Component.FindWellKnownComponent(this.ComponentName);
			if (component != null)
			{
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "SystemFailoverResponder.DoWork: Attempting to perform system failover (componentName={0})", component.ToString(), null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\SystemFailoverResponder.cs", 163);
				new RecoveryActionRunner(RecoveryActionId.ServerFailover, Environment.MachineName, this, true, cancellationToken, null)
				{
					IsIgnoreResourceName = true
				}.Execute(delegate()
				{
					string comment = string.Format("Managed availability system failover initiated by Responder={0} Component={1}.", this.Definition.Name, component.Name);
					this.PerformSystemFailoverAsync(component.ToString(), comment);
				});
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002D8A4 File Offset: 0x0002BAA4
		private void PerformSystemFailoverAsync(string componentName, string comment)
		{
			ThreadPool.QueueUserWorkItem(delegate(object unused)
			{
				RecoveryActionHelper.RunAndMeasure(string.Format("SystemFailover(WorkitemId={0}, ResultId={1}, Component={2}, Comment{3})", new object[]
				{
					this.Id,
					this.Result.ResultId,
					componentName,
					comment
				}), false, ManagedAvailabilityCrimsonEvents.MeasureOperation, delegate
				{
					ManagedAvailabilityHelper.PerformSystemFailover(componentName, comment);
					return string.Empty;
				});
			});
		}

		// Token: 0x04000518 RID: 1304
		internal readonly TimeSpan CoordinatedQueryDuration = TimeSpan.FromDays(1.0);

		// Token: 0x04000519 RID: 1305
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400051A RID: 1306
		private static readonly string TypeName = typeof(SystemFailoverResponder).FullName;

		// Token: 0x020000F6 RID: 246
		internal static class AttributeNames
		{
			// Token: 0x0400051E RID: 1310
			internal const string IsAutomaticallyDetectServers = "IsAutomaticallyDetectServers";

			// Token: 0x0400051F RID: 1311
			internal const string ComponentName = "ComponentName";

			// Token: 0x04000520 RID: 1312
			internal const string MinimumRequiredServers = "MinimumRequiredServers";
		}

		// Token: 0x020000F7 RID: 247
		internal static class DefaultValues
		{
			// Token: 0x04000521 RID: 1313
			internal const string[] ServersInGroup = null;

			// Token: 0x04000522 RID: 1314
			internal const bool IsAutomaticallyDetectServers = true;

			// Token: 0x04000523 RID: 1315
			internal const int MinimumRequiredServers = -1;

			// Token: 0x04000524 RID: 1316
			internal const string ServiceName = "Exchange";

			// Token: 0x04000525 RID: 1317
			internal const bool Enabled = true;
		}
	}
}
