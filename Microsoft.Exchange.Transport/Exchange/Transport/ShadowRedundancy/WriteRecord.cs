using System;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x020003A0 RID: 928
	internal class WriteRecord
	{
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x000A4017 File Offset: 0x000A2217
		// (set) Token: 0x06002973 RID: 10611 RVA: 0x000A401F File Offset: 0x000A221F
		public byte[] WriteBuffer { get; private set; }

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000A4028 File Offset: 0x000A2228
		// (set) Token: 0x06002975 RID: 10613 RVA: 0x000A4030 File Offset: 0x000A2230
		public bool Eod { get; set; }

		// Token: 0x06002976 RID: 10614 RVA: 0x000A4039 File Offset: 0x000A2239
		public WriteRecord(byte[] buffer, int offset, int count, bool seenEod)
		{
			this.WriteBuffer = new byte[count];
			this.Eod = seenEod;
			Buffer.BlockCopy(buffer, offset, this.WriteBuffer, 0, count);
		}
	}
}
