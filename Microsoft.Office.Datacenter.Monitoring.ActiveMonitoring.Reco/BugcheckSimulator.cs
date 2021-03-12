using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200002E RID: 46
	public class BugcheckSimulator
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004BA8 File Offset: 0x00002DA8
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00004BAF File Offset: 0x00002DAF
		public static BugcheckSimulator Instance { get; private set; } = new BugcheckSimulator();

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004BB7 File Offset: 0x00002DB7
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00004BBF File Offset: 0x00002DBF
		public bool IsHangRpc { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public bool IsEnabled
		{
			get
			{
				return RegistryHelper.GetPropertyIntBool("IsEnabled", false, "BugcheckSimulation", null, false);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004BDC File Offset: 0x00002DDC
		public DateTime SimulatedSystemBootTime
		{
			get
			{
				DateTime result = DateTime.MinValue;
				string property = RegistryHelper.GetProperty<string>("SimulatedBootTime", string.Empty, "BugcheckSimulation", null, false);
				if (!string.IsNullOrEmpty(property))
				{
					result = DateTime.Parse(property);
				}
				return result;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004C16 File Offset: 0x00002E16
		public TimeSpan Duration
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryHelper.GetProperty<int>("DurationInSeconds", 60, "BugcheckSimulation", null, false));
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004C31 File Offset: 0x00002E31
		public bool IsExitProcess
		{
			get
			{
				return RegistryHelper.GetPropertyIntBool("IsExitProcess", true, "BugcheckSimulation", null, false);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004C48 File Offset: 0x00002E48
		public void TakeActionIfRequired()
		{
			if (!this.IsEnabled)
			{
				return;
			}
			WTFDiagnostics.TraceWarning(ExTraceGlobals.RecoveryActionTracer, this.traceContext, "Bugcheck simulator is attempting to do the operation.", null, "TakeActionIfRequired", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\BugcheckSimulator.cs", 153);
			bool isExitProcess = this.IsExitProcess;
			TimeSpan duration = this.Duration;
			try
			{
				this.IsHangRpc = true;
				Thread.Sleep(duration);
				DateTime localTime = ExDateTime.Now.LocalTime;
				this.SetSimulatedBootTime(localTime);
				ManagedAvailabilityCrimsonEvents.SimulatedBugcheckInAction.Log<TimeSpan, bool, DateTime, bool>(duration, false, localTime, isExitProcess);
				if (isExitProcess)
				{
					Environment.Exit(1);
				}
			}
			finally
			{
				this.IsHangRpc = false;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004CE4 File Offset: 0x00002EE4
		private void SetDuration(TimeSpan duration)
		{
			RegistryHelper.SetProperty<int>("DurationInSeconds", (int)duration.TotalSeconds, "BugcheckSimulation", null, false);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004D00 File Offset: 0x00002F00
		private void SetSimulatedBootTime(DateTime simulatedDateTime)
		{
			string propertValue = simulatedDateTime.ToString("o");
			RegistryHelper.SetProperty<string>("SimulatedBootTime", propertValue, "BugcheckSimulation", null, false);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004D2D File Offset: 0x00002F2D
		private void SetEnabled(bool isEnabled)
		{
			RegistryHelper.SetPropertyIntBool("IsEnabled", isEnabled, "BugcheckSimulation", null, false);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004D42 File Offset: 0x00002F42
		private void SetIsExitProcess(bool isExitProcess)
		{
			RegistryHelper.SetPropertyIntBool("IsExitProcess", isExitProcess, "BugcheckSimulation", null, false);
		}

		// Token: 0x040000B0 RID: 176
		internal const string SubKeyName = "BugcheckSimulation";

		// Token: 0x040000B1 RID: 177
		internal const string PropertyNameIsEnabled = "IsEnabled";

		// Token: 0x040000B2 RID: 178
		internal const string PropertyNameDurationInSeconds = "DurationInSeconds";

		// Token: 0x040000B3 RID: 179
		internal const string PropertyNameSimulatedBootTime = "SimulatedBootTime";

		// Token: 0x040000B4 RID: 180
		internal const string PropertyNameIsExitProcess = "IsExitProcess";

		// Token: 0x040000B5 RID: 181
		private TracingContext traceContext = TracingContext.Default;
	}
}
