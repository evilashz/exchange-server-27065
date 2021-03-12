using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200004E RID: 78
	internal class DisconnectedMailboxException : AIMailboxUnavailableException
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public DisconnectedMailboxException(Exception innerException) : base(Strings.descDisconnectedMailboxException, innerException)
		{
		}
	}
}
