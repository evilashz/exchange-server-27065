using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public abstract class MessageStoreId : MapiObjectId
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002626 File Offset: 0x00000826
		public MessageStoreId()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000262E File Offset: 0x0000082E
		public MessageStoreId(byte[] bytes) : base(bytes)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002637 File Offset: 0x00000837
		public MessageStoreId(MapiEntryId entryId) : base(entryId)
		{
		}
	}
}
