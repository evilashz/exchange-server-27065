using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Asn1Reader : IDisposable
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		public Asn1Reader(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (!inputStream.CanRead)
			{
				throw new NotSupportedException("Strings.StreamDoesNotSupportRead");
			}
			this.InputStream = inputStream;
			this.readBuffer = new byte[4096];
			this.tagStack = new Asn1Reader.TagInfo[32];
			this.tagStackTop = -1;
			this.readState = Asn1Reader.ReadState.Begin;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000B95E File Offset: 0x00009B5E
		public int StreamOffset
		{
			get
			{
				this.AssertGoodToUse(true);
				return this.bufferStreamOffset + this.readOffset;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000B974 File Offset: 0x00009B74
		public int Depth
		{
			get
			{
				return this.tagStackTop + 1;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000B97E File Offset: 0x00009B7E
		public int SequenceNum
		{
			get
			{
				this.AssertCurrentTag();
				if (this.tagStackTop > 0)
				{
					return this.tagStack[this.tagStackTop - 1].SequenceNum;
				}
				return 0;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000B9A9 File Offset: 0x00009BA9
		public byte TagIdByte
		{
			get
			{
				this.AssertCurrentTag();
				return this.tagStack[this.tagStackTop].TagIdByte;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000B9C7 File Offset: 0x00009BC7
		public int TagNumber
		{
			get
			{
				this.AssertCurrentTag();
				return this.tagStack[this.tagStackTop].TagNumber;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000B9E5 File Offset: 0x00009BE5
		public bool IsConstructedTag
		{
			get
			{
				this.AssertCurrentTag();
				return 0 != (this.tagStack[this.tagStackTop].TagIdByte & 32);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public TagClass TagClass
		{
			get
			{
				this.AssertCurrentTag();
				return (TagClass)(this.tagStack[this.tagStackTop].TagIdByte & 192);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000BA31 File Offset: 0x00009C31
		public EncodingType EncodingType
		{
			get
			{
				this.AssertCurrentTag();
				return this.tagStack[this.tagStackTop].EncodingType;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000BA4F File Offset: 0x00009C4F
		public int ValueLength
		{
			get
			{
				this.AssertCurrentTag();
				return this.tagStack[this.tagStackTop].ValueLength;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000BA6D File Offset: 0x00009C6D
		public bool IsLongValue
		{
			get
			{
				this.AssertCurrentTag();
				return this.ValueLength < 0 || this.ValueLength > 1048576;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000BA8D File Offset: 0x00009C8D
		public int ValueStreamOffset
		{
			get
			{
				this.AssertCurrentTag();
				return this.tagStack[this.tagStackTop].ValueStreamOffset;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000BAAB File Offset: 0x00009CAB
		private bool TagIsEndOfContentMarker
		{
			get
			{
				return this.tagStack[this.tagStackTop].TagIdByte == 0 && this.tagStack[this.tagStackTop].ValueLength == 0;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		private Decoder AsciiDecoder
		{
			get
			{
				if (this.asciiDecoder == null)
				{
					this.asciiDecoder = CTSGlobals.AsciiEncoding.GetDecoder();
				}
				return this.asciiDecoder;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000BB00 File Offset: 0x00009D00
		private Decoder Utf8Decoder
		{
			get
			{
				if (this.utf8Decoder == null)
				{
					this.utf8Decoder = Encoding.UTF8.GetDecoder();
				}
				return this.utf8Decoder;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000BB20 File Offset: 0x00009D20
		private Decoder UnicodeDecoder
		{
			get
			{
				if (this.unicodeDecoder == null)
				{
					this.unicodeDecoder = Encoding.Unicode.GetDecoder();
				}
				return this.unicodeDecoder;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000BB40 File Offset: 0x00009D40
		private Decoder Utf32Decoder
		{
			get
			{
				if (this.utf32Decoder == null)
				{
					this.utf32Decoder = Encoding.UTF32.GetDecoder();
				}
				return this.utf32Decoder;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000BB60 File Offset: 0x00009D60
		public bool ReadNext()
		{
			this.AssertGoodToUse(true);
			if (this.readState != Asn1Reader.ReadState.Begin)
			{
				if (this.tagStackTop < 0)
				{
					return false;
				}
				if (!this.IsConstructedTag)
				{
					this.SkipValue();
				}
			}
			for (;;)
			{
				if (this.readState != Asn1Reader.ReadState.Begin && this.ValueLength >= 0 && this.StreamOffset >= this.ValueStreamOffset + this.ValueLength)
				{
					if (!this.PopTag())
					{
						break;
					}
				}
				else
				{
					this.ReadAndPushTag();
					if (!this.TagIsEndOfContentMarker)
					{
						goto IL_9C;
					}
					this.PopTag();
					if (this.ValueLength != -1)
					{
						goto Block_9;
					}
					if (!this.PopTag())
					{
						goto Block_10;
					}
				}
			}
			this.readState = Asn1Reader.ReadState.EndOfFile;
			return false;
			Block_9:
			throw new ExchangeDataException("invalid data - end-of-content marker for a tag with definite length");
			Block_10:
			this.readState = Asn1Reader.ReadState.EndOfFile;
			return false;
			IL_9C:
			this.readState = Asn1Reader.ReadState.BeginValue;
			return true;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000BC14 File Offset: 0x00009E14
		public bool ReadFirstChild()
		{
			this.AssertGoodToUse(true);
			if (this.readState != Asn1Reader.ReadState.BeginValue || !this.IsConstructedTag)
			{
				throw new InvalidOperationException("not at the beginning of tag content or tag is not constructed");
			}
			if (this.ValueLength >= 0 && this.StreamOffset >= this.ValueStreamOffset + this.ValueLength)
			{
				this.readState = Asn1Reader.ReadState.ReadValue;
				return false;
			}
			this.ReadAndPushTag();
			if (!this.TagIsEndOfContentMarker)
			{
				this.readState = Asn1Reader.ReadState.BeginValue;
				return true;
			}
			this.PopTag();
			if (this.ValueLength != -1)
			{
				throw new ExchangeDataException("invalid data - end-of-content marker for a tag with definite length");
			}
			this.tagStack[this.tagStackTop].ValueLength = this.StreamOffset - this.ValueStreamOffset;
			this.readState = Asn1Reader.ReadState.ReadValue;
			return false;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000BCCC File Offset: 0x00009ECC
		public bool ReadNextSibling()
		{
			this.AssertGoodToUse(true);
			if (this.tagStackTop < 0)
			{
				throw new InvalidOperationException("not inside a tag");
			}
			if (this.readState == Asn1Reader.ReadState.BeginValue || this.readState == Asn1Reader.ReadState.ReadValue)
			{
				if (this.ValueLength >= 0)
				{
					this.SkipValue();
				}
				else
				{
					this.SkipTagsToMarker();
				}
			}
			if (!this.PopTag())
			{
				return false;
			}
			if (this.ValueLength >= 0 && this.StreamOffset >= this.ValueStreamOffset + this.ValueLength)
			{
				this.readState = Asn1Reader.ReadState.ReadValue;
				return false;
			}
			this.ReadAndPushTag();
			if (!this.TagIsEndOfContentMarker)
			{
				this.readState = Asn1Reader.ReadState.BeginValue;
				return true;
			}
			this.PopTag();
			if (this.ValueLength != -1)
			{
				throw new ExchangeDataException("invalid data - end-of-content marker for a tag with definite length");
			}
			this.tagStack[this.tagStackTop].ValueLength = this.StreamOffset - this.ValueStreamOffset;
			this.readState = Asn1Reader.ReadState.ReadValue;
			return false;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public bool ReadValueAsBool()
		{
			this.AssertAtTheBeginningOfValue();
			if (this.EncodingType != EncodingType.Boolean && (this.EncodingType != EncodingType.Unknown || this.IsConstructedTag))
			{
				throw new InvalidOperationException("cannot convert value");
			}
			if (this.ValueLength != 1)
			{
				throw new ExchangeDataException("invalid data - invalid boolean value encoding");
			}
			if (!this.EnsureMoreDataLoaded(this.ValueLength))
			{
				throw new ExchangeDataException("invalid data - premature end of file");
			}
			this.readState = Asn1Reader.ReadState.ReadValue;
			return 0 != this.ReadByte();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000BE24 File Offset: 0x0000A024
		public int ReadValueAsInt()
		{
			this.AssertAtTheBeginningOfValue();
			if (this.EncodingType != EncodingType.Integer && this.EncodingType != EncodingType.Enumerated && (this.EncodingType != EncodingType.Unknown || this.IsConstructedTag))
			{
				throw new InvalidOperationException("cannot convert value");
			}
			if (this.ValueLength > 4)
			{
				throw new ExchangeDataException("invalid data - integer value overflow");
			}
			if (!this.EnsureMoreDataLoaded(this.ValueLength))
			{
				throw new ExchangeDataException("invalid data - premature end of file");
			}
			this.readState = Asn1Reader.ReadState.ReadValue;
			return this.ReadInteger(this.ValueLength);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		public long ReadValueAsLong()
		{
			this.AssertAtTheBeginningOfValue();
			if (this.EncodingType != EncodingType.Integer && (this.EncodingType != EncodingType.Unknown || this.IsConstructedTag))
			{
				throw new InvalidOperationException("cannot convert value");
			}
			if (this.ValueLength > 8)
			{
				throw new ExchangeDataException("invalid data - long value overflow");
			}
			if (!this.EnsureMoreDataLoaded(this.ValueLength))
			{
				throw new ExchangeDataException("invalid data - premature end of file");
			}
			this.readState = Asn1Reader.ReadState.ReadValue;
			return this.ReadLong(this.ValueLength);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000BF20 File Offset: 0x0000A120
		public OID ReadValueAsOID()
		{
			this.AssertAtTheBeginningOfValue();
			if (this.EncodingType != EncodingType.ObjectIdentifier && (this.EncodingType != EncodingType.Unknown || this.IsConstructedTag))
			{
				throw new InvalidOperationException("cannot convert value");
			}
			if (this.ValueLength > 1024)
			{
				throw new ExchangeDataException("inalid data - object identifier is too long");
			}
			if (!this.EnsureMoreDataLoaded(this.ValueLength))
			{
				throw new ExchangeDataException("invalid data - premature end of file");
			}
			this.readState = Asn1Reader.ReadState.ReadValue;
			return new OID(this.readBuffer, this.readOffset, this.ValueLength);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		public byte[] ReadValueAsByteArray()
		{
			this.AssertAtTheBeginningOfValue();
			if (this.IsLongValue)
			{
				throw new InvalidOperationException("this value can be read only with a stream");
			}
			byte[] array = new byte[this.ValueLength];
			this.ReadRawValue(array, 0, array.Length, false);
			return array;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		public string ReadValueAsString()
		{
			this.AssertAtTheBeginningOfValue();
			if (this.IsLongValue)
			{
				throw new InvalidOperationException("this value can be read only with a stream");
			}
			if (this.decodeBuffer == null)
			{
				this.decodeBuffer = new char[512];
			}
			int num = this.ReadTextValue(this.decodeBuffer, 0, this.decodeBuffer.Length);
			if (this.StreamOffset != this.ValueStreamOffset + this.ValueLength || !this.decoderFlushed)
			{
				StringBuilder stringBuilder = new StringBuilder();
				do
				{
					stringBuilder.Append(this.decodeBuffer, 0, num);
					num = this.ReadTextValue(this.decodeBuffer, 0, this.decodeBuffer.Length);
				}
				while (num != 0);
				return stringBuilder.ToString();
			}
			if (num == 0)
			{
				return string.Empty;
			}
			return new string(this.decodeBuffer, 0, num);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		public int ReadTextValue(char[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Strings.OffsetOutOfRange");
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Strings.CountOutOfRange");
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", "Strings.CountTooLarge");
			}
			this.AssertGoodToUse(true);
			this.AssertCurrentTag();
			if (this.EncodingType != EncodingType.Utf8String && this.EncodingType != EncodingType.NumericString && this.EncodingType != EncodingType.PrintableString && this.EncodingType != EncodingType.TeletexString && this.EncodingType != EncodingType.VideotexString && this.EncodingType != EncodingType.IA5String && this.EncodingType != EncodingType.UtcTime && this.EncodingType != EncodingType.GeneralizedTime && this.EncodingType != EncodingType.GraphicString && this.EncodingType != EncodingType.VisibleString && this.EncodingType != EncodingType.GeneralString && this.EncodingType != EncodingType.UniversalString && this.EncodingType != EncodingType.BMPString && (this.EncodingType != EncodingType.Unknown || this.IsConstructedTag))
			{
				throw new InvalidOperationException("cannot convert value");
			}
			if (this.IsConstructedTag)
			{
				throw new InvalidOperationException("NYI");
			}
			if (this.readState == Asn1Reader.ReadState.BeginValue)
			{
				if (this.EncodingType == EncodingType.Utf8String)
				{
					this.decoder = this.Utf8Decoder;
				}
				else if (this.EncodingType == EncodingType.BMPString)
				{
					this.decoder = this.UnicodeDecoder;
				}
				else if (this.EncodingType == EncodingType.UniversalString)
				{
					this.decoder = this.Utf32Decoder;
				}
				else
				{
					this.decoder = this.AsciiDecoder;
				}
				this.decoder.Reset();
				this.decoderFlushed = false;
				this.readState = Asn1Reader.ReadState.ReadValue;
			}
			else if (this.decoder == null)
			{
				throw new InvalidOperationException("Strings.ReaderInvalidOperationPropTextAfterRaw");
			}
			int num = 0;
			while ((this.StreamOffset != this.ValueStreamOffset + this.ValueLength || !this.decoderFlushed) && count > 12)
			{
				if (!this.EnsureMoreDataLoaded(1))
				{
					throw new ExchangeDataException("invalid data - premature end of file");
				}
				int num2 = Math.Min(this.AvailableCount(), this.ValueStreamOffset + this.ValueLength - this.StreamOffset);
				int count2;
				int num3;
				this.decoder.Convert(this.readBuffer, this.readOffset, num2, buffer, offset, count, num2 == this.ValueStreamOffset + this.ValueLength - this.StreamOffset, out count2, out num3, out this.decoderFlushed);
				this.SkipBytes(count2);
				offset += num3;
				count -= num3;
				num += num3;
			}
			this.readState = Asn1Reader.ReadState.ReadValue;
			return num;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000C314 File Offset: 0x0000A514
		public int ReadBytesValue(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Strings.OffsetOutOfRange");
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Strings.CountOutOfRange");
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", "Strings.CountTooLarge");
			}
			return this.ReadRawValue(buffer, offset, count, false);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000C385 File Offset: 0x0000A585
		public void SetEncodingType(EncodingType encodingType)
		{
			this.tagStack[this.tagStackTop].EncodingType = encodingType;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C39E File Offset: 0x0000A59E
		public void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000C3AD File Offset: 0x0000A5AD
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		internal void AssertGoodToUse(bool affectsChild)
		{
			if (this.InputStream == null)
			{
				throw new ObjectDisposedException("Asn1Reader");
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C3D1 File Offset: 0x0000A5D1
		internal void AssertCurrentTag()
		{
			if (this.readState < Asn1Reader.ReadState.BeginValue)
			{
				throw new InvalidOperationException("Strings.ReaderInvalidOperationMustBeInTagValue");
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000C3E7 File Offset: 0x0000A5E7
		internal void AssertAtTheBeginningOfValue()
		{
			if (this.readState != Asn1Reader.ReadState.BeginValue)
			{
				throw new InvalidOperationException("Strings.ReaderInvalidOperationMustBeAtTheBeginningOfTagValue");
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000C400 File Offset: 0x0000A600
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.InputStream == null)
				{
					return;
				}
				this.InputStream.Flush();
				this.InputStream.Dispose();
			}
			this.InputStream = null;
			this.readBuffer = null;
			this.decoder = null;
			this.unicodeDecoder = null;
			this.utf8Decoder = null;
			this.utf32Decoder = null;
			this.asciiDecoder = null;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000C460 File Offset: 0x0000A660
		private bool ReadAndPushTag()
		{
			int num = this.tagStackTop + 1;
			if (num == this.tagStack.Length)
			{
				if (this.tagStack.Length >= 4096)
				{
					throw new ExchangeDataException("invalid data - nesting too deep");
				}
				Asn1Reader.TagInfo[] destinationArray = new Asn1Reader.TagInfo[this.tagStack.Length * 2];
				Array.Copy(this.tagStack, 0, destinationArray, 0, this.tagStack.Length);
				this.tagStack = destinationArray;
			}
			if (!this.EnsureMoreDataLoaded(2))
			{
				throw new ExchangeDataException("invalid data - premature end of file");
			}
			this.tagStack[num].TagIdByte = this.ReadByte();
			this.tagStack[num].EncodingType = EncodingType.Unknown;
			this.tagStack[num].TagNumber = (int)(this.tagStack[num].TagIdByte & 31);
			if (this.tagStack[num].TagIdByte < 64)
			{
				if (!Asn1Reader.ValidUniversalTags[(int)this.tagStack[num].TagIdByte])
				{
					throw new ExchangeDataException("invalid universal tag");
				}
				this.tagStack[num].EncodingType = (EncodingType)(this.tagStack[num].TagIdByte & 31);
			}
			if (this.tagStack[num].TagNumber == 31)
			{
				this.tagStack[num].TagNumber = this.ReadLongTagNumber();
			}
			if (!this.EnsureMoreDataLoaded(1))
			{
				throw new ExchangeDataException("invalid data - premature end of file");
			}
			byte b = this.ReadByte();
			if ((b & 128) == 0)
			{
				this.tagStack[num].ValueLength = (int)b;
			}
			else if (b == 128)
			{
				this.tagStack[num].ValueLength = -1;
				if ((this.tagStack[num].TagIdByte & 32) == 0)
				{
					throw new ExchangeDataException("invalid data - indefinite length for primitive type");
				}
			}
			else
			{
				int num2 = (int)(b & 127);
				if (num2 == 127)
				{
					throw new ExchangeDataException("invalid data - invalid length value");
				}
				if (num2 > 4)
				{
					throw new ExchangeDataException("invalid data - length field does not fit into integer");
				}
				if (!this.EnsureMoreDataLoaded(num2))
				{
					throw new ExchangeDataException("invalid data - premature end of file");
				}
				this.tagStack[num].ValueLength = this.ReadUnsignedInteger(num2);
				if (this.tagStack[num].ValueLength < 0 || this.StreamOffset + this.tagStack[num].ValueLength < this.StreamOffset)
				{
					throw new ExchangeDataException("invalid data - negative or extremely large content length");
				}
			}
			if ((this.tagStack[num].TagIdByte == 0 || this.tagStack[num].TagIdByte == 5) && this.tagStack[num].ValueLength != 0)
			{
				throw new ExchangeDataException("invalid length for null or end of content");
			}
			this.tagStack[num].ValueStreamOffset = this.StreamOffset;
			this.tagStack[num].SequenceNum = 0;
			if (this.tagStackTop >= 0)
			{
				Asn1Reader.TagInfo[] array = this.tagStack;
				int num3 = this.tagStackTop;
				array[num3].SequenceNum = array[num3].SequenceNum + 1;
			}
			if (this.tagStackTop >= 0 && this.ValueLength >= 0 && this.tagStack[num].ValueLength >= 0 && (long)this.tagStack[num].ValueStreamOffset + (long)this.tagStack[num].ValueLength > (long)this.ValueStreamOffset + (long)this.ValueLength)
			{
				throw new ExchangeDataException("nested tag overflows its parent");
			}
			this.tagStackTop++;
			return true;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		private bool PopTag()
		{
			this.tagStackTop--;
			if (this.tagStackTop < 0)
			{
				return false;
			}
			if (this.ValueLength >= 0 && this.StreamOffset > this.ValueStreamOffset + this.ValueLength)
			{
				throw new ExchangeDataException("invalid data - tag overflows its parent");
			}
			return true;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000C818 File Offset: 0x0000AA18
		private void PushBackTag()
		{
			this.tagStackTop++;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000C828 File Offset: 0x0000AA28
		private int ReadRawValue(byte[] buffer, int offset, int count, bool fromWrapper)
		{
			this.AssertGoodToUse(!fromWrapper);
			if (this.readState == Asn1Reader.ReadState.BeginValue)
			{
				if (this.IsConstructedTag)
				{
					throw new InvalidOperationException("NYI");
				}
				this.decoder = null;
				this.readState = Asn1Reader.ReadState.ReadValue;
			}
			else
			{
				if (this.readState != Asn1Reader.ReadState.ReadValue)
				{
					throw new InvalidOperationException("must be inside value");
				}
				if (this.decoder != null)
				{
					throw new InvalidOperationException("continue reading value as text");
				}
			}
			int num = 0;
			while (this.StreamOffset < this.ValueStreamOffset + this.ValueLength && count != 0)
			{
				if (!this.EnsureMoreDataLoaded(1))
				{
					throw new ExchangeDataException("invalid data - premature end of file");
				}
				int num2 = Math.Min(count, Math.Min(this.ValueStreamOffset + this.ValueLength - this.StreamOffset, this.AvailableCount()));
				this.ReadBytes(buffer, offset, num2);
				offset += num2;
				count -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C904 File Offset: 0x0000AB04
		private void SkipTagsToMarker()
		{
			int num = this.tagStackTop;
			for (;;)
			{
				if (this.ValueLength >= 0 && this.StreamOffset >= this.ValueStreamOffset + this.ValueLength)
				{
					this.PopTag();
				}
				else
				{
					this.ReadAndPushTag();
					if (this.TagIsEndOfContentMarker)
					{
						this.PopTag();
						if (this.tagStackTop == num)
						{
							break;
						}
					}
					else if (this.ValueLength >= 0)
					{
						this.SkipValue();
						this.PopTag();
					}
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000C978 File Offset: 0x0000AB78
		private void SkipValue()
		{
			if (this.StreamOffset < this.ValueStreamOffset + this.ValueLength)
			{
				int num2;
				for (int num = this.ValueStreamOffset + this.ValueLength - this.StreamOffset; num != 0; num -= num2)
				{
					if (!this.EnsureMoreDataLoaded(1))
					{
						throw new ExchangeDataException("invalid data - premature end of file");
					}
					num2 = Math.Min(num, this.AvailableCount());
					this.SkipBytes(num2);
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		private int AvailableCount()
		{
			return this.readEnd - this.readOffset;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000C9EF File Offset: 0x0000ABEF
		private bool EnsureMoreDataLoaded(int bytes)
		{
			return !this.NeedToLoadMoreData(bytes) || this.LoadMoreData(bytes);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000CA03 File Offset: 0x0000AC03
		private bool NeedToLoadMoreData(int bytes)
		{
			return this.AvailableCount() < bytes;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000CA10 File Offset: 0x0000AC10
		private bool LoadMoreData(int bytes)
		{
			if (this.endOfFile)
			{
				return false;
			}
			if (this.readBuffer.Length < bytes)
			{
				byte[] dst = new byte[Math.Max(this.readBuffer.Length * 2, bytes)];
				if (this.readEnd - this.readOffset != 0)
				{
					Buffer.BlockCopy(this.readBuffer, this.readOffset, dst, 0, this.readEnd - this.readOffset);
				}
				this.readBuffer = dst;
			}
			else if (this.readEnd - this.readOffset != 0 && this.readOffset != 0)
			{
				Buffer.BlockCopy(this.readBuffer, this.readOffset, this.readBuffer, 0, this.readEnd - this.readOffset);
			}
			int num = this.readOffset;
			this.readEnd -= this.readOffset;
			this.readOffset = 0;
			this.bufferStreamOffset += num;
			int num2 = this.InputStream.Read(this.readBuffer, this.readEnd, this.readBuffer.Length - this.readEnd);
			this.readEnd += num2;
			this.endOfFile = (num2 == 0);
			return this.readEnd >= bytes;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000CB38 File Offset: 0x0000AD38
		private byte ReadByte()
		{
			byte result = this.readBuffer[this.readOffset];
			this.readOffset++;
			return result;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000CB64 File Offset: 0x0000AD64
		private int ReadInteger(int length)
		{
			int num = 0;
			if (length != 0)
			{
				num = this.readBuffer[this.readOffset++] << 24 >> 24;
				while (--length != 0)
				{
					num = (num << 8 | (int)this.readBuffer[this.readOffset++]);
				}
			}
			return num;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		private long ReadLong(int length)
		{
			long num = 0L;
			if (length != 0)
			{
				num = (long)(this.readBuffer[this.readOffset++] << 24 >> 24);
				while (--length != 0)
				{
					num = (num << 8 | (long)((ulong)this.readBuffer[this.readOffset++]));
				}
			}
			return num;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		private int ReadUnsignedInteger(int length)
		{
			int num = 0;
			if (length != 0)
			{
				num = (int)this.readBuffer[this.readOffset++];
				while (--length != 0)
				{
					num = (num << 8 | (int)this.readBuffer[this.readOffset++]);
				}
			}
			return num;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000CC70 File Offset: 0x0000AE70
		private int ReadLongTagNumber()
		{
			int num = 0;
			while (this.EnsureMoreDataLoaded(1))
			{
				if (num >= 16777216)
				{
					throw new ExchangeDataException("invalid data - tag number is too big");
				}
				byte b = this.ReadByte();
				num = (num << 7) + (int)(b & 127);
				if ((b & 128) == 0)
				{
					return num;
				}
			}
			throw new ExchangeDataException("invalid data - premature end of file");
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		private void ReadBytes(byte[] buffer, int offset, int count)
		{
			Buffer.BlockCopy(this.readBuffer, this.readOffset, buffer, offset, count);
			this.readOffset += count;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000CCE4 File Offset: 0x0000AEE4
		private void SkipBytes(int count)
		{
			this.readOffset += count;
		}

		// Token: 0x04000281 RID: 641
		private const int ReadBufferSize = 4096;

		// Token: 0x04000282 RID: 642
		private const byte TagClassMask = 192;

		// Token: 0x04000283 RID: 643
		private const byte TagFormConstructed = 32;

		// Token: 0x04000284 RID: 644
		private const byte TagNumberMask = 31;

		// Token: 0x04000285 RID: 645
		private const byte TagNumberLong = 31;

		// Token: 0x04000286 RID: 646
		private const byte TagNumberContinuationMask = 127;

		// Token: 0x04000287 RID: 647
		private const byte TagNumberContinuationSentinel = 128;

		// Token: 0x04000288 RID: 648
		private const byte LengthLong = 128;

		// Token: 0x04000289 RID: 649
		private const byte LengthMask = 127;

		// Token: 0x0400028A RID: 650
		private const byte LengthIndefinite = 128;

		// Token: 0x0400028B RID: 651
		internal Stream InputStream;

		// Token: 0x0400028C RID: 652
		private static readonly bool[] ValidUniversalTags = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			true,
			false,
			true,
			true,
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			false,
			false,
			false,
			false,
			true,
			true,
			false,
			false,
			true,
			true,
			false,
			false,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false
		};

		// Token: 0x0400028D RID: 653
		private byte[] readBuffer;

		// Token: 0x0400028E RID: 654
		private int readOffset;

		// Token: 0x0400028F RID: 655
		private int readEnd;

		// Token: 0x04000290 RID: 656
		private bool endOfFile;

		// Token: 0x04000291 RID: 657
		private int bufferStreamOffset;

		// Token: 0x04000292 RID: 658
		private Asn1Reader.ReadState readState;

		// Token: 0x04000293 RID: 659
		private Asn1Reader.TagInfo[] tagStack;

		// Token: 0x04000294 RID: 660
		private int tagStackTop;

		// Token: 0x04000295 RID: 661
		private bool decoderFlushed;

		// Token: 0x04000296 RID: 662
		private Decoder decoder;

		// Token: 0x04000297 RID: 663
		private char[] decodeBuffer;

		// Token: 0x04000298 RID: 664
		private Decoder asciiDecoder;

		// Token: 0x04000299 RID: 665
		private Decoder utf8Decoder;

		// Token: 0x0400029A RID: 666
		private Decoder unicodeDecoder;

		// Token: 0x0400029B RID: 667
		private Decoder utf32Decoder;

		// Token: 0x02000037 RID: 55
		internal enum ReadState
		{
			// Token: 0x0400029D RID: 669
			EndOfFile,
			// Token: 0x0400029E RID: 670
			Begin,
			// Token: 0x0400029F RID: 671
			BeginValue,
			// Token: 0x040002A0 RID: 672
			ReadValue
		}

		// Token: 0x02000038 RID: 56
		private struct TagInfo
		{
			// Token: 0x040002A1 RID: 673
			public byte TagIdByte;

			// Token: 0x040002A2 RID: 674
			public EncodingType EncodingType;

			// Token: 0x040002A3 RID: 675
			public int TagNumber;

			// Token: 0x040002A4 RID: 676
			public int ValueLength;

			// Token: 0x040002A5 RID: 677
			public int ValueStreamOffset;

			// Token: 0x040002A6 RID: 678
			public int SequenceNum;
		}
	}
}
