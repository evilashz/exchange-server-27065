using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000085 RID: 133
	internal struct ValueIterator
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x0001E064 File Offset: 0x0001C264
		public ValueIterator(MimeStringList lines, uint linesMask)
		{
			this.lines = lines;
			this.linesMask = linesMask;
			this.lineStart = (this.lineEnd = (this.currentLine = (this.currentOffset = 0)));
			this.lineBytes = null;
			this.endLine = this.lines.Count;
			this.endOffset = 0;
			while (this.currentLine != this.endLine)
			{
				MimeString mimeString = this.lines[this.currentLine];
				if ((mimeString.Mask & this.linesMask) != 0U)
				{
					int num;
					this.lineBytes = mimeString.GetData(out this.lineStart, out num);
					this.lineEnd = this.lineStart + num;
					this.currentOffset = this.lineStart;
					return;
				}
				this.currentLine++;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001E134 File Offset: 0x0001C334
		public ValueIterator(MimeStringList lines, uint linesMask, ValuePosition startPosition, ValuePosition endPosition)
		{
			this.lines = lines;
			this.linesMask = linesMask;
			this.currentLine = startPosition.Line;
			this.currentOffset = startPosition.Offset;
			this.endLine = endPosition.Line;
			this.endOffset = endPosition.Offset;
			if (startPosition != endPosition)
			{
				int num;
				this.lineBytes = this.lines[this.currentLine].GetData(out this.lineStart, out num);
				this.lineEnd = ((this.currentLine == this.endLine) ? this.endOffset : (this.lineStart + num));
				return;
			}
			this.lineStart = (this.lineEnd = this.currentOffset);
			this.lineBytes = null;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001E1F5 File Offset: 0x0001C3F5
		public ValuePosition CurrentPosition
		{
			get
			{
				return new ValuePosition(this.currentLine, this.currentOffset);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001E208 File Offset: 0x0001C408
		public byte[] Bytes
		{
			get
			{
				return this.lineBytes;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001E210 File Offset: 0x0001C410
		public int Offset
		{
			get
			{
				return this.currentOffset;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001E218 File Offset: 0x0001C418
		public int Length
		{
			get
			{
				return this.lineEnd - this.currentOffset;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001E227 File Offset: 0x0001C427
		public int TotalLength
		{
			get
			{
				return this.lines.Length;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001E234 File Offset: 0x0001C434
		public MimeStringList Lines
		{
			get
			{
				return this.lines;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001E23C File Offset: 0x0001C43C
		public uint LinesMask
		{
			get
			{
				return this.linesMask;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001E244 File Offset: 0x0001C444
		public bool Eof
		{
			get
			{
				return this.currentLine == this.endLine && this.currentOffset == this.lineEnd;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001E264 File Offset: 0x0001C464
		public void Get(int length)
		{
			this.currentOffset += length;
			if (this.currentOffset == this.lineEnd)
			{
				this.NextLine();
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001E288 File Offset: 0x0001C488
		public int Get()
		{
			if (this.Eof)
			{
				return -1;
			}
			byte result = this.lineBytes[this.currentOffset++];
			if (this.currentOffset == this.lineEnd)
			{
				this.NextLine();
			}
			return (int)result;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001E2CD File Offset: 0x0001C4CD
		public int Pick()
		{
			if (this.Eof)
			{
				return -1;
			}
			return (int)this.lineBytes[this.currentOffset];
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001E2E8 File Offset: 0x0001C4E8
		public void Unget()
		{
			if (this.currentOffset == this.lineStart)
			{
				MimeString mimeString;
				do
				{
					mimeString = this.lines[--this.currentLine];
				}
				while ((mimeString.Mask & this.linesMask) == 0U);
				int num;
				this.lineBytes = mimeString.GetData(out this.lineStart, out num);
				this.currentOffset = (this.lineEnd = this.lineStart + num);
			}
			this.currentOffset--;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001E36A File Offset: 0x0001C56A
		public void SkipToEof()
		{
			while (!this.Eof)
			{
				this.Get(this.Length);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001E384 File Offset: 0x0001C584
		private void NextLine()
		{
			if (!this.Eof)
			{
				MimeString mimeString;
				for (;;)
				{
					this.currentLine++;
					if (this.currentLine == this.lines.Count)
					{
						break;
					}
					mimeString = this.lines[this.currentLine];
					if ((mimeString.Mask & this.linesMask) != 0U)
					{
						goto Block_2;
					}
				}
				this.lineEnd = (this.lineStart = (this.currentOffset = 0));
				return;
				Block_2:
				int num;
				this.lineBytes = mimeString.GetData(out this.lineStart, out num);
				this.currentOffset = this.lineStart;
				this.lineEnd = ((this.currentLine == this.endLine) ? this.endOffset : (this.lineStart + num));
			}
		}

		// Token: 0x040003DE RID: 990
		private MimeStringList lines;

		// Token: 0x040003DF RID: 991
		private uint linesMask;

		// Token: 0x040003E0 RID: 992
		private int currentLine;

		// Token: 0x040003E1 RID: 993
		private int currentOffset;

		// Token: 0x040003E2 RID: 994
		private int lineStart;

		// Token: 0x040003E3 RID: 995
		private int lineEnd;

		// Token: 0x040003E4 RID: 996
		private byte[] lineBytes;

		// Token: 0x040003E5 RID: 997
		private int endLine;

		// Token: 0x040003E6 RID: 998
		private int endOffset;
	}
}
