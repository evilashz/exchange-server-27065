using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x0200027B RID: 635
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoteUserMailboxPrincipalBuilder : ExchangePrincipalBuilder
	{
		// Token: 0x06001A84 RID: 6788 RVA: 0x0007C8ED File Offset: 0x0007AAED
		public RemoteUserMailboxPrincipalBuilder(Func<IADUserFinder, IRecipientSession, IGenericADUser> findADUser) : base(findADUser)
		{
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0007C8F6 File Offset: 0x0007AAF6
		public RemoteUserMailboxPrincipalBuilder(IGenericADUser adUser) : base(adUser)
		{
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0007C8FF File Offset: 0x0007AAFF
		protected override ExchangePrincipal BuildPrincipal(IGenericADUser recipient, IEnumerable<IMailboxInfo> allMailboxes, Func<IMailboxInfo, bool> mailboxSelector, RemotingOptions remotingOptions)
		{
			return new RemoteUserMailboxPrincipal(recipient, allMailboxes, mailboxSelector, remotingOptions);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0007C90B File Offset: 0x0007AB0B
		protected override bool IsRecipientTypeSupported(IGenericADUser user)
		{
			return user.RecipientType == RecipientType.MailUser && user.RecipientTypeDetails == (RecipientTypeDetails)((ulong)int.MinValue);
		}
	}
}
