using System;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Inference
{
	// Token: 0x0200002B RID: 43
	public class TrainingFailurePercentageCounter : MultiInstanceFailurePercentageCalculatedCounter
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00007127 File Offset: 0x00005327
		public TrainingFailurePercentageCounter() : base("MSExchangeInference Pipeline", "Training Failure Percentage", "Number Of Succeeded Documents", "Number Of Failed Documents", TrainingFailurePercentageCounter.TimeRange, TrainingFailurePercentageCounter.MinimumProcessedCountNeeded)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007150 File Offset: 0x00005350
		protected override bool ShouldCalculateForInstance(string instanceName)
		{
			Guid guid;
			return !string.IsNullOrEmpty(instanceName) && instanceName.StartsWith("training-", StringComparison.OrdinalIgnoreCase) && instanceName.Length > 9 && Guid.TryParse(instanceName.Substring(9), out guid);
		}

		// Token: 0x0400012F RID: 303
		public const string TrainingFailurePercentageCounterName = "Training Failure Percentage";

		// Token: 0x04000130 RID: 304
		private static readonly TimeSpan TimeRange = TimeSpan.FromMinutes(60.0);

		// Token: 0x04000131 RID: 305
		private static readonly int MinimumProcessedCountNeeded = 1000;
	}
}
