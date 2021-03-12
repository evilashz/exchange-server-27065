using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000018 RID: 24
	public class AsyncLogWrapperFactory
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000614C File Offset: 0x0000434C
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006153 File Offset: 0x00004353
		internal static Func<string, LogHeaderFormatter, string, IAsyncLogWrapper> CreateAsyncLogWrapperFunc
		{
			get
			{
				return AsyncLogWrapperFactory.createAsyncLogWrapperFunc;
			}
			set
			{
				AsyncLogWrapperFactory.createAsyncLogWrapperFunc = value;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000615B File Offset: 0x0000435B
		internal static IAsyncLogWrapper CreateAsyncLogWrapper(string logFileName, LogHeaderFormatter logHeaderFormatter, string logComponentName)
		{
			return AsyncLogWrapperFactory.CreateAsyncLogWrapperFunc(logFileName, logHeaderFormatter, logComponentName);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000616A File Offset: 0x0000436A
		private static IAsyncLogWrapper CreateRealAsyncLogWrapper(string logFileName, LogHeaderFormatter logHeaderFormatter, string logComponentName)
		{
			return new AsyncLogWrapper(logFileName, logHeaderFormatter, logComponentName);
		}

		// Token: 0x0400007E RID: 126
		private static Func<string, LogHeaderFormatter, string, IAsyncLogWrapper> createAsyncLogWrapperFunc = new Func<string, LogHeaderFormatter, string, IAsyncLogWrapper>(AsyncLogWrapperFactory.CreateRealAsyncLogWrapper);
	}
}
