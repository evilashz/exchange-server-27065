using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000251 RID: 593
	internal class FxProxyPoolGetUploadedIDsRequestMessage : DataMessageBase
	{
		// Token: 0x06001E93 RID: 7827 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
		private FxProxyPoolGetUploadedIDsRequestMessage()
		{
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
		public static FxProxyPoolGetUploadedIDsRequestMessage Instance
		{
			get
			{
				return FxProxyPoolGetUploadedIDsRequestMessage.instance;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x0003F700 File Offset: 0x0003D900
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolGetUploadedIDsRequest
				};
			}
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0003F71D File Offset: 0x0003D91D
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return FxProxyPoolGetUploadedIDsRequestMessage.Instance;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0003F724 File Offset: 0x0003D924
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolGetUploadedIDsRequest;
			data = null;
		}

		// Token: 0x04000C6E RID: 3182
		private static FxProxyPoolGetUploadedIDsRequestMessage instance = new FxProxyPoolGetUploadedIDsRequestMessage();
	}
}
