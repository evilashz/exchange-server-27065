using System;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200007B RID: 123
	internal interface IDirectoryListener
	{
		// Token: 0x0600044E RID: 1102
		void ObjectLoaded(DirectoryObject directoryObject);
	}
}
