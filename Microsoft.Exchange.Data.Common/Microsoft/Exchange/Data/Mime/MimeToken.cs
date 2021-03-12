using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000058 RID: 88
	internal struct MimeToken
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00011588 File Offset: 0x0000F788
		public MimeToken(MimeTokenId id, int length)
		{
			this.Id = id;
			this.Length = (short)length;
		}

		// Token: 0x0400028C RID: 652
		public MimeTokenId Id;

		// Token: 0x0400028D RID: 653
		public short Length;
	}
}
