using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000226 RID: 550
	public class HtmlReader : IRestartable, IResultsFeedback, IDisposable
	{
		// Token: 0x06001660 RID: 5728 RVA: 0x000B0600 File Offset: 0x000AE800
		public HtmlReader(Stream input, Encoding inputEncoding)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (!input.CanRead)
			{
				throw new ArgumentException("input stream must support reading");
			}
			this.input = input;
			this.inputEncoding = inputEncoding;
			this.state = HtmlReader.State.Begin;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000B0657 File Offset: 0x000AE857
		public HtmlReader(TextReader input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.input = input;
			this.inputEncoding = Encoding.Unicode;
			this.state = HtmlReader.State.Begin;
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x000B0694 File Offset: 0x000AE894
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x000B069C File Offset: 0x000AE89C
		public Encoding InputEncoding
		{
			get
			{
				return this.inputEncoding;
			}
			set
			{
				this.AssertNotLocked();
				this.inputEncoding = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x000B06AB File Offset: 0x000AE8AB
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x000B06B3 File Offset: 0x000AE8B3
		public bool DetectEncodingFromByteOrderMark
		{
			get
			{
				return this.detectEncodingFromByteOrderMark;
			}
			set
			{
				this.AssertNotLocked();
				this.detectEncodingFromByteOrderMark = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x000B06C2 File Offset: 0x000AE8C2
		// (set) Token: 0x06001667 RID: 5735 RVA: 0x000B06CA File Offset: 0x000AE8CA
		public bool NormalizeHtml
		{
			get
			{
				return this.normalizeInputHtml;
			}
			set
			{
				this.AssertNotLocked();
				this.normalizeInputHtml = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x000B06D9 File Offset: 0x000AE8D9
		public HtmlTokenKind TokenKind
		{
			get
			{
				this.AssertInToken();
				return this.tokenKind;
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000B06E8 File Offset: 0x000AE8E8
		public bool ReadNextToken()
		{
			this.AssertNotDisposed();
			if (this.state == HtmlReader.State.EndOfFile)
			{
				return false;
			}
			if (!this.locked)
			{
				this.InitializeAndLock();
			}
			if (this.state == HtmlReader.State.Text)
			{
				for (;;)
				{
					this.ParseToken();
					if (this.parserTokenId != HtmlTokenId.Text)
					{
						if (!this.literalTags || this.parserTokenId != HtmlTokenId.Tag)
						{
							break;
						}
						if (this.parserToken.TagIndex >= HtmlTagIndex.Unknown)
						{
							break;
						}
					}
				}
			}
			else if (this.state >= HtmlReader.State.SpecialTag)
			{
				while (!this.parserToken.IsTagEnd)
				{
					this.ParseToken();
				}
				if (this.parserToken.TagIndex > HtmlTagIndex.Unknown && !this.parserToken.IsEndTag && HtmlDtd.tags[(int)this.parserToken.TagIndex].Scope != HtmlDtd.TagScope.EMPTY && !this.parserToken.IsEmptyScope)
				{
					this.depth++;
				}
				if (!this.parserToken.IsEndTag && (byte)(HtmlDtd.tags[(int)this.parserToken.TagIndex].Literal & HtmlDtd.Literal.Tags) != 0)
				{
					this.literalTags = true;
				}
				this.ParseToken();
			}
			else
			{
				if (this.state == HtmlReader.State.OverlappedClose)
				{
					this.depth -= this.parserToken.Argument;
				}
				this.ParseToken();
			}
			for (;;)
			{
				switch (this.parserTokenId)
				{
				case HtmlTokenId.Text:
					goto IL_15A;
				case HtmlTokenId.EncodingChange:
					this.ParseToken();
					continue;
				case HtmlTokenId.Tag:
					if (this.parserToken.TagIndex < HtmlTagIndex.Unknown)
					{
						goto Block_17;
					}
					if (this.parserToken.TagIndex == HtmlTagIndex.TC)
					{
						this.ParseToken();
						continue;
					}
					goto IL_1E6;
				case HtmlTokenId.Restart:
					continue;
				case HtmlTokenId.OverlappedClose:
					goto IL_2DC;
				case HtmlTokenId.OverlappedReopen:
					goto IL_2EC;
				}
				break;
			}
			this.state = HtmlReader.State.EndOfFile;
			return false;
			IL_15A:
			this.state = HtmlReader.State.Text;
			this.tokenKind = HtmlTokenKind.Text;
			this.parserToken.Text.Rewind();
			return true;
			Block_17:
			if (this.literalTags)
			{
				this.state = HtmlReader.State.Text;
				this.tokenKind = HtmlTokenKind.Text;
			}
			else
			{
				this.state = HtmlReader.State.SpecialTag;
				this.tokenKind = HtmlTokenKind.SpecialTag;
			}
			this.parserToken.Text.Rewind();
			return true;
			IL_1E6:
			if (this.parserToken.IsTagNameEmpty && this.parserToken.TagIndex == HtmlTagIndex.Unknown)
			{
				this.state = HtmlReader.State.SpecialTag;
				this.tokenKind = HtmlTokenKind.SpecialTag;
				this.parserToken.Text.Rewind();
				return true;
			}
			this.state = HtmlReader.State.BeginTag;
			if (this.parserToken.IsEndTag)
			{
				this.tokenKind = HtmlTokenKind.EndTag;
				if ((byte)(HtmlDtd.tags[(int)this.parserToken.TagIndex].Literal & HtmlDtd.Literal.Tags) != 0)
				{
					this.literalTags = false;
				}
			}
			else if (this.parserToken.TagIndex > HtmlTagIndex.Unknown && HtmlDtd.tags[(int)this.parserToken.TagIndex].Scope == HtmlDtd.TagScope.EMPTY)
			{
				this.tokenKind = HtmlTokenKind.EmptyElementTag;
			}
			else
			{
				this.tokenKind = HtmlTokenKind.StartTag;
			}
			this.parserToken.Text.Rewind();
			if (this.parserToken.IsEndTag && this.parserToken.TagIndex != HtmlTagIndex.Unknown)
			{
				this.depth--;
				return true;
			}
			return true;
			IL_2DC:
			this.state = HtmlReader.State.OverlappedClose;
			this.tokenKind = HtmlTokenKind.OverlappedClose;
			return true;
			IL_2EC:
			this.depth += this.parserToken.Argument;
			this.state = HtmlReader.State.OverlappedReopen;
			this.tokenKind = HtmlTokenKind.OverlappedReopen;
			return true;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x000B0A1E File Offset: 0x000AEC1E
		public int Depth
		{
			get
			{
				this.AssertNotDisposed();
				return this.depth;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x000B0A2C File Offset: 0x000AEC2C
		public int CurrentOffset
		{
			get
			{
				this.AssertNotDisposed();
				return this.parser.CurrentOffset;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x000B0A3F File Offset: 0x000AEC3F
		public int OverlappedDepth
		{
			get
			{
				if (this.state != HtmlReader.State.OverlappedClose && this.state != HtmlReader.State.OverlappedReopen)
				{
					this.AssertInToken();
					throw new InvalidOperationException("Reader must be positioned on OverlappedClose or OverlappedReopen token");
				}
				return this.parserToken.Argument;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x000B0A6F File Offset: 0x000AEC6F
		public HtmlTagId TagId
		{
			get
			{
				this.AssertInTag();
				return HtmlNameData.Names[(int)this.parserToken.NameIndex].PublicTagId;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x000B0A91 File Offset: 0x000AEC91
		public bool TagInjectedByNormalizer
		{
			get
			{
				this.AssertInTag();
				if (this.state != HtmlReader.State.BeginTag)
				{
					throw new InvalidOperationException("Reader must be positioned at the beginning of a StartTag, EndTag or EmptyElementTag token");
				}
				return this.parserToken.Argument == 1;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x000B0ABC File Offset: 0x000AECBC
		public bool TagNameIsLong
		{
			get
			{
				this.AssertInTag();
				if (this.state != HtmlReader.State.BeginTag)
				{
					throw new InvalidOperationException("Reader must be positioned at the beginning of a StartTag, EndTag or EmptyElementTag token");
				}
				return this.parserToken.NameIndex == HtmlNameIndex.Unknown && this.parserToken.IsTagNameBegin && !this.parserToken.IsTagNameEnd;
			}
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000B0B10 File Offset: 0x000AED10
		public string ReadTagName()
		{
			if (this.state != HtmlReader.State.BeginTag)
			{
				this.AssertInTag();
				throw new InvalidOperationException("Reader must be positioned at the beginning of a StartTag, EndTag or EmptyElementTag token");
			}
			string result;
			if (this.parserToken.NameIndex != HtmlNameIndex.Unknown)
			{
				result = HtmlNameData.Names[(int)this.parserToken.NameIndex].Name;
			}
			else
			{
				if (this.parserToken.IsTagNameEnd)
				{
					return this.parserToken.Name.GetString(int.MaxValue);
				}
				StringBuildSink stringBuildSink = this.GetStringBuildSink();
				this.parserToken.Name.WriteTo(stringBuildSink);
				do
				{
					this.ParseToken();
					this.parserToken.Name.WriteTo(stringBuildSink);
				}
				while (!this.parserToken.IsTagNameEnd);
				result = stringBuildSink.ToString();
			}
			this.state = HtmlReader.State.EndTagName;
			return result;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000B0BDC File Offset: 0x000AEDDC
		public int ReadTagName(char[] buffer, int offset, int count)
		{
			this.AssertInTag();
			if (this.state > HtmlReader.State.EndTagName)
			{
				throw new InvalidOperationException("Reader must be positioned at the beginning of a StartTag, EndTag or EmptyElementTag token");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TextConvertersStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountTooLarge);
			}
			int num = 0;
			if (this.state != HtmlReader.State.EndTagName)
			{
				if (this.state == HtmlReader.State.BeginTag)
				{
					this.state = HtmlReader.State.ReadTagName;
					this.parserToken.Name.Rewind();
				}
				while (count != 0)
				{
					int num2 = this.parserToken.Name.Read(buffer, offset, count);
					if (num2 == 0)
					{
						if (this.parserToken.IsTagNameEnd)
						{
							this.state = HtmlReader.State.EndTagName;
							break;
						}
						this.ParseToken();
						this.parserToken.Name.Rewind();
					}
					else
					{
						offset += num2;
						count -= num2;
						num += num2;
					}
				}
			}
			return num;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000B0CF4 File Offset: 0x000AEEF4
		public static string CharsetFromString(string arg, bool lookForWordCharset)
		{
			for (int i = 0; i < arg.Length; i++)
			{
				while (i < arg.Length && ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(arg[i])))
				{
					i++;
				}
				if (i == arg.Length)
				{
					break;
				}
				if (!lookForWordCharset || (arg.Length - i >= 7 && string.Equals(arg.Substring(i, 7), "charset", StringComparison.OrdinalIgnoreCase)))
				{
					if (lookForWordCharset)
					{
						i = arg.IndexOf('=', i + 7);
						if (i < 0)
						{
							break;
						}
						i++;
						while (i < arg.Length && ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(arg[i])))
						{
							i++;
						}
						if (i == arg.Length)
						{
							break;
						}
					}
					int num = i;
					while (num < arg.Length && arg[num] != ';' && !ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(arg[num])))
					{
						num++;
					}
					return arg.Substring(i, num - i);
				}
				i = arg.IndexOf(';', i);
				if (i < 0)
				{
					break;
				}
			}
			return null;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x000B0DF8 File Offset: 0x000AEFF8
		internal void WriteTagNameTo(ITextSink sink)
		{
			if (this.state != HtmlReader.State.BeginTag)
			{
				this.AssertInTag();
				throw new InvalidOperationException("Reader must be positioned at the beginning of a StartTag, EndTag or EmptyElementTag token");
			}
			for (;;)
			{
				this.parserToken.Name.WriteTo(sink);
				if (this.parserToken.IsTagNameEnd)
				{
					break;
				}
				this.ParseToken();
			}
			this.state = HtmlReader.State.EndTagName;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x000B0E51 File Offset: 0x000AF051
		public HtmlAttributeReader AttributeReader
		{
			get
			{
				this.AssertInTag();
				if (this.state == HtmlReader.State.ReadTag)
				{
					throw new InvalidOperationException("Cannot read attributes after reading tag as a markup text");
				}
				return new HtmlAttributeReader(this);
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000B0E74 File Offset: 0x000AF074
		public int ReadText(char[] buffer, int offset, int count)
		{
			if (this.state == HtmlReader.State.EndText)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TextConvertersStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountTooLarge);
			}
			if (this.state != HtmlReader.State.Text)
			{
				this.AssertInToken();
				throw new InvalidOperationException("Reader must be positioned on a Text token");
			}
			int num = 0;
			while (count != 0)
			{
				int num2 = this.parserToken.Text.Read(buffer, offset, count);
				if (num2 == 0)
				{
					HtmlTokenId htmlTokenId = this.PreviewNextToken();
					if (htmlTokenId != HtmlTokenId.Text && (!this.literalTags || htmlTokenId != HtmlTokenId.Tag || this.nextParserToken.TagIndex >= HtmlTagIndex.Unknown))
					{
						this.state = HtmlReader.State.EndText;
						break;
					}
					this.ParseToken();
					this.parserToken.Text.Rewind();
				}
				else
				{
					offset += num2;
					count -= num2;
					num += num2;
				}
			}
			return num;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000B0F7C File Offset: 0x000AF17C
		internal void WriteTextTo(ITextSink sink)
		{
			if (this.state != HtmlReader.State.Text)
			{
				this.AssertInToken();
				throw new InvalidOperationException("Reader must be positioned on a Text token");
			}
			for (;;)
			{
				this.parserToken.Text.WriteTo(sink);
				HtmlTokenId htmlTokenId = this.PreviewNextToken();
				if (htmlTokenId != HtmlTokenId.Text && (!this.literalTags || htmlTokenId != HtmlTokenId.Tag || this.nextParserToken.TagIndex >= HtmlTagIndex.Unknown))
				{
					break;
				}
				this.ParseToken();
			}
			this.state = HtmlReader.State.EndText;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000B0FEC File Offset: 0x000AF1EC
		public int ReadMarkupText(char[] buffer, int offset, int count)
		{
			if (this.state == HtmlReader.State.EndTag || this.state == HtmlReader.State.EndSpecialTag || this.state == HtmlReader.State.EndText)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TextConvertersStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountTooLarge);
			}
			if (this.state == HtmlReader.State.BeginTag)
			{
				this.state = HtmlReader.State.ReadTag;
			}
			else if (this.state != HtmlReader.State.SpecialTag && this.state != HtmlReader.State.ReadTag && this.state != HtmlReader.State.Text)
			{
				this.AssertInToken();
				if (this.state > HtmlReader.State.BeginTag)
				{
					throw new InvalidOperationException("Cannot read tag content as markup text after accessing tag name or attributes");
				}
				throw new InvalidOperationException("Reader must be positioned on Text, StartTag, EndTag, EmptyElementTag or SpecialTag token");
			}
			int num = 0;
			while (count != 0)
			{
				int num2 = this.parserToken.Text.ReadOriginal(buffer, offset, count);
				if (num2 == 0)
				{
					if (this.state == HtmlReader.State.SpecialTag)
					{
						if (this.parserToken.IsTagEnd)
						{
							this.state = HtmlReader.State.EndSpecialTag;
							break;
						}
					}
					else if (this.state == HtmlReader.State.ReadTag)
					{
						if (this.parserToken.IsTagEnd)
						{
							this.state = HtmlReader.State.EndTag;
							break;
						}
					}
					else
					{
						HtmlTokenId htmlTokenId = this.PreviewNextToken();
						if (htmlTokenId != HtmlTokenId.Text && (!this.literalTags || htmlTokenId != HtmlTokenId.Tag || this.nextParserToken.TagIndex >= HtmlTagIndex.Unknown))
						{
							this.state = HtmlReader.State.EndText;
							break;
						}
					}
					this.ParseToken();
					this.parserToken.Text.Rewind();
				}
				else
				{
					offset += num2;
					count -= num2;
					num += num2;
				}
			}
			return num;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000B118C File Offset: 0x000AF38C
		internal void WriteMarkupTextTo(ITextSink sink)
		{
			if (this.state == HtmlReader.State.BeginTag)
			{
				this.state = HtmlReader.State.ReadTag;
			}
			else if (this.state != HtmlReader.State.SpecialTag && this.state != HtmlReader.State.ReadTag && this.state != HtmlReader.State.Text)
			{
				this.AssertInToken();
				if (this.state > HtmlReader.State.BeginTag)
				{
					throw new InvalidOperationException("Cannot read tag content as markup text after accessing tag name or attributes");
				}
				throw new InvalidOperationException("Reader must be positioned on Text, StartTag, EndTag, EmptyElementTag or SpecialTag token");
			}
			for (;;)
			{
				this.parserToken.Text.WriteOriginalTo(sink);
				if (this.state == HtmlReader.State.SpecialTag)
				{
					if (this.parserToken.IsTagEnd)
					{
						break;
					}
				}
				else if (this.state == HtmlReader.State.ReadTag)
				{
					if (this.parserToken.IsTagEnd)
					{
						goto Block_9;
					}
				}
				else
				{
					HtmlTokenId htmlTokenId = this.PreviewNextToken();
					if (htmlTokenId != HtmlTokenId.Text && (!this.literalTags || htmlTokenId != HtmlTokenId.Tag || this.nextParserToken.TagIndex >= HtmlTagIndex.Unknown))
					{
						goto IL_CD;
					}
				}
				this.ParseToken();
			}
			this.state = HtmlReader.State.EndSpecialTag;
			return;
			Block_9:
			this.state = HtmlReader.State.EndTag;
			return;
			IL_CD:
			this.state = HtmlReader.State.EndText;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000B1278 File Offset: 0x000AF478
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000B1280 File Offset: 0x000AF480
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000B1290 File Offset: 0x000AF490
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.parser != null && this.parser is IDisposable)
				{
					((IDisposable)this.parser).Dispose();
				}
				if (this.input != null && this.input is IDisposable)
				{
					((IDisposable)this.input).Dispose();
				}
			}
			this.parser = null;
			this.input = null;
			this.stringBuildSink = null;
			this.parserToken = null;
			this.nextParserToken = null;
			this.state = HtmlReader.State.Disposed;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000B1314 File Offset: 0x000AF514
		internal HtmlReader SetInputEncoding(Encoding value)
		{
			this.InputEncoding = value;
			return this;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000B131E File Offset: 0x000AF51E
		internal HtmlReader SetDetectEncodingFromByteOrderMark(bool value)
		{
			this.DetectEncodingFromByteOrderMark = value;
			return this;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000B1328 File Offset: 0x000AF528
		internal HtmlReader SetNormalizeHtml(bool value)
		{
			this.NormalizeHtml = value;
			return this;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000B1332 File Offset: 0x000AF532
		internal HtmlReader SetTestBoundaryConditions(bool value)
		{
			this.testBoundaryConditions = value;
			return this;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000B133C File Offset: 0x000AF53C
		internal HtmlReader SetTestTraceStream(Stream value)
		{
			this.testTraceStream = value;
			return this;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000B1346 File Offset: 0x000AF546
		internal HtmlReader SetTestTraceShowTokenNum(bool value)
		{
			this.testTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x000B1350 File Offset: 0x000AF550
		internal HtmlReader SetTestTraceStopOnTokenNum(int value)
		{
			this.testTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000B135A File Offset: 0x000AF55A
		internal HtmlReader SetTestNormalizerTraceStream(Stream value)
		{
			this.testNormalizerTraceStream = value;
			return this;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x000B1364 File Offset: 0x000AF564
		internal HtmlReader SetTestNormalizerTraceShowTokenNum(bool value)
		{
			this.testNormalizerTraceShowTokenNum = value;
			return this;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x000B136E File Offset: 0x000AF56E
		internal HtmlReader SetTestNormalizerTraceStopOnTokenNum(int value)
		{
			this.testNormalizerTraceStopOnTokenNum = value;
			return this;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x000B1378 File Offset: 0x000AF578
		private void InitializeAndLock()
		{
			this.locked = true;
			ConverterInput converterInput;
			if (this.input is Stream)
			{
				if (this.inputEncoding == null)
				{
					throw new InvalidOperationException(TextConvertersStrings.InputEncodingRequired);
				}
				converterInput = new ConverterDecodingInput((Stream)this.input, false, this.inputEncoding, this.detectEncodingFromByteOrderMark, TextConvertersDefaults.MaxTokenSize(this.testBoundaryConditions), TextConvertersDefaults.MaxHtmlMetaRestartOffset(this.testBoundaryConditions), 16384, this.testBoundaryConditions, this, null);
			}
			else
			{
				converterInput = new ConverterUnicodeInput(this.input, false, TextConvertersDefaults.MaxTokenSize(this.testBoundaryConditions), this.testBoundaryConditions, null);
			}
			HtmlParser htmlParser = new HtmlParser(converterInput, false, false, TextConvertersDefaults.MaxTokenRuns(this.testBoundaryConditions), TextConvertersDefaults.MaxHtmlAttributes(this.testBoundaryConditions), this.testBoundaryConditions);
			if (this.normalizeInputHtml)
			{
				this.parser = new HtmlNormalizingParser(htmlParser, null, false, TextConvertersDefaults.MaxHtmlNormalizerNesting(this.testBoundaryConditions), this.testBoundaryConditions, this.testNormalizerTraceStream, this.testNormalizerTraceShowTokenNum, this.testNormalizerTraceStopOnTokenNum);
				return;
			}
			this.parser = htmlParser;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000B1474 File Offset: 0x000AF674
		bool IRestartable.CanRestart()
		{
			return false;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000B1477 File Offset: 0x000AF677
		void IRestartable.Restart()
		{
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000B1479 File Offset: 0x000AF679
		void IRestartable.DisableRestart()
		{
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000B147C File Offset: 0x000AF67C
		void IResultsFeedback.Set(ConfigParameter parameterId, object val)
		{
			if (parameterId != ConfigParameter.InputEncoding)
			{
				return;
			}
			this.inputEncoding = (Encoding)val;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000B149C File Offset: 0x000AF69C
		private void ParseToken()
		{
			if (this.nextParserToken != null)
			{
				this.parserToken = this.nextParserToken;
				this.parserTokenId = this.parserToken.HtmlTokenId;
				this.nextParserToken = null;
				return;
			}
			this.parserTokenId = this.parser.Parse();
			this.parserToken = this.parser.Token;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000B14F8 File Offset: 0x000AF6F8
		private HtmlTokenId PreviewNextToken()
		{
			if (this.nextParserToken == null)
			{
				this.parser.Parse();
				this.nextParserToken = this.parser.Token;
			}
			return this.nextParserToken.HtmlTokenId;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000B152A File Offset: 0x000AF72A
		private StringBuildSink GetStringBuildSink()
		{
			if (this.stringBuildSink == null)
			{
				this.stringBuildSink = new StringBuildSink();
			}
			this.stringBuildSink.Reset(int.MaxValue);
			return this.stringBuildSink;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000B1558 File Offset: 0x000AF758
		internal bool AttributeReader_ReadNextAttribute()
		{
			if (this.state == HtmlReader.State.EndTag)
			{
				return false;
			}
			this.AssertInTag();
			if (this.state == HtmlReader.State.ReadTag)
			{
				throw new InvalidOperationException("Cannot read attributes after reading tag as markup text");
			}
			for (;;)
			{
				if (this.state >= HtmlReader.State.BeginTag && this.state < HtmlReader.State.BeginAttribute)
				{
					while (this.parserToken.Attributes.Count == 0 && !this.parserToken.IsTagEnd)
					{
						this.ParseToken();
					}
					if (this.parserToken.Attributes.Count == 0)
					{
						break;
					}
					this.currentAttribute = 0;
					this.state = HtmlReader.State.BeginAttribute;
				}
				else if (++this.currentAttribute == this.parserToken.Attributes.Count)
				{
					if (this.parserToken.IsTagEnd)
					{
						goto Block_8;
					}
					for (;;)
					{
						this.ParseToken();
						if (this.parserToken.Attributes.Count != 0 && (this.parserToken.Attributes[0].IsAttrBegin || this.parserToken.Attributes.Count > 1))
						{
							break;
						}
						if (this.parserToken.IsTagEnd)
						{
							goto Block_11;
						}
					}
					this.currentAttribute = 0;
					if (!this.parserToken.Attributes[0].IsAttrBegin)
					{
						this.currentAttribute++;
					}
				}
				if (!this.parserToken.Attributes[this.currentAttribute].IsAttrEmptyName)
				{
					goto Block_13;
				}
			}
			this.state = HtmlReader.State.EndTag;
			return false;
			Block_8:
			this.state = HtmlReader.State.EndTag;
			return false;
			Block_11:
			this.state = HtmlReader.State.EndTag;
			return false;
			Block_13:
			this.state = HtmlReader.State.BeginAttribute;
			return true;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000B1710 File Offset: 0x000AF910
		internal HtmlAttributeId AttributeReader_GetCurrentAttributeId()
		{
			this.AssertInAttribute();
			return HtmlNameData.Names[(int)this.parserToken.Attributes[this.currentAttribute].NameIndex].PublicAttributeId;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000B1754 File Offset: 0x000AF954
		internal bool AttributeReader_CurrentAttributeNameIsLong()
		{
			if (this.state != HtmlReader.State.BeginAttribute)
			{
				this.AssertInAttribute();
				throw new InvalidOperationException();
			}
			return this.parserToken.Attributes[this.currentAttribute].NameIndex == HtmlNameIndex.Unknown && this.parserToken.Attributes[this.currentAttribute].IsAttrBegin && !this.parserToken.Attributes[this.currentAttribute].IsAttrNameEnd;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000B17E8 File Offset: 0x000AF9E8
		internal string AttributeReader_ReadCurrentAttributeName()
		{
			if (this.state != HtmlReader.State.BeginAttribute)
			{
				this.AssertInAttribute();
				throw new InvalidOperationException("Reader must be positioned at the beginning of attribute.");
			}
			string result;
			if (this.parserToken.Attributes[this.currentAttribute].NameIndex != HtmlNameIndex.Unknown)
			{
				result = HtmlNameData.Names[(int)this.parserToken.Attributes[this.currentAttribute].NameIndex].Name;
			}
			else
			{
				if (this.parserToken.Attributes[this.currentAttribute].IsAttrNameEnd)
				{
					return this.parserToken.Attributes[this.currentAttribute].Name.GetString(int.MaxValue);
				}
				StringBuildSink stringBuildSink = this.GetStringBuildSink();
				this.parserToken.Attributes[this.currentAttribute].Name.WriteTo(stringBuildSink);
				do
				{
					this.ParseToken();
					this.currentAttribute = 0;
					this.parserToken.Attributes[this.currentAttribute].Name.WriteTo(stringBuildSink);
				}
				while (!this.parserToken.Attributes[0].IsAttrNameEnd);
				result = stringBuildSink.ToString();
			}
			this.state = HtmlReader.State.EndAttributeName;
			return result;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000B1964 File Offset: 0x000AFB64
		internal int AttributeReader_ReadCurrentAttributeName(char[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TextConvertersStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountTooLarge);
			}
			if (this.state < HtmlReader.State.BeginAttribute || this.state > HtmlReader.State.EndAttributeName)
			{
				this.AssertInAttribute();
				throw new InvalidOperationException("Reader must be positioned at the beginning of attribute.");
			}
			int num = 0;
			if (this.state != HtmlReader.State.EndAttributeName)
			{
				if (this.state == HtmlReader.State.BeginAttribute)
				{
					this.state = HtmlReader.State.ReadAttributeName;
					this.parserToken.Attributes[this.currentAttribute].Name.Rewind();
				}
				while (count != 0)
				{
					int num2 = this.parserToken.Attributes[this.currentAttribute].Name.Read(buffer, offset, count);
					if (num2 == 0)
					{
						if (this.parserToken.Attributes[this.currentAttribute].IsAttrNameEnd)
						{
							this.state = HtmlReader.State.EndAttributeName;
							break;
						}
						this.ParseToken();
						this.currentAttribute = 0;
						this.parserToken.Attributes[this.currentAttribute].Name.Rewind();
					}
					else
					{
						offset += num2;
						count -= num2;
						num += num2;
					}
				}
			}
			return num;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000B1AF8 File Offset: 0x000AFCF8
		internal void AttributeReader_WriteCurrentAttributeNameTo(ITextSink sink)
		{
			if (this.state != HtmlReader.State.BeginAttribute)
			{
				this.AssertInAttribute();
				throw new InvalidOperationException("Reader must be positioned at the beginning of attribute.");
			}
			for (;;)
			{
				this.parserToken.Attributes[this.currentAttribute].Name.WriteTo(sink);
				if (this.parserToken.Attributes[this.currentAttribute].IsAttrNameEnd)
				{
					break;
				}
				this.ParseToken();
				this.currentAttribute = 0;
			}
			this.state = HtmlReader.State.EndAttributeName;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000B1B88 File Offset: 0x000AFD88
		internal bool AttributeReader_CurrentAttributeHasValue()
		{
			if (this.state != HtmlReader.State.BeginAttributeValue)
			{
				this.AssertInAttribute();
				if (this.state > HtmlReader.State.BeginAttributeValue)
				{
					throw new InvalidOperationException("Reader must be positioned before attribute value");
				}
				if (!this.SkipToAttributeValue())
				{
					this.state = HtmlReader.State.EndAttributeName;
					return false;
				}
				this.state = HtmlReader.State.BeginAttributeValue;
			}
			return true;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000B1BD8 File Offset: 0x000AFDD8
		internal bool AttributeReader_CurrentAttributeValueIsLong()
		{
			if (this.state != HtmlReader.State.BeginAttributeValue)
			{
				this.AssertInAttribute();
				if (this.state > HtmlReader.State.BeginAttributeValue)
				{
					throw new InvalidOperationException("Reader must be positioned before attribute value");
				}
				if (!this.SkipToAttributeValue())
				{
					this.state = HtmlReader.State.EndAttributeName;
					return false;
				}
				this.state = HtmlReader.State.BeginAttributeValue;
			}
			return this.parserToken.Attributes[this.currentAttribute].IsAttrValueBegin && !this.parserToken.Attributes[this.currentAttribute].IsAttrEnd;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000B1C70 File Offset: 0x000AFE70
		internal string AttributeReader_ReadCurrentAttributeValue()
		{
			if (this.state != HtmlReader.State.BeginAttributeValue)
			{
				this.AssertInAttribute();
				if (this.state > HtmlReader.State.BeginAttributeValue)
				{
					throw new InvalidOperationException("Reader must be positioned before attribute value");
				}
				if (!this.SkipToAttributeValue())
				{
					this.state = HtmlReader.State.EndAttribute;
					return null;
				}
			}
			if (this.parserToken.Attributes[this.currentAttribute].IsAttrEnd)
			{
				return this.parserToken.Attributes[this.currentAttribute].Value.GetString(int.MaxValue);
			}
			StringBuildSink stringBuildSink = this.GetStringBuildSink();
			this.parserToken.Attributes[this.currentAttribute].Value.WriteTo(stringBuildSink);
			do
			{
				this.ParseToken();
				this.currentAttribute = 0;
				this.parserToken.Attributes[0].Value.WriteTo(stringBuildSink);
			}
			while (!this.parserToken.Attributes[0].IsAttrEnd);
			this.state = HtmlReader.State.EndAttribute;
			return stringBuildSink.ToString();
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000B1DA0 File Offset: 0x000AFFA0
		internal int AttributeReader_ReadCurrentAttributeValue(char[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TextConvertersStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountTooLarge);
			}
			this.AssertInAttribute();
			if (this.state < HtmlReader.State.BeginAttributeValue)
			{
				if (!this.SkipToAttributeValue())
				{
					this.state = HtmlReader.State.EndAttribute;
					return 0;
				}
				this.state = HtmlReader.State.BeginAttributeValue;
			}
			int num = 0;
			if (this.state != HtmlReader.State.EndAttribute)
			{
				if (this.state == HtmlReader.State.BeginAttributeValue)
				{
					this.state = HtmlReader.State.ReadAttributeValue;
					this.parserToken.Attributes[this.currentAttribute].Value.Rewind();
				}
				while (count != 0)
				{
					int num2 = this.parserToken.Attributes[this.currentAttribute].Value.Read(buffer, offset, count);
					if (num2 == 0)
					{
						if (this.parserToken.Attributes[this.currentAttribute].IsAttrEnd)
						{
							this.state = HtmlReader.State.EndAttribute;
							break;
						}
						this.ParseToken();
						this.currentAttribute = 0;
						this.parserToken.Attributes[this.currentAttribute].Value.Rewind();
					}
					else
					{
						offset += num2;
						count -= num2;
						num += num2;
					}
				}
			}
			return num;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000B1F38 File Offset: 0x000B0138
		internal void AttributeReader_WriteCurrentAttributeValueTo(ITextSink sink)
		{
			if (this.state != HtmlReader.State.BeginAttributeValue)
			{
				this.AssertInAttribute();
				if (this.state > HtmlReader.State.BeginAttributeValue)
				{
					throw new InvalidOperationException("Reader must be positioned before attribute value");
				}
				if (!this.SkipToAttributeValue())
				{
					this.state = HtmlReader.State.EndAttribute;
					return;
				}
			}
			for (;;)
			{
				this.parserToken.Attributes[this.currentAttribute].Value.WriteTo(sink);
				if (this.parserToken.Attributes[this.currentAttribute].IsAttrEnd)
				{
					break;
				}
				this.ParseToken();
				this.currentAttribute = 0;
			}
			this.state = HtmlReader.State.EndAttribute;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000B1FE0 File Offset: 0x000B01E0
		private bool SkipToAttributeValue()
		{
			if (!this.parserToken.Attributes[this.currentAttribute].IsAttrValueBegin)
			{
				if (this.parserToken.Attributes[this.currentAttribute].IsAttrEnd)
				{
					return false;
				}
				do
				{
					this.ParseToken();
				}
				while (!this.parserToken.Attributes[0].IsAttrValueBegin && !this.parserToken.Attributes[0].IsAttrEnd);
				if (this.parserToken.Attributes[this.currentAttribute].IsAttrEnd)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000B20A6 File Offset: 0x000B02A6
		private void AssertNotLocked()
		{
			this.AssertNotDisposed();
			if (this.locked)
			{
				throw new InvalidOperationException("Cannot set reader properties after reading a first token");
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000B20C1 File Offset: 0x000B02C1
		private void AssertNotDisposed()
		{
			if (this.state == HtmlReader.State.Disposed)
			{
				throw new ObjectDisposedException("HtmlReader");
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000B20D6 File Offset: 0x000B02D6
		private void AssertInToken()
		{
			if (this.state <= HtmlReader.State.Begin)
			{
				this.AssertNotDisposed();
				throw new InvalidOperationException("Reader must be positioned inside a valid token");
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000B20F2 File Offset: 0x000B02F2
		private void AssertInTag()
		{
			if (this.state < HtmlReader.State.BeginTag)
			{
				this.AssertInToken();
				throw new InvalidOperationException("Reader must be positioned inside a StartTag, EndTag or EmptyElementTag token");
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000B210F File Offset: 0x000B030F
		private void AssertInAttribute()
		{
			if (this.state < HtmlReader.State.BeginAttribute || this.state > HtmlReader.State.EndAttribute)
			{
				this.AssertInTag();
				throw new InvalidOperationException("Reader must be positioned inside attribute");
			}
		}

		// Token: 0x04001945 RID: 6469
		private const int InputBufferSize = 16384;

		// Token: 0x04001946 RID: 6470
		private Encoding inputEncoding;

		// Token: 0x04001947 RID: 6471
		private bool detectEncodingFromByteOrderMark;

		// Token: 0x04001948 RID: 6472
		private bool normalizeInputHtml;

		// Token: 0x04001949 RID: 6473
		private bool testBoundaryConditions;

		// Token: 0x0400194A RID: 6474
		private Stream testTraceStream;

		// Token: 0x0400194B RID: 6475
		private bool testTraceShowTokenNum = true;

		// Token: 0x0400194C RID: 6476
		private int testTraceStopOnTokenNum;

		// Token: 0x0400194D RID: 6477
		private Stream testNormalizerTraceStream;

		// Token: 0x0400194E RID: 6478
		private bool testNormalizerTraceShowTokenNum = true;

		// Token: 0x0400194F RID: 6479
		private int testNormalizerTraceStopOnTokenNum;

		// Token: 0x04001950 RID: 6480
		private bool locked;

		// Token: 0x04001951 RID: 6481
		private object input;

		// Token: 0x04001952 RID: 6482
		private IHtmlParser parser;

		// Token: 0x04001953 RID: 6483
		private HtmlTokenId parserTokenId;

		// Token: 0x04001954 RID: 6484
		private HtmlToken parserToken;

		// Token: 0x04001955 RID: 6485
		private HtmlToken nextParserToken;

		// Token: 0x04001956 RID: 6486
		private int depth;

		// Token: 0x04001957 RID: 6487
		private StringBuildSink stringBuildSink;

		// Token: 0x04001958 RID: 6488
		private int currentAttribute;

		// Token: 0x04001959 RID: 6489
		private bool literalTags;

		// Token: 0x0400195A RID: 6490
		private HtmlReader.State state;

		// Token: 0x0400195B RID: 6491
		private HtmlTokenKind tokenKind;

		// Token: 0x02000227 RID: 551
		private enum State : byte
		{
			// Token: 0x0400195D RID: 6493
			Disposed,
			// Token: 0x0400195E RID: 6494
			EndOfFile,
			// Token: 0x0400195F RID: 6495
			Begin,
			// Token: 0x04001960 RID: 6496
			Text,
			// Token: 0x04001961 RID: 6497
			EndText,
			// Token: 0x04001962 RID: 6498
			OverlappedClose,
			// Token: 0x04001963 RID: 6499
			OverlappedReopen,
			// Token: 0x04001964 RID: 6500
			SpecialTag,
			// Token: 0x04001965 RID: 6501
			EndSpecialTag,
			// Token: 0x04001966 RID: 6502
			BeginTag,
			// Token: 0x04001967 RID: 6503
			ReadTagName,
			// Token: 0x04001968 RID: 6504
			EndTagName,
			// Token: 0x04001969 RID: 6505
			BeginAttribute,
			// Token: 0x0400196A RID: 6506
			ReadAttributeName,
			// Token: 0x0400196B RID: 6507
			EndAttributeName,
			// Token: 0x0400196C RID: 6508
			BeginAttributeValue,
			// Token: 0x0400196D RID: 6509
			ReadAttributeValue,
			// Token: 0x0400196E RID: 6510
			EndAttribute,
			// Token: 0x0400196F RID: 6511
			ReadTag,
			// Token: 0x04001970 RID: 6512
			EndTag
		}
	}
}
