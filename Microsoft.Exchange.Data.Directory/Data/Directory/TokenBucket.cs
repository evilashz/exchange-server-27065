using System;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009DE RID: 2526
	internal class TokenBucket : ITokenBucket
	{
		// Token: 0x060074EE RID: 29934 RVA: 0x001818BC File Offset: 0x0017FABC
		public static ITokenBucket Create(ITokenBucket tokenBucket, Unlimited<uint> maxBalance, Unlimited<uint> rechargeRate, Unlimited<uint> minBalance, BudgetKey budgetKey)
		{
			if (rechargeRate == 0U)
			{
				return new FullyThrottledTokenBucket(tokenBucket);
			}
			if (rechargeRate == 2147483647U || rechargeRate.IsUnlimited)
			{
				return new UnthrottledTokenBucket(tokenBucket);
			}
			TokenBucket tokenBucket2 = tokenBucket as TokenBucket;
			if (tokenBucket2 != null && tokenBucket2.BudgetKey == budgetKey)
			{
				tokenBucket2.UpdateSettings(maxBalance, rechargeRate, minBalance);
				return tokenBucket;
			}
			return new TokenBucket(tokenBucket, maxBalance, rechargeRate, minBalance, budgetKey);
		}

		// Token: 0x060074EF RID: 29935 RVA: 0x0018192F File Offset: 0x0017FB2F
		public static ITokenBucket Create(Unlimited<uint> maxBalance, Unlimited<uint> rechargeRate, Unlimited<uint> minBalance, BudgetKey budgetKey)
		{
			return TokenBucket.Create(null, maxBalance, rechargeRate, minBalance, budgetKey);
		}

		// Token: 0x060074F0 RID: 29936 RVA: 0x0018193C File Offset: 0x0017FB3C
		private TokenBucket(ITokenBucket oldBucket, Unlimited<uint> maxBalance, Unlimited<uint> rechargeRate, Unlimited<uint> minBalance, BudgetKey budgetKey)
		{
			this.BudgetKey = budgetKey;
			this.LastUpdateUtc = TimeProvider.UtcNow;
			this.UpdateSettings(maxBalance, rechargeRate, minBalance);
			this.balance = (maxBalance.IsUnlimited ? 2147483647U : maxBalance.Value);
			if (oldBucket != null)
			{
				this.PendingCharges = oldBucket.PendingCharges;
			}
		}

		// Token: 0x1700299D RID: 10653
		// (get) Token: 0x060074F1 RID: 29937 RVA: 0x001819A5 File Offset: 0x0017FBA5
		// (set) Token: 0x060074F2 RID: 29938 RVA: 0x001819AD File Offset: 0x0017FBAD
		public BudgetKey BudgetKey { get; private set; }

		// Token: 0x1700299E RID: 10654
		// (get) Token: 0x060074F3 RID: 29939 RVA: 0x001819B6 File Offset: 0x0017FBB6
		// (set) Token: 0x060074F4 RID: 29940 RVA: 0x001819BE File Offset: 0x0017FBBE
		public int PendingCharges { get; private set; }

		// Token: 0x1700299F RID: 10655
		// (get) Token: 0x060074F5 RID: 29941 RVA: 0x001819C8 File Offset: 0x0017FBC8
		public DateTime? LockedUntilUtc
		{
			get
			{
				DateTime? lockedUntilUtcNonUpdating;
				lock (this.instanceLock)
				{
					this.Update(default(TimeSpan));
					lockedUntilUtcNonUpdating = this.LockedUntilUtcNonUpdating;
				}
				return lockedUntilUtcNonUpdating;
			}
		}

		// Token: 0x170029A0 RID: 10656
		// (get) Token: 0x060074F6 RID: 29942 RVA: 0x00181A1C File Offset: 0x0017FC1C
		public DateTime? LockedAt
		{
			get
			{
				DateTime? result;
				lock (this.instanceLock)
				{
					this.Update(default(TimeSpan));
					result = (this.locked ? new DateTime?(this.lockedAt) : null);
				}
				return result;
			}
		}

		// Token: 0x170029A1 RID: 10657
		// (get) Token: 0x060074F7 RID: 29943 RVA: 0x00181A88 File Offset: 0x0017FC88
		private DateTime? LockedUntilUtcNonUpdating
		{
			get
			{
				DateTime? result;
				lock (this.instanceLock)
				{
					if (this.locked)
					{
						DateTime dateTime = this.lockedAt + TokenBucket.MinimumLockoutTime;
						float num = (float)this.MinimumBalance - this.balance;
						DateTime dateTime2 = (num > 0f) ? TimeProvider.UtcNow.AddMilliseconds((double)((float)(3600000 / this.RechargeRate) * num)) : DateTime.MinValue;
						result = new DateTime?((dateTime > dateTime2) ? dateTime : dateTime2);
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		// Token: 0x170029A2 RID: 10658
		// (get) Token: 0x060074F8 RID: 29944 RVA: 0x00181B40 File Offset: 0x0017FD40
		public bool Locked
		{
			get
			{
				bool result;
				lock (this.instanceLock)
				{
					this.Update(default(TimeSpan));
					result = this.locked;
				}
				return result;
			}
		}

		// Token: 0x170029A3 RID: 10659
		// (get) Token: 0x060074F9 RID: 29945 RVA: 0x00181B94 File Offset: 0x0017FD94
		// (set) Token: 0x060074FA RID: 29946 RVA: 0x00181B9C File Offset: 0x0017FD9C
		public int MaximumBalance { get; private set; }

		// Token: 0x170029A4 RID: 10660
		// (get) Token: 0x060074FB RID: 29947 RVA: 0x00181BA5 File Offset: 0x0017FDA5
		// (set) Token: 0x060074FC RID: 29948 RVA: 0x00181BAD File Offset: 0x0017FDAD
		public int MinimumBalance { get; private set; }

		// Token: 0x170029A5 RID: 10661
		// (get) Token: 0x060074FD RID: 29949 RVA: 0x00181BB6 File Offset: 0x0017FDB6
		// (set) Token: 0x060074FE RID: 29950 RVA: 0x00181BBE File Offset: 0x0017FDBE
		public int RechargeRate { get; private set; }

		// Token: 0x060074FF RID: 29951 RVA: 0x00181BC8 File Offset: 0x0017FDC8
		public float GetBalance()
		{
			float result;
			lock (this.instanceLock)
			{
				this.Update(default(TimeSpan));
				result = this.balance;
			}
			return result;
		}

		// Token: 0x170029A6 RID: 10662
		// (get) Token: 0x06007500 RID: 29952 RVA: 0x00181C1C File Offset: 0x0017FE1C
		// (set) Token: 0x06007501 RID: 29953 RVA: 0x00181C24 File Offset: 0x0017FE24
		public DateTime LastUpdateUtc { get; private set; }

		// Token: 0x06007502 RID: 29954 RVA: 0x00181C30 File Offset: 0x0017FE30
		public void Increment()
		{
			lock (this.instanceLock)
			{
				this.Update(default(TimeSpan));
				this.PendingCharges++;
			}
		}

		// Token: 0x06007503 RID: 29955 RVA: 0x00181C88 File Offset: 0x0017FE88
		public void Decrement(TimeSpan extraDuration = default(TimeSpan), bool reverseBudgetCharge = false)
		{
			lock (this.instanceLock)
			{
				if (reverseBudgetCharge)
				{
					this.Update(-extraDuration);
				}
				else
				{
					this.Update(extraDuration);
				}
				this.PendingCharges--;
			}
		}

		// Token: 0x06007504 RID: 29956 RVA: 0x00181CE8 File Offset: 0x0017FEE8
		private void UpdateSettings(Unlimited<uint> maxBalance, Unlimited<uint> rechargeRate, Unlimited<uint> minBalance)
		{
			if (rechargeRate == 0U)
			{
				throw new ArgumentOutOfRangeException("rechargeRate", rechargeRate, "rechargeRate must be greater than zero.");
			}
			lock (this.instanceLock)
			{
				this.Update(default(TimeSpan));
				this.MaximumBalance = (int)(maxBalance.IsUnlimited ? 2147483647U : maxBalance.Value);
				this.MinimumBalance = (int)(minBalance.IsUnlimited ? 2147483648U : (uint.MaxValue * minBalance.Value));
				this.RechargeRate = (int)(rechargeRate.IsUnlimited ? 2147483647U : rechargeRate.Value);
				this.rechargeRateMsec = (double)this.RechargeRate / 3600000.0;
				if (!minBalance.IsUnlimited && this.balance < (float)this.MinimumBalance)
				{
					this.LockBucket();
				}
				else if (this.balance > (float)this.MaximumBalance)
				{
					this.balance = (float)this.MaximumBalance;
				}
				if (this.locked && this.balance > (float)this.MinimumBalance)
				{
					this.UnlockBucket();
				}
			}
		}

		// Token: 0x06007505 RID: 29957 RVA: 0x00181E20 File Offset: 0x00180020
		internal void SetBalanceForTest(float newBalance)
		{
			lock (this.instanceLock)
			{
				this.locked = false;
				this.balance = newBalance;
				this.LastUpdateUtc = TimeProvider.UtcNow;
				this.Update(default(TimeSpan));
			}
		}

		// Token: 0x06007506 RID: 29958 RVA: 0x00181E84 File Offset: 0x00180084
		private void Update(TimeSpan extraDuration = default(TimeSpan))
		{
			DateTime utcNow = TimeProvider.UtcNow;
			TimeSpan t = utcNow - this.LastUpdateUtc;
			if (t <= TimeSpan.Zero)
			{
				return;
			}
			this.LastUpdateUtc = utcNow;
			double num = this.rechargeRateMsec - (double)this.PendingCharges;
			this.balance += (float)(num * t.TotalMilliseconds - extraDuration.TotalMilliseconds);
			if (this.balance > (float)this.MaximumBalance)
			{
				this.balance = (float)this.MaximumBalance;
			}
			if (this.MinimumBalance != -2147483648 && this.balance < (float)this.MinimumBalance && !this.locked)
			{
				this.LockBucket();
				return;
			}
			if (this.locked && this.LockedUntilUtcNonUpdating <= TimeProvider.UtcNow)
			{
				this.UnlockBucket();
			}
		}

		// Token: 0x06007507 RID: 29959 RVA: 0x00181F68 File Offset: 0x00180168
		private void UnlockBucket()
		{
			lock (this.instanceLock)
			{
				this.locked = false;
				this.lockedAt = DateTime.MinValue;
			}
			ExTraceGlobals.ClientThrottlingTracer.TraceDebug((long)this.GetHashCode(), "[TokenBucket.UnlockBucket] Bucket is now unlocked. Resetting state.");
			if (Globals.ProcessInstanceType != InstanceType.NotInitialized)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_UserNoLongerLockedOutThrottling, string.Empty, new object[]
				{
					this.BudgetKey
				});
			}
		}

		// Token: 0x06007508 RID: 29960 RVA: 0x00181FF4 File Offset: 0x001801F4
		private void LockBucket()
		{
			lock (this.instanceLock)
			{
				this.locked = true;
				this.lockedAt = TimeProvider.UtcNow;
			}
			DateTime value = this.LockedUntilUtcNonUpdating.Value;
			ThrottlingPerfCounterWrapper.IncrementBudgetsLockedOut(this.BudgetKey, value - TimeProvider.UtcNow);
			if (Globals.ProcessInstanceType != InstanceType.NotInitialized)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_UserLockedOutThrottling, string.Empty, new object[]
				{
					this.BudgetKey,
					value,
					this.GetTraceInt(this.MaximumBalance),
					this.GetTraceInt(this.RechargeRate),
					this.GetTraceInt(this.MinimumBalance)
				});
			}
			ExTraceGlobals.ClientThrottlingTracer.TraceDebug<DateTime, int>((long)this.GetHashCode(), "[TokenBucket.LockBucket] Bucket locked until {0}.  Current Pending charges: {1}", value, this.PendingCharges);
		}

		// Token: 0x06007509 RID: 29961 RVA: 0x001820E8 File Offset: 0x001802E8
		private string GetTraceInt(int valueToTrace)
		{
			if (valueToTrace != 2147483647)
			{
				return valueToTrace.ToString();
			}
			return "$null";
		}

		// Token: 0x04004B51 RID: 19281
		private const int MsecPerHour = 3600000;

		// Token: 0x04004B52 RID: 19282
		public static readonly TimeSpan MinimumLockoutTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x04004B53 RID: 19283
		private double rechargeRateMsec;

		// Token: 0x04004B54 RID: 19284
		private float balance;

		// Token: 0x04004B55 RID: 19285
		private bool locked;

		// Token: 0x04004B56 RID: 19286
		private DateTime lockedAt;

		// Token: 0x04004B57 RID: 19287
		private object instanceLock = new object();
	}
}
