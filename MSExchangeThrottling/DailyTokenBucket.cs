using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x02000002 RID: 2
	internal sealed class DailyTokenBucket
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public DailyTokenBucket()
		{
			this.timestamp = DateTime.MinValue.ToUniversalTime();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public bool IsExpired(DateTime currentTime)
		{
			if (currentTime < this.timestamp)
			{
				this.timestamp = currentTime;
				return false;
			}
			long num = (currentTime - this.timestamp).Ticks / 36000000000L;
			return num >= 24L || this.issuedCount < (int)num * this.totalCount / 24;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000215C File Offset: 0x0000035C
		public bool ObtainTokens<T>(ObtainTokensRequest<T> request, DateTime currentTime)
		{
			int requestedTokenCount = request.RequestedTokenCount;
			int totalTokenCount = request.TotalTokenCount;
			if (totalTokenCount < 1)
			{
				throw new ArgumentOutOfRangeException("totalTokenCount", totalTokenCount, "totalTokenCount should be greater than 0");
			}
			if (requestedTokenCount < 1 || requestedTokenCount > totalTokenCount)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "requestedCount should be greater than 0 and less or equal to totalCount of {0}", new object[]
				{
					totalTokenCount
				});
				throw new ArgumentOutOfRangeException("requestedCount", requestedTokenCount, message);
			}
			this.totalCount = totalTokenCount;
			if (this.IsExpired(currentTime))
			{
				ThrottlingServiceLog.LogTokenExpiry(request.MailboxGuid, this);
				this.timestamp = currentTime;
				this.issuedCount = 0;
			}
			bool result = false;
			if (this.issuedCount + requestedTokenCount <= this.totalCount)
			{
				this.issuedCount += requestedTokenCount;
				result = true;
			}
			ThrottlingServiceLog.LogObtainTokens<T>(request, this, result);
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000222C File Offset: 0x0000042C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Time stamp = ",
				this.timestamp,
				" Issued count = ",
				this.issuedCount,
				" Total count =",
				this.totalCount
			});
		}

		// Token: 0x04000001 RID: 1
		private DateTime timestamp;

		// Token: 0x04000002 RID: 2
		private int issuedCount;

		// Token: 0x04000003 RID: 3
		private int totalCount;
	}
}
