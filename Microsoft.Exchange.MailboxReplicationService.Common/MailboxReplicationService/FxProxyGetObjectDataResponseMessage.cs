using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000249 RID: 585
	internal class FxProxyGetObjectDataResponseMessage : MessageWithBuffer
	{
		// Token: 0x06001E63 RID: 7779 RVA: 0x0003F058 File Offset: 0x0003D258
		public FxProxyGetObjectDataResponseMessage(byte[] data) : base(data)
		{
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x0003F064 File Offset: 0x0003D264
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyGetObjectDataResponse
				};
			}
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0003F081 File Offset: 0x0003D281
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyGetObjectDataResponseMessage(data);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0003F089 File Offset: 0x0003D289
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyGetObjectDataResponse;
			data = base.Buffer;
		}
	}
}
