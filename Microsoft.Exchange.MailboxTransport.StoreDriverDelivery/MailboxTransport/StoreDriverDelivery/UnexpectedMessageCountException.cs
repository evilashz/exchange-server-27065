using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	internal class UnexpectedMessageCountException : Exception
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000CE26 File Offset: 0x0000B026
		public UnexpectedMessageCountException(string message) : base(message)
		{
		}
	}
}
