using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000047 RID: 71
	internal abstract class AIPermanentException : AIException
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000E9BB File Offset: 0x0000CBBB
		protected AIPermanentException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E9C5 File Offset: 0x0000CBC5
		protected AIPermanentException() : this(LocalizedString.Empty, null)
		{
		}
	}
}
