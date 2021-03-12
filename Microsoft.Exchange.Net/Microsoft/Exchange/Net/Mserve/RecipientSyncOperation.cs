using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020008A8 RID: 2216
	internal sealed class RecipientSyncOperation
	{
		// Token: 0x06002F77 RID: 12151 RVA: 0x0006BCB8 File Offset: 0x00069EB8
		public RecipientSyncOperation(string distinguishedName, int partnerId, RecipientSyncState recipientSyncState, bool suppressSyncStateUpdate)
		{
			this.distinguishedName = distinguishedName;
			this.partnerId = partnerId;
			this.suppressSyncStateUpdate = suppressSyncStateUpdate;
			this.recipientSyncState = recipientSyncState;
			this.retryableEntries.Add(OperationType.Add, new List<FailedAddress>());
			this.retryableEntries.Add(OperationType.Delete, new List<FailedAddress>());
			this.retryableEntries.Add(OperationType.Read, new List<FailedAddress>());
			this.nonRetryableEntries.Add(OperationType.Add, new List<FailedAddress>());
			this.nonRetryableEntries.Add(OperationType.Delete, new List<FailedAddress>());
			this.nonRetryableEntries.Add(OperationType.Read, new List<FailedAddress>());
			this.pendingSyncStateCommitEntries.Add(OperationType.Add, new List<string>());
			this.pendingSyncStateCommitEntries.Add(OperationType.Delete, new List<string>());
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x0006BDCD File Offset: 0x00069FCD
		public RecipientSyncOperation() : this(null, -1, null, true)
		{
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x0006BDD9 File Offset: 0x00069FD9
		public List<string> ReadEntries
		{
			get
			{
				return this.readEntries;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x0006BDE1 File Offset: 0x00069FE1
		public List<string> AddedEntries
		{
			get
			{
				return this.addedEntries;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x0006BDE9 File Offset: 0x00069FE9
		public List<string> RemovedEntries
		{
			get
			{
				return this.removedEntries;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x0006BDF1 File Offset: 0x00069FF1
		public Dictionary<OperationType, List<string>> PendingSyncStateCommitEntries
		{
			get
			{
				return this.pendingSyncStateCommitEntries;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x0006BDFC File Offset: 0x00069FFC
		public int TotalPendingSyncStateCommitEntries
		{
			get
			{
				int num = 0;
				foreach (List<string> list in this.pendingSyncStateCommitEntries.Values)
				{
					num += list.Count;
				}
				return num;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x0006BE5C File Offset: 0x0006A05C
		public Dictionary<OperationType, List<FailedAddress>> RetryableEntries
		{
			get
			{
				return this.retryableEntries;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x0006BE64 File Offset: 0x0006A064
		public Dictionary<OperationType, List<FailedAddress>> NonRetryableEntries
		{
			get
			{
				return this.nonRetryableEntries;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x0006BE6C File Offset: 0x0006A06C
		public Dictionary<string, string> AddressTypeTable
		{
			get
			{
				return this.addressTypeTable;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x0006BE74 File Offset: 0x0006A074
		public List<string> DuplicatedAddEntries
		{
			get
			{
				return this.duplicatedAddEntries;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x0006BE7C File Offset: 0x0006A07C
		// (set) Token: 0x06002F83 RID: 12163 RVA: 0x0006BE84 File Offset: 0x0006A084
		public int PartnerId
		{
			get
			{
				return this.partnerId;
			}
			set
			{
				this.partnerId = value;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x0006BE8D File Offset: 0x0006A08D
		public string DistinguishedName
		{
			get
			{
				return this.distinguishedName;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x0006BE95 File Offset: 0x0006A095
		public RecipientSyncState RecipientSyncState
		{
			get
			{
				return this.recipientSyncState;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x0006BE9D File Offset: 0x0006A09D
		public bool SuppressSyncStateUpdate
		{
			get
			{
				return this.suppressSyncStateUpdate;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x0006BEA5 File Offset: 0x0006A0A5
		public bool Synchronized
		{
			get
			{
				return this.completedSyncCount == this.addedEntries.Count + this.removedEntries.Count + this.readEntries.Count;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x0006BED2 File Offset: 0x0006A0D2
		// (set) Token: 0x06002F89 RID: 12169 RVA: 0x0006BEDA File Offset: 0x0006A0DA
		public int CompletedSyncCount
		{
			get
			{
				return this.completedSyncCount;
			}
			set
			{
				this.completedSyncCount = value;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x0006BEE3 File Offset: 0x0006A0E3
		public bool HasRetryableErrors
		{
			get
			{
				return this.retryableEntries[OperationType.Add].Count > 0 || this.retryableEntries[OperationType.Delete].Count > 0 || this.retryableEntries[OperationType.Read].Count > 0;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x0006BF23 File Offset: 0x0006A123
		public bool HasNonRetryableErrors
		{
			get
			{
				return this.nonRetryableEntries[OperationType.Add].Count > 0 || this.nonRetryableEntries[OperationType.Delete].Count > 0 || this.nonRetryableEntries[OperationType.Read].Count > 0;
			}
		}

		// Token: 0x04002924 RID: 10532
		private readonly List<string> addedEntries = new List<string>();

		// Token: 0x04002925 RID: 10533
		private readonly List<string> removedEntries = new List<string>();

		// Token: 0x04002926 RID: 10534
		private readonly List<string> readEntries = new List<string>();

		// Token: 0x04002927 RID: 10535
		private readonly Dictionary<OperationType, List<FailedAddress>> retryableEntries = new Dictionary<OperationType, List<FailedAddress>>();

		// Token: 0x04002928 RID: 10536
		private readonly Dictionary<OperationType, List<FailedAddress>> nonRetryableEntries = new Dictionary<OperationType, List<FailedAddress>>();

		// Token: 0x04002929 RID: 10537
		private readonly Dictionary<OperationType, List<string>> pendingSyncStateCommitEntries = new Dictionary<OperationType, List<string>>();

		// Token: 0x0400292A RID: 10538
		private readonly Dictionary<string, string> addressTypeTable = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400292B RID: 10539
		private readonly List<string> duplicatedAddEntries = new List<string>();

		// Token: 0x0400292C RID: 10540
		private readonly string distinguishedName;

		// Token: 0x0400292D RID: 10541
		private readonly RecipientSyncState recipientSyncState;

		// Token: 0x0400292E RID: 10542
		private readonly bool suppressSyncStateUpdate;

		// Token: 0x0400292F RID: 10543
		private int completedSyncCount;

		// Token: 0x04002930 RID: 10544
		private int partnerId;
	}
}
