using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A8 RID: 424
	internal class MailboxCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x0002E178 File Offset: 0x0002C378
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"DisplayName",
				"Alias",
				"HiddenFromAddressListsEnabled",
				"ServerLegacyDN",
				"Database",
				"ArchiveDatabase",
				"WhenChanged",
				"CustomAttribute1",
				"CustomAttribute2",
				"CustomAttribute3",
				"CustomAttribute4",
				"CustomAttribute5",
				"CustomAttribute6",
				"CustomAttribute7",
				"CustomAttribute8",
				"CustomAttribute9",
				"CustomAttribute10",
				"CustomAttribute11",
				"CustomAttribute12",
				"CustomAttribute13",
				"CustomAttribute14",
				"CustomAttribute15",
				"EmailAddressPolicyEnabled",
				"EmailAddresses",
				"ManagedFolderMailboxPolicy",
				"RetentionPolicy",
				"RetentionUrl",
				"RetentionComment",
				"LitigationHoldEnabled",
				"RetentionHoldEnabled",
				"StartDateForRetentionHold",
				"EndDateForRetentionHold",
				"UseDatabaseQuotaDefaults",
				"ArchiveQuota",
				"IssueWarningQuota",
				"ProhibitSendQuota",
				"ProhibitSendReceiveQuota",
				"ArchiveGuid",
				"ArchiveName",
				"ArchiveWarningQuota",
				"UseDatabaseRetentionDefaults",
				"RetainDeletedItemsUntilBackup",
				"RetainDeletedItemsFor",
				"SharingPolicy",
				"RoleAssignmentPolicy",
				"MailboxPlan",
				"GrantSendOnBehalfTo",
				"ForwardingAddress",
				"DeliverToMailboxAndForward",
				"RecipientLimits",
				"MaxSendSize",
				"MaxReceiveSize",
				"RecipientTypeDetails",
				"AcceptMessagesOnlyFromSendersOrMembers",
				"RejectMessagesFromSendersOrMembers",
				"RequireSenderAuthenticationEnabled",
				"UMEnabled",
				"ResourceCapacity",
				"ResourceCustom",
				"ResourceType"
			};
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0002E3AC File Offset: 0x0002C5AC
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "RequireSenderAuthenticationEnabled")
			{
				configObject.propertyBag[MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled.Type);
				return;
			}
			if (propertyName == "RetentionHoldEnabled")
			{
				configObject.propertyBag[MailboxSchema.ElcExpirationSuspensionEnabled] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, MailboxSchema.ElcExpirationSuspensionEnabled.Type);
				return;
			}
			if (propertyName == "StartDateForRetentionHold")
			{
				configObject.propertyBag[MailboxSchema.ElcExpirationSuspensionStartDate] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, MailboxSchema.ElcExpirationSuspensionStartDate.Type);
				return;
			}
			if (propertyName == "EndDateForRetentionHold")
			{
				configObject.propertyBag[MailboxSchema.ElcExpirationSuspensionEndDate] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, MailboxSchema.ElcExpirationSuspensionEndDate.Type);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
