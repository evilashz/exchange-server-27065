using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004EF RID: 1263
	[Cmdlet("Stop", "MigrationBatch", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class StopMigrationBatch : MigrationObjectTaskBase<MigrationBatchIdParameter>
	{
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000B320A File Offset: 0x000B140A
		public override string Action
		{
			get
			{
				return "StopMigrationBatch";
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000B3211 File Offset: 0x000B1411
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.DataObject != null)
				{
					return Strings.ConfirmationMessageStopMigrationBatch(this.DataObject.Identity.ToString());
				}
				return Strings.ConfirmationMessageStopMigrationBatch(this.Identity.ToString());
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000B3244 File Offset: 0x000B1444
		protected override void InternalValidate()
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			migrationBatchDataProvider.MigrationJob = base.GetAndValidateMigrationJob(true);
			LocalizedString? localizedString;
			bool flag;
			if (migrationBatchDataProvider.MigrationJob.IsPAW)
			{
				flag = migrationBatchDataProvider.MigrationJob.SupportsFlag(MigrationFlags.Stop, out localizedString);
			}
			else
			{
				flag = migrationBatchDataProvider.MigrationJob.SupportsStopping(out localizedString);
			}
			if (!flag)
			{
				if (localizedString == null)
				{
					localizedString = new LocalizedString?(Strings.MigrationJobCannotBeStopped);
				}
				base.WriteError(new MigrationPermanentException(localizedString.Value));
				migrationBatchDataProvider.MigrationJob = null;
			}
			if (migrationBatchDataProvider.MigrationJob == null)
			{
				MigrationObjectTaskBase<MigrationBatchIdParameter>.WriteJobNotFoundError(this, this.Identity.RawIdentity);
			}
			base.InternalValidate();
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000B3324 File Offset: 0x000B1524
		protected override void InternalProcessRecord()
		{
			MigrationBatchDataProvider batchProvider = (MigrationBatchDataProvider)base.DataSession;
			if (batchProvider.MigrationJob.IsPAW)
			{
				batchProvider.MigrationJob.SetMigrationFlags(batchProvider.MailboxProvider, MigrationFlags.Stop);
			}
			else
			{
				MigrationHelper.RunUpdateOperation(delegate
				{
					batchProvider.MigrationJob.StopJob(batchProvider.MailboxProvider, batchProvider.MigrationSession.Config, JobCancellationStatus.CancelledByUserRequest);
				});
			}
			MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, batchProvider.MailboxSession, base.CurrentOrganizationId, false, false);
			base.InternalProcessRecord();
		}
	}
}
