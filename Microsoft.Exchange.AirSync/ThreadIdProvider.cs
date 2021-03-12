using System;
using System.Threading;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000296 RID: 662
	internal class ThreadIdProvider
	{
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0008E156 File Offset: 0x0008C356
		public static int ManagedThreadId
		{
			get
			{
				if (ThreadIdProvider.provider != null)
				{
					return ThreadIdProvider.provider.ManagedThreadId;
				}
				return Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008E174 File Offset: 0x0008C374
		public static IThreadIdProvider SetProvider(IThreadIdProvider provider)
		{
			IThreadIdProvider result = ThreadIdProvider.provider;
			ThreadIdProvider.provider = provider;
			return result;
		}

		// Token: 0x04000EF7 RID: 3831
		private static IThreadIdProvider provider;
	}
}
