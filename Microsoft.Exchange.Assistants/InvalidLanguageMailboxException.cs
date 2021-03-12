using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000050 RID: 80
	internal class InvalidLanguageMailboxException : AIMailboxUnavailableException
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000EB99 File Offset: 0x0000CD99
		public InvalidLanguageMailboxException(Exception innerException) : base(Strings.descInvalidLanguageMailboxException, innerException)
		{
		}
	}
}
