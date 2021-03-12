using System;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	internal sealed class AmDeferredRecoveryEntry
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x000024B8 File Offset: 0x000018B8
		public AmDeferredRecoveryEntry(string serverFqdn, string recoveryDueTimeInUtc)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x000024DC File Offset: 0x000018DC
		public string ServerFqdn
		{
			get
			{
				return this.m_serverFqdn;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000024F0 File Offset: 0x000018F0
		public string RecoveryDueTimeInUtc
		{
			get
			{
				return this.m_recoveryDueTimeInUtc;
			}
		}

		// Token: 0x04000968 RID: 2408
		private readonly string m_serverFqdn = serverFqdn;

		// Token: 0x04000969 RID: 2409
		private readonly string m_recoveryDueTimeInUtc = recoveryDueTimeInUtc;
	}
}
