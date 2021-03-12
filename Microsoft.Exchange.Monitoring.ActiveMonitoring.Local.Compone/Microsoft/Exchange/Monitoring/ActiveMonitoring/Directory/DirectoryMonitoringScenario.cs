using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x0200013A RID: 314
	public class DirectoryMonitoringScenario
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000376DE File Offset: 0x000358DE
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x000376E6 File Offset: 0x000358E6
		internal string Scenario { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000376EF File Offset: 0x000358EF
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x000376F7 File Offset: 0x000358F7
		internal string EscalationMessageSubject { get; private set; }

		// Token: 0x06000939 RID: 2361 RVA: 0x00037700 File Offset: 0x00035900
		public DirectoryMonitoringScenario(string scenario, string escalationMessageSubject, bool allowCorrelation)
		{
			this.Scenario = scenario;
			this.EscalationMessageSubject = escalationMessageSubject;
			this.AllowCorrelationToMonitoring = allowCorrelation;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0003771D File Offset: 0x0003591D
		public DirectoryMonitoringScenario(string scenario, string escalationMessageSubject) : this(scenario, escalationMessageSubject, false)
		{
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00037728 File Offset: 0x00035928
		public string ProbeName
		{
			get
			{
				return string.Format("{0}Probe", this.Scenario);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0003773A File Offset: 0x0003593A
		public string MonitorName
		{
			get
			{
				return string.Format("{0}Monitor", this.Scenario);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0003774C File Offset: 0x0003594C
		public string EscalateResponderName
		{
			get
			{
				return string.Format("{0}Escalate", this.Scenario);
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0003775E File Offset: 0x0003595E
		public string EscalatePerExceptionResponderName(string exception)
		{
			return string.Format("{0}{1}Escalate", this.Scenario, exception);
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00037771 File Offset: 0x00035971
		public string RestartResponderName
		{
			get
			{
				return string.Format("{0}Restart", this.Scenario);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00037783 File Offset: 0x00035983
		public string RestartServerResponderName
		{
			get
			{
				return string.Format("{0}ServerReboot", this.Scenario);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00037795 File Offset: 0x00035995
		public string PutDCInMMResponderName
		{
			get
			{
				return string.Format("{0}PutDCIntoMM", this.Scenario);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x000377A7 File Offset: 0x000359A7
		public string PutMultipleDCInMMResponderName
		{
			get
			{
				return string.Format("{0}PutMultipleDCInMM", this.Scenario);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x000377B9 File Offset: 0x000359B9
		public string CheckDCInMMEscalateResponderName
		{
			get
			{
				return string.Format("{0}CheckDCInMMEscalate", this.Scenario);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000377CB File Offset: 0x000359CB
		public string CheckMultipleDCInMMEscalateResponderName
		{
			get
			{
				return string.Format("{0}CheckMultipleDCInMMEscalate", this.Scenario);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000377DD File Offset: 0x000359DD
		public string RenameNTDSPowerOffResponderName
		{
			get
			{
				return string.Format("{0}RenameNTDSPowerOff", this.Scenario);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x000377EF File Offset: 0x000359EF
		public string ADDiagTraceResponderName
		{
			get
			{
				return string.Format("{0}ADDiagTrace", this.Scenario);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00037801 File Offset: 0x00035A01
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x00037809 File Offset: 0x00035A09
		public bool AllowCorrelationToMonitoring { get; private set; }
	}
}
