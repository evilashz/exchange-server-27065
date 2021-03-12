using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000247 RID: 583
	internal class FlushMessage : DataMessageBase
	{
		// Token: 0x06001E57 RID: 7767 RVA: 0x0003EFC1 File Offset: 0x0003D1C1
		private FlushMessage()
		{
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0003EFC9 File Offset: 0x0003D1C9
		public static FlushMessage Instance
		{
			get
			{
				return FlushMessage.instance;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0003EFD0 File Offset: 0x0003D1D0
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.Flush
				};
			}
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0003EFED File Offset: 0x0003D1ED
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return FlushMessage.Instance;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0003EFF4 File Offset: 0x0003D1F4
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.Flush;
			data = null;
		}

		// Token: 0x04000C64 RID: 3172
		private static FlushMessage instance = new FlushMessage();
	}
}
