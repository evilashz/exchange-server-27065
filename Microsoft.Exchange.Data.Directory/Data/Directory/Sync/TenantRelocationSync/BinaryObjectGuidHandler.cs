using System;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F9 RID: 2041
	internal class BinaryObjectGuidHandler : ObjectGuidHandler<byte[]>
	{
		// Token: 0x060064CB RID: 25803 RVA: 0x0015FBAE File Offset: 0x0015DDAE
		private BinaryObjectGuidHandler()
		{
		}

		// Token: 0x170023BA RID: 9146
		// (get) Token: 0x060064CC RID: 25804 RVA: 0x0015FBB6 File Offset: 0x0015DDB6
		public static BinaryObjectGuidHandler Instance
		{
			get
			{
				if (BinaryObjectGuidHandler.instance == null)
				{
					BinaryObjectGuidHandler.instance = new BinaryObjectGuidHandler();
				}
				return BinaryObjectGuidHandler.instance;
			}
		}

		// Token: 0x040042F7 RID: 17143
		private static BinaryObjectGuidHandler instance;
	}
}
