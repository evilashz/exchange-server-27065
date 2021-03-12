using System;
using System.Configuration;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Imap4;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200000B RID: 11
	internal sealed class Imap4Server : ProtocolBaseServices
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000034EC File Offset: 0x000016EC
		public Imap4Server()
		{
			ProtocolBaseServices.TroubleshootingContext = new TroubleshootingContext("MSExchangeIMAP4");
			ProtocolBaseServices.ServerTracer = ExTraceGlobals.ServerTracer;
			ProtocolBaseServices.SessionTracer = ExTraceGlobals.SessionTracer;
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe)
			{
				ProtocolBaseServices.Logger = new ExEventLog(ExTraceGlobals.ServerTracer.Category, "MSExchangeIMAP4");
				return;
			}
			ProtocolBaseServices.Logger = new ExEventLog(ExTraceGlobals.ServerTracer.Category, "MSExchangeIMAP4BE");
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000355D File Offset: 0x0000175D
		public new static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (Imap4Server.faultInjectionTracer == null)
				{
					Imap4Server.faultInjectionTracer = new FaultInjectionTrace(Imap4Server.imapComponentGuid, 2);
					Imap4Server.faultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(ProtocolBaseServices.FaultInjectionCallback));
				}
				return Imap4Server.faultInjectionTracer;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003591 File Offset: 0x00001791
		public bool ShowHiddenFolders
		{
			get
			{
				return ((Imap4AdConfiguration)base.Configuration).ShowHiddenFoldersEnabled;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000035A3 File Offset: 0x000017A3
		public int MaxReceiveSize
		{
			get
			{
				return this.maxReceiveSize;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000035AB File Offset: 0x000017AB
		public override string MaxConnectionsError
		{
			get
			{
				return "* BYE Connection is closed. 10";
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000035B2 File Offset: 0x000017B2
		public override string NoSslCertificateError
		{
			get
			{
				return "* BYE Connection is closed. 14";
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000035B9 File Offset: 0x000017B9
		public override string TimeoutError
		{
			get
			{
				return "* BYE Connection is closed. 11";
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000035C0 File Offset: 0x000017C0
		public override string ServerShutdownMessage
		{
			get
			{
				return "* BYE Connection is closed. 12";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000035C7 File Offset: 0x000017C7
		public override ExEventLog.EventTuple ServerStartEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_Imap4StartSuccess;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000035CE File Offset: 0x000017CE
		public override ExEventLog.EventTuple ServerStopEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_Imap4StopSuccess;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000035D5 File Offset: 0x000017D5
		public override ExEventLog.EventTuple BasicOverPlainTextEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_BasicOverPlainText;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000035DC File Offset: 0x000017DC
		public override ExEventLog.EventTuple MaxConnectionCountExceededEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_MaxConnectionCountExceeded;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000035E3 File Offset: 0x000017E3
		public override ExEventLog.EventTuple MaxConnectionsFromSingleIpExceededEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_MaxConnectionsFromSingleIpExceeded;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000035EA File Offset: 0x000017EA
		public override ExEventLog.EventTuple SslConnectionNotStartedEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_SslConnectionNotStarted;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000035F1 File Offset: 0x000017F1
		public override ExEventLog.EventTuple NoConfigurationFoundEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_NoConfigurationFound;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000035F8 File Offset: 0x000017F8
		public override ExEventLog.EventTuple PortBusyEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_PortBusy;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000035FF File Offset: 0x000017FF
		public override ExEventLog.EventTuple SslCertificateNotFoundEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_SslCertificateNotFound;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003606 File Offset: 0x00001806
		public override ExEventLog.EventTuple ProcessNotRespondingEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_ProcessNotResponding;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000360D File Offset: 0x0000180D
		public override ExEventLog.EventTuple ControlAddressInUseEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_ControlAddressInUse;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003614 File Offset: 0x00001814
		public override ExEventLog.EventTuple ControlAddressDeniedEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_ControlAddressDenied;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000361B File Offset: 0x0000181B
		public override ExEventLog.EventTuple SpnRegisterFailureEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_SpnRegisterFailure;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003622 File Offset: 0x00001822
		public override ExEventLog.EventTuple CreateMailboxLoggerFailedEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_CreateMailboxLoggerFailed;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003629 File Offset: 0x00001829
		public override ExEventLog.EventTuple BadPasswordCodePageEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_BadPasswordCodePage;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003630 File Offset: 0x00001830
		public override ExEventLog.EventTuple OnlineValueChangedEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_OnlineValueChanged;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003637 File Offset: 0x00001837
		public bool SupportUidPlus
		{
			get
			{
				return this.supportUidPlus;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000363F File Offset: 0x0000183F
		public override ExEventLog.EventTuple NoPerfCounterTimerEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_NoPerfCounterTimer;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003646 File Offset: 0x00001846
		public override ExEventLog.EventTuple LrsListErrorEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_LrsListError;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000364D File Offset: 0x0000184D
		public override ExEventLog.EventTuple LrsPartnerResolutionWarningEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_LrsPartnerResolutionWarning;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003654 File Offset: 0x00001854
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
			ProtocolBaseServices.ServiceName = "IMAP4";
			ProtocolBaseServices.ShortServiceName = "IMAP";
			ProtocolBaseServices.TargetProtocol = ServerComponentEnum.ImapProxy;
			ThrottlingPerfCounterWrapper.Initialize(BudgetType.Imap);
			ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.IMAP);
			ResponseFactory.ClientAccessRulesProtocol = ClientAccessProtocol.IMAP4;
			using (Imap4Server imap4Server = new Imap4Server())
			{
				imap4Server.Run(args);
				ProtocolBaseServices.Exit(0);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003764 File Offset: 0x00001964
		public override PopImapAdConfiguration GetConfiguration(ITopologyConfigurationSession session)
		{
			Imap4ResponseFactory.MoveEnableAllowed = true;
			if (!ProtocolBaseServices.IsMultiTenancyEnabled)
			{
				ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Imap4\\Imap4Server.cs", "GetConfiguration", 432);
				string value = ConfigurationManager.AppSettings["SupportUidPlus"];
				Predicate<Imap4Service> serviceFilter;
				Imap4Service imap4Service;
				if (!string.IsNullOrEmpty(value))
				{
					if (!bool.TryParse(value, out this.supportUidPlus))
					{
						this.supportUidPlus = false;
					}
				}
				else
				{
					serviceFilter = ((Imap4Service service) => service.ServerVersionNumber < Server.E14SP1MinVersion && service.ServerVersionNumber >= Server.E2007MinVersion);
					imap4Service = currentServiceTopology.FindAny<Imap4Service>(ClientAccessType.Internal, serviceFilter, "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Imap4\\Imap4Server.cs", "GetConfiguration", 450);
					this.supportUidPlus = (imap4Service == null);
				}
				serviceFilter = ((Imap4Service service) => service.ServerVersionNumber < Imap4Server.MoveMinVersion && service.ServerVersionNumber >= Server.E2007MinVersion);
				imap4Service = currentServiceTopology.FindAny<Imap4Service>(ClientAccessType.Internal, serviceFilter, "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Imap4\\Imap4Server.cs", "GetConfiguration", 456);
				Imap4ResponseFactory.MoveEnableAllowed = (imap4Service == null);
			}
			else
			{
				this.supportUidPlus = true;
			}
			string distinguishedName = "CN=Transport Settings," + session.GetOrgContainerId().DistinguishedName;
			ADObjectId rootId = new ADObjectId(distinguishedName);
			TransportConfigContainer[] array = session.Find<TransportConfigContainer>(rootId, QueryScope.Base, null, null, 1);
			if (array != null && array.Length > 0 && !array[0].MaxReceiveSize.IsUnlimited)
			{
				this.maxReceiveSize = (int)array[0].MaxReceiveSize.Value.ToBytes();
			}
			else
			{
				this.maxReceiveSize = 2147483645;
			}
			PopImapAdConfiguration popImapAdConfiguration = PopImapAdConfiguration.FindOne<Imap4AdConfiguration>(session);
			if (popImapAdConfiguration == null)
			{
				base.ProcessGetConfigurationException(null);
			}
			return popImapAdConfiguration;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000038DE File Offset: 0x00001ADE
		public override VirtualServer NewVirtualServer(ProtocolBaseServices server, PopImapAdConfiguration configuration)
		{
			return new Imap4VirtualServer(server, configuration);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003914 File Offset: 0x00001B14
		public override void LoadFlightingState()
		{
			ResponseFactory.CheckOnlyAuthenticationStatusEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Imap.CheckOnlyAuthenticationStatus.Enabled;
			ResponseFactory.EnforceLogsRetentionPolicyEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Imap.EnforceLogsRetentionPolicy.Enabled;
			ResponseFactory.AppendServerNameInBannerEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Imap.AppendServerNameInBanner.Enabled;
			ResponseFactory.GlobalCriminalComplianceEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Imap.GlobalCriminalCompliance.Enabled;
			ResponseFactory.IgnoreNonProvisionedServersEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Imap.IgnoreNonProvisionedServers.Enabled;
			ResponseFactory.UsePrimarySmtpAddressEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Imap.UsePrimarySmtpAddress.Enabled;
			ResponseFactory.GetClientAccessRulesEnabled = (() => VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.ImapClientAccessRulesEnabled.Enabled);
			ResponseFactory.LrsLoggingEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Imap.LrsLogging.Enabled;
			ResponseFactory.KerberosAuthEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Imap.AllowKerberosAuth.Enabled;
		}

		// Token: 0x0400001B RID: 27
		private const string MaxConnectionsErrorString = "* BYE Connection is closed. 10";

		// Token: 0x0400001C RID: 28
		private const string TimeoutErrorString = "* BYE Connection is closed. 11";

		// Token: 0x0400001D RID: 29
		private const string ShutdownErrorString = "* BYE Connection is closed. 12";

		// Token: 0x0400001E RID: 30
		private const string NoSslCertificateErrorString = "* BYE Connection is closed. 14";

		// Token: 0x0400001F RID: 31
		private const int TagFaultInjection = 2;

		// Token: 0x04000020 RID: 32
		private static readonly int MoveMinVersion = new ServerVersion(15, 0, 954, 0).ToInt();

		// Token: 0x04000021 RID: 33
		private static Guid imapComponentGuid = new Guid("B338D7C6-58F5-4523-B459-E387B7C956BA");

		// Token: 0x04000022 RID: 34
		private static FaultInjectionTrace faultInjectionTracer;

		// Token: 0x04000023 RID: 35
		private int maxReceiveSize;

		// Token: 0x04000024 RID: 36
		private bool supportUidPlus;
	}
}
