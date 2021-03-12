using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Inference
{
	// Token: 0x02000029 RID: 41
	public class CounterValueHistory : IDisposable
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00006B6B File Offset: 0x00004D6B
		public CounterValueHistory(TimeSpan retentionTime)
		{
			this.retentionTime = retentionTime;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006B90 File Offset: 0x00004D90
		public void AddCounterValues(params float[] counterValues)
		{
			DateTime utcNow = DateTime.UtcNow;
			utcNow = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, utcNow.Minute, 0, DateTimeKind.Utc);
			try
			{
				this.rwLock.EnterWriteLock();
				int num = 0;
				while (num < this.historicalCounterValues.Count && !(this.historicalCounterValues[num].Key > DateTime.UtcNow - this.retentionTime))
				{
					num++;
				}
				this.historicalCounterValues.RemoveRange(0, num);
				if (this.historicalCounterValues.Count <= 0 || !(this.historicalCounterValues[this.historicalCounterValues.Count - 1].Key == utcNow))
				{
					KeyValuePair<DateTime, float[]> item = new KeyValuePair<DateTime, float[]>(utcNow, counterValues);
					this.historicalCounterValues.Add(item);
				}
			}
			finally
			{
				try
				{
					this.rwLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public bool TryGetClosestCounterValues(DateTime timestamp, out float[] counterValues, out DateTime counterValuesTimestamp)
		{
			counterValues = new float[0];
			counterValuesTimestamp = DateTime.MinValue;
			KeyValuePair<DateTime, float[]> keyValuePair = new KeyValuePair<DateTime, float[]>(counterValuesTimestamp, counterValues);
			bool result;
			try
			{
				this.rwLock.EnterReadLock();
				if (this.historicalCounterValues.Count == 0)
				{
					result = false;
				}
				else
				{
					foreach (KeyValuePair<DateTime, float[]> keyValuePair2 in this.historicalCounterValues)
					{
						if (!(keyValuePair2.Key < timestamp))
						{
							KeyValuePair<DateTime, float[]> keyValuePair3;
							if (keyValuePair2.Key - timestamp < timestamp - keyValuePair.Key)
							{
								keyValuePair3 = keyValuePair2;
							}
							else
							{
								keyValuePair3 = keyValuePair;
							}
							counterValuesTimestamp = keyValuePair3.Key;
							counterValues = keyValuePair3.Value;
							return true;
						}
						keyValuePair = keyValuePair2;
					}
					counterValuesTimestamp = keyValuePair.Key;
					counterValues = keyValuePair.Value;
					result = true;
				}
			}
			finally
			{
				try
				{
					this.rwLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public void Dispose()
		{
			this.rwLock.Dispose();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006DDD File Offset: 0x00004FDD
		internal void AddCounterValues(DateTime key, params float[] counterValues)
		{
			this.historicalCounterValues.Add(new KeyValuePair<DateTime, float[]>(key, counterValues));
		}

		// Token: 0x04000128 RID: 296
		private readonly List<KeyValuePair<DateTime, float[]>> historicalCounterValues = new List<KeyValuePair<DateTime, float[]>>();

		// Token: 0x04000129 RID: 297
		private readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

		// Token: 0x0400012A RID: 298
		private readonly TimeSpan retentionTime;
	}
}
