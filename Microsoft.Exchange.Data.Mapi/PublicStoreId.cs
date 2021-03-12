using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public sealed class PublicStoreId : MessageStoreId
	{
		// Token: 0x06000033 RID: 51 RVA: 0x0000289C File Offset: 0x00000A9C
		public PublicStoreId()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028A4 File Offset: 0x00000AA4
		public PublicStoreId(byte[] bytes) : base(bytes)
		{
		}
	}
}
