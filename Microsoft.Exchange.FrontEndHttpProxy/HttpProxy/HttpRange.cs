using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200008A RID: 138
	internal class HttpRange
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00019388 File Offset: 0x00017588
		public HttpRange(long firstBytePosition, long lastBytePosition)
		{
			this.FirstBytePosition = firstBytePosition;
			this.LastBytePosition = lastBytePosition;
			if (this.HasFirstBytePosition && this.HasLastBytePosition)
			{
				if (this.FirstBytePosition > this.LastBytePosition)
				{
					throw new ArgumentOutOfRangeException("firstBytePosition", "FirstBytePosition cannot be larger than LastBytePosition");
				}
			}
			else if (!this.HasFirstBytePosition && !this.HasLastBytePosition && !this.HasSuffixLength)
			{
				throw new ArgumentOutOfRangeException("firstBytePosition", "At least firstBytePosition or lastBytePosition must be larger than or equal to 0.");
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000193FF File Offset: 0x000175FF
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00019407 File Offset: 0x00017607
		public long FirstBytePosition { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00019410 File Offset: 0x00017610
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00019418 File Offset: 0x00017618
		public long LastBytePosition { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00019421 File Offset: 0x00017621
		public long SuffixLength
		{
			get
			{
				return this.LastBytePosition;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00019429 File Offset: 0x00017629
		public bool HasFirstBytePosition
		{
			get
			{
				return this.FirstBytePosition >= 0L;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00019438 File Offset: 0x00017638
		public bool HasLastBytePosition
		{
			get
			{
				return this.HasFirstBytePosition && this.LastBytePosition >= 0L;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00019451 File Offset: 0x00017651
		public bool HasSuffixLength
		{
			get
			{
				return this.FirstBytePosition < 0L && this.LastBytePosition >= 0L;
			}
		}
	}
}
