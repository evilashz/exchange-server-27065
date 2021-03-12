using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200005C RID: 92
	public class AsyncLocalizedExceptionWrapper : LocalizedException
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00008CFD File Offset: 0x00006EFD
		public AsyncLocalizedExceptionWrapper(Exception innerException) : base(CommonStrings.AsyncExceptionMessage(innerException.Message), innerException)
		{
		}
	}
}
