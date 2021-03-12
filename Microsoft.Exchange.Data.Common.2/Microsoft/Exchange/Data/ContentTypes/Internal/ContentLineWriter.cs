using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000D0 RID: 208
	internal class ContentLineWriter : IDisposable
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x0002D7FF File Offset: 0x0002B9FF
		public ContentLineWriter(Stream s, Encoding encoding)
		{
			this.foldingTextWriter = new ContentLineWriter.FoldingTextWriter(s, encoding, "\r\n ");
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0002D820 File Offset: 0x0002BA20
		public ContentLineWriteState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002D828 File Offset: 0x0002BA28
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002D837 File Offset: 0x0002BA37
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.foldingTextWriter != null)
			{
				this.Flush();
				this.foldingTextWriter.Dispose();
				this.foldingTextWriter = null;
			}
			this.state = ContentLineWriteState.Closed;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002D867 File Offset: 0x0002BA67
		public void Flush()
		{
			this.foldingTextWriter.Flush();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002D874 File Offset: 0x0002BA74
		public void WriteProperty(string property, string data)
		{
			this.AssertValidState(ContentLineWriteState.Start | ContentLineWriteState.PropertyEnd);
			this.WriteToStream(property + ":" + data + "\r\n");
			this.state = ContentLineWriteState.PropertyEnd;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002D89C File Offset: 0x0002BA9C
		public void StartProperty(string property)
		{
			this.AssertValidState(ContentLineWriteState.Start | ContentLineWriteState.PropertyEnd);
			this.WriteToStream(property);
			this.state = ContentLineWriteState.Property;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		public void EndProperty()
		{
			this.AssertValidState(ContentLineWriteState.PropertyValue);
			this.WriteToStream("\r\n");
			this.state = ContentLineWriteState.PropertyEnd;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002D8CF File Offset: 0x0002BACF
		public void StartParameter(string parameter)
		{
			this.AssertValidState(ContentLineWriteState.Property | ContentLineWriteState.ParameterEnd);
			this.WriteToStream(";");
			if (parameter != null)
			{
				this.WriteToStream(parameter);
			}
			this.emptyParamName = (parameter == null);
			this.state = ContentLineWriteState.Parameter;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002D900 File Offset: 0x0002BB00
		public void EndParameter()
		{
			this.AssertValidState(ContentLineWriteState.Parameter | ContentLineWriteState.ParameterValue);
			this.state = ContentLineWriteState.ParameterEnd;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0002D914 File Offset: 0x0002BB14
		public void WriteNextValue(ContentLineParser.Separators separator)
		{
			string data;
			if (separator == ContentLineParser.Separators.Comma)
			{
				data = ",";
			}
			else
			{
				if (separator != ContentLineParser.Separators.SemiColon)
				{
					throw new ArgumentException();
				}
				data = ";";
			}
			ContentLineWriteState contentLineWriteState = this.state;
			if (contentLineWriteState == ContentLineWriteState.PropertyValue || contentLineWriteState == ContentLineWriteState.ParameterValue)
			{
				this.WriteToStream(data);
				return;
			}
			throw new InvalidOperationException(CalendarStrings.InvalidState);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002D964 File Offset: 0x0002BB64
		public void WriteStartValue()
		{
			ContentLineWriteState contentLineWriteState = this.state;
			if (contentLineWriteState <= ContentLineWriteState.Parameter)
			{
				switch (contentLineWriteState)
				{
				case ContentLineWriteState.Property:
					break;
				case ContentLineWriteState.Start | ContentLineWriteState.Property:
				case ContentLineWriteState.PropertyValue:
					goto IL_60;
				default:
					if (contentLineWriteState != ContentLineWriteState.Parameter)
					{
						goto IL_60;
					}
					if (!this.emptyParamName)
					{
						this.WriteToStream("=");
					}
					this.state = ContentLineWriteState.ParameterValue;
					return;
				}
			}
			else if (contentLineWriteState == ContentLineWriteState.ParameterValue || contentLineWriteState != ContentLineWriteState.ParameterEnd)
			{
				goto IL_60;
			}
			this.WriteToStream(":");
			this.state = ContentLineWriteState.PropertyValue;
			return;
			IL_60:
			throw new InvalidOperationException(CalendarStrings.InvalidState);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002D9DB File Offset: 0x0002BBDB
		public void WriteChars(char[] data, int offset, int size)
		{
			this.AssertValidState(ContentLineWriteState.PropertyValue | ContentLineWriteState.ParameterValue);
			this.foldingTextWriter.Write(data, offset, size);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002D9F3 File Offset: 0x0002BBF3
		internal void WriteToStream(byte[] data)
		{
			this.AssertValidState(ContentLineWriteState.PropertyValue);
			this.foldingTextWriter.Write(data, 0, data.Length);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002DA0C File Offset: 0x0002BC0C
		internal void WriteToStream(byte[] data, int offset, int length)
		{
			this.AssertValidState(ContentLineWriteState.PropertyValue);
			this.foldingTextWriter.Write(data, offset, length);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002DA23 File Offset: 0x0002BC23
		internal void WriteToStream(byte data)
		{
			this.AssertValidState(ContentLineWriteState.PropertyValue | ContentLineWriteState.ParameterValue);
			this.foldingTextWriter.WriteByte(data);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002DA39 File Offset: 0x0002BC39
		internal void WriteToStream(string data)
		{
			this.foldingTextWriter.Write(data);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002DA47 File Offset: 0x0002BC47
		private void AssertValidState(ContentLineWriteState state)
		{
			if ((state & this.state) == (ContentLineWriteState)0)
			{
				throw new InvalidOperationException(CalendarStrings.InvalidStateForOperation);
			}
		}

		// Token: 0x04000700 RID: 1792
		private const string FoldingTagString = "\r\n ";

		// Token: 0x04000701 RID: 1793
		private ContentLineWriter.FoldingTextWriter foldingTextWriter;

		// Token: 0x04000702 RID: 1794
		private ContentLineWriteState state = ContentLineWriteState.Start;

		// Token: 0x04000703 RID: 1795
		private bool emptyParamName;

		// Token: 0x020000D1 RID: 209
		private class FoldingTextWriter : TextWriter
		{
			// Token: 0x06000840 RID: 2112 RVA: 0x0002DA60 File Offset: 0x0002BC60
			public FoldingTextWriter(Stream s, Encoding encoding, string foldingString)
			{
				this.baseStream = s;
				this.encoding = encoding;
				this.encoder = this.encoding.GetEncoder();
				this.decoder = this.encoding.GetDecoder();
				this.foldingBytes = CTSGlobals.AsciiEncoding.GetBytes(foldingString);
			}

			// Token: 0x06000841 RID: 2113 RVA: 0x0002DACD File Offset: 0x0002BCCD
			private FoldingTextWriter()
			{
			}

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x06000842 RID: 2114 RVA: 0x0002DAEE File Offset: 0x0002BCEE
			public override Encoding Encoding
			{
				get
				{
					return this.encoding;
				}
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x0002DAF6 File Offset: 0x0002BCF6
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.baseStream != null)
				{
					this.baseStream.Dispose();
					this.baseStream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x0002DB1C File Offset: 0x0002BD1C
			public override void Flush()
			{
				this.baseStream.Flush();
			}

			// Token: 0x06000845 RID: 2117 RVA: 0x0002DB2C File Offset: 0x0002BD2C
			public void Write(byte[] buffer, int offset, int count)
			{
				int charCount = this.decoder.GetCharCount(buffer, offset, count, false);
				char[] array = new char[charCount];
				this.decoder.GetChars(buffer, offset, count, array, 0);
				this.Write(array, 0, array.Length, buffer, count);
			}

			// Token: 0x06000846 RID: 2118 RVA: 0x0002DB70 File Offset: 0x0002BD70
			public override void Write(string data)
			{
				byte[] bytes = this.encoding.GetBytes(data);
				int charCount = this.decoder.GetCharCount(bytes, 0, bytes.Length, false);
				char[] array = new char[charCount];
				this.decoder.GetChars(bytes, 0, bytes.Length, array, 0);
				this.Write(array, 0, array.Length, bytes, bytes.Length);
			}

			// Token: 0x06000847 RID: 2119 RVA: 0x0002DBC5 File Offset: 0x0002BDC5
			public override void Write(char[] buffer, int offset, int count)
			{
				this.Write(buffer, offset, count, null, -1);
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
			public void Write(char[] charBuffer, int charOffset, int charCount, byte[] buffer, int byteCount)
			{
				bool flag = false;
				int num = 1;
				int num2 = 0;
				int num3 = 0;
				bool flag2 = false;
				if (buffer == null)
				{
					buffer = new byte[this.encoder.GetByteCount(charBuffer, charOffset, charCount, false)];
					this.encoder.GetBytes(charBuffer, charOffset, charCount, buffer, 0, false);
				}
				if (byteCount == -1)
				{
					byteCount = buffer.Length;
				}
				while (byteCount > 0 && num3 < charBuffer.Length)
				{
					switch (this.state)
					{
					case ContentLineWriter.FoldingTextWriter.States.Normal:
					{
						if (flag || this.linePosition == 75)
						{
							if (flag)
							{
								flag = false;
							}
							if (charBuffer[num3] == '\r')
							{
								num3++;
								this.state = ContentLineWriter.FoldingTextWriter.States.CR;
								this.baseStream.WriteByte(buffer[num2++]);
								byteCount--;
								break;
							}
							this.baseStream.Write(this.foldingBytes, 0, this.foldingBytes.Length);
							this.linePosition = 1;
						}
						int num4 = Math.Min(75 - this.linePosition, byteCount);
						int num5 = num2;
						int codePage = CodePageMap.GetCodePage(this.encoding);
						while (num2 - num5 < num4)
						{
							bool flag3 = false;
							if (num3 == charBuffer.Length)
							{
								break;
							}
							int num6 = codePage;
							if (num6 != 1200)
							{
								switch (num6)
								{
								case 65000:
									if (!flag2)
									{
										if (num3 == charBuffer.Length - 1)
										{
											flag2 = true;
										}
										num = this.WriteCharIntoBytes(charBuffer[num3], flag2);
									}
									break;
								case 65001:
									if (charBuffer[num3] < '\u0080')
									{
										num = 1;
									}
									else if (charBuffer[num3] < 'ࠀ')
									{
										num = 2;
									}
									else if (char.IsHighSurrogate(charBuffer[num3]))
									{
										if (num3 < charBuffer.Length - 1 && char.IsLowSurrogate(charBuffer[num3 + 1]))
										{
											flag3 = true;
											if (buffer[num2] < 248)
											{
												num = 4;
											}
											else if (buffer[num2] < 252)
											{
												num = 5;
											}
											else
											{
												num = 6;
											}
										}
									}
									else if (ContentLineWriter.FoldingTextWriter.IsInvalidUTF8Byte(charBuffer[num3], buffer, num2))
									{
										num = 1;
									}
									else
									{
										num = 3;
									}
									break;
								default:
									num = this.WriteCharIntoBytes(charBuffer[num3], false);
									break;
								}
							}
							else if (this.linePosition + num2 - num5 > 71 && char.IsHighSurrogate(charBuffer[num3]))
							{
								flag = true;
							}
							else
							{
								num = 2;
							}
							if (flag)
							{
								break;
							}
							if (this.linePosition + (num2 - num5) + num > 75)
							{
								flag = true;
								break;
							}
							num2 += num;
							if (flag3)
							{
								num3++;
							}
							if (charBuffer[num3++] == '\r')
							{
								this.state = ContentLineWriter.FoldingTextWriter.States.CR;
								break;
							}
						}
						int num7 = num2 - num5;
						this.baseStream.Write(buffer, num5, num7);
						this.linePosition += num7;
						byteCount -= num7;
						break;
					}
					case ContentLineWriter.FoldingTextWriter.States.CR:
						this.baseStream.WriteByte(buffer[num2]);
						if (charBuffer[num3++] == '\n')
						{
							this.linePosition = 0;
							this.state = ContentLineWriter.FoldingTextWriter.States.Normal;
						}
						else
						{
							if (this.linePosition == 75 || flag)
							{
								if (flag)
								{
									flag = false;
								}
								this.baseStream.Write(this.foldingBytes, 0, this.foldingBytes.Length);
								this.linePosition = 1;
							}
							else
							{
								this.linePosition++;
							}
							if (num3 < charBuffer.Length && charBuffer[num3] != '\r')
							{
								this.state = ContentLineWriter.FoldingTextWriter.States.Normal;
							}
						}
						num2++;
						byteCount--;
						break;
					}
				}
			}

			// Token: 0x06000849 RID: 2121 RVA: 0x0002DEDF File Offset: 0x0002C0DF
			public void WriteByte(byte byteToWrite)
			{
				this.baseStream.WriteByte(byteToWrite);
			}

			// Token: 0x0600084A RID: 2122 RVA: 0x0002DEED File Offset: 0x0002C0ED
			public override void Write(char charToWrite)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600084B RID: 2123 RVA: 0x0002DEF4 File Offset: 0x0002C0F4
			private static bool IsInvalidUTF8Byte(char inputChar, byte[] buffer, int offset)
			{
				return inputChar == '�' && (offset >= buffer.Length - 2 || buffer[offset] != 239 || buffer[offset + 1] != 191 || buffer[offset + 2] != 189);
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x0002DF2B File Offset: 0x0002C12B
			private int WriteCharIntoBytes(char ch, bool flush)
			{
				this.charCheckerArray[0] = ch;
				return this.encoder.GetBytes(this.charCheckerArray, 0, 1, this.byteBuffer, 0, flush);
			}

			// Token: 0x04000704 RID: 1796
			private const char CR = '\r';

			// Token: 0x04000705 RID: 1797
			private const char LF = '\n';

			// Token: 0x04000706 RID: 1798
			private const int MaxTextLength = 75;

			// Token: 0x04000707 RID: 1799
			private Stream baseStream;

			// Token: 0x04000708 RID: 1800
			private int linePosition;

			// Token: 0x04000709 RID: 1801
			private byte[] foldingBytes;

			// Token: 0x0400070A RID: 1802
			private byte[] byteBuffer = new byte[10];

			// Token: 0x0400070B RID: 1803
			private char[] charCheckerArray = new char[1];

			// Token: 0x0400070C RID: 1804
			private Encoding encoding;

			// Token: 0x0400070D RID: 1805
			private Encoder encoder;

			// Token: 0x0400070E RID: 1806
			private Decoder decoder;

			// Token: 0x0400070F RID: 1807
			private ContentLineWriter.FoldingTextWriter.States state;

			// Token: 0x020000D2 RID: 210
			private enum States
			{
				// Token: 0x04000711 RID: 1809
				Normal,
				// Token: 0x04000712 RID: 1810
				CR
			}
		}
	}
}
