using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000254 RID: 596
	internal class FxProxyPoolOpenItemMessage : MessageWithBuffer
	{
		// Token: 0x06001EA4 RID: 7844 RVA: 0x0003F950 File Offset: 0x0003DB50
		public FxProxyPoolOpenItemMessage(byte[] entryID) : base(entryID)
		{
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0003F95C File Offset: 0x0003DB5C
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolOpenItem
				};
			}
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0003F976 File Offset: 0x0003DB76
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolOpenItemMessage(data);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0003F97E File Offset: 0x0003DB7E
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolOpenItem;
			data = base.Buffer;
		}
	}
}
