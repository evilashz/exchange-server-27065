using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x02000022 RID: 34
	internal class TokenBucket : ITokenBucket
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00004058 File Offset: 0x00002258
		public TokenBucket(uint maxBurst, uint rechargeRate, uint rechargeInterval) : this(maxBurst, rechargeRate, rechargeInterval, maxBurst, null)
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004078 File Offset: 0x00002278
		public TokenBucket(uint maxBurst, uint rechargeRate, uint rechargeInterval, uint initialBalance, ExDateTime? nextRecharge = null)
		{
			ArgumentValidator.ThrowIfOutOfRange<uint>("maxBurst", maxBurst, 1U, uint.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<uint>("rechargeRate", rechargeRate, 1U, uint.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<uint>("rechargeInterval", rechargeInterval, 1U, uint.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<uint>("initialBalance", initialBalance, 0U, maxBurst);
			this.MaxBurst = maxBurst;
			this.RechargeRate = rechargeRate;
			this.RechargeInterval = rechargeInterval;
			this.currentBalance = initialBalance;
			this.nextRecharge = ((nextRecharge != null) ? nextRecharge.Value : ((ExDateTime)TimeProvider.UtcNow).AddMilliseconds(this.RechargeInterval));
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000410F File Offset: 0x0000230F
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004117 File Offset: 0x00002317
		public uint MaxBurst { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004120 File Offset: 0x00002320
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004128 File Offset: 0x00002328
		public uint RechargeRate { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004131 File Offset: 0x00002331
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004139 File Offset: 0x00002339
		public uint RechargeInterval { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004142 File Offset: 0x00002342
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004150 File Offset: 0x00002350
		public uint CurrentBalance
		{
			get
			{
				this.RechargeBalance();
				return this.currentBalance;
			}
			private set
			{
				this.currentBalance = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004159 File Offset: 0x00002359
		public ExDateTime NextRecharge
		{
			get
			{
				this.RechargeBalance();
				return this.nextRecharge;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004167 File Offset: 0x00002367
		public bool IsFull
		{
			get
			{
				return this.CurrentBalance == this.MaxBurst;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004177 File Offset: 0x00002377
		public bool IsEmpty
		{
			get
			{
				return this.CurrentBalance <= 0U;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004185 File Offset: 0x00002385
		public bool TryTakeToken()
		{
			if (this.CurrentBalance > 0U)
			{
				this.CurrentBalance -= 1U;
				return true;
			}
			return false;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000041A4 File Offset: 0x000023A4
		public override string ToString()
		{
			return string.Format("{{currentBalance:{0}; maxBurst:{1}; rechargeRate:{2}; rechargeInterval:{3}; nextRecharge:{4}}}", new object[]
			{
				this.CurrentBalance,
				this.MaxBurst,
				this.RechargeRate,
				this.RechargeInterval,
				this.NextRecharge
			});
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000420C File Offset: 0x0000240C
		private void RechargeBalance()
		{
			ExDateTime t = (ExDateTime)TimeProvider.UtcNow;
			if (t < this.nextRecharge)
			{
				return;
			}
			double num = this.RechargeInterval;
			num += t.Subtract(this.nextRecharge).TotalMilliseconds;
			double num2 = Math.Floor(num / this.RechargeInterval);
			if (num2 <= (this.MaxBurst - this.currentBalance) / this.RechargeRate)
			{
				this.currentBalance += (uint)num2 * this.RechargeRate;
			}
			else
			{
				this.currentBalance = this.MaxBurst;
			}
			double num3 = num - num2 * this.RechargeInterval;
			this.nextRecharge = t.AddMilliseconds(this.RechargeInterval - num3);
		}

		// Token: 0x04000058 RID: 88
		private uint currentBalance;

		// Token: 0x04000059 RID: 89
		private ExDateTime nextRecharge;
	}
}
