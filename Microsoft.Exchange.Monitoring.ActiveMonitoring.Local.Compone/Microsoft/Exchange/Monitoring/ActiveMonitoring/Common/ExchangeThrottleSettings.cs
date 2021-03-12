using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000112 RID: 274
	internal class ExchangeThrottleSettings : ThrottleSettingsBase
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00031164 File Offset: 0x0002F364
		internal static ExchangeThrottleSettings Instance
		{
			get
			{
				return ExchangeThrottleSettings.lazy.Value;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00031170 File Offset: 0x0002F370
		private ExchangeThrottleSettings()
		{
			base.Initialize(ExchangeThrottleSettings.BaseSettings, ResponderDefinition.GlobalOverrides, ResponderDefinition.LocalOverrides);
			base.ReportAllThrottleEntriesToCrimson(true);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00031194 File Offset: 0x0002F394
		public override FixedThrottleEntry ConstructDefaultThrottlingSettings(RecoveryActionId recoveryActionId)
		{
			return new FixedThrottleEntry(recoveryActionId, 60, -1, 1, 60, 4);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000311A3 File Offset: 0x0002F3A3
		private static bool IsDagGroup(string categoryName)
		{
			return string.Equals(categoryName, "Dag", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000311B1 File Offset: 0x0002F3B1
		private static bool IsCafeGroup(string categoryName)
		{
			return string.Equals(categoryName, "Cafe", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000311BF File Offset: 0x0002F3BF
		private static bool IsDomainControllerGroup(string categoryName)
		{
			return string.Equals(categoryName, "DomainController", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000311CD File Offset: 0x0002F3CD
		private static bool IsCentralAdminGroup(string categoryName)
		{
			return string.Equals(categoryName, "CentralAdmin", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000311DB File Offset: 0x0002F3DB
		public override string[] GetServersInGroup(string categoryName)
		{
			return Dependencies.ThrottleHelper.GetServersInGroup(categoryName);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000311E8 File Offset: 0x0002F3E8
		internal static string[] ResolveKnownExchangeGroupToServers(string categoryName)
		{
			if (ExchangeThrottleSettings.IsDagGroup(categoryName))
			{
				return DagUtils.GetMailboxServersInSameDag();
			}
			if (ExchangeThrottleSettings.IsCafeGroup(categoryName))
			{
				return CafeUtils.GetCafeServersInSameArray();
			}
			if (ExchangeThrottleSettings.IsDomainControllerGroup(categoryName))
			{
				return ADUtils.GetDomainControllersInSameSite();
			}
			if (ExchangeThrottleSettings.IsCentralAdminGroup(categoryName))
			{
				return ADUtils.GetCentralAdminSvrsInSameSite();
			}
			throw new InvalidOperationException(string.Format("Unknown group category '{0}' specified.", categoryName));
		}

		// Token: 0x04000596 RID: 1430
		internal const int DefaultLocalMinimumMinutesBetweenAttempts = 60;

		// Token: 0x04000597 RID: 1431
		internal const int DefaultLocalMaximumAllowedAttemptsInOneHour = -1;

		// Token: 0x04000598 RID: 1432
		internal const int DefaultLocalMaximumAllowedAttemptsInADay = 1;

		// Token: 0x04000599 RID: 1433
		internal const int DefaultGroupMinimumMinutesBetweenAttempts = 60;

		// Token: 0x0400059A RID: 1434
		internal const int DefaultGroupMaximumAllowedAttemptsInADay = 4;

		// Token: 0x0400059B RID: 1435
		private static readonly Lazy<ExchangeThrottleSettings> lazy = new Lazy<ExchangeThrottleSettings>(() => new ExchangeThrottleSettings());

		// Token: 0x0400059C RID: 1436
		internal static readonly FixedThrottleEntry[] BaseSettings = new FixedThrottleEntry[]
		{
			new FixedThrottleEntry(RecoveryActionId.ForceReboot, 720, -1, 1, 600, 4),
			new FixedThrottleEntry(RecoveryActionId.ServerFailover, 60, -1, 1, 60, 4),
			new FixedThrottleEntry(RecoveryActionId.RestartService, 60, -1, 1, 60, 4),
			new FixedThrottleEntry(RecoveryActionId.RecycleApplicationPool, 60, -1, 1, 60, 4),
			new FixedThrottleEntry(RecoveryActionId.DatabaseFailover, 60, -1, 2, 60, 6),
			new FixedThrottleEntry(RecoveryActionId.TakeComponentOffline, 60, -1, 1, 60, 4),
			new FixedThrottleEntry(RecoveryActionId.MoveClusterGroup, 240, -1, 1, 480, 3),
			new FixedThrottleEntry(RecoveryActionId.ResumeCatalog, 5, 4, 8, 5, 12),
			new FixedThrottleEntry(RecoveryActionId.WatsonDump, 480, -1, 1, 720, 4),
			new FixedThrottleEntry(RecoveryActionId.ControlService, 60, -1, 1, 60, 4),
			new FixedThrottleEntry(RecoveryActionId.DeleteFile, 1, -1, 15, 1, 30),
			new FixedThrottleEntry(RecoveryActionId.PutDCInMM, 60, -1, 1, 60, 4),
			new FixedThrottleEntry(RecoveryActionId.KillProcess, 1, -1, 15, 1, 30),
			new FixedThrottleEntry(RecoveryActionId.RenameNTDSPowerOff, 60, -1, 1, 60, 4),
			new FixedThrottleEntry(RecoveryActionId.RpcClientAccessRestart, 4, -1, 2, 4, 8),
			new FixedThrottleEntry(RecoveryActionId.RemoteForceReboot, 5, -1, 2, 15, 7),
			new FixedThrottleEntry(RecoveryActionId.RestartService, "hostcontrollerservice", 60, -1, 6, -1, -1),
			new FixedThrottleEntry(RecoveryActionId.RestartService, "msexchangefastsearch", 60, -1, 4, -1, -1),
			new FixedThrottleEntry(RecoveryActionId.RestartFastNode, 60, -1, 6, -1, -1),
			new FixedThrottleEntry(RecoveryActionId.SetNetworkAdapter, 120, -1, 1, 60, 16),
			new FixedThrottleEntry(RecoveryActionId.AddRoute, 120, -1, 1, 60, 16),
			new FixedThrottleEntry(RecoveryActionId.ClusterNodeHammerDown, 720, -1, 2, 600, 7),
			new FixedThrottleEntry(RecoveryActionId.ClearLsassCache, 45, -1, 6, 3, 60),
			new FixedThrottleEntry(RecoveryActionId.Watson, 240, -1, 3, 120, 4),
			new FixedThrottleEntry(RecoveryActionId.RaiseFailureItem, 240, -1, 4, 5, 10),
			new FixedThrottleEntry(RecoveryActionId.PotentialOneCopyRemoteServerRestartResponder, 720, -1, 1, 720, 2),
			new FixedThrottleEntry(RecoveryActionId.RemoteForceReboot, ResponderCategory.Default, "Microsoft.Exchange.Monitoring.ActiveMonitoring.RemoteStore.Responders.RemoteStoreAdminRPCInterfaceForceRebootResponder", "RemoteStoreAdminRPCInterfaceKillServer", null, new ThrottleParameters(true, 60, -1, 2, 30, 7))
		};

		// Token: 0x02000113 RID: 275
		internal class WellKnownThrottleGroup
		{
			// Token: 0x0400059E RID: 1438
			internal const string Dag = "Dag";

			// Token: 0x0400059F RID: 1439
			internal const string Cafe = "Cafe";

			// Token: 0x040005A0 RID: 1440
			internal const string DomainController = "DomainController";

			// Token: 0x040005A1 RID: 1441
			internal const string CentralAdmin = "CentralAdmin";

			// Token: 0x040005A2 RID: 1442
			internal const string StampedGroup = "StampedGroup";
		}
	}
}
