using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.ObjectModel.EventLog
{
	// Token: 0x02000297 RID: 663
	public static class TaskEventLogConstants
	{
		// Token: 0x040006FE RID: 1790
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogCmdletSuccess = new ExEventLog.EventTuple(1073741825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040006FF RID: 1791
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogLowLevelCmdletSuccess = new ExEventLog.EventTuple(1073741826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000700 RID: 1792
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogMediumLevelCmdletSuccess = new ExEventLog.EventTuple(1073741827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000701 RID: 1793
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogCmdletStopped = new ExEventLog.EventTuple(2147483652U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000702 RID: 1794
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogCmdletCancelled = new ExEventLog.EventTuple(2147483653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000703 RID: 1795
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogCmdletError = new ExEventLog.EventTuple(3221225478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000704 RID: 1796
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogLowLevelCmdletError = new ExEventLog.EventTuple(3221225479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000705 RID: 1797
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TaskThrowingUnhandledException = new ExEventLog.EventTuple(3221225480U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000706 RID: 1798
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TaskThrottled = new ExEventLog.EventTuple(1073741874U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000707 RID: 1799
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SlimTenantTaskThrottled = new ExEventLog.EventTuple(1073741882U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000708 RID: 1800
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HydrationTaskFailed = new ExEventLog.EventTuple(3221229476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000709 RID: 1801
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DestructiveTaskThrottledForFirstOrg = new ExEventLog.EventTuple(1073741880U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400070A RID: 1802
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DestructiveTaskThrottledForTenant = new ExEventLog.EventTuple(1073741881U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400070B RID: 1803
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResourceHealthCutOff = new ExEventLog.EventTuple(1073741883U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400070C RID: 1804
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_NativeCallFailed = new ExEventLog.EventTuple(3221225487U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400070D RID: 1805
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_UserNotFoundBySid = new ExEventLog.EventTuple(3221225488U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400070E RID: 1806
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_NoRoleAssignments = new ExEventLog.EventTuple(3221225489U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400070F RID: 1807
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_NoValidEnabledRoleAssignments = new ExEventLog.EventTuple(3221225490U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000710 RID: 1808
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_UserNotEnabledForRemotePS = new ExEventLog.EventTuple(3221225491U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000711 RID: 1809
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CmdletAccessDenied_InvalidCmdlet = new ExEventLog.EventTuple(3221225492U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000712 RID: 1810
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CmdletAccessDenied_InvalidParameter = new ExEventLog.EventTuple(3221225493U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000713 RID: 1811
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBACUnavailable_TransientError = new ExEventLog.EventTuple(3221225494U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000714 RID: 1812
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBACUnavailable_UnknownError = new ExEventLog.EventTuple(3221225495U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000715 RID: 1813
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_DelegatedUser = new ExEventLog.EventTuple(3221225501U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000716 RID: 1814
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_NoPartnerScopes = new ExEventLog.EventTuple(3221225496U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000717 RID: 1815
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_OrgNotFound = new ExEventLog.EventTuple(3221225497U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000718 RID: 1816
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_OrgOutOfPartnerScope = new ExEventLog.EventTuple(3221225498U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000719 RID: 1817
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TaskMediumDetailWritingErrorInProcessing = new ExEventLog.EventTuple(3221225499U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400071A RID: 1818
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TaskMediumDetailWritingErrorNotProcessing = new ExEventLog.EventTuple(3221225500U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400071B RID: 1819
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_MultipleUsersFoundByCertificate = new ExEventLog.EventTuple(3221225502U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400071C RID: 1820
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_CannotCreateSecurityIdentifier = new ExEventLog.EventTuple(3221225523U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400071D RID: 1821
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_NoGroupsResolvedForDelegatedAdmin = new ExEventLog.EventTuple(3221225524U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400071E RID: 1822
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SnapinLoadFailed = new ExEventLog.EventTuple(3221225503U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400071F RID: 1823
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ScriptNotFound = new ExEventLog.EventTuple(3221225504U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000720 RID: 1824
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ScriptCorrupted = new ExEventLog.EventTuple(3221225505U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000721 RID: 1825
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_UserPrincipalNameNotSet = new ExEventLog.EventTuple(3221225506U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000722 RID: 1826
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToLoadValidationRules = new ExEventLog.EventTuple(3221225511U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000723 RID: 1827
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RoleBasedStringMappingFailure = new ExEventLog.EventTuple(2147483688U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000724 RID: 1828
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_FailedToMapPUIDToADAccount = new ExEventLog.EventTuple(3221225513U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000725 RID: 1829
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProvisioningCacheActivating = new ExEventLog.EventTuple(1073741866U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000726 RID: 1830
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProvisioningCacheDeactivating = new ExEventLog.EventTuple(1073741867U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000727 RID: 1831
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RBACUnavailable_InitialSessionStateIsNull = new ExEventLog.EventTuple(3221225516U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000728 RID: 1832
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RuntimeException = new ExEventLog.EventTuple(3221225517U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000729 RID: 1833
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBAC_TenantRedirectionFailed = new ExEventLog.EventTuple(3221225518U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400072A RID: 1834
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebConfigCorrupted = new ExEventLog.EventTuple(3221225519U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400072B RID: 1835
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SnapinTypeLoadFailed = new ExEventLog.EventTuple(3221225520U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400072C RID: 1836
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBACUnavailable_CannotOpenWebConfig = new ExEventLog.EventTuple(3221225521U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400072D RID: 1837
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReachedMaxPSConnectionLimit = new ExEventLog.EventTuple(3221225527U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400072E RID: 1838
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReachedMaxTenantPSConnectionLimit = new ExEventLog.EventTuple(3221225525U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400072F RID: 1839
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToResolveOrganizationIdForDelegatedPrincipal = new ExEventLog.EventTuple(3221225526U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000730 RID: 1840
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AccessDenied_CertificateAuthenticationNotAllowed = new ExEventLog.EventTuple(3221225532U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000731 RID: 1841
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProvisioningCacheError = new ExEventLog.EventTuple(3221225533U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000732 RID: 1842
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowershellSessionExpired = new ExEventLog.EventTuple(2147483710U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000733 RID: 1843
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReachedMaxUserPSConnectionLimit = new ExEventLog.EventTuple(3221225542U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000734 RID: 1844
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReachedMaxPSRunspaceInTimePeriodLimit = new ExEventLog.EventTuple(3221225543U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000735 RID: 1845
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReachedMaxPowershellCmdletLimit = new ExEventLog.EventTuple(3221225544U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000736 RID: 1846
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReachedMaxTenantPSRunspaceInTimePeriodLimit = new ExEventLog.EventTuple(3221225545U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000737 RID: 1847
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PSConnectionLeakDetected = new ExEventLog.EventTuple(3221225546U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000738 RID: 1848
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PSConnectionLeakPassivelyCorrected = new ExEventLog.EventTuple(3221225547U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000739 RID: 1849
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBACUnavailable_FatalError = new ExEventLog.EventTuple(3221225552U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400073A RID: 1850
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NullItemAddedIntoInitialSessionStateEntryCollection = new ExEventLog.EventTuple(3221225553U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400073B RID: 1851
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExecuteTaskScriptLatency = new ExEventLog.EventTuple(252U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400073C RID: 1852
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HandleADDriverTimeoutInPowershellStarted = new ExEventLog.EventTuple(253U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400073D RID: 1853
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HandleADDriverTimeoutInPowershellFinishedSuccessfully = new ExEventLog.EventTuple(254U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400073E RID: 1854
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HandleADDriverTimeoutInPowershellFinishedWithError = new ExEventLog.EventTuple(3221225727U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400073F RID: 1855
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TestRpsConnectivityRecoveryWorkflowRecyclePoolSuccessfully = new ExEventLog.EventTuple(256U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000740 RID: 1856
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToInitailizeCmdletDataRedactionConfiguration = new ExEventLog.EventTuple(3221225729U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000741 RID: 1857
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemotePSPublicAPIFailed = new ExEventLog.EventTuple(3221225730U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000742 RID: 1858
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PswsPublicAPIFailed = new ExEventLog.EventTuple(3221225733U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000743 RID: 1859
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidCultureInfo = new ExEventLog.EventTuple(3221225734U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000744 RID: 1860
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PswsOverBudgetException = new ExEventLog.EventTuple(3221225735U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000298 RID: 664
		private enum Category : short
		{
			// Token: 0x04000746 RID: 1862
			General = 1,
			// Token: 0x04000747 RID: 1863
			RBAC,
			// Token: 0x04000748 RID: 1864
			StringInterfacePacks
		}

		// Token: 0x02000299 RID: 665
		internal enum Message : uint
		{
			// Token: 0x0400074A RID: 1866
			LogCmdletSuccess = 1073741825U,
			// Token: 0x0400074B RID: 1867
			LogLowLevelCmdletSuccess,
			// Token: 0x0400074C RID: 1868
			LogMediumLevelCmdletSuccess,
			// Token: 0x0400074D RID: 1869
			LogCmdletStopped = 2147483652U,
			// Token: 0x0400074E RID: 1870
			LogCmdletCancelled,
			// Token: 0x0400074F RID: 1871
			LogCmdletError = 3221225478U,
			// Token: 0x04000750 RID: 1872
			LogLowLevelCmdletError,
			// Token: 0x04000751 RID: 1873
			TaskThrowingUnhandledException,
			// Token: 0x04000752 RID: 1874
			TaskThrottled = 1073741874U,
			// Token: 0x04000753 RID: 1875
			SlimTenantTaskThrottled = 1073741882U,
			// Token: 0x04000754 RID: 1876
			HydrationTaskFailed = 3221229476U,
			// Token: 0x04000755 RID: 1877
			DestructiveTaskThrottledForFirstOrg = 1073741880U,
			// Token: 0x04000756 RID: 1878
			DestructiveTaskThrottledForTenant,
			// Token: 0x04000757 RID: 1879
			ResourceHealthCutOff = 1073741883U,
			// Token: 0x04000758 RID: 1880
			AccessDenied_NativeCallFailed = 3221225487U,
			// Token: 0x04000759 RID: 1881
			AccessDenied_UserNotFoundBySid,
			// Token: 0x0400075A RID: 1882
			AccessDenied_NoRoleAssignments,
			// Token: 0x0400075B RID: 1883
			AccessDenied_NoValidEnabledRoleAssignments,
			// Token: 0x0400075C RID: 1884
			AccessDenied_UserNotEnabledForRemotePS,
			// Token: 0x0400075D RID: 1885
			CmdletAccessDenied_InvalidCmdlet,
			// Token: 0x0400075E RID: 1886
			CmdletAccessDenied_InvalidParameter,
			// Token: 0x0400075F RID: 1887
			RBACUnavailable_TransientError,
			// Token: 0x04000760 RID: 1888
			RBACUnavailable_UnknownError,
			// Token: 0x04000761 RID: 1889
			AccessDenied_DelegatedUser = 3221225501U,
			// Token: 0x04000762 RID: 1890
			AccessDenied_NoPartnerScopes = 3221225496U,
			// Token: 0x04000763 RID: 1891
			AccessDenied_OrgNotFound,
			// Token: 0x04000764 RID: 1892
			AccessDenied_OrgOutOfPartnerScope,
			// Token: 0x04000765 RID: 1893
			TaskMediumDetailWritingErrorInProcessing,
			// Token: 0x04000766 RID: 1894
			TaskMediumDetailWritingErrorNotProcessing,
			// Token: 0x04000767 RID: 1895
			AccessDenied_MultipleUsersFoundByCertificate = 3221225502U,
			// Token: 0x04000768 RID: 1896
			AccessDenied_CannotCreateSecurityIdentifier = 3221225523U,
			// Token: 0x04000769 RID: 1897
			AccessDenied_NoGroupsResolvedForDelegatedAdmin,
			// Token: 0x0400076A RID: 1898
			SnapinLoadFailed = 3221225503U,
			// Token: 0x0400076B RID: 1899
			ScriptNotFound,
			// Token: 0x0400076C RID: 1900
			ScriptCorrupted,
			// Token: 0x0400076D RID: 1901
			AccessDenied_UserPrincipalNameNotSet,
			// Token: 0x0400076E RID: 1902
			FailedToLoadValidationRules = 3221225511U,
			// Token: 0x0400076F RID: 1903
			RoleBasedStringMappingFailure = 2147483688U,
			// Token: 0x04000770 RID: 1904
			AccessDenied_FailedToMapPUIDToADAccount = 3221225513U,
			// Token: 0x04000771 RID: 1905
			ProvisioningCacheActivating = 1073741866U,
			// Token: 0x04000772 RID: 1906
			ProvisioningCacheDeactivating,
			// Token: 0x04000773 RID: 1907
			RBACUnavailable_InitialSessionStateIsNull = 3221225516U,
			// Token: 0x04000774 RID: 1908
			RuntimeException,
			// Token: 0x04000775 RID: 1909
			RBAC_TenantRedirectionFailed,
			// Token: 0x04000776 RID: 1910
			WebConfigCorrupted,
			// Token: 0x04000777 RID: 1911
			SnapinTypeLoadFailed,
			// Token: 0x04000778 RID: 1912
			RBACUnavailable_CannotOpenWebConfig,
			// Token: 0x04000779 RID: 1913
			ReachedMaxPSConnectionLimit = 3221225527U,
			// Token: 0x0400077A RID: 1914
			ReachedMaxTenantPSConnectionLimit = 3221225525U,
			// Token: 0x0400077B RID: 1915
			FailedToResolveOrganizationIdForDelegatedPrincipal,
			// Token: 0x0400077C RID: 1916
			AccessDenied_CertificateAuthenticationNotAllowed = 3221225532U,
			// Token: 0x0400077D RID: 1917
			ProvisioningCacheError,
			// Token: 0x0400077E RID: 1918
			PowershellSessionExpired = 2147483710U,
			// Token: 0x0400077F RID: 1919
			ReachedMaxUserPSConnectionLimit = 3221225542U,
			// Token: 0x04000780 RID: 1920
			ReachedMaxPSRunspaceInTimePeriodLimit,
			// Token: 0x04000781 RID: 1921
			ReachedMaxPowershellCmdletLimit,
			// Token: 0x04000782 RID: 1922
			ReachedMaxTenantPSRunspaceInTimePeriodLimit,
			// Token: 0x04000783 RID: 1923
			PSConnectionLeakDetected,
			// Token: 0x04000784 RID: 1924
			PSConnectionLeakPassivelyCorrected,
			// Token: 0x04000785 RID: 1925
			RBACUnavailable_FatalError = 3221225552U,
			// Token: 0x04000786 RID: 1926
			NullItemAddedIntoInitialSessionStateEntryCollection,
			// Token: 0x04000787 RID: 1927
			ExecuteTaskScriptLatency = 252U,
			// Token: 0x04000788 RID: 1928
			HandleADDriverTimeoutInPowershellStarted,
			// Token: 0x04000789 RID: 1929
			HandleADDriverTimeoutInPowershellFinishedSuccessfully,
			// Token: 0x0400078A RID: 1930
			HandleADDriverTimeoutInPowershellFinishedWithError = 3221225727U,
			// Token: 0x0400078B RID: 1931
			TestRpsConnectivityRecoveryWorkflowRecyclePoolSuccessfully = 256U,
			// Token: 0x0400078C RID: 1932
			FailedToInitailizeCmdletDataRedactionConfiguration = 3221225729U,
			// Token: 0x0400078D RID: 1933
			RemotePSPublicAPIFailed,
			// Token: 0x0400078E RID: 1934
			PswsPublicAPIFailed = 3221225733U,
			// Token: 0x0400078F RID: 1935
			InvalidCultureInfo,
			// Token: 0x04000790 RID: 1936
			PswsOverBudgetException
		}
	}
}
