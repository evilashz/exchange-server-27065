using System;

namespace Microsoft.Exchange.Data.GroupMailbox.Consistency
{
	// Token: 0x0200002D RID: 45
	internal interface IReplicationAssistantInvoker
	{
		// Token: 0x06000165 RID: 357
		bool Invoke(string command, IAssociationAdaptor masterAdaptor, params MailboxAssociation[] associations);
	}
}
