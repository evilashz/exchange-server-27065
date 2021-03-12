using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005D6 RID: 1494
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Test", "MRSHealth", SupportsShouldProcess = true)]
	public sealed class TestMRSHealth : SystemConfigurationObjectActionTask<ServerIdParameter, Server>
	{
		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000D8950 File Offset: 0x000D6B50
		// (set) Token: 0x060034DE RID: 13534 RVA: 0x000D8958 File Offset: 0x000D6B58
		public MonitoringData MonitoringData { get; private set; }

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000D8961 File Offset: 0x000D6B61
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x000D8969 File Offset: 0x000D6B69
		[Alias(new string[]
		{
			"Server"
		})]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override ServerIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000D8972 File Offset: 0x000D6B72
		// (set) Token: 0x060034E2 RID: 13538 RVA: 0x000D8993 File Offset: 0x000D6B93
		[Parameter(Mandatory = false)]
		public bool MonitoringContext
		{
			get
			{
				return (bool)(base.Fields["MonitoringContext"] ?? false);
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000D89AB File Offset: 0x000D6BAB
		// (set) Token: 0x060034E4 RID: 13540 RVA: 0x000D89D0 File Offset: 0x000D6BD0
		[Parameter(Mandatory = false)]
		[ValidateRange(1, 2147483647)]
		public int MaxQueueScanAgeSeconds
		{
			get
			{
				return (int)(base.Fields["MaxQueueScanAgeSeconds"] ?? 3600);
			}
			set
			{
				base.Fields["MaxQueueScanAgeSeconds"] = value;
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000D89E8 File Offset: 0x000D6BE8
		// (set) Token: 0x060034E6 RID: 13542 RVA: 0x000D89FF File Offset: 0x000D6BFF
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public Fqdn MRSProxyServer
		{
			get
			{
				return (Fqdn)base.Fields["MRSProxyServer"];
			}
			set
			{
				base.Fields["MRSProxyServer"] = value;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000D8A12 File Offset: 0x000D6C12
		// (set) Token: 0x060034E8 RID: 13544 RVA: 0x000D8A29 File Offset: 0x000D6C29
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public PSCredential MRSProxyCredentials
		{
			get
			{
				return (PSCredential)base.Fields["MRSProxyCredentials"];
			}
			set
			{
				base.Fields["MRSProxyCredentials"] = value;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000D8A3C File Offset: 0x000D6C3C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestMRSHealth(this.Identity.ToString());
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000D8A4E File Offset: 0x000D6C4E
		public void WriteErrorAndMonitoringEvent(Exception exception, ErrorCategory errorCategory, int eventId)
		{
			this.MonitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring MRSHealth", eventId, EventTypeEnumeration.Error, CommonUtils.FullExceptionMessage(exception, true)));
			base.WriteError(exception, errorCategory, this.Identity);
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000D8A86 File Offset: 0x000D6C86
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || MonitoringHelper.IsKnownExceptionForMonitoring(exception);
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000D8AA1 File Offset: 0x000D6CA1
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.MonitoringData = new MonitoringData();
			this.serverName = null;
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000D8ABC File Offset: 0x000D6CBC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.Identity == null)
				{
					this.Identity = ServerIdParameter.Parse(Environment.MachineName);
				}
				base.InternalValidate();
				this.serverName = this.DataObject.Name;
				if (!this.DataObject.IsClientAccessServer)
				{
					this.WriteErrorAndMonitoringEvent(this.DataObject.GetServerRoleError(ServerRole.ClientAccess), ErrorCategory.InvalidOperation, 1001);
				}
				if (!this.DataObject.IsE14OrLater)
				{
					this.WriteErrorAndMonitoringEvent(new ServerConfigurationException(this.serverName, Strings.ErrorServerNotE14OrLater(this.serverName)), ErrorCategory.InvalidOperation, 1001);
				}
			}
			finally
			{
				if (base.HasErrors && this.MonitoringContext)
				{
					base.WriteObject(this.MonitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000D8B8C File Offset: 0x000D6D8C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				MRSHealthCheckOutcome mrshealthCheckOutcome = MRSHealth.VerifyServiceIsUp(this.serverName, this.DataObject.Fqdn, this);
				base.WriteObject(mrshealthCheckOutcome);
				bool flag = mrshealthCheckOutcome.Passed;
				if (flag)
				{
					mrshealthCheckOutcome = MRSHealth.VerifyServiceIsRespondingToRPCPing(this.serverName, this);
					base.WriteObject(mrshealthCheckOutcome);
					if (mrshealthCheckOutcome.Passed)
					{
						try
						{
							mrshealthCheckOutcome = MRSHealth.VerifyMRSProxyIsRespondingToWCFPing(this.serverName, this.MRSProxyServer, (this.MRSProxyCredentials == null) ? null : this.MRSProxyCredentials.GetNetworkCredential(), this);
						}
						catch (UnsupportedRemoteServerVersionWithOperationPermanentException)
						{
							mrshealthCheckOutcome = new MRSHealthCheckOutcome(this.serverName, MRSHealthCheckId.MRSProxyPingCheck, true, Strings.MRSProxyPingSkipped(this.serverName));
						}
						base.WriteObject(mrshealthCheckOutcome);
					}
					flag = mrshealthCheckOutcome.Passed;
					mrshealthCheckOutcome = MRSHealth.VerifyServiceIsScanningForJobs(this.serverName, (long)this.MaxQueueScanAgeSeconds, this);
					base.WriteObject(mrshealthCheckOutcome);
					flag &= mrshealthCheckOutcome.Passed;
				}
				if (flag)
				{
					this.MonitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring MRSHealth", 1000, EventTypeEnumeration.Success, Strings.MRSHealthPassed));
				}
			}
			catch (ConfigurationSettingsException exception)
			{
				this.WriteErrorAndMonitoringEvent(exception, ErrorCategory.InvalidOperation, 1001);
			}
			finally
			{
				if (this.MonitoringContext)
				{
					base.WriteObject(this.MonitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x04002470 RID: 9328
		public const string CmdletMonitoringEventSource = "MSExchange Monitoring MRSHealth";

		// Token: 0x04002471 RID: 9329
		public const string MonitoringScanElapsedTimePerfCounter = "Last Scan Age (secs)";

		// Token: 0x04002472 RID: 9330
		private const string CmdletNoun = "MRSHealth";

		// Token: 0x04002473 RID: 9331
		private string serverName;

		// Token: 0x020005D7 RID: 1495
		public static class EventId
		{
			// Token: 0x04002475 RID: 9333
			internal const int Success = 1000;

			// Token: 0x04002476 RID: 9334
			internal const int ServiceOperationError = 1001;

			// Token: 0x04002477 RID: 9335
			internal const int ServiceBadResponse = 1002;

			// Token: 0x04002478 RID: 9336
			internal const int ServiceNotScanningForJobs = 1003;

			// Token: 0x04002479 RID: 9337
			internal const int MRSProxyBadResponse = 1004;
		}
	}
}
