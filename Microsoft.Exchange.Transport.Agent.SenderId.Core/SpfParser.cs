using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200001E RID: 30
	internal sealed class SpfParser
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000041B0 File Offset: 0x000023B0
		private SpfParser(string record, bool expEnabled)
		{
			this.s = record;
			this.index = 0;
			this.lookahead = ((this.s.Length > 0) ? this.s[this.index] : 'ÿ');
			this.expEnabled = expEnabled;
			this.parseStatus = SpfParser.ParseStatus.Success;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000420C File Offset: 0x0000240C
		public static SpfTerm ParseSpfRecord(SenderIdValidationContext context, string record)
		{
			SpfParser spfParser = new SpfParser(record, false);
			return spfParser.ParseSpfRecord(context);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004228 File Offset: 0x00002428
		public static MacroTermSpfNode ParseExpMacro(string record)
		{
			SpfParser spfParser = new SpfParser(record, true);
			return spfParser.ParseExpMacro();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004243 File Offset: 0x00002443
		private static bool IsLegalUnknownModifierArgument(char c)
		{
			return c != 'ÿ' && c != ' ';
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004257 File Offset: 0x00002457
		private static bool SaveMacroLiteral(MacroTermSpfNode p, ref StringBuilder literal)
		{
			if (literal.Length > 0)
			{
				p.Next = new MacroLiteralSpfNode(literal.ToString());
				literal = new StringBuilder();
				return true;
			}
			return false;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000427F File Offset: 0x0000247F
		private static bool IsLegalIPAddressCharacter(char c)
		{
			return char.IsDigit(c) || (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || c == '.' || c == ':';
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000042B4 File Offset: 0x000024B4
		private static SpfTerm CreateSpfChain(SenderIdValidationContext context, SpfParser.SpfNode ast)
		{
			SpfParser.SpfNode spfNode = ast;
			RedirectSpfModifier redirectSpfModifier = null;
			ExpSpfModifier expSpfModifier = null;
			SpfTerm spfTerm = new UnknownSpfMechanism(context);
			SpfTerm spfTerm2 = spfTerm;
			int num = 0;
			while (spfNode != null)
			{
				if (spfNode.RequiresDns)
				{
					num++;
				}
				if (spfNode.RequiresDns && num > 10)
				{
					spfTerm2 = spfTerm2.Append(new UnknownSpfMechanism(context));
				}
				else if (spfNode.IsMechanism)
				{
					spfTerm2 = spfTerm2.Append(spfNode.CreateMechanism(context));
				}
				else if (spfNode.IsRedirect)
				{
					if (redirectSpfModifier != null)
					{
						context.SetInvalid();
						return null;
					}
					redirectSpfModifier = spfNode.CreateRedirect(context);
				}
				else if (spfNode.IsExp)
				{
					if (expSpfModifier != null)
					{
						context.SetInvalid();
						return null;
					}
					expSpfModifier = spfNode.CreateExp(context);
				}
				spfNode = spfNode.Next;
			}
			if (redirectSpfModifier != null)
			{
				spfTerm2 = spfTerm2.Append(redirectSpfModifier);
			}
			else
			{
				spfTerm2 = spfTerm2.Append(new NoMatch(context));
			}
			context.AddExpModifier(expSpfModifier);
			return spfTerm.Next;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004390 File Offset: 0x00002590
		private SpfTerm ParseSpfRecord(SenderIdValidationContext context)
		{
			SpfParser.SpfNode spfNode = null;
			this.OptionalParseSpaces();
			if (this.lookahead != 'ÿ')
			{
				spfNode = this.ParseSpfTerm();
				SpfParser.SpfNode spfNode2 = spfNode;
				while (this.OptionalParseSpaces() && this.lookahead != 'ÿ')
				{
					spfNode2.Next = this.ParseSpfTerm();
					spfNode2 = spfNode2.Next;
				}
				this.Parse('ÿ');
			}
			if (this.parseStatus == SpfParser.ParseStatus.Success)
			{
				return SpfParser.CreateSpfChain(context, spfNode);
			}
			context.SetInvalid();
			return null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000440C File Offset: 0x0000260C
		private SpfParser.SpfNode ParseSpfTerm()
		{
			bool flag = true;
			SenderIdStatus prefix = SenderIdStatus.Pass;
			char c = this.lookahead;
			switch (c)
			{
			case '+':
				this.Advance();
				prefix = SenderIdStatus.Pass;
				goto IL_5A;
			case ',':
				break;
			case '-':
				this.Advance();
				prefix = SenderIdStatus.Fail;
				goto IL_5A;
			default:
				if (c == '?')
				{
					this.Advance();
					prefix = SenderIdStatus.Neutral;
					goto IL_5A;
				}
				if (c == '~')
				{
					this.Advance();
					prefix = SenderIdStatus.SoftFail;
					goto IL_5A;
				}
				break;
			}
			flag = false;
			IL_5A:
			string text = this.ParseName();
			string key;
			switch (key = text)
			{
			case "all":
				return this.ParseAllMechanism(prefix);
			case "include":
				return this.ParseIncludeMechanism(prefix);
			case "a":
				return this.ParseAMechanism(prefix);
			case "mx":
				return this.ParseMxMechanism(prefix);
			case "ptr":
				return this.ParsePtrMechanism(prefix);
			case "ip4":
				return this.ParseIP4Mechanism(prefix);
			case "ip6":
				return this.ParseIP6Mechanism(prefix);
			case "exists":
				return this.ParseExistsMechanism(prefix);
			case "redirect":
				if (!flag)
				{
					return this.ParseRedirectModifier();
				}
				this.SetError(SpfParser.ParseStatus.PrefixSpecifiedForNonMechanism);
				goto IL_1C5;
			case "exp":
				if (!flag)
				{
					return this.ParseExpModifier();
				}
				this.SetError(SpfParser.ParseStatus.PrefixSpecifiedForNonMechanism);
				goto IL_1C5;
			}
			if (!flag && this.lookahead == '=')
			{
				return this.ParseUnknownModifier();
			}
			this.SetError(SpfParser.ParseStatus.UnrecognizedMechanism);
			IL_1C5:
			this.SetError(SpfParser.ParseStatus.IllegalCharacter);
			return null;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000045E8 File Offset: 0x000027E8
		private MacroTermSpfNode ParseExpMacro()
		{
			MacroTermSpfNode result = this.ParseDomainSpec();
			if (!this.Parse('ÿ'))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000460C File Offset: 0x0000280C
		private string ParseName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c = this.lookahead;
				switch (c)
				{
				case '-':
				case '.':
					break;
				default:
					if (c != '_')
					{
						if (this.lookahead != 'ÿ' && char.IsLetterOrDigit(this.lookahead))
						{
							stringBuilder.Append(this.Advance());
							continue;
						}
						goto IL_5B;
					}
					break;
				}
				stringBuilder.Append(this.Advance());
			}
			IL_5B:
			return stringBuilder.ToString().ToLowerInvariant();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000467F File Offset: 0x0000287F
		private SpfParser.SpfNode ParseAllMechanism(SenderIdStatus prefix)
		{
			return new SpfParser.AllSpfMechanismNode(prefix);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004687 File Offset: 0x00002887
		private SpfParser.SpfNode ParseIncludeMechanism(SenderIdStatus prefix)
		{
			this.Parse(':');
			return new SpfParser.IncludeSpfMechanismNode(prefix, this.ParseDomainSpec());
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000046A0 File Offset: 0x000028A0
		private SpfParser.SpfNode ParseAMechanism(SenderIdStatus prefix)
		{
			MacroTermSpfNode domainSpec = this.OptionalParse(':') ? this.ParseDomainSpec() : null;
			SpfParser.DualCidrLengthSpfNode dualCidrLength = (this.lookahead == '/') ? this.ParseDualCidrLength() : SpfParser.MaxDualCidrLength;
			return new SpfParser.ASpfMechanismNode(prefix, domainSpec, dualCidrLength);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000046E4 File Offset: 0x000028E4
		private SpfParser.SpfNode ParseMxMechanism(SenderIdStatus prefix)
		{
			MacroTermSpfNode domainSpec = this.OptionalParse(':') ? this.ParseDomainSpec() : null;
			SpfParser.DualCidrLengthSpfNode dualCidrLength = (this.lookahead == '/') ? this.ParseDualCidrLength() : SpfParser.MaxDualCidrLength;
			return new SpfParser.MxSpfMechanismNode(prefix, domainSpec, dualCidrLength);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004728 File Offset: 0x00002928
		private SpfParser.SpfNode ParsePtrMechanism(SenderIdStatus prefix)
		{
			MacroTermSpfNode domainSpec = this.OptionalParse(':') ? this.ParseDomainSpec() : null;
			return new SpfParser.PtrSpfMechanismNode(prefix, domainSpec);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004750 File Offset: 0x00002950
		private SpfParser.SpfNode ParseIP4Mechanism(SenderIdStatus prefix)
		{
			this.Parse(':');
			IPAddress ip4Network = this.ParseIP4Network();
			int ip4CidrLength = (this.lookahead == '/') ? this.ParseIP4CidrLength() : 32;
			return new SpfParser.IP4SpfMechanismNode(prefix, ip4Network, ip4CidrLength);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000478C File Offset: 0x0000298C
		private SpfParser.SpfNode ParseIP6Mechanism(SenderIdStatus prefix)
		{
			this.Parse(':');
			IPAddress ip6Network = this.ParseIP6Network();
			int ip6CidrLength = (this.lookahead == '/') ? this.ParseIP6CidrLength() : 128;
			return new SpfParser.IP6SpfMechanismNode(prefix, ip6Network, ip6CidrLength);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000047C9 File Offset: 0x000029C9
		private SpfParser.SpfNode ParseExistsMechanism(SenderIdStatus prefix)
		{
			this.Parse(':');
			return new SpfParser.ExistsSpfMechanismNode(prefix, this.ParseDomainSpec());
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000047E0 File Offset: 0x000029E0
		private SpfParser.SpfNode ParseRedirectModifier()
		{
			this.Parse('=');
			return new SpfParser.RedirectSpfNode(this.ParseDomainSpec());
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000047F6 File Offset: 0x000029F6
		private SpfParser.SpfNode ParseExpModifier()
		{
			this.Parse('=');
			return new SpfParser.ExpSpfNode(this.ParseDomainSpec());
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000480C File Offset: 0x00002A0C
		private SpfParser.SpfNode ParseUnknownModifier()
		{
			this.Parse('=');
			while (SpfParser.IsLegalUnknownModifierArgument(this.lookahead))
			{
				this.Advance();
			}
			return new SpfParser.UnknownModifierSpfNode();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004834 File Offset: 0x00002A34
		private MacroTermSpfNode ParseDomainSpec()
		{
			MacroTermSpfNode macroTermSpfNode = new MacroLiteralSpfNode(string.Empty);
			MacroTermSpfNode macroTermSpfNode2 = macroTermSpfNode;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			while (!flag)
			{
				char c = this.lookahead;
				if (c == '%')
				{
					MacroTermSpfNode macroTermSpfNode3 = this.ParseMacroEscape(stringBuilder);
					if (macroTermSpfNode3 != null)
					{
						if (SpfParser.SaveMacroLiteral(macroTermSpfNode2, ref stringBuilder))
						{
							macroTermSpfNode2 = macroTermSpfNode2.Next;
						}
						macroTermSpfNode2.Next = macroTermSpfNode3;
						macroTermSpfNode2 = macroTermSpfNode2.Next;
					}
				}
				else if (this.IsLegalLiteralCharacter(this.lookahead))
				{
					stringBuilder.Append(this.Advance());
				}
				else
				{
					flag = true;
				}
			}
			SpfParser.SaveMacroLiteral(macroTermSpfNode2, ref stringBuilder);
			if (macroTermSpfNode2.Next != null)
			{
				macroTermSpfNode2 = macroTermSpfNode2.Next;
			}
			if (!macroTermSpfNode2.IsExpand && !this.expEnabled)
			{
				string literal = ((MacroLiteralSpfNode)macroTermSpfNode2).Literal;
				int num = literal.LastIndexOf('.');
				if (num == -1 || num == literal.Length - 1)
				{
					this.SetError(SpfParser.ParseStatus.InvalidDomainSpec);
					return null;
				}
			}
			return macroTermSpfNode.Next;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004919 File Offset: 0x00002B19
		private bool IsLegalLiteralCharacter(char c)
		{
			return (c >= '!' && c <= '$') || (c >= '&' && c <= '.') || (c >= '0' && c <= '~') || (this.expEnabled && (c == ' ' || c == '/'));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004954 File Offset: 0x00002B54
		private MacroTermSpfNode ParseMacroEscape(StringBuilder literal)
		{
			this.Parse('%');
			char c = char.ToLowerInvariant(this.lookahead);
			if (c <= '-')
			{
				if (c == '%')
				{
					this.Advance();
					literal.Append('%');
					goto IL_FE;
				}
				if (c == '-')
				{
					this.Advance();
					literal.Append("%20");
					goto IL_FE;
				}
			}
			else
			{
				if (c == '_')
				{
					this.Advance();
					literal.Append(' ');
					goto IL_FE;
				}
				if (c == '{')
				{
					this.Advance();
					switch (char.ToLowerInvariant(this.lookahead))
					{
					case 'c':
					case 'd':
					case 'h':
					case 'i':
					case 'l':
					case 'o':
					case 'p':
					case 'r':
					case 's':
					case 't':
					case 'v':
						return this.ParseMacroExpand();
					}
					this.SetError(SpfParser.ParseStatus.IllegalMacroCharacter);
					goto IL_FE;
				}
			}
			this.SetError(SpfParser.ParseStatus.RequiredTokenNotFound);
			IL_FE:
			return null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004A60 File Offset: 0x00002C60
		private MacroExpandSpfNode ParseMacroExpand()
		{
			char macroCharacter = this.Advance();
			int transformerDigits = char.IsDigit(this.lookahead) ? this.ParseNumber() : 0;
			bool transformerReverse = this.OptionalParse('r');
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			while (!flag)
			{
				char c = this.lookahead;
				switch (c)
				{
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
					break;
				default:
					if (c != '=' && c != '_')
					{
						flag = true;
						continue;
					}
					break;
				}
				stringBuilder.Append(this.Advance());
			}
			string text = stringBuilder.ToString();
			if (string.IsNullOrEmpty(text))
			{
				text = ".";
			}
			this.Parse('}');
			return new MacroExpandSpfNode(macroCharacter, transformerDigits, transformerReverse, text);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B14 File Offset: 0x00002D14
		private SpfParser.DualCidrLengthSpfNode ParseDualCidrLength()
		{
			int num = 32;
			int ip6CidrLength = 128;
			this.Parse('/');
			if (char.IsDigit(this.lookahead))
			{
				num = this.ParseNumber();
				this.ValidateCidrLength(num, 32);
				if (this.lookahead == '/')
				{
					this.Parse('/');
					ip6CidrLength = this.ParseIP6CidrLength();
				}
			}
			else
			{
				ip6CidrLength = this.ParseIP6CidrLength();
			}
			return new SpfParser.DualCidrLengthSpfNode(num, ip6CidrLength);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004B7C File Offset: 0x00002D7C
		private int ParseIP4CidrLength()
		{
			this.Parse('/');
			int num = this.ParseNumber();
			this.ValidateCidrLength(num, 32);
			return num;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004BA4 File Offset: 0x00002DA4
		private int ParseIP6CidrLength()
		{
			this.Parse('/');
			int num = this.ParseNumber();
			this.ValidateCidrLength(num, 128);
			return num;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004BCE File Offset: 0x00002DCE
		private void ValidateCidrLength(int cidrLength, int maxCidrLength)
		{
			if (cidrLength < 0 || cidrLength > maxCidrLength)
			{
				this.SetError(SpfParser.ParseStatus.InvalidCidrLength);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004BE0 File Offset: 0x00002DE0
		private IPAddress ParseIP4Network()
		{
			return this.ParseIPAddress(AddressFamily.InterNetwork);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004BE9 File Offset: 0x00002DE9
		private IPAddress ParseIP6Network()
		{
			return this.ParseIPAddress(AddressFamily.InterNetworkV6);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004BF4 File Offset: 0x00002DF4
		private IPAddress ParseIPAddress(AddressFamily addressFamily)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (SpfParser.IsLegalIPAddressCharacter(this.lookahead))
			{
				stringBuilder.Append(this.Advance());
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(stringBuilder.ToString(), out ipaddress))
			{
				if (ipaddress.AddressFamily == addressFamily)
				{
					return ipaddress;
				}
				this.SetError(SpfParser.ParseStatus.IncorrectAddressFamily);
			}
			else
			{
				this.SetError(SpfParser.ParseStatus.InvalidIPAddress);
			}
			return null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004C50 File Offset: 0x00002E50
		private int ParseNumber()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (char.IsDigit(this.lookahead))
			{
				stringBuilder.Append(this.Advance());
			}
			int result;
			if (int.TryParse(stringBuilder.ToString(), out result))
			{
				return result;
			}
			this.SetError(SpfParser.ParseStatus.RequiredTokenNotFound);
			return -1;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004C99 File Offset: 0x00002E99
		private bool Parse(char c)
		{
			if (!this.OptionalParse(c))
			{
				this.SetError(SpfParser.ParseStatus.RequiredTokenNotFound);
				return false;
			}
			return true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004CAF File Offset: 0x00002EAF
		private bool OptionalParse(char c)
		{
			if (this.lookahead == c)
			{
				this.Advance();
				return true;
			}
			return false;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private bool OptionalParseSpaces()
		{
			if (this.lookahead == ' ')
			{
				while (this.lookahead == ' ')
				{
					this.Advance();
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004CE8 File Offset: 0x00002EE8
		private char Advance()
		{
			char result;
			if (this.parseStatus == SpfParser.ParseStatus.Success && this.index < this.s.Length)
			{
				result = this.lookahead;
				this.index++;
				this.lookahead = ((this.index < this.s.Length) ? this.s[this.index] : 'ÿ');
			}
			else
			{
				result = 'ÿ';
				this.lookahead = 'ÿ';
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004D6B File Offset: 0x00002F6B
		private void SetError(SpfParser.ParseStatus error)
		{
			if (this.parseStatus == SpfParser.ParseStatus.Success)
			{
				this.parseStatus = error;
				this.lookahead = 'ÿ';
			}
		}

		// Token: 0x0400004E RID: 78
		private const char EOF = 'ÿ';

		// Token: 0x0400004F RID: 79
		private const int IP4MaxCidrLength = 32;

		// Token: 0x04000050 RID: 80
		private const int IP6MaxCidrLength = 128;

		// Token: 0x04000051 RID: 81
		private const int MaxDnsBasedMechanisms = 10;

		// Token: 0x04000052 RID: 82
		private static readonly SpfParser.DualCidrLengthSpfNode MaxDualCidrLength = new SpfParser.DualCidrLengthSpfNode(32, 128);

		// Token: 0x04000053 RID: 83
		private string s;

		// Token: 0x04000054 RID: 84
		private int index;

		// Token: 0x04000055 RID: 85
		private char lookahead;

		// Token: 0x04000056 RID: 86
		private bool expEnabled;

		// Token: 0x04000057 RID: 87
		private SpfParser.ParseStatus parseStatus;

		// Token: 0x0200001F RID: 31
		private enum ParseStatus
		{
			// Token: 0x04000059 RID: 89
			Success = 1,
			// Token: 0x0400005A RID: 90
			UnconsumedCharacters,
			// Token: 0x0400005B RID: 91
			PrefixSpecifiedForNonMechanism,
			// Token: 0x0400005C RID: 92
			IllegalCharacter,
			// Token: 0x0400005D RID: 93
			IllegalMacroCharacter,
			// Token: 0x0400005E RID: 94
			IllegalLiteralCharacter,
			// Token: 0x0400005F RID: 95
			IllegalCidrPrefix,
			// Token: 0x04000060 RID: 96
			IncorrectAddressFamily,
			// Token: 0x04000061 RID: 97
			InvalidIPAddress,
			// Token: 0x04000062 RID: 98
			InvalidCidrLength,
			// Token: 0x04000063 RID: 99
			InvalidDomainSpec,
			// Token: 0x04000064 RID: 100
			RequiredTokenNotFound,
			// Token: 0x04000065 RID: 101
			UnrecognizedMechanism,
			// Token: 0x04000066 RID: 102
			RequiredDomainSpecMissing
		}

		// Token: 0x02000020 RID: 32
		private abstract class SpfNode
		{
			// Token: 0x060000B2 RID: 178 RVA: 0x00004D9B File Offset: 0x00002F9B
			public SpfNode(bool isMechanism, bool isRedirect, bool isExp, bool requiresDns)
			{
				this.IsMechanism = isMechanism;
				this.IsRedirect = isRedirect;
				this.IsExp = isExp;
				this.RequiresDns = requiresDns;
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x00004DC0 File Offset: 0x00002FC0
			public virtual SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return null;
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x00004DC3 File Offset: 0x00002FC3
			public virtual RedirectSpfModifier CreateRedirect(SenderIdValidationContext context)
			{
				return null;
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00004DC6 File Offset: 0x00002FC6
			public virtual ExpSpfModifier CreateExp(SenderIdValidationContext context)
			{
				return null;
			}

			// Token: 0x04000067 RID: 103
			public SpfParser.SpfNode Next;

			// Token: 0x04000068 RID: 104
			public bool IsMechanism;

			// Token: 0x04000069 RID: 105
			public bool IsRedirect;

			// Token: 0x0400006A RID: 106
			public bool IsExp;

			// Token: 0x0400006B RID: 107
			public bool RequiresDns;
		}

		// Token: 0x02000021 RID: 33
		private abstract class SpfMechanismNode : SpfParser.SpfNode
		{
			// Token: 0x060000B6 RID: 182 RVA: 0x00004DC9 File Offset: 0x00002FC9
			public SpfMechanismNode(SenderIdStatus prefix, bool requiresDns) : base(true, false, false, requiresDns)
			{
				this.prefix = prefix;
			}

			// Token: 0x0400006C RID: 108
			protected SenderIdStatus prefix;
		}

		// Token: 0x02000022 RID: 34
		private class AllSpfMechanismNode : SpfParser.SpfMechanismNode
		{
			// Token: 0x060000B7 RID: 183 RVA: 0x00004DDC File Offset: 0x00002FDC
			public AllSpfMechanismNode(SenderIdStatus prefix) : base(prefix, false)
			{
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x00004DE6 File Offset: 0x00002FE6
			public override SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return new AllSpfMechanism(context, this.prefix);
			}
		}

		// Token: 0x02000023 RID: 35
		private class IncludeSpfMechanismNode : SpfParser.SpfMechanismNode
		{
			// Token: 0x060000B9 RID: 185 RVA: 0x00004DF4 File Offset: 0x00002FF4
			public IncludeSpfMechanismNode(SenderIdStatus prefix, MacroTermSpfNode domainSpec) : base(prefix, true)
			{
				this.DomainSpec = domainSpec;
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00004E05 File Offset: 0x00003005
			public override SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return new IncludeSpfMechanism(context, this.prefix, this.DomainSpec);
			}

			// Token: 0x0400006D RID: 109
			public MacroTermSpfNode DomainSpec;
		}

		// Token: 0x02000024 RID: 36
		private class ASpfMechanismNode : SpfParser.SpfMechanismNode
		{
			// Token: 0x060000BB RID: 187 RVA: 0x00004E19 File Offset: 0x00003019
			public ASpfMechanismNode(SenderIdStatus prefix, MacroTermSpfNode domainSpec, SpfParser.DualCidrLengthSpfNode dualCidrLength) : base(prefix, true)
			{
				this.DomainSpec = domainSpec;
				this.DualCidrLength = dualCidrLength;
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00004E31 File Offset: 0x00003031
			public override SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return new ASpfMechanism(context, this.prefix, this.DomainSpec, this.DualCidrLength.Ip4CidrLength, this.DualCidrLength.Ip6CidrLength);
			}

			// Token: 0x0400006E RID: 110
			public MacroTermSpfNode DomainSpec;

			// Token: 0x0400006F RID: 111
			public SpfParser.DualCidrLengthSpfNode DualCidrLength;
		}

		// Token: 0x02000025 RID: 37
		private class MxSpfMechanismNode : SpfParser.SpfMechanismNode
		{
			// Token: 0x060000BD RID: 189 RVA: 0x00004E5B File Offset: 0x0000305B
			public MxSpfMechanismNode(SenderIdStatus prefix, MacroTermSpfNode domainSpec, SpfParser.DualCidrLengthSpfNode dualCidrLength) : base(prefix, true)
			{
				this.DomainSpec = domainSpec;
				this.DualCidrLength = dualCidrLength;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00004E73 File Offset: 0x00003073
			public override SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return new MxSpfMechanism(context, this.prefix, this.DomainSpec, this.DualCidrLength.Ip4CidrLength, this.DualCidrLength.Ip6CidrLength);
			}

			// Token: 0x04000070 RID: 112
			public MacroTermSpfNode DomainSpec;

			// Token: 0x04000071 RID: 113
			public SpfParser.DualCidrLengthSpfNode DualCidrLength;
		}

		// Token: 0x02000026 RID: 38
		private class PtrSpfMechanismNode : SpfParser.SpfMechanismNode
		{
			// Token: 0x060000BF RID: 191 RVA: 0x00004E9D File Offset: 0x0000309D
			public PtrSpfMechanismNode(SenderIdStatus prefix, MacroTermSpfNode domainSpec) : base(prefix, true)
			{
				this.DomainSpec = domainSpec;
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00004EAE File Offset: 0x000030AE
			public override SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return new PtrSpfMechanism(context, this.prefix, this.DomainSpec);
			}

			// Token: 0x04000072 RID: 114
			public MacroTermSpfNode DomainSpec;
		}

		// Token: 0x02000027 RID: 39
		private class IP4SpfMechanismNode : SpfParser.SpfMechanismNode
		{
			// Token: 0x060000C1 RID: 193 RVA: 0x00004EC2 File Offset: 0x000030C2
			public IP4SpfMechanismNode(SenderIdStatus prefix, IPAddress ip4Network, int ip4CidrLength) : base(prefix, false)
			{
				this.Ip4Network = ip4Network;
				this.Ip4CidrLength = ip4CidrLength;
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00004EDA File Offset: 0x000030DA
			public override SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return new IPSpfMechanism(context, this.prefix, IPNetwork.Create(this.Ip4Network, this.Ip4CidrLength));
			}

			// Token: 0x04000073 RID: 115
			public IPAddress Ip4Network;

			// Token: 0x04000074 RID: 116
			public int Ip4CidrLength;
		}

		// Token: 0x02000028 RID: 40
		private class IP6SpfMechanismNode : SpfParser.SpfMechanismNode
		{
			// Token: 0x060000C3 RID: 195 RVA: 0x00004EF9 File Offset: 0x000030F9
			public IP6SpfMechanismNode(SenderIdStatus prefix, IPAddress ip6Network, int ip6CidrLength) : base(prefix, false)
			{
				this.Ip6Network = ip6Network;
				this.Ip6CidrLength = ip6CidrLength;
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00004F11 File Offset: 0x00003111
			public override SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return new IPSpfMechanism(context, this.prefix, IPNetwork.Create(this.Ip6Network, this.Ip6CidrLength));
			}

			// Token: 0x04000075 RID: 117
			public IPAddress Ip6Network;

			// Token: 0x04000076 RID: 118
			public int Ip6CidrLength;
		}

		// Token: 0x02000029 RID: 41
		private class ExistsSpfMechanismNode : SpfParser.SpfMechanismNode
		{
			// Token: 0x060000C5 RID: 197 RVA: 0x00004F30 File Offset: 0x00003130
			public ExistsSpfMechanismNode(SenderIdStatus prefix, MacroTermSpfNode domainSpec) : base(prefix, true)
			{
				this.DomainSpec = domainSpec;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00004F41 File Offset: 0x00003141
			public override SpfMechanism CreateMechanism(SenderIdValidationContext context)
			{
				return new ExistsSpfMechanism(context, this.prefix, this.DomainSpec);
			}

			// Token: 0x04000077 RID: 119
			public MacroTermSpfNode DomainSpec;
		}

		// Token: 0x0200002A RID: 42
		private class RedirectSpfNode : SpfParser.SpfNode
		{
			// Token: 0x060000C7 RID: 199 RVA: 0x00004F55 File Offset: 0x00003155
			public RedirectSpfNode(MacroTermSpfNode domainSpec) : base(false, true, false, true)
			{
				this.DomainSpec = domainSpec;
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00004F68 File Offset: 0x00003168
			public override RedirectSpfModifier CreateRedirect(SenderIdValidationContext context)
			{
				return new RedirectSpfModifier(context, this.DomainSpec);
			}

			// Token: 0x04000078 RID: 120
			public MacroTermSpfNode DomainSpec;
		}

		// Token: 0x0200002B RID: 43
		private class ExpSpfNode : SpfParser.SpfNode
		{
			// Token: 0x060000C9 RID: 201 RVA: 0x00004F76 File Offset: 0x00003176
			public ExpSpfNode(MacroTermSpfNode domainSpec) : base(false, false, true, false)
			{
				this.DomainSpec = domainSpec;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00004F89 File Offset: 0x00003189
			public override ExpSpfModifier CreateExp(SenderIdValidationContext context)
			{
				return new ExpSpfModifier(context, this.DomainSpec);
			}

			// Token: 0x04000079 RID: 121
			public MacroTermSpfNode DomainSpec;
		}

		// Token: 0x0200002C RID: 44
		private class UnknownModifierSpfNode : SpfParser.SpfNode
		{
			// Token: 0x060000CB RID: 203 RVA: 0x00004F97 File Offset: 0x00003197
			public UnknownModifierSpfNode() : base(false, false, false, false)
			{
			}
		}

		// Token: 0x0200002D RID: 45
		private class DualCidrLengthSpfNode
		{
			// Token: 0x060000CC RID: 204 RVA: 0x00004FA3 File Offset: 0x000031A3
			public DualCidrLengthSpfNode(int ip4CidrLength, int ip6CidrLength)
			{
				this.Ip4CidrLength = ip4CidrLength;
				this.Ip6CidrLength = ip6CidrLength;
			}

			// Token: 0x0400007A RID: 122
			public int Ip4CidrLength;

			// Token: 0x0400007B RID: 123
			public int Ip6CidrLength;
		}
	}
}
