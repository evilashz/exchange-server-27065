using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.ApplicationLogic.Autodiscover;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess
{
	// Token: 0x020001F1 RID: 497
	internal static class Extensions
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0005DEB6 File Offset: 0x0005C0B6
		public static ProbeDefinition ConfigureAuthenticationForBackendProbe(this ProbeDefinition definition, MailboxDatabaseInfo dbInfo, bool useServerAuthforBackEndProbes)
		{
			if (Extensions.ShouldUseLiveIdOnBackEnd && !useServerAuthforBackEndProbes)
			{
				return definition.AuthenticateAsUser(dbInfo).TargetBackEndPort();
			}
			return definition.AuthenticateAsCafeServer(dbInfo);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0005DED8 File Offset: 0x0005C0D8
		public static ProbeDefinition AuthenticateAsCafeServer(this ProbeDefinition definition, MailboxDatabaseInfo dbInfo)
		{
			if (Extensions.useBackendAuthenticationHook != null)
			{
				return Extensions.useBackendAuthenticationHook();
			}
			definition.SetAccountCommonAccessToken(Datacenter.IsLiveIDForExchangeLogin(true) ? dbInfo.MonitoringAccountExchangeLoginName : dbInfo.MonitoringAccountWindowsLoginName);
			definition.AccountDisplayName = dbInfo.MonitoringAccountExchangeLoginName;
			definition.SecondaryEndpoint = definition.Endpoint;
			return definition;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0005DF30 File Offset: 0x0005C130
		public static ProbeDefinition SetAccountCommonAccessToken(this ProbeDefinition definition, string tokenAccount)
		{
			CommonAccessToken commonAccessToken = null;
			if (Datacenter.IsLiveIDForExchangeLogin(true))
			{
				commonAccessToken = CommonAccessTokenHelper.CreateLiveIdBasic(tokenAccount);
			}
			else
			{
				using (WindowsIdentity windowsIdentity = new WindowsIdentity(tokenAccount))
				{
					commonAccessToken = CommonAccessTokenHelper.CreateWindows(windowsIdentity);
				}
			}
			definition.Account = commonAccessToken.Serialize();
			return definition;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0005DF88 File Offset: 0x0005C188
		public static TDefinition SuppressOnFreshBootUntilServiceIsStarted<TDefinition>(this TDefinition definition, string windowsServiceName) where TDefinition : WorkDefinition
		{
			StartupNotification.SetStartupNotificationDefinition(definition, windowsServiceName, Extensions.RoundInterval(Extensions.FreshBootServiceStartGracePeriod.TotalSeconds));
			return definition;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0005DFB4 File Offset: 0x0005C1B4
		public static ProbeDefinition AuthenticateAsUser(this ProbeDefinition definition, MailboxDatabaseInfo dbInfo)
		{
			if (string.IsNullOrEmpty(dbInfo.MonitoringAccountPassword))
			{
				throw new InvalidOperationException("Cafe authentication requires a valid monitoring account password");
			}
			definition.Account = dbInfo.MonitoringAccountExchangeLoginName;
			definition.AccountPassword = dbInfo.MonitoringAccountPassword;
			definition.AccountDisplayName = dbInfo.MonitoringAccountExchangeLoginName;
			definition.SecondaryEndpoint = definition.Attributes["PersonalizedServerName"];
			return definition;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0005E014 File Offset: 0x0005C214
		public static ProbeDefinition SetSecondaryEndpointAsPersonalizedServerName(this ProbeDefinition definition, MailboxDatabaseInfo dbInfo)
		{
			definition.SecondaryEndpoint = Extensions.GetPersonalizedServerName(dbInfo);
			return definition;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0005E023 File Offset: 0x0005C223
		public static ProbeDefinition TargetPrimaryMailbox(this ProbeDefinition definition, MailboxDatabaseInfo dbInfo)
		{
			definition.Attributes["AccountLegacyDN"] = dbInfo.MonitoringAccountLegacyDN;
			definition.Attributes["PersonalizedServerName"] = Extensions.GetPersonalizedServerName(dbInfo);
			return definition;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0005E052 File Offset: 0x0005C252
		private static string GetPersonalizedServerName(MailboxDatabaseInfo dbInfo)
		{
			return DirectoryAccessor.Instance.CreatePersonalizedServerName(dbInfo.MonitoringAccountMailboxGuid, dbInfo.MonitoringAccountDomain);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0005E06C File Offset: 0x0005C26C
		public static ProbeDefinition TargetArchiveMailbox(this ProbeDefinition definition, MailboxDatabaseInfo dbInfo)
		{
			definition.Attributes["AccountLegacyDN"] = dbInfo.MonitoringAccountLegacyDN;
			definition.Attributes["PersonalizedServerName"] = DirectoryAccessor.Instance.CreatePersonalizedServerName(dbInfo.MonitoringAccountMailboxArchiveGuid, dbInfo.MonitoringAccountDomain);
			string value = DirectoryAccessor.Instance.CreateAlternateMailboxLegDN(dbInfo.MonitoringAccountLegacyDN, dbInfo.MonitoringAccountMailboxArchiveGuid);
			definition.Attributes["MailboxLegacyDN"] = value;
			return definition;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0005E0E0 File Offset: 0x0005C2E0
		public static ProbeDefinition ConfigureDeepTest(this ProbeDefinition definition, int numberOfResources)
		{
			TimeSpan timeout = TimeSpan.FromSeconds((double)(numberOfResources * Extensions.ProtocolDeepTestProbeIntervalInSeconds));
			definition.RecurrenceIntervalSeconds = Extensions.RoundInterval(timeout.TotalSeconds);
			definition.TimeoutSeconds = Extensions.RoundInterval(Extensions.AdjustTimeoutForSchedulingOverhead(timeout).TotalSeconds);
			return definition;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0005E128 File Offset: 0x0005C328
		public static ProbeDefinition ConfigureSelfTest(this ProbeDefinition definition)
		{
			TimeSpan probeRecurrenceForConsecutiveFailuresMonitor = Extensions.GetProbeRecurrenceForConsecutiveFailuresMonitor(Extensions.ProtocolSelfTestFailureDetectionTime, 5);
			definition.RecurrenceIntervalSeconds = Extensions.RoundInterval(probeRecurrenceForConsecutiveFailuresMonitor.TotalSeconds);
			definition.TimeoutSeconds = Extensions.RoundInterval(Extensions.AdjustTimeoutForSchedulingOverhead(probeRecurrenceForConsecutiveFailuresMonitor).TotalSeconds);
			return definition;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0005E170 File Offset: 0x0005C370
		public static ProbeDefinition ConfigureCtp(this ProbeDefinition definition, int numberOfResources)
		{
			TimeSpan probeRecurrenceIntervalForSmoothMonitor = Extensions.GetProbeRecurrenceIntervalForSmoothMonitor(Extensions.CtpFailureDetectionTime, numberOfResources);
			definition.RecurrenceIntervalSeconds = Extensions.RoundInterval(probeRecurrenceIntervalForSmoothMonitor.TotalSeconds);
			definition.TimeoutSeconds = Extensions.RoundInterval(Extensions.AdjustTimeoutForSchedulingOverhead(probeRecurrenceIntervalForSmoothMonitor).TotalSeconds);
			definition.Attributes["SiteName"] = DirectoryAccessor.Instance.Server.ServerSite.Name;
			return definition;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0005E1DC File Offset: 0x0005C3DC
		public static ProbeDefinition MakeTemplateForOnDemandExecution(this ProbeDefinition definition)
		{
			definition.RecurrenceIntervalSeconds = 0;
			definition.Enabled = false;
			definition.TimeoutSeconds = Extensions.RoundInterval(Extensions.OnDemandExecutionTimeout.TotalSeconds);
			return definition;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0005E210 File Offset: 0x0005C410
		public static ProbeDefinition ConfigureCtpAuthenticationMethod(this ProbeDefinition definition, AutodiscoverRpcHttpSettings settings)
		{
			RpcProxyPort rpcProxyPort = settings.SslRequired ? RpcProxyPort.Default : RpcProxyPort.LegacyHttp;
			definition.Attributes["RpcProxyPort"] = rpcProxyPort.ToString();
			definition.Attributes["RpcProxyAuthenticationType"] = settings.AuthPackageString;
			return definition;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0005E261 File Offset: 0x0005C461
		public static ProbeDefinition ForceSslCtpAuthenticationMethod(this ProbeDefinition definition)
		{
			definition.Attributes["RpcProxyPort"] = RpcProxyPort.Default.ToString();
			return definition;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0005E283 File Offset: 0x0005C483
		public static ProbeDefinition TargetBackEndPort(this ProbeDefinition definition)
		{
			definition.Attributes["RpcProxyPort"] = RpcProxyPort.Backend.ToString();
			return definition;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0005E2DC File Offset: 0x0005C4DC
		public static MonitorDefinition DelayStateTransitions(this MonitorDefinition monitorDefinition, TimeSpan transitionDelay)
		{
			monitorDefinition.MonitorStateTransitions = (from transition in monitorDefinition.MonitorStateTransitions
			select new MonitorStateTransition(transition.ToState, transition.TransitionTimeout.Add(transitionDelay))).ToArray<MonitorStateTransition>();
			return monitorDefinition;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0005E319 File Offset: 0x0005C519
		public static MonitorDefinition ConfigureMonitorStateTransitions(this MonitorDefinition monitorDefinition, params MonitorStateTransition[] monitorStateTransitions)
		{
			monitorDefinition.MonitorStateTransitions = monitorStateTransitions;
			return monitorDefinition;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0005E340 File Offset: 0x0005C540
		public static MonitorDefinition LimitRespondersTo(this MonitorDefinition monitorDefinition, params ServiceHealthStatus[] supportedStates)
		{
			monitorDefinition.MonitorStateTransitions = (from state in monitorDefinition.MonitorStateTransitions
			where supportedStates.Contains(state.ToState)
			select state).ToArray<MonitorStateTransition>();
			return monitorDefinition;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0005E380 File Offset: 0x0005C580
		private static TimeSpan GetProbeRecurrenceIntervalForSmoothMonitor(TimeSpan detectionTime, int numberOfResources)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(detectionTime.TotalSeconds / 10.0);
			return TimeSpan.FromSeconds((double)numberOfResources * timeSpan.TotalSeconds);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0005E3B3 File Offset: 0x0005C5B3
		private static TimeSpan GetProbeRecurrenceForConsecutiveFailuresMonitor(TimeSpan detectionTime, int failureCount)
		{
			return TimeSpan.FromSeconds(detectionTime.TotalSeconds / (double)failureCount);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0005E3C4 File Offset: 0x0005C5C4
		private static TimeSpan AdjustTimeoutForSchedulingOverhead(TimeSpan timeout)
		{
			if (timeout < Extensions.MaxAnticipatedSchedulingOverhead)
			{
				throw new ArgumentOutOfRangeException("timeout", "Must be greater than the anticipated scheduling overhead");
			}
			timeout -= Extensions.MaxAnticipatedSchedulingOverhead;
			if (!(timeout > Extensions.MaxTimeOut))
			{
				return timeout;
			}
			return Extensions.MaxTimeOut;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0005E404 File Offset: 0x0005C604
		private static int RoundInterval(double interval)
		{
			return Math.Max(1, (int)interval);
		}

		// Token: 0x04000A61 RID: 2657
		public const int NumberOfChunksInASmoothMonitor = 5;

		// Token: 0x04000A62 RID: 2658
		public const int NumberOfProbesPerChunkInASmoothMonitor = 2;

		// Token: 0x04000A63 RID: 2659
		public static readonly TimeSpan ProtocolSelfTestFailureDetectionTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000A64 RID: 2660
		public static readonly TimeSpan ProtocolDeepTestFailureDetectionTime = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000A65 RID: 2661
		public static readonly TimeSpan CtpFailureDetectionTime = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000A66 RID: 2662
		public static readonly TimeSpan OnDemandExecutionTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000A67 RID: 2663
		public static readonly TimeSpan FreshBootServiceStartGracePeriod = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000A68 RID: 2664
		public static readonly TimeSpan MaxAnticipatedSchedulingOverhead = TimeSpan.FromSeconds(2.0);

		// Token: 0x04000A69 RID: 2665
		public static readonly int ProtocolDeepTestProbeIntervalInSeconds = 60;

		// Token: 0x04000A6A RID: 2666
		public static double ProtocolDeepTestAvailabilityThreshold = 25.0;

		// Token: 0x04000A6B RID: 2667
		public static TimeSpan ProtocolDeepTestMonitorRecurrenceInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000A6C RID: 2668
		public static readonly string MomtComponentName = "RPCClientAccess";

		// Token: 0x04000A6D RID: 2669
		public static readonly bool IsDatacenter = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled;

		// Token: 0x04000A6E RID: 2670
		public static readonly bool ShouldUseLiveIdOnBackEnd = Extensions.IsDatacenter;

		// Token: 0x04000A6F RID: 2671
		private static Func<ProbeDefinition> useBackendAuthenticationHook = null;

		// Token: 0x04000A70 RID: 2672
		private static TimeSpan MaxTimeOut = TimeSpan.FromMinutes(3.0);
	}
}
