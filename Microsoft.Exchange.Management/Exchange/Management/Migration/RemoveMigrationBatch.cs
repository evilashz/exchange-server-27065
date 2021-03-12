using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004E8 RID: 1256
	[Cmdlet("Remove", "MigrationBatch", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMigrationBatch : MigrationObjectTaskBase<MigrationBatchIdParameter>
	{
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000B1363 File Offset: 0x000AF563
		public override string Action
		{
			get
			{
				return "RemoveMigrationBatch";
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000B136C File Offset: 0x000AF56C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (!this.Force && this.DataObject != null && this.DataObject.PendingCount > 0)
				{
					return Strings.ConfirmationMessageRemoveMigrationBatchWithPendingItems(this.DataObject.Identity.ToString(), this.DataObject.PendingCount);
				}
				if (this.DataObject != null)
				{
					return Strings.ConfirmationMessageRemoveMigrationBatch(this.DataObject.Identity.ToString());
				}
				return Strings.ConfirmationMessageRemoveMigrationBatch(this.Identity.ToString());
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000B13EB File Offset: 0x000AF5EB
		// (set) Token: 0x06002C4D RID: 11341 RVA: 0x000B1411 File Offset: 0x000AF611
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000B142C File Offset: 0x000AF62C
		protected override void InternalValidate()
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			migrationBatchDataProvider.MigrationJob = base.GetAndValidateMigrationJob(false);
			if (!this.Force)
			{
				LocalizedString? localizedString;
				bool flag;
				if (migrationBatchDataProvider.MigrationJob.IsPAW)
				{
					flag = migrationBatchDataProvider.MigrationJob.SupportsFlag(MigrationFlags.Remove, out localizedString);
				}
				else
				{
					flag = migrationBatchDataProvider.MigrationJob.SupportsRemoving(out localizedString);
				}
				if (!flag)
				{
					if (localizedString == null)
					{
						localizedString = new LocalizedString?(Strings.MigrationJobCannotBeRemoved);
					}
					base.WriteError(new LocalizedException(localizedString.Value));
					migrationBatchDataProvider.MigrationJob = null;
				}
			}
			if (migrationBatchDataProvider.MigrationJob == null)
			{
				MigrationObjectTaskBase<MigrationBatchIdParameter>.WriteJobNotFoundError(this, this.Identity.RawIdentity);
			}
			base.InternalValidate();
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000B1570 File Offset: 0x000AF770
		protected override void InternalProcessRecord()
		{
			MigrationBatchDataProvider batchProvider = (MigrationBatchDataProvider)base.DataSession;
			try
			{
				MigrationHelper.RunUpdateOperation(delegate
				{
					if (this.Force)
					{
						batchProvider.MigrationJob.Delete(batchProvider.MailboxProvider, true);
						return;
					}
					if (batchProvider.MigrationJob.IsPAW)
					{
						batchProvider.MigrationJob.SetMigrationFlags(batchProvider.MailboxProvider, MigrationFlags.Remove);
						return;
					}
					batchProvider.MigrationJob.RemoveJob(batchProvider.MailboxProvider);
				});
			}
			catch (ObjectNotFoundException ex)
			{
				MigrationObjectTaskBase<MigrationBatchIdParameter>.WriteJobNotFoundError(this, this.Identity.RawIdentity, ex);
			}
			batchProvider.MigrationJob.ReportData.Append(Strings.MigrationReportJobRemoved(base.ExecutingUserIdentityName));
			MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, batchProvider.MailboxSession, base.CurrentOrganizationId, false, false);
			base.InternalProcessRecord();
		}
	}
}
