using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000273 RID: 627
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxInfo : IMailboxInfo
	{
		// Token: 0x060019F0 RID: 6640 RVA: 0x0007B344 File Offset: 0x00079544
		public MailboxInfo(Guid mailboxGuid, ADObjectId databaseId, IGenericADUser adUser, IMailboxConfiguration mailboxConfiguration, IMailboxLocation mailboxLocation)
		{
			ArgumentValidator.ThrowIfEmpty("mailboxGuid", mailboxGuid);
			ArgumentValidator.ThrowIfNull("adUser", adUser);
			ArgumentValidator.ThrowIfNull("mailboxConfiguration", mailboxConfiguration);
			ArgumentValidator.ThrowIfNull("mailboxLocation", mailboxLocation);
			MailboxLocationType? mailboxLocationType = adUser.GetMailboxLocationType(mailboxGuid);
			if (mailboxLocationType == null)
			{
				throw new ArgumentException("The provided mailbox guid doesn't match with any of the user's mailbox.");
			}
			this.MailboxType = mailboxLocationType.Value;
			this.DisplayName = adUser.DisplayName;
			this.PrimarySmtpAddress = adUser.PrimarySmtpAddress;
			this.ExternalEmailAddress = adUser.ExternalEmailAddress;
			this.EmailAddresses = (adUser.EmailAddresses ?? Array<ProxyAddress>.Empty);
			this.OrganizationId = adUser.OrganizationId;
			this.MailboxGuid = mailboxGuid;
			this.MailboxDatabase = databaseId;
			if (this.IsArchive)
			{
				this.archiveName = ((adUser.ArchiveName != null) ? (adUser.ArchiveName.FirstOrDefault<string>() ?? string.Empty) : string.Empty);
				this.archiveState = adUser.ArchiveState;
				this.archiveStatus = adUser.ArchiveStatus;
				this.IsRemote = adUser.IsArchiveMailboxRemote();
			}
			else
			{
				this.IsRemote = adUser.IsPrimaryMailboxRemote();
			}
			if (this.IsRemote)
			{
				this.remoteIdentity = this.GetRemoteIdentity(adUser, this.IsArchive);
			}
			else if (this.MailboxDatabase.IsNullOrEmpty())
			{
				throw new ObjectNotFoundException(ServerStrings.MailboxDatabaseRequired(mailboxGuid));
			}
			this.WhenMailboxCreated = adUser.WhenMailboxCreated;
			this.Location = mailboxLocation;
			this.Configuration = mailboxConfiguration;
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x0007B4BB File Offset: 0x000796BB
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x0007B4C3 File Offset: 0x000796C3
		public string DisplayName { get; private set; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x0007B4CC File Offset: 0x000796CC
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x0007B4D4 File Offset: 0x000796D4
		public SmtpAddress PrimarySmtpAddress { get; private set; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0007B4DD File Offset: 0x000796DD
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0007B4E5 File Offset: 0x000796E5
		public ProxyAddress ExternalEmailAddress { get; private set; }

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0007B4EE File Offset: 0x000796EE
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x0007B4F6 File Offset: 0x000796F6
		public IEnumerable<ProxyAddress> EmailAddresses { get; private set; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0007B4FF File Offset: 0x000796FF
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x0007B507 File Offset: 0x00079707
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0007B510 File Offset: 0x00079710
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x0007B518 File Offset: 0x00079718
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0007B521 File Offset: 0x00079721
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x0007B529 File Offset: 0x00079729
		public ADObjectId MailboxDatabase { get; private set; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0007B532 File Offset: 0x00079732
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x0007B53A File Offset: 0x0007973A
		public DateTime? WhenMailboxCreated { get; private set; }

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0007B543 File Offset: 0x00079743
		public bool IsArchive
		{
			get
			{
				return this.MailboxType == MailboxLocationType.MainArchive;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0007B54E File Offset: 0x0007974E
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x0007B556 File Offset: 0x00079756
		public MailboxLocationType MailboxType { get; private set; }

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x0007B55F File Offset: 0x0007975F
		public string ArchiveName
		{
			get
			{
				if (!this.IsArchive)
				{
					throw new InvalidOperationException("Not an archive mailbox");
				}
				return this.archiveName;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0007B57A File Offset: 0x0007977A
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				if (!this.IsArchive)
				{
					throw new InvalidOperationException("Not an archive mailbox");
				}
				return this.archiveStatus;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0007B595 File Offset: 0x00079795
		public ArchiveState ArchiveState
		{
			get
			{
				if (!this.IsArchive)
				{
					throw new InvalidOperationException("Not an archive mailbox");
				}
				return this.archiveState;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0007B5B0 File Offset: 0x000797B0
		public SmtpAddress? RemoteIdentity
		{
			get
			{
				if (!this.IsRemote)
				{
					throw new InvalidOperationException("Not a remote mailbox");
				}
				return this.remoteIdentity;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0007B5CB File Offset: 0x000797CB
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x0007B5D3 File Offset: 0x000797D3
		public bool IsRemote { get; private set; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0007B5DC File Offset: 0x000797DC
		public bool IsAggregated
		{
			get
			{
				return this.MailboxType == MailboxLocationType.Aggregated;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0007B5E7 File Offset: 0x000797E7
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x0007B5EF File Offset: 0x000797EF
		public IMailboxLocation Location { get; private set; }

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0007B5F8 File Offset: 0x000797F8
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x0007B600 File Offset: 0x00079800
		public IMailboxConfiguration Configuration { get; private set; }

		// Token: 0x06001A0F RID: 6671 RVA: 0x0007B60C File Offset: 0x0007980C
		public override string ToString()
		{
			return string.Format("Display Name: {0}, Mailbox Guid: {1}, Database: {2}, Location: {3}", new object[]
			{
				this.DisplayName,
				this.MailboxGuid,
				this.MailboxDatabase,
				this.Location
			});
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0007B654 File Offset: 0x00079854
		private SmtpAddress? GetRemoteIdentity(IGenericADUser adUser, bool isArchive)
		{
			if (isArchive)
			{
				if (adUser.ArchiveDomain != null && (adUser.ArchiveStatus & ArchiveStatusFlags.Active) == ArchiveStatusFlags.Active)
				{
					return new SmtpAddress?(new SmtpAddress(SmtpProxyAddress.EncapsulateExchangeGuid(adUser.ArchiveDomain.Domain, adUser.ArchiveGuid)));
				}
			}
			else if (adUser.ExternalEmailAddress.Prefix == ProxyAddressPrefix.Smtp && SmtpAddress.IsValidSmtpAddress(adUser.ExternalEmailAddress.AddressString))
			{
				return new SmtpAddress?(new SmtpAddress(adUser.ExternalEmailAddress.AddressString));
			}
			return null;
		}

		// Token: 0x04001273 RID: 4723
		private readonly string archiveName;

		// Token: 0x04001274 RID: 4724
		private readonly ArchiveState archiveState;

		// Token: 0x04001275 RID: 4725
		private readonly ArchiveStatusFlags archiveStatus;

		// Token: 0x04001276 RID: 4726
		private readonly SmtpAddress? remoteIdentity;
	}
}
