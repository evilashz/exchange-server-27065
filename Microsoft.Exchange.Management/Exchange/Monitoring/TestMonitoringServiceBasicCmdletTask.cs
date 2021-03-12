using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200057A RID: 1402
	[Cmdlet("Test", "MonitoringServiceBasicCmdlet")]
	public sealed class TestMonitoringServiceBasicCmdletTask : Task
	{
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000C8E3B File Offset: 0x000C703B
		// (set) Token: 0x0600315E RID: 12638 RVA: 0x000C8E61 File Offset: 0x000C7061
		[Parameter(Mandatory = false)]
		public SwitchParameter MonitoringContext
		{
			get
			{
				return (SwitchParameter)(base.Fields["MonitoringContext"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000C8E79 File Offset: 0x000C7079
		// (set) Token: 0x06003160 RID: 12640 RVA: 0x000C8E99 File Offset: 0x000C7099
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public ServerIdParameter Server
		{
			get
			{
				return ((ServerIdParameter)base.Fields["Server"]) ?? new ServerIdParameter();
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000C8EAC File Offset: 0x000C70AC
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x000C8EB4 File Offset: 0x000C70B4
		public PSCredential MailboxCredential { get; set; }

		// Token: 0x06003163 RID: 12643 RVA: 0x000C8EBD File Offset: 0x000C70BD
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000C8ED0 File Offset: 0x000C70D0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (base.HasErrors)
				{
				}
			}
			catch (LocalizedException exception)
			{
				this.WriteError(exception, ErrorCategory.OperationStopped, this, true);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000C8F28 File Offset: 0x000C7128
		protected override void InternalBeginProcessing()
		{
			if (this.MonitoringContext)
			{
				this.monitoringData = new MonitoringData();
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000C8F44 File Offset: 0x000C7144
		protected override void InternalProcessRecord()
		{
			base.InternalBeginProcessing();
			TaskLogger.LogEnter();
			try
			{
				MonitoringServiceBasicCmdletOutcome sendToPipeline = new MonitoringServiceBasicCmdletOutcome(this.Server.ToString());
				this.PerformMonitoringServiceBasicCmdletTest(ref sendToPipeline);
				base.WriteObject(sendToPipeline);
			}
			catch (LocalizedException e)
			{
				this.HandleException(e);
			}
			finally
			{
				if (this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000C8FC8 File Offset: 0x000C71C8
		private void PerformMonitoringServiceBasicCmdletTest(ref MonitoringServiceBasicCmdletOutcome result)
		{
			SmtpAddress? multiTenantAutomatedTaskUser = TestConnectivityCredentialsManager.GetMultiTenantAutomatedTaskUser(this, null, null, DatacenterUserType.EDU);
			if (multiTenantAutomatedTaskUser == null)
			{
				throw new MailboxNotFoundException(new MailboxIdParameter(), null);
			}
			result.Update(MonitoringServiceBasicCmdletResultEnum.Success, null);
			if (this.MonitoringContext)
			{
				this.monitoringData.Events.Add(new MonitoringEvent(TestMonitoringServiceBasicCmdletTask.CmdletMonitoringEventSource, 1000, EventTypeEnumeration.Success, string.Format("TestMonitoringServiceBasicCmdlet succeeded. Test user returned: [{0}]", multiTenantAutomatedTaskUser.ToString())));
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000C9041 File Offset: 0x000C7241
		// (set) Token: 0x06003169 RID: 12649 RVA: 0x000C9049 File Offset: 0x000C7249
		private Server ServerObject { get; set; }

		// Token: 0x0600316A RID: 12650 RVA: 0x000C9054 File Offset: 0x000C7254
		private void HandleException(LocalizedException e)
		{
			if (!this.MonitoringContext)
			{
				this.WriteError(e, ErrorCategory.OperationStopped, this, true);
				return;
			}
			this.monitoringData.Events.Add(new MonitoringEvent(TestMonitoringServiceBasicCmdletTask.CmdletMonitoringEventSource, 3006, EventTypeEnumeration.Error, e.ToString()));
		}

		// Token: 0x040022F3 RID: 8947
		private const string ServerParam = "Server";

		// Token: 0x040022F4 RID: 8948
		private const string MonitoringContextParam = "MonitoringContext";

		// Token: 0x040022F5 RID: 8949
		private const int FailedEventIdBase = 2000;

		// Token: 0x040022F6 RID: 8950
		private const int SuccessEventIdBase = 1000;

		// Token: 0x040022F7 RID: 8951
		internal const string MonitoringServiceBasicCmdlet = "MonitoringServiceBasicCmdlet";

		// Token: 0x040022F8 RID: 8952
		private MonitoringData monitoringData;

		// Token: 0x040022F9 RID: 8953
		public static readonly string CmdletMonitoringEventSource = "MSExchange Monitoring MonitoringServiceBasicCmdlet";

		// Token: 0x040022FA RID: 8954
		public static readonly string PerformanceCounter = "MonitoringServiceBasicCmdlet Latency";

		// Token: 0x0200057B RID: 1403
		public enum ScenarioId
		{
			// Token: 0x040022FE RID: 8958
			PlaceHolderNoException = 1006,
			// Token: 0x040022FF RID: 8959
			ExceptionThrown = 3006,
			// Token: 0x04002300 RID: 8960
			AllTransactionsSucceeded = 3001
		}
	}
}
