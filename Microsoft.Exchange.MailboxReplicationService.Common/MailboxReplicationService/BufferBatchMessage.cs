using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000244 RID: 580
	internal class BufferBatchMessage : MessageWithBuffer
	{
		// Token: 0x06001E4B RID: 7755 RVA: 0x0003ECF6 File Offset: 0x0003CEF6
		public BufferBatchMessage(byte[] data, bool flushAfterImport) : base(data)
		{
			this.flushAfterImport = flushAfterImport;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0003ED08 File Offset: 0x0003CF08
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.BufferBatch,
					DataMessageOpcode.BufferBatchWithFlush
				};
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x0003ED2D File Offset: 0x0003CF2D
		public bool FlushAfterImport
		{
			get
			{
				return this.flushAfterImport;
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0003ED35 File Offset: 0x0003CF35
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new BufferBatchMessage(data, opcode == DataMessageOpcode.BufferBatchWithFlush);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0003ED45 File Offset: 0x0003CF45
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = (this.flushAfterImport ? DataMessageOpcode.BufferBatchWithFlush : DataMessageOpcode.BufferBatch);
			data = base.Buffer;
		}

		// Token: 0x04000C62 RID: 3170
		private bool flushAfterImport;
	}
}
