using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200002F RID: 47
	internal sealed class E4eBackoffListCache
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00007CAB File Offset: 0x00005EAB
		private E4eBackoffListCache()
		{
			this.senderBackoffListCache = new ExactTimeoutCache<string, DateTime>(null, null, null, 10240, false);
			this.recipientsBackoffListCache = new ExactTimeoutCache<string, DateTime>(null, null, null, 10240, false);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007CDB File Offset: 0x00005EDB
		public static E4eBackoffListCache Instance
		{
			get
			{
				return E4eBackoffListCache.instance;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public void UpdateCache(string budgetType, string emailAddress, string backoffUntilUtcStr)
		{
			if (string.IsNullOrEmpty(budgetType) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(backoffUntilUtcStr))
			{
				return;
			}
			if (!SmtpAddress.IsValidSmtpAddress(emailAddress))
			{
				return;
			}
			BudgetType budgetType2;
			try
			{
				if (!Enum.TryParse<BudgetType>(budgetType, true, out budgetType2))
				{
					return;
				}
			}
			catch (ArgumentException)
			{
				return;
			}
			DateTime dateTime;
			bool flag = DateTime.TryParse(backoffUntilUtcStr, out dateTime);
			if (flag && !(dateTime <= DateTime.UtcNow))
			{
				TimeSpan absoluteLiveTime = (dateTime == DateTime.MaxValue) ? TimeSpan.MaxValue : (dateTime - DateTime.UtcNow);
				if (absoluteLiveTime.TotalMilliseconds <= 0.0)
				{
					return;
				}
				if (budgetType2 == BudgetType.E4eSender)
				{
					this.senderBackoffListCache.TryInsertAbsolute(emailAddress, dateTime, absoluteLiveTime);
					return;
				}
				if (budgetType2 == BudgetType.E4eRecipient)
				{
					this.recipientsBackoffListCache.TryInsertAbsolute(emailAddress, dateTime, absoluteLiveTime);
				}
				return;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public bool ShouldBackOff(string senderEmailAddress, string recipientEmailAddress)
		{
			return (!string.IsNullOrEmpty(senderEmailAddress) && this.ContainsValidBackoffEntry(this.senderBackoffListCache, senderEmailAddress)) || (!string.IsNullOrEmpty(recipientEmailAddress) && this.ContainsValidBackoffEntry(this.recipientsBackoffListCache, recipientEmailAddress));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007DE8 File Offset: 0x00005FE8
		private bool ContainsValidBackoffEntry(ExactTimeoutCache<string, DateTime> timeoutCache, string emailAddress)
		{
			DateTime t;
			return timeoutCache.TryGetValue(emailAddress, out t) && t > DateTime.UtcNow;
		}

		// Token: 0x0400007E RID: 126
		private const int BackoffListCacheSize = 10240;

		// Token: 0x0400007F RID: 127
		private static E4eBackoffListCache instance = new E4eBackoffListCache();

		// Token: 0x04000080 RID: 128
		private static object staticLock = new object();

		// Token: 0x04000081 RID: 129
		private ExactTimeoutCache<string, DateTime> senderBackoffListCache;

		// Token: 0x04000082 RID: 130
		private ExactTimeoutCache<string, DateTime> recipientsBackoffListCache;
	}
}
