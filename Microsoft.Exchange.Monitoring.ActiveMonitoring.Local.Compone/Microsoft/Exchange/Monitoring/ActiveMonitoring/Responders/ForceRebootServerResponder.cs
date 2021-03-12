using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ServiceContextProvider;
using Microsoft.Forefront.RecoveryActionArbiter.Contract;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x02000052 RID: 82
	public class ForceRebootServerResponder : ResponderWorkItem
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00012025 File Offset: 0x00010225
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0001202D File Offset: 0x0001022D
		internal string[] ServersInGroup { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00012036 File Offset: 0x00010236
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0001203E File Offset: 0x0001023E
		internal int MinimumRequiredServers { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00012047 File Offset: 0x00010247
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0001204F File Offset: 0x0001024F
		internal string RecoveryId { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00012058 File Offset: 0x00010258
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00012060 File Offset: 0x00010260
		internal string FailureReason { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00012069 File Offset: 0x00010269
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00012071 File Offset: 0x00010271
		internal string FfoArbitrationScope { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0001207A File Offset: 0x0001027A
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00012082 File Offset: 0x00010282
		internal string FfoArbitrationSource { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0001208B File Offset: 0x0001028B
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00012093 File Offset: 0x00010293
		internal string FfoRequestedAction { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0001209C File Offset: 0x0001029C
		// (set) Token: 0x0600029D RID: 669 RVA: 0x000120A4 File Offset: 0x000102A4
		internal bool IgnoreGroupThrottlingWhenMajorityNotSucceeded { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000120AD File Offset: 0x000102AD
		internal bool IsServersInGroupNullOrEmpty
		{
			get
			{
				return this.ServersInGroup == null || this.ServersInGroup.Length == 0;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000120C4 File Offset: 0x000102C4
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, ServiceHealthStatus responderTargetState, string[] serversInGroup = null, int minimumRequiredServers = -1, string recoveryId = "", string failureReason = "", string arbitrationScope = "Datacenter, Stamp", string arbitrationSource = "RecoveryData", string requestedAction = "ArbitrationOnly", string serviceName = "Exchange", bool enabled = true, string throttleGroupName = null, bool isIgnoreGroupThrottlingWhenMajorityNotSucceeded = false)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = ForceRebootServerResponder.AssemblyPath;
			responderDefinition.TypeName = ForceRebootServerResponder.TypeName;
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
			if (serversInGroup != null)
			{
				responderDefinition.Attributes["ServersInGroup"] = AttributeHelper.StringArrayToString(serversInGroup);
			}
			responderDefinition.Attributes["MinimumRequiredServers"] = minimumRequiredServers.ToString();
			responderDefinition.Attributes["RecoveryId"] = recoveryId;
			responderDefinition.Attributes["FailureReason"] = failureReason;
			responderDefinition.Attributes["ArbitrationScope"] = arbitrationScope;
			responderDefinition.Attributes["ArbitrationSource"] = arbitrationSource;
			responderDefinition.Attributes["RequestedAction"] = requestedAction;
			responderDefinition.Attributes["IgnoreGroupThrottling"] = isIgnoreGroupThrottlingWhenMajorityNotSucceeded.ToString();
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.ForceReboot, Environment.MachineName, serversInGroup);
			return responderDefinition;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000121F4 File Offset: 0x000103F4
		protected virtual void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			if (attributeHelper == null)
			{
				attributeHelper = new AttributeHelper(base.Definition);
			}
			this.IgnoreGroupThrottlingWhenMajorityNotSucceeded = attributeHelper.GetBool("IgnoreGroupThrottling", false, false);
			bool isMandatory = DatacenterRegistry.IsForefrontForOffice();
			if (this.IsServersInGroupNullOrEmpty)
			{
				this.ServersInGroup = attributeHelper.GetStrings("ServersInGroup", false, null, ';', true);
			}
			int num = this.IsServersInGroupNullOrEmpty ? 0 : this.ServersInGroup.Length;
			this.MinimumRequiredServers = attributeHelper.GetInt("MinimumRequiredServers", false, -1, null, null);
			if (num > 0 && this.MinimumRequiredServers == -1)
			{
				this.MinimumRequiredServers = num / 2 + 1;
			}
			this.RecoveryId = attributeHelper.GetString("RecoveryId", isMandatory, "");
			this.FailureReason = attributeHelper.GetString("FailureReason", isMandatory, "");
			this.FfoArbitrationScope = attributeHelper.GetString("ArbitrationScope", false, "Datacenter, Stamp");
			this.FfoArbitrationSource = attributeHelper.GetString("ArbitrationSource", false, "RecoveryData");
			this.FfoRequestedAction = attributeHelper.GetString("RequestedAction", false, "ArbitrationOnly");
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0001237C File Offset: 0x0001057C
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes(null);
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.ForceReboot, Environment.MachineName, this, true, cancellationToken, null);
			recoveryActionRunner.IgnoreGroupThrottlingWhenMajorityNotSucceeded = this.IgnoreGroupThrottlingWhenMajorityNotSucceeded;
			recoveryActionRunner.IsIgnoreResourceName = true;
			if (!this.IsServersInGroupNullOrEmpty)
			{
				recoveryActionRunner.SetServersInGroup(this.ServersInGroup);
			}
			string[] serversInGroup = recoveryActionRunner.GetServersInGroup();
			recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (instance.WindowsServerRoleEndpoint != null && instance.WindowsServerRoleEndpoint.IsDirectoryServiceRoleInstalled)
				{
					DirectoryGeneralUtils.InternalPutDCInMM(DirectoryGeneralUtils.GetLocalFQDN(), this.TraceContext);
				}
				if (DatacenterRegistry.IsForefrontForOffice())
				{
					this.ArbitrateForFfo();
					return;
				}
				this.ReportBugcheck(startEntry, serversInGroup);
				ForceRebootHelper.PerformBugcheck();
			});
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000123F4 File Offset: 0x000105F4
		private void ArbitrateForFfo()
		{
			ArbitrationScope scope = (ArbitrationScope)Enum.Parse(typeof(ArbitrationScope), this.FfoArbitrationScope);
			ArbitrationSource source = (ArbitrationSource)Enum.Parse(typeof(ArbitrationSource), this.FfoArbitrationSource);
			RequestedAction requestedAction = (RequestedAction)Enum.Parse(typeof(RequestedAction), this.FfoRequestedAction);
			bool flag = ServiceContextProvider.Instance.RequestApprovalForRecovery(this.RecoveryId, scope, source, requestedAction, 2, this.FailureReason, "");
			if (flag)
			{
				ForceRebootHelper.PerformBugcheck();
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001247C File Offset: 0x0001067C
		private void ReportBugcheck(RecoveryActionEntry startEntry, string[] serversInGroup)
		{
			PersistedGlobalActionEntry persistedGlobalActionEntry = new PersistedGlobalActionEntry(startEntry);
			if (!persistedGlobalActionEntry.WriteToFile(TimeSpan.FromSeconds(60.0)))
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.RecoveryActionTracer, base.TraceContext, "PersistedGlobalActionEntry.WriteToFile failed to complete in 60 seconds.", null, "ReportBugcheck", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\ForceRebootServerResponder.cs", 347);
			}
			if (!Utilities.IsSequenceNullOrEmpty<string>(serversInGroup))
			{
				string subkeyName = string.Format("RecoveryAction\\{0}", RecoveryActionId.ForceReboot);
				TimeSpan rpcTimeout = TimeSpan.FromMilliseconds((double)RegistryHelper.GetProperty<int>("LogCrimsonEventRpcTimeoutInMs", 25000, subkeyName, null, false));
				TimeSpan overallTimeout = TimeSpan.FromMilliseconds((double)RegistryHelper.GetProperty<int>("restartReportingTimeoutInMs", 30000, subkeyName, null, false));
				string timeStampStr = DateTime.UtcNow.ToString("o");
				ForceRebootHelper.ReportBugcheckToOtherServersInGroup(serversInGroup, Environment.MachineName, RecoveryActionId.ForceReboot, base.Definition.Name, startEntry.Context ?? string.Empty, timeStampStr, rpcTimeout, overallTimeout);
			}
		}

		// Token: 0x040001E5 RID: 485
		internal readonly TimeSpan CoordinatedQueryDuration = TimeSpan.FromDays(1.0);

		// Token: 0x040001E6 RID: 486
		internal readonly TimeSpan DurationToWaitToVerifyBugcheck = TimeSpan.FromSeconds(30.0);

		// Token: 0x040001E7 RID: 487
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040001E8 RID: 488
		private static readonly string TypeName = typeof(ForceRebootServerResponder).FullName;

		// Token: 0x02000053 RID: 83
		internal static class AttributeNames
		{
			// Token: 0x040001F1 RID: 497
			internal const string ServersInGroup = "ServersInGroup";

			// Token: 0x040001F2 RID: 498
			internal const string MinimumRequiredServers = "MinimumRequiredServers";

			// Token: 0x040001F3 RID: 499
			internal const string RecoveryId = "RecoveryId";

			// Token: 0x040001F4 RID: 500
			internal const string FailureReason = "FailureReason";

			// Token: 0x040001F5 RID: 501
			internal const string ArbitrationScope = "ArbitrationScope";

			// Token: 0x040001F6 RID: 502
			internal const string ArbitrationSource = "ArbitrationSource";

			// Token: 0x040001F7 RID: 503
			internal const string RequestedAction = "RequestedAction";

			// Token: 0x040001F8 RID: 504
			internal const string IgnoreGroupThrottlingWhenMajorityNotSucceeded = "IgnoreGroupThrottling";
		}

		// Token: 0x02000054 RID: 84
		internal static class DefaultValues
		{
			// Token: 0x040001F9 RID: 505
			internal const int MinimumRequiredServers = -1;

			// Token: 0x040001FA RID: 506
			internal const string RecoveryId = "";

			// Token: 0x040001FB RID: 507
			internal const string FailureReason = "";

			// Token: 0x040001FC RID: 508
			internal const string ArbitrationScope = "Datacenter, Stamp";

			// Token: 0x040001FD RID: 509
			internal const string ArbitrationSource = "RecoveryData";

			// Token: 0x040001FE RID: 510
			internal const string RequestedAction = "ArbitrationOnly";

			// Token: 0x040001FF RID: 511
			internal const bool IgnoreGroupThrottlingWhenMajorityNotSucceeded = false;

			// Token: 0x04000200 RID: 512
			internal const string ServiceName = "Exchange";

			// Token: 0x04000201 RID: 513
			internal const bool Enabled = true;
		}
	}
}
