using System;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x02000027 RID: 39
	internal static class ExceptionInjectionCallback
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public static Exception ExceptionLookup(string exceptionType)
		{
			Exception result = null;
			if (!string.IsNullOrEmpty(exceptionType))
			{
				if (exceptionType.Equals("System.NullReferenceException", StringComparison.OrdinalIgnoreCase))
				{
					result = new NullReferenceException();
				}
				else if (exceptionType.Equals("System.ArgumentNullException", StringComparison.OrdinalIgnoreCase))
				{
					result = new ArgumentNullException();
				}
				else if (exceptionType.Equals("NonGrayException", StringComparison.OrdinalIgnoreCase))
				{
					result = new NonGrayException();
				}
			}
			return result;
		}
	}
}
