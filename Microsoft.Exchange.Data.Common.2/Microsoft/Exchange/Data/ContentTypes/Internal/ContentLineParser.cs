using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000C6 RID: 198
	internal class ContentLineParser : IDisposable
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x0002B498 File Offset: 0x00029698
		public ContentLineParser(Stream stream, Encoding encoding, ComplianceTracker complianceTracker)
		{
			this.reader = new DirectoryReader(stream, encoding, complianceTracker);
			this.complianceTracker = complianceTracker;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002B4BC File Offset: 0x000296BC
		public ContentLineParser.States State
		{
			get
			{
				this.CheckDisposed("State::get");
				return this.state;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0002B4CF File Offset: 0x000296CF
		public Encoding CurrentCharsetEncoding
		{
			get
			{
				this.CheckDisposed("CurrentEncoding::get");
				return this.reader.CurrentCharsetEncoding;
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002B4E7 File Offset: 0x000296E7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002B4F8 File Offset: 0x000296F8
		public bool ParseElement(char[] buffer, int offset, int size, out int filled, bool parseAsText, ContentLineParser.Separators separators)
		{
			this.CheckDisposed("ParseElement");
			bool flag = false;
			bool flag2 = false;
			int num = offset;
			int num2 = offset + size;
			filled = 0;
			IL_756:
			while (!flag)
			{
				if (!this.eof && this.state != ContentLineParser.States.ValueStart && this.state != ContentLineParser.States.ValueStartComma && this.state != ContentLineParser.States.ValueStartSemiColon && this.state != ContentLineParser.States.Value && this.state != ContentLineParser.States.ValueEnd)
				{
					char c = this.GetCurrentChar();
				}
				if (this.eof)
				{
					if (this.state != ContentLineParser.States.ValueStartComma && this.state != ContentLineParser.States.ValueStartSemiColon && this.state != ContentLineParser.States.ValueStart && this.state != ContentLineParser.States.Value && this.state != ContentLineParser.States.ValueEnd && this.state != ContentLineParser.States.End && this.state != ContentLineParser.States.PropName)
					{
						this.complianceTracker.SetComplianceStatus(ComplianceStatus.StreamTruncated, CalendarStrings.UnexpectedEndOfStream);
					}
					this.state = ContentLineParser.States.End;
					flag2 = true;
					break;
				}
				if (this.state != ContentLineParser.States.ValueStart && this.state != ContentLineParser.States.ValueStartComma && this.state != ContentLineParser.States.ValueStartSemiColon && this.state != ContentLineParser.States.Value && this.state != ContentLineParser.States.ValueEnd)
				{
					this.lastCharProcessed = false;
				}
				switch (this.state)
				{
				case ContentLineParser.States.PropName:
					this.currentValueCharsetOverride = null;
					this.currentValueEncodingOverride = null;
					for (;;)
					{
						char c = this.GetCurrentChar();
						if (this.eof)
						{
							break;
						}
						if (this.isEndOfLine || c == '\r')
						{
							goto IL_189;
						}
						if (c == ':')
						{
							goto Block_23;
						}
						if (c == ';')
						{
							goto Block_24;
						}
						if ((byte)(ContentLineParser.GetToken((int)c) & ContentLineParser.Tokens.Alpha) == 0 && (byte)(ContentLineParser.GetToken((int)c) & ContentLineParser.Tokens.Digit) == 0 && c != '-')
						{
							this.complianceTracker.SetComplianceStatus(ComplianceStatus.InvalidCharacterInPropertyName, CalendarStrings.InvalidCharacterInPropertyName);
						}
						buffer[offset++] = c;
						if (offset == num2)
						{
							goto Block_28;
						}
					}
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.StreamTruncated | ComplianceStatus.PropertyTruncated, CalendarStrings.PropertyTruncated);
					continue;
					IL_189:
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.PropertyTruncated, CalendarStrings.PropertyTruncated);
					this.lastCharProcessed = false;
					this.state = ContentLineParser.States.Value;
					flag2 = true;
					flag = true;
					continue;
					Block_23:
					this.state = ContentLineParser.States.ValueStart;
					this.escaped = false;
					flag = true;
					flag2 = true;
					continue;
					Block_24:
					this.state = ContentLineParser.States.ParamName;
					flag = true;
					flag2 = true;
					continue;
					Block_28:
					flag = true;
					continue;
				case ContentLineParser.States.ParamName:
					for (;;)
					{
						char c = this.GetCurrentChar();
						if (this.eof)
						{
							goto IL_756;
						}
						if (this.isEndOfLine || c == '\r')
						{
							break;
						}
						if (c == '=')
						{
							goto Block_31;
						}
						if (this.complianceTracker.Format == FormatType.VCard && (c == ';' || c == ':'))
						{
							goto IL_2A7;
						}
						if ((byte)(ContentLineParser.GetToken((int)c) & ContentLineParser.Tokens.Alpha) == 0 && (byte)(ContentLineParser.GetToken((int)c) & ContentLineParser.Tokens.Digit) == 0 && c != '-')
						{
							this.complianceTracker.SetComplianceStatus(ComplianceStatus.InvalidCharacterInParameterName, CalendarStrings.InvalidCharacterInParameterName);
						}
						buffer[offset++] = c;
						if (offset == num2)
						{
							goto Block_37;
						}
					}
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.PropertyTruncated, CalendarStrings.PropertyTruncated);
					this.lastCharProcessed = false;
					this.state = ContentLineParser.States.Value;
					flag2 = true;
					flag = true;
					continue;
					Block_31:
					this.state = ContentLineParser.States.ParamValueStart;
					flag2 = true;
					flag = true;
					continue;
					IL_2A7:
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.ParameterNameMissing, CalendarStrings.ParameterNameMissing);
					this.state = ContentLineParser.States.UnnamedParamEnd;
					this.lastCharProcessed = false;
					flag = true;
					flag2 = true;
					continue;
					Block_37:
					flag = true;
					continue;
				case ContentLineParser.States.UnnamedParamEnd:
				{
					char c = this.GetCurrentChar();
					flag = true;
					flag2 = true;
					if (c == ':')
					{
						this.state = ContentLineParser.States.ValueStart;
						continue;
					}
					if (c == ';')
					{
						this.state = ContentLineParser.States.ParamName;
						continue;
					}
					continue;
				}
				case ContentLineParser.States.ParamValueStart:
				{
					char c = this.GetCurrentChar();
					this.lastCharProcessed = false;
					if (this.isEndOfLine || c == '\r')
					{
						this.complianceTracker.SetComplianceStatus(ComplianceStatus.PropertyTruncated, CalendarStrings.PropertyTruncated);
						this.state = ContentLineParser.States.ParamValueUnquoted;
						flag2 = true;
						flag = true;
						continue;
					}
					if (c == '"')
					{
						this.lastCharProcessed = true;
						this.state = ContentLineParser.States.ParamValueQuoted;
					}
					else
					{
						this.state = ContentLineParser.States.ParamValueUnquoted;
					}
					flag = true;
					flag2 = true;
					continue;
				}
				case ContentLineParser.States.ParamValueUnquoted:
					for (;;)
					{
						char c = this.GetCurrentChar();
						if (this.eof)
						{
							goto IL_756;
						}
						if (this.isEndOfLine || c == '\r')
						{
							break;
						}
						if (c == ':')
						{
							goto Block_44;
						}
						if (c == ';')
						{
							goto Block_45;
						}
						if ((separators & ContentLineParser.Separators.Comma) != ContentLineParser.Separators.None && c == ',')
						{
							goto Block_47;
						}
						if ((byte)(ContentLineParser.GetToken((int)c) & ContentLineParser.Tokens.SafeChar) == 0)
						{
							this.complianceTracker.SetComplianceStatus(ComplianceStatus.InvalidCharacterInParameterText, CalendarStrings.InvalidCharacterInParameterText);
						}
						buffer[offset++] = c;
						if (offset == num2)
						{
							goto Block_49;
						}
					}
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.PropertyTruncated, CalendarStrings.PropertyTruncated);
					this.lastCharProcessed = false;
					this.state = ContentLineParser.States.Value;
					flag2 = true;
					flag = true;
					continue;
					Block_44:
					this.state = ContentLineParser.States.ValueStart;
					this.escaped = false;
					flag = true;
					flag2 = true;
					continue;
					Block_45:
					this.state = ContentLineParser.States.ParamName;
					flag = true;
					flag2 = true;
					continue;
					Block_47:
					this.state = ContentLineParser.States.ParamValueStart;
					flag = true;
					flag2 = true;
					continue;
					Block_49:
					flag = true;
					continue;
				case ContentLineParser.States.ParamValueQuoted:
					for (;;)
					{
						char c = this.GetCurrentChar();
						if (this.eof)
						{
							goto IL_756;
						}
						if (this.isEndOfLine || c == '\r')
						{
							break;
						}
						if (c == '"')
						{
							goto Block_52;
						}
						if ((byte)(ContentLineParser.GetToken((int)c) & ContentLineParser.Tokens.QSafeChar) == 0)
						{
							this.complianceTracker.SetComplianceStatus(ComplianceStatus.InvalidCharacterInQuotedString, CalendarStrings.InvalidCharacterInQuotedString);
						}
						buffer[offset++] = c;
						if (offset == num2)
						{
							goto Block_54;
						}
					}
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.PropertyTruncated, CalendarStrings.PropertyTruncated);
					this.lastCharProcessed = false;
					this.state = ContentLineParser.States.ParamValueQuotedEnd;
					continue;
					Block_52:
					this.state = ContentLineParser.States.ParamValueQuotedEnd;
					continue;
					Block_54:
					flag = true;
					continue;
				case ContentLineParser.States.ParamValueQuotedEnd:
				{
					char c = this.GetCurrentChar();
					if (this.isEndOfLine || c == '\r')
					{
						this.complianceTracker.SetComplianceStatus(ComplianceStatus.PropertyTruncated, CalendarStrings.PropertyTruncated);
						this.lastCharProcessed = false;
						this.state = ContentLineParser.States.Value;
						flag2 = true;
						flag = true;
						continue;
					}
					if (c == ';')
					{
						this.state = ContentLineParser.States.ParamName;
						flag = true;
						flag2 = true;
						continue;
					}
					if (c == ':')
					{
						this.state = ContentLineParser.States.ValueStart;
						this.escaped = false;
						flag = true;
						flag2 = true;
						continue;
					}
					if ((separators & ContentLineParser.Separators.Comma) != ContentLineParser.Separators.None && c == ',')
					{
						this.state = ContentLineParser.States.ParamValueStart;
						flag = true;
						flag2 = true;
						continue;
					}
					this.complianceTracker.SetComplianceStatus(ComplianceStatus.InvalidParameterValue, CalendarStrings.InvalidParameterValue);
					this.state = ContentLineParser.States.ParamValueUnquoted;
					continue;
				}
				case ContentLineParser.States.ValueStartComma:
				case ContentLineParser.States.ValueStartSemiColon:
					this.state = ContentLineParser.States.Value;
					this.GetCurrentChar();
					flag = true;
					flag2 = true;
					continue;
				case ContentLineParser.States.ValueStart:
					this.state = ContentLineParser.States.Value;
					flag = true;
					flag2 = true;
					continue;
				case ContentLineParser.States.Value:
					if (this.currentValueCharsetOverride != null)
					{
						this.reader.SwitchCharsetEncoding(this.currentValueCharsetOverride);
						this.currentValueCharsetOverride = null;
					}
					if (this.currentValueEncodingOverride != null)
					{
						this.reader.ApplyValueDecoder(this.currentValueEncodingOverride);
						this.currentValueEncodingOverride = null;
					}
					for (;;)
					{
						if (this.emitLF)
						{
							this.emitLF = false;
							buffer[offset++] = '\n';
							if (offset == num2)
							{
								break;
							}
						}
						char c = this.GetCurrentChar();
						if (this.isEndOfLine || this.eof)
						{
							goto IL_665;
						}
						if (parseAsText && c == '\\' && !this.escaped)
						{
							this.escaped = true;
						}
						else
						{
							if ((separators & ContentLineParser.Separators.Comma) != ContentLineParser.Separators.None && c == ',' && !this.escaped)
							{
								goto Block_70;
							}
							if ((separators & ContentLineParser.Separators.SemiColon) != ContentLineParser.Separators.None && c == ';' && !this.escaped)
							{
								goto Block_73;
							}
							if ((byte)(ContentLineParser.GetToken((int)c) & ContentLineParser.Tokens.ValueChar) == 0)
							{
								this.complianceTracker.SetComplianceStatus(ComplianceStatus.InvalidCharacterInPropertyValue, CalendarStrings.InvalidCharacterInPropertyValue);
							}
							if (this.escaped)
							{
								if ('n' == c || 'N' == c)
								{
									c = '\r';
									this.emitLF = true;
								}
								this.escaped = false;
							}
							buffer[offset++] = c;
							if (offset == num2)
							{
								goto Block_77;
							}
						}
					}
					flag = true;
					continue;
					IL_665:
					this.state = ContentLineParser.States.ValueEnd;
					continue;
					Block_70:
					this.state = ContentLineParser.States.ValueStartComma;
					this.lastCharProcessed = false;
					flag = true;
					flag2 = true;
					continue;
					Block_73:
					this.state = ContentLineParser.States.ValueStartSemiColon;
					this.lastCharProcessed = false;
					flag = true;
					flag2 = true;
					continue;
					Block_77:
					flag = true;
					continue;
				case ContentLineParser.States.ValueEnd:
					this.reader.RestoreCharsetEncoding();
					this.state = ContentLineParser.States.PropName;
					flag = true;
					flag2 = true;
					continue;
				}
				flag = true;
				flag2 = true;
			}
			filled = offset - num;
			return !flag2;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002BC6B File Offset: 0x00029E6B
		public void ApplyValueOverrides(Encoding charset, ByteEncoder decoder)
		{
			this.CheckDisposed("ApplyValueDecoder");
			if (this.state == ContentLineParser.States.Value)
			{
				throw new InvalidOperationException();
			}
			this.currentValueCharsetOverride = charset;
			this.currentValueEncodingOverride = decoder;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0002BCD4 File Offset: 0x00029ED4
		public Stream GetValueReadStream()
		{
			this.CheckDisposed("GetValueReadStream");
			if (this.state != ContentLineParser.States.Value)
			{
				throw new InvalidOperationException();
			}
			Stream stream = this.reader.GetValueReadStream(delegate
			{
				if (this.state != ContentLineParser.States.Value)
				{
					throw new InvalidOperationException();
				}
				this.state = ContentLineParser.States.ValueEnd;
				char[] buffer = new char[0];
				int num;
				this.ParseElement(buffer, 0, 0, out num, true, ContentLineParser.Separators.None);
			});
			if (this.currentValueEncodingOverride != null)
			{
				stream = new EncoderStream(stream, this.currentValueEncodingOverride, EncoderStreamAccess.Read);
				this.currentValueEncodingOverride = null;
			}
			return stream;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0002BD32 File Offset: 0x00029F32
		protected virtual void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ContentLineParser", methodName);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002BD48 File Offset: 0x00029F48
		private static ContentLineParser.Tokens GetToken(int ch)
		{
			if (ch > 255)
			{
				return ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar;
			}
			return ContentLineParser.Dictionary[ch];
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0002BD5C File Offset: 0x00029F5C
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002BD74 File Offset: 0x00029F74
		private void InternalDispose(bool disposing)
		{
			if (disposing && this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002BD94 File Offset: 0x00029F94
		private char GetCurrentChar()
		{
			if (this.lastCharProcessed)
			{
				if (this.eof)
				{
					throw new InvalidOperationException();
				}
				this.eof = !this.reader.ReadChar(out this.lastChar, out this.isEndOfLine);
			}
			this.lastCharProcessed = true;
			return this.lastChar;
		}

		// Token: 0x0400068B RID: 1675
		private const char DQuote = '"';

		// Token: 0x0400068C RID: 1676
		private const char CR = '\r';

		// Token: 0x0400068D RID: 1677
		private const char LF = '\n';

		// Token: 0x0400068E RID: 1678
		private const char SemiColon = ';';

		// Token: 0x0400068F RID: 1679
		private const char Colon = ':';

		// Token: 0x04000690 RID: 1680
		private const char Comma = ',';

		// Token: 0x04000691 RID: 1681
		private const char Dash = '-';

		// Token: 0x04000692 RID: 1682
		private const char BackSlash = '\\';

		// Token: 0x04000693 RID: 1683
		internal static readonly ContentLineParser.Tokens[] Dictionary = new ContentLineParser.Tokens[]
		{
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.CTL,
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			~(ContentLineParser.Tokens.CTL | ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP | ContentLineParser.Tokens.NonASCII),
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.WSP,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Digit | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.Alpha | ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.SafeChar | ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar,
			ContentLineParser.Tokens.CTL,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII,
			ContentLineParser.Tokens.QSafeChar | ContentLineParser.Tokens.ValueChar | ContentLineParser.Tokens.NonASCII
		};

		// Token: 0x04000694 RID: 1684
		private ContentLineParser.States state;

		// Token: 0x04000695 RID: 1685
		private DirectoryReader reader;

		// Token: 0x04000696 RID: 1686
		private char lastChar;

		// Token: 0x04000697 RID: 1687
		private bool lastCharProcessed = true;

		// Token: 0x04000698 RID: 1688
		private bool eof;

		// Token: 0x04000699 RID: 1689
		private bool isEndOfLine;

		// Token: 0x0400069A RID: 1690
		private bool escaped;

		// Token: 0x0400069B RID: 1691
		private bool emitLF;

		// Token: 0x0400069C RID: 1692
		private ComplianceTracker complianceTracker;

		// Token: 0x0400069D RID: 1693
		private bool isDisposed;

		// Token: 0x0400069E RID: 1694
		private Encoding currentValueCharsetOverride;

		// Token: 0x0400069F RID: 1695
		private ByteEncoder currentValueEncodingOverride;

		// Token: 0x020000C7 RID: 199
		internal enum States
		{
			// Token: 0x040006A1 RID: 1697
			PropName,
			// Token: 0x040006A2 RID: 1698
			ParamName,
			// Token: 0x040006A3 RID: 1699
			UnnamedParamEnd,
			// Token: 0x040006A4 RID: 1700
			ParamValueStart,
			// Token: 0x040006A5 RID: 1701
			ParamValueUnquoted,
			// Token: 0x040006A6 RID: 1702
			ParamValueQuoted,
			// Token: 0x040006A7 RID: 1703
			ParamValueQuotedEnd,
			// Token: 0x040006A8 RID: 1704
			ValueStartComma,
			// Token: 0x040006A9 RID: 1705
			ValueStartSemiColon,
			// Token: 0x040006AA RID: 1706
			ValueStart,
			// Token: 0x040006AB RID: 1707
			Value,
			// Token: 0x040006AC RID: 1708
			ValueEnd,
			// Token: 0x040006AD RID: 1709
			End
		}

		// Token: 0x020000C8 RID: 200
		[Flags]
		internal enum Tokens : byte
		{
			// Token: 0x040006AF RID: 1711
			CTL = 1,
			// Token: 0x040006B0 RID: 1712
			Alpha = 2,
			// Token: 0x040006B1 RID: 1713
			Digit = 4,
			// Token: 0x040006B2 RID: 1714
			SafeChar = 8,
			// Token: 0x040006B3 RID: 1715
			QSafeChar = 16,
			// Token: 0x040006B4 RID: 1716
			ValueChar = 32,
			// Token: 0x040006B5 RID: 1717
			WSP = 64,
			// Token: 0x040006B6 RID: 1718
			NonASCII = 128
		}

		// Token: 0x020000C9 RID: 201
		[Flags]
		internal enum Separators
		{
			// Token: 0x040006B8 RID: 1720
			None = 0,
			// Token: 0x040006B9 RID: 1721
			Comma = 1,
			// Token: 0x040006BA RID: 1722
			SemiColon = 2
		}
	}
}
