using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000101 RID: 257
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationJobItemProcessorResponse : MigrationProcessorResponse
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x00037B48 File Offset: 0x00035D48
		protected MigrationJobItemProcessorResponse(MigrationProcessorResult result, LocalizedException error = null, MailboxData mailboxData = null, IStepSnapshot stepSnapshot = null, IStepSettings stepSettings = null, bool updated = false, MigrationCountCache.MigrationStatusChange statusChange = null) : base(result, error)
		{
			this.MailboxData = mailboxData;
			this.Snapshot = stepSnapshot;
			this.Settings = stepSettings;
			this.Updated = updated;
			this.StatusChange = statusChange;
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00037B79 File Offset: 0x00035D79
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x00037B81 File Offset: 0x00035D81
		public MailboxData MailboxData { get; private set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00037B8A File Offset: 0x00035D8A
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x00037B92 File Offset: 0x00035D92
		public IStepSnapshot Snapshot { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00037B9B File Offset: 0x00035D9B
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x00037BA3 File Offset: 0x00035DA3
		public IStepSettings Settings { get; private set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00037BAC File Offset: 0x00035DAC
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x00037BB4 File Offset: 0x00035DB4
		public bool Updated { get; private set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00037BBD File Offset: 0x00035DBD
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x00037BC5 File Offset: 0x00035DC5
		public MigrationCountCache.MigrationStatusChange StatusChange { get; internal set; }

		// Token: 0x06000D95 RID: 3477 RVA: 0x00037BD0 File Offset: 0x00035DD0
		public static MigrationJobItemProcessorResponse Create(MigrationProcessorResult result, TimeSpan? delayTime = null, LocalizedException error = null, MailboxData mailboxData = null, IStepSnapshot stepSnapshot = null, IStepSettings stepSettings = null, bool updated = false, MigrationCountCache.MigrationStatusChange statusChange = null)
		{
			MigrationJobItemProcessorResponse migrationJobItemProcessorResponse = new MigrationJobItemProcessorResponse(result, error, mailboxData, stepSnapshot, stepSettings, updated, statusChange);
			if (delayTime != null)
			{
				migrationJobItemProcessorResponse.DelayTime = delayTime;
			}
			return migrationJobItemProcessorResponse;
		}
	}
}
