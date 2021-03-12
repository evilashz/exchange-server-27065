using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000048 RID: 72
	internal class AmbiguousAliasMailboxException : AIMailboxUnavailableException
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000E9D3 File Offset: 0x0000CBD3
		public AmbiguousAliasMailboxException(Exception innerException) : base(Strings.descAmbiguousAliasMailboxException, innerException)
		{
		}
	}
}
