using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000243 RID: 579
	internal abstract class MessageWithBuffer : DataMessageBase
	{
		// Token: 0x06001E48 RID: 7752 RVA: 0x0003ECCB File Offset: 0x0003CECB
		public MessageWithBuffer(byte[] buffer)
		{
			this.buffer = buffer;
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x0003ECDA File Offset: 0x0003CEDA
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0003ECE2 File Offset: 0x0003CEE2
		protected override int GetSizeInternal()
		{
			if (this.buffer == null)
			{
				return 0;
			}
			return this.buffer.Length;
		}

		// Token: 0x04000C61 RID: 3169
		private byte[] buffer;
	}
}
