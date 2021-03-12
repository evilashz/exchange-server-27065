using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public sealed class MessageId : MapiObjectId
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002A9A File Offset: 0x00000C9A
		public MessageId()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AA2 File Offset: 0x00000CA2
		public MessageId(byte[] bytes) : base(bytes)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AAB File Offset: 0x00000CAB
		internal MessageId(MapiEntryId entryId) : base(entryId)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public override string ToString()
		{
			return base.MapiEntryId.ToString();
		}
	}
}
