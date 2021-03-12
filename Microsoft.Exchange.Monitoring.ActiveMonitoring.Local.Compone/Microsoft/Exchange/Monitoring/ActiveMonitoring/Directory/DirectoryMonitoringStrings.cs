using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x0200013B RID: 315
	public static class DirectoryMonitoringStrings
	{
		// Token: 0x0400064C RID: 1612
		public const string NTDSServiceName = "NTDS";

		// Token: 0x0400064D RID: 1613
		public const string MSExchangeADTopologyServiceName = "MSExchangeADTopology";

		// Token: 0x0400064E RID: 1614
		public const string MSExchangeProtectedServiceHostServiceName = "MSExchangeProtectedServiceHost";

		// Token: 0x0400064F RID: 1615
		public const string KDCServiceName = "KDC";

		// Token: 0x04000650 RID: 1616
		public const string MSExchangeRPCServiceName = "MSExchangeRPC";

		// Token: 0x04000651 RID: 1617
		public static DirectoryMonitoringScenario NTDSNotRunning = new DirectoryMonitoringScenario("NTDSServiceNotRunning", Strings.ServiceNotRunningEscalationMessage("NTDS"));

		// Token: 0x04000652 RID: 1618
		public static DirectoryMonitoringScenario MSExchangeADTopologyNotRunning = new DirectoryMonitoringScenario("MSExchangeADTopologyNotRunning", Strings.ServiceNotRunningEscalationMessage("MSExchangeADTopology"));

		// Token: 0x04000653 RID: 1619
		public static DirectoryMonitoringScenario MSExchangeProtectedServiceHostNotRunning = new DirectoryMonitoringScenario("MSExchangeProtectedServiceHostNotRunning", Strings.ServiceNotRunningEscalationMessage("MSExchangeProtectedServiceHost"));

		// Token: 0x04000654 RID: 1620
		public static DirectoryMonitoringScenario MSExchangeProtectedServiceHostCrashing = new DirectoryMonitoringScenario("MSExchangeProtectedServiceHostCrashing", Strings.MSExchangeProtectedServiceHostCrashingMessage);

		// Token: 0x04000655 RID: 1621
		public static DirectoryMonitoringScenario KDCServiceStatusTest = new DirectoryMonitoringScenario("KDCServiceStatusTest", Strings.KDCServiceStatusTestMessage);

		// Token: 0x04000656 RID: 1622
		public static DirectoryMonitoringScenario KDCNotRunning = new DirectoryMonitoringScenario("KDCServiceNotRunning", Strings.ServiceNotRunningEscalationMessage("KDC"));

		// Token: 0x04000657 RID: 1623
		public static DirectoryMonitoringScenario ActiveDirectoryConnectivity = new DirectoryMonitoringScenario("ActiveDirectoryConnectivity", Strings.ActiveDirectoryConnectivityEscalationMessage, true);

		// Token: 0x04000658 RID: 1624
		public static DirectoryMonitoringScenario ActiveDirectoryConnectivityLocal = new DirectoryMonitoringScenario("ActiveDirectoryConnectivityLocal", Strings.ActiveDirectoryConnectivityLocalEscalationMessage);

		// Token: 0x04000659 RID: 1625
		public static DirectoryMonitoringScenario ActiveDirectoryConnectivityConfigDC = new DirectoryMonitoringScenario("ActiveDirectoryConnectivityConfigDC", Strings.ActiveDirectoryConnectivityConfigDCEscalationMessage, true);

		// Token: 0x0400065A RID: 1626
		public static DirectoryMonitoringScenario NtlmConnectivity = new DirectoryMonitoringScenario("NtlmConnectivity", Strings.NtlmConnectivityEscalationMessage, true);

		// Token: 0x0400065B RID: 1627
		public static DirectoryMonitoringScenario TopologyServiceConnectivity = new DirectoryMonitoringScenario("TopologyServiceConnectivity", Strings.TopologyServiceConnectivityEscalationMessage);

		// Token: 0x0400065C RID: 1628
		public static DirectoryMonitoringScenario LiveIdAuthenticationConnectivity = new DirectoryMonitoringScenario("LiveIdAuthenticationConnectivity", Strings.LiveIdAuthenticationEscalationMesage, true);

		// Token: 0x0400065D RID: 1629
		public static DirectoryMonitoringScenario GlsConnectivity = new DirectoryMonitoringScenario("GlsConnectivity", Strings.GLSEscalationMessage, true);

		// Token: 0x0400065E RID: 1630
		public static DirectoryMonitoringScenario OfflineGLS = new DirectoryMonitoringScenario("OfflineGls", Strings.OfflineGLSEscalationMessage, true);

		// Token: 0x0400065F RID: 1631
		public static DirectoryMonitoringScenario MSExchangeInformationStoreCannotContactAD = new DirectoryMonitoringScenario("MSExchangeInformationStoreCannotContactAD", Strings.MSExchangeInformationStoreCannotContactADEscalationMessage, true);

		// Token: 0x04000660 RID: 1632
		public static DirectoryMonitoringScenario TopoDiscoveryFailedAllServers = new DirectoryMonitoringScenario("TopoDiscoveryFailedAllServers", Strings.TopoDiscoveryFailedAllServersEscalationMessage, true);

		// Token: 0x04000661 RID: 1633
		public static DirectoryMonitoringScenario RidConsumption = new DirectoryMonitoringScenario("RidConsumption", Strings.RidMonitorEscalationMessage);

		// Token: 0x04000662 RID: 1634
		public static DirectoryMonitoringScenario RidSetValidation = new DirectoryMonitoringScenario("RidSetValidation", Strings.RidSetMonitorEscalationMessage);

		// Token: 0x04000663 RID: 1635
		public static DirectoryMonitoringScenario RidPoolRequestFailed = new DirectoryMonitoringScenario("RidPoolRequestFailed", Strings.RequestForNewRidPoolFailedEscalationMessage);

		// Token: 0x04000664 RID: 1636
		public static DirectoryMonitoringScenario KerbAuthFailure = new DirectoryMonitoringScenario("KerbAuthFailure", Strings.KerbAuthFailureEscalationMessage);

		// Token: 0x04000665 RID: 1637
		public static DirectoryMonitoringScenario PACValidationFailure = new DirectoryMonitoringScenario("PACValidationFailure", Strings.KerbAuthFailureEscalationMessagPAC);

		// Token: 0x04000666 RID: 1638
		public static DirectoryMonitoringScenario SyntheticReplicationTransactionAll = new DirectoryMonitoringScenario("SyntheticReplicationTransactionAll", Strings.SyntheticReplicationTransactionEscalationMessage);

		// Token: 0x04000667 RID: 1639
		public static DirectoryMonitoringScenario PassiveReplicationMonitorAll = new DirectoryMonitoringScenario("PassiveReplicationMonitorAll", Strings.PassiveReplicationMonitorEscalationMessage);

		// Token: 0x04000668 RID: 1640
		public static DirectoryMonitoringScenario PassiveReplicationPerformanceCounterProbeAll = new DirectoryMonitoringScenario("PassiveReplicationPerformanceCounterProbeAll", Strings.PassiveReplicationPerformanceCounterProbeEscalationMessage);

		// Token: 0x04000669 RID: 1641
		public static DirectoryMonitoringScenario PassiveADReplicationMonitorAll = new DirectoryMonitoringScenario("PassiveADReplicationMonitorAll", Strings.PassiveADReplicationMonitorEscalationMessage);

		// Token: 0x0400066A RID: 1642
		public static DirectoryMonitoringScenario TrustMonitorProbeAll = new DirectoryMonitoringScenario("TrustMonitorProbeAll", Strings.TrustMonitorProbeEscalationMessage);

		// Token: 0x0400066B RID: 1643
		public static DirectoryMonitoringScenario RemoteDomainControllerState = new DirectoryMonitoringScenario("RemoteDomainControllerState", Strings.RemoteDomainControllerStateEscalationMessage);

		// Token: 0x0400066C RID: 1644
		public static DirectoryMonitoringScenario SyntheticReplicationMonitorRID = new DirectoryMonitoringScenario("SyntheticReplicationMonitorRID", Strings.SyntheticReplicationMonitorEscalationMessage);

		// Token: 0x0400066D RID: 1645
		public static DirectoryMonitoringScenario ReplicationDisabled = new DirectoryMonitoringScenario("ReplicationDisabled", Strings.ReplicationDisabledEscalationMessage);

		// Token: 0x0400066E RID: 1646
		public static DirectoryMonitoringScenario ReplicationKccInsufficientInfo = new DirectoryMonitoringScenario("ReplicationKccInsufficientInfo", Strings.InsufficientInformationKCCEscalationMessage);

		// Token: 0x0400066F RID: 1647
		public static DirectoryMonitoringScenario ReplicationBridgehead = new DirectoryMonitoringScenario("ReplicationBridgehead", Strings.BridgeHeadReplicationEscalationMessage);

		// Token: 0x04000670 RID: 1648
		public static DirectoryMonitoringScenario ReplicationIncompatibleVector = new DirectoryMonitoringScenario("ReplicationIncompatibleVector", Strings.IncompatibleVectorEscalationMessage);

		// Token: 0x04000671 RID: 1649
		public static DirectoryMonitoringScenario ReplicationSlowADWrites = new DirectoryMonitoringScenario("ReplicationSlowADWrites", Strings.SlowADWritesEscalationMessage);

		// Token: 0x04000672 RID: 1650
		public static DirectoryMonitoringScenario ReplicationUnableToCompleteTopology = new DirectoryMonitoringScenario("ReplicationUnableToCompleteTopology", Strings.UnableToCompleteTopologyEscalationMessage);

		// Token: 0x04000673 RID: 1651
		public static DirectoryMonitoringScenario ReplicationDraPendingReps = new DirectoryMonitoringScenario("ReplicationDraPendingReps", Strings.DRAPendingReplication5MinutesEscalationMessage);

		// Token: 0x04000674 RID: 1652
		public static DirectoryMonitoringScenario OutStandingATQRequests = new DirectoryMonitoringScenario("OutStandingATQRequests", Strings.OutStandingATQRequests15MinutesEscalationMessage);

		// Token: 0x04000675 RID: 1653
		public static DirectoryMonitoringScenario HighProcessor = new DirectoryMonitoringScenario("HighProcessor", Strings.HighProcessor15MinutesEscalationMessage);

		// Token: 0x04000676 RID: 1654
		public static DirectoryMonitoringScenario ReplicationOutdatedObjectsFailed = new DirectoryMonitoringScenario("ReplicationOutdatedObjectsFailed", Strings.ReplicationOutdatedObjectsFailedEscalationMessage);

		// Token: 0x04000677 RID: 1655
		public static DirectoryMonitoringScenario ReplicationFailures = new DirectoryMonitoringScenario("ReplicationFailures", Strings.ReplicationFailuresEscalationMessage);

		// Token: 0x04000678 RID: 1656
		public static DirectoryMonitoringScenario ADCannotBoot = new DirectoryMonitoringScenario("ADCannotBoot", Strings.CannotBootEscalationMessage);

		// Token: 0x04000679 RID: 1657
		public static DirectoryMonitoringScenario ADFailedToUpgradeIndex = new DirectoryMonitoringScenario("ADFailedToUpgradeIndex", Strings.FailedToUpgradeIndexEscalationMessage);

		// Token: 0x0400067A RID: 1658
		public static DirectoryMonitoringScenario ADReinstallServer = new DirectoryMonitoringScenario("ADReinstallServer", Strings.ReinstallServerEscalationMessage);

		// Token: 0x0400067B RID: 1659
		public static DirectoryMonitoringScenario ADCannotFunctionNormally = new DirectoryMonitoringScenario("ADCannotFunctionNormally", Strings.CannotFunctionNormallyEscalationMessage);

		// Token: 0x0400067C RID: 1660
		public static DirectoryMonitoringScenario ADCannotRecover = new DirectoryMonitoringScenario("ADCannotRecover", Strings.CannotRecoverEscalationMessage);

		// Token: 0x0400067D RID: 1661
		public static DirectoryMonitoringScenario ADSchemaPartitionFailed = new DirectoryMonitoringScenario("ADSchemaPartitionFailed", Strings.SchemaPartitionFailedEscalationMessage);

		// Token: 0x0400067E RID: 1662
		public static DirectoryMonitoringScenario ADCannotRebuildIndex = new DirectoryMonitoringScenario("ADCannotRebuildIndex", Strings.CannotRebuildIndexEscalationMessage);

		// Token: 0x0400067F RID: 1663
		public static DirectoryMonitoringScenario ADContentsUnpredictable = new DirectoryMonitoringScenario("ADContentsUnpredictable", Strings.ContentsUnpredictableEscalationMessage);

		// Token: 0x04000680 RID: 1664
		public static DirectoryMonitoringScenario ADNoNTDSObject = new DirectoryMonitoringScenario("ADNoNTDSObject", Strings.NoNTDSObjectEscalationMessage);

		// Token: 0x04000681 RID: 1665
		public static DirectoryMonitoringScenario ADVersionStore623 = new DirectoryMonitoringScenario("ADVersionStore623", Strings.VersionStore623EscalationMessage);

		// Token: 0x04000682 RID: 1666
		public static DirectoryMonitoringScenario ADVersionStore2008 = new DirectoryMonitoringScenario("ADVersionStore2008", Strings.VersionStore2008EscalationMessage);

		// Token: 0x04000683 RID: 1667
		public static DirectoryMonitoringScenario ADVersionStore1479 = new DirectoryMonitoringScenario("ADVersionStore1479", Strings.VersionStore1479EscalationMessage);

		// Token: 0x04000684 RID: 1668
		public static DirectoryMonitoringScenario DsNotificationQueue = new DirectoryMonitoringScenario("DsNotificationQueue", Strings.DSNotifyQueueHigh15MinutesEscalationMessage);

		// Token: 0x04000685 RID: 1669
		public static DirectoryMonitoringScenario ADDatabaseCorruption1017 = new DirectoryMonitoringScenario("ADDatabaseCorruption1017", Strings.ADDatabaseCorruption1017EscalationMessage);

		// Token: 0x04000686 RID: 1670
		public static DirectoryMonitoringScenario LogicalDiskFreeMegabytes = new DirectoryMonitoringScenario("LogicalDiskFreeMegabytes", Strings.LogicalDiskFreeMegabytesEscalationMessage);

		// Token: 0x04000687 RID: 1671
		public static DirectoryMonitoringScenario ADDatabaseCorrupt = new DirectoryMonitoringScenario("ADDatabaseCorrupt", Strings.DatabaseCorruptEscalationMessage);

		// Token: 0x04000688 RID: 1672
		public static DirectoryMonitoringScenario NTDSCorruption = new DirectoryMonitoringScenario("NTDSCorruption", Strings.NTDSCorruptionEscalationMessage);

		// Token: 0x04000689 RID: 1673
		public static DirectoryMonitoringScenario ADDatabaseCorruption = new DirectoryMonitoringScenario("ADDatabaseCorruption", Strings.ADDatabaseCorruptionEscalationMessage);

		// Token: 0x0400068A RID: 1674
		public static DirectoryMonitoringScenario ADCheckSum = new DirectoryMonitoringScenario("ADChecksum", Strings.CheckSumEscalationMessage);

		// Token: 0x0400068B RID: 1675
		public static DirectoryMonitoringScenario ADDataIssue = new DirectoryMonitoringScenario("ADDataIssue", Strings.DataIssueEscalationMessage);

		// Token: 0x0400068C RID: 1676
		public static DirectoryMonitoringScenario DatabaseCorruption = new DirectoryMonitoringScenario("DatabaseCorruption", Strings.DatabaseCorruptionEscalationMessage);

		// Token: 0x0400068D RID: 1677
		public static DirectoryMonitoringScenario RaidDegraded = new DirectoryMonitoringScenario("RaidDegraded", Strings.RaidDegradedEscalationMessage);

		// Token: 0x0400068E RID: 1678
		public static DirectoryMonitoringScenario DeviceDegraded = new DirectoryMonitoringScenario("DeviceDegraded", Strings.DeviceDegradedEscalationMessage);

		// Token: 0x0400068F RID: 1679
		public static DirectoryMonitoringScenario PermanentExceptionInRelocationService = new DirectoryMonitoringScenario("PermanentExceptionInRelocationService", Strings.RelocationServicePermanentExceptionMessage);

		// Token: 0x04000690 RID: 1680
		public static DirectoryMonitoringScenario TenantRelocationErrorsFound = new DirectoryMonitoringScenario("TenantRelocationErrorsFound", Strings.TenantRelocationErrorsFoundExceptionMessage);

		// Token: 0x04000691 RID: 1681
		public static DirectoryMonitoringScenario SCTNotFoundForAllVersions = new DirectoryMonitoringScenario("SCTNotFoundForAllVersions", Strings.SCTNotFoundForAllVersionsExceptionMessage);

		// Token: 0x04000692 RID: 1682
		public static DirectoryMonitoringScenario SCTMonitoringScriptException = new DirectoryMonitoringScenario("SCTMonitoringScriptException", Strings.SCTMonitoringScriptExceptionMessage);

		// Token: 0x04000693 RID: 1683
		public static DirectoryMonitoringScenario InocrrectSCTStateException = new DirectoryMonitoringScenario("InocrrectSCTStateException", Strings.InocrrectSCTStateExceptionMessage);

		// Token: 0x04000694 RID: 1684
		public static DirectoryMonitoringScenario SCTStateMonitoringScriptException = new DirectoryMonitoringScenario("SCTStateMonitoringScriptException", Strings.SCTStateMonitoringScriptExceptionMessage);

		// Token: 0x04000695 RID: 1685
		public static DirectoryMonitoringScenario DivergenceBetweenCAAndAD1003 = new DirectoryMonitoringScenario("DivergenceBetweenCAAndAD1003", Strings.DivergenceBetweenCAAndAD1003EscalationMessage);

		// Token: 0x04000696 RID: 1686
		public static DirectoryMonitoringScenario CheckDCMMDivergenceScriptException = new DirectoryMonitoringScenario("CheckDCMMDivergenceScriptException", Strings.CheckDCMMDivergenceScriptExceptionMessage);

		// Token: 0x04000697 RID: 1687
		public static DirectoryMonitoringScenario DivergenceInDefinition = new DirectoryMonitoringScenario("DivergenceInDefinition", Strings.DivergenceInDefinitionEscalationMessage);

		// Token: 0x04000698 RID: 1688
		public static DirectoryMonitoringScenario DivergenceBetweenCAAndAD1006 = new DirectoryMonitoringScenario("DivergenceBetweenCAAndAD1006", Strings.DivergenceBetweenCAAndAD1006EscalationMessage);

		// Token: 0x04000699 RID: 1689
		public static DirectoryMonitoringScenario DivergenceInSiteName = new DirectoryMonitoringScenario("DivergenceInSiteName", Strings.DivergenceInSiteNameEscalationMessage);

		// Token: 0x0400069A RID: 1690
		public static DirectoryMonitoringScenario ProvisionedDCBelowMinimum = new DirectoryMonitoringScenario("ProvisionedDCBelowMinimum", Strings.ProvisionedDCBelowMinimumEscalationMessage);

		// Token: 0x0400069B RID: 1691
		public static DirectoryMonitoringScenario CheckProvisionedDCException = new DirectoryMonitoringScenario("CheckProvisionedDCException", Strings.CheckProvisionedDCExceptionMessage);

		// Token: 0x0400069C RID: 1692
		public static DirectoryMonitoringScenario FSMODCNotProvisioned = new DirectoryMonitoringScenario("FSMODCNotProvisioned", Strings.FSMODCNotProvisionedEscalationMessage);

		// Token: 0x0400069D RID: 1693
		public static DirectoryMonitoringScenario CheckFsmoRolesScriptException = new DirectoryMonitoringScenario("CheckFsmoRolesScriptException", Strings.CheckFsmoRolesScriptExceptionMessage);

		// Token: 0x0400069E RID: 1694
		public static DirectoryMonitoringScenario DirectoryConfigDiscrepancy = new DirectoryMonitoringScenario("DirectoryConfigDiscrepancy", Strings.DirectoryConfigDiscrepancyEscalationMessage);

		// Token: 0x0400069F RID: 1695
		public static DirectoryMonitoringScenario DirectorySettingOverride = new DirectoryMonitoringScenario("DirectorySettingOverride", "A directory setting override validation failed. Please see event message for details");

		// Token: 0x040006A0 RID: 1696
		public static DirectoryMonitoringScenario CheckZombieDCScriptException = new DirectoryMonitoringScenario("CheckZombieDCScriptException", Strings.CheckZombieDCEscalateMessage);

		// Token: 0x040006A1 RID: 1697
		public static DirectoryMonitoringScenario DoMTConnectivity = new DirectoryMonitoringScenario("DoMTConnectivity", Strings.DoMTConnectivityEscalateMessage);
	}
}
