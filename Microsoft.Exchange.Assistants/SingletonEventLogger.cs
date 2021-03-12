using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000079 RID: 121
	internal sealed class SingletonEventLogger
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0001152C File Offset: 0x0000F72C
		public static EventLogger Logger
		{
			get
			{
				return SingletonEventLogger.logger;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011533 File Offset: 0x0000F733
		public static EventLogger GetSingleton(string serviceName)
		{
			if (SingletonEventLogger.logger == null)
			{
				SingletonEventLogger.logger = new EventLogger(serviceName);
			}
			return SingletonEventLogger.logger;
		}

		// Token: 0x04000200 RID: 512
		private static EventLogger logger;
	}
}
