using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000050 RID: 80
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SessionDeadException : Exception
	{
		// Token: 0x0600029E RID: 670 RVA: 0x000091D6 File Offset: 0x000073D6
		internal SessionDeadException(string message) : base(message)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000091DF File Offset: 0x000073DF
		internal SessionDeadException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
