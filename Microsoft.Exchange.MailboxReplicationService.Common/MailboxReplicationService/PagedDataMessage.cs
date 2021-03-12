using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200025C RID: 604
	internal class PagedDataMessage : MessageWithBuffer
	{
		// Token: 0x06001ED8 RID: 7896 RVA: 0x0003FE33 File Offset: 0x0003E033
		public PagedDataMessage(byte[] data, bool isLastChunk) : base(data)
		{
			this.isLastChunk = isLastChunk;
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0003FE44 File Offset: 0x0003E044
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.PagedDataChunk,
					DataMessageOpcode.PagedLastDataChunk
				};
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x0003FE69 File Offset: 0x0003E069
		public bool IsLastChunk
		{
			get
			{
				return this.isLastChunk;
			}
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0003FE71 File Offset: 0x0003E071
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new PagedDataMessage(data, opcode == DataMessageOpcode.PagedLastDataChunk);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0003FE81 File Offset: 0x0003E081
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = (this.isLastChunk ? DataMessageOpcode.PagedLastDataChunk : DataMessageOpcode.PagedDataChunk);
			data = base.Buffer;
		}

		// Token: 0x04000C79 RID: 3193
		private bool isLastChunk;
	}
}
