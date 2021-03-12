using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200025A RID: 602
	internal class FxProxyPoolWriteToMimeMessage : MessageWithBuffer
	{
		// Token: 0x06001ECA RID: 7882 RVA: 0x0003FC2D File Offset: 0x0003DE2D
		public FxProxyPoolWriteToMimeMessage(byte[] buffer) : base(buffer)
		{
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0003FC38 File Offset: 0x0003DE38
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolWriteToMime
				};
			}
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0003FC52 File Offset: 0x0003DE52
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolWriteToMimeMessage(data);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0003FC5A File Offset: 0x0003DE5A
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolWriteToMime;
			data = base.Buffer;
		}
	}
}
