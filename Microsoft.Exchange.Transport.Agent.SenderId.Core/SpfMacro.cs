using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200000E RID: 14
	internal sealed class SpfMacro
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002DC1 File Offset: 0x00000FC1
		private SpfMacro(SenderIdValidationContext context, MacroTermSpfNode domainSpec, bool exp, AsyncCallback asyncCallback, object asyncState)
		{
			this.context = context;
			this.currentMacroTerm = domainSpec;
			this.expanded = new StringBuilder();
			this.exp = exp;
			this.asyncResult = new SenderIdAsyncResult(asyncCallback, asyncState);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public static IAsyncResult BeginExpandDomainSpec(SenderIdValidationContext context, MacroTermSpfNode domainSpec, AsyncCallback asyncCallback, object asyncState)
		{
			SpfMacro spfMacro = new SpfMacro(context, domainSpec, false, asyncCallback, asyncState);
			return spfMacro.BeginExpandMacros();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E18 File Offset: 0x00001018
		public static SpfMacro.ExpandedMacro EndExpandDomainSpec(IAsyncResult ar)
		{
			SpfMacro.ExpandedMacro expandedMacro = SpfMacro.EndExpandMacros(ar);
			if (expandedMacro.IsValid && expandedMacro.Value.Length > 253)
			{
				int startIndex = expandedMacro.Value.Length - 253;
				int num = expandedMacro.Value.IndexOf(".", startIndex, StringComparison.OrdinalIgnoreCase);
				if (num >= 0)
				{
					expandedMacro = new SpfMacro.ExpandedMacro(true, expandedMacro.Value.Substring(num));
				}
				else
				{
					expandedMacro = new SpfMacro.ExpandedMacro(false, string.Empty);
				}
			}
			return expandedMacro;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E94 File Offset: 0x00001094
		public static IAsyncResult BeginExpandExp(SenderIdValidationContext context, string macro, AsyncCallback asyncCallback, object asyncState)
		{
			SpfMacro spfMacro = new SpfMacro(context, SpfParser.ParseExpMacro(macro), true, asyncCallback, asyncState);
			return spfMacro.BeginExpandMacros();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002EB7 File Offset: 0x000010B7
		public static SpfMacro.ExpandedMacro EndExpandExp(IAsyncResult ar)
		{
			return SpfMacro.EndExpandMacros(ar);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002EBF File Offset: 0x000010BF
		private static SpfMacro.ExpandedMacro EndExpandMacros(IAsyncResult ar)
		{
			return (SpfMacro.ExpandedMacro)((SenderIdAsyncResult)ar).GetResult();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002ED4 File Offset: 0x000010D4
		private static string DotFormattedIPv6Address(IPAddress address)
		{
			byte[] addressBytes = address.GetAddressBytes();
			StringBuilder stringBuilder = new StringBuilder(addressBytes.Length * 2);
			for (int i = 0; i < addressBytes.Length - 1; i++)
			{
				stringBuilder.AppendFormat("{0:x}.", (int)addressBytes[i]);
			}
			stringBuilder.AppendFormat("{0:x}", (int)addressBytes[addressBytes.Length - 1]);
			return stringBuilder.ToString();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F38 File Offset: 0x00001138
		private static string URLEscape(string s)
		{
			StringBuilder stringBuilder = new StringBuilder(s.Length * 2);
			for (int i = 0; i < s.Length; i++)
			{
				if (SpfMacro.UnreservedURLCharacters[(int)s[i]])
				{
					stringBuilder.Append(s[i]);
				}
				else
				{
					stringBuilder.AppendFormat("%{0:x}", (int)s[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FA4 File Offset: 0x000011A4
		private static bool[] GetUnreservedURLCharacters()
		{
			bool[] array = new bool[255];
			for (char c = 'a'; c <= 'z'; c += '\u0001')
			{
				array[(int)c] = true;
				array[(int)char.ToUpperInvariant(c)] = true;
			}
			for (char c2 = '0'; c2 <= '9'; c2 += '\u0001')
			{
				array[(int)c2] = true;
			}
			array[45] = true;
			array[95] = true;
			array[46] = true;
			array[33] = true;
			array[126] = true;
			array[42] = true;
			array[39] = true;
			array[40] = true;
			array[41] = true;
			return array;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003019 File Offset: 0x00001219
		private IAsyncResult BeginExpandMacros()
		{
			if (this.currentMacroTerm == null)
			{
				this.asyncResult.InvokeCompleted(new SpfMacro.ExpandedMacro(false, null));
			}
			else
			{
				this.ExpandRemainingMacros();
			}
			return this.asyncResult;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003044 File Offset: 0x00001244
		private void ExpandRemainingMacros()
		{
			while (this.currentMacroTerm != null)
			{
				if (this.currentMacroTerm.IsLiteral)
				{
					MacroLiteralSpfNode macroLiteralSpfNode = (MacroLiteralSpfNode)this.currentMacroTerm;
					this.expanded.Append(macroLiteralSpfNode.Literal);
					this.currentMacroTerm = this.currentMacroTerm.Next;
				}
				else
				{
					if (this.currentMacroTerm.IsExpand)
					{
						MacroExpandSpfNode expandTerm = (MacroExpandSpfNode)this.currentMacroTerm;
						this.BeginExpandMacro(expandTerm, new AsyncCallback(this.ExpandMacroCallback), null);
						return;
					}
					this.asyncResult.InvokeCompleted(new SpfMacro.ExpandedMacro(false, null));
					return;
				}
			}
			this.asyncResult.InvokeCompleted(new SpfMacro.ExpandedMacro(true, this.expanded.ToString()));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003100 File Offset: 0x00001300
		private void ExpandMacroCallback(IAsyncResult ar)
		{
			string value = this.EndExpandMacro(ar);
			this.expanded.Append(value);
			this.currentMacroTerm = this.currentMacroTerm.Next;
			this.ExpandRemainingMacros();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000313C File Offset: 0x0000133C
		private IAsyncResult BeginExpandMacro(MacroExpandSpfNode expandTerm, AsyncCallback asyncCallback, object asyncState)
		{
			SenderIdAsyncResult result = new SenderIdAsyncResult(asyncCallback, asyncState);
			SpfMacro.ExpandMacroAsyncState asyncState2 = new SpfMacro.ExpandMacroAsyncState(expandTerm, result);
			this.BeginExpandMacroCharacter(expandTerm.MacroCharacter, new AsyncCallback(this.ExpandMacroCharacterCallback), asyncState2);
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003174 File Offset: 0x00001374
		private string EndExpandMacro(IAsyncResult ar)
		{
			return (string)((SenderIdAsyncResult)ar).GetResult();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003188 File Offset: 0x00001388
		private void ExpandMacroCharacterCallback(IAsyncResult ar)
		{
			SpfMacro.ExpandMacroAsyncState expandMacroAsyncState = (SpfMacro.ExpandMacroAsyncState)ar.AsyncState;
			MacroExpandSpfNode macro = expandMacroAsyncState.Macro;
			string text = this.EndExpandMacroCharacter(ar);
			if (macro.TransformerDigits != 0 || macro.TransformerReverse || macro.Delimiters.Length > 0)
			{
				string[] array = text.Split(macro.Delimiters.ToCharArray());
				if (macro.TransformerReverse)
				{
					Array.Reverse(array);
				}
				int num = macro.TransformerDigits;
				if (num == 0 || num > array.Length)
				{
					num = array.Length;
				}
				text = string.Join(".", array, array.Length - num, num);
			}
			if (char.IsUpper(macro.MacroCharacter))
			{
				text = SpfMacro.URLEscape(text);
			}
			expandMacroAsyncState.AsyncResult.InvokeCompleted(text);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000323C File Offset: 0x0000143C
		private IAsyncResult BeginExpandMacroCharacter(char macroCharacter, AsyncCallback asyncCallback, object asyncState)
		{
			SenderIdAsyncResult senderIdAsyncResult = new SenderIdAsyncResult(asyncCallback, asyncState);
			string text = null;
			macroCharacter = char.ToLowerInvariant(macroCharacter);
			char c = macroCharacter;
			if (c <= 'p')
			{
				if (c == 'd')
				{
					text = this.context.PurportedResponsibleDomain;
					goto IL_2C1;
				}
				switch (c)
				{
				case 'h':
					text = (this.context.BaseContext.HelloDomain ?? string.Empty);
					this.context.BaseContext.SetUncacheable();
					goto IL_2C1;
				case 'i':
					if (this.context.BaseContext.IPAddress.AddressFamily == AddressFamily.InterNetwork)
					{
						text = this.context.BaseContext.IPAddress.ToString();
						goto IL_2C1;
					}
					if (this.context.BaseContext.IPAddress.AddressFamily == AddressFamily.InterNetworkV6)
					{
						text = SpfMacro.DotFormattedIPv6Address(this.context.BaseContext.IPAddress);
						goto IL_2C1;
					}
					throw new InvalidOperationException("IP address has invalid address family");
				case 'l':
					text = this.context.BaseContext.PurportedResponsibleAddress.LocalPart;
					this.context.BaseContext.SetUncacheable();
					goto IL_2C1;
				case 'o':
					text = this.context.BaseContext.PurportedResponsibleAddress.DomainPart;
					this.context.BaseContext.SetUncacheable();
					goto IL_2C1;
				case 'p':
				{
					if (this.context.BaseContext.ExpandedPMacro != null)
					{
						text = this.context.BaseContext.ExpandedPMacro;
						goto IL_2C1;
					}
					SpfMacro.ExpandPMacroAsyncState asyncState2 = new SpfMacro.ExpandPMacroAsyncState(senderIdAsyncResult);
					this.BeginGetValidatedDomainName(new AsyncCallback(this.GetValidatedDomainNameCallback), asyncState2);
					return senderIdAsyncResult;
				}
				}
			}
			else
			{
				if (c == 's')
				{
					text = (string)this.context.BaseContext.PurportedResponsibleAddress;
					this.context.BaseContext.SetUncacheable();
					goto IL_2C1;
				}
				if (c == 'v')
				{
					if (this.context.BaseContext.IPAddress.AddressFamily == AddressFamily.InterNetwork)
					{
						text = "in-addr";
						goto IL_2C1;
					}
					if (this.context.BaseContext.IPAddress.AddressFamily == AddressFamily.InterNetworkV6)
					{
						text = "ip6";
						goto IL_2C1;
					}
					throw new InvalidOperationException("IP address has invalid address family");
				}
			}
			if (this.exp)
			{
				char c2 = macroCharacter;
				if (c2 != 'c')
				{
					switch (c2)
					{
					case 'r':
						text = this.context.BaseContext.Server.Name;
						break;
					case 't':
						text = ((int)DateTime.UtcNow.Subtract(SpfMacro.Epoch).TotalSeconds).ToString(CultureInfo.InvariantCulture);
						break;
					}
				}
				else
				{
					text = this.context.BaseContext.IPAddress.ToString();
				}
			}
			IL_2C1:
			if (text != null)
			{
				senderIdAsyncResult.InvokeCompleted(text);
			}
			else
			{
				this.context.ValidationCompleted(SenderIdStatus.PermError);
			}
			return senderIdAsyncResult;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003523 File Offset: 0x00001723
		private string EndExpandMacroCharacter(IAsyncResult ar)
		{
			return (string)((SenderIdAsyncResult)ar).GetResult();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003538 File Offset: 0x00001738
		private void GetValidatedDomainNameCallback(IAsyncResult ar)
		{
			SpfMacro.ExpandPMacroAsyncState expandPMacroAsyncState = (SpfMacro.ExpandPMacroAsyncState)ar.AsyncState;
			string invokeResult = this.EndGetValidatedDomainName(ar);
			expandPMacroAsyncState.AsyncResult.InvokeCompleted(invokeResult);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003568 File Offset: 0x00001768
		private IAsyncResult BeginGetValidatedDomainName(AsyncCallback asyncCallback, object asyncState)
		{
			SenderIdAsyncResult senderIdAsyncResult = new SenderIdAsyncResult(asyncCallback, asyncState);
			Util.AsyncDns.BeginPtrRecordQuery(this.context.BaseContext.IPAddress, new AsyncCallback(this.PtrCallback), senderIdAsyncResult);
			return senderIdAsyncResult;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000035A1 File Offset: 0x000017A1
		private string EndGetValidatedDomainName(IAsyncResult ar)
		{
			return (string)((SenderIdAsyncResult)ar).GetResult();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000035B4 File Offset: 0x000017B4
		private void PtrCallback(IAsyncResult ar)
		{
			string[] array;
			DnsStatus dnsStatus = Util.AsyncDns.EndPtrRecordQuery(ar, out array);
			SenderIdAsyncResult senderIdAsyncResult = (SenderIdAsyncResult)ar.AsyncState;
			string text2;
			if (dnsStatus == DnsStatus.Success)
			{
				string text = array[0];
				int num = 0;
				while (num < array.Length && num < 10)
				{
					if (array[num].EndsWith(this.context.PurportedResponsibleDomain, StringComparison.OrdinalIgnoreCase))
					{
						text = array[num];
						break;
					}
					num++;
				}
				text2 = text;
			}
			else
			{
				text2 = "unknown";
			}
			this.context.BaseContext.ExpandedPMacro = text2;
			senderIdAsyncResult.InvokeCompleted(text2);
		}

		// Token: 0x04000031 RID: 49
		private const int MaxDomainLength = 253;

		// Token: 0x04000032 RID: 50
		private const string UnknownDomainName = "unknown";

		// Token: 0x04000033 RID: 51
		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000034 RID: 52
		private static readonly bool[] UnreservedURLCharacters = SpfMacro.GetUnreservedURLCharacters();

		// Token: 0x04000035 RID: 53
		private SenderIdValidationContext context;

		// Token: 0x04000036 RID: 54
		private MacroTermSpfNode currentMacroTerm;

		// Token: 0x04000037 RID: 55
		private bool exp;

		// Token: 0x04000038 RID: 56
		private SenderIdAsyncResult asyncResult;

		// Token: 0x04000039 RID: 57
		private StringBuilder expanded;

		// Token: 0x0200000F RID: 15
		internal class ExpandedMacro
		{
			// Token: 0x0600005D RID: 93 RVA: 0x00003659 File Offset: 0x00001859
			public ExpandedMacro(bool isValid, string value)
			{
				this.isValid = isValid;
				this.value = value;
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600005E RID: 94 RVA: 0x0000366F File Offset: 0x0000186F
			public bool IsValid
			{
				get
				{
					return this.isValid;
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00003677 File Offset: 0x00001877
			public string Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x0400003A RID: 58
			private readonly bool isValid;

			// Token: 0x0400003B RID: 59
			private readonly string value;
		}

		// Token: 0x02000010 RID: 16
		private class ExpandMacroAsyncState
		{
			// Token: 0x06000060 RID: 96 RVA: 0x0000367F File Offset: 0x0000187F
			public ExpandMacroAsyncState(MacroExpandSpfNode macro, SenderIdAsyncResult asyncResult)
			{
				this.Macro = macro;
				this.AsyncResult = asyncResult;
			}

			// Token: 0x0400003C RID: 60
			public readonly MacroExpandSpfNode Macro;

			// Token: 0x0400003D RID: 61
			public readonly SenderIdAsyncResult AsyncResult;
		}

		// Token: 0x02000011 RID: 17
		private class ExpandPMacroAsyncState
		{
			// Token: 0x06000061 RID: 97 RVA: 0x00003695 File Offset: 0x00001895
			public ExpandPMacroAsyncState(SenderIdAsyncResult asyncResult)
			{
				this.AsyncResult = asyncResult;
			}

			// Token: 0x0400003E RID: 62
			public readonly SenderIdAsyncResult AsyncResult;
		}
	}
}
