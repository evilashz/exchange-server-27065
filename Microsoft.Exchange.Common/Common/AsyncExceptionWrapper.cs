using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200005B RID: 91
	public class AsyncExceptionWrapper : Exception
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public AsyncExceptionWrapper(Exception innerExceptions) : base(CommonStrings.AsyncExceptionMessage(innerExceptions.Message), innerExceptions)
		{
		}
	}
}
