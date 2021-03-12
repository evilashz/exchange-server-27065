using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NspiDataException : Exception
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000040FA File Offset: 0x000022FA
		internal NspiDataException(string methodName, string message) : base(string.Format("{0} :: {1}", methodName, message))
		{
		}
	}
}
