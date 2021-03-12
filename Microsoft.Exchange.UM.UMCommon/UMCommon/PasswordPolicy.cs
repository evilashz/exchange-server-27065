using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000DF RID: 223
	internal class PasswordPolicy
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x0001CF20 File Offset: 0x0001B120
		internal PasswordPolicy(UMMailboxPolicy policy)
		{
			this.minimumLength = policy.MinPINLength;
			this.daysBeforeExpiry = (policy.PINLifetime.IsUnlimited ? 0 : Convert.ToInt32(policy.PINLifetime.Value.TotalDays));
			this.logonFailuresBeforeLockout = (policy.MaxLogonAttempts.IsUnlimited ? 0 : policy.MaxLogonAttempts.Value);
			this.previousPasswordsDisallowed = policy.PINHistoryCount;
			this.allowCommonPatterns = policy.AllowCommonPatterns;
			this.logonFailuresBeforePINReset = (policy.LogonFailuresBeforePINReset.IsUnlimited ? 0 : policy.LogonFailuresBeforePINReset.Value);
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001CFE4 File Offset: 0x0001B1E4
		internal int MinimumLength
		{
			get
			{
				return this.minimumLength;
			}
			set
			{
				this.minimumLength = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001CFED File Offset: 0x0001B1ED
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0001CFF5 File Offset: 0x0001B1F5
		internal int DaysBeforeExpiry
		{
			get
			{
				return this.daysBeforeExpiry;
			}
			set
			{
				this.daysBeforeExpiry = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001CFFE File Offset: 0x0001B1FE
		internal int LogonFailuresBeforePINReset
		{
			get
			{
				return this.logonFailuresBeforePINReset;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001D006 File Offset: 0x0001B206
		internal int LogonFailuresBeforeLockout
		{
			get
			{
				return this.logonFailuresBeforeLockout;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001D00E File Offset: 0x0001B20E
		internal int PreviousPasswordsDisallowed
		{
			get
			{
				return this.previousPasswordsDisallowed;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001D016 File Offset: 0x0001B216
		internal bool AllowCommonPatterns
		{
			get
			{
				return this.allowCommonPatterns;
			}
		}

		// Token: 0x04000430 RID: 1072
		private int minimumLength;

		// Token: 0x04000431 RID: 1073
		private int daysBeforeExpiry;

		// Token: 0x04000432 RID: 1074
		private int logonFailuresBeforeLockout;

		// Token: 0x04000433 RID: 1075
		private int logonFailuresBeforePINReset;

		// Token: 0x04000434 RID: 1076
		private int previousPasswordsDisallowed;

		// Token: 0x04000435 RID: 1077
		private bool allowCommonPatterns;
	}
}
