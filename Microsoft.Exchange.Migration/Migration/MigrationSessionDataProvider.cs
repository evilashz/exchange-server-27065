using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000080 RID: 128
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationSessionDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00021ABE File Offset: 0x0001FCBE
		private MigrationSessionDataProvider(MigrationDataProvider dataProvider) : base(dataProvider.MailboxSession)
		{
			this.dataProvider = dataProvider;
			this.MigrationSession = MigrationSession.Get(this.dataProvider);
			this.diagnosticEnabled = false;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00021AEB File Offset: 0x0001FCEB
		public IMigrationDataProvider MailboxProvider
		{
			get
			{
				return this.dataProvider;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00021AF3 File Offset: 0x0001FCF3
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x00021AFB File Offset: 0x0001FCFB
		public MigrationSession MigrationSession { get; private set; }

		// Token: 0x06000755 RID: 1877 RVA: 0x00021B04 File Offset: 0x0001FD04
		public static MigrationSessionDataProvider CreateDataProvider(string action, IRecipientSession recipientSession, ADUser partitionMailbox)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(action, "action");
			MigrationUtil.ThrowOnNullArgument(recipientSession, "recipientSession");
			MigrationSessionDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MigrationDataProvider disposable = MigrationDataProvider.CreateProviderForMigrationMailbox(action, recipientSession, partitionMailbox);
				disposeGuard.Add<MigrationDataProvider>(disposable);
				MigrationSessionDataProvider migrationSessionDataProvider = new MigrationSessionDataProvider(disposable);
				disposeGuard.Success();
				result = migrationSessionDataProvider;
			}
			return result;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00021B74 File Offset: 0x0001FD74
		public static bool IsKnownException(Exception exception)
		{
			return exception is StorageTransientException || exception is StoragePermanentException || exception is MigrationTransientException || exception is MigrationPermanentException || exception is MigrationDataCorruptionException || exception is DiagnosticArgumentException;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00021BA9 File Offset: 0x0001FDA9
		public void EnableDiagnostics(string argument)
		{
			this.diagnosticEnabled = true;
			this.diagnosticArgument = new MigrationDiagnosticArgument(argument);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00021DA4 File Offset: 0x0001FFA4
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			Type pagedType = typeof(T);
			if (pagedType == typeof(MigrationStatistics))
			{
				MigrationStatistics migrationStats = this.MigrationSession.GetMigrationStatistics(this.dataProvider);
				if (this.diagnosticEnabled)
				{
					XElement diagnosticInfo = this.MigrationSession.GetDiagnosticInfo(this.dataProvider, this.diagnosticArgument);
					if (diagnosticInfo != null)
					{
						migrationStats.DiagnosticInfo = diagnosticInfo.ToString();
					}
				}
				yield return (T)((object)migrationStats);
			}
			else
			{
				if (!(pagedType == typeof(MigrationConfig)))
				{
					throw new ArgumentException("Unknown type: " + pagedType, "pagedType");
				}
				yield return (T)((object)this.MigrationSession.GetMigrationConfig(this.dataProvider));
			}
			yield break;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00021DC4 File Offset: 0x0001FFC4
		protected override void InternalSave(ConfigurableObject instance)
		{
			switch (instance.ObjectState)
			{
			default:
				return;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00021DF0 File Offset: 0x0001FFF0
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

		// Token: 0x0600075B RID: 1883 RVA: 0x00021E34 File Offset: 0x00020034
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationSessionDataProvider>(this);
		}

		// Token: 0x04000314 RID: 788
		private MigrationDataProvider dataProvider;

		// Token: 0x04000315 RID: 789
		private bool diagnosticEnabled;

		// Token: 0x04000316 RID: 790
		private MigrationDiagnosticArgument diagnosticArgument;
	}
}
