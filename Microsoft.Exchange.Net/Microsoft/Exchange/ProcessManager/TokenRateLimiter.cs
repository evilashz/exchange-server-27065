using System;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000819 RID: 2073
	internal class TokenRateLimiter
	{
		// Token: 0x06002BBC RID: 11196 RVA: 0x00061564 File Offset: 0x0005F764
		public bool TryFetchToken(int ratePerMinute)
		{
			if (++this.tokens <= ratePerMinute)
			{
				return true;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (this.IsIdle(utcNow))
			{
				this.lastTokensReset = utcNow;
				this.tokens = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000615A7 File Offset: 0x0005F7A7
		public void ReleaseUnusedToken()
		{
			if (this.tokens > 0)
			{
				this.tokens--;
			}
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000615C0 File Offset: 0x0005F7C0
		public bool IsIdle(DateTime currentTime)
		{
			return currentTime.Ticks >= this.lastTokensReset.Ticks + 600000000L;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000615E0 File Offset: 0x0005F7E0
		private void SetLastTokensResetTime(DateTime time)
		{
			this.lastTokensReset = time;
		}

		// Token: 0x04002610 RID: 9744
		private int tokens;

		// Token: 0x04002611 RID: 9745
		private DateTime lastTokensReset = DateTime.UtcNow;
	}
}
