using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Inference
{
	// Token: 0x0200002A RID: 42
	public class MultiInstanceFailurePercentageCalculatedCounter : MultiInstanceSingleObjectCalculatedCounter, IDisposable
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public MultiInstanceFailurePercentageCalculatedCounter(string objectName, string calculatedCounterName, string successCounterName, string failureCounterName) : this(objectName, calculatedCounterName, successCounterName, failureCounterName, TimeSpan.FromMinutes(60.0), 0)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006E10 File Offset: 0x00005010
		public MultiInstanceFailurePercentageCalculatedCounter(string objectName, string calculatedCounterName, string successCounterName, string failureCounterName, TimeSpan timeRange, int minimumProcessedCountNeeded) : base(objectName, calculatedCounterName, new string[]
		{
			successCounterName,
			failureCounterName
		})
		{
			this.timeRange = timeRange;
			this.minimumProcessedCountNeeded = minimumProcessedCountNeeded;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006E60 File Offset: 0x00005060
		public override void OnLogLine(Dictionary<DiagnosticMeasurement, float?> countersAndValues, DateTime? timestamp = null)
		{
			foreach (KeyValuePair<string, DiagnosticMeasurement[]> keyValuePair in base.Instances)
			{
				string key = keyValuePair.Key;
				if (this.ShouldCalculateForInstance(key))
				{
					float? num = countersAndValues[keyValuePair.Value[0]];
					float? num2 = countersAndValues[keyValuePair.Value[1]];
					DiagnosticMeasurement key2 = keyValuePair.Value[2];
					if (num2 != null && num != null)
					{
						CounterValueHistory counterValueHistory = this.GetCounterValueHistory(key);
						counterValueHistory.AddCounterValues(new float[]
						{
							num.Value,
							num2.Value
						});
						DateTime dateTime = DateTime.UtcNow - this.timeRange;
						float[] array;
						DateTime d;
						if (!counterValueHistory.TryGetClosestCounterValues(dateTime, out array, out d) || Math.Abs((d - dateTime).TotalMinutes) > (double)this.counterValueMinuteGapAllowed)
						{
							break;
						}
						float num3 = 0f;
						float num4 = 0f;
						if (num.Value >= array[0] && num2.Value >= array[1])
						{
							num3 = array[0];
							num4 = array[1];
						}
						float num5 = num.Value - num3;
						float num6 = num2.Value - num4;
						float num7 = num5 + num6;
						if (num7 < (float)this.minimumProcessedCountNeeded)
						{
							break;
						}
						float value = 0f;
						if (num7 > 0f)
						{
							value = num6 / num7 * 100f;
						}
						countersAndValues.Add(key2, new float?(value));
					}
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007018 File Offset: 0x00005218
		public void Dispose()
		{
			lock (this.counterValueHistories)
			{
				foreach (CounterValueHistory counterValueHistory in this.counterValueHistories.Values)
				{
					counterValueHistory.Dispose();
				}
				this.counterValueHistories.Clear();
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000070A4 File Offset: 0x000052A4
		internal CounterValueHistory GetCounterValueHistory(string instanceName)
		{
			CounterValueHistory result;
			lock (this.counterValueHistories)
			{
				CounterValueHistory counterValueHistory;
				if (!this.counterValueHistories.TryGetValue(instanceName, out counterValueHistory))
				{
					counterValueHistory = new CounterValueHistory(TimeSpan.FromMinutes(this.timeRange.TotalMinutes * 1.5));
					this.counterValueHistories[instanceName] = counterValueHistory;
				}
				result = counterValueHistory;
			}
			return result;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007124 File Offset: 0x00005324
		protected virtual bool ShouldCalculateForInstance(string instanceName)
		{
			return true;
		}

		// Token: 0x0400012B RID: 299
		private readonly int counterValueMinuteGapAllowed = 10;

		// Token: 0x0400012C RID: 300
		private readonly TimeSpan timeRange;

		// Token: 0x0400012D RID: 301
		private readonly int minimumProcessedCountNeeded;

		// Token: 0x0400012E RID: 302
		private readonly Dictionary<string, CounterValueHistory> counterValueHistories = new Dictionary<string, CounterValueHistory>(StringComparer.OrdinalIgnoreCase);
	}
}
