using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200004B RID: 75
	public abstract class OverallConsecutiveFailuresMonitor : MonitorWorkItem
	{
		// Token: 0x06000518 RID: 1304
		protected abstract bool ShouldAlert();

		// Token: 0x06000519 RID: 1305
		protected abstract bool HaveInsufficientSamples();

		// Token: 0x0600051A RID: 1306
		protected abstract Task SetConsecutiveFailureNumbers(CancellationToken cancellationToken);

		// Token: 0x0600051B RID: 1307 RVA: 0x00013AA8 File Offset: 0x00011CA8
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			this.SetConsecutiveFailureNumbers(cancellationToken).ContinueWith(delegate(Task t)
			{
				this.HandleInsufficientSamples(new Func<bool>(this.HaveInsufficientSamples), cancellationToken);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Current).ContinueWith(delegate(Task t)
			{
				if (!base.Result.IsAlert)
				{
					base.Result.IsAlert = this.ShouldAlert();
				}
				WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "OverallConsecutiveFailuresMonitor: Finished analyzing probe results.", null, "DoMonitorWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Monitors\\OverallConsecutiveFailuresMonitor.cs", 71);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Current);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00013B18 File Offset: 0x00011D18
		protected virtual void SetStateAttribute6ForScopeMonitoring(double counter)
		{
			if (!string.IsNullOrWhiteSpace(base.Definition.TargetScopes))
			{
				if (!double.IsNaN(counter) || counter < 0.0)
				{
					string paramName = "OverallConsecutiveFailuresMonitor: Counter passed to the scope monitor is invalid";
					throw new ArgumentNullException(paramName);
				}
				base.Result.StateAttribute6 = counter;
			}
		}
	}
}
