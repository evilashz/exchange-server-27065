using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000580 RID: 1408
	internal class ActiveSyncExecutionContext
	{
		// Token: 0x0600317E RID: 12670 RVA: 0x000C97FD File Offset: 0x000C79FD
		public ActiveSyncExecutionContext(TestCasConnectivity.TestCasConnectivityRunInstance instance, string mailboxFqdn)
		{
			this.instance = instance;
			this.mailboxFqdn = mailboxFqdn;
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000C9836 File Offset: 0x000C7A36
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x000C983E File Offset: 0x000C7A3E
		public TimeSpan PingLatency
		{
			get
			{
				return this.pingLatency;
			}
			set
			{
				this.pingLatency = value;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000C9847 File Offset: 0x000C7A47
		// (set) Token: 0x06003182 RID: 12674 RVA: 0x000C984F File Offset: 0x000C7A4F
		public bool VerifyItemCameDown
		{
			get
			{
				return this.verifyItemCameDown;
			}
			set
			{
				this.verifyItemCameDown = value;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000C9858 File Offset: 0x000C7A58
		// (set) Token: 0x06003184 RID: 12676 RVA: 0x000C9860 File Offset: 0x000C7A60
		public ActiveSyncExecutionState NextStateToExecute
		{
			get
			{
				return this.nextStateToRun;
			}
			set
			{
				this.nextStateToRun = value;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000C9869 File Offset: 0x000C7A69
		// (set) Token: 0x06003186 RID: 12678 RVA: 0x000C9871 File Offset: 0x000C7A71
		public TimeSpan SyncAfterPingLatency
		{
			get
			{
				return this.syncAfterPingLatency;
			}
			set
			{
				this.syncAfterPingLatency = value;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000C987A File Offset: 0x000C7A7A
		public TestCasConnectivity.TestCasConnectivityRunInstance Instance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000C9882 File Offset: 0x000C7A82
		public string MailboxFqdn
		{
			get
			{
				return this.mailboxFqdn;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000C988A File Offset: 0x000C7A8A
		// (set) Token: 0x0600318A RID: 12682 RVA: 0x000C9892 File Offset: 0x000C7A92
		public ActiveSyncState CurrentState
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x000C989B File Offset: 0x000C7A9B
		// (set) Token: 0x0600318C RID: 12684 RVA: 0x000C98A3 File Offset: 0x000C7AA3
		public string CollectionId
		{
			get
			{
				return this.collectionId;
			}
			set
			{
				this.collectionId = value;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000C98AC File Offset: 0x000C7AAC
		// (set) Token: 0x0600318E RID: 12686 RVA: 0x000C98B4 File Offset: 0x000C7AB4
		public string SyncState
		{
			get
			{
				return this.syncState;
			}
			set
			{
				this.syncState = value;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000C98BD File Offset: 0x000C7ABD
		// (set) Token: 0x06003190 RID: 12688 RVA: 0x000C98C5 File Offset: 0x000C7AC5
		public int Estimate
		{
			get
			{
				return this.estimate;
			}
			set
			{
				this.estimate = value;
			}
		}

		// Token: 0x0400231D RID: 8989
		private TestCasConnectivity.TestCasConnectivityRunInstance instance;

		// Token: 0x0400231E RID: 8990
		private ActiveSyncState state;

		// Token: 0x0400231F RID: 8991
		private string collectionId;

		// Token: 0x04002320 RID: 8992
		private readonly string mailboxFqdn;

		// Token: 0x04002321 RID: 8993
		private string syncState = "0";

		// Token: 0x04002322 RID: 8994
		private int estimate;

		// Token: 0x04002323 RID: 8995
		private bool verifyItemCameDown;

		// Token: 0x04002324 RID: 8996
		private ActiveSyncExecutionState nextStateToRun;

		// Token: 0x04002325 RID: 8997
		private TimeSpan pingLatency = default(TimeSpan);

		// Token: 0x04002326 RID: 8998
		private TimeSpan syncAfterPingLatency = default(TimeSpan);
	}
}
