using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000262 RID: 610
	internal class RtfToken
	{
		// Token: 0x06001942 RID: 6466 RVA: 0x000C8A31 File Offset: 0x000C6C31
		public RtfToken(byte[] buffer, RtfRunEntry[] runQueue)
		{
			this.dataBuffer = buffer;
			this.runQueue = runQueue;
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x000C8A47 File Offset: 0x000C6C47
		public RtfTokenId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x000C8A4F File Offset: 0x000C6C4F
		public byte[] Buffer
		{
			get
			{
				return this.dataBuffer;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x000C8A57 File Offset: 0x000C6C57
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000C8A5F File Offset: 0x000C6C5F
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x000C8A67 File Offset: 0x000C6C67
		public bool IsEmpty
		{
			get
			{
				return this.runQueueTail == 0;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x000C8A72 File Offset: 0x000C6C72
		public RtfToken.RunEnumerator Runs
		{
			get
			{
				return new RtfToken.RunEnumerator(this);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x000C8A7A File Offset: 0x000C6C7A
		public RtfToken.KeywordEnumerator Keywords
		{
			get
			{
				return new RtfToken.KeywordEnumerator(this);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x000C8A82 File Offset: 0x000C6C82
		public TextMapping TextMapping
		{
			get
			{
				return this.textMapping;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x000C8A8A File Offset: 0x000C6C8A
		public int TextCodePage
		{
			get
			{
				return this.textCodePage;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x000C8A92 File Offset: 0x000C6C92
		public RtfToken.TextReader Text
		{
			get
			{
				return new RtfToken.TextReader(this);
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x000C8A9A File Offset: 0x000C6C9A
		public RtfToken.TextEnumerator TextElements
		{
			get
			{
				return new RtfToken.TextEnumerator(this);
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x000C8AA2 File Offset: 0x000C6CA2
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x000C8AAA File Offset: 0x000C6CAA
		public bool StripZeroBytes
		{
			get
			{
				return this.stripZeroBytes;
			}
			set
			{
				this.stripZeroBytes = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000C8AB3 File Offset: 0x000C6CB3
		internal RtfRunEntry[] RunQueue
		{
			get
			{
				return this.runQueue;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000C8ABB File Offset: 0x000C6CBB
		internal int CurrentRun
		{
			get
			{
				return this.currentRun;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x000C8AC3 File Offset: 0x000C6CC3
		internal int CurrentRunOffset
		{
			get
			{
				return this.currentRunOffset;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x000C8ACB File Offset: 0x000C6CCB
		internal char[] CharBuffer
		{
			get
			{
				return this.charBuffer;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x000C8AD3 File Offset: 0x000C6CD3
		internal bool IsTextEof
		{
			get
			{
				return this.charBufferCount == 0 && this.byteBufferCount == 0 && this.currentRun == this.runQueueTail;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x000C8AF5 File Offset: 0x000C6CF5
		internal RunTextType ElementTextType
		{
			get
			{
				return this.elementTextType;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x000C8AFD File Offset: 0x000C6CFD
		internal int ElementOffset
		{
			get
			{
				return this.elementOffset;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x000C8B05 File Offset: 0x000C6D05
		internal int ElementLength
		{
			get
			{
				return this.elementLength;
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000C8B0D File Offset: 0x000C6D0D
		public static RtfTokenId TokenIdFromRunKind(RtfRunKind runKind)
		{
			if (runKind >= RtfRunKind.Text)
			{
				return RtfTokenId.Text;
			}
			return (RtfTokenId)(runKind >> 12);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000C8B1D File Offset: 0x000C6D1D
		public void Reset()
		{
			this.id = RtfTokenId.None;
			this.offset = 0;
			this.length = 0;
			this.runQueueTail = 0;
			this.textCodePage = 0;
			this.Rewind();
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000C8B48 File Offset: 0x000C6D48
		public void Initialize(RtfTokenId tokenId, int queueTail, int offset, int length)
		{
			this.id = tokenId;
			this.offset = offset;
			this.length = length;
			this.runQueueTail = queueTail;
			this.Rewind();
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000C8B6D File Offset: 0x000C6D6D
		public void SetCodePage(int codePage, TextMapping textMapping)
		{
			this.textCodePage = codePage;
			this.textMapping = textMapping;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000C8B80 File Offset: 0x000C6D80
		internal void Rewind()
		{
			this.charBufferOffet = (this.charBufferCount = 0);
			this.byteBufferOffet = (this.byteBufferCount = 0);
			this.currentRun = -1;
			this.currentRunOffset = this.offset;
			this.currentRunDelta = 0;
			this.elementOffset = (this.elementLength = 0);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000C8BD8 File Offset: 0x000C6DD8
		private bool DecodeMore()
		{
			if (this.charBuffer == null)
			{
				this.charBuffer = new char[1025];
			}
			int i = this.charBuffer.Length - 1;
			int num = 0;
			this.charBufferOffet = 0;
			while (i >= 32)
			{
				if (this.byteBufferCount != 0 || this.byteBufferOffet != 0)
				{
					Decoder decoder = this.GetDecoder();
					bool flush = this.NeedToFlushDecoderBeforeRun(this.currentRun);
					int num2;
					int num3;
					bool flag;
					decoder.Convert(this.byteBuffer, this.byteBufferOffet, this.byteBufferCount, this.charBuffer, num, i, flush, out num2, out num3, out flag);
					num += num3;
					i -= num3;
					this.byteBufferOffet += num2;
					this.byteBufferCount -= num2;
					if (!flag)
					{
						break;
					}
					this.byteBufferOffet = 0;
					if (i < 32)
					{
						break;
					}
				}
				if (this.currentRun == this.runQueueTail)
				{
					break;
				}
				if (this.currentRun == -1 || this.CurrentRunIsSkiped())
				{
					do
					{
						this.MoveToNextRun();
					}
					while (this.currentRun != this.runQueueTail && this.CurrentRunIsSkiped());
					if (this.currentRun == this.runQueueTail)
					{
						break;
					}
				}
				if (this.CurrentRunIsSmall())
				{
					while (this.CurrentRunIsSkiped() || (this.CurrentRunIsSmall() && this.CopyCurrentRunToBuffer()))
					{
						this.MoveToNextRun();
						if (this.currentRun == this.runQueueTail)
						{
							break;
						}
					}
				}
				else if (!this.CurrentRunIsUnicode())
				{
					int num4 = this.currentRunOffset + this.currentRunDelta;
					int num5 = (int)this.runQueue[this.currentRun].Length - this.currentRunDelta;
					Decoder decoder = this.GetDecoder();
					bool flush = this.NeedToFlushDecoderBeforeRun((this.currentRun + 1) % this.runQueue.Length);
					int num2;
					int num3;
					bool flag;
					decoder.Convert(this.dataBuffer, num4, num5, this.charBuffer, num, i, flush, out num2, out num3, out flag);
					num += num3;
					i -= num3;
					num4 += num2;
					num5 -= num2;
					this.currentRunDelta += num2;
					if (!flag)
					{
						break;
					}
					this.MoveToNextRun();
				}
				else
				{
					do
					{
						if (!this.CurrentRunIsSkiped())
						{
							if (!this.CurrentRunIsUnicode())
							{
								break;
							}
							int value = this.runQueue[this.currentRun].Value;
							if (i < 2)
							{
								break;
							}
							if (value > 65535)
							{
								this.charBuffer[num++] = ParseSupport.HighSurrogateCharFromUcs4(value);
								this.charBuffer[num++] = ParseSupport.LowSurrogateCharFromUcs4(value);
								i -= 2;
							}
							else
							{
								this.charBuffer[num++] = (char)value;
								i--;
							}
						}
						this.MoveToNextRun();
					}
					while (this.currentRun != this.runQueueTail);
				}
			}
			this.charBufferCount = num;
			this.charBuffer[this.charBufferCount] = '\0';
			return num != 0;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000C8E94 File Offset: 0x000C7094
		private bool MoveToNextTextElement()
		{
			if (this.charBufferCount == 0 && !this.DecodeMore())
			{
				return false;
			}
			int num = this.charBufferOffet;
			this.elementOffset = num;
			char c = this.charBuffer[num];
			if (c > ' ' && c != '\u00a0')
			{
				this.elementTextType = RunTextType.NonSpace;
				do
				{
					c = this.charBuffer[++num];
					if (c <= ' ')
					{
						break;
					}
				}
				while (c != '\u00a0');
			}
			else if (c == ' ')
			{
				this.elementTextType = RunTextType.Space;
				while (this.charBuffer[++num] == ' ')
				{
				}
			}
			else
			{
				char c2 = c;
				switch (c2)
				{
				case '\t':
				case '\n':
				case '\v':
				case '\f':
				case '\r':
					this.elementTextType = RunTextType.UnusualWhitespace;
					while (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(c = this.charBuffer[++num])))
					{
						if (c == ' ')
						{
							break;
						}
					}
					break;
				default:
					if (c2 != '\u00a0')
					{
						this.elementTextType = RunTextType.NonSpace;
						while (ParseSupport.ControlCharacter(ParseSupport.GetCharClass(this.charBuffer[++num])))
						{
						}
					}
					else if (this.textMapping == TextMapping.Unicode)
					{
						this.elementTextType = RunTextType.Nbsp;
						while (this.charBuffer[++num] == '\u00a0')
						{
						}
					}
					else
					{
						this.elementTextType = RunTextType.NonSpace;
						do
						{
							c = this.charBuffer[++num];
							if (c <= ' ')
							{
								break;
							}
						}
						while (c != '\u00a0');
					}
					break;
				}
			}
			this.elementLength = num - this.elementOffset;
			this.charBufferOffet = num;
			this.charBufferCount -= this.elementLength;
			return true;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000C901C File Offset: 0x000C721C
		private int WriteTo(ITextSink sink)
		{
			int num = 0;
			while (this.charBufferCount != 0 || this.DecodeMore())
			{
				sink.Write(this.charBuffer, this.charBufferOffet, this.charBufferCount);
				this.charBufferOffet = 0;
				this.charBufferCount = 0;
				num += this.charBufferCount;
			}
			return num;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000C9070 File Offset: 0x000C7270
		private void OutputTextElements(TextOutput output, bool treatNbspAsBreakable)
		{
			int num = 0;
			char c = '\0';
			for (;;)
			{
				if (this.charBufferCount == 0)
				{
					if (!this.DecodeMore())
					{
						break;
					}
					num = this.charBufferOffet;
					c = this.charBuffer[num];
				}
				int num2 = num;
				if (c > ' ' && c != '\u00a0')
				{
					do
					{
						c = this.charBuffer[++num];
					}
					while (c > ' ' && c != '\u00a0');
					output.OutputNonspace(this.charBuffer, num2, num - num2, this.textMapping);
				}
				else if (c == ' ')
				{
					do
					{
						c = this.charBuffer[++num];
					}
					while (c == ' ');
					output.OutputSpace(num - num2);
				}
				else
				{
					char c2 = c;
					if (c2 != '\0')
					{
						switch (c2)
						{
						case '\t':
						case '\n':
						case '\v':
						case '\f':
						case '\r':
							do
							{
								c = this.charBuffer[++num];
							}
							while (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(c)) && c != ' ');
							output.OutputSpace(1);
							break;
						default:
							if (c2 != '\u00a0')
							{
								do
								{
									c = this.charBuffer[++num];
								}
								while (ParseSupport.ControlCharacter(ParseSupport.GetCharClass(c)));
								output.OutputNonspace(this.charBuffer, num2, num - num2, this.textMapping);
							}
							else if (this.textMapping == TextMapping.Unicode)
							{
								do
								{
									c = this.charBuffer[++num];
								}
								while (c == '\u00a0');
								if (treatNbspAsBreakable)
								{
									output.OutputSpace(num - num2);
								}
								else
								{
									output.OutputNbsp(num - num2);
								}
							}
							else
							{
								do
								{
									c = this.charBuffer[++num];
								}
								while (c > ' ');
								output.OutputNonspace(this.charBuffer, num2, num - num2, this.textMapping);
							}
							break;
						}
					}
				}
				this.charBufferOffet = num;
				this.charBufferCount -= num - num2;
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000C9214 File Offset: 0x000C7414
		private bool NeedToFlushDecoderBeforeRun(int run)
		{
			return run == this.runQueueTail || this.runQueue[run].Kind == RtfRunKind.Unicode || this.runQueue[run].Kind == RtfRunKind.Ignore;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000C9250 File Offset: 0x000C7450
		private Decoder GetDecoder()
		{
			if (this.mruDecoders == null)
			{
				this.mruDecoders = new RtfToken.DecoderMruEntry[4];
				this.mruDecodersLastIndex = this.mruDecoders.Length - 1;
			}
			if (this.textCodePage == 0)
			{
				this.textCodePage = 1252;
			}
			if (this.mruDecoders[this.mruDecodersLastIndex].CodePage == this.textCodePage)
			{
				return this.mruDecoders[this.mruDecodersLastIndex].Decoder;
			}
			for (int i = 0; i < this.mruDecoders.Length; i++)
			{
				if (this.mruDecoders[i].CodePage == this.textCodePage)
				{
					this.mruDecodersLastIndex = i;
					return this.mruDecoders[i].Decoder;
				}
			}
			Decoder decoder = null;
			if (this.decoderCache != null && this.decoderCache.ContainsKey(this.textCodePage))
			{
				decoder = this.decoderCache[this.textCodePage];
			}
			if (decoder == null)
			{
				int num = this.textCodePage;
				if (num == 42)
				{
					num = 28591;
				}
				Encoding encoding = Charset.GetEncoding(num);
				decoder = encoding.GetDecoder();
			}
			this.mruDecodersLastIndex = (this.mruDecodersLastIndex + 1) % this.mruDecoders.Length;
			if (this.mruDecoders[this.mruDecodersLastIndex].Decoder != null)
			{
				if (this.decoderCache == null)
				{
					this.decoderCache = new Dictionary<int, Decoder>();
				}
				if (!this.decoderCache.ContainsKey(this.mruDecoders[this.mruDecodersLastIndex].CodePage))
				{
					this.decoderCache[this.mruDecoders[this.mruDecodersLastIndex].CodePage] = this.mruDecoders[this.mruDecodersLastIndex].Decoder;
				}
			}
			this.mruDecoders[this.mruDecodersLastIndex].Decoder = decoder;
			this.mruDecoders[this.mruDecodersLastIndex].CodePage = this.textCodePage;
			return decoder;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000C9430 File Offset: 0x000C7630
		private void MoveToNextRun()
		{
			if (this.currentRun >= 0)
			{
				this.currentRunOffset += (int)this.runQueue[this.currentRun].Length;
			}
			this.currentRunDelta = 0;
			this.currentRun++;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000C947E File Offset: 0x000C767E
		private bool CurrentRunIsSkiped()
		{
			return this.runQueue[this.currentRun].IsSkiped;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000C9496 File Offset: 0x000C7696
		private bool CurrentRunIsSmall()
		{
			return this.runQueue[this.currentRun].IsSmall;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000C94AE File Offset: 0x000C76AE
		private bool CurrentRunIsUnicode()
		{
			return this.runQueue[this.currentRun].IsUnicode;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000C94C8 File Offset: 0x000C76C8
		private bool CopyCurrentRunToBuffer()
		{
			if (this.byteBuffer == null)
			{
				this.byteBuffer = new byte[256];
			}
			if (this.byteBuffer.Length == this.byteBufferCount)
			{
				return false;
			}
			RtfRunKind kind = this.runQueue[this.currentRun].Kind;
			if (kind != RtfRunKind.Text)
			{
				if (kind == RtfRunKind.Escape)
				{
					this.byteBuffer[this.byteBufferOffet + this.byteBufferCount] = (byte)this.runQueue[this.currentRun].Value;
					this.byteBufferCount++;
					return true;
				}
				if (kind == RtfRunKind.Zero)
				{
					int num = Math.Min((int)this.runQueue[this.currentRun].Length - this.currentRunDelta, this.byteBuffer.Length - this.byteBufferCount);
					if (!this.stripZeroBytes)
					{
						for (int i = 0; i < num; i++)
						{
							this.byteBuffer[this.byteBufferOffet + this.byteBufferCount] = 32;
							this.byteBufferCount++;
						}
					}
					this.currentRunDelta += num;
				}
			}
			else
			{
				int num = Math.Min((int)this.runQueue[this.currentRun].Length - this.currentRunDelta, this.byteBuffer.Length - this.byteBufferCount);
				System.Buffer.BlockCopy(this.dataBuffer, this.currentRunOffset + this.currentRunDelta, this.byteBuffer, this.byteBufferOffet + this.byteBufferCount, num);
				this.byteBufferCount += num;
				this.currentRunDelta += num;
			}
			return this.currentRunDelta == (int)this.runQueue[this.currentRun].Length;
		}

		// Token: 0x04001E06 RID: 7686
		private RtfTokenId id;

		// Token: 0x04001E07 RID: 7687
		private byte[] dataBuffer;

		// Token: 0x04001E08 RID: 7688
		private int offset;

		// Token: 0x04001E09 RID: 7689
		private int length;

		// Token: 0x04001E0A RID: 7690
		private RtfRunEntry[] runQueue;

		// Token: 0x04001E0B RID: 7691
		private int runQueueTail;

		// Token: 0x04001E0C RID: 7692
		private int textCodePage;

		// Token: 0x04001E0D RID: 7693
		private TextMapping textMapping;

		// Token: 0x04001E0E RID: 7694
		private int currentRun;

		// Token: 0x04001E0F RID: 7695
		private int currentRunOffset;

		// Token: 0x04001E10 RID: 7696
		private int currentRunDelta;

		// Token: 0x04001E11 RID: 7697
		private byte[] byteBuffer;

		// Token: 0x04001E12 RID: 7698
		private int byteBufferOffet;

		// Token: 0x04001E13 RID: 7699
		private int byteBufferCount;

		// Token: 0x04001E14 RID: 7700
		private char[] charBuffer;

		// Token: 0x04001E15 RID: 7701
		private int charBufferOffet;

		// Token: 0x04001E16 RID: 7702
		private int charBufferCount;

		// Token: 0x04001E17 RID: 7703
		private RunTextType elementTextType;

		// Token: 0x04001E18 RID: 7704
		private int elementOffset;

		// Token: 0x04001E19 RID: 7705
		private int elementLength;

		// Token: 0x04001E1A RID: 7706
		private RtfToken.DecoderMruEntry[] mruDecoders;

		// Token: 0x04001E1B RID: 7707
		private int mruDecodersLastIndex;

		// Token: 0x04001E1C RID: 7708
		private bool stripZeroBytes;

		// Token: 0x04001E1D RID: 7709
		private Dictionary<int, Decoder> decoderCache;

		// Token: 0x02000263 RID: 611
		internal struct RunEnumerator
		{
			// Token: 0x06001968 RID: 6504 RVA: 0x000C9683 File Offset: 0x000C7883
			internal RunEnumerator(RtfToken token)
			{
				this.token = token;
			}

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x06001969 RID: 6505 RVA: 0x000C968C File Offset: 0x000C788C
			public int Count
			{
				get
				{
					return this.token.runQueueTail;
				}
			}

			// Token: 0x17000681 RID: 1665
			// (get) Token: 0x0600196A RID: 6506 RVA: 0x000C9699 File Offset: 0x000C7899
			public RtfRun Current
			{
				get
				{
					return new RtfRun(this.token);
				}
			}

			// Token: 0x0600196B RID: 6507 RVA: 0x000C96A6 File Offset: 0x000C78A6
			public bool MoveNext()
			{
				if (this.token.currentRun != this.token.runQueueTail)
				{
					this.token.MoveToNextRun();
					if (this.token.currentRun != this.token.runQueueTail)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600196C RID: 6508 RVA: 0x000C96E6 File Offset: 0x000C78E6
			public void Rewind()
			{
				this.token.Rewind();
			}

			// Token: 0x0600196D RID: 6509 RVA: 0x000C96F3 File Offset: 0x000C78F3
			public RtfToken.RunEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600196E RID: 6510 RVA: 0x000C96FB File Offset: 0x000C78FB
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001E1E RID: 7710
			private RtfToken token;
		}

		// Token: 0x02000264 RID: 612
		internal struct KeywordEnumerator
		{
			// Token: 0x0600196F RID: 6511 RVA: 0x000C96FD File Offset: 0x000C78FD
			internal KeywordEnumerator(RtfToken token)
			{
				this.token = token;
			}

			// Token: 0x17000682 RID: 1666
			// (get) Token: 0x06001970 RID: 6512 RVA: 0x000C9706 File Offset: 0x000C7906
			public int Count
			{
				get
				{
					return this.token.runQueueTail;
				}
			}

			// Token: 0x17000683 RID: 1667
			// (get) Token: 0x06001971 RID: 6513 RVA: 0x000C9713 File Offset: 0x000C7913
			public RtfKeyword Current
			{
				get
				{
					return new RtfKeyword(this.token);
				}
			}

			// Token: 0x06001972 RID: 6514 RVA: 0x000C9720 File Offset: 0x000C7920
			public bool MoveNext()
			{
				if (this.token.currentRun != this.token.runQueueTail)
				{
					this.token.MoveToNextRun();
					if (this.token.currentRun != this.token.runQueueTail)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001973 RID: 6515 RVA: 0x000C9760 File Offset: 0x000C7960
			public void Rewind()
			{
				this.token.Rewind();
			}

			// Token: 0x06001974 RID: 6516 RVA: 0x000C976D File Offset: 0x000C796D
			public RtfToken.KeywordEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06001975 RID: 6517 RVA: 0x000C9775 File Offset: 0x000C7975
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001E1F RID: 7711
			private RtfToken token;
		}

		// Token: 0x02000265 RID: 613
		internal struct TextReader
		{
			// Token: 0x06001976 RID: 6518 RVA: 0x000C9777 File Offset: 0x000C7977
			internal TextReader(RtfToken token)
			{
				this.token = token;
			}

			// Token: 0x06001977 RID: 6519 RVA: 0x000C9780 File Offset: 0x000C7980
			public int Read(char[] buffer, int offset, int count)
			{
				int num = offset;
				if (!this.token.IsTextEof)
				{
					do
					{
						if (this.token.charBufferCount != 0)
						{
							int num2 = Math.Min(count, this.token.charBufferCount);
							System.Buffer.BlockCopy(this.token.charBuffer, this.token.charBufferOffet * 2, buffer, offset * 2, num2 * 2);
							offset += num2;
							count -= num2;
							this.token.charBufferOffet += num2;
							this.token.charBufferCount -= num2;
							if (count == 0)
							{
								break;
							}
						}
					}
					while (this.token.DecodeMore());
				}
				return offset - num;
			}

			// Token: 0x06001978 RID: 6520 RVA: 0x000C9828 File Offset: 0x000C7A28
			public void Rewind()
			{
				this.token.Rewind();
			}

			// Token: 0x06001979 RID: 6521 RVA: 0x000C9835 File Offset: 0x000C7A35
			public int WriteTo(ITextSink sink)
			{
				return this.token.WriteTo(sink);
			}

			// Token: 0x0600197A RID: 6522 RVA: 0x000C9843 File Offset: 0x000C7A43
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001E20 RID: 7712
			private RtfToken token;
		}

		// Token: 0x02000266 RID: 614
		internal struct TextEnumerator
		{
			// Token: 0x0600197B RID: 6523 RVA: 0x000C9845 File Offset: 0x000C7A45
			internal TextEnumerator(RtfToken token)
			{
				this.token = token;
			}

			// Token: 0x17000684 RID: 1668
			// (get) Token: 0x0600197C RID: 6524 RVA: 0x000C984E File Offset: 0x000C7A4E
			public RtfTextElement Current
			{
				get
				{
					return new RtfTextElement(this.token);
				}
			}

			// Token: 0x0600197D RID: 6525 RVA: 0x000C985C File Offset: 0x000C7A5C
			public bool MoveNext()
			{
				return this.token.MoveToNextTextElement();
			}

			// Token: 0x0600197E RID: 6526 RVA: 0x000C9878 File Offset: 0x000C7A78
			public bool MoveNext(bool skipAllWhitespace)
			{
				return this.token.MoveToNextTextElement();
			}

			// Token: 0x0600197F RID: 6527 RVA: 0x000C9892 File Offset: 0x000C7A92
			public void Rewind()
			{
				this.token.Rewind();
			}

			// Token: 0x06001980 RID: 6528 RVA: 0x000C989F File Offset: 0x000C7A9F
			public RtfToken.TextEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06001981 RID: 6529 RVA: 0x000C98A7 File Offset: 0x000C7AA7
			public void OutputTextElements(TextOutput output, bool treatNbspAsBreakable)
			{
				this.token.OutputTextElements(output, treatNbspAsBreakable);
			}

			// Token: 0x06001982 RID: 6530 RVA: 0x000C98B6 File Offset: 0x000C7AB6
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x04001E21 RID: 7713
			private RtfToken token;
		}

		// Token: 0x02000267 RID: 615
		private struct DecoderMruEntry
		{
			// Token: 0x04001E22 RID: 7714
			public int CodePage;

			// Token: 0x04001E23 RID: 7715
			public Decoder Decoder;
		}
	}
}
