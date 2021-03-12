using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000002 RID: 2
	[Cmdlet("Test", "ActiveDirectoryConnectivity", DefaultParameterSetName = "MonitoringContext", SupportsShouldProcess = true)]
	public sealed class TestActiveDirectoryConnectivityTask : Task
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020FE File Offset: 0x000002FE
		[Parameter(Mandatory = false, ParameterSetName = "MonitoringContext")]
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002116 File Offset: 0x00000316
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002137 File Offset: 0x00000337
		[Parameter(Mandatory = false)]
		public int TotalTimeoutInMinutes
		{
			get
			{
				return (int)(base.Fields["TotalTimeoutInMinutes"] ?? 2);
			}
			set
			{
				base.Fields["TotalTimeoutInMinutes"] = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000214F File Offset: 0x0000034F
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002171 File Offset: 0x00000371
		[Parameter(Mandatory = false)]
		public int SearchLatencyThresholdInMilliseconds
		{
			get
			{
				return (int)(base.Fields["SearchLatencyThresholdInMilliseconds"] ?? 50);
			}
			set
			{
				base.Fields["SearchLatencyThresholdInMilliseconds"] = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002189 File Offset: 0x00000389
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021AA File Offset: 0x000003AA
		[Parameter(Mandatory = false, ParameterSetName = "TargetDC")]
		public bool UseADDriver
		{
			get
			{
				return (bool)(base.Fields["UseADDriver"] ?? true);
			}
			set
			{
				base.Fields["UseADDriver"] = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021C2 File Offset: 0x000003C2
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021D9 File Offset: 0x000003D9
		[Parameter(Mandatory = true, ParameterSetName = "TargetDC")]
		public string TargetDC
		{
			get
			{
				return (string)base.Fields["TargetDC"];
			}
			set
			{
				base.Fields["TargetDC"] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021F4 File Offset: 0x000003F4
		internal bool SkipRemainingTests { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x000021FD File Offset: 0x000003FD
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002210 File Offset: 0x00000410
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (!base.HasErrors)
				{
					this.ActiveDirectoryConnectivityContext = ActiveDirectoryConnectivityContext.CreateForActiveDirectoryConnectivity(this, this.monitoringData, (this.TargetDC != null) ? this.TargetDC.ToString() : null);
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

		// Token: 0x06000010 RID: 16 RVA: 0x00002290 File Offset: 0x00000490
		protected override void InternalBeginProcessing()
		{
			if (this.MonitoringContext)
			{
				this.monitoringData = new MonitoringData();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022AC File Offset: 0x000004AC
		protected override void InternalProcessRecord()
		{
			base.InternalBeginProcessing();
			TaskLogger.LogEnter();
			try
			{
				this.RunTasksWithTimeout(ExDateTime.Now.AddMinutes((double)this.TotalTimeoutInMinutes), ActiveDirectoryConnectivityBase.BuildTransactionHelper(this.BuildActiveDirectoryConnectivityTestPipeline()));
			}
			catch (LocalizedException e)
			{
				this.HandleException(e);
			}
			finally
			{
				if (this.monitoringData != null)
				{
					this.monitoringData.PerformanceCounters.Add(new MonitoringPerformanceCounter(TestActiveDirectoryConnectivityTask.CmdletMonitoringEventSource, TestActiveDirectoryConnectivityTask.PerformanceCounter, this.BuildInstanceName(), this.TotalLatency.TotalMilliseconds));
					if (this.TotalLatency.TotalMilliseconds > 0.0)
					{
						this.monitoringData.Events.Add(new MonitoringEvent(TestActiveDirectoryConnectivityTask.CmdletMonitoringEventSource, 3001, EventTypeEnumeration.Success, Strings.ActiveDirectoryConnectivityTransactionsAllSucceeded.ToString()));
					}
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023B4 File Offset: 0x000005B4
		private void RunTasksWithTimeout(ExDateTime expireTime, IEnumerable<AsyncResult<ActiveDirectoryConnectivityOutcome>> task)
		{
			this.TotalLatency = TimeSpan.Zero;
			using (IEnumerator<AsyncResult<ActiveDirectoryConnectivityOutcome>> enumerator = task.GetEnumerator())
			{
				for (;;)
				{
					AsyncResult<ActiveDirectoryConnectivityOutcome> asyncResult = null;
					TimeSpan timeSpan = expireTime - ExDateTime.Now;
					try
					{
						if (enumerator.MoveNext())
						{
							if (timeSpan.Ticks >= 0L)
							{
								asyncResult = enumerator.Current;
								if (timeSpan.Ticks >= 0L && !asyncResult.IsCompleted)
								{
									ActiveDirectoryConnectivityOutcome activeDirectoryConnectivityOutcome = asyncResult.Outcomes[asyncResult.Outcomes.Count - 1];
									if (activeDirectoryConnectivityOutcome.Timeout != null && timeSpan > activeDirectoryConnectivityOutcome.Timeout.Value)
									{
										timeSpan = activeDirectoryConnectivityOutcome.Timeout.Value;
									}
									asyncResult.AsyncWaitHandle.WaitOne(timeSpan, true);
								}
								continue;
							}
							string message = string.Format("Task failed on timeout. Overtime = {0}.", ExDateTime.Now - expireTime);
							this.WriteError(new TaskException(Strings.ErrorRecordReport(message, 1)), ErrorCategory.OperationTimeout, this, false);
						}
					}
					finally
					{
						if (asyncResult != null)
						{
							this.ReportTestStepResult(asyncResult, timeSpan);
						}
					}
					break;
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E8 File Offset: 0x000006E8
		private void ReportTestStepResult(AsyncResult<ActiveDirectoryConnectivityOutcome> asyncResult, TimeSpan expireTime)
		{
			foreach (ActiveDirectoryConnectivityOutcome activeDirectoryConnectivityOutcome in asyncResult.Outcomes)
			{
				if (activeDirectoryConnectivityOutcome.Result.Value == CasTransactionResultEnum.Undefined)
				{
					base.WriteVerbose(Strings.TaskTimeout(activeDirectoryConnectivityOutcome.Scenario, expireTime));
					if (this.monitoringData != null)
					{
						this.monitoringData.Events.Add(new MonitoringEvent(TestActiveDirectoryConnectivityTask.CmdletMonitoringEventSource, (int)TestActiveDirectoryConnectivityTask.EnsureFailureEventId(activeDirectoryConnectivityOutcome.Id), EventTypeEnumeration.Error, Strings.TaskTimeout(activeDirectoryConnectivityOutcome.Scenario, expireTime)));
					}
					activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Failure);
				}
				if (activeDirectoryConnectivityOutcome.Result.Value == CasTransactionResultEnum.Success && this.TotalLatency.TotalMilliseconds >= 0.0)
				{
					this.TotalLatency += activeDirectoryConnectivityOutcome.Latency;
				}
				base.WriteObject(activeDirectoryConnectivityOutcome);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000025EC File Offset: 0x000007EC
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000025F4 File Offset: 0x000007F4
		private TimeSpan TotalLatency { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x000025FD File Offset: 0x000007FD
		private string BuildInstanceName()
		{
			return string.Format("ActiveDirectoryConnectivity", new object[0]);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002610 File Offset: 0x00000810
		private void HandleException(LocalizedException e)
		{
			this.TotalLatency = TestActiveDirectoryConnectivityTask.DefaultFailureTime;
			if (!this.MonitoringContext)
			{
				this.WriteError(e, ErrorCategory.OperationStopped, this, true);
				return;
			}
			this.monitoringData.Events.Add(new MonitoringEvent(TestActiveDirectoryConnectivityTask.CmdletMonitoringEventSource, 2006, EventTypeEnumeration.Error, Strings.ActiveDirectoryConnectivityExceptionThrown(e.ToString())));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000268C File Offset: 0x0000088C
		private Func<ActiveDirectoryConnectivityBase>[] BuildActiveDirectoryConnectivityTestPipeline()
		{
			List<Func<ActiveDirectoryConnectivityBase>> list = new List<Func<ActiveDirectoryConnectivityBase>>();
			if (base.ParameterSetName == "TargetDC")
			{
				list.Add(() => new MachinePingTask(this.ActiveDirectoryConnectivityContext));
			}
			list.Add(() => new ActiveDirectorySearchTask(this.ActiveDirectoryConnectivityContext));
			return list.ToArray();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000026E2 File Offset: 0x000008E2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestActiveDirectoryConnectivity;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026EC File Offset: 0x000008EC
		internal static TestActiveDirectoryConnectivityTask.ScenarioId EnsureFailureEventId(TestActiveDirectoryConnectivityTask.ScenarioId eventId)
		{
			if (eventId < TestActiveDirectoryConnectivityTask.ScenarioId.MachinePingFailed)
			{
				TestActiveDirectoryConnectivityTask.ScenarioId scenarioId = eventId + 1000;
				ExAssert.RetailAssert(EnumValidator.IsValidValue<TestActiveDirectoryConnectivityTask.ScenarioId>(scenarioId), "Corresponding failure eventId {0} is not defined for scenario {1}.", new object[]
				{
					(int)scenarioId,
					eventId
				});
				return scenarioId;
			}
			return eventId;
		}

		// Token: 0x04000001 RID: 1
		private const int DefaultTimeOutInMinutes = 2;

		// Token: 0x04000002 RID: 2
		private const int DefaultSearchLatencyThresholdInMilliseconds = 50;

		// Token: 0x04000003 RID: 3
		private const string UseADDriverParam = "UseADDriver";

		// Token: 0x04000004 RID: 4
		private const string TargetDCParam = "TargetDC";

		// Token: 0x04000005 RID: 5
		private const string MonitoringContextParam = "MonitoringContext";

		// Token: 0x04000006 RID: 6
		private const string TotalTimeoutInMinutesParam = "TotalTimeoutInMinutes";

		// Token: 0x04000007 RID: 7
		private const string SearchLatencyThresholdInMillisecondsParam = "SearchLatencyThresholdInMilliseconds";

		// Token: 0x04000008 RID: 8
		internal const string ActiveDirectoryConnectivity = "ActiveDirectoryConnectivity";

		// Token: 0x04000009 RID: 9
		private const int FailedEventIdBase = 1000;

		// Token: 0x0400000A RID: 10
		private ActiveDirectoryConnectivityContext ActiveDirectoryConnectivityContext;

		// Token: 0x0400000B RID: 11
		private MonitoringData monitoringData;

		// Token: 0x0400000C RID: 12
		public static readonly string CmdletMonitoringEventSource = "MSExchange Monitoring ActiveDirectoryConnectivity";

		// Token: 0x0400000D RID: 13
		public static readonly string PerformanceCounter = "ActiveDirectoryConnectivity Latency";

		// Token: 0x0400000E RID: 14
		public static readonly TimeSpan DefaultFailureTime = TimeSpan.FromMilliseconds(-1000.0);

		// Token: 0x02000003 RID: 3
		public enum ScenarioId
		{
			// Token: 0x04000012 RID: 18
			MachinePing = 1001,
			// Token: 0x04000013 RID: 19
			Search,
			// Token: 0x04000014 RID: 20
			IsNTDSRunning,
			// Token: 0x04000015 RID: 21
			IsNetlogonRunning,
			// Token: 0x04000016 RID: 22
			SearchLatency,
			// Token: 0x04000017 RID: 23
			PlaceHolderNoException,
			// Token: 0x04000018 RID: 24
			MachinePingFailed = 2001,
			// Token: 0x04000019 RID: 25
			SearchFailed,
			// Token: 0x0400001A RID: 26
			NTDSNotRunning,
			// Token: 0x0400001B RID: 27
			NetLogonNotRunning,
			// Token: 0x0400001C RID: 28
			SearchOverLatency,
			// Token: 0x0400001D RID: 29
			ExceptionThrown,
			// Token: 0x0400001E RID: 30
			AllTransactionsSucceeded = 3001
		}
	}
}
