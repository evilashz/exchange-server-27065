using System;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007FA RID: 2042
	internal class StringObjectGuidHandler : ObjectGuidHandler<string>
	{
		// Token: 0x060064CD RID: 25805 RVA: 0x0015FBCE File Offset: 0x0015DDCE
		private StringObjectGuidHandler()
		{
		}

		// Token: 0x170023BB RID: 9147
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x0015FBD6 File Offset: 0x0015DDD6
		public static StringObjectGuidHandler Instance
		{
			get
			{
				if (StringObjectGuidHandler.instance == null)
				{
					StringObjectGuidHandler.instance = new StringObjectGuidHandler();
				}
				return StringObjectGuidHandler.instance;
			}
		}

		// Token: 0x040042F8 RID: 17144
		private static StringObjectGuidHandler instance;
	}
}
