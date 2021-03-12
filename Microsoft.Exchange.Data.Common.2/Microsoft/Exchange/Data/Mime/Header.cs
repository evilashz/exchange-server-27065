using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200001F RID: 31
	public abstract class Header : MimeNode
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005B5B File Offset: 0x00003D5B
		internal Header(string name, HeaderId headerId)
		{
			this.name = name;
			this.headerId = headerId;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005B7D File Offset: 0x00003D7D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005B85 File Offset: 0x00003D85
		public HeaderId HeaderId
		{
			get
			{
				return this.headerId;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000147 RID: 327
		// (set) Token: 0x06000148 RID: 328
		public abstract string Value { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005B8D File Offset: 0x00003D8D
		public virtual bool RequiresSMTPUTF8
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005B90 File Offset: 0x00003D90
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00005BC6 File Offset: 0x00003DC6
		internal virtual byte[] RawValue
		{
			get
			{
				if (this.lines.Length == 0)
				{
					return MimeString.EmptyByteArray;
				}
				byte[] array = this.lines.GetSz();
				if (array == null)
				{
					array = MimeString.EmptyByteArray;
				}
				return array;
			}
			set
			{
				this.SetRawValue(value, true);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005BD0 File Offset: 0x00003DD0
		internal MimeString FirstRawToken
		{
			get
			{
				MimeStringList mimeStringList;
				if (this.lines.Length == 0)
				{
					byte[] rawValue = this.RawValue;
					mimeStringList = new MimeStringList(rawValue);
				}
				else
				{
					mimeStringList = this.lines;
				}
				DecodingOptions headerDecodingOptions = base.GetHeaderDecodingOptions();
				ValueParser valueParser = new ValueParser(mimeStringList, headerDecodingOptions.AllowUTF8);
				MimeStringList mimeStringList2 = default(MimeStringList);
				valueParser.ParseCFWS(false, ref mimeStringList2, true);
				return valueParser.ParseToken();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005C32 File Offset: 0x00003E32
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00005C3A File Offset: 0x00003E3A
		internal MimeStringList Lines
		{
			get
			{
				return this.lines;
			}
			set
			{
				this.SetRawValue(value, true);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005C44 File Offset: 0x00003E44
		internal int RawLength
		{
			get
			{
				return this.lines.Length;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005C51 File Offset: 0x00003E51
		internal bool IsDirty
		{
			get
			{
				return this.dirty;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00005C5C File Offset: 0x00003E5C
		internal bool IsProtected
		{
			get
			{
				MimePart mimePart = base.GetTreeRoot() as MimePart;
				return mimePart != null && mimePart.IsProtectedHeader(this.Name);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005C8C File Offset: 0x00003E8C
		public static Header Create(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			HeaderId headerId = Header.GetHeaderId(name, true);
			if (headerId != HeaderId.Unknown)
			{
				return Header.Create(name, headerId);
			}
			return new TextHeader(name, HeaderId.Unknown);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005CC1 File Offset: 0x00003EC1
		public static Header Create(HeaderId headerId)
		{
			return Header.Create(null, headerId);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005CCC File Offset: 0x00003ECC
		internal static Header Create(string name, HeaderId headerId)
		{
			if (headerId < HeaderId.Unknown || headerId > (HeaderId)MimeData.nameIndex.Length)
			{
				throw new ArgumentException(Strings.InvalidHeaderId, "headerId");
			}
			if (headerId == HeaderId.Unknown)
			{
				throw new ArgumentException(Strings.CannotDetermineHeaderNameFromId, "headerId");
			}
			Header header;
			switch (MimeData.headerNames[(int)MimeData.nameIndex[(int)headerId]].headerType)
			{
			case HeaderType.AsciiText:
				header = new AsciiTextHeader(MimeData.headerNames[(int)MimeData.nameIndex[(int)headerId]].name, headerId);
				break;
			case HeaderType.Date:
				header = new DateHeader(MimeData.headerNames[(int)MimeData.nameIndex[(int)headerId]].name, headerId);
				break;
			case HeaderType.Received:
				header = new ReceivedHeader();
				break;
			case HeaderType.ContentType:
				header = new ContentTypeHeader();
				break;
			case HeaderType.ContentDisposition:
				header = new ContentDispositionHeader();
				break;
			case HeaderType.Address:
				header = new AddressHeader(MimeData.headerNames[(int)MimeData.nameIndex[(int)headerId]].name, headerId);
				break;
			default:
				header = new TextHeader(MimeData.headerNames[(int)MimeData.nameIndex[(int)headerId]].name, headerId);
				break;
			}
			if (!string.IsNullOrEmpty(name) && !header.MatchName(name))
			{
				throw new ArgumentException("name");
			}
			return header;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005DFC File Offset: 0x00003FFC
		public static bool IsHeaderNameValid(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			for (int i = 0; i < name.Length; i++)
			{
				if (name[i] >= '\u0080' || !MimeScan.IsField((byte)name[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005E44 File Offset: 0x00004044
		public static HeaderId GetHeaderId(string name)
		{
			return Header.GetHeaderId(name, true);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005E4D File Offset: 0x0000404D
		public static Header ReadFrom(MimeHeaderReader reader)
		{
			if (reader.MimeReader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return reader.MimeReader.ReadHeaderObject();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005E6F File Offset: 0x0000406F
		public virtual bool TryGetValue(out string value)
		{
			value = this.Value;
			return true;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005E7A File Offset: 0x0000407A
		public virtual bool IsValueValid(string value)
		{
			return false;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005E80 File Offset: 0x00004080
		public override void CopyTo(object destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == this)
			{
				return;
			}
			Header header = destination as Header;
			if (header == null)
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			base.CopyTo(destination);
			header.lines = this.lines.Clone();
			header.dirty = this.dirty;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005EDC File Offset: 0x000040DC
		internal static Type TypeFromHeaderId(HeaderId headerId)
		{
			switch (MimeData.headerNames[(int)MimeData.nameIndex[(int)headerId]].headerType)
			{
			case HeaderType.AsciiText:
				return typeof(AsciiTextHeader);
			case HeaderType.Date:
				return typeof(DateHeader);
			case HeaderType.Received:
				return typeof(ReceivedHeader);
			case HeaderType.ContentType:
				return typeof(ContentTypeHeader);
			case HeaderType.ContentDisposition:
				return typeof(ContentDispositionHeader);
			case HeaderType.Address:
				return typeof(AddressHeader);
			default:
				return typeof(TextHeader);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005F70 File Offset: 0x00004170
		internal static HeaderId GetHeaderId(string name, bool validateArgument)
		{
			if (name == null)
			{
				if (validateArgument)
				{
					throw new ArgumentNullException("name");
				}
				return HeaderId.Unknown;
			}
			else
			{
				if (name.Length != 0)
				{
					if (validateArgument)
					{
						for (int i = 0; i < name.Length; i++)
						{
							if (name[i] >= '\u0080' || !MimeScan.IsField((byte)name[i]))
							{
								throw new ArgumentException(Strings.InvalidHeaderName(name, i), "name");
							}
						}
					}
					HeaderNameIndex headerNameIndex = Header.LookupName(name);
					return MimeData.headerNames[(int)headerNameIndex].publicHeaderId;
				}
				if (validateArgument)
				{
					throw new ArgumentException("Header name cannot be an empty string", "name");
				}
				return HeaderId.Unknown;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006008 File Offset: 0x00004208
		internal static HeaderId GetHeaderId(byte[] name, int offset, int length)
		{
			HeaderNameIndex headerNameIndex = Header.LookupName(name, offset, length);
			return MimeData.headerNames[(int)headerNameIndex].publicHeaderId;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000602E File Offset: 0x0000422E
		internal static string GetHeaderName(HeaderId headerId)
		{
			return MimeData.headerNames[(int)MimeData.nameIndex[(int)headerId]].name;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006046 File Offset: 0x00004246
		internal static Header CreateGeneralHeader(string name)
		{
			return new TextHeader(name, HeaderId.Unknown);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000604F File Offset: 0x0000424F
		internal static bool IsRestrictedHeader(HeaderId headerId)
		{
			return MimeData.headerNames[(int)MimeData.nameIndex[(int)headerId]].restricted;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006068 File Offset: 0x00004268
		internal static long WriteLines(MimeStringList lines, Stream stream)
		{
			if (lines.Count == 0)
			{
				MimeStringLength mimeStringLength = new MimeStringLength(0);
				return Header.WriteLineEnd(stream, ref mimeStringLength);
			}
			long num = 0L;
			for (int i = 0; i < lines.Count; i++)
			{
				int num2;
				int num3;
				byte[] data = lines[i].GetData(out num2, out num3);
				if (num3 != 0)
				{
					if (!MimeScan.IsLWSP(data[num2]))
					{
						stream.Write(MimeString.Space, 0, MimeString.Space.Length);
						num += (long)MimeString.Space.Length;
					}
					stream.Write(data, num2, num3);
					num += (long)num3;
				}
				MimeStringLength mimeStringLength2 = new MimeStringLength(0);
				num += Header.WriteLineEnd(stream, ref mimeStringLength2);
			}
			return num;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006118 File Offset: 0x00004318
		private static long WriteToken(byte[] token, int tokenOffset, MimeStringLength tokenLength, Stream stream, ref MimeStringLength currentLineLength, ref Header.LineBuffer lineBuffer, ref bool autoAddedLastLWSP, bool allowUTF8)
		{
			long num = 0L;
			bool flag = token != null && tokenLength.InChars == 1 && MimeScan.IsFWSP(token[tokenOffset]);
			if (!flag && currentLineLength.InChars + lineBuffer.Length.InChars + tokenLength.InChars > 78 && lineBuffer.LengthTillLastLWSP.InBytes >= 0)
			{
				if (lineBuffer.LengthTillLastLWSP.InBytes > 0)
				{
					stream.Write(lineBuffer.Bytes, 0, lineBuffer.LengthTillLastLWSP.InBytes);
					num += (long)lineBuffer.LengthTillLastLWSP.InBytes;
					currentLineLength.IncrementBy(lineBuffer.LengthTillLastLWSP);
				}
				if (currentLineLength.InBytes > 0)
				{
					num += Header.WriteLineEnd(stream, ref currentLineLength);
				}
				if (autoAddedLastLWSP)
				{
					autoAddedLastLWSP = false;
					lineBuffer.LengthTillLastLWSP.IncrementBy(1);
				}
				if (lineBuffer.LengthTillLastLWSP.InBytes != lineBuffer.Length.InBytes)
				{
					Buffer.BlockCopy(lineBuffer.Bytes, lineBuffer.LengthTillLastLWSP.InBytes, lineBuffer.Bytes, 0, lineBuffer.Length.InBytes - lineBuffer.LengthTillLastLWSP.InBytes);
					lineBuffer.Length.DecrementBy(lineBuffer.LengthTillLastLWSP);
					if (lineBuffer.Length.InBytes > 0 && MimeScan.IsFWSP(lineBuffer.Bytes[0]))
					{
						lineBuffer.LengthTillLastLWSP.SetAs(0);
					}
					else
					{
						lineBuffer.LengthTillLastLWSP.SetAs(-1);
					}
				}
				else
				{
					lineBuffer.Length.SetAs(0);
					lineBuffer.LengthTillLastLWSP.SetAs(-1);
				}
				bool flag2 = false;
				if (lineBuffer.Length.InBytes > 0)
				{
					if (!MimeScan.IsFWSP(lineBuffer.Bytes[0]))
					{
						flag2 = true;
					}
				}
				else if (!flag)
				{
					flag2 = true;
				}
				if (flag2)
				{
					stream.Write(Header.LineStartWhitespace, 0, Header.LineStartWhitespace.Length);
					num += (long)Header.LineStartWhitespace.Length;
					currentLineLength.IncrementBy(Header.LineStartWhitespace.Length);
				}
			}
			if (currentLineLength.InBytes + lineBuffer.Length.InBytes + tokenLength.InBytes > 998)
			{
				if (lineBuffer.Length.InBytes > 0)
				{
					stream.Write(lineBuffer.Bytes, 0, lineBuffer.Length.InBytes);
					num += (long)lineBuffer.Length.InBytes;
					currentLineLength.IncrementBy(lineBuffer.Length);
					lineBuffer.Length.SetAs(0);
					autoAddedLastLWSP = false;
					lineBuffer.LengthTillLastLWSP.SetAs(-1);
				}
				if (token != null)
				{
					while (currentLineLength.InBytes + tokenLength.InBytes > 998)
					{
						int num2 = Math.Max(0, 998 - currentLineLength.InBytes);
						int num3 = 0;
						int num4 = 0;
						if (allowUTF8)
						{
							int num5;
							for (int i = 0; i < tokenLength.InBytes; i += num5)
							{
								byte b = token[tokenOffset + i];
								num5 = 1;
								if (b >= 128 && !MimeScan.IsUTF8NonASCII(token, tokenOffset + i, tokenOffset + tokenLength.InBytes, out num5))
								{
									num5 = 1;
								}
								if (num4 + num5 > num2)
								{
									break;
								}
								num3++;
								num4 += num5;
							}
						}
						else
						{
							num3 = num2;
							num4 = num2;
						}
						stream.Write(token, tokenOffset, num4);
						num += (long)num4;
						currentLineLength.IncrementBy(num3, num4);
						tokenOffset += num4;
						tokenLength.DecrementBy(num3, num4);
						num += Header.WriteLineEnd(stream, ref currentLineLength);
						if (!flag)
						{
							stream.Write(Header.LineStartWhitespace, 0, Header.LineStartWhitespace.Length);
							num += (long)Header.LineStartWhitespace.Length;
							currentLineLength.IncrementBy(Header.LineStartWhitespace.Length);
						}
					}
				}
			}
			if (token != null)
			{
				Buffer.BlockCopy(token, tokenOffset, lineBuffer.Bytes, lineBuffer.Length.InBytes, tokenLength.InBytes);
				if (flag && (lineBuffer.Length.InBytes == 0 || !MimeScan.IsFWSP(lineBuffer.Bytes[lineBuffer.Length.InBytes - 1])))
				{
					autoAddedLastLWSP = false;
					lineBuffer.LengthTillLastLWSP.SetAs(lineBuffer.Length);
				}
				lineBuffer.Length.IncrementBy(tokenLength);
			}
			return num;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006514 File Offset: 0x00004714
		internal static long QuoteAndFold(Stream stream, MimeStringList fragments, uint inputMask, bool quoteOutput, bool addSpaceAtStart, bool allowUTF8, int lastLineReserve, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			long num = 0L;
			Header.LineBuffer lineBuffer = default(Header.LineBuffer);
			lineBuffer.Length = new MimeStringLength(0);
			lineBuffer.LengthTillLastLWSP = new MimeStringLength(-1);
			if (scratchBuffer == null || scratchBuffer.Length < 998)
			{
				scratchBuffer = new byte[998];
			}
			lineBuffer.Bytes = scratchBuffer;
			MimeScan.Token token = quoteOutput ? (MimeScan.Token.Spec | MimeScan.Token.Fwsp) : MimeScan.Token.Fwsp;
			bool flag = false;
			if (addSpaceAtStart && currentLineLength.InBytes != 0)
			{
				num += Header.WriteToken(Header.Space, 0, new MimeStringLength(1), stream, ref currentLineLength, ref lineBuffer, ref flag, allowUTF8);
				flag = true;
			}
			if (quoteOutput)
			{
				num += Header.WriteToken(Header.DoubleQuote, 0, new MimeStringLength(1), stream, ref currentLineLength, ref lineBuffer, ref flag, allowUTF8);
			}
			for (int i = 0; i < fragments.Count; i++)
			{
				MimeString mimeString = fragments[i];
				int num2 = 0;
				int num3 = 0;
				byte[] data = mimeString.GetData(out num2, out num3);
				if ((mimeString.Mask & inputMask) != 0U)
				{
					do
					{
						int valueInChars = 0;
						int num4 = MimeScan.FindNextOf(token, data, num2, num3, out valueInChars, allowUTF8);
						if (num4 > 0)
						{
							num += Header.WriteToken(data, num2, new MimeStringLength(valueInChars, num4), stream, ref currentLineLength, ref lineBuffer, ref flag, allowUTF8);
							num2 += num4;
							num3 -= num4;
						}
						if (num3 != 0)
						{
							byte b = data[num2];
							if ((b == 34 || b == 92) && (mimeString.Mask & 3758096383U) != 0U)
							{
								num += Header.WriteToken(new byte[]
								{
									92,
									data[num2]
								}, 0, new MimeStringLength(2), stream, ref currentLineLength, ref lineBuffer, ref flag, allowUTF8);
								num2++;
								num3--;
							}
							else
							{
								num += Header.WriteToken(new byte[]
								{
									data[num2]
								}, 0, new MimeStringLength(1), stream, ref currentLineLength, ref lineBuffer, ref flag, allowUTF8);
								num2++;
								num3--;
							}
						}
					}
					while (num3 != 0);
				}
			}
			if (quoteOutput)
			{
				num += Header.WriteToken(Header.DoubleQuote, 0, new MimeStringLength(1), stream, ref currentLineLength, ref lineBuffer, ref flag, allowUTF8);
			}
			if (lastLineReserve > 0)
			{
				num += Header.WriteToken(null, 0, new MimeStringLength(lastLineReserve), stream, ref currentLineLength, ref lineBuffer, ref flag, allowUTF8);
			}
			if (lineBuffer.Length.InBytes > 0)
			{
				stream.Write(lineBuffer.Bytes, 0, lineBuffer.Length.InBytes);
				num += (long)lineBuffer.Length.InBytes;
				currentLineLength.IncrementBy(lineBuffer.Length);
			}
			return num;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006788 File Offset: 0x00004988
		internal static int WriteName(Stream stream, string name, ref byte[] scratchBuffer)
		{
			if (scratchBuffer == null || scratchBuffer.Length < name.Length)
			{
				scratchBuffer = new byte[Math.Max(998, name.Length)];
			}
			ByteString.StringToBytes(name, scratchBuffer, 0, false);
			stream.Write(scratchBuffer, 0, name.Length);
			stream.Write(MimeString.Colon, 0, MimeString.Colon.Length);
			return name.Length + MimeString.Colon.Length;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000067F8 File Offset: 0x000049F8
		internal static HeaderNameIndex LookupName(string name)
		{
			if (name.Length >= 2 && name.Length <= 31)
			{
				int num = MimeData.HashName(name);
				int num2 = (int)MimeData.nameHashTable[num];
				if (num2 > 0)
				{
					for (;;)
					{
						string text = MimeData.headerNames[num2].name;
						if (name.Length == text.Length && name.Equals(text, StringComparison.OrdinalIgnoreCase))
						{
							break;
						}
						num2++;
						if ((int)MimeData.headerNames[num2].hash != num)
						{
							return HeaderNameIndex.Unknown;
						}
					}
					return (HeaderNameIndex)num2;
				}
			}
			return HeaderNameIndex.Unknown;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006870 File Offset: 0x00004A70
		internal static HeaderNameIndex LookupName(byte[] nameBuffer, int offset, int length)
		{
			if (length >= 2 && length <= 31)
			{
				int num = MimeData.HashName(nameBuffer, offset, length);
				int num2 = (int)MimeData.nameHashTable[num];
				if (num2 > 0)
				{
					for (;;)
					{
						string str = MimeData.headerNames[num2].name;
						if (ByteString.EqualsI(str, nameBuffer, offset, length, false))
						{
							break;
						}
						num2++;
						if ((int)MimeData.headerNames[num2].hash != num)
						{
							return HeaderNameIndex.Unknown;
						}
					}
					return (HeaderNameIndex)num2;
				}
			}
			return HeaderNameIndex.Unknown;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000068D4 File Offset: 0x00004AD4
		internal static string NormalizeString(string value)
		{
			if (value.Length >= 2 && value.Length <= 32)
			{
				int num = MimeData.HashValue(value);
				int num2 = MimeData.valueHashTable[num];
				if (num2 > 0)
				{
					string value2;
					for (;;)
					{
						value2 = MimeData.values[num2].value;
						if (value.Length == value2.Length && value.Equals(value2, StringComparison.OrdinalIgnoreCase))
						{
							break;
						}
						num2++;
						if ((int)MimeData.values[num2].hash != num)
						{
							goto IL_68;
						}
					}
					return value2;
				}
			}
			IL_68:
			return value.ToLowerInvariant();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006950 File Offset: 0x00004B50
		internal static string NormalizeString(string value, int offset, int length)
		{
			if (length >= 2 && length <= 32)
			{
				int num = MimeData.HashValue(value, offset, length);
				int num2 = MimeData.valueHashTable[num];
				if (num2 > 0)
				{
					string value2;
					for (;;)
					{
						value2 = MimeData.values[num2].value;
						if (length == value2.Length && string.Compare(value2, 0, value, offset, length, StringComparison.OrdinalIgnoreCase) == 0)
						{
							break;
						}
						num2++;
						if ((int)MimeData.values[num2].hash != num)
						{
							goto IL_5E;
						}
					}
					return value2;
				}
			}
			IL_5E:
			return value.Substring(offset, length).ToLowerInvariant();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000069C8 File Offset: 0x00004BC8
		internal static string NormalizeString(byte[] value, int offset, int length, bool allowUTF8)
		{
			if (length >= 2 && length <= 32)
			{
				int num = MimeData.HashValue(value, offset, length);
				int num2 = MimeData.valueHashTable[num];
				if (num2 > 0)
				{
					string value2;
					for (;;)
					{
						value2 = MimeData.values[num2].value;
						if (ByteString.EqualsI(value2, value, offset, length, allowUTF8))
						{
							break;
						}
						num2++;
						if ((int)MimeData.values[num2].hash != num)
						{
							goto IL_54;
						}
					}
					return value2;
				}
			}
			IL_54:
			return ByteString.BytesToString(value, offset, length, allowUTF8).ToLowerInvariant();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006A38 File Offset: 0x00004C38
		internal string GetRawValue(bool allowUTF8)
		{
			byte[] rawValue = this.RawValue;
			if (rawValue == null || rawValue.Length == 0)
			{
				return string.Empty;
			}
			return ByteString.BytesToString(rawValue, allowUTF8);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006A64 File Offset: 0x00004C64
		internal void SetRawValue(string value, bool markDirty, bool allowUTF8)
		{
			if (string.IsNullOrEmpty(value))
			{
				this.SetRawValue(null, markDirty);
				return;
			}
			byte[] array = ByteString.StringToBytes(value, allowUTF8);
			int num = ByteString.IndexOf(array, 10, 0, array.Length);
			if (num == -1)
			{
				this.SetRawValue(array, markDirty);
				return;
			}
			this.RawValueAboutToChange();
			this.lines = default(MimeStringList);
			int num2 = 0;
			do
			{
				int num3 = num;
				while (num3 > num2 && array[num3 - 1] == 13)
				{
					num3--;
				}
				if (num3 > num2)
				{
					this.lines.Append(new MimeString(array, num2, num3 - num2));
				}
				num2 = num + 1;
				num = ByteString.IndexOf(array, 10, num2, array.Length - num2);
			}
			while (num != -1);
			if (num2 != array.Length)
			{
				this.lines.Append(new MimeString(array, num2, array.Length - num2));
			}
			if (markDirty)
			{
				this.SetDirty();
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006B23 File Offset: 0x00004D23
		internal void SetRawValue(byte[] value, bool markDirty)
		{
			this.RawValueAboutToChange();
			if (value == null || value.Length == 0)
			{
				this.lines = default(MimeStringList);
			}
			else
			{
				this.lines = new MimeStringList(new MimeString(value));
			}
			if (markDirty)
			{
				this.SetDirty();
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006B5B File Offset: 0x00004D5B
		internal void SetRawValue(MimeStringList newLines, bool markDirty)
		{
			this.RawValueAboutToChange();
			this.lines = newLines;
			if (markDirty)
			{
				this.SetDirty();
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006B73 File Offset: 0x00004D73
		internal virtual void RawValueAboutToChange()
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006B75 File Offset: 0x00004D75
		internal virtual void ForceParse()
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006B77 File Offset: 0x00004D77
		internal bool IsName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006B94 File Offset: 0x00004D94
		internal bool MatchName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.Name.Equals(name))
			{
				return true;
			}
			if (this.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
			{
				this.customName = name;
				return true;
			}
			return false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006BCD File Offset: 0x00004DCD
		internal override void SetDirty()
		{
			this.dirty = true;
			base.SetDirty();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006BDC File Offset: 0x00004DDC
		internal long WriteName(Stream stream, ref byte[] scratchBuffer)
		{
			if (!this.IsDirty && this.IsProtected)
			{
				string text = string.IsNullOrEmpty(this.customName) ? this.Name : this.customName;
				return (long)Header.WriteName(stream, text, ref scratchBuffer);
			}
			return (long)Header.WriteName(stream, this.Name, ref scratchBuffer);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006C30 File Offset: 0x00004E30
		internal static long WriteLineEnd(Stream stream, ref MimeStringLength currentLineLength)
		{
			long num = 0L;
			stream.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
			num += (long)MimeString.CrLf.Length;
			currentLineLength.SetAs(0);
			return num;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006C68 File Offset: 0x00004E68
		internal bool IsHeaderLineTooLong(long nameLength, out bool merge)
		{
			int num = 0;
			bool result = false;
			merge = false;
			for (int i = 0; i < this.lines.Count; i++)
			{
				int num2;
				int num3;
				byte[] data = this.lines[i].GetData(out num2, out num3);
				bool flag = MimeScan.IsLWSP(data[num2]);
				if (num != 0 && !flag)
				{
					result = true;
					merge = true;
				}
				num += num3;
				if (i == 0 && (long)num3 + nameLength + 1L > 998L)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006CE0 File Offset: 0x00004EE0
		internal override long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			long num = this.WriteName(stream, ref scratchBuffer);
			currentLineLength.IncrementBy((int)num);
			bool flag = false;
			if (!this.IsDirty && this.RawLength != 0)
			{
				if (this.IsProtected)
				{
					num += Header.WriteLines(this.lines, stream);
					currentLineLength.SetAs(0);
					return num;
				}
				if (!this.IsHeaderLineTooLong(num, out flag))
				{
					num += Header.WriteLines(this.lines, stream);
					currentLineLength.SetAs(0);
					return num;
				}
			}
			MimeStringList fragments = this.lines;
			if (flag)
			{
				fragments = Header.MergeLines(fragments);
			}
			num += Header.QuoteAndFold(stream, fragments, 4026531840U, false, fragments.Length > 0, encodingOptions.AllowUTF8, 0, ref currentLineLength, ref scratchBuffer);
			return num + Header.WriteLineEnd(stream, ref currentLineLength);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006D9F File Offset: 0x00004F9F
		internal void AppendLine(MimeString line)
		{
			this.AppendLine(line, true);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006DA9 File Offset: 0x00004FA9
		internal virtual void AppendLine(MimeString line, bool markDirty)
		{
			this.RawValueAboutToChange();
			this.lines.Append(line);
			if (markDirty)
			{
				this.SetDirty();
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006DC8 File Offset: 0x00004FC8
		internal static MimeStringList MergeLines(MimeStringList lines)
		{
			if (lines.Length != 0)
			{
				byte[] array = new byte[lines.Length];
				int num = 0;
				for (int i = 0; i < lines.Count; i++)
				{
					MimeString mimeString = lines[i];
					mimeString.CopyTo(array, num);
					num += mimeString.Length;
				}
				MimeStringList result = new MimeStringList(array);
				return result;
			}
			return lines;
		}

		// Token: 0x040000C8 RID: 200
		internal const bool AllowUTF8Name = false;

		// Token: 0x040000C9 RID: 201
		internal static readonly byte[] LineStartWhitespace = MimeString.Tabulation;

		// Token: 0x040000CA RID: 202
		internal static readonly byte[] DoubleQuote = new byte[]
		{
			34
		};

		// Token: 0x040000CB RID: 203
		internal static readonly byte[] Space = new byte[]
		{
			32
		};

		// Token: 0x040000CC RID: 204
		private string name;

		// Token: 0x040000CD RID: 205
		private string customName;

		// Token: 0x040000CE RID: 206
		private HeaderId headerId;

		// Token: 0x040000CF RID: 207
		private MimeStringList lines = default(MimeStringList);

		// Token: 0x040000D0 RID: 208
		private bool dirty;

		// Token: 0x02000020 RID: 32
		private struct LineBuffer
		{
			// Token: 0x040000D1 RID: 209
			public byte[] Bytes;

			// Token: 0x040000D2 RID: 210
			public MimeStringLength Length;

			// Token: 0x040000D3 RID: 211
			public MimeStringLength LengthTillLastLWSP;
		}
	}
}
