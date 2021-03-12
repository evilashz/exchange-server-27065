using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x0200026A RID: 618
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupPrincipalBuilder : ExchangePrincipalBuilder
	{
		// Token: 0x06001965 RID: 6501 RVA: 0x000799A2 File Offset: 0x00077BA2
		public GroupPrincipalBuilder(Func<IADUserFinder, IRecipientSession, IGenericADUser> findADUser) : base(findADUser)
		{
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000799AB File Offset: 0x00077BAB
		public GroupPrincipalBuilder(IGenericADUser adUser) : base(adUser)
		{
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000799B4 File Offset: 0x00077BB4
		protected override ExchangePrincipal BuildPrincipal(IGenericADUser recipient, IEnumerable<IMailboxInfo> allMailboxes, Func<IMailboxInfo, bool> mailboxSelector, RemotingOptions remotingOptions)
		{
			return new GroupPrincipal(recipient, allMailboxes, mailboxSelector, remotingOptions);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000799C0 File Offset: 0x00077BC0
		protected override bool IsRecipientTypeSupported(IGenericADUser group)
		{
			return group.RecipientType == RecipientType.MailUniversalDistributionGroup && group.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox;
		}
	}
}
