using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000036 RID: 54
	internal class MimeAddressParser
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00009EDE File Offset: 0x000080DE
		public MimeAddressParser()
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009EE8 File Offset: 0x000080E8
		public MimeAddressParser(MimeStringList lines, MimeAddressParser source)
		{
			this.parserInit = source.parserInit;
			this.ignoreComments = source.ignoreComments;
			this.useSquareBrackets = source.useSquareBrackets;
			this.valueParser = new ValueParser(lines, source.valueParser);
			this.groupInProgress = source.groupInProgress;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009F3D File Offset: 0x0000813D
		public bool Initialized
		{
			get
			{
				return this.parserInit;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009F48 File Offset: 0x00008148
		public static bool IsWellFormedAddress(string address, bool allowUTF8 = false)
		{
			int num;
			return MimeAddressParser.IsValidSmtpAddress(address, false, out num, allowUTF8);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009F60 File Offset: 0x00008160
		public static bool IsValidSmtpAddress(string address, bool checkLength, out int domainStart, bool allowUTF8 = false)
		{
			if (string.IsNullOrEmpty(address))
			{
				domainStart = -1;
				return false;
			}
			if (checkLength && address.Length > 571 && (address.Length > 1860 || !MimeAddressParser.IsEncapsulatedX400Address(address)))
			{
				domainStart = -1;
				return false;
			}
			int num = 0;
			MimeAddressParser.ValidationStage validationStage = MimeAddressParser.ValidationStage.BEGIN;
			while (num < address.Length && validationStage != MimeAddressParser.ValidationStage.ERROR)
			{
				char c = address[num];
				num++;
				switch (validationStage)
				{
				case MimeAddressParser.ValidationStage.BEGIN:
					if ((c < '\u0080' && MimeScan.IsAtom((byte)c)) || (c >= '\u0080' && allowUTF8))
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL;
						continue;
					}
					if (c == '\\')
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL_ESC;
						continue;
					}
					if (c == '"')
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL_DQS;
						continue;
					}
					goto IL_1B6;
				case MimeAddressParser.ValidationStage.LOCAL:
					if (c == '@')
					{
						goto IL_182;
					}
					if (c == '.')
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL_DOT;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.LOCAL_DOT:
					break;
				case MimeAddressParser.ValidationStage.LOCAL_DQS:
					if (c == '"')
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL_DQS_END;
						continue;
					}
					if (c == '\\')
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL_DQS_ESC;
						continue;
					}
					if ((c < '\u0080' && '\r' != c && '\n' != c && '\\' != c && '"' != c) || (c >= '\u0080' && allowUTF8))
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL_DQS;
						continue;
					}
					goto IL_1B6;
				case MimeAddressParser.ValidationStage.LOCAL_ESC:
					if (c < '\u0080' || (c >= '\u0080' && allowUTF8))
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL;
						continue;
					}
					goto IL_1B6;
				case MimeAddressParser.ValidationStage.LOCAL_DQS_ESC:
					if (c < '\u0080' || (c >= '\u0080' && allowUTF8))
					{
						validationStage = MimeAddressParser.ValidationStage.LOCAL_DQS;
						continue;
					}
					goto IL_1B6;
				case MimeAddressParser.ValidationStage.LOCAL_DQS_END:
					if (c == '@')
					{
						goto IL_182;
					}
					goto IL_1B6;
				case MimeAddressParser.ValidationStage.DOMAIN:
					goto IL_182;
				default:
					goto IL_1B6;
				}
				if ((c < '\u0080' && MimeScan.IsAtom((byte)c)) || (c >= '\u0080' && allowUTF8))
				{
					validationStage = MimeAddressParser.ValidationStage.LOCAL;
					continue;
				}
				if (c == '\\')
				{
					validationStage = MimeAddressParser.ValidationStage.LOCAL_ESC;
					continue;
				}
				IL_1B6:
				validationStage = MimeAddressParser.ValidationStage.ERROR;
				continue;
				IL_182:
				if (checkLength && num - 1 > 315 && (num - 1 > 1604 || !MimeAddressParser.IsEncapsulatedX400Address(address)))
				{
					domainStart = -1;
					return false;
				}
				if (MimeAddressParser.IsValidDomain(address, num, checkLength, allowUTF8))
				{
					domainStart = num;
					return true;
				}
				goto IL_1B6;
			}
			domainStart = -1;
			return false;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000A13C File Offset: 0x0000833C
		public static bool IsValidDomain(string address, int offset, bool checkLength, bool allowUTF8 = false)
		{
			if (string.IsNullOrEmpty(address) || (checkLength && address.Length - offset > 255))
			{
				return false;
			}
			MimeAddressParser.ValidationStage validationStage = MimeAddressParser.ValidationStage.DOMAIN;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			bool flag = false;
			int num4 = 0;
			while (offset < address.Length && validationStage != MimeAddressParser.ValidationStage.ERROR)
			{
				char c = address[offset];
				offset++;
				switch (validationStage)
				{
				case MimeAddressParser.ValidationStage.DOMAIN:
					if (c == '[')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL;
						continue;
					}
					if ((c < '\u0080' && MimeScan.IsAlphaOrDigit((byte)c)) || (c >= '\u0080' && allowUTF8) || c == '-' || c == '_')
					{
						num4 = offset;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_SUB;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_SUB:
					if (c == '.')
					{
						if (checkLength && offset - num4 > 63)
						{
							return false;
						}
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_DOT;
						continue;
					}
					else if ((c < '\u0080' && MimeScan.IsAlphaOrDigit((byte)c)) || (c >= '\u0080' && allowUTF8) || c == '-' || c == '_')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_SUB;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_DOT:
					if ((c < '\u0080' && MimeScan.IsAlphaOrDigit((byte)c)) || (c >= '\u0080' && allowUTF8) || c == '-' || c == '_')
					{
						num4 = offset;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_SUB;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL:
					if (c < '\u0080' && MimeScan.IsDigit((byte)c))
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV4;
						num = 1;
						num2 = 1;
						num3 = (int)(c - '0');
						continue;
					}
					if (c == 'I' || c == 'i')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_I;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV4:
					if (c < '\u0080' && MimeScan.IsDigit((byte)c) && num2 < 3)
					{
						num2++;
						num3 = num3 * 10 + (int)(c - '0');
						if (num3 <= 255)
						{
							validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV4;
							continue;
						}
					}
					else
					{
						if (c == '.')
						{
							validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV4_DOT;
							continue;
						}
						if (c == ']' && num == 4)
						{
							validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_END;
							continue;
						}
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV4_DOT:
					if (c < '\u0080' && MimeScan.IsDigit((byte)c) && num < 4)
					{
						num++;
						num2 = 1;
						num3 = (int)(c - '0');
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV4;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_I:
					if (c == 'P' || c == 'p')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IP;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IP:
					if (c == 'v' || c == 'V')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV:
					if (c == '6')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6:
					if (c == ':')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_:
					if (c < '\u0080' && MimeScan.IsHex((byte)c))
					{
						flag = false;
						num = 1;
						num2 = 1;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_GRP;
						continue;
					}
					if (c == ':')
					{
						num = 0;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_STARTCOLON;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_STARTCOLON:
					if (c == ':')
					{
						flag = true;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_COMP;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_GRP:
					if (c < '\u0080' && MimeScan.IsHex((byte)c) && num2 < 4)
					{
						num2++;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_GRP;
						continue;
					}
					if (c == ':')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_COLON;
						continue;
					}
					if (c == ']' && ((!flag && num == 8) || (flag && num <= 6)))
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_END;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_V4GRP:
					if (c < '\u0080' && MimeScan.IsDigit((byte)c) && num2 < 3)
					{
						num2++;
						num3 = num3 * 10 + (int)(c - '0');
						validationStage = ((num3 <= 255) ? MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_V4GRP : MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_GRP);
						continue;
					}
					if (c < '\u0080' && MimeScan.IsHex((byte)c) && num2 < 4)
					{
						num2++;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_GRP;
						continue;
					}
					if (c == ':')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_COLON;
						continue;
					}
					if (c == '.')
					{
						num = 1;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV4_DOT;
						continue;
					}
					if (c == ']' && flag && num <= 6)
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_END;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_COLON:
					if ((c < '\u0080' && MimeScan.IsDigit((byte)c) && !flag && num == 6) || (flag && num <= 4))
					{
						num++;
						num2 = 1;
						num3 = (int)(c - '0');
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_V4GRP;
						continue;
					}
					if ((c < '\u0080' && MimeScan.IsHex((byte)c) && !flag && num < 8) || (flag && num < 6))
					{
						num++;
						num2 = 1;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_GRP;
						continue;
					}
					if (c == ':' && !flag && num <= 6)
					{
						flag = true;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_COMP;
						continue;
					}
					break;
				case MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_COMP:
					if (c < '\u0080' && MimeScan.IsHex((byte)c) && num < 6)
					{
						num++;
						num2 = 1;
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_IPV6_GRP;
						continue;
					}
					if (c == ']')
					{
						validationStage = MimeAddressParser.ValidationStage.DOMAIN_LITERAL_END;
						continue;
					}
					break;
				}
				validationStage = MimeAddressParser.ValidationStage.ERROR;
			}
			return validationStage == MimeAddressParser.ValidationStage.DOMAIN_LITERAL_END || (validationStage == MimeAddressParser.ValidationStage.DOMAIN_SUB && (!checkLength || offset - num4 < 63));
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000A5D6 File Offset: 0x000087D6
		public void Initialize(MimeStringList lines, bool ignoreComments, bool useSquareBrackets, bool allowUTF8)
		{
			this.Reset();
			this.ignoreComments = ignoreComments;
			this.useSquareBrackets = useSquareBrackets;
			this.valueParser = new ValueParser(lines, allowUTF8);
			this.parserInit = true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000A601 File Offset: 0x00008801
		public void Reset()
		{
			this.groupInProgress = false;
			this.parserInit = false;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000A614 File Offset: 0x00008814
		public AddressParserResult ParseNextMailbox(ref MimeStringList displayName, ref MimeStringList address)
		{
			AddressParserResult result = this.groupInProgress ? AddressParserResult.GroupInProgress : AddressParserResult.Mailbox;
			MimeAddressParser.ParserStage parserStage = MimeAddressParser.ParserStage.BEGIN;
			MimeStringList mimeStringList = default(MimeStringList);
			MimeStringList mimeStringList2 = default(MimeStringList);
			MimeStringList mimeStringList3 = default(MimeStringList);
			bool flag = true;
			bool flag2 = false;
			bool ignoreNextByte = false;
			if (!this.parserInit)
			{
				throw new InvalidOperationException(Strings.AddressParserNotInitialized);
			}
			for (;;)
			{
				if (this.valueParser.ParseToDelimiter(ignoreNextByte, !flag && flag2, ref mimeStringList))
				{
					flag2 = false;
					ignoreNextByte = false;
					flag = false;
				}
				byte b = this.valueParser.ParseGet();
				byte b2 = b;
				if (b2 <= 34)
				{
					if (b2 != 0)
					{
						switch (b2)
						{
						case 9:
						case 10:
						case 13:
							break;
						case 11:
						case 12:
							goto IL_200;
						default:
							switch (b2)
							{
							case 32:
								break;
							case 33:
								goto IL_200;
							case 34:
								this.valueParser.ParseUnget();
								if (mimeStringList.Length != 0 && !flag)
								{
									this.valueParser.ParseAppendSpace(ref mimeStringList);
								}
								else
								{
									flag = false;
								}
								this.valueParser.ParseQString(true, ref mimeStringList, true);
								flag2 = true;
								continue;
							default:
								goto IL_200;
							}
							break;
						}
						this.valueParser.ParseWhitespace(false, ref mimeStringList3);
						flag2 = true;
						continue;
					}
					goto IL_213;
				}
				else if (b2 <= 46)
				{
					if (b2 == 40)
					{
						if (mimeStringList2.Length != 0)
						{
							mimeStringList2.Reset();
						}
						this.valueParser.ParseUnget();
						if (this.ignoreComments)
						{
							this.valueParser.ParseComment(true, false, ref mimeStringList2, true);
							if (flag2)
							{
								mimeStringList.AppendFragment(new MimeString(" "));
							}
							mimeStringList.TakeOverAppend(ref mimeStringList2);
						}
						else
						{
							this.valueParser.ParseComment(true, true, ref mimeStringList2, true);
						}
						flag2 = true;
						continue;
					}
					switch (b2)
					{
					case 44:
						goto IL_213;
					case 46:
						this.valueParser.ParseAppendLastByte(ref mimeStringList);
						flag = true;
						continue;
					}
				}
				else
				{
					switch (b2)
					{
					case 58:
					case 59:
					case 60:
					case 62:
					case 64:
						goto IL_213;
					case 61:
					case 63:
						break;
					default:
						switch (b2)
						{
						case 91:
						case 93:
							goto IL_213;
						}
						break;
					}
				}
				IL_200:
				this.valueParser.ParseUnget();
				ignoreNextByte = true;
				continue;
				IL_213:
				switch (parserStage)
				{
				case MimeAddressParser.ParserStage.BEGIN:
				{
					byte b3 = b;
					if (b3 > 44)
					{
						switch (b3)
						{
						case 58:
							if (mimeStringList.GetLength(4026531839U) != 0)
							{
								goto Block_26;
							}
							continue;
						case 59:
							this.groupInProgress = false;
							result = AddressParserResult.Mailbox;
							goto IL_28D;
						case 60:
							break;
						case 61:
						case 63:
							goto IL_33D;
						case 62:
							mimeStringList.Reset();
							continue;
						case 64:
						{
							int length = mimeStringList.Length;
							this.valueParser.ParseAppendLastByte(ref mimeStringList);
							address.TakeOver(ref mimeStringList);
							parserStage = MimeAddressParser.ParserStage.ADDRSPEC;
							continue;
						}
						default:
							if (b3 != 91)
							{
								goto IL_33D;
							}
							if (!this.useSquareBrackets)
							{
								this.valueParser.ParseUnget();
								ignoreNextByte = true;
								continue;
							}
							break;
						}
						displayName.TakeOver(ref mimeStringList);
						parserStage = MimeAddressParser.ParserStage.ANGLEADDR;
						continue;
					}
					if (b3 == 0)
					{
						goto IL_330;
					}
					if (b3 != 44)
					{
						goto IL_33D;
					}
					IL_28D:
					if (mimeStringList.GetLength(4026531839U) != 0)
					{
						goto Block_25;
					}
					mimeStringList.Reset();
					continue;
					IL_33D:
					this.valueParser.ParseUnget();
					ignoreNextByte = true;
					continue;
				}
				case MimeAddressParser.ParserStage.ANGLEADDR:
				{
					byte b4 = b;
					if (b4 <= 44)
					{
						if (b4 == 0)
						{
							goto IL_478;
						}
						if (b4 == 44)
						{
							if (displayName.Length != 0 || mimeStringList.Length != 0)
							{
								goto IL_478;
							}
							continue;
						}
					}
					else
					{
						switch (b4)
						{
						case 58:
							mimeStringList.Reset();
							continue;
						case 59:
						case 61:
						case 63:
							goto IL_485;
						case 60:
							break;
						case 62:
							goto IL_432;
						case 64:
							if (mimeStringList.Length == 0)
							{
								parserStage = MimeAddressParser.ParserStage.ROUTEDOMAIN;
								continue;
							}
							this.valueParser.ParseAppendLastByte(ref mimeStringList);
							address.TakeOver(ref mimeStringList);
							parserStage = MimeAddressParser.ParserStage.ADDRSPEC;
							continue;
						default:
							switch (b4)
							{
							case 91:
								if (!this.useSquareBrackets)
								{
									this.valueParser.ParseUnget();
									ignoreNextByte = true;
									continue;
								}
								break;
							case 92:
								goto IL_485;
							case 93:
								if (!this.useSquareBrackets)
								{
									this.valueParser.ParseUnget();
									ignoreNextByte = true;
									continue;
								}
								goto IL_432;
							default:
								goto IL_485;
							}
							break;
						}
						if (mimeStringList.Length != 0)
						{
							this.valueParser.ParseUnget();
							ignoreNextByte = true;
							continue;
						}
						continue;
						IL_432:
						address.TakeOver(ref mimeStringList);
						if (address.Length != 0 || displayName.Length != 0)
						{
							parserStage = MimeAddressParser.ParserStage.END;
							continue;
						}
						parserStage = MimeAddressParser.ParserStage.BEGIN;
						continue;
					}
					IL_485:
					this.valueParser.ParseUnget();
					ignoreNextByte = true;
					continue;
				}
				case MimeAddressParser.ParserStage.ADDRSPEC:
				{
					byte b5 = b;
					if (b5 > 44)
					{
						switch (b5)
						{
						case 59:
							this.groupInProgress = false;
							goto IL_58B;
						case 60:
							if (displayName.Length == 0)
							{
								displayName.TakeOverAppend(ref address);
								parserStage = MimeAddressParser.ParserStage.ANGLEADDR;
								continue;
							}
							this.valueParser.ParseUnget();
							ignoreNextByte = true;
							continue;
						case 61:
							goto IL_5B5;
						case 62:
							break;
						default:
							switch (b5)
							{
							case 91:
								if (mimeStringList.Length == 0)
								{
									this.valueParser.ParseUnget();
									this.valueParser.ParseDomainLiteral(true, ref mimeStringList);
									address.TakeOverAppend(ref mimeStringList);
									parserStage = MimeAddressParser.ParserStage.END;
									continue;
								}
								this.valueParser.ParseUnget();
								ignoreNextByte = true;
								continue;
							case 92:
								goto IL_5B5;
							case 93:
								if (!this.useSquareBrackets)
								{
									this.valueParser.ParseUnget();
									ignoreNextByte = true;
									continue;
								}
								break;
							default:
								goto IL_5B5;
							}
							break;
						}
						address.TakeOverAppend(ref mimeStringList);
						parserStage = MimeAddressParser.ParserStage.END;
						continue;
					}
					if (b5 != 0 && b5 != 44)
					{
						goto IL_5B5;
					}
					IL_58B:
					address.TakeOverAppend(ref mimeStringList);
					if (address.Length == 0 && displayName.Length == 0 && b != 0)
					{
						continue;
					}
					break;
					IL_5B5:
					this.valueParser.ParseUnget();
					ignoreNextByte = true;
					continue;
				}
				case MimeAddressParser.ParserStage.ROUTEDOMAIN:
				{
					byte b6 = b;
					if (b6 <= 44)
					{
						if (b6 == 0)
						{
							break;
						}
						if (b6 != 44)
						{
							goto IL_665;
						}
					}
					else if (b6 != 58)
					{
						if (b6 != 62)
						{
							switch (b6)
							{
							case 91:
								mimeStringList.Reset();
								this.valueParser.ParseUnget();
								this.valueParser.ParseDomainLiteral(false, ref mimeStringList3);
								continue;
							case 92:
								goto IL_665;
							case 93:
								if (!this.useSquareBrackets)
								{
									this.valueParser.ParseUnget();
									ignoreNextByte = true;
									continue;
								}
								break;
							default:
								goto IL_665;
							}
						}
						mimeStringList.Reset();
						parserStage = MimeAddressParser.ParserStage.END;
						continue;
					}
					mimeStringList.Reset();
					parserStage = MimeAddressParser.ParserStage.ANGLEADDR;
					continue;
					IL_665:
					this.valueParser.ParseUnget();
					ignoreNextByte = true;
					continue;
				}
				case MimeAddressParser.ParserStage.END:
				{
					mimeStringList.Reset();
					byte b7 = b;
					if (b7 != 0 && b7 != 44)
					{
						if (b7 != 59)
						{
							continue;
						}
						this.groupInProgress = false;
					}
					if (address.Length == 0 && displayName.Length == 0 && b != 0)
					{
						parserStage = MimeAddressParser.ParserStage.BEGIN;
						continue;
					}
					break;
				}
				}
				break;
			}
			goto IL_6B9;
			Block_25:
			displayName.TakeOver(ref mimeStringList);
			goto IL_6B9;
			Block_26:
			displayName.TakeOver(ref mimeStringList);
			this.groupInProgress = true;
			return AddressParserResult.GroupStart;
			IL_330:
			displayName.TakeOver(ref mimeStringList);
			goto IL_6B9;
			IL_478:
			address.TakeOver(ref mimeStringList);
			IL_6B9:
			if (displayName.Length == 0 && mimeStringList2.Length != 0 && address.Length != 0)
			{
				displayName.TakeOver(ref mimeStringList2);
			}
			if (address.Length == 0 && displayName.Length == 0)
			{
				result = AddressParserResult.End;
			}
			return result;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AD0E File Offset: 0x00008F0E
		private static bool IsEncapsulatedX400Address(string address)
		{
			return address.StartsWith("IMCEAX400-", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400017B RID: 379
		public const int MaxEmailName = 315;

		// Token: 0x0400017C RID: 380
		public const int MaxX400EmailName = 1604;

		// Token: 0x0400017D RID: 381
		public const int MaxDomainName = 255;

		// Token: 0x0400017E RID: 382
		public const int MaxSubDomainName = 63;

		// Token: 0x0400017F RID: 383
		public const int MaxInternetName = 571;

		// Token: 0x04000180 RID: 384
		public const int MaxX400InternetName = 1860;

		// Token: 0x04000181 RID: 385
		private bool parserInit;

		// Token: 0x04000182 RID: 386
		private bool ignoreComments;

		// Token: 0x04000183 RID: 387
		private bool useSquareBrackets;

		// Token: 0x04000184 RID: 388
		private ValueParser valueParser;

		// Token: 0x04000185 RID: 389
		private bool groupInProgress;

		// Token: 0x02000037 RID: 55
		private enum ParserStage
		{
			// Token: 0x04000187 RID: 391
			BEGIN,
			// Token: 0x04000188 RID: 392
			ANGLEADDR,
			// Token: 0x04000189 RID: 393
			ADDRSPEC,
			// Token: 0x0400018A RID: 394
			ROUTEDOMAIN,
			// Token: 0x0400018B RID: 395
			END
		}

		// Token: 0x02000038 RID: 56
		private enum ValidationStage
		{
			// Token: 0x0400018D RID: 397
			BEGIN,
			// Token: 0x0400018E RID: 398
			LOCAL,
			// Token: 0x0400018F RID: 399
			LOCAL_DOT,
			// Token: 0x04000190 RID: 400
			LOCAL_DQS,
			// Token: 0x04000191 RID: 401
			LOCAL_ESC,
			// Token: 0x04000192 RID: 402
			LOCAL_DQS_ESC,
			// Token: 0x04000193 RID: 403
			LOCAL_DQS_END,
			// Token: 0x04000194 RID: 404
			DOMAIN,
			// Token: 0x04000195 RID: 405
			DOMAIN_SUB,
			// Token: 0x04000196 RID: 406
			DOMAIN_DOT,
			// Token: 0x04000197 RID: 407
			DOMAIN_LITERAL,
			// Token: 0x04000198 RID: 408
			DOMAIN_LITERAL_IPV4,
			// Token: 0x04000199 RID: 409
			DOMAIN_LITERAL_IPV4_DOT,
			// Token: 0x0400019A RID: 410
			DOMAIN_LITERAL_I,
			// Token: 0x0400019B RID: 411
			DOMAIN_LITERAL_IP,
			// Token: 0x0400019C RID: 412
			DOMAIN_LITERAL_IPV,
			// Token: 0x0400019D RID: 413
			DOMAIN_LITERAL_IPV6,
			// Token: 0x0400019E RID: 414
			DOMAIN_LITERAL_IPV6_,
			// Token: 0x0400019F RID: 415
			DOMAIN_LITERAL_IPV6_STARTCOLON,
			// Token: 0x040001A0 RID: 416
			DOMAIN_LITERAL_IPV6_GRP,
			// Token: 0x040001A1 RID: 417
			DOMAIN_LITERAL_IPV6_V4GRP,
			// Token: 0x040001A2 RID: 418
			DOMAIN_LITERAL_IPV6_COLON,
			// Token: 0x040001A3 RID: 419
			DOMAIN_LITERAL_IPV6_COMP,
			// Token: 0x040001A4 RID: 420
			DOMAIN_LITERAL_END,
			// Token: 0x040001A5 RID: 421
			ERROR
		}
	}
}
