using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000248 RID: 584
	internal class FxProxyGetObjectDataRequestMessage : DataMessageBase
	{
		// Token: 0x06001E5D RID: 7773 RVA: 0x0003F00C File Offset: 0x0003D20C
		private FxProxyGetObjectDataRequestMessage()
		{
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0003F014 File Offset: 0x0003D214
		public static FxProxyGetObjectDataRequestMessage Instance
		{
			get
			{
				return FxProxyGetObjectDataRequestMessage.instance;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x0003F01C File Offset: 0x0003D21C
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyGetObjectDataRequest
				};
			}
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0003F039 File Offset: 0x0003D239
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return FxProxyGetObjectDataRequestMessage.Instance;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0003F040 File Offset: 0x0003D240
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyGetObjectDataRequest;
			data = null;
		}

		// Token: 0x04000C65 RID: 3173
		private static FxProxyGetObjectDataRequestMessage instance = new FxProxyGetObjectDataRequestMessage();
	}
}
