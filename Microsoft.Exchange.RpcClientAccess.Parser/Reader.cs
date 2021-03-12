using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200003C RID: 60
	internal abstract class Reader : BaseObject
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000129 RID: 297
		public abstract long Length { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600012A RID: 298
		// (set) Token: 0x0600012B RID: 299
		public abstract long Position { get; set; }

		// Token: 0x0600012C RID: 300 RVA: 0x00004D60 File Offset: 0x00002F60
		public static BufferReader CreateBufferReader(byte[] buffer)
		{
			return Reader.CreateBufferReader(new ArraySegment<byte>(buffer, 0, buffer.Length));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004D71 File Offset: 0x00002F71
		public static BufferReader CreateBufferReader(ArraySegment<byte> arraySegment)
		{
			return new BufferReader(arraySegment);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004D79 File Offset: 0x00002F79
		public static Reader CreateStreamReader(Stream stream)
		{
			return new StreamReader(stream);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004D84 File Offset: 0x00002F84
		public byte PeekByte(long offset)
		{
			base.CheckDisposed();
			long position = this.Position;
			this.Position = position + offset;
			byte result = this.ReadByte();
			this.Position = position;
			return result;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public ushort PeekUInt16(long offset)
		{
			base.CheckDisposed();
			long position = this.Position;
			this.Position = position + offset;
			ushort result = this.ReadUInt16();
			this.Position = position;
			return result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004DEC File Offset: 0x00002FEC
		public uint PeekUInt32(long offset)
		{
			base.CheckDisposed();
			long position = this.Position;
			this.Position = position + offset;
			uint result = this.ReadUInt32();
			this.Position = position;
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004E1E File Offset: 0x0000301E
		public bool ReadBool()
		{
			base.CheckDisposed();
			return this.ReadByte() != 0;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004E32 File Offset: 0x00003032
		public byte ReadByte()
		{
			base.CheckDisposed();
			this.CheckCanRead(1);
			return this.InternalReadByte();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004E48 File Offset: 0x00003048
		public byte[] ReadBytes(uint count)
		{
			ArraySegment<byte> arraySegment = this.ReadArraySegment(count);
			if (arraySegment.Count == 0)
			{
				return Array<byte>.Empty;
			}
			byte[] array = new byte[arraySegment.Count];
			Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
			return array;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004E98 File Offset: 0x00003098
		public ArraySegment<byte> ReadArraySegment(uint count)
		{
			base.CheckDisposed();
			if (count == 0U)
			{
				return Array<byte>.EmptySegment;
			}
			this.CheckCanRead((int)count);
			return this.InternalReadArraySegment(count);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004EC4 File Offset: 0x000030C4
		public double ReadDouble()
		{
			base.CheckDisposed();
			this.CheckCanRead(8);
			return this.InternalReadDouble();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004EDC File Offset: 0x000030DC
		public Guid ReadGuid()
		{
			base.CheckDisposed();
			ArraySegment<byte> arraySegment = this.ReadArraySegment(16U);
			return ExBitConverter.ReadGuid(arraySegment.Array, arraySegment.Offset);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004F0B File Offset: 0x0000310B
		public byte[] ReadSizeAndByteArray()
		{
			return this.ReadSizeAndByteArray(FieldLength.WordSize);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004F14 File Offset: 0x00003114
		public byte[] ReadSizeAndByteArray(FieldLength lengthSize)
		{
			base.CheckDisposed();
			uint count = this.ReadCountOrSize(lengthSize);
			return this.ReadBytes(count);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004F36 File Offset: 0x00003136
		public ArraySegment<byte> ReadSizeAndByteArraySegment()
		{
			return this.ReadSizeAndByteArraySegment(FieldLength.WordSize);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004F40 File Offset: 0x00003140
		public ArraySegment<byte> ReadSizeAndByteArraySegment(FieldLength lengthSize)
		{
			base.CheckDisposed();
			uint count = this.ReadCountOrSize(lengthSize);
			return this.ReadArraySegment(count);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004F64 File Offset: 0x00003164
		public byte[][] ReadCountAndByteArrayList(FieldLength lengthSize)
		{
			base.CheckDisposed();
			uint num = this.ReadCountOrSize(lengthSize);
			byte[][] array = Array<byte[]>.Empty;
			if (num != 0U)
			{
				array = new byte[num][];
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					array[num2] = this.ReadSizeAndByteArray(lengthSize);
					num2++;
				}
			}
			return array;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004FA9 File Offset: 0x000031A9
		public short ReadInt16()
		{
			base.CheckDisposed();
			this.CheckCanRead(2);
			return this.InternalReadInt16();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004FBE File Offset: 0x000031BE
		public ushort ReadUInt16()
		{
			base.CheckDisposed();
			this.CheckCanRead(2);
			return this.InternalReadUInt16();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004FD3 File Offset: 0x000031D3
		public int ReadInt32()
		{
			base.CheckDisposed();
			this.CheckCanRead(4);
			return this.InternalReadInt32();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004FE8 File Offset: 0x000031E8
		public uint ReadUInt32()
		{
			base.CheckDisposed();
			this.CheckCanRead(4);
			return this.InternalReadUInt32();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004FFD File Offset: 0x000031FD
		public long ReadInt64()
		{
			base.CheckDisposed();
			this.CheckCanRead(8);
			return this.InternalReadInt64();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005012 File Offset: 0x00003212
		public ulong ReadUInt64()
		{
			base.CheckDisposed();
			this.CheckCanRead(8);
			return this.InternalReadUInt64();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005027 File Offset: 0x00003227
		public float ReadSingle()
		{
			base.CheckDisposed();
			this.CheckCanRead(4);
			return this.InternalReadSingle();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000503C File Offset: 0x0000323C
		public string ReadAsciiString(StringFlags flags)
		{
			String8 @string = this.ReadString8(flags);
			@string.ResolveString8Values(Reader.EncodingWithDecoderFallback(CTSGlobals.AsciiEncoding, flags));
			return @string.StringValue;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005068 File Offset: 0x00003268
		public String8 ReadString8(StringFlags flags)
		{
			ArraySegment<byte> encodedBytes = this.ReadStringSegment(flags);
			return new String8(encodedBytes);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005084 File Offset: 0x00003284
		public string ReadString8(Encoding encoding, StringFlags flags)
		{
			String8 @string = this.ReadString8(flags);
			if ((flags & StringFlags.IncludeNull) != StringFlags.IncludeNull)
			{
				throw new NotSupportedException("Reading non-terminated String8 values is not supported.");
			}
			@string.ResolveString8Values(Reader.EncodingWithDecoderFallback(encoding, flags));
			return @string.StringValue;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000050BD File Offset: 0x000032BD
		public string ReadUnicodeString(StringFlags flags)
		{
			base.CheckDisposed();
			if ((flags & (StringFlags.IncludeNull | StringFlags.Sized | StringFlags.Sized16 | StringFlags.Sized32)) == StringFlags.None)
			{
				throw new ArgumentException("This ReadString only allows Sized and/or IncludeNull strings.");
			}
			if ((flags & (StringFlags.Sized | StringFlags.Sized16 | StringFlags.Sized32)) == StringFlags.None)
			{
				return this.ReadVariableLengthUnicodeString(flags);
			}
			return this.ReadFixedLengthUnicodeString(flags);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000050EC File Offset: 0x000032EC
		public SecurityIdentifier ReadSecurityIdentifier()
		{
			uint count = (uint)(8 + 4 * this.PeekByte(1L));
			byte[] binaryForm = this.ReadBytes(count);
			SecurityIdentifier result;
			try
			{
				result = new SecurityIdentifier(binaryForm, 0);
			}
			catch (ArgumentException)
			{
				throw new BufferParseException("Invalid SecurityIdentifier");
			}
			return result;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005138 File Offset: 0x00003338
		public PropertyTag ReadPropertyTag()
		{
			return new PropertyTag(this.ReadUInt32());
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005148 File Offset: 0x00003348
		public PropertyValue ReadPropertyValue(WireFormatStyle wireFormatStyle)
		{
			PropertyTag propertyTag = this.ReadPropertyTag();
			return this.ReadPropertyValueForTag(propertyTag, wireFormatStyle);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005164 File Offset: 0x00003364
		public PropertyValue? ReadNullablePropertyValue(WireFormatStyle wireFormatStyle)
		{
			if (wireFormatStyle == WireFormatStyle.Nspi && !this.ReadBool())
			{
				return null;
			}
			return new PropertyValue?(this.ReadPropertyValue(wireFormatStyle));
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005194 File Offset: 0x00003394
		public PropertyValue ReadPropertyValueForTag(PropertyTag propertyTag, WireFormatStyle wireFormatStyle)
		{
			object obj = this.ReadPropertyValueForType(propertyTag.PropertyType, wireFormatStyle);
			if (obj == null)
			{
				return PropertyValue.NullValue(propertyTag);
			}
			return new PropertyValue(propertyTag, obj);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000051C4 File Offset: 0x000033C4
		internal void CheckBoundary(uint estimateCount, uint elementSize)
		{
			ulong num = (ulong)(this.Length - this.Position);
			if (num / (ulong)elementSize < (ulong)estimateCount)
			{
				this.Position = this.Length;
				string message = string.Format("Buffer not large enough to accomodate sized array based on minimum element size.  Count = {0}", estimateCount);
				throw new BufferParseException(message);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000520C File Offset: 0x0000340C
		internal uint ReadCountOrSize(FieldLength countLength)
		{
			uint result;
			switch (countLength)
			{
			case FieldLength.WordSize:
				result = (uint)this.ReadUInt16();
				break;
			case FieldLength.DWordSize:
				result = this.ReadUInt32();
				break;
			default:
				throw new ArgumentException("Unrecognized FieldLength: " + countLength, "lengthSize");
			}
			return result;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000525C File Offset: 0x0000345C
		internal uint ReadCountOrSize(WireFormatStyle wireFormatStyle)
		{
			uint result;
			switch (wireFormatStyle)
			{
			case WireFormatStyle.Rop:
				result = (uint)this.ReadUInt16();
				break;
			case WireFormatStyle.Nspi:
				result = this.ReadUInt32();
				break;
			default:
				throw new ArgumentException("Unrecognized wire format style: " + wireFormatStyle, "wireFormatStyle");
			}
			return result;
		}

		// Token: 0x06000150 RID: 336
		protected abstract byte InternalReadByte();

		// Token: 0x06000151 RID: 337
		protected abstract double InternalReadDouble();

		// Token: 0x06000152 RID: 338
		protected abstract short InternalReadInt16();

		// Token: 0x06000153 RID: 339
		protected abstract ushort InternalReadUInt16();

		// Token: 0x06000154 RID: 340
		protected abstract int InternalReadInt32();

		// Token: 0x06000155 RID: 341
		protected abstract uint InternalReadUInt32();

		// Token: 0x06000156 RID: 342
		protected abstract long InternalReadInt64();

		// Token: 0x06000157 RID: 343
		protected abstract ulong InternalReadUInt64();

		// Token: 0x06000158 RID: 344
		protected abstract float InternalReadSingle();

		// Token: 0x06000159 RID: 345
		protected abstract ArraySegment<byte> InternalReadArraySegment(uint count);

		// Token: 0x0600015A RID: 346
		protected abstract ArraySegment<byte> InternalReadArraySegmentForString(int maxCount);

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000052AB File Offset: 0x000034AB
		protected virtual bool NeedsStagingAreaForFixedLengthStrings
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000052AE File Offset: 0x000034AE
		protected byte[] StringBuffer
		{
			get
			{
				return this.charBytes;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000052B6 File Offset: 0x000034B6
		private static ushort ValidateAsciiChar(ushort c, StringFlags flags)
		{
			if (c <= 127)
			{
				return c;
			}
			if ((flags & StringFlags.SevenBitAsciiOrFail) == StringFlags.SevenBitAsciiOrFail)
			{
				throw new BufferParseException("Invalid ASCII character");
			}
			return 63;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000052D4 File Offset: 0x000034D4
		private static string ValidateString(StringBuilder sb, StringFlags flags)
		{
			if (sb.Length == 0)
			{
				return string.Empty;
			}
			int num = sb.Length;
			if ((flags & StringFlags.IncludeNull) == StringFlags.IncludeNull)
			{
				if (sb[sb.Length - 1] != '\0')
				{
					throw new BufferParseException("String doesn't contain a null.");
				}
				num--;
			}
			for (int i = 0; i < num; i++)
			{
				if (sb[i] == '\0')
				{
					num = i;
					break;
				}
				if ((flags & StringFlags.SevenBitAscii) == StringFlags.SevenBitAscii)
				{
					sb[i] = (char)Reader.ValidateAsciiChar((ushort)sb[i], flags);
				}
			}
			if (num != 0)
			{
				return sb.ToString(0, num);
			}
			return string.Empty;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005360 File Offset: 0x00003560
		private ArraySegment<byte> ReadStringSegment(StringFlags flags)
		{
			if ((flags & StringFlags.Sized) != StringFlags.None)
			{
				uint count = (uint)this.ReadByte();
				return this.ReadArraySegment(count);
			}
			if ((flags & StringFlags.Sized16) != StringFlags.None)
			{
				uint count2 = (uint)this.ReadUInt16();
				return this.ReadArraySegment(count2);
			}
			uint num = 0U;
			uint num2 = (uint)(this.Length - this.Position);
			while (this.PeekByte((long)((ulong)num)) != 0)
			{
				num += 1U;
				if (num >= num2)
				{
					throw new BufferParseException("End of buffer reached prematurely.");
				}
			}
			num += 1U;
			return this.ReadArraySegment(num);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000053D0 File Offset: 0x000035D0
		private string ReadVariableLengthUnicodeString(StringFlags flags)
		{
			ushort num = this.ReadUInt16();
			if (num == 0)
			{
				return string.Empty;
			}
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder(Reader.MaxUnicodeCharsSize);
			Decoder decoder = Encoding.Unicode.GetDecoder();
			if (this.charBytes == null)
			{
				this.charBytes = new byte[128];
			}
			if (this.charUnicodeBuffer == null)
			{
				this.charUnicodeBuffer = new char[Reader.MaxUnicodeCharsSize];
			}
			if ((flags & StringFlags.SevenBitAscii) == StringFlags.SevenBitAscii)
			{
				num = Reader.ValidateAsciiChar(num, flags);
			}
			num2 += ExBitConverter.Write(num, this.charBytes, num2);
			while ((num = this.ReadUInt16()) != 0)
			{
				if ((flags & StringFlags.SevenBitAscii) == StringFlags.SevenBitAscii)
				{
					num = Reader.ValidateAsciiChar(num, flags);
				}
				if (num2 >= this.charBytes.Length)
				{
					int chars = decoder.GetChars(this.charBytes, 0, num2, this.charUnicodeBuffer, 0, false);
					stringBuilder.Append(this.charUnicodeBuffer, 0, chars);
					num2 = 0;
				}
				num2 += ExBitConverter.Write(num, this.charBytes, num2);
			}
			if (num2 > 0)
			{
				int chars2 = decoder.GetChars(this.charBytes, 0, num2, this.charUnicodeBuffer, 0, true);
				stringBuilder.Append(this.charUnicodeBuffer, 0, chars2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000054E8 File Offset: 0x000036E8
		private string ReadFixedLengthUnicodeString(StringFlags flags)
		{
			uint length;
			if ((flags & StringFlags.Sized) == StringFlags.Sized)
			{
				length = (uint)this.ReadByte();
			}
			else if ((flags & StringFlags.Sized16) == StringFlags.Sized16)
			{
				length = (uint)this.ReadUInt16();
			}
			else
			{
				length = this.ReadUInt32();
			}
			return this.ReadFixedLengthUnicodeString(flags, length);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005524 File Offset: 0x00003724
		private string ReadFixedLengthUnicodeString(StringFlags flags, uint length)
		{
			int num = 2;
			if ((flags & StringFlags.IncludeNull) == StringFlags.IncludeNull && length < (uint)num)
			{
				throw new BufferParseException("Length cannot be less than a single character when IncludeNull is specified.");
			}
			if (length == 0U)
			{
				return string.Empty;
			}
			if (length % 2U != 0U)
			{
				throw new BufferParseException("Unicode string is not even length.");
			}
			Decoder decoder = Encoding.Unicode.GetDecoder();
			if (this.charUnicodeBuffer == null)
			{
				this.charUnicodeBuffer = new char[Reader.MaxUnicodeCharsSize];
			}
			char[] array = this.charUnicodeBuffer;
			if (this.charBytes == null && this.NeedsStagingAreaForFixedLengthStrings)
			{
				this.charBytes = new byte[128];
			}
			StringBuilder stringBuilder = new StringBuilder(checked((int)length));
			uint num2 = 0U;
			for (;;)
			{
				int maxCount = Math.Min((int)(length - num2), 128);
				ArraySegment<byte> arraySegment = this.InternalReadArraySegmentForString(maxCount);
				if (arraySegment.Count == 0)
				{
					break;
				}
				int chars = decoder.GetChars(arraySegment.Array, arraySegment.Offset, arraySegment.Count, array, 0);
				stringBuilder.Append(array, 0, chars);
				num2 += (uint)arraySegment.Count;
				if (num2 >= length)
				{
					goto Block_9;
				}
			}
			throw new BufferParseException("End of buffer reached prematurely.");
			Block_9:
			return Reader.ValidateString(stringBuilder, flags);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005626 File Offset: 0x00003826
		private void CheckCanRead(int size)
		{
			if (this.Position > this.Length - (long)size)
			{
				this.Position = this.Length;
				throw new BufferParseException("End of buffer reached prematurely.");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005650 File Offset: 0x00003850
		private static int MaxUnicodeCharsSize
		{
			get
			{
				if (Reader.maxUnicodeCharsSize == 0)
				{
					Reader.maxUnicodeCharsSize = Encoding.Unicode.GetMaxCharCount(128);
				}
				return Reader.maxUnicodeCharsSize;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005672 File Offset: 0x00003872
		private static int MaxAsciiCharsSize
		{
			get
			{
				if (Reader.maxAsciiCharsSize == 0)
				{
					Reader.maxAsciiCharsSize = CTSGlobals.AsciiEncoding.GetMaxCharCount(128);
				}
				return Reader.maxAsciiCharsSize;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005694 File Offset: 0x00003894
		private bool TryReadHasValue(WireFormatStyle wireFormatStyle)
		{
			return wireFormatStyle != WireFormatStyle.Nspi || this.ReadBool();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000056A4 File Offset: 0x000038A4
		private object ReadPropertyValueForType(PropertyType propertyType, WireFormatStyle wireFormatStyle)
		{
			bool flag = wireFormatStyle == WireFormatStyle.Nspi;
			if (propertyType <= PropertyType.Binary)
			{
				if (propertyType <= PropertyType.SysTime)
				{
					switch (propertyType)
					{
					case PropertyType.Unspecified:
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)12:
					case (PropertyType)14:
					case (PropertyType)15:
					case (PropertyType)16:
					case (PropertyType)17:
					case (PropertyType)18:
					case (PropertyType)19:
						break;
					case PropertyType.Null:
						return null;
					case PropertyType.Int16:
						return this.ReadInt16();
					case PropertyType.Int32:
						return this.ReadInt32();
					case PropertyType.Float:
						return this.ReadSingle();
					case PropertyType.Double:
					case PropertyType.AppTime:
						return this.ReadDouble();
					case PropertyType.Currency:
					case PropertyType.Int64:
						return this.ReadInt64();
					case PropertyType.Error:
						return (ErrorCode)this.ReadUInt32();
					case PropertyType.Bool:
						return this.ReadBool();
					case PropertyType.Object:
						if (wireFormatStyle != WireFormatStyle.Nspi)
						{
							throw new NotImplementedException(string.Format("Property type not implemented: {0}.", propertyType));
						}
						goto IL_3B1;
					default:
						switch (propertyType)
						{
						case PropertyType.String8:
							if (this.TryReadHasValue(wireFormatStyle))
							{
								return this.ReadString8(StringFlags.IncludeNull);
							}
							goto IL_3B1;
						case PropertyType.Unicode:
							if (this.TryReadHasValue(wireFormatStyle))
							{
								return this.ReadUnicodeString(StringFlags.IncludeNull);
							}
							goto IL_3B1;
						default:
							if (propertyType == PropertyType.SysTime)
							{
								long fileTimeAsInt = this.ReadInt64();
								return PropertyValue.ExDateTimeFromFileTimeUtc(fileTimeAsInt);
							}
							break;
						}
						break;
					}
				}
				else if (propertyType != PropertyType.Guid)
				{
					switch (propertyType)
					{
					case PropertyType.ServerId:
						break;
					case (PropertyType)252:
						goto IL_39B;
					case PropertyType.Restriction:
						if (this.TryReadHasValue(wireFormatStyle))
						{
							return Restriction.Parse(this, wireFormatStyle);
						}
						goto IL_3B1;
					case PropertyType.Actions:
						if (wireFormatStyle == WireFormatStyle.Nspi)
						{
							throw new NotSupportedException(string.Format("Property type not supported: {0}.", propertyType));
						}
						return RuleAction.Parse(this);
					default:
						if (propertyType != PropertyType.Binary)
						{
							goto IL_39B;
						}
						break;
					}
					if (this.TryReadHasValue(wireFormatStyle))
					{
						uint count = this.ReadCountOrSize(wireFormatStyle);
						return this.ReadBytes(count);
					}
					goto IL_3B1;
				}
				else
				{
					if (this.TryReadHasValue(wireFormatStyle))
					{
						return this.ReadGuid();
					}
					goto IL_3B1;
				}
			}
			else if (propertyType <= PropertyType.MultiValueUnicode)
			{
				switch (propertyType)
				{
				case PropertyType.MultiValueInt16:
					if (this.TryReadHasValue(wireFormatStyle))
					{
						return this.ReadPropertyMultiValueForTagInternal<short>(PropertyType.Int16, flag ? 1U : 2U, wireFormatStyle);
					}
					goto IL_3B1;
				case PropertyType.MultiValueInt32:
					if (this.TryReadHasValue(wireFormatStyle))
					{
						return this.ReadPropertyMultiValueForTagInternal<int>(PropertyType.Int32, flag ? 1U : 4U, wireFormatStyle);
					}
					goto IL_3B1;
				case PropertyType.MultiValueFloat:
					if (this.TryReadHasValue(wireFormatStyle))
					{
						return this.ReadPropertyMultiValueForTagInternal<float>(PropertyType.Float, flag ? 1U : 4U, wireFormatStyle);
					}
					goto IL_3B1;
				case PropertyType.MultiValueDouble:
					if (this.TryReadHasValue(wireFormatStyle))
					{
						return this.ReadPropertyMultiValueForTagInternal<double>(PropertyType.Double, flag ? 1U : 8U, wireFormatStyle);
					}
					goto IL_3B1;
				case PropertyType.MultiValueCurrency:
					if (this.TryReadHasValue(wireFormatStyle))
					{
						return this.ReadPropertyMultiValueForTagInternal<long>(PropertyType.Currency, flag ? 1U : 8U, wireFormatStyle);
					}
					goto IL_3B1;
				case PropertyType.MultiValueAppTime:
					if (this.TryReadHasValue(wireFormatStyle))
					{
						return this.ReadPropertyMultiValueForTagInternal<double>(PropertyType.AppTime, flag ? 1U : 8U, wireFormatStyle);
					}
					goto IL_3B1;
				default:
					if (propertyType != PropertyType.MultiValueInt64)
					{
						switch (propertyType)
						{
						case PropertyType.MultiValueString8:
							if (this.TryReadHasValue(wireFormatStyle))
							{
								return this.ReadPropertyMultiValueForTagInternal<String8>(PropertyType.String8, 1U, wireFormatStyle);
							}
							goto IL_3B1;
						case PropertyType.MultiValueUnicode:
							if (this.TryReadHasValue(wireFormatStyle))
							{
								return this.ReadPropertyMultiValueForTagInternal<string>(PropertyType.Unicode, flag ? 1U : 2U, wireFormatStyle);
							}
							goto IL_3B1;
						}
					}
					else
					{
						if (this.TryReadHasValue(wireFormatStyle))
						{
							return this.ReadPropertyMultiValueForTagInternal<long>(PropertyType.Int64, flag ? 1U : 8U, wireFormatStyle);
						}
						goto IL_3B1;
					}
					break;
				}
			}
			else if (propertyType != PropertyType.MultiValueSysTime)
			{
				if (propertyType != PropertyType.MultiValueGuid)
				{
					if (propertyType == PropertyType.MultiValueBinary)
					{
						if (this.TryReadHasValue(wireFormatStyle))
						{
							return this.ReadPropertyMultiValueForTagInternal<byte[]>(PropertyType.Binary, flag ? 1U : 2U, wireFormatStyle);
						}
						goto IL_3B1;
					}
				}
				else
				{
					if (this.TryReadHasValue(wireFormatStyle))
					{
						return this.ReadPropertyMultiValueForTagInternal<Guid>(PropertyType.Guid, flag ? 1U : 16U, wireFormatStyle);
					}
					goto IL_3B1;
				}
			}
			else
			{
				if (this.TryReadHasValue(wireFormatStyle))
				{
					return this.ReadPropertyMultiValueForTagInternal<ExDateTime>(PropertyType.SysTime, flag ? 1U : 8U, wireFormatStyle);
				}
				goto IL_3B1;
			}
			IL_39B:
			throw new NotSupportedException(string.Format("Property type not supported: {0}.", propertyType));
			IL_3B1:
			return null;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005A64 File Offset: 0x00003C64
		private T[] ReadPropertyMultiValueForTagInternal<T>(PropertyType elementPropertyType, uint minimumElementSize, WireFormatStyle wireFormatStyle)
		{
			uint num = this.ReadUInt32();
			this.CheckBoundary(num, minimumElementSize);
			T[] array = new T[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				object obj = this.ReadPropertyValueForType(elementPropertyType, wireFormatStyle);
				if (obj != null)
				{
					array[num2] = (T)((object)obj);
				}
				num2++;
			}
			return array;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005AB0 File Offset: 0x00003CB0
		private static Encoding EncodingWithDecoderFallback(Encoding encoding, StringFlags flags)
		{
			if (encoding == CTSGlobals.AsciiEncoding && (flags & StringFlags.FailOnError) == StringFlags.FailOnError)
			{
				Encoding encoding2 = (Encoding)CTSGlobals.AsciiEncoding.Clone();
				encoding2.DecoderFallback = new Reader.BufferParseExceptionFallback();
				return encoding2;
			}
			return encoding;
		}

		// Token: 0x040000BE RID: 190
		private const int MaxCharBytesSize = 128;

		// Token: 0x040000BF RID: 191
		private const char FallbackCharacter = '?';

		// Token: 0x040000C0 RID: 192
		public const uint GuidSize = 16U;

		// Token: 0x040000C1 RID: 193
		private static int maxUnicodeCharsSize;

		// Token: 0x040000C2 RID: 194
		private static int maxAsciiCharsSize;

		// Token: 0x040000C3 RID: 195
		private byte[] charBytes;

		// Token: 0x040000C4 RID: 196
		private char[] charUnicodeBuffer;

		// Token: 0x0200003D RID: 61
		private sealed class BufferParseExceptionFallback : DecoderFallback
		{
			// Token: 0x0600016B RID: 363 RVA: 0x00005AF3 File Offset: 0x00003CF3
			public override DecoderFallbackBuffer CreateFallbackBuffer()
			{
				return new Reader.BufferParseExceptionFallbackBuffer();
			}

			// Token: 0x0600016C RID: 364 RVA: 0x00005AFA File Offset: 0x00003CFA
			public override bool Equals(object value)
			{
				return value is Reader.BufferParseExceptionFallback;
			}

			// Token: 0x0600016D RID: 365 RVA: 0x00005B05 File Offset: 0x00003D05
			public override int GetHashCode()
			{
				return 880;
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600016E RID: 366 RVA: 0x00005B0C File Offset: 0x00003D0C
			public override int MaxCharCount
			{
				get
				{
					return 0;
				}
			}
		}

		// Token: 0x0200003E RID: 62
		public sealed class BufferParseExceptionFallbackBuffer : DecoderFallbackBuffer
		{
			// Token: 0x06000170 RID: 368 RVA: 0x00005B17 File Offset: 0x00003D17
			public override bool Fallback(byte[] bytesUnknown, int index)
			{
				throw new BufferParseException("Contains an invalid character in this encoding");
			}

			// Token: 0x06000171 RID: 369 RVA: 0x00005B23 File Offset: 0x00003D23
			public override char GetNextChar()
			{
				return '\0';
			}

			// Token: 0x06000172 RID: 370 RVA: 0x00005B26 File Offset: 0x00003D26
			public override bool MovePrevious()
			{
				return false;
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000173 RID: 371 RVA: 0x00005B29 File Offset: 0x00003D29
			public override int Remaining
			{
				get
				{
					return 0;
				}
			}
		}
	}
}
