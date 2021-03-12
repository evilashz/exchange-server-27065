using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring.MailFlowTestHelper;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005D2 RID: 1490
	[Cmdlet("Test", "Mailflow", SupportsShouldProcess = true, DefaultParameterSetName = "SourceServer")]
	public sealed class TestMailFlow : RecipientObjectActionTask<ServerIdParameter, Server>
	{
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000D4425 File Offset: 0x000D2625
		// (set) Token: 0x0600346B RID: 13419 RVA: 0x000D443C File Offset: 0x000D263C
		[Alias(new string[]
		{
			"SourceMailboxServer"
		})]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = "TargetDatabase")]
		[Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = "AutoDiscoverTargetMailboxServer")]
		[Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = "TargetMailboxServer")]
		[Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = "SourceServer")]
		[Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = "TargetEmailAddress")]
		public override ServerIdParameter Identity
		{
			get
			{
				return (ServerIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000D444F File Offset: 0x000D264F
		// (set) Token: 0x0600346D RID: 13421 RVA: 0x000D4474 File Offset: 0x000D2674
		[ValidateRange(1, 2147483647)]
		[Parameter(Mandatory = false)]
		public int ExecutionTimeout
		{
			get
			{
				return (int)(base.Fields["ExecutionTimeout"] ?? 240);
			}
			set
			{
				base.Fields["ExecutionTimeout"] = value;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000D448C File Offset: 0x000D268C
		// (set) Token: 0x0600346F RID: 13423 RVA: 0x000D44AE File Offset: 0x000D26AE
		[ValidateRange(1, 2147483647)]
		[Parameter(Mandatory = false)]
		public int ActiveDirectoryTimeout
		{
			get
			{
				return (int)(base.Fields["ActiveDirectoryTimeout"] ?? 15);
			}
			set
			{
				base.Fields["ActiveDirectoryTimeout"] = value;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000D44C6 File Offset: 0x000D26C6
		// (set) Token: 0x06003471 RID: 13425 RVA: 0x000D44E7 File Offset: 0x000D26E7
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

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06003472 RID: 13426 RVA: 0x000D44FF File Offset: 0x000D26FF
		// (set) Token: 0x06003473 RID: 13427 RVA: 0x000D4516 File Offset: 0x000D2716
		[Parameter(Mandatory = true, ParameterSetName = "TargetMailboxServer", ValueFromPipeline = false)]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter TargetMailboxServer
		{
			get
			{
				return (ServerIdParameter)base.Fields["TargetMailboxServer"];
			}
			set
			{
				base.Fields["TargetMailboxServer"] = value;
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x000D4529 File Offset: 0x000D2729
		// (set) Token: 0x06003475 RID: 13429 RVA: 0x000D4540 File Offset: 0x000D2740
		[Parameter(Mandatory = true, ParameterSetName = "TargetDatabase", ValueFromPipeline = false)]
		public DatabaseIdParameter TargetDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["TargetDatabase"];
			}
			set
			{
				base.Fields["TargetDatabase"] = value;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000D4553 File Offset: 0x000D2753
		// (set) Token: 0x06003477 RID: 13431 RVA: 0x000D456A File Offset: 0x000D276A
		[Parameter(Mandatory = true, ParameterSetName = "TargetEmailAddress", ValueFromPipeline = false)]
		public string TargetEmailAddress
		{
			get
			{
				return (string)base.Fields["TargetEmailAddress"];
			}
			set
			{
				base.Fields["TargetEmailAddress"] = value;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x000D457D File Offset: 0x000D277D
		// (set) Token: 0x06003479 RID: 13433 RVA: 0x000D4594 File Offset: 0x000D2794
		[Parameter(Mandatory = false, ParameterSetName = "TargetEmailAddress")]
		public string TargetEmailAddressDisplayName
		{
			get
			{
				return (string)base.Fields["TargetEmailAddressDisplayName"];
			}
			set
			{
				base.Fields["TargetEmailAddressDisplayName"] = value;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000D45A7 File Offset: 0x000D27A7
		// (set) Token: 0x0600347B RID: 13435 RVA: 0x000D45CD File Offset: 0x000D27CD
		[Parameter(Mandatory = true, ParameterSetName = "AutoDiscoverTargetMailboxServer")]
		public SwitchParameter AutoDiscoverTargetMailboxServer
		{
			get
			{
				return (SwitchParameter)(base.Fields["AutoDiscoverTargetMailboxServer"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["AutoDiscoverTargetMailboxServer"] = value;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x000D45E5 File Offset: 0x000D27E5
		// (set) Token: 0x0600347D RID: 13437 RVA: 0x000D460A File Offset: 0x000D280A
		[ValidateRange(1, 2147483647)]
		[Parameter(Mandatory = false)]
		public int ErrorLatency
		{
			get
			{
				return (int)(base.Fields["ErrorLatency"] ?? 180);
			}
			set
			{
				base.Fields["ErrorLatency"] = value;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000D4622 File Offset: 0x000D2822
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000D4643 File Offset: 0x000D2843
		[Parameter(Mandatory = true, ParameterSetName = "CrossPremises", ValueFromPipeline = false)]
		public bool CrossPremises
		{
			get
			{
				return (bool)(base.Fields["CrossPremises"] ?? false);
			}
			set
			{
				base.Fields["CrossPremises"] = value;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000D465B File Offset: 0x000D285B
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000D467C File Offset: 0x000D287C
		[ValidateRange(1, 2147483647)]
		[Parameter(Mandatory = false, ParameterSetName = "CrossPremises", ValueFromPipeline = false)]
		public int CrossPremisesPendingErrorCount
		{
			get
			{
				return (int)(base.Fields["CrossPremisesPendingErrorCount"] ?? 3);
			}
			set
			{
				base.Fields["CrossPremisesPendingErrorCount"] = value;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000D4694 File Offset: 0x000D2894
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x000D46B9 File Offset: 0x000D28B9
		[Parameter(Mandatory = false, ParameterSetName = "CrossPremises", ValueFromPipeline = false)]
		public EnhancedTimeSpan CrossPremisesExpirationTimeout
		{
			get
			{
				return (EnhancedTimeSpan)(base.Fields["CrossPremisesExpirationTimeout"] ?? TestMailFlow.defaultCrossPremisesExpirationTimeout);
			}
			set
			{
				base.Fields["CrossPremisesExpirationTimeout"] = value;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000D46D1 File Offset: 0x000D28D1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestMailflow;
			}
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000D46D8 File Offset: 0x000D28D8
		internal IConfigurable GetAdDataObject<T>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootId, LocalizedString? notFoundError, LocalizedString? multipleFoundError) where T : IConfigurable, new()
		{
			return base.GetDataObject<T>(id, session, rootId, notFoundError, multipleFoundError);
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000D46E7 File Offset: 0x000D28E7
		internal IEnumerable<T> GetAdDataObjects<T>(IIdentityParameter id, IConfigDataProvider session) where T : IConfigurable, new()
		{
			return base.GetDataObjects<T>(id, session, null);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000D46F2 File Offset: 0x000D28F2
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || DataAccessHelper.IsDataAccessKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000D4710 File Offset: 0x000D2910
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.CrossPremises)
				{
					this.helper = new CrossPremiseTestMailFlowHelper(this);
				}
				else
				{
					this.helper = new LegacyTestMailFlowHelper();
				}
				this.helper.SetTask(this);
				this.helper.InternalValidate();
			}
			finally
			{
				if (base.HasErrors && this.MonitoringContext)
				{
					this.helper.OutputMonitoringData();
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000D4790 File Offset: 0x000D2990
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				this.helper.InternalProcessRecord();
			}
			catch (MapiPermanentException ex)
			{
				this.helper.DiagnoseAndReportMapiException(ex);
			}
			catch (MapiRetryableException ex2)
			{
				this.helper.DiagnoseAndReportMapiException(ex2);
			}
			finally
			{
				if (this.MonitoringContext)
				{
					this.helper.OutputMonitoringData();
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000D4810 File Offset: 0x000D2A10
		protected override void InternalStateReset()
		{
			if (this.helper != null)
			{
				this.helper.InternalStateReset();
			}
		}

		// Token: 0x0400243F RID: 9279
		internal const int DefaultErrorLatencySeconds = 180;

		// Token: 0x04002440 RID: 9280
		private const int DefaultExecutionTimeoutSeconds = 240;

		// Token: 0x04002441 RID: 9281
		private const int DefaultADOperationsTimeoutInSeconds = 15;

		// Token: 0x04002442 RID: 9282
		private const int DefaultCrossPremisesPendingErrorCount = 3;

		// Token: 0x04002443 RID: 9283
		private static EnhancedTimeSpan defaultCrossPremisesExpirationTimeout = EnhancedTimeSpan.FromHours(48.0);

		// Token: 0x04002444 RID: 9284
		private TestMailFlowHelper helper;
	}
}
