using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000253 RID: 595
	internal class FxProxyPoolOpenFolderMessage : MessageWithBuffer
	{
		// Token: 0x06001EA0 RID: 7840 RVA: 0x0003F914 File Offset: 0x0003DB14
		public FxProxyPoolOpenFolderMessage(byte[] folderID) : base(folderID)
		{
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0003F920 File Offset: 0x0003DB20
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolOpenFolder
				};
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0003F93A File Offset: 0x0003DB3A
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolOpenFolderMessage(data);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0003F942 File Offset: 0x0003DB42
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolOpenFolder;
			data = base.Buffer;
		}
	}
}
