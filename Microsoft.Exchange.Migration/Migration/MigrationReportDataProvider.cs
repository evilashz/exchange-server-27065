using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationReportDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x0001F178 File Offset: 0x0001D378
		private MigrationReportDataProvider(MigrationDataProvider dataProvider) : base(dataProvider.MailboxSession)
		{
			this.dataProvider = dataProvider;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001F18D File Offset: 0x0001D38D
		public IMigrationDataProvider MailboxProvider
		{
			get
			{
				return this.dataProvider;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001F198 File Offset: 0x0001D398
		public static MigrationReportDataProvider CreateDataProvider(string action, IRecipientSession recipientSession, Stream csvStream, int startingRowIndex, int rowCount, ADUser partitionMailbox, bool isTenantAdmin)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(action, "action");
			MigrationUtil.ThrowOnNullArgument(recipientSession, "recipientSession");
			MigrationReportDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MigrationDataProvider disposable = MigrationDataProvider.CreateProviderForReportMailbox(action, recipientSession, partitionMailbox);
				disposeGuard.Add<MigrationDataProvider>(disposable);
				MigrationReportDataProvider migrationReportDataProvider = new MigrationReportDataProvider(disposable);
				migrationReportDataProvider.csvStream = csvStream;
				migrationReportDataProvider.startingRowIndex = startingRowIndex;
				migrationReportDataProvider.rowCount = rowCount;
				migrationReportDataProvider.isTenantAdmin = isTenantAdmin;
				disposeGuard.Success();
				result = migrationReportDataProvider;
			}
			return result;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001F374 File Offset: 0x0001D574
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			MigrationReportItem reportItem = MigrationReportItem.Get(this.MailboxProvider, (MigrationReportId)rootId);
			MigrationReport migrationReport = reportItem.GetMigrationReport(this.MailboxProvider, this.csvStream, this.startingRowIndex, this.rowCount, this.isTenantAdmin);
			yield return (T)((object)migrationReport);
			yield break;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001F398 File Offset: 0x0001D598
		protected override void InternalSave(ConfigurableObject instance)
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001F39C File Offset: 0x0001D59C
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.dataProvider != null)
					{
						this.dataProvider.Dispose();
					}
					this.dataProvider = null;
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationReportDataProvider>(this);
		}

		// Token: 0x040002E3 RID: 739
		private MigrationDataProvider dataProvider;

		// Token: 0x040002E4 RID: 740
		private Stream csvStream;

		// Token: 0x040002E5 RID: 741
		private int startingRowIndex;

		// Token: 0x040002E6 RID: 742
		private int rowCount;

		// Token: 0x040002E7 RID: 743
		private bool isTenantAdmin;
	}
}
