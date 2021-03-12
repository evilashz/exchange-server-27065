using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationADProvider
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002D3 RID: 723
		string TenantOrganizationName { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002D4 RID: 724
		bool IsLicensingEnforced { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002D5 RID: 725
		bool IsSmtpAddressCheckWithAcceptedDomain { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002D6 RID: 726
		bool IsMigrationEnabled { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002D7 RID: 727
		bool IsDirSyncEnabled { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002D8 RID: 728
		bool IsMSOSyncEnabled { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002D9 RID: 729
		MicrosoftExchangeRecipient PrimaryExchangeRecipient { get; }

		// Token: 0x060002DA RID: 730
		ADRecipient GetADRecipientByProxyAddress(string userEmail);

		// Token: 0x060002DB RID: 731
		ADRecipient GetADRecipientByObjectId(ADObjectId objectId);

		// Token: 0x060002DC RID: 732
		ADRecipient GetADRecipientByExchangeObjectId(Guid exchangeObjectGuid);

		// Token: 0x060002DD RID: 733
		MailboxData GetMailboxDataFromSmtpAddress(string userEmail, bool forceRefresh, bool throwOnNotFound = true);

		// Token: 0x060002DE RID: 734
		MailboxData GetMailboxDataFromLegacyDN(string userLegDN, bool forceRefresh, string userEmailAddressForDebug);

		// Token: 0x060002DF RID: 735
		MailboxData GetPublicFolderMailboxDataFromName(string name, bool forceRefresh, bool throwOnNotFound = true);

		// Token: 0x060002E0 RID: 736
		string GetPublicFolderHierarchyMailboxName();

		// Token: 0x060002E1 RID: 737
		string GetDatabaseServerFqdn(Guid mdbGuid, bool forceRefresh);

		// Token: 0x060002E2 RID: 738
		void UpdateMigrationUpgradeConstraint(UpgradeConstraint constraint);

		// Token: 0x060002E3 RID: 739
		void RemovePublicFolderMigrationLock();

		// Token: 0x060002E4 RID: 740
		bool CheckPublicFoldersLockedForMigration();

		// Token: 0x060002E5 RID: 741
		string GetPreferredDomainController();

		// Token: 0x060002E6 RID: 742
		ExchangePrincipal GetExchangePrincipalFromMbxGuid(Guid mailboxGuid);
	}
}
