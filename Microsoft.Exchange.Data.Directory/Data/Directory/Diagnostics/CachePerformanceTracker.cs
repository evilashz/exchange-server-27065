using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.Diagnostics
{
	// Token: 0x020000B9 RID: 185
	internal sealed class CachePerformanceTracker
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x0002BD70 File Offset: 0x00029F70
		internal static void StartLogging()
		{
			CachePerformanceTracker.log = new StringBuilder();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002BD7C File Offset: 0x00029F7C
		internal static void AddPerfData(Operation operation, long totalMilliseconds)
		{
			StringBuilder stringBuilder = CachePerformanceTracker.log;
			if (stringBuilder == null)
			{
				return;
			}
			stringBuilder.AppendFormat("{0}={1}", operation, totalMilliseconds);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002BDAC File Offset: 0x00029FAC
		internal static void AddException(Operation operation, Exception ex)
		{
			StringBuilder stringBuilder = CachePerformanceTracker.log;
			if (stringBuilder == null)
			{
				return;
			}
			stringBuilder.AppendFormat("{0}={1}", operation, ex.Message);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002BDDC File Offset: 0x00029FDC
		internal static string StopLogging()
		{
			if (CachePerformanceTracker.log == null)
			{
				return string.Empty;
			}
			string result = CachePerformanceTracker.log.ToString();
			CachePerformanceTracker.log = null;
			return result;
		}

		// Token: 0x0400037A RID: 890
		[ThreadStatic]
		private static StringBuilder log;
	}
}
