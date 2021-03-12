using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x0200025E RID: 606
	internal struct RtfRunEntry
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x000C8644 File Offset: 0x000C6844
		public RtfRunKind Kind
		{
			get
			{
				return (RtfRunKind)(this.bitFields & 61440);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x000C8653 File Offset: 0x000C6853
		public short KeywordId
		{
			get
			{
				return (short)(this.bitFields & 511);
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x000C8662 File Offset: 0x000C6862
		public bool Skip
		{
			get
			{
				return 0 != (this.bitFields & 1024);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000C8676 File Offset: 0x000C6876
		public bool Lead
		{
			get
			{
				return 0 != (this.bitFields & 512);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x000C868A File Offset: 0x000C688A
		public ushort Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x000C8692 File Offset: 0x000C6892
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x000C869A File Offset: 0x000C689A
		public bool IsSkiped
		{
			get
			{
				return this.Kind == RtfRunKind.Ignore || this.Skip;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x000C86AC File Offset: 0x000C68AC
		public bool IsSmall
		{
			get
			{
				RtfRunKind kind = this.Kind;
				return kind == RtfRunKind.Escape || kind == RtfRunKind.Zero || (kind == RtfRunKind.Text && this.length == 1);
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x000C86E5 File Offset: 0x000C68E5
		public bool IsUnicode
		{
			get
			{
				return this.Kind == RtfRunKind.Unicode;
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000C86F4 File Offset: 0x000C68F4
		internal void Reset()
		{
			this.bitFields = 0;
			this.length = 0;
			this.value = 0;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000C870B File Offset: 0x000C690B
		internal void Initialize(RtfRunKind kind, int length, int value)
		{
			this.bitFields = (ushort)kind;
			this.length = (ushort)length;
			this.value = value;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000C8724 File Offset: 0x000C6924
		internal void Initialize(RtfRunKind kind, int length, int unescaped, bool skip, bool lead)
		{
			ushort num = (ushort)kind;
			if (skip)
			{
				num |= 1024;
			}
			if (lead)
			{
				num |= 512;
			}
			this.bitFields = num;
			this.length = (ushort)length;
			this.value = unescaped;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000C8764 File Offset: 0x000C6964
		internal void InitializeKeyword(short keywordId, int value, int length, bool skip, bool firstKeyword)
		{
			ushort num = 20480 | (ushort)keywordId;
			if (skip)
			{
				num |= 1024;
			}
			if (firstKeyword)
			{
				num |= 512;
			}
			this.bitFields = num;
			this.length = (ushort)length;
			this.value = value;
		}

		// Token: 0x04001DFC RID: 7676
		private const ushort RunKindMask = 61440;

		// Token: 0x04001DFD RID: 7677
		private const ushort SkipBit = 1024;

		// Token: 0x04001DFE RID: 7678
		private const ushort LeadBit = 512;

		// Token: 0x04001DFF RID: 7679
		private const ushort KeywordIdMask = 511;

		// Token: 0x04001E00 RID: 7680
		private ushort bitFields;

		// Token: 0x04001E01 RID: 7681
		private ushort length;

		// Token: 0x04001E02 RID: 7682
		private int value;
	}
}
