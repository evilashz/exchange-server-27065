using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ProvisioningStepHandler : ProvisioningStepHandlerBase
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00009D78 File Offset: 0x00007F78
		public ProvisioningStepHandler(IMigrationDataProvider dataProvider) : base(dataProvider)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009D84 File Offset: 0x00007F84
		public override MigrationUserStatus ResolvePresentationStatus(MigrationFlags flags, IStepSnapshot stepSnapshot = null)
		{
			MigrationUserStatus? migrationUserStatus = MigrationJobItem.ResolveFlagStatus(flags);
			if (migrationUserStatus != null)
			{
				return migrationUserStatus.Value;
			}
			return MigrationUserStatus.Provisioning;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009DAC File Offset: 0x00007FAC
		protected override IProvisioningData GetProvisioningData(MigrationJobItem jobItem)
		{
			switch (jobItem.MigrationType)
			{
			case MigrationType.XO1:
				return XO1ProvisioningDataFactory.GetProvisioningData(jobItem.LocalMailboxIdentifier, jobItem.ProvisioningData);
			case MigrationType.ExchangeOutlookAnywhere:
				return ExchangeProvisioningDataFactory.GetProvisioningData(base.DataProvider, jobItem.MigrationJob, jobItem);
			}
			return null;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00009DFC File Offset: 0x00007FFC
		protected override ProvisioningType GetProvisioningType(MigrationJobItem jobItem)
		{
			switch (jobItem.RecipientType)
			{
			case MigrationUserRecipientType.Mailbox:
				return ProvisioningType.User;
			case MigrationUserRecipientType.Contact:
				return ProvisioningType.Contact;
			case MigrationUserRecipientType.Group:
				return ProvisioningType.Group;
			case MigrationUserRecipientType.Mailuser:
				return ProvisioningType.MailEnabledUser;
			}
			throw new NotSupportedException("recipient type not supported!");
		}
	}
}
