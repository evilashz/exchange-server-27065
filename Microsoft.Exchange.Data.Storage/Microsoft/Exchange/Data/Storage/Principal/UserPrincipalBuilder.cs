using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x0200026B RID: 619
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UserPrincipalBuilder : ExchangePrincipalBuilder
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x000799DE File Offset: 0x00077BDE
		public UserPrincipalBuilder(Func<IADUserFinder, IRecipientSession, IGenericADUser> findADUser) : base(findADUser)
		{
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000799E7 File Offset: 0x00077BE7
		public UserPrincipalBuilder(IGenericADUser adUser) : base(adUser)
		{
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000799F0 File Offset: 0x00077BF0
		protected override ExchangePrincipal BuildPrincipal(IGenericADUser recipient, IEnumerable<IMailboxInfo> allMailboxes, Func<IMailboxInfo, bool> mailboxSelector, RemotingOptions remotingOptions)
		{
			return new UserPrincipal(recipient, allMailboxes, mailboxSelector, remotingOptions);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000799FC File Offset: 0x00077BFC
		protected override bool IsRecipientTypeSupported(IGenericADUser user)
		{
			return user.RecipientType == RecipientType.UserMailbox || user.RecipientType == RecipientType.SystemMailbox || user.RecipientType == RecipientType.SystemAttendantMailbox || this.IsArchiveMailUser(user.RecipientType, user.ArchiveGuid, user.ArchiveDatabase);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00079A37 File Offset: 0x00077C37
		private bool IsArchiveMailUser(RecipientType recipientType, Guid archiveMailboxGuid, ADObjectId archiveDatabase)
		{
			return recipientType == RecipientType.MailUser && archiveMailboxGuid != Guid.Empty && !archiveDatabase.IsNullOrEmpty();
		}
	}
}
