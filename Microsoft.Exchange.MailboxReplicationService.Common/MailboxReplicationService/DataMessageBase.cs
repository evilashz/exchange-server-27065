using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000242 RID: 578
	internal abstract class DataMessageBase : IDataMessage
	{
		// Token: 0x06001E43 RID: 7747 RVA: 0x0003ECAD File Offset: 0x0003CEAD
		public DataMessageBase()
		{
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0003ECB5 File Offset: 0x0003CEB5
		int IDataMessage.GetSize()
		{
			return this.GetSizeInternal();
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0003ECBD File Offset: 0x0003CEBD
		void IDataMessage.Serialize(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			this.SerializeInternal(useCompression, out opcode, out data);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0003ECC8 File Offset: 0x0003CEC8
		protected virtual int GetSizeInternal()
		{
			return 0;
		}

		// Token: 0x06001E47 RID: 7751
		protected abstract void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data);
	}
}
