using System;
using System.Diagnostics;
using System.Management.Automation;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000018 RID: 24
	[Cmdlet("Test", "GlobalLocatorService", SupportsShouldProcess = true)]
	public sealed class TestGlobalLocatorServiceTask : Task
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004815 File Offset: 0x00002A15
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000483B File Offset: 0x00002A3B
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004853 File Offset: 0x00002A53
		internal ITopologyConfigurationSession SystemConfigurationSession
		{
			get
			{
				if (this.systemConfigurationSession == null)
				{
					this.systemConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 190, "SystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\ActiveDirectory\\TestGlobalLocatorServiceTask.cs");
				}
				return this.systemConfigurationSession;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004888 File Offset: 0x00002A88
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000489C File Offset: 0x00002A9C
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

		// Token: 0x060000C0 RID: 192 RVA: 0x000048F4 File Offset: 0x00002AF4
		protected override void InternalBeginProcessing()
		{
			if (this.MonitoringContext)
			{
				this.monitoringData = new MonitoringData();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004910 File Offset: 0x00002B10
		protected override void InternalProcessRecord()
		{
			base.InternalBeginProcessing();
			TaskLogger.LogEnter();
			try
			{
				GlobalLocatorServiceOutcome sendToPipeline = new GlobalLocatorServiceOutcome(new ServerIdParameter().ToString());
				this.PerformGlobalLocatorServiceTest(ref sendToPipeline);
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004990 File Offset: 0x00002B90
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestGlobalLocatorServiceIdentity;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004998 File Offset: 0x00002B98
		private void PerformGlobalLocatorServiceTest(ref GlobalLocatorServiceOutcome result)
		{
			bool flag = true;
			string error = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			GlobalLocatorServiceError globalLocatorServiceError = GlobalLocatorServiceError.None;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				SmtpAddress? multiTenantAutomatedTaskUser = TestConnectivityCredentialsManager.GetMultiTenantAutomatedTaskUser(this, this.SystemConfigurationSession, this.SystemConfigurationSession.GetLocalSite(), DatacenterUserType.EDU);
				if (multiTenantAutomatedTaskUser == null)
				{
					throw new MailboxNotFoundException(new MailboxIdParameter(), null);
				}
				IGlobalLocatorServiceReader globalLocatorServiceReader = LocatorServiceClientReader.Create(GlsCallerId.Exchange);
				FindDomainResult findDomainResult = globalLocatorServiceReader.FindDomain(new SmtpDomain(multiTenantAutomatedTaskUser.Value.Domain), GlsDirectorySession.AllExoDomainProperties, GlsDirectorySession.AllExoTenantProperties);
				if (string.IsNullOrEmpty(findDomainResult.Domain))
				{
					flag = false;
					error = "Domain not found";
				}
				else
				{
					stringBuilder.AppendLine("Domain Found");
				}
			}
			catch (CommunicationException ex)
			{
				flag = false;
				error = ex.Message;
				globalLocatorServiceError = GlobalLocatorServiceError.CommunicationException;
			}
			catch (Exception ex2)
			{
				flag = false;
				error = ex2.Message;
				globalLocatorServiceError = GlobalLocatorServiceError.OtherException;
			}
			stopwatch.Stop();
			result.Update(flag ? GlobalLocatorServiceResultEnum.Success : GlobalLocatorServiceResultEnum.Failure, stopwatch.Elapsed, error, stringBuilder.ToString());
			if (this.MonitoringContext)
			{
				this.monitoringData.Events.Add(new MonitoringEvent(TestGlobalLocatorServiceTask.CmdletMonitoringEventSource, flag ? 1000 : 2000, flag ? EventTypeEnumeration.Success : EventTypeEnumeration.Error, flag ? Strings.GlobalLocatorServiceSuccess : (Strings.GlobalLocatorServiceFailed(error) + " " + globalLocatorServiceError)));
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004B0C File Offset: 0x00002D0C
		private bool IsExplicitlySet(string param)
		{
			return base.Fields.Contains(param);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004B1A File Offset: 0x00002D1A
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004B22 File Offset: 0x00002D22
		private TimeSpan TotalLatency { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004B2B File Offset: 0x00002D2B
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004B33 File Offset: 0x00002D33
		private Server ServerObject { get; set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x00004B3C File Offset: 0x00002D3C
		private void HandleException(LocalizedException e)
		{
			if (!this.MonitoringContext)
			{
				this.WriteError(e, ErrorCategory.OperationStopped, this, true);
				return;
			}
			this.monitoringData.Events.Add(new MonitoringEvent(TestGlobalLocatorServiceTask.CmdletMonitoringEventSource, 3006, EventTypeEnumeration.Error, Strings.LiveIdConnectivityExceptionThrown(e.ToString())));
		}

		// Token: 0x0400006F RID: 111
		private const string MonitoringContextParam = "MonitoringContext";

		// Token: 0x04000070 RID: 112
		private const int FailedEventIdBase = 2000;

		// Token: 0x04000071 RID: 113
		private const int SuccessEventIdBase = 1000;

		// Token: 0x04000072 RID: 114
		internal const string GlobalLocatorService = "GlobalLocatorService";

		// Token: 0x04000073 RID: 115
		private MonitoringData monitoringData;

		// Token: 0x04000074 RID: 116
		private ITopologyConfigurationSession systemConfigurationSession;

		// Token: 0x04000075 RID: 117
		public static readonly string CmdletMonitoringEventSource = "MSExchange Monitoring GlobalLocatorService";

		// Token: 0x04000076 RID: 118
		public static readonly string PerformanceCounter = "GlobalLocatorService Latency";

		// Token: 0x02000019 RID: 25
		public enum ScenarioId
		{
			// Token: 0x0400007A RID: 122
			PlaceHolderNoException = 1006,
			// Token: 0x0400007B RID: 123
			ExceptionThrown = 3006,
			// Token: 0x0400007C RID: 124
			AllTransactionsSucceeded = 3001
		}
	}
}
