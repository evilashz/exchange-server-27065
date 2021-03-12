using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000422 RID: 1058
	[Cmdlet("Test", "AssistantHealth", DefaultParameterSetName = "AssistantHealthParameterSetName", SupportsShouldProcess = true)]
	public sealed class TestAssistantHealth : Task
	{
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x00092A0C File Offset: 0x00090C0C
		// (set) Token: 0x060024BF RID: 9407 RVA: 0x00092A31 File Offset: 0x00090C31
		[Parameter(ParameterSetName = "AssistantHealthParameterSetName", Position = 0, ValueFromPipeline = true)]
		public ServerIdParameter ServerName
		{
			get
			{
				return (ServerIdParameter)(base.Fields["ServerName"] ?? ServerIdParameter.Parse(Environment.MachineName));
			}
			set
			{
				base.Fields["ServerName"] = value;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x00092A44 File Offset: 0x00090C44
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x00092A6A File Offset: 0x00090C6A
		[Parameter(Mandatory = false, ParameterSetName = "AssistantHealthParameterSetName")]
		public SwitchParameter IncludeCrashDump
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeCrashDump"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeCrashDump"] = value;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x00092A82 File Offset: 0x00090C82
		// (set) Token: 0x060024C3 RID: 9411 RVA: 0x00092AA8 File Offset: 0x00090CA8
		[Parameter(Mandatory = false, ParameterSetName = "AssistantHealthParameterSetName")]
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

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x00092AC0 File Offset: 0x00090CC0
		// (set) Token: 0x060024C5 RID: 9413 RVA: 0x00092AE6 File Offset: 0x00090CE6
		[Parameter(Mandatory = false, ParameterSetName = "AssistantHealthParameterSetName")]
		public SwitchParameter ResolveProblems
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResolveProblems"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ResolveProblems"] = value;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x00092AFE File Offset: 0x00090CFE
		// (set) Token: 0x060024C7 RID: 9415 RVA: 0x00092B15 File Offset: 0x00090D15
		[Parameter(Mandatory = false, ParameterSetName = "AssistantHealthParameterSetName")]
		[ValidateRange(1, 3600)]
		public uint MaxProcessingTimeInMinutes
		{
			get
			{
				return (uint)base.Fields["MaxProcessingTimeInMinutes"];
			}
			set
			{
				base.Fields["MaxProcessingTimeInMinutes"] = value;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x00092B2D File Offset: 0x00090D2D
		// (set) Token: 0x060024C9 RID: 9417 RVA: 0x00092B44 File Offset: 0x00090D44
		[ValidateRange(0, 10080)]
		[Parameter(Mandatory = false, ParameterSetName = "AssistantHealthParameterSetName")]
		public uint WatermarkBehindWarningThreholdInMinutes
		{
			get
			{
				return (uint)base.Fields["WatermarkBehindWarningThreholdInMinutes"];
			}
			set
			{
				base.Fields["WatermarkBehindWarningThreholdInMinutes"] = value;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x00092B5C File Offset: 0x00090D5C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationTestAssistantHealth;
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x00092B64 File Offset: 0x00090D64
		protected override bool IsKnownException(Exception e)
		{
			return e is Win32Exception || e is InvalidOperationException || e is ManagementObjectNotFoundException || e is ManagementObjectAmbiguousException || e is System.ServiceProcess.TimeoutException || e is WmiException || e is TSCrashDumpsOnlyAvailableOnLocalMachineException || base.IsKnownException(e);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00092BB4 File Offset: 0x00090DB4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			Server server = this.GetServer();
			ExchangeServer exchangeServer = new ExchangeServer(server);
			base.Fields["ExchangeServer"] = exchangeServer;
			if (this.ResolveProblems.IsPresent && this.IncludeCrashDump.IsPresent && !StringComparer.OrdinalIgnoreCase.Equals(exchangeServer.Name, Environment.MachineName))
			{
				throw new TSCrashDumpsOnlyAvailableOnLocalMachineException();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00092C30 File Offset: 0x00090E30
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			MonitoringData monitoringData = null;
			List<TroubleshooterCheck> checkList = this.GetCheckList();
			List<TroubleshooterCheck> failedCheckList = TroubleshooterCheck.RunChecks(checkList, new TroubleshooterCheck.ContinueToNextCheck(TroubleshooterCheck.ShouldContinue), out monitoringData);
			this.RunResolveProblems(monitoringData, checkList, failedCheckList);
			base.WriteObject(monitoringData, true);
			TaskLogger.LogExit();
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00092C7C File Offset: 0x00090E7C
		private List<TroubleshooterCheck> GetCheckList()
		{
			return new List<TroubleshooterCheck>
			{
				new MailboxServerCheck(base.Fields),
				new MailboxAssistantsServiceStatusCheck(base.Fields),
				new MailboxAssistantsProcessingEvents(base.Fields),
				new MailboxAssistantsWatermarks(base.Fields)
			};
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x00092CD4 File Offset: 0x00090ED4
		private Server GetServer()
		{
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 172, "GetServer", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxAssistants\\TroubleShooter\\TestAssistantHealth.cs");
			LocalizedString? localizedString = null;
			IEnumerable<Server> objects = this.ServerName.GetObjects<Server>(null, session, new OptionalIdentityData(), out localizedString);
			using (IEnumerator<Server> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Server result = enumerator.Current;
					if (enumerator.MoveNext())
					{
						throw new ManagementObjectAmbiguousException(Strings.ErrorServerNotUnique(this.ServerName.ToString()));
					}
					return result;
				}
			}
			throw new ManagementObjectNotFoundException(localizedString ?? Strings.ErrorManagementObjectNotFound(this.ServerName.ToString()));
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00092DA4 File Offset: 0x00090FA4
		private void RunResolveProblems(MonitoringData monitoringData, List<TroubleshooterCheck> checkList, List<TroubleshooterCheck> failedCheckList)
		{
			if (failedCheckList.Count == 0)
			{
				ExchangeServer exchangeServer = (ExchangeServer)base.Fields["ExchangeServer"];
				monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5000, EventTypeEnumeration.Information, Strings.TSNoProblemsDetected(exchangeServer.Name)));
				using (List<TroubleshooterCheck>.Enumerator enumerator = checkList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TroubleshooterCheck troubleshooterCheck = enumerator.Current;
						AssistantTroubleshooterBase assistantTroubleshooterBase = troubleshooterCheck as AssistantTroubleshooterBase;
						if (assistantTroubleshooterBase != null)
						{
							monitoringData.PerformanceCounters.Add(assistantTroubleshooterBase.GetCrashDumpCountPerformanceCounter());
							break;
						}
					}
					return;
				}
			}
			if (this.ResolveProblems.IsPresent)
			{
				foreach (TroubleshooterCheck troubleshooterCheck2 in failedCheckList)
				{
					troubleshooterCheck2.Resolve(monitoringData);
				}
			}
		}

		// Token: 0x04001D0E RID: 7438
		private const string AssistantHealthParameterSetName = "AssistantHealthParameterSetName";
	}
}
