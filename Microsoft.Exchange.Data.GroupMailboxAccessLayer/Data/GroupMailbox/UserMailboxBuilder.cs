using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UserMailboxBuilder : IMailboxBuilder<UserMailbox>
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public UserMailboxBuilder(UserMailboxLocator locator, IEnumerable<ADObjectId> owners = null)
		{
			ArgumentValidator.ThrowIfNull("locator", locator);
			this.Mailbox = new UserMailbox(locator);
			this.owners = ((owners != null) ? new HashSet<ADObjectId>(owners) : new HashSet<ADObjectId>());
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000FE19 File Offset: 0x0000E019
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000FE21 File Offset: 0x0000E021
		public UserMailbox Mailbox { get; private set; }

		// Token: 0x0600029A RID: 666 RVA: 0x0000FE2C File Offset: 0x0000E02C
		public IMailboxBuilder<UserMailbox> BuildFromAssociation(MailboxAssociation association)
		{
			ArgumentValidator.ThrowIfNull("association", association);
			this.Mailbox.IsMember = association.IsMember;
			this.Mailbox.JoinDate = association.JoinDate;
			this.Mailbox.LastVisitedDate = association.LastVisitedDate;
			this.Mailbox.ShouldEscalate = association.ShouldEscalate;
			this.Mailbox.IsAutoSubscribed = association.IsAutoSubscribed;
			this.Mailbox.IsPin = association.IsPin;
			return this;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		public IMailboxBuilder<UserMailbox> BuildFromDirectory(ADRawEntry rawEntry)
		{
			ArgumentValidator.ThrowIfNull("rawEntry", rawEntry);
			this.Mailbox.ADObjectId = rawEntry.Id;
			this.Mailbox.Alias = (rawEntry[ADRecipientSchema.Alias] as string);
			this.Mailbox.DisplayName = (rawEntry[ADRecipientSchema.DisplayName] as string);
			this.Mailbox.ImAddress = ADPersonToContactConverter.GetSipUri(rawEntry);
			this.Mailbox.IsOwner = this.owners.Contains(rawEntry.Id);
			this.Mailbox.SmtpAddress = (SmtpAddress)rawEntry[ADRecipientSchema.PrimarySmtpAddress];
			this.Mailbox.Title = (rawEntry[ADOrgPersonSchema.Title] as string);
			return this;
		}

		// Token: 0x0400016B RID: 363
		public static readonly PropertyDefinition[] AllADProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.Alias,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.PrimarySmtpAddress,
			ADUserSchema.RTCSIPPrimaryUserAddress,
			ADOrgPersonSchema.Title
		};

		// Token: 0x0400016C RID: 364
		private readonly ISet<ADObjectId> owners;
	}
}
