using System;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005BF RID: 1471
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversionLimits : ICloneable
	{
		// Token: 0x06003C4B RID: 15435 RVA: 0x000F7F48 File Offset: 0x000F6148
		public ConversionLimits(bool isInboundConversion)
		{
			this.maxMimeTextHeaderLength = 2000;
			this.maxMimeSubjectLength = 255;
			this.maxSize = int.MaxValue;
			this.maxMimeRecipients = 12288;
			this.maxBodyPartsTotal = 250;
			this.maxEmbeddedMessageDepth = (isInboundConversion ? 30 : 100);
			this.exemptPFReplicationMessages = true;
			this.mimeLimits = MimeLimits.Default;
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x000F7FB4 File Offset: 0x000F61B4
		public ConversionLimits(ConversionLimits origin)
		{
			this.maxMimeTextHeaderLength = origin.maxMimeTextHeaderLength;
			this.maxMimeSubjectLength = origin.maxMimeSubjectLength;
			this.maxSize = origin.maxSize;
			this.maxMimeRecipients = origin.maxMimeRecipients;
			this.maxBodyPartsTotal = origin.maxBodyPartsTotal;
			this.maxEmbeddedMessageDepth = origin.maxEmbeddedMessageDepth;
			this.exemptPFReplicationMessages = origin.exemptPFReplicationMessages;
			this.mimeLimits = origin.mimeLimits;
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x000F8028 File Offset: 0x000F6228
		public override string ToString()
		{
			return string.Format("ConversionLimits:\r\n- maxMimeTextHeaderLength: {0}\r\n- maxMimeSubjectLength: {1}\r\n- maxSize: {2}\r\n- maxMimeRecipients: {3}\r\n- maxBodyPartsTotal: {4}\r\n- maxEmbeddedMessageDepth: {5}\r\n- exemptPFReplicationMessages: {6}\r\n", new object[]
			{
				this.maxMimeTextHeaderLength,
				this.maxMimeSubjectLength,
				this.maxSize,
				this.maxMimeRecipients,
				this.maxBodyPartsTotal,
				this.maxEmbeddedMessageDepth,
				this.exemptPFReplicationMessages
			});
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x000F80A9 File Offset: 0x000F62A9
		public object Clone()
		{
			return new ConversionLimits(this);
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000F80B1 File Offset: 0x000F62B1
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000F80B9 File Offset: 0x000F62B9
		public int MaxMimeTextHeaderLength
		{
			get
			{
				return this.maxMimeTextHeaderLength;
			}
			set
			{
				this.SetMaxMimeTextHeaderLength(value);
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000F80C2 File Offset: 0x000F62C2
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000F80CA File Offset: 0x000F62CA
		public int MaxMimeSubjectLength
		{
			get
			{
				return this.maxMimeSubjectLength;
			}
			set
			{
				this.SetMaxMimeSubjectLength(value);
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000F80D3 File Offset: 0x000F62D3
		// (set) Token: 0x06003C54 RID: 15444 RVA: 0x000F80DB File Offset: 0x000F62DB
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
			set
			{
				this.SetMaxSize(value);
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000F80E4 File Offset: 0x000F62E4
		// (set) Token: 0x06003C56 RID: 15446 RVA: 0x000F80EC File Offset: 0x000F62EC
		public int MaxMimeRecipients
		{
			get
			{
				return this.maxMimeRecipients;
			}
			set
			{
				this.SetMaxMimeRecipients(value);
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x000F80F5 File Offset: 0x000F62F5
		// (set) Token: 0x06003C58 RID: 15448 RVA: 0x000F80FD File Offset: 0x000F62FD
		public int MaxBodyPartsTotal
		{
			get
			{
				return this.maxBodyPartsTotal;
			}
			set
			{
				this.SetMaxBodyPartsTotal(value);
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000F8106 File Offset: 0x000F6306
		// (set) Token: 0x06003C5A RID: 15450 RVA: 0x000F810E File Offset: 0x000F630E
		public int MaxEmbeddedMessageDepth
		{
			get
			{
				return this.maxEmbeddedMessageDepth;
			}
			set
			{
				this.SetMaxEmbeddedMessageDepth(value);
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000F8117 File Offset: 0x000F6317
		// (set) Token: 0x06003C5C RID: 15452 RVA: 0x000F811F File Offset: 0x000F631F
		public bool ExemptPFReplicationMessages
		{
			get
			{
				return this.exemptPFReplicationMessages;
			}
			set
			{
				this.exemptPFReplicationMessages = value;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000F8128 File Offset: 0x000F6328
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x000F8130 File Offset: 0x000F6330
		public MimeLimits MimeLimits
		{
			get
			{
				return this.mimeLimits;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("MimeLimits");
				}
				this.mimeLimits = value;
			}
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x000F8147 File Offset: 0x000F6347
		private void SetMaxMimeTextHeaderLength(int maxMimeTextHeaderLength)
		{
			if (maxMimeTextHeaderLength < 78)
			{
				throw new ArgumentOutOfRangeException("maxMimeTextHeaderLength");
			}
			this.maxMimeTextHeaderLength = maxMimeTextHeaderLength;
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000F8160 File Offset: 0x000F6360
		private void SetMaxMimeSubjectLength(int maxMimeSubjectLength)
		{
			if (maxMimeSubjectLength < 78)
			{
				throw new ArgumentOutOfRangeException("maxMimeSubjectLength");
			}
			this.maxMimeSubjectLength = maxMimeSubjectLength;
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x000F8179 File Offset: 0x000F6379
		private void SetMaxSize(int maxSize)
		{
			if (maxSize < 1024)
			{
				throw new ArgumentOutOfRangeException("maxSize");
			}
			this.maxSize = maxSize;
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x000F8195 File Offset: 0x000F6395
		private void SetMaxMimeRecipients(int maxMimeRecipients)
		{
			if (maxMimeRecipients < 0)
			{
				throw new ArgumentOutOfRangeException("maxMimeRecipients");
			}
			this.maxMimeRecipients = maxMimeRecipients;
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x000F81AD File Offset: 0x000F63AD
		private void SetMaxBodyPartsTotal(int maxBodyPartsTotal)
		{
			if (maxBodyPartsTotal < 1)
			{
				throw new ArgumentOutOfRangeException("maxBodyPartsTotal");
			}
			this.maxBodyPartsTotal = maxBodyPartsTotal;
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x000F81C5 File Offset: 0x000F63C5
		private void SetMaxEmbeddedMessageDepth(int maxEmbeddedMessageDepth)
		{
			if (maxEmbeddedMessageDepth < 0)
			{
				throw new ArgumentOutOfRangeException("maxEmbeddedMessageDepth");
			}
			this.maxEmbeddedMessageDepth = maxEmbeddedMessageDepth;
		}

		// Token: 0x04002010 RID: 8208
		private int maxMimeTextHeaderLength;

		// Token: 0x04002011 RID: 8209
		private int maxMimeSubjectLength;

		// Token: 0x04002012 RID: 8210
		private int maxSize;

		// Token: 0x04002013 RID: 8211
		private int maxMimeRecipients;

		// Token: 0x04002014 RID: 8212
		private int maxBodyPartsTotal;

		// Token: 0x04002015 RID: 8213
		private int maxEmbeddedMessageDepth;

		// Token: 0x04002016 RID: 8214
		private bool exemptPFReplicationMessages;

		// Token: 0x04002017 RID: 8215
		private MimeLimits mimeLimits;
	}
}
