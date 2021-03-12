using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public sealed class RetryStrategy
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x0001797C File Offset: 0x00015B7C
		public RetryStrategy(string retryStrategy = null)
		{
			if (string.IsNullOrEmpty(retryStrategy))
			{
				retryStrategy = "2:15;2:30;2:60;2:300;2:1200;2:3600";
			}
			string[] array = retryStrategy.ToLower().Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					':'
				});
				if (array3.Length != 2)
				{
					throw new ArgumentException("invalid format for retryStrategy", "retryStrategy");
				}
				int num = int.Parse(array3[0].Trim());
				int num2 = int.Parse(array3[1].Trim());
				if (num < 0 || num2 < 0)
				{
					throw new ArgumentException("invalid format for retryStrategy", "retryStrategy");
				}
				if (num > 0)
				{
					int key = this.retryTable.Any<KeyValuePair<int, TimeSpan>>() ? (this.retryTable.Keys.Max() + num) : num;
					this.retryTable[key] = TimeSpan.FromSeconds((double)num2);
				}
			}
			if (!this.retryTable.Any<KeyValuePair<int, TimeSpan>>())
			{
				this.retryTable[0] = TimeSpan.Zero;
			}
			this.maxTryCount = this.retryTable.Keys.Max();
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00017ABB File Offset: 0x00015CBB
		public bool CanRetry(int currentTryCount)
		{
			return currentTryCount <= this.maxTryCount && currentTryCount >= 0;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00017AE8 File Offset: 0x00015CE8
		public TimeSpan GetRetryInterval(int currentTryCount)
		{
			if (!this.CanRetry(currentTryCount))
			{
				throw new ArgumentException("not retriable", "currentTryCount");
			}
			return this.retryTable[(from p in this.retryTable.Keys
			where p >= currentTryCount
			select p).Min()];
		}

		// Token: 0x04000442 RID: 1090
		private const string DefaultRetryStrategy = "2:15;2:30;2:60;2:300;2:1200;2:3600";

		// Token: 0x04000443 RID: 1091
		private readonly int maxTryCount;

		// Token: 0x04000444 RID: 1092
		private readonly Dictionary<int, TimeSpan> retryTable = new Dictionary<int, TimeSpan>();
	}
}
