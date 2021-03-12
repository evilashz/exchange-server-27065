using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200024A RID: 586
	internal class FxProxyImportBufferMessage : MessageWithBuffer
	{
		// Token: 0x06001E67 RID: 7783 RVA: 0x0003F09A File Offset: 0x0003D29A
		public FxProxyImportBufferMessage(FxOpcodes opcode, byte[] data) : base(data)
		{
			this.opcode = opcode;
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x0003F0AC File Offset: 0x0003D2AC
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.MapiFxConfig,
					DataMessageOpcode.MapiFxTransferBuffer,
					DataMessageOpcode.MapiFxIsInterfaceOk,
					DataMessageOpcode.MapiFxTellPartnerVersion,
					DataMessageOpcode.MapiFxStartMdbEventsImport,
					DataMessageOpcode.MapiFxFinishMdbEventsImport,
					DataMessageOpcode.MapiFxAddMdbEvents,
					DataMessageOpcode.MapiFxSetWatermarks,
					DataMessageOpcode.MapiFxSetReceiveFolder,
					DataMessageOpcode.MapiFxSetPerUser,
					DataMessageOpcode.MapiFxSetProps
				};
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0003F0F7 File Offset: 0x0003D2F7
		public FxOpcodes Opcode
		{
			get
			{
				return this.opcode;
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0003F0FF File Offset: 0x0003D2FF
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyImportBufferMessage((FxOpcodes)opcode, data);
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0003F108 File Offset: 0x0003D308
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = (DataMessageOpcode)this.opcode;
			data = base.Buffer;
		}

		// Token: 0x04000C66 RID: 3174
		private FxOpcodes opcode;
	}
}
