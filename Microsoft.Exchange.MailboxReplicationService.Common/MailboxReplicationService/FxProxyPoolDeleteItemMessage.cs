using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200024E RID: 590
	internal class FxProxyPoolDeleteItemMessage : MessageWithBuffer
	{
		// Token: 0x06001E82 RID: 7810 RVA: 0x0003F432 File Offset: 0x0003D632
		public FxProxyPoolDeleteItemMessage(byte[] entryID) : base(entryID)
		{
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x0003F43C File Offset: 0x0003D63C
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolDeleteItem
				};
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0003F456 File Offset: 0x0003D656
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolDeleteItemMessage(data);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0003F45E File Offset: 0x0003D65E
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolDeleteItem;
			data = base.Buffer;
		}
	}
}
