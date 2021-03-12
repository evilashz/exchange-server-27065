using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200024B RID: 587
	internal class FxProxyPoolCloseEntryMessage : DataMessageBase
	{
		// Token: 0x06001E6C RID: 7788 RVA: 0x0003F11A File Offset: 0x0003D31A
		private FxProxyPoolCloseEntryMessage()
		{
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x0003F122 File Offset: 0x0003D322
		public static FxProxyPoolCloseEntryMessage Instance
		{
			get
			{
				return FxProxyPoolCloseEntryMessage.instance;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x0003F12C File Offset: 0x0003D32C
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolCloseEntry
				};
			}
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0003F146 File Offset: 0x0003D346
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return FxProxyPoolCloseEntryMessage.Instance;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0003F14D File Offset: 0x0003D34D
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolCloseEntry;
			data = null;
		}

		// Token: 0x04000C67 RID: 3175
		private static FxProxyPoolCloseEntryMessage instance = new FxProxyPoolCloseEntryMessage();
	}
}
