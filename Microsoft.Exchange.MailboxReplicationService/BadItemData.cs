using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000079 RID: 121
	internal class BadItemData
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x0001EAE1 File Offset: 0x0001CCE1
		internal BadItemData(Guid requestGuid, BadMessageRec badMessage)
		{
			this.RequestGuid = requestGuid;
			this.badMessage = badMessage;
			this.CallStackHash = ((badMessage.RawFailure != null) ? CommonUtils.ComputeCallStackHash(badMessage.RawFailure, 5) : string.Empty);
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001EB18 File Offset: 0x0001CD18
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001EB20 File Offset: 0x0001CD20
		public Guid RequestGuid { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001EB29 File Offset: 0x0001CD29
		public BadItemKind Kind
		{
			get
			{
				return this.badMessage.Kind;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001EB36 File Offset: 0x0001CD36
		public byte[] EntryId
		{
			get
			{
				return this.badMessage.EntryId;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001EB43 File Offset: 0x0001CD43
		public byte[] FolderId
		{
			get
			{
				return this.badMessage.FolderId;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001EB50 File Offset: 0x0001CD50
		public string FolderName
		{
			get
			{
				return this.badMessage.FolderName;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001EB5D File Offset: 0x0001CD5D
		public WellKnownFolderType WKFType
		{
			get
			{
				return this.badMessage.WellKnownFolderType;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001EB6A File Offset: 0x0001CD6A
		public string MessageClass
		{
			get
			{
				return this.badMessage.MessageClass;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001EB77 File Offset: 0x0001CD77
		public int? MessageSize
		{
			get
			{
				return this.badMessage.MessageSize;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001EB84 File Offset: 0x0001CD84
		public DateTime? DateSent
		{
			get
			{
				return this.badMessage.DateSent;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001EB91 File Offset: 0x0001CD91
		public DateTime? DateReceived
		{
			get
			{
				return this.badMessage.DateReceived;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001EB9E File Offset: 0x0001CD9E
		public Exception FailureMessage
		{
			get
			{
				return this.badMessage.RawFailure;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001EBAB File Offset: 0x0001CDAB
		public string Category
		{
			get
			{
				return this.badMessage.Category;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001EBB8 File Offset: 0x0001CDB8
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0001EBC0 File Offset: 0x0001CDC0
		public string CallStackHash { get; private set; }

		// Token: 0x04000222 RID: 546
		private BadMessageRec badMessage;
	}
}
