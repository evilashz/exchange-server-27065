using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ProvisioningUpdateStepHandler : ProvisioningStepHandlerBase
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00009E42 File Offset: 0x00008042
		public ProvisioningUpdateStepHandler(IMigrationDataProvider dataProvider) : base(dataProvider)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009E4C File Offset: 0x0000804C
		public override MigrationUserStatus ResolvePresentationStatus(MigrationFlags flags, IStepSnapshot stepSnapshot = null)
		{
			MigrationUserStatus? migrationUserStatus = MigrationJobItem.ResolveFlagStatus(flags);
			if (migrationUserStatus != null)
			{
				return migrationUserStatus.Value;
			}
			return MigrationUserStatus.ProvisionUpdating;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009E74 File Offset: 0x00008074
		protected override IProvisioningData GetProvisioningData(MigrationJobItem jobItem)
		{
			MigrationType migrationType = jobItem.MigrationType;
			if (migrationType == MigrationType.ExchangeOutlookAnywhere)
			{
				return ExchangeProvisioningDataFactory.GetProvisioningUpdateData(base.DataProvider, jobItem.MigrationJob, jobItem);
			}
			return null;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009EA0 File Offset: 0x000080A0
		protected override ProvisioningType GetProvisioningType(MigrationJobItem jobItem)
		{
			switch (jobItem.RecipientType)
			{
			case MigrationUserRecipientType.Mailbox:
				return ProvisioningType.UserUpdate;
			case MigrationUserRecipientType.Contact:
				return ProvisioningType.ContactUpdate;
			case MigrationUserRecipientType.Group:
				return ProvisioningType.GroupMember;
			case MigrationUserRecipientType.Mailuser:
				return ProvisioningType.MailEnabledUserUpdate;
			}
			throw new NotSupportedException("recipient type not supported!");
		}
	}
}
