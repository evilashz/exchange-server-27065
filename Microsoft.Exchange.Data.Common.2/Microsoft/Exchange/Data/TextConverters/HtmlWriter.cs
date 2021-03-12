using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200022A RID: 554
	public class HtmlWriter : IRestartable, IFallback, IDisposable, ITextSinkEx, ITextSink
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x000B21D6 File Offset: 0x000B03D6
		public HtmlWriter(Stream output, Encoding outputEncoding)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (outputEncoding == null)
			{
				throw new ArgumentNullException("outputEncoding");
			}
			this.output = new ConverterEncodingOutput(output, true, false, outputEncoding, false, false, null);
			this.autoNewLines = true;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000B2213 File Offset: 0x000B0413
		public HtmlWriter(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			this.output = new ConverterUnicodeOutput(output, true, false);
			this.autoNewLines = true;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000B223E File Offset: 0x000B043E
		internal HtmlWriter(ConverterOutput output, bool filterHtml, bool autoNewLines)
		{
			this.output = output;
			this.filterHtml = filterHtml;
			this.autoNewLines = autoNewLines;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000B225B File Offset: 0x000B045B
		public HtmlWriterState WriterState
		{
			get
			{
				if (this.outputState == HtmlWriter.OutputState.OutsideTag)
				{
					return HtmlWriterState.Default;
				}
				if (this.outputState >= HtmlWriter.OutputState.WritingAttributeName)
				{
					return HtmlWriterState.Attribute;
				}
				return HtmlWriterState.Tag;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x000B2273 File Offset: 0x000B0473
		bool ITextSink.IsEnough
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000B2276 File Offset: 0x000B0476
		internal bool HasEncoding
		{
			get
			{
				return this.output is ConverterEncodingOutput;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x000B2286 File Offset: 0x000B0486
		internal bool CodePageSameAsInput
		{
			get
			{
				return (this.output as ConverterEncodingOutput).CodePageSameAsInput;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x000B2298 File Offset: 0x000B0498
		// (set) Token: 0x060016B3 RID: 5811 RVA: 0x000B22AA File Offset: 0x000B04AA
		internal Encoding Encoding
		{
			get
			{
				return (this.output as ConverterEncodingOutput).Encoding;
			}
			set
			{
				(this.output as ConverterEncodingOutput).Encoding = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000B22BD File Offset: 0x000B04BD
		internal bool CanAcceptMore
		{
			get
			{
				return this.output.CanAcceptMore;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000B22CA File Offset: 0x000B04CA
		internal bool IsTagOpen
		{
			get
			{
				return this.outputState != HtmlWriter.OutputState.OutsideTag;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000B22D8 File Offset: 0x000B04D8
		internal int LineLength
		{
			get
			{
				return this.lineLength;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x000B22E0 File Offset: 0x000B04E0
		internal int LiteralWhitespaceNesting
		{
			get
			{
				return this.literalWhitespaceNesting;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000B22E8 File Offset: 0x000B04E8
		internal bool IsCopyPending
		{
			get
			{
				return this.copyPending;
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000B22F0 File Offset: 0x000B04F0
		public void Flush()
		{
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			this.output.Flush();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000B2320 File Offset: 0x000B0520
		public void WriteTag(HtmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (reader.TagId != HtmlTagId.Unknown)
			{
				this.WriteTagBegin(HtmlNameData.TagIndex[(int)reader.TagId], null, reader.TokenKind == HtmlTokenKind.EndTag, false, false);
			}
			else
			{
				this.WriteTagBegin(HtmlNameIndex.Unknown, null, reader.TokenKind == HtmlTokenKind.EndTag, false, false);
				reader.WriteTagNameTo(this.WriteTagName());
			}
			this.isEmptyScopeTag = (reader.TokenKind == HtmlTokenKind.EmptyElementTag);
			if (reader.TokenKind == HtmlTokenKind.StartTag || reader.TokenKind == HtmlTokenKind.EmptyElementTag)
			{
				HtmlAttributeReader attributeReader = reader.AttributeReader;
				while (attributeReader.ReadNext())
				{
					if (attributeReader.Id != HtmlAttributeId.Unknown)
					{
						this.OutputAttributeName(HtmlNameData.Names[(int)HtmlNameData.attributeIndex[(int)attributeReader.Id]].Name);
					}
					else
					{
						attributeReader.WriteNameTo(this.WriteAttributeName());
					}
					if (attributeReader.HasValue)
					{
						attributeReader.WriteValueTo(this.WriteAttributeValue());
					}
					this.OutputAttributeEnd();
					this.outputState = HtmlWriter.OutputState.BeforeAttribute;
				}
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000B2425 File Offset: 0x000B0625
		public void WriteStartTag(HtmlTagId id)
		{
			this.WriteTag(id, false);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000B242F File Offset: 0x000B062F
		public void WriteStartTag(string name)
		{
			this.WriteTag(name, false);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000B2439 File Offset: 0x000B0639
		public void WriteEndTag(HtmlTagId id)
		{
			this.WriteTag(id, true);
			this.WriteTagEnd();
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000B2449 File Offset: 0x000B0649
		public void WriteEndTag(string name)
		{
			this.WriteTag(name, true);
			this.WriteTagEnd();
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000B2459 File Offset: 0x000B0659
		public void WriteEmptyElementTag(HtmlTagId id)
		{
			this.WriteTag(id, false);
			this.isEmptyScopeTag = true;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000B246A File Offset: 0x000B066A
		public void WriteEmptyElementTag(string name)
		{
			this.WriteTag(name, false);
			this.isEmptyScopeTag = true;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000B247C File Offset: 0x000B067C
		public void WriteAttribute(HtmlAttributeId id, string value)
		{
			if (id < HtmlAttributeId.Unknown || (int)id >= HtmlNameData.attributeIndex.Length)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeIdInvalid, "id");
			}
			if (id == HtmlAttributeId.Unknown)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeIdIsUnknown, "id");
			}
			if (this.outputState < HtmlWriter.OutputState.WritingTagName)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.isEndTag)
			{
				throw new InvalidOperationException(TextConvertersStrings.EndTagCannotHaveAttributes);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(HtmlNameData.Names[(int)HtmlNameData.attributeIndex[(int)id]].Name);
			if (value != null)
			{
				this.OutputAttributeValue(value);
				this.OutputAttributeEnd();
			}
			this.outputState = HtmlWriter.OutputState.BeforeAttribute;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x000B2538 File Offset: 0x000B0738
		public void WriteAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeNameIsEmpty, "name");
			}
			if (this.outputState < HtmlWriter.OutputState.WritingTagName)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.isEndTag)
			{
				throw new InvalidOperationException(TextConvertersStrings.EndTagCannotHaveAttributes);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(name);
			if (value != null)
			{
				this.OutputAttributeValue(value);
				this.OutputAttributeEnd();
			}
			this.outputState = HtmlWriter.OutputState.BeforeAttribute;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000B25D4 File Offset: 0x000B07D4
		public void WriteAttribute(HtmlAttributeId id, char[] buffer, int index, int count)
		{
			if (id < HtmlAttributeId.Unknown || (int)id >= HtmlNameData.attributeIndex.Length)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeIdInvalid, "id");
			}
			if (id == HtmlAttributeId.Unknown)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeIdIsUnknown, "id");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.outputState < HtmlWriter.OutputState.WritingTagName)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.isEndTag)
			{
				throw new InvalidOperationException(TextConvertersStrings.EndTagCannotHaveAttributes);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(HtmlNameData.Names[(int)HtmlNameData.attributeIndex[(int)id]].Name);
			this.OutputAttributeValue(buffer, index, count);
			this.OutputAttributeEnd();
			this.outputState = HtmlWriter.OutputState.BeforeAttribute;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000B26CC File Offset: 0x000B08CC
		public void WriteAttribute(string name, char[] buffer, int index, int count)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeNameIsEmpty, "name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.outputState < HtmlWriter.OutputState.WritingTagName)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.isEndTag)
			{
				throw new InvalidOperationException(TextConvertersStrings.EndTagCannotHaveAttributes);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(name);
			this.OutputAttributeValue(buffer, index, count);
			this.OutputAttributeEnd();
			this.outputState = HtmlWriter.OutputState.BeforeAttribute;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000B27A4 File Offset: 0x000B09A4
		public void WriteAttribute(HtmlAttributeReader attributeReader)
		{
			if (this.outputState < HtmlWriter.OutputState.WritingTagName)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.isEndTag)
			{
				throw new InvalidOperationException(TextConvertersStrings.EndTagCannotHaveAttributes);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			attributeReader.WriteNameTo(this.WriteAttributeName());
			if (attributeReader.HasValue)
			{
				attributeReader.WriteValueTo(this.WriteAttributeValue());
			}
			this.OutputAttributeEnd();
			this.outputState = HtmlWriter.OutputState.BeforeAttribute;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000B281C File Offset: 0x000B0A1C
		public void WriteAttributeName(HtmlAttributeId id)
		{
			if (id < HtmlAttributeId.Unknown || (int)id >= HtmlNameData.attributeIndex.Length)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeIdInvalid, "id");
			}
			if (id == HtmlAttributeId.Unknown)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeIdIsUnknown, "id");
			}
			if (this.outputState < HtmlWriter.OutputState.WritingTagName)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.isEndTag)
			{
				throw new InvalidOperationException(TextConvertersStrings.EndTagCannotHaveAttributes);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(HtmlNameData.Names[(int)HtmlNameData.attributeIndex[(int)id]].Name);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000B28C0 File Offset: 0x000B0AC0
		public void WriteAttributeName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(TextConvertersStrings.AttributeNameIsEmpty, "name");
			}
			if (this.outputState < HtmlWriter.OutputState.WritingTagName)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.isEndTag)
			{
				throw new InvalidOperationException(TextConvertersStrings.EndTagCannotHaveAttributes);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(name);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000B2944 File Offset: 0x000B0B44
		public void WriteAttributeName(HtmlAttributeReader attributeReader)
		{
			if (this.outputState < HtmlWriter.OutputState.WritingTagName)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.isEndTag)
			{
				throw new InvalidOperationException(TextConvertersStrings.EndTagCannotHaveAttributes);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			attributeReader.WriteNameTo(this.WriteAttributeName());
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000B2998 File Offset: 0x000B0B98
		public void WriteAttributeValue(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.outputState < HtmlWriter.OutputState.TagStarted)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.outputState < HtmlWriter.OutputState.WritingAttributeName)
			{
				throw new InvalidOperationException(TextConvertersStrings.AttributeNotStarted);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			this.OutputAttributeValue(value);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000B29F8 File Offset: 0x000B0BF8
		public void WriteAttributeValue(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.outputState < HtmlWriter.OutputState.TagStarted)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.outputState < HtmlWriter.OutputState.WritingAttributeName)
			{
				throw new InvalidOperationException(TextConvertersStrings.AttributeNotStarted);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			this.OutputAttributeValue(buffer, index, count);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000B2A84 File Offset: 0x000B0C84
		public void WriteAttributeValue(HtmlAttributeReader attributeReader)
		{
			if (this.outputState < HtmlWriter.OutputState.TagStarted)
			{
				throw new InvalidOperationException(TextConvertersStrings.TagNotStarted);
			}
			if (this.outputState < HtmlWriter.OutputState.WritingAttributeName)
			{
				throw new InvalidOperationException(TextConvertersStrings.AttributeNotStarted);
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (attributeReader.HasValue)
			{
				attributeReader.WriteValueTo(this.WriteAttributeValue());
			}
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000B2AE4 File Offset: 0x000B0CE4
		public void WriteText(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (value.Length != 0)
			{
				if (this.lastWhitespace)
				{
					this.OutputLastWhitespace(value[0]);
				}
				this.output.Write(value, this);
				this.lineLength += value.Length;
				this.textLineLength += value.Length;
				this.allowWspBeforeFollowingTag = false;
			}
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000B2B78 File Offset: 0x000B0D78
		public void WriteText(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			this.WriteTextInternal(buffer, index, count);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000B2BE9 File Offset: 0x000B0DE9
		public void WriteText(HtmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			reader.WriteTextTo(this.WriteText());
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000B2C18 File Offset: 0x000B0E18
		public void WriteMarkupText(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (this.lastWhitespace)
			{
				this.OutputLastWhitespace(value[0]);
			}
			this.output.Write(value, null);
			this.lineLength += value.Length;
			this.allowWspBeforeFollowingTag = false;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000B2C90 File Offset: 0x000B0E90
		public void WriteMarkupText(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (this.lastWhitespace)
			{
				this.OutputLastWhitespace(buffer[index]);
			}
			this.output.Write(buffer, index, count, null);
			this.lineLength += count;
			this.allowWspBeforeFollowingTag = false;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000B2D2D File Offset: 0x000B0F2D
		public void WriteMarkupText(HtmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			reader.WriteMarkupTextTo(this.WriteMarkupText());
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000B2D5C File Offset: 0x000B0F5C
		bool IRestartable.CanRestart()
		{
			return this.output is IRestartable && ((IRestartable)this.output).CanRestart();
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000B2D80 File Offset: 0x000B0F80
		void IRestartable.Restart()
		{
			if (this.output is IRestartable)
			{
				((IRestartable)this.output).Restart();
			}
			this.allowWspBeforeFollowingTag = false;
			this.lastWhitespace = false;
			this.lineLength = 0;
			this.longestLineLength = 0;
			this.literalWhitespaceNesting = 0;
			this.literalTags = false;
			this.literalEntities = false;
			this.cssEscaping = false;
			this.tagNameIndex = HtmlNameIndex._NOTANAME;
			this.previousTagNameIndex = HtmlNameIndex._NOTANAME;
			this.isEndTag = false;
			this.isEmptyScopeTag = false;
			this.copyPending = false;
			this.outputState = HtmlWriter.OutputState.OutsideTag;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000B2E0C File Offset: 0x000B100C
		void IRestartable.DisableRestart()
		{
			if (this.output is IRestartable)
			{
				((IRestartable)this.output).DisableRestart();
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000B2E2B File Offset: 0x000B102B
		byte[] IFallback.GetUnsafeAsciiMap(out byte unsafeAsciiMask)
		{
			if (this.literalEntities)
			{
				unsafeAsciiMask = 0;
				return null;
			}
			if (this.filterHtml)
			{
				unsafeAsciiMask = 1;
			}
			else
			{
				unsafeAsciiMask = 1;
			}
			return HtmlSupport.UnsafeAsciiMap;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000B2E4F File Offset: 0x000B104F
		bool IFallback.HasUnsafeUnicode()
		{
			return this.filterHtml;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000B2E57 File Offset: 0x000B1057
		bool IFallback.TreatNonAsciiAsUnsafe(string charset)
		{
			return this.filterHtml && charset.StartsWith("x-", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000B2E70 File Offset: 0x000B1070
		bool IFallback.IsUnsafeUnicode(char ch, bool isFirstChar)
		{
			return this.filterHtml && (ch < '\ud800' || ch >= '') && ((byte)(ch & 'ÿ') == 60 || (byte)(ch >> 8 & 'ÿ') == 60 || (!isFirstChar && ch == '﻿') || CharUnicodeInfo.GetUnicodeCategory(ch) == UnicodeCategory.PrivateUse);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000B2ECC File Offset: 0x000B10CC
		bool IFallback.FallBackChar(char ch, char[] outputBuffer, ref int outputBufferCount, int outputEnd)
		{
			if (this.literalEntities)
			{
				if (this.cssEscaping)
				{
					uint num = (uint)ch;
					int num2 = (num < 16U) ? 1 : ((num < 256U) ? 2 : ((num < 4096U) ? 3 : 4));
					if (outputEnd - outputBufferCount < num2 + 2)
					{
						return false;
					}
					outputBuffer[outputBufferCount++] = '\\';
					int num3 = outputBufferCount + num2;
					while (num != 0U)
					{
						uint num4 = num & 15U;
						outputBuffer[--num3] = (char)((ulong)num4 + (ulong)((num4 < 10U) ? 48L : 55L));
						num >>= 4;
					}
					outputBufferCount += num2;
					outputBuffer[outputBufferCount++] = ' ';
				}
				else
				{
					if (outputEnd - outputBufferCount < 1)
					{
						return false;
					}
					outputBuffer[outputBufferCount++] = (this.filterHtml ? '?' : ch);
				}
			}
			else
			{
				HtmlEntityIndex htmlEntityIndex = (HtmlEntityIndex)0;
				if (ch <= '>')
				{
					if (ch == '>')
					{
						htmlEntityIndex = HtmlEntityIndex.gt;
					}
					else if (ch == '<')
					{
						htmlEntityIndex = HtmlEntityIndex.lt;
					}
					else if (ch == '&')
					{
						htmlEntityIndex = HtmlEntityIndex.amp;
					}
					else if (ch == '"')
					{
						htmlEntityIndex = HtmlEntityIndex.quot;
					}
				}
				else if ('\u00a0' <= ch && ch <= 'ÿ')
				{
					htmlEntityIndex = HtmlSupport.EntityMap[(int)(ch - '\u00a0')];
				}
				if (htmlEntityIndex != (HtmlEntityIndex)0)
				{
					string name = HtmlNameData.entities[(int)htmlEntityIndex].Name;
					if (outputEnd - outputBufferCount < name.Length + 2)
					{
						return false;
					}
					outputBuffer[outputBufferCount++] = '&';
					name.CopyTo(0, outputBuffer, outputBufferCount, name.Length);
					outputBufferCount += name.Length;
					outputBuffer[outputBufferCount++] = ';';
				}
				else
				{
					uint num5 = (uint)ch;
					int num6 = (num5 < 10U) ? 1 : ((num5 < 100U) ? 2 : ((num5 < 1000U) ? 3 : ((num5 < 10000U) ? 4 : 5)));
					if (outputEnd - outputBufferCount < num6 + 3)
					{
						return false;
					}
					outputBuffer[outputBufferCount++] = '&';
					outputBuffer[outputBufferCount++] = '#';
					int num7 = outputBufferCount + num6;
					while (num5 != 0U)
					{
						uint num8 = num5 % 10U;
						outputBuffer[--num7] = (char)(num8 + 48U);
						num5 /= 10U;
					}
					outputBufferCount += num6;
					outputBuffer[outputBufferCount++] = ';';
				}
			}
			return true;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x000B30F8 File Offset: 0x000B12F8
		void ITextSink.Write(char[] buffer, int offset, int count)
		{
			this.lineLength += count;
			this.textLineLength += count;
			this.output.Write(buffer, offset, count, this.fallback);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000B312A File Offset: 0x000B132A
		void ITextSink.Write(int ucs32Char)
		{
			this.lineLength++;
			this.textLineLength++;
			this.output.Write(ucs32Char, this.fallback);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000B315A File Offset: 0x000B135A
		void ITextSinkEx.Write(string text)
		{
			this.lineLength += text.Length;
			this.textLineLength += text.Length;
			this.output.Write(text, this.fallback);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000B3194 File Offset: 0x000B1394
		void ITextSinkEx.WriteNewLine()
		{
			if (this.lineLength > this.longestLineLength)
			{
				this.longestLineLength = this.lineLength;
			}
			this.output.Write("\r\n");
			this.lineLength = 0;
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000B31C7 File Offset: 0x000B13C7
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000B31CF File Offset: 0x000B13CF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000B31E0 File Offset: 0x000B13E0
		internal static HtmlNameIndex LookupName(string name)
		{
			if (name.Length <= 14)
			{
				short num = (short)((ulong)(HashCode.CalculateLowerCase(name) ^ 221) % 601UL);
				int num2 = (int)HtmlNameData.nameHashTable[(int)num];
				if (num2 > 0)
				{
					for (;;)
					{
						string name2 = HtmlNameData.Names[num2].Name;
						if (name2.Length == name.Length && name2[0] == ParseSupport.ToLowerCase(name[0]) && (name.Length == 1 || name2.Equals(name, StringComparison.OrdinalIgnoreCase)))
						{
							break;
						}
						if (HtmlNameData.Names[++num2].Hash != num)
						{
							return HtmlNameIndex.Unknown;
						}
					}
					return (HtmlNameIndex)num2;
				}
			}
			return HtmlNameIndex.Unknown;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000B327E File Offset: 0x000B147E
		internal void SetCopyPending(bool copyPending)
		{
			this.copyPending = copyPending;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000B3287 File Offset: 0x000B1487
		internal void WriteStartTag(HtmlNameIndex nameIndex)
		{
			this.WriteTagBegin(nameIndex, null, false, false, false);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000B3294 File Offset: 0x000B1494
		internal void WriteEndTag(HtmlNameIndex nameIndex)
		{
			this.WriteTagBegin(nameIndex, null, true, false, false);
			this.WriteTagEnd();
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000B32A7 File Offset: 0x000B14A7
		internal void WriteEmptyElementTag(HtmlNameIndex nameIndex)
		{
			this.WriteTagBegin(nameIndex, null, true, false, false);
			this.isEmptyScopeTag = true;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000B32BC File Offset: 0x000B14BC
		internal void WriteTagBegin(HtmlNameIndex nameIndex, string name, bool isEndTag, bool allowWspLeft, bool allowWspRight)
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (this.literalTags && nameIndex >= HtmlNameIndex.Unknown && (!isEndTag || nameIndex != this.tagNameIndex))
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteOtherTagsInsideElement(HtmlNameData.Names[(int)this.tagNameIndex].Name));
			}
			HtmlTagIndex tagIndex = HtmlNameData.Names[(int)nameIndex].TagIndex;
			if (nameIndex > HtmlNameIndex.Unknown)
			{
				this.isEmptyScopeTag = (HtmlDtd.tags[(int)tagIndex].Scope == HtmlDtd.TagScope.EMPTY);
				if (isEndTag && this.isEmptyScopeTag)
				{
					if (HtmlDtd.tags[(int)tagIndex].UnmatchedSubstitute != HtmlTagIndex._IMPLICIT_BEGIN)
					{
						this.output.Write("<!-- </");
						this.lineLength += 7;
						if (nameIndex > HtmlNameIndex.Unknown)
						{
							this.output.Write(HtmlNameData.Names[(int)nameIndex].Name);
							this.lineLength += HtmlNameData.Names[(int)nameIndex].Name.Length;
						}
						else
						{
							this.output.Write((name != null) ? name : "???");
							this.lineLength += ((name != null) ? name.Length : 3);
						}
						this.output.Write("> ");
						this.lineLength += 2;
						this.tagNameIndex = HtmlNameIndex._COMMENT;
						this.outputState = HtmlWriter.OutputState.WritingUnstructuredTagContent;
						return;
					}
					isEndTag = false;
				}
			}
			if (this.autoNewLines && this.literalWhitespaceNesting == 0)
			{
				bool flag = this.lastWhitespace;
				HtmlDtd.TagFill fill = HtmlDtd.tags[(int)tagIndex].Fill;
				if (this.lineLength != 0)
				{
					HtmlDtd.TagFmt fmt = HtmlDtd.tags[(int)tagIndex].Fmt;
					if ((!isEndTag && fmt.LB == HtmlDtd.FmtCode.BRK) || (isEndTag && fmt.LE == HtmlDtd.FmtCode.BRK) || (this.lineLength > 80 && (this.lastWhitespace || this.allowWspBeforeFollowingTag || (!isEndTag && fill.LB == HtmlDtd.FillCode.EAT) || (isEndTag && fill.LE == HtmlDtd.FillCode.EAT))))
					{
						if (this.lineLength > this.longestLineLength)
						{
							this.longestLineLength = this.lineLength;
						}
						this.output.Write("\r\n");
						this.lineLength = 0;
						this.lastWhitespace = false;
					}
				}
				this.allowWspBeforeFollowingTag = (((!isEndTag && fill.RB == HtmlDtd.FillCode.EAT) || (isEndTag && fill.RE == HtmlDtd.FillCode.EAT) || (flag && ((!isEndTag && fill.RB == HtmlDtd.FillCode.NUL) || (isEndTag && fill.RE == HtmlDtd.FillCode.NUL)))) && (nameIndex != HtmlNameIndex.Body || !isEndTag));
			}
			if (this.lastWhitespace)
			{
				this.output.Write(' ');
				this.lineLength++;
				this.lastWhitespace = false;
			}
			if (HtmlDtd.tags[(int)tagIndex].BlockElement || tagIndex == HtmlTagIndex.BR)
			{
				this.textLineLength = 0;
			}
			this.output.Write('<');
			this.lineLength++;
			if (nameIndex >= HtmlNameIndex.Unknown)
			{
				if (isEndTag)
				{
					if ((byte)(HtmlDtd.tags[(int)tagIndex].Literal & HtmlDtd.Literal.Tags) != 0)
					{
						this.literalTags = false;
						this.literalEntities = false;
						this.cssEscaping = false;
					}
					if (HtmlDtd.tags[(int)tagIndex].ContextTextType == HtmlDtd.ContextTextType.Literal)
					{
						this.literalWhitespaceNesting--;
					}
					this.output.Write('/');
					this.lineLength++;
				}
				if (nameIndex != HtmlNameIndex.Unknown)
				{
					this.output.Write(HtmlNameData.Names[(int)nameIndex].Name);
					this.lineLength += HtmlNameData.Names[(int)nameIndex].Name.Length;
					this.outputState = HtmlWriter.OutputState.BeforeAttribute;
				}
				else
				{
					if (name != null)
					{
						this.output.Write(name);
						this.lineLength += name.Length;
						this.outputState = HtmlWriter.OutputState.BeforeAttribute;
					}
					else
					{
						this.outputState = HtmlWriter.OutputState.TagStarted;
					}
					this.isEmptyScopeTag = false;
				}
			}
			else
			{
				this.previousTagNameIndex = this.tagNameIndex;
				if (nameIndex == HtmlNameIndex._COMMENT)
				{
					this.output.Write("!--");
					this.lineLength += 3;
				}
				else if (nameIndex == HtmlNameIndex._ASP)
				{
					this.output.Write('%');
					this.lineLength++;
				}
				else if (nameIndex == HtmlNameIndex._CONDITIONAL)
				{
					this.output.Write("!--[");
					this.lineLength += 4;
				}
				else if (nameIndex == HtmlNameIndex._DTD)
				{
					this.output.Write('?');
					this.lineLength++;
				}
				else
				{
					this.output.Write('!');
					this.lineLength++;
				}
				this.outputState = HtmlWriter.OutputState.WritingUnstructuredTagContent;
				this.isEmptyScopeTag = true;
			}
			this.tagNameIndex = nameIndex;
			this.isEndTag = isEndTag;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000B3757 File Offset: 0x000B1957
		internal void WriteTagEnd()
		{
			this.WriteTagEnd(this.isEmptyScopeTag);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000B3768 File Offset: 0x000B1968
		internal void WriteTagEnd(bool emptyScopeTag)
		{
			HtmlTagIndex tagIndex = HtmlNameData.Names[(int)this.tagNameIndex].TagIndex;
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			if (this.tagNameIndex > HtmlNameIndex.Unknown)
			{
				this.output.Write('>');
				this.lineLength++;
			}
			else
			{
				if (this.tagNameIndex == HtmlNameIndex._COMMENT)
				{
					this.output.Write("-->");
					this.lineLength += 3;
				}
				else if (this.tagNameIndex == HtmlNameIndex._ASP)
				{
					this.output.Write("%>");
					this.lineLength += 2;
				}
				else if (this.tagNameIndex == HtmlNameIndex._CONDITIONAL)
				{
					this.output.Write("]-->");
					this.lineLength += 4;
				}
				else if (this.tagNameIndex == HtmlNameIndex.Unknown && emptyScopeTag)
				{
					this.output.Write(" />");
					this.lineLength += 3;
				}
				else
				{
					this.output.Write('>');
					this.lineLength++;
				}
				this.tagNameIndex = this.previousTagNameIndex;
			}
			if (this.isEndTag && (tagIndex == HtmlTagIndex.LI || tagIndex == HtmlTagIndex.DD || tagIndex == HtmlTagIndex.DT))
			{
				this.lineLength = 0;
			}
			if (this.autoNewLines && this.literalWhitespaceNesting == 0)
			{
				HtmlDtd.TagFmt fmt = HtmlDtd.tags[(int)tagIndex].Fmt;
				HtmlDtd.TagFill fill = HtmlDtd.tags[(int)tagIndex].Fill;
				if ((!this.isEndTag && fmt.RB == HtmlDtd.FmtCode.BRK) || (this.isEndTag && fmt.RE == HtmlDtd.FmtCode.BRK) || (this.lineLength > 80 && (this.allowWspBeforeFollowingTag || (!this.isEndTag && fill.RB == HtmlDtd.FillCode.EAT) || (this.isEndTag && fill.RE == HtmlDtd.FillCode.EAT))))
				{
					if (this.lineLength > this.longestLineLength)
					{
						this.longestLineLength = this.lineLength;
					}
					this.output.Write("\r\n");
					this.lineLength = 0;
				}
			}
			if (!this.isEndTag && !emptyScopeTag)
			{
				HtmlDtd.Literal literal = HtmlDtd.tags[(int)tagIndex].Literal;
				if ((byte)(literal & HtmlDtd.Literal.Tags) != 0)
				{
					this.literalTags = true;
					this.literalEntities = (0 != (byte)(literal & HtmlDtd.Literal.Entities));
					this.cssEscaping = (tagIndex == HtmlTagIndex.Style);
				}
				if (HtmlDtd.tags[(int)tagIndex].ContextTextType == HtmlDtd.ContextTextType.Literal)
				{
					this.literalWhitespaceNesting++;
				}
			}
			this.outputState = HtmlWriter.OutputState.OutsideTag;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x000B39D1 File Offset: 0x000B1BD1
		internal void WriteAttribute(HtmlNameIndex nameIndex, string value)
		{
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(HtmlNameData.Names[(int)nameIndex].Name);
			if (value != null)
			{
				this.OutputAttributeValue(value);
				this.OutputAttributeEnd();
			}
			this.outputState = HtmlWriter.OutputState.BeforeAttribute;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000B3A10 File Offset: 0x000B1C10
		internal void WriteAttribute(HtmlNameIndex nameIndex, BufferString value)
		{
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(HtmlNameData.Names[(int)nameIndex].Name);
			this.OutputAttributeValue(value.Buffer, value.Offset, value.Length);
			this.OutputAttributeEnd();
			this.outputState = HtmlWriter.OutputState.BeforeAttribute;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x000B3A6A File Offset: 0x000B1C6A
		internal void WriteAttributeName(HtmlNameIndex nameIndex)
		{
			if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
			{
				this.OutputAttributeEnd();
			}
			this.OutputAttributeName(HtmlNameData.Names[(int)nameIndex].Name);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000B3A91 File Offset: 0x000B1C91
		internal void WriteAttributeValue(BufferString value)
		{
			this.OutputAttributeValue(value.Buffer, value.Offset, value.Length);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000B3AAE File Offset: 0x000B1CAE
		internal void WriteAttributeValueInternal(string value)
		{
			this.OutputAttributeValue(value);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000B3AB7 File Offset: 0x000B1CB7
		internal void WriteAttributeValueInternal(char[] buffer, int index, int count)
		{
			this.OutputAttributeValue(buffer, index, count);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x000B3AC4 File Offset: 0x000B1CC4
		internal void WriteText(char ch)
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (this.lastWhitespace)
			{
				this.OutputLastWhitespace(ch);
			}
			this.output.Write(ch, this);
			this.lineLength++;
			this.textLineLength++;
			this.allowWspBeforeFollowingTag = false;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000B3B20 File Offset: 0x000B1D20
		internal void WriteMarkupText(char ch)
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (this.lastWhitespace)
			{
				this.OutputLastWhitespace(ch);
			}
			this.output.Write(ch, null);
			this.lineLength++;
			this.allowWspBeforeFollowingTag = false;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000B3B6C File Offset: 0x000B1D6C
		internal ITextSinkEx WriteUnstructuredTagContent()
		{
			this.fallback = null;
			return this;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x000B3B76 File Offset: 0x000B1D76
		internal ITextSinkEx WriteTagName()
		{
			this.outputState = HtmlWriter.OutputState.WritingTagName;
			this.fallback = null;
			return this;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x000B3B88 File Offset: 0x000B1D88
		internal ITextSinkEx WriteAttributeName()
		{
			if (this.outputState != HtmlWriter.OutputState.WritingAttributeName)
			{
				if (this.outputState > HtmlWriter.OutputState.BeforeAttribute)
				{
					this.OutputAttributeEnd();
				}
				this.output.Write(' ');
				this.lineLength++;
			}
			this.outputState = HtmlWriter.OutputState.WritingAttributeName;
			this.fallback = null;
			return this;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x000B3BD7 File Offset: 0x000B1DD7
		internal ITextSinkEx WriteAttributeValue()
		{
			if (this.outputState != HtmlWriter.OutputState.WritingAttributeValue)
			{
				this.output.Write("=\"");
				this.lineLength += 2;
			}
			this.outputState = HtmlWriter.OutputState.WritingAttributeValue;
			this.fallback = this;
			return this;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000B3C0F File Offset: 0x000B1E0F
		internal ITextSinkEx WriteText()
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			this.allowWspBeforeFollowingTag = false;
			if (this.lastWhitespace)
			{
				this.OutputLastWhitespace('\u3000');
			}
			this.fallback = this;
			return this;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000B3C44 File Offset: 0x000B1E44
		internal ITextSinkEx WriteMarkupText()
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (this.lastWhitespace)
			{
				this.output.Write(' ');
				this.lineLength++;
				this.lastWhitespace = false;
			}
			this.fallback = null;
			return this;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000B3C91 File Offset: 0x000B1E91
		internal void WriteNewLine()
		{
			this.WriteNewLine(false);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x000B3C9C File Offset: 0x000B1E9C
		internal void WriteNewLine(bool optional)
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (!optional || (this.lineLength != 0 && this.literalWhitespaceNesting == 0))
			{
				if (this.lineLength > this.longestLineLength)
				{
					this.longestLineLength = this.lineLength;
				}
				this.output.Write("\r\n");
				this.lineLength = 0;
				this.lastWhitespace = false;
				this.allowWspBeforeFollowingTag = false;
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000B3D09 File Offset: 0x000B1F09
		internal void WriteAutoNewLine()
		{
			this.WriteNewLine(false);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x000B3D14 File Offset: 0x000B1F14
		internal void WriteAutoNewLine(bool optional)
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			if (this.autoNewLines && (!optional || (this.lineLength != 0 && this.literalWhitespaceNesting == 0)))
			{
				if (this.lineLength > this.longestLineLength)
				{
					this.longestLineLength = this.lineLength;
				}
				this.output.Write("\r\n");
				this.lineLength = 0;
				this.lastWhitespace = false;
				this.allowWspBeforeFollowingTag = false;
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000B3D89 File Offset: 0x000B1F89
		internal void WriteTabulation(int count)
		{
			this.WriteSpace(this.textLineLength / 8 * 8 + 8 * count - this.textLineLength);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x000B3DA8 File Offset: 0x000B1FA8
		internal void WriteSpace(int count)
		{
			if (this.literalWhitespaceNesting != 0)
			{
				while (count-- != 0)
				{
					this.output.Write(' ');
				}
				this.lineLength += count;
				this.textLineLength += count;
				this.lastWhitespace = false;
				this.allowWspBeforeFollowingTag = false;
				return;
			}
			if (this.lineLength == 0 && count == 1)
			{
				this.output.Write('\u00a0', this);
				return;
			}
			if (this.lastWhitespace)
			{
				this.lineLength++;
				this.output.Write('\u00a0', this);
			}
			this.lineLength += count - 1;
			this.textLineLength += count - 1;
			while (--count != 0)
			{
				this.output.Write('\u00a0', this);
			}
			this.lastWhitespace = true;
			this.allowWspBeforeFollowingTag = false;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x000B3E90 File Offset: 0x000B2090
		internal void WriteNbsp(int count)
		{
			if (this.lastWhitespace)
			{
				this.OutputLastWhitespace('\u00a0');
			}
			this.lineLength += count;
			this.textLineLength += count;
			while (count-- != 0)
			{
				this.output.Write('\u00a0', this);
			}
			this.allowWspBeforeFollowingTag = false;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x000B3EF0 File Offset: 0x000B20F0
		internal void WriteTextInternal(char[] buffer, int index, int count)
		{
			if (count != 0)
			{
				if (this.lastWhitespace)
				{
					this.OutputLastWhitespace(buffer[index]);
				}
				this.output.Write(buffer, index, count, this);
				this.lineLength += count;
				this.textLineLength += count;
				this.allowWspBeforeFollowingTag = false;
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000B3F43 File Offset: 0x000B2143
		internal void StartTextChunk()
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			this.lastWhitespace = false;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000B3F5A File Offset: 0x000B215A
		internal void EndTextChunk()
		{
			if (this.lastWhitespace)
			{
				this.OutputLastWhitespace('\n');
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000B3F6C File Offset: 0x000B216C
		internal void WriteCollapsedWhitespace()
		{
			if (this.outputState != HtmlWriter.OutputState.OutsideTag)
			{
				this.WriteTagEnd();
			}
			this.lastWhitespace = true;
			this.allowWspBeforeFollowingTag = false;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000B3F8A File Offset: 0x000B218A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.output != null)
			{
				if (!this.copyPending)
				{
					this.Flush();
				}
				if (this.output != null)
				{
					((IDisposable)this.output).Dispose();
				}
			}
			this.output = null;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000B3FC0 File Offset: 0x000B21C0
		private void WriteTag(HtmlTagId id, bool isEndTag)
		{
			if (id < HtmlTagId.Unknown || (int)id >= HtmlNameData.TagIndex.Length)
			{
				throw new ArgumentException(TextConvertersStrings.TagIdInvalid, "id");
			}
			if (id == HtmlTagId.Unknown)
			{
				throw new ArgumentException(TextConvertersStrings.TagIdIsUnknown, "id");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			this.WriteTagBegin(HtmlNameData.TagIndex[(int)id], null, isEndTag, false, false);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000B4024 File Offset: 0x000B2224
		private void WriteTag(string name, bool isEndTag)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(TextConvertersStrings.TagNameIsEmpty, "name");
			}
			if (this.copyPending)
			{
				throw new InvalidOperationException(TextConvertersStrings.CannotWriteWhileCopyPending);
			}
			HtmlNameIndex htmlNameIndex = HtmlWriter.LookupName(name);
			if (htmlNameIndex != HtmlNameIndex.Unknown)
			{
				name = null;
			}
			this.WriteTagBegin(htmlNameIndex, name, isEndTag, false, false);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000B4084 File Offset: 0x000B2284
		private void OutputLastWhitespace(char nextChar)
		{
			if (this.lineLength > 255 && this.autoNewLines)
			{
				if (this.lineLength > this.longestLineLength)
				{
					this.longestLineLength = this.lineLength;
				}
				this.output.Write("\r\n");
				this.lineLength = 0;
				if (ParseSupport.FarEastNonHanguelChar(nextChar))
				{
					this.output.Write(' ');
					this.lineLength++;
				}
			}
			else
			{
				this.output.Write(' ');
				this.lineLength++;
			}
			this.textLineLength++;
			this.lastWhitespace = false;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000B412C File Offset: 0x000B232C
		private void OutputAttributeName(string name)
		{
			this.output.Write(' ');
			this.output.Write(name);
			this.lineLength += name.Length + 1;
			this.outputState = HtmlWriter.OutputState.AfterAttributeName;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000B4164 File Offset: 0x000B2364
		private void OutputAttributeValue(string value)
		{
			if (this.outputState < HtmlWriter.OutputState.WritingAttributeValue)
			{
				this.output.Write("=\"");
				this.lineLength += 2;
			}
			this.output.Write(value, this);
			this.lineLength += value.Length;
			this.outputState = HtmlWriter.OutputState.WritingAttributeValue;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000B41C0 File Offset: 0x000B23C0
		private void OutputAttributeValue(char[] value, int index, int count)
		{
			if (this.outputState < HtmlWriter.OutputState.WritingAttributeValue)
			{
				this.output.Write("=\"");
				this.lineLength += 2;
			}
			this.output.Write(value, index, count, this);
			this.lineLength += count;
			this.outputState = HtmlWriter.OutputState.WritingAttributeValue;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000B4218 File Offset: 0x000B2418
		private void OutputAttributeEnd()
		{
			if (this.outputState < HtmlWriter.OutputState.WritingAttributeValue)
			{
				this.output.Write("=\"");
				this.lineLength += 2;
			}
			this.output.Write('"');
			this.lineLength++;
		}

		// Token: 0x04001976 RID: 6518
		private ConverterOutput output;

		// Token: 0x04001977 RID: 6519
		private HtmlWriter.OutputState outputState;

		// Token: 0x04001978 RID: 6520
		private bool filterHtml;

		// Token: 0x04001979 RID: 6521
		private bool autoNewLines;

		// Token: 0x0400197A RID: 6522
		private bool allowWspBeforeFollowingTag;

		// Token: 0x0400197B RID: 6523
		private bool lastWhitespace;

		// Token: 0x0400197C RID: 6524
		private int lineLength;

		// Token: 0x0400197D RID: 6525
		private int longestLineLength;

		// Token: 0x0400197E RID: 6526
		private int textLineLength;

		// Token: 0x0400197F RID: 6527
		private int literalWhitespaceNesting;

		// Token: 0x04001980 RID: 6528
		private bool literalTags;

		// Token: 0x04001981 RID: 6529
		private bool literalEntities;

		// Token: 0x04001982 RID: 6530
		private bool cssEscaping;

		// Token: 0x04001983 RID: 6531
		private IFallback fallback;

		// Token: 0x04001984 RID: 6532
		private HtmlNameIndex tagNameIndex;

		// Token: 0x04001985 RID: 6533
		private HtmlNameIndex previousTagNameIndex;

		// Token: 0x04001986 RID: 6534
		private bool isEndTag;

		// Token: 0x04001987 RID: 6535
		private bool isEmptyScopeTag;

		// Token: 0x04001988 RID: 6536
		private bool copyPending;

		// Token: 0x0200022B RID: 555
		internal enum OutputState
		{
			// Token: 0x0400198A RID: 6538
			OutsideTag,
			// Token: 0x0400198B RID: 6539
			TagStarted,
			// Token: 0x0400198C RID: 6540
			WritingUnstructuredTagContent,
			// Token: 0x0400198D RID: 6541
			WritingTagName,
			// Token: 0x0400198E RID: 6542
			BeforeAttribute,
			// Token: 0x0400198F RID: 6543
			WritingAttributeName,
			// Token: 0x04001990 RID: 6544
			AfterAttributeName,
			// Token: 0x04001991 RID: 6545
			WritingAttributeValue
		}
	}
}
