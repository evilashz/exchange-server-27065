using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Pop3;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200001C RID: 28
	internal sealed class Pop3Server : ProtocolBaseServices
	{
		// Token: 0x060000BE RID: 190 RVA: 0x0000599C File Offset: 0x00003B9C
		public Pop3Server()
		{
			ProtocolBaseServices.TroubleshootingContext = new TroubleshootingContext("MSExchangePOP3");
			ProtocolBaseServices.ServerTracer = ExTraceGlobals.ServerTracer;
			ProtocolBaseServices.SessionTracer = ExTraceGlobals.SessionTracer;
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe)
			{
				ProtocolBaseServices.Logger = new ExEventLog(ExTraceGlobals.ServerTracer.Category, "MSExchangePOP3");
				return;
			}
			ProtocolBaseServices.Logger = new ExEventLog(ExTraceGlobals.ServerTracer.Category, "MSExchangePOP3BE");
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005A0D File Offset: 0x00003C0D
		public new static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (Pop3Server.faultInjectionTracer == null)
				{
					Pop3Server.faultInjectionTracer = new FaultInjectionTrace(Pop3Server.popComponentGuid, 2);
					Pop3Server.faultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(ProtocolBaseServices.FaultInjectionCallback));
				}
				return Pop3Server.faultInjectionTracer;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005A41 File Offset: 0x00003C41
		public SortOrder MessagesRetrievalSortOrder
		{
			get
			{
				return ((Pop3AdConfiguration)base.Configuration).MessageRetrievalSortOrder;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005A53 File Offset: 0x00003C53
		public override string MaxConnectionsError
		{
			get
			{
				return "-ERR BYE Connection refused";
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005A5A File Offset: 0x00003C5A
		public override string NoSslCertificateError
		{
			get
			{
				return "-ERR Connection is closed. 13";
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005A61 File Offset: 0x00003C61
		public override string TimeoutError
		{
			get
			{
				return "-ERR Connection is closed. 10";
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005A68 File Offset: 0x00003C68
		public override string ServerShutdownMessage
		{
			get
			{
				return "-ERR Connection is closed. 11";
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005A6F File Offset: 0x00003C6F
		public override ExEventLog.EventTuple ServerStartEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_Pop3StartSuccess;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005A76 File Offset: 0x00003C76
		public override ExEventLog.EventTuple ServerStopEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_Pop3StopSuccess;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005A7D File Offset: 0x00003C7D
		public override ExEventLog.EventTuple NoConfigurationFoundEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_NoConfigurationFound;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005A84 File Offset: 0x00003C84
		public override ExEventLog.EventTuple BasicOverPlainTextEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_BasicOverPlainText;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005A8B File Offset: 0x00003C8B
		public override ExEventLog.EventTuple MaxConnectionCountExceededEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_MaxConnectionCountExceeded;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005A92 File Offset: 0x00003C92
		public override ExEventLog.EventTuple MaxConnectionsFromSingleIpExceededEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_MaxConnectionsFromSingleIpExceeded;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005A99 File Offset: 0x00003C99
		public override ExEventLog.EventTuple SslConnectionNotStartedEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_SslConnectionNotStarted;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005AA0 File Offset: 0x00003CA0
		public override ExEventLog.EventTuple PortBusyEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_PortBusy;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005AA7 File Offset: 0x00003CA7
		public override ExEventLog.EventTuple SslCertificateNotFoundEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_SslCertificateNotFound;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005AAE File Offset: 0x00003CAE
		public override ExEventLog.EventTuple ProcessNotRespondingEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_ProcessNotResponding;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005AB5 File Offset: 0x00003CB5
		public override ExEventLog.EventTuple ControlAddressInUseEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_ControlAddressInUse;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005ABC File Offset: 0x00003CBC
		public override ExEventLog.EventTuple ControlAddressDeniedEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_ControlAddressDenied;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005AC3 File Offset: 0x00003CC3
		public override ExEventLog.EventTuple SpnRegisterFailureEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_SpnRegisterFailure;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005ACA File Offset: 0x00003CCA
		public override ExEventLog.EventTuple CreateMailboxLoggerFailedEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_CreateMailboxLoggerFailed;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005AD1 File Offset: 0x00003CD1
		public override ExEventLog.EventTuple NoPerfCounterTimerEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_NoPerfCounterTimer;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public override ExEventLog.EventTuple BadPasswordCodePageEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_BadPasswordCodePage;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005ADF File Offset: 0x00003CDF
		public override ExEventLog.EventTuple OnlineValueChangedEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_OnlineValueChanged;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005AE6 File Offset: 0x00003CE6
		public override ExEventLog.EventTuple LrsListErrorEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_LrsListError;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005AED File Offset: 0x00003CED
		public override ExEventLog.EventTuple LrsPartnerResolutionWarningEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_LrsPartnerResolutionWarning;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public static void Main(string[] args)
		{
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeIncreaseQuotaPrivilege",
				"SeAssignPrimaryTokenPrivilege"
			});
			if (num != 0)
			{
				Environment.Exit(num);
			}
			ExWatson.Init();
			AppDomain.CurrentDomain.UnhandledException += ProtocolBaseServices.SendWatsonForUnhandledException;
			ProtocolBaseServices.DetermineServiceRole();
			ProtocolBaseServices.ServiceName = "POP3";
			ProtocolBaseServices.ShortServiceName = "POP";
			ProtocolBaseServices.TargetProtocol = ServerComponentEnum.PopProxy;
			ThrottlingPerfCounterWrapper.Initialize(BudgetType.Pop);
			ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.POP);
			ResponseFactory.ClientAccessRulesProtocol = ClientAccessProtocol.POP3;
			using (Pop3Server pop3Server = new Pop3Server())
			{
				pop3Server.Run(args);
				ProtocolBaseServices.Exit(0);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public override PopImapAdConfiguration GetConfiguration(ITopologyConfigurationSession session)
		{
			PopImapAdConfiguration popImapAdConfiguration = PopImapAdConfiguration.FindOne<Pop3AdConfiguration>(session);
			if (popImapAdConfiguration == null)
			{
				base.ProcessGetConfigurationException(null);
			}
			return popImapAdConfiguration;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005BDF File Offset: 0x00003DDF
		public override VirtualServer NewVirtualServer(ProtocolBaseServices server, PopImapAdConfiguration configuration)
		{
			return new Pop3VirtualServer(server, configuration);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005C14 File Offset: 0x00003E14
		public override void LoadFlightingState()
		{
			ResponseFactory.CheckOnlyAuthenticationStatusEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Pop.CheckOnlyAuthenticationStatus.Enabled;
			ResponseFactory.EnforceLogsRetentionPolicyEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Pop.EnforceLogsRetentionPolicy.Enabled;
			ResponseFactory.AppendServerNameInBannerEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Pop.AppendServerNameInBanner.Enabled;
			ResponseFactory.GlobalCriminalComplianceEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Pop.GlobalCriminalCompliance.Enabled;
			ResponseFactory.IgnoreNonProvisionedServersEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Pop.IgnoreNonProvisionedServers.Enabled;
			ResponseFactory.UsePrimarySmtpAddressEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Pop.UsePrimarySmtpAddress.Enabled;
			ResponseFactory.GetClientAccessRulesEnabled = (() => VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Pop.PopClientAccessRulesEnabled.Enabled);
			ResponseFactory.LrsLoggingEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Pop.LrsLogging.Enabled;
		}

		// Token: 0x04000075 RID: 117
		internal const string MaxConnectionsErrorString = "-ERR BYE Connection refused";

		// Token: 0x04000076 RID: 118
		internal const string TimeoutErrorString = "-ERR Connection is closed. 10";

		// Token: 0x04000077 RID: 119
		internal const string ShutdownError = "-ERR Connection is closed. 11";

		// Token: 0x04000078 RID: 120
		internal const string NoSslCertificateErrorString = "-ERR Connection is closed. 13";

		// Token: 0x04000079 RID: 121
		private const int TagFaultInjection = 2;

		// Token: 0x0400007A RID: 122
		private static Guid popComponentGuid = new Guid("CE267B2B-B25F-4e73-BDDA-0C0734D8019B");

		// Token: 0x0400007B RID: 123
		private static FaultInjectionTrace faultInjectionTracer;
	}
}
