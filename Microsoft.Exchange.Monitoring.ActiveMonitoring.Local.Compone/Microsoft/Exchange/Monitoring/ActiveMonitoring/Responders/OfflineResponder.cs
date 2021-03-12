using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ServiceContextProvider;
using Microsoft.Forefront.RecoveryActionArbiter.Contract;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000ED RID: 237
	public class OfflineResponder : ResponderWorkItem
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0002C6C6 File Offset: 0x0002A8C6
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0002C6CE File Offset: 0x0002A8CE
		internal string[] ServersInGroup { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0002C6D7 File Offset: 0x0002A8D7
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0002C6DF File Offset: 0x0002A8DF
		internal int MinimumRequiredServers { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0002C6E8 File Offset: 0x0002A8E8
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0002C6F0 File Offset: 0x0002A8F0
		internal int MaximumConcurrentOfflines { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0002C6F9 File Offset: 0x0002A8F9
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0002C701 File Offset: 0x0002A901
		internal string FailureReason { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0002C70A File Offset: 0x0002A90A
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0002C712 File Offset: 0x0002A912
		internal string FfoArbitrationScope { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0002C71B File Offset: 0x0002A91B
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0002C723 File Offset: 0x0002A923
		internal string FfoArbitrationSource { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0002C72C File Offset: 0x0002A92C
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x0002C734 File Offset: 0x0002A934
		internal string FfoRequestedAction { get; set; }

		// Token: 0x0600076F RID: 1903 RVA: 0x0002C740 File Offset: 0x0002A940
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, ServerComponentEnum componentToTakeOffline, ServiceHealthStatus responderTargetState, string serviceName, string[] serversInGroup, int minimumRequiredServers = -1, string failureReason = "", string arbitrationScope = "Datacenter", string arbitrationSource = "F5AvailabilityData", string requestedAction = "MachineOut")
		{
			if (!OfflineResponder.IsSupportedComponent(componentToTakeOffline))
			{
				throw new ArgumentException(string.Format("componentToTakeOffline was passed as '{0}'. This is not a supported componentId", componentToTakeOffline));
			}
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = OfflineResponder.AssemblyPath;
			responderDefinition.TypeName = OfflineResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = monitorName;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = true;
			responderDefinition.Attributes["ComponentToTakeOffline"] = componentToTakeOffline.ToString();
			if (serversInGroup != null)
			{
				responderDefinition.Attributes["ServersInGroup"] = string.Join(';'.ToString(), serversInGroup);
			}
			responderDefinition.Attributes["MinimumRequiredServers"] = minimumRequiredServers.ToString();
			responderDefinition.Attributes["FailureReason"] = failureReason;
			responderDefinition.Attributes["ArbitrationScope"] = arbitrationScope;
			responderDefinition.Attributes["ArbitrationSource"] = arbitrationSource;
			responderDefinition.Attributes["RequestedAction"] = requestedAction;
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, null, RecoveryActionId.TakeComponentOffline, componentToTakeOffline.ToString(), serversInGroup);
			return responderDefinition;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0002C894 File Offset: 0x0002AA94
		protected virtual void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			if (attributeHelper == null)
			{
				attributeHelper = new AttributeHelper(base.Definition);
			}
			bool isMandatory = DatacenterRegistry.IsForefrontForOffice();
			this.componentToTakeOffline = attributeHelper.GetEnum<ServerComponentEnum>("ComponentToTakeOffline", true, ServerComponentEnum.None);
			if (this.ServersInGroup == null || this.ServersInGroup.Length == 0)
			{
				this.ServersInGroup = attributeHelper.GetStrings("ServersInGroup", true, null, ';', true);
			}
			this.MinimumRequiredServers = attributeHelper.GetInt("MinimumRequiredServers", false, -1, null, null);
			if (this.MinimumRequiredServers == -1)
			{
				this.MinimumRequiredServers = (this.ServersInGroup.Length + 1) / 2;
			}
			this.FailureReason = attributeHelper.GetString("FailureReason", isMandatory, "");
			this.FfoArbitrationScope = attributeHelper.GetString("ArbitrationScope", false, "Datacenter");
			this.FfoArbitrationSource = attributeHelper.GetString("ArbitrationSource", false, "F5AvailabilityData");
			this.FfoRequestedAction = attributeHelper.GetString("RequestedAction", false, "MachineOut");
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0002C998 File Offset: 0x0002AB98
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes(null);
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.TakeComponentOffline, this.componentToTakeOffline.ToString(), this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate()
			{
				this.PerformOffline();
			});
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0002C9DC File Offset: 0x0002ABDC
		private static bool IsSupportedComponent(ServerComponentEnum compId)
		{
			switch (compId)
			{
			case ServerComponentEnum.None:
			case ServerComponentEnum.ServerWideOffline:
			case ServerComponentEnum.ServerWideSettings:
				return false;
			}
			return true;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0002CA08 File Offset: 0x0002AC08
		private void PerformOffline()
		{
			bool flag = false;
			Exception ex = null;
			try
			{
				if (DatacenterRegistry.IsForefrontForOffice())
				{
					this.RequestFfoApprovalAndTakeOffline();
				}
				else
				{
					this.ArbitrateAcrossServersAndTakeOffline();
				}
				flag = true;
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				if (!flag)
				{
					WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format("OfflineResponder: ForcedOnline: {0} (Reason: {1})", this.componentToTakeOffline.ToString(), (ex != null) ? ex.Message : "<none>"), null, "PerformOffline", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\OfflineResponder.cs", 311);
				}
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0002CAA4 File Offset: 0x0002ACA4
		private void RequestFfoApprovalAndTakeOffline()
		{
			ArbitrationScope scope = (ArbitrationScope)Enum.Parse(typeof(ArbitrationScope), this.FfoArbitrationScope);
			ArbitrationSource source = (ArbitrationSource)Enum.Parse(typeof(ArbitrationSource), this.FfoArbitrationSource);
			RequestedAction requestedAction = (RequestedAction)Enum.Parse(typeof(RequestedAction), this.FfoRequestedAction);
			bool flag = ServiceContextProvider.Instance.RequestApprovalForRecovery(this.componentToTakeOffline.ToString(), scope, source, requestedAction, 0, this.FailureReason, "");
			if (flag)
			{
				ServerComponentStateManager.SetOffline(this.componentToTakeOffline);
				return;
			}
			throw new RequestForFfoApprovalToOfflineFailedException();
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002CB40 File Offset: 0x0002AD40
		private void ArbitrateAcrossServersAndTakeOffline()
		{
			CoordinatedOfflineAction coordinatedOfflineAction = new CoordinatedOfflineAction(RecoveryActionId.TakeComponentOffline, this.QueryDuration, this.componentToTakeOffline, base.Definition.Name, this.MinimumRequiredServers, this.ServersInGroup);
			TimeSpan arbitrationTimeout = TimeSpan.FromMilliseconds(40000.0);
			coordinatedOfflineAction.InvokeOfflineOnMajority(arbitrationTimeout);
		}

		// Token: 0x040004E8 RID: 1256
		internal readonly TimeSpan QueryDuration = TimeSpan.FromMinutes(2.0);

		// Token: 0x040004E9 RID: 1257
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040004EA RID: 1258
		private static readonly string TypeName = typeof(OfflineResponder).FullName;

		// Token: 0x040004EB RID: 1259
		private ServerComponentEnum componentToTakeOffline;

		// Token: 0x020000EE RID: 238
		internal static class AttributeNames
		{
			// Token: 0x040004F3 RID: 1267
			internal const string ServersInGroup = "ServersInGroup";

			// Token: 0x040004F4 RID: 1268
			internal const string ComponentToTakeOffline = "ComponentToTakeOffline";

			// Token: 0x040004F5 RID: 1269
			internal const string MinimumRequiredServers = "MinimumRequiredServers";

			// Token: 0x040004F6 RID: 1270
			internal const string FailureReason = "FailureReason";

			// Token: 0x040004F7 RID: 1271
			internal const string ArbitrationScope = "ArbitrationScope";

			// Token: 0x040004F8 RID: 1272
			internal const string ArbitrationSource = "ArbitrationSource";

			// Token: 0x040004F9 RID: 1273
			internal const string RequestedAction = "RequestedAction";
		}

		// Token: 0x020000EF RID: 239
		internal static class DefaultValues
		{
			// Token: 0x040004FA RID: 1274
			internal const int MinimumRequiredServers = -1;

			// Token: 0x040004FB RID: 1275
			internal const string FailureReason = "";

			// Token: 0x040004FC RID: 1276
			internal const string ArbitrationScope = "Datacenter";

			// Token: 0x040004FD RID: 1277
			internal const string ArbitrationSource = "F5AvailabilityData";

			// Token: 0x040004FE RID: 1278
			internal const string RequestedAction = "MachineOut";
		}
	}
}
