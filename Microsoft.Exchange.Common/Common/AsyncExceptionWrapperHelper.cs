using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200005A RID: 90
	public static class AsyncExceptionWrapperHelper
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public static Exception GetRootException(Exception ex)
		{
			Exception ex2 = ex;
			while (ex2 is AsyncExceptionWrapper || ex2 is AsyncLocalizedExceptionWrapper)
			{
				ex2 = ex2.InnerException;
			}
			return ex2;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008CCD File Offset: 0x00006ECD
		public static Exception GetAsyncWrapper(Exception ex)
		{
			if (ex is LocalizedException)
			{
				return new AsyncLocalizedExceptionWrapper(ex);
			}
			return new AsyncExceptionWrapper(ex);
		}
	}
}
