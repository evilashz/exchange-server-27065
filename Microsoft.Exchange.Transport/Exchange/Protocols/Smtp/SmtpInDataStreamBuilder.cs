using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F6 RID: 1270
	internal class SmtpInDataStreamBuilder : SmtpInStreamBuilderBase
	{
		// Token: 0x06003A96 RID: 14998 RVA: 0x000F3684 File Offset: 0x000F1884
		public SmtpInDataStreamBuilder()
		{
			base.EohPos = -1L;
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06003A97 RID: 14999 RVA: 0x000F369B File Offset: 0x000F189B
		public ParserState State
		{
			get
			{
				return this.parserState;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x000F36A3 File Offset: 0x000F18A3
		public override bool IsEodSeen
		{
			get
			{
				return this.parserState == ParserState.EOD;
			}
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000F36AE File Offset: 0x000F18AE
		public override void Reset()
		{
			base.Reset();
			this.parserState = ParserState.LF1;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000F36C0 File Offset: 0x000F18C0
		public override bool Write(byte[] data, int offset, int numBytes, out int numBytesConsumed)
		{
			bool result = false;
			int num = offset;
			int i = offset;
			int num2 = offset + numBytes;
			if (this.parserState > ParserState.EOHCR2)
			{
				throw new InvalidOperationException("SmtpInDataParser is in an unknown state");
			}
			if (this.parserState == ParserState.EOD)
			{
				throw new InvalidOperationException("SmtpInDataParser received data after EOD");
			}
			while (i < num2)
			{
				switch (this.parserState)
				{
				case ParserState.NONE:
				{
					int num3 = Array.IndexOf<byte>(data, 13, i, num2 - i);
					if (num3 >= 0)
					{
						this.parserState = ParserState.CR1;
						i = num3 + 1;
						continue;
					}
					i = num2;
					continue;
				}
				case ParserState.CR1:
					if (data[i] == 10)
					{
						this.parserState = ParserState.LF1;
					}
					else if (data[i] == 13)
					{
						this.parserState = ParserState.CR1;
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					i++;
					continue;
				case ParserState.LF1:
					if (data[i] == 46)
					{
						this.parserState = ParserState.DOT;
					}
					else if (data[i] == 13)
					{
						if (base.EohPos != -1L)
						{
							this.parserState = ParserState.CR1;
						}
						else
						{
							this.parserState = ParserState.EOHCR2;
						}
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					i++;
					continue;
				case ParserState.DOT:
					if (data[i] == 13)
					{
						this.parserState = ParserState.CR2;
					}
					else if (!base.IsDiscardingData)
					{
						int num4 = i - offset - 1;
						if (num4 > 0)
						{
							base.Write(data, offset, num4);
						}
						this.parserState = ParserState.NONE;
						offset = i;
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					i++;
					continue;
				case ParserState.CR2:
					if (data[i] == 10)
					{
						this.parserState = ParserState.EOD;
						result = true;
						num2 = i;
					}
					else if (!base.IsDiscardingData)
					{
						int num5 = i - offset - 2;
						if (num5 > 0)
						{
							base.Write(data, offset, num5);
						}
						base.Write(SmtpInParser.EodSequence, 3, 1);
						if (data[i] == 13)
						{
							this.parserState = ParserState.CR1;
						}
						else
						{
							this.parserState = ParserState.NONE;
						}
						offset = i;
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					i++;
					continue;
				case ParserState.EOHCR2:
					if (data[i] == 10)
					{
						base.EohPos = base.TotalBytesRead + (long)i - (long)num - 1L;
						this.parserState = ParserState.LF1;
					}
					else if (data[i] == 13)
					{
						this.parserState = ParserState.CR1;
					}
					else
					{
						this.parserState = ParserState.NONE;
					}
					i++;
					continue;
				}
				throw new InvalidOperationException("SmtpInDataParser got into an unknown state");
			}
			numBytesConsumed = i - num;
			base.TotalBytesRead += (long)numBytesConsumed;
			if (!base.IsDiscardingData)
			{
				int num6 = i - offset;
				if (this.parserState >= ParserState.DOT && this.parserState <= ParserState.EOD)
				{
					num6 -= this.parserState - ParserState.DOT + 1;
				}
				if (num6 > 0)
				{
					base.Write(data, offset, num6);
				}
			}
			return result;
		}

		// Token: 0x04001D7E RID: 7550
		private ParserState parserState = ParserState.LF1;
	}
}
