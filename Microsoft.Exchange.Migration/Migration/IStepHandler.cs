using System;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000033 RID: 51
	internal interface IStepHandler
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001F9 RID: 505
		bool ExpectMailboxData { get; }

		// Token: 0x060001FA RID: 506
		IStepSettings Discover(MigrationJobItem jobItem, MailboxData localMailbox);

		// Token: 0x060001FB RID: 507
		void Validate(MigrationJobItem jobItem);

		// Token: 0x060001FC RID: 508
		IStepSnapshot Inject(MigrationJobItem jobItem);

		// Token: 0x060001FD RID: 509
		IStepSnapshot Process(ISnapshotId id, MigrationJobItem jobItem, out bool updated);

		// Token: 0x060001FE RID: 510
		void Start(ISnapshotId id);

		// Token: 0x060001FF RID: 511
		IStepSnapshot Stop(ISnapshotId id);

		// Token: 0x06000200 RID: 512
		void Delete(ISnapshotId id);

		// Token: 0x06000201 RID: 513
		bool CanProcess(MigrationJobItem jobItem);

		// Token: 0x06000202 RID: 514
		MigrationUserStatus ResolvePresentationStatus(MigrationFlags flags, IStepSnapshot stepSnapshot = null);
	}
}
