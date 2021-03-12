using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200002C RID: 44
	[Cmdlet("Remove", "MailboxFileStore")]
	internal sealed class RemoveMailboxFileStore : MailboxFileStoreBase
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000A1FC File Offset: 0x000083FC
		protected override void Process(MailboxSession mailboxSession, MailboxFileStore mailboxFileStore)
		{
			mailboxFileStore.RemoveAll(base.FileSetId, mailboxSession);
		}
	}
}
