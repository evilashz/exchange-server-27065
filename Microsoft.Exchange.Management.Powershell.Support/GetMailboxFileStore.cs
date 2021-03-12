using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200002B RID: 43
	[Cmdlet("Get", "MailboxFileStore")]
	internal sealed class GetMailboxFileStore : MailboxFileStoreBase
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000A198 File Offset: 0x00008398
		protected override void Process(MailboxSession mailboxSession, MailboxFileStore mailboxFileStore)
		{
			foreach (FileSetItem sendToPipeline in mailboxFileStore.GetAll(base.FileSetId, mailboxSession))
			{
				base.WriteObject(sendToPipeline);
			}
		}
	}
}
