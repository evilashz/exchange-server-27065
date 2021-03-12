using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004EE RID: 1262
	[Cmdlet("Start", "MigrationBatch", SupportsShouldProcess = true)]
	public sealed class StartMigrationBatch : MigrationObjectTaskBase<MigrationBatchIdParameter>
	{
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000B3005 File Offset: 0x000B1205
		public override string Action
		{
			get
			{
				return "StartMigrationBatch";
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000B300C File Offset: 0x000B120C
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x000B3032 File Offset: 0x000B1232
		[Parameter(Mandatory = false)]
		public new SwitchParameter Validate
		{
			get
			{
				return (SwitchParameter)(base.Fields["Validate"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Validate"] = value;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x000B304A File Offset: 0x000B124A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.DataObject != null)
				{
					return Strings.ConfirmationMessageStartMigrationBatch(this.DataObject.Identity.ToString());
				}
				return Strings.ConfirmationMessageStartMigrationBatch(this.Identity.ToString());
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000B307C File Offset: 0x000B127C
		protected override void InternalValidate()
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			migrationBatchDataProvider.MigrationJob = base.GetAndValidateMigrationJob(true);
			bool isPAW = migrationBatchDataProvider.MigrationJob.IsPAW;
			if (migrationBatchDataProvider.MigrationJob == null)
			{
				MigrationObjectTaskBase<MigrationBatchIdParameter>.WriteJobNotFoundError(this, this.Identity.RawIdentity);
			}
			LocalizedString? localizedString;
			bool flag;
			if (isPAW)
			{
				flag = migrationBatchDataProvider.MigrationJob.SupportsFlag(MigrationFlags.Start, out localizedString);
			}
			else
			{
				flag = migrationBatchDataProvider.MigrationJob.SupportsStarting(out localizedString);
			}
			if (!flag)
			{
				if (localizedString == null)
				{
					localizedString = new LocalizedString?(Strings.MigrationOperationFailed);
				}
				base.WriteError(new MigrationPermanentException(localizedString.Value));
				migrationBatchDataProvider.MigrationJob = null;
			}
			if (this.Validate && (isPAW || (migrationBatchDataProvider.MigrationJob.MigrationType != MigrationType.ExchangeLocalMove && migrationBatchDataProvider.MigrationJob.MigrationType != MigrationType.ExchangeRemoteMove)))
			{
				base.WriteError(new ValidateNotSupportedException());
			}
			base.InternalValidate();
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000B3160 File Offset: 0x000B1360
		protected override void InternalProcessRecord()
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			migrationBatchDataProvider.MigrationJob.ReportData.Append(Strings.MigrationReportJobStarted(base.ExecutingUserIdentityName));
			if (migrationBatchDataProvider.MigrationJob.IsPAW)
			{
				migrationBatchDataProvider.MigrationJob.SetMigrationFlags(migrationBatchDataProvider.MailboxProvider, MigrationFlags.Start);
			}
			else
			{
				MigrationBatchFlags migrationBatchFlags = migrationBatchDataProvider.MigrationJob.BatchFlags;
				if (this.Validate)
				{
					migrationBatchFlags |= MigrationBatchFlags.UseAdvancedValidation;
				}
				else
				{
					migrationBatchFlags &= ~MigrationBatchFlags.UseAdvancedValidation;
				}
				MigrationObjectTaskBase<MigrationBatchIdParameter>.StartJob(this, migrationBatchDataProvider, migrationBatchDataProvider.MigrationJob, null, migrationBatchFlags);
			}
			MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, migrationBatchDataProvider.MailboxSession, base.CurrentOrganizationId, false, false);
			base.InternalProcessRecord();
		}
	}
}
