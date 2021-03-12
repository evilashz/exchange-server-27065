using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200024F RID: 591
	internal class FxProxyPoolGetFolderDataRequestMessage : DataMessageBase
	{
		// Token: 0x06001E86 RID: 7814 RVA: 0x0003F46C File Offset: 0x0003D66C
		private FxProxyPoolGetFolderDataRequestMessage()
		{
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0003F474 File Offset: 0x0003D674
		public static FxProxyPoolGetFolderDataRequestMessage Instance
		{
			get
			{
				return FxProxyPoolGetFolderDataRequestMessage.instance;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0003F47C File Offset: 0x0003D67C
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolGetFolderDataRequest
				};
			}
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x0003F499 File Offset: 0x0003D699
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return FxProxyPoolGetFolderDataRequestMessage.Instance;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0003F4A0 File Offset: 0x0003D6A0
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolGetFolderDataRequest;
			data = null;
		}

		// Token: 0x04000C6C RID: 3180
		private static FxProxyPoolGetFolderDataRequestMessage instance = new FxProxyPoolGetFolderDataRequestMessage();
	}
}
