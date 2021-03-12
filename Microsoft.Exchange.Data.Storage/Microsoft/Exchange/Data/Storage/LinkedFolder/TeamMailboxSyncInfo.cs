using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x020009A2 RID: 2466
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TeamMailboxSyncInfo
	{
		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x06005B00 RID: 23296 RVA: 0x0017CD65 File Offset: 0x0017AF65
		// (set) Token: 0x06005B01 RID: 23297 RVA: 0x0017CD6D File Offset: 0x0017AF6D
		public Guid MailboxGuid { get; private set; }

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x06005B02 RID: 23298 RVA: 0x0017CD76 File Offset: 0x0017AF76
		// (set) Token: 0x06005B03 RID: 23299 RVA: 0x0017CD7E File Offset: 0x0017AF7E
		public MailboxSession MailboxSession { get; set; }

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x0017CD87 File Offset: 0x0017AF87
		// (set) Token: 0x06005B05 RID: 23301 RVA: 0x0017CD8F File Offset: 0x0017AF8F
		public string DisplayName { get; private set; }

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x06005B06 RID: 23302 RVA: 0x0017CD98 File Offset: 0x0017AF98
		// (set) Token: 0x06005B07 RID: 23303 RVA: 0x0017CDA0 File Offset: 0x0017AFA0
		public Uri WebCollectionUrl { get; private set; }

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x0017CDA9 File Offset: 0x0017AFA9
		// (set) Token: 0x06005B09 RID: 23305 RVA: 0x0017CDB1 File Offset: 0x0017AFB1
		public Guid WebId { get; private set; }

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x06005B0A RID: 23306 RVA: 0x0017CDBA File Offset: 0x0017AFBA
		// (set) Token: 0x06005B0B RID: 23307 RVA: 0x0017CDC2 File Offset: 0x0017AFC2
		public string SiteUrl { get; private set; }

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06005B0C RID: 23308 RVA: 0x0017CDCB File Offset: 0x0017AFCB
		// (set) Token: 0x06005B0D RID: 23309 RVA: 0x0017CDD5 File Offset: 0x0017AFD5
		public string PendingClientString
		{
			get
			{
				return this.pendingClientString;
			}
			set
			{
				this.pendingClientString = value;
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06005B0E RID: 23310 RVA: 0x0017CDE0 File Offset: 0x0017AFE0
		// (set) Token: 0x06005B0F RID: 23311 RVA: 0x0017CDE8 File Offset: 0x0017AFE8
		public ExDateTime PendingClientRequestTime { get; set; }

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06005B10 RID: 23312 RVA: 0x0017CDF1 File Offset: 0x0017AFF1
		// (set) Token: 0x06005B11 RID: 23313 RVA: 0x0017CDFB File Offset: 0x0017AFFB
		public bool IsPending
		{
			get
			{
				return this.isPending;
			}
			set
			{
				this.isPending = value;
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06005B12 RID: 23314 RVA: 0x0017CE06 File Offset: 0x0017B006
		// (set) Token: 0x06005B13 RID: 23315 RVA: 0x0017CE0E File Offset: 0x0017B00E
		public ExDateTime NextAllowedSyncUtcTime { get; set; }

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06005B14 RID: 23316 RVA: 0x0017CE17 File Offset: 0x0017B017
		// (set) Token: 0x06005B15 RID: 23317 RVA: 0x0017CE1F File Offset: 0x0017B01F
		public ExDateTime LastSyncUtcTime { get; set; }

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06005B16 RID: 23318 RVA: 0x0017CE28 File Offset: 0x0017B028
		// (set) Token: 0x06005B17 RID: 23319 RVA: 0x0017CE30 File Offset: 0x0017B030
		public ExDateTime WhenCreatedUtcTime { get; private set; }

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06005B18 RID: 23320 RVA: 0x0017CE39 File Offset: 0x0017B039
		// (set) Token: 0x06005B19 RID: 23321 RVA: 0x0017CE41 File Offset: 0x0017B041
		public SortedList<ExDateTime, Exception> SyncErrors { get; private set; }

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x0017CE4A File Offset: 0x0017B04A
		// (set) Token: 0x06005B1B RID: 23323 RVA: 0x0017CE52 File Offset: 0x0017B052
		public IResourceMonitor ResourceMonitor { get; private set; }

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x0017CE5B File Offset: 0x0017B05B
		// (set) Token: 0x06005B1D RID: 23325 RVA: 0x0017CE63 File Offset: 0x0017B063
		public ExchangePrincipal MailboxPrincipal { get; private set; }

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06005B1E RID: 23326 RVA: 0x0017CE6C File Offset: 0x0017B06C
		// (set) Token: 0x06005B1F RID: 23327 RVA: 0x0017CE74 File Offset: 0x0017B074
		public UserConfiguration Logger { get; private set; }

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x06005B20 RID: 23328 RVA: 0x0017CE7D File Offset: 0x0017B07D
		// (set) Token: 0x06005B21 RID: 23329 RVA: 0x0017CE85 File Offset: 0x0017B085
		public TeamMailboxLifecycleState LifeCycleState { get; private set; }

		// Token: 0x06005B22 RID: 23330 RVA: 0x0017CE90 File Offset: 0x0017B090
		public TeamMailboxSyncInfo(Guid mailboxGuid, TeamMailboxLifecycleState lifeCycleState, MailboxSession mailboxSession, ExchangePrincipal mailboxPrincipal, string displayName, Uri webCollectionUrl, Guid webId, string siteUrl, IResourceMonitor resourceMonitor, UserConfiguration logger)
		{
			this.MailboxGuid = mailboxGuid;
			this.LifeCycleState = lifeCycleState;
			this.MailboxSession = mailboxSession;
			this.DisplayName = displayName;
			this.WebCollectionUrl = webCollectionUrl;
			this.WebId = webId;
			this.SiteUrl = siteUrl;
			this.NextAllowedSyncUtcTime = ExDateTime.UtcNow;
			this.LastSyncUtcTime = ExDateTime.MinValue;
			this.SyncErrors = new SortedList<ExDateTime, Exception>();
			this.ResourceMonitor = resourceMonitor;
			this.MailboxPrincipal = mailboxPrincipal;
			this.Logger = logger;
			this.WhenCreatedUtcTime = ExDateTime.UtcNow;
		}

		// Token: 0x04003232 RID: 12850
		private volatile bool isPending;

		// Token: 0x04003233 RID: 12851
		private volatile string pendingClientString;
	}
}
