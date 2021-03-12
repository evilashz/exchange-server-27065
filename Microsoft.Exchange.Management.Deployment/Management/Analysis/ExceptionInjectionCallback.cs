using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000002 RID: 2
	internal static class ExceptionInjectionCallback
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static Exception Win32InvalidOperationException(string exceptionType)
		{
			Exception result = null;
			if (!string.IsNullOrEmpty(exceptionType) && exceptionType.Equals("System.InvalidOperationException", StringComparison.OrdinalIgnoreCase))
			{
				result = new InvalidOperationException("Fault Injection", new Win32Exception(1072));
			}
			return result;
		}
	}
}
