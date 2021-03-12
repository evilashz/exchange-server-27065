using System;
using System.IO;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x02000358 RID: 856
	internal sealed class Tokenizer
	{
		// Token: 0x06002BA1 RID: 11169 RVA: 0x000A3AD0 File Offset: 0x000A1CD0
		internal void BasicInitialization()
		{
			this.LineNo = 1;
			this._inProcessingTag = 0;
			this._inSavedCharacter = -1;
			this._inIndex = 0;
			this._inSize = 0;
			this._inNestedSize = 0;
			this._inNestedIndex = 0;
			this._inTokenSource = Tokenizer.TokenSource.Other;
			this._maker = SharedStatics.GetSharedStringMaker();
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000A3B20 File Offset: 0x000A1D20
		public void Recycle()
		{
			SharedStatics.ReleaseSharedStringMaker(ref this._maker);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000A3B2D File Offset: 0x000A1D2D
		internal Tokenizer(string input)
		{
			this.BasicInitialization();
			this._inString = input;
			this._inSize = input.Length;
			this._inTokenSource = Tokenizer.TokenSource.String;
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000A3B55 File Offset: 0x000A1D55
		internal Tokenizer(string input, string[] searchStrings, string[] replaceStrings)
		{
			this.BasicInitialization();
			this._inString = input;
			this._inSize = this._inString.Length;
			this._inTokenSource = Tokenizer.TokenSource.NestedStrings;
			this._searchStrings = searchStrings;
			this._replaceStrings = replaceStrings;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000A3B90 File Offset: 0x000A1D90
		internal Tokenizer(byte[] array, Tokenizer.ByteTokenEncoding encoding, int startIndex)
		{
			this.BasicInitialization();
			this._inBytes = array;
			this._inSize = array.Length;
			this._inIndex = startIndex;
			switch (encoding)
			{
			case Tokenizer.ByteTokenEncoding.UnicodeTokens:
				this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
				return;
			case Tokenizer.ByteTokenEncoding.UTF8Tokens:
				this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
				return;
			case Tokenizer.ByteTokenEncoding.ByteTokens:
				this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
				return;
			default:
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)encoding
				}));
			}
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000A3C0A File Offset: 0x000A1E0A
		internal Tokenizer(char[] array)
		{
			this.BasicInitialization();
			this._inChars = array;
			this._inSize = array.Length;
			this._inTokenSource = Tokenizer.TokenSource.CharArray;
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000A3C2F File Offset: 0x000A1E2F
		internal Tokenizer(StreamReader input)
		{
			this.BasicInitialization();
			this._inTokenReader = new Tokenizer.StreamTokenReader(input);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000A3C4C File Offset: 0x000A1E4C
		internal void ChangeFormat(Encoding encoding)
		{
			if (encoding == null)
			{
				return;
			}
			Tokenizer.TokenSource inTokenSource = this._inTokenSource;
			if (inTokenSource > Tokenizer.TokenSource.ASCIIByteArray)
			{
				if (inTokenSource - Tokenizer.TokenSource.CharArray <= 2)
				{
					return;
				}
			}
			else
			{
				if (encoding == Encoding.Unicode)
				{
					this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
					return;
				}
				if (encoding == Encoding.UTF8)
				{
					this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
					return;
				}
				if (encoding == Encoding.ASCII)
				{
					this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
					return;
				}
			}
			inTokenSource = this._inTokenSource;
			Stream stream;
			if (inTokenSource > Tokenizer.TokenSource.ASCIIByteArray)
			{
				if (inTokenSource - Tokenizer.TokenSource.CharArray <= 2)
				{
					return;
				}
				Tokenizer.StreamTokenReader streamTokenReader = this._inTokenReader as Tokenizer.StreamTokenReader;
				if (streamTokenReader == null)
				{
					return;
				}
				stream = streamTokenReader._in.BaseStream;
				string s = new string(' ', streamTokenReader.NumCharEncountered);
				stream.Position = (long)streamTokenReader._in.CurrentEncoding.GetByteCount(s);
			}
			else
			{
				stream = new MemoryStream(this._inBytes, this._inIndex, this._inSize - this._inIndex);
			}
			this._inTokenReader = new Tokenizer.StreamTokenReader(new StreamReader(stream, encoding));
			this._inTokenSource = Tokenizer.TokenSource.Other;
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000A3D34 File Offset: 0x000A1F34
		internal void GetTokens(TokenizerStream stream, int maxNum, bool endAfterKet)
		{
			while (maxNum == -1 || stream.GetTokenCount() < maxNum)
			{
				int num = 0;
				bool flag = false;
				bool flag2 = false;
				Tokenizer.StringMaker maker = this._maker;
				maker._outStringBuilder = null;
				maker._outIndex = 0;
				int num2;
				for (;;)
				{
					if (this._inSavedCharacter != -1)
					{
						num2 = this._inSavedCharacter;
						this._inSavedCharacter = -1;
					}
					else
					{
						switch (this._inTokenSource)
						{
						case Tokenizer.TokenSource.UnicodeByteArray:
							if (this._inIndex + 1 >= this._inSize)
							{
								goto Block_3;
							}
							num2 = ((int)this._inBytes[this._inIndex + 1] << 8) + (int)this._inBytes[this._inIndex];
							this._inIndex += 2;
							break;
						case Tokenizer.TokenSource.UTF8ByteArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_4;
							}
							byte[] inBytes = this._inBytes;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inBytes[num3];
							if ((num2 & 128) != 0)
							{
								switch ((num2 & 240) >> 4)
								{
								case 8:
								case 9:
								case 10:
								case 11:
									goto IL_12D;
								case 12:
								case 13:
									num2 &= 31;
									num = 2;
									break;
								case 14:
									num2 &= 15;
									num = 3;
									break;
								case 15:
									goto IL_14B;
								}
								if (this._inIndex >= this._inSize)
								{
									goto Block_7;
								}
								byte[] inBytes2 = this._inBytes;
								num3 = this._inIndex;
								this._inIndex = num3 + 1;
								byte b = inBytes2[num3];
								if ((b & 192) != 128)
								{
									goto Block_8;
								}
								num2 = (num2 << 6 | (int)(b & 63));
								if (num != 2)
								{
									if (this._inIndex >= this._inSize)
									{
										goto Block_10;
									}
									byte[] inBytes3 = this._inBytes;
									num3 = this._inIndex;
									this._inIndex = num3 + 1;
									b = inBytes3[num3];
									if ((b & 192) != 128)
									{
										goto Block_11;
									}
									num2 = (num2 << 6 | (int)(b & 63));
								}
							}
							break;
						}
						case Tokenizer.TokenSource.ASCIIByteArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_12;
							}
							byte[] inBytes4 = this._inBytes;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inBytes4[num3];
							break;
						}
						case Tokenizer.TokenSource.CharArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_13;
							}
							char[] inChars = this._inChars;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inChars[num3];
							break;
						}
						case Tokenizer.TokenSource.String:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_14;
							}
							string inString = this._inString;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = (int)inString[num3];
							break;
						}
						case Tokenizer.TokenSource.NestedStrings:
						{
							int num3;
							if (this._inNestedSize != 0)
							{
								if (this._inNestedIndex < this._inNestedSize)
								{
									string inNestedString = this._inNestedString;
									num3 = this._inNestedIndex;
									this._inNestedIndex = num3 + 1;
									num2 = (int)inNestedString[num3];
									break;
								}
								this._inNestedSize = 0;
							}
							if (this._inIndex >= this._inSize)
							{
								goto Block_17;
							}
							string inString2 = this._inString;
							num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = (int)inString2[num3];
							if (num2 == 123)
							{
								for (int i = 0; i < this._searchStrings.Length; i++)
								{
									if (string.Compare(this._searchStrings[i], 0, this._inString, this._inIndex - 1, this._searchStrings[i].Length, StringComparison.Ordinal) == 0)
									{
										this._inNestedString = this._replaceStrings[i];
										this._inNestedSize = this._inNestedString.Length;
										this._inNestedIndex = 1;
										num2 = (int)this._inNestedString[0];
										this._inIndex += this._searchStrings[i].Length - 1;
										break;
									}
								}
							}
							break;
						}
						default:
							num2 = this._inTokenReader.Read();
							if (num2 == -1)
							{
								goto Block_21;
							}
							break;
						}
					}
					if (!flag)
					{
						if (num2 <= 34)
						{
							switch (num2)
							{
							case 9:
							case 13:
								continue;
							case 10:
								this.LineNo++;
								continue;
							case 11:
							case 12:
								break;
							default:
								switch (num2)
								{
								case 32:
									continue;
								case 33:
									if (this._inProcessingTag != 0)
									{
										goto Block_32;
									}
									break;
								case 34:
									flag = true;
									flag2 = true;
									continue;
								}
								break;
							}
						}
						else if (num2 != 45)
						{
							if (num2 != 47)
							{
								switch (num2)
								{
								case 60:
									goto IL_48A;
								case 61:
									goto IL_4C0;
								case 62:
									goto IL_4A4;
								case 63:
									if (this._inProcessingTag != 0)
									{
										goto Block_31;
									}
									break;
								}
							}
							else if (this._inProcessingTag != 0)
							{
								goto Block_30;
							}
						}
						else if (this._inProcessingTag != 0)
						{
							goto Block_33;
						}
					}
					else if (num2 <= 34)
					{
						switch (num2)
						{
						case 9:
						case 13:
							break;
						case 10:
							this.LineNo++;
							if (!flag2)
							{
								goto Block_46;
							}
							goto IL_62F;
						case 11:
						case 12:
							goto IL_62F;
						default:
							if (num2 != 32)
							{
								if (num2 != 34)
								{
									goto IL_62F;
								}
								if (flag2)
								{
									goto Block_44;
								}
								goto IL_62F;
							}
							break;
						}
						if (!flag2)
						{
							goto Block_45;
						}
					}
					else
					{
						if (num2 != 47)
						{
							if (num2 != 60)
							{
								if (num2 - 61 > 1)
								{
									goto IL_62F;
								}
							}
							else
							{
								if (!flag2)
								{
									goto Block_41;
								}
								goto IL_62F;
							}
						}
						if (!flag2 && this._inProcessingTag != 0)
						{
							goto Block_43;
						}
					}
					IL_62F:
					flag = true;
					if (maker._outIndex < 512)
					{
						char[] outChars = maker._outChars;
						Tokenizer.StringMaker stringMaker = maker;
						int num3 = stringMaker._outIndex;
						stringMaker._outIndex = num3 + 1;
						outChars[num3] = (ushort)num2;
					}
					else
					{
						if (maker._outStringBuilder == null)
						{
							maker._outStringBuilder = new StringBuilder();
						}
						maker._outStringBuilder.Append(maker._outChars, 0, 512);
						maker._outChars[0] = (char)num2;
						maker._outIndex = 1;
					}
				}
				IL_48A:
				this._inProcessingTag++;
				stream.AddToken(0);
				continue;
				Block_3:
				stream.AddToken(-1);
				return;
				IL_4A4:
				this._inProcessingTag--;
				stream.AddToken(1);
				if (endAfterKet)
				{
					return;
				}
				continue;
				IL_4C0:
				stream.AddToken(4);
				continue;
				Block_30:
				stream.AddToken(2);
				continue;
				Block_31:
				stream.AddToken(5);
				continue;
				Block_32:
				stream.AddToken(6);
				continue;
				Block_33:
				stream.AddToken(7);
				continue;
				Block_41:
				this._inSavedCharacter = num2;
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_43:
				this._inSavedCharacter = num2;
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_44:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_45:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_46:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_4:
				stream.AddToken(-1);
				return;
				IL_12D:
				throw new XmlSyntaxException(this.LineNo);
				IL_14B:
				throw new XmlSyntaxException(this.LineNo);
				Block_7:
				throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
				Block_8:
				throw new XmlSyntaxException(this.LineNo);
				Block_10:
				throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
				Block_11:
				throw new XmlSyntaxException(this.LineNo);
				Block_12:
				stream.AddToken(-1);
				return;
				Block_13:
				stream.AddToken(-1);
				return;
				Block_14:
				stream.AddToken(-1);
				return;
				Block_17:
				stream.AddToken(-1);
				return;
				Block_21:
				stream.AddToken(-1);
				return;
			}
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000A43FE File Offset: 0x000A25FE
		private string GetStringToken()
		{
			return this._maker.MakeString();
		}

		// Token: 0x0400113E RID: 4414
		internal const byte bra = 0;

		// Token: 0x0400113F RID: 4415
		internal const byte ket = 1;

		// Token: 0x04001140 RID: 4416
		internal const byte slash = 2;

		// Token: 0x04001141 RID: 4417
		internal const byte cstr = 3;

		// Token: 0x04001142 RID: 4418
		internal const byte equals = 4;

		// Token: 0x04001143 RID: 4419
		internal const byte quest = 5;

		// Token: 0x04001144 RID: 4420
		internal const byte bang = 6;

		// Token: 0x04001145 RID: 4421
		internal const byte dash = 7;

		// Token: 0x04001146 RID: 4422
		internal const int intOpenBracket = 60;

		// Token: 0x04001147 RID: 4423
		internal const int intCloseBracket = 62;

		// Token: 0x04001148 RID: 4424
		internal const int intSlash = 47;

		// Token: 0x04001149 RID: 4425
		internal const int intEquals = 61;

		// Token: 0x0400114A RID: 4426
		internal const int intQuote = 34;

		// Token: 0x0400114B RID: 4427
		internal const int intQuest = 63;

		// Token: 0x0400114C RID: 4428
		internal const int intBang = 33;

		// Token: 0x0400114D RID: 4429
		internal const int intDash = 45;

		// Token: 0x0400114E RID: 4430
		internal const int intTab = 9;

		// Token: 0x0400114F RID: 4431
		internal const int intCR = 13;

		// Token: 0x04001150 RID: 4432
		internal const int intLF = 10;

		// Token: 0x04001151 RID: 4433
		internal const int intSpace = 32;

		// Token: 0x04001152 RID: 4434
		public int LineNo;

		// Token: 0x04001153 RID: 4435
		private int _inProcessingTag;

		// Token: 0x04001154 RID: 4436
		private byte[] _inBytes;

		// Token: 0x04001155 RID: 4437
		private char[] _inChars;

		// Token: 0x04001156 RID: 4438
		private string _inString;

		// Token: 0x04001157 RID: 4439
		private int _inIndex;

		// Token: 0x04001158 RID: 4440
		private int _inSize;

		// Token: 0x04001159 RID: 4441
		private int _inSavedCharacter;

		// Token: 0x0400115A RID: 4442
		private Tokenizer.TokenSource _inTokenSource;

		// Token: 0x0400115B RID: 4443
		private Tokenizer.ITokenReader _inTokenReader;

		// Token: 0x0400115C RID: 4444
		private Tokenizer.StringMaker _maker;

		// Token: 0x0400115D RID: 4445
		private string[] _searchStrings;

		// Token: 0x0400115E RID: 4446
		private string[] _replaceStrings;

		// Token: 0x0400115F RID: 4447
		private int _inNestedIndex;

		// Token: 0x04001160 RID: 4448
		private int _inNestedSize;

		// Token: 0x04001161 RID: 4449
		private string _inNestedString;

		// Token: 0x02000B27 RID: 2855
		private enum TokenSource
		{
			// Token: 0x04003342 RID: 13122
			UnicodeByteArray,
			// Token: 0x04003343 RID: 13123
			UTF8ByteArray,
			// Token: 0x04003344 RID: 13124
			ASCIIByteArray,
			// Token: 0x04003345 RID: 13125
			CharArray,
			// Token: 0x04003346 RID: 13126
			String,
			// Token: 0x04003347 RID: 13127
			NestedStrings,
			// Token: 0x04003348 RID: 13128
			Other
		}

		// Token: 0x02000B28 RID: 2856
		internal enum ByteTokenEncoding
		{
			// Token: 0x0400334A RID: 13130
			UnicodeTokens,
			// Token: 0x0400334B RID: 13131
			UTF8Tokens,
			// Token: 0x0400334C RID: 13132
			ByteTokens
		}

		// Token: 0x02000B29 RID: 2857
		[Serializable]
		internal sealed class StringMaker
		{
			// Token: 0x06006ACA RID: 27338 RVA: 0x00170EC8 File Offset: 0x0016F0C8
			private static uint HashString(string str)
			{
				uint num = 0U;
				int length = str.Length;
				for (int i = 0; i < length; i++)
				{
					num = (num << 3 ^ (uint)str[i] ^ num >> 29);
				}
				return num;
			}

			// Token: 0x06006ACB RID: 27339 RVA: 0x00170EFC File Offset: 0x0016F0FC
			private static uint HashCharArray(char[] a, int l)
			{
				uint num = 0U;
				for (int i = 0; i < l; i++)
				{
					num = (num << 3 ^ (uint)a[i] ^ num >> 29);
				}
				return num;
			}

			// Token: 0x06006ACC RID: 27340 RVA: 0x00170F25 File Offset: 0x0016F125
			public StringMaker()
			{
				this.cStringsMax = 2048U;
				this.cStringsUsed = 0U;
				this.aStrings = new string[this.cStringsMax];
				this._outChars = new char[512];
			}

			// Token: 0x06006ACD RID: 27341 RVA: 0x00170F60 File Offset: 0x0016F160
			private bool CompareStringAndChars(string str, char[] a, int l)
			{
				if (str.Length != l)
				{
					return false;
				}
				for (int i = 0; i < l; i++)
				{
					if (a[i] != str[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06006ACE RID: 27342 RVA: 0x00170F94 File Offset: 0x0016F194
			public string MakeString()
			{
				char[] outChars = this._outChars;
				int outIndex = this._outIndex;
				if (this._outStringBuilder != null)
				{
					this._outStringBuilder.Append(this._outChars, 0, this._outIndex);
					return this._outStringBuilder.ToString();
				}
				uint num3;
				if (this.cStringsUsed > this.cStringsMax / 4U * 3U)
				{
					uint num = this.cStringsMax * 2U;
					string[] array = new string[num];
					int num2 = 0;
					while ((long)num2 < (long)((ulong)this.cStringsMax))
					{
						if (this.aStrings[num2] != null)
						{
							num3 = Tokenizer.StringMaker.HashString(this.aStrings[num2]) % num;
							while (array[(int)num3] != null)
							{
								if ((num3 += 1U) >= num)
								{
									num3 = 0U;
								}
							}
							array[(int)num3] = this.aStrings[num2];
						}
						num2++;
					}
					this.cStringsMax = num;
					this.aStrings = array;
				}
				num3 = Tokenizer.StringMaker.HashCharArray(outChars, outIndex) % this.cStringsMax;
				string text;
				while ((text = this.aStrings[(int)num3]) != null)
				{
					if (this.CompareStringAndChars(text, outChars, outIndex))
					{
						return text;
					}
					if ((num3 += 1U) >= this.cStringsMax)
					{
						num3 = 0U;
					}
				}
				text = new string(outChars, 0, outIndex);
				this.aStrings[(int)num3] = text;
				this.cStringsUsed += 1U;
				return text;
			}

			// Token: 0x0400334D RID: 13133
			private string[] aStrings;

			// Token: 0x0400334E RID: 13134
			private uint cStringsMax;

			// Token: 0x0400334F RID: 13135
			private uint cStringsUsed;

			// Token: 0x04003350 RID: 13136
			public StringBuilder _outStringBuilder;

			// Token: 0x04003351 RID: 13137
			public char[] _outChars;

			// Token: 0x04003352 RID: 13138
			public int _outIndex;

			// Token: 0x04003353 RID: 13139
			public const int outMaxSize = 512;
		}

		// Token: 0x02000B2A RID: 2858
		internal interface ITokenReader
		{
			// Token: 0x06006ACF RID: 27343
			int Read();
		}

		// Token: 0x02000B2B RID: 2859
		internal class StreamTokenReader : Tokenizer.ITokenReader
		{
			// Token: 0x06006AD0 RID: 27344 RVA: 0x001710BF File Offset: 0x0016F2BF
			internal StreamTokenReader(StreamReader input)
			{
				this._in = input;
				this._numCharRead = 0;
			}

			// Token: 0x06006AD1 RID: 27345 RVA: 0x001710D8 File Offset: 0x0016F2D8
			public virtual int Read()
			{
				int num = this._in.Read();
				if (num != -1)
				{
					this._numCharRead++;
				}
				return num;
			}

			// Token: 0x1700121F RID: 4639
			// (get) Token: 0x06006AD2 RID: 27346 RVA: 0x00171104 File Offset: 0x0016F304
			internal int NumCharEncountered
			{
				get
				{
					return this._numCharRead;
				}
			}

			// Token: 0x04003354 RID: 13140
			internal StreamReader _in;

			// Token: 0x04003355 RID: 13141
			internal int _numCharRead;
		}
	}
}
