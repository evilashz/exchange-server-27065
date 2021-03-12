using System;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200004B RID: 75
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupMailboxBuilder : IMailboxBuilder<GroupMailbox>
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000ED56 File Offset: 0x0000CF56
		public GroupMailboxBuilder(GroupMailboxLocator locator)
		{
			ArgumentValidator.ThrowIfNull("locator", locator);
			this.Mailbox = new GroupMailbox(locator);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000ED75 File Offset: 0x0000CF75
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000ED7D File Offset: 0x0000CF7D
		public GroupMailbox Mailbox { get; private set; }

		// Token: 0x06000262 RID: 610 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public IMailboxBuilder<GroupMailbox> BuildFromAssociation(MailboxAssociation association)
		{
			ArgumentValidator.ThrowIfNull("association", association);
			this.Mailbox.IsPinned = association.IsPin;
			this.Mailbox.IsMember = association.IsMember;
			this.Mailbox.JoinedBy = association.JoinedBy;
			this.Mailbox.JoinDate = association.JoinDate;
			this.Mailbox.PinDate = association.PinDate;
			return this;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		public IMailboxBuilder<GroupMailbox> BuildFromDirectory(ADRawEntry rawEntry)
		{
			this.Mailbox.Alias = (rawEntry[GroupMailboxSchema.Alias] as string);
			this.Mailbox.DisplayName = (rawEntry[GroupMailboxSchema.DisplayName] as string);
			this.Mailbox.Owners = (rawEntry[GroupMailboxSchema.Owners] as MultiValuedProperty<ADObjectId>);
			this.Mailbox.SmtpAddress = (SmtpAddress)rawEntry[GroupMailboxSchema.PrimarySmtpAddress];
			this.Mailbox.Type = (ModernGroupObjectType)rawEntry[GroupMailboxSchema.ModernGroupType];
			this.Mailbox.SharePointUrl = (rawEntry[GroupMailboxSchema.SharePointUrl] as Uri);
			this.Mailbox.SharePointSiteUrl = (rawEntry[GroupMailboxSchema.SharePointSiteUrl] as string);
			this.Mailbox.SharePointDocumentsUrl = (rawEntry[GroupMailboxSchema.SharePointDocumentsUrl] as string);
			this.Mailbox.RequireSenderAuthenticationEnabled = (bool)rawEntry[GroupMailboxSchema.RequireSenderAuthenticationEnabled];
			this.Mailbox.AutoSubscribeNewGroupMembers = (bool)rawEntry[GroupMailboxSchema.AutoSubscribeNewGroupMembers];
			MultiValuedProperty<CultureInfo> multiValuedProperty = rawEntry[GroupMailboxSchema.Languages] as MultiValuedProperty<CultureInfo>;
			if (multiValuedProperty != null)
			{
				this.Mailbox.Language = multiValuedProperty.FirstOrDefault<CultureInfo>();
			}
			MultiValuedProperty<string> multiValuedProperty2 = rawEntry[GroupMailboxSchema.Description] as MultiValuedProperty<string>;
			if (multiValuedProperty2 != null)
			{
				this.Mailbox.Description = multiValuedProperty2.FirstOrDefault<string>();
			}
			return this;
		}

		// Token: 0x04000153 RID: 339
		public static readonly PropertyDefinition[] AllADProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			GroupMailboxSchema.Alias,
			GroupMailboxSchema.DisplayName,
			GroupMailboxSchema.Description,
			GroupMailboxSchema.ModernGroupType,
			GroupMailboxSchema.Owners,
			GroupMailboxSchema.PrimarySmtpAddress,
			GroupMailboxSchema.SharePointUrl,
			GroupMailboxSchema.RequireSenderAuthenticationEnabled,
			GroupMailboxSchema.AutoSubscribeNewGroupMembers,
			GroupMailboxSchema.Languages,
			ADMailboxRecipientSchema.SharePointResources
		};
	}
}
