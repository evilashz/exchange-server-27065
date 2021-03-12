using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Inference
{
	// Token: 0x0200002C RID: 44
	public class TrainingFailurePercentageTrigger : PerInstanceTrigger
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000071B4 File Offset: 0x000053B4
		public TrainingFailurePercentageTrigger(IJob job) : base(job, string.Format("{0}\\({1}\\)\\\\{2}", "MSExchangeInference Pipeline", "training-(.+?)", "Training Failure Percentage"), new PerfLogCounterTrigger.TriggerConfiguration("TrainingFailurePercentageTrigger", 5.0, double.MaxValue, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(30.0), 0), new HashSet<DiagnosticMeasurement>(), new HashSet<string>())
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007232 File Offset: 0x00005432
		protected override string CollectAdditionalInformation(PerfLogCounterTrigger.SurpassedThresholdContext context)
		{
			return "The failure percentage is calculated from the 'MSExchangeInference Pipeline(training-<mdbGuid>)\\Number Of Succeeded Documents' counter and 'Number Of Failed Documents' counter over the past hour.";
		}

		// Token: 0x04000132 RID: 306
		private const string AdditionalInfo = "The failure percentage is calculated from the 'MSExchangeInference Pipeline(training-<mdbGuid>)\\Number Of Succeeded Documents' counter and 'Number Of Failed Documents' counter over the past hour.";
	}
}
