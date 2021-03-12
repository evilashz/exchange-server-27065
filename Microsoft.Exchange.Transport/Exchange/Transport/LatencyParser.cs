using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000172 RID: 370
	internal class LatencyParser
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x00041E1F File Offset: 0x0004001F
		protected LatencyParser(Trace tracer)
		{
			this.tracer = tracer;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00041E2E File Offset: 0x0004002E
		public string StringToParse
		{
			get
			{
				return this.stringToParse;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00041E36 File Offset: 0x00040036
		protected Trace Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00041E40 File Offset: 0x00040040
		protected static int SkipWhitespaces(string s, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00041E70 File Offset: 0x00040070
		protected static bool TryParseLatency(string s, int startIndex, int count, out ushort latencySeconds)
		{
			string s2 = s.Substring(startIndex, count);
			if (!ushort.TryParse(s2, out latencySeconds) || (double)latencySeconds > TransportAppConfig.LatencyTrackerConfig.MaxLatency.TotalSeconds)
			{
				latencySeconds = ushort.MaxValue;
				return false;
			}
			return true;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00041EAC File Offset: 0x000400AC
		protected static bool TryParseLatency(string s, int startIndex, int count, out float latencySeconds)
		{
			string s2 = s.Substring(startIndex, count);
			if (!float.TryParse(s2, out latencySeconds) || (double)latencySeconds > TransportAppConfig.LatencyTrackerConfig.MaxLatency.TotalSeconds)
			{
				latencySeconds = 65535f;
				return false;
			}
			return true;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00041EE8 File Offset: 0x000400E8
		protected static bool TryParseDateTime(string s, int startIndex, int count, out DateTime dt)
		{
			string s2 = s.Substring(startIndex, count);
			DateTimeStyles style = DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;
			return DateTime.TryParseExact(s2, "yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo, style, out dt);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00041F14 File Offset: 0x00040114
		protected bool TryParse(string s, int startIndex, int count)
		{
			this.state = LatencyParser.State.Server;
			this.latencyType = null;
			this.componentNameStart = (this.componentNameLength = -1);
			this.seenTotal = (this.isTotal = false);
			this.stringToParse = s;
			while (this.TryGetNextToken(startIndex, count))
			{
				if (this.tokenLength == 0)
				{
					this.Tracer.TraceError<LatencyParser.State, int, string>(0L, "Latency Parser (State={0}): Consecutive separators at position {1} while extracting a token in string '{2}'", this.state, this.tokenStart, s);
					return false;
				}
				switch (this.state)
				{
				case LatencyParser.State.Component:
					if (!this.ProcessComponentToken())
					{
						return false;
					}
					break;
				case LatencyParser.State.ComponentValue:
					if (!this.ProcessComponentValueToken())
					{
						return false;
					}
					break;
				case LatencyParser.State.ComponentOrPendingComponent:
				{
					char c = this.separator;
					if (c != '\0')
					{
						switch (c)
						{
						case ';':
							goto IL_CC;
						case '<':
							break;
						case '=':
							if (!this.ProcessComponentToken())
							{
								return false;
							}
							goto IL_13E;
						default:
							if (c == '|')
							{
								goto IL_CC;
							}
							break;
						}
						return this.ProcessUnexpectedSeparator();
					}
					IL_CC:
					if (!this.ProcessPendingComponentToken())
					{
						return false;
					}
					break;
				}
				case LatencyParser.State.End:
					throw new InvalidOperationException("Latency Parser: lingering End state in the state machine; string: " + s);
				case LatencyParser.State.PendingComponent:
					if (!this.ProcessPendingComponentToken())
					{
						return false;
					}
					break;
				case LatencyParser.State.PendingComponentOrServer:
					if (this.separator == '=')
					{
						if (!this.ProcessServerToken())
						{
							return false;
						}
					}
					else if (!this.ProcessPendingComponentToken())
					{
						return false;
					}
					break;
				case LatencyParser.State.Server:
					if (!this.ProcessServerToken())
					{
						return false;
					}
					break;
				case LatencyParser.State.ServerFqdn:
					if (!this.ProcessServerFqdnToken())
					{
						return false;
					}
					break;
				}
				IL_13E:
				if (this.separator == '\0')
				{
					break;
				}
				count -= this.tokenStart + this.tokenLength + 1 - startIndex;
				startIndex = this.tokenStart + this.tokenLength + 1;
				if (count <= 0)
				{
					break;
				}
			}
			if (this.state != LatencyParser.State.End)
			{
				this.Tracer.TraceError<string>(0L, "Latency Parser: unexpected end of string in '{0}'", s);
				return false;
			}
			return true;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000420BD File Offset: 0x000402BD
		protected virtual bool HandleLocalServerFqdn(string s, int startIndex, int count)
		{
			return true;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000420C0 File Offset: 0x000402C0
		protected virtual bool HandleServerFqdn(string s, int startIndex, int count)
		{
			return true;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000420C3 File Offset: 0x000402C3
		protected virtual bool HandleComponentLatency(string s, int componentNameStart, int componentNameLength, int latencyStart, int latencyLength)
		{
			return true;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000420C6 File Offset: 0x000402C6
		protected virtual bool HandleTotalLatency(string s, int startIndex, int count)
		{
			return true;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000420C9 File Offset: 0x000402C9
		protected virtual void HandleTotalComponent(string s, int startIndex, int count)
		{
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000420CB File Offset: 0x000402CB
		protected virtual bool HandlePendingComponent(string s, int startIndex, int count)
		{
			return true;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000420CE File Offset: 0x000402CE
		private static bool SubstringEquals(string s1, int startIndex, int count, string s2)
		{
			return s2.Length == count && string.Compare(s1, startIndex, s2, 0, count, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000420E9 File Offset: 0x000402E9
		private static object TraceSeparator(char separator)
		{
			if (separator != '\0')
			{
				return separator;
			}
			return "<eos>";
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000420FC File Offset: 0x000402FC
		private bool ProcessUnexpectedSeparator()
		{
			this.Tracer.TraceError(0L, "Latency Parser (State={0}): Unexpected separator '{1}' at position {2} in string '{3}'", new object[]
			{
				this.state,
				LatencyParser.TraceSeparator(this.separator),
				this.tokenStart + this.tokenLength,
				this.stringToParse
			});
			return false;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00042160 File Offset: 0x00040360
		private bool ProcessUnexpectedToken(string tokenDescr)
		{
			this.Tracer.TraceError(0L, "Latency Parser (State={0}): Unexpected {1}token at position {2} in string '{3}'", new object[]
			{
				this.state,
				tokenDescr,
				this.tokenStart,
				this.stringToParse
			});
			return false;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000421B1 File Offset: 0x000403B1
		private bool ProcessForcedExit()
		{
			this.Tracer.TraceError<LatencyParser.State>(0L, "Latency Parser (State={0}): forced exit", this.state);
			return false;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000421CC File Offset: 0x000403CC
		private bool ProcessServerToken()
		{
			if (this.separator != '=')
			{
				return this.ProcessUnexpectedSeparator();
			}
			if (LatencyParser.SubstringEquals(this.stringToParse, this.tokenStart, this.tokenLength, "SRV"))
			{
				if (this.latencyType != null && this.latencyType.Value == LatencyParser.LatencyType.LocalServer)
				{
					return this.ProcessUnexpectedToken("server ");
				}
				this.latencyType = new LatencyParser.LatencyType?(LatencyParser.LatencyType.EndToEnd);
			}
			else
			{
				if (!LatencyParser.SubstringEquals(this.stringToParse, this.tokenStart, this.tokenLength, "LSRV"))
				{
					return this.ProcessUnexpectedToken(string.Empty);
				}
				if (this.latencyType != null)
				{
					return this.ProcessUnexpectedToken("local server ");
				}
				this.latencyType = new LatencyParser.LatencyType?(LatencyParser.LatencyType.LocalServer);
			}
			this.seenTotal = false;
			this.state = LatencyParser.State.ServerFqdn;
			return true;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0004229C File Offset: 0x0004049C
		private bool ProcessServerFqdnToken()
		{
			if (this.separator != ':')
			{
				return this.ProcessUnexpectedSeparator();
			}
			bool flag;
			if (this.latencyType.Value == LatencyParser.LatencyType.LocalServer)
			{
				flag = this.HandleLocalServerFqdn(this.stringToParse, this.tokenStart, this.tokenLength);
			}
			else
			{
				flag = this.HandleServerFqdn(this.stringToParse, this.tokenStart, this.tokenLength);
			}
			if (!flag)
			{
				return this.ProcessForcedExit();
			}
			this.state = LatencyParser.State.ComponentOrPendingComponent;
			return true;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00042310 File Offset: 0x00040510
		private bool ProcessComponentToken()
		{
			if (this.separator != '=')
			{
				return this.ProcessUnexpectedSeparator();
			}
			if (this.stringToParse.IndexOf("TOTAL", this.tokenStart, this.tokenLength, StringComparison.OrdinalIgnoreCase) == this.tokenStart)
			{
				if (this.seenTotal)
				{
					this.Tracer.TraceError<LatencyParser.State, string>(0L, "Latency Parser (State={0}): Multiple TOTALs for the same server in string '{1}'", this.state, this.stringToParse);
					return false;
				}
				this.seenTotal = (this.isTotal = true);
				this.HandleTotalComponent(this.stringToParse, this.tokenStart, this.tokenLength);
			}
			else
			{
				this.componentNameStart = this.tokenStart;
				this.componentNameLength = this.tokenLength;
			}
			this.state = LatencyParser.State.ComponentValue;
			return true;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000423C8 File Offset: 0x000405C8
		private bool ProcessComponentValueToken()
		{
			bool flag;
			if (this.isTotal)
			{
				flag = this.HandleTotalLatency(this.stringToParse, this.tokenStart, this.tokenLength);
				this.isTotal = false;
			}
			else
			{
				flag = this.HandleComponentLatency(this.stringToParse, this.componentNameStart, this.componentNameLength, this.tokenStart, this.tokenLength);
				this.componentNameStart = (this.componentNameLength = -1);
			}
			if (!flag)
			{
				return this.ProcessForcedExit();
			}
			char c = this.separator;
			if (c <= '(')
			{
				if (c == '\0')
				{
					this.state = LatencyParser.State.End;
					return true;
				}
				if (c == '(')
				{
					this.state = LatencyParser.State.Component;
					return true;
				}
			}
			else
			{
				if (c == ';')
				{
					this.state = LatencyParser.State.PendingComponentOrServer;
					return true;
				}
				if (c == '|')
				{
					this.state = LatencyParser.State.Component;
					return true;
				}
			}
			return this.ProcessUnexpectedSeparator();
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00042490 File Offset: 0x00040690
		private bool ProcessPendingComponentToken()
		{
			if (!this.HandlePendingComponent(this.stringToParse, this.tokenStart, this.tokenLength))
			{
				return this.ProcessForcedExit();
			}
			char c = this.separator;
			if (c != '\0')
			{
				if (c != ';')
				{
					if (c != '|')
					{
						return this.ProcessUnexpectedSeparator();
					}
					this.state = LatencyParser.State.PendingComponent;
				}
				else
				{
					this.state = LatencyParser.State.Server;
				}
			}
			else
			{
				this.state = LatencyParser.State.End;
			}
			return true;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000424F8 File Offset: 0x000406F8
		private bool TryGetNextToken(int startIndex, int count)
		{
			if (startIndex < 0 || startIndex >= this.stringToParse.Length)
			{
				string message = string.Format("Index out of range for string with length {0}", this.stringToParse.Length);
				throw new ArgumentOutOfRangeException("startIndex", startIndex, message);
			}
			if (count < 0 || startIndex + count > this.stringToParse.Length)
			{
				string message2 = string.Format("Count out of range for string with length {0} and startIndex {1}", this.stringToParse.Length, startIndex);
				throw new ArgumentOutOfRangeException("count", count, message2);
			}
			this.tokenStart = (this.tokenLength = -1);
			this.separator = '\0';
			this.tokenStart = LatencyParser.SkipWhitespaces(this.stringToParse, startIndex, count);
			if (this.tokenStart == -1)
			{
				return false;
			}
			this.tokenLength = 0;
			while (this.ShouldKeepLooking(startIndex, count))
			{
				this.tokenLength++;
			}
			if (this.tokenStart + this.tokenLength < startIndex + count)
			{
				this.separator = this.stringToParse[this.tokenStart + this.tokenLength];
			}
			return true;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00042612 File Offset: 0x00040812
		private bool ShouldKeepLooking(int startIndex, int maxCharToExamine)
		{
			return this.tokenStart + this.tokenLength < startIndex + maxCharToExamine && !LatencyFormatter.IsSeparator(this.stringToParse[this.tokenStart + this.tokenLength]);
		}

		// Token: 0x0400082D RID: 2093
		private const char EndOfStringSeparator = '\0';

		// Token: 0x0400082E RID: 2094
		private readonly Trace tracer;

		// Token: 0x0400082F RID: 2095
		private string stringToParse;

		// Token: 0x04000830 RID: 2096
		private int tokenStart;

		// Token: 0x04000831 RID: 2097
		private int tokenLength;

		// Token: 0x04000832 RID: 2098
		private char separator;

		// Token: 0x04000833 RID: 2099
		private LatencyParser.State state;

		// Token: 0x04000834 RID: 2100
		private LatencyParser.LatencyType? latencyType;

		// Token: 0x04000835 RID: 2101
		private int componentNameStart;

		// Token: 0x04000836 RID: 2102
		private int componentNameLength;

		// Token: 0x04000837 RID: 2103
		private bool seenTotal;

		// Token: 0x04000838 RID: 2104
		private bool isTotal;

		// Token: 0x02000173 RID: 371
		private enum State
		{
			// Token: 0x0400083A RID: 2106
			Component,
			// Token: 0x0400083B RID: 2107
			ComponentValue,
			// Token: 0x0400083C RID: 2108
			ComponentOrPendingComponent,
			// Token: 0x0400083D RID: 2109
			End,
			// Token: 0x0400083E RID: 2110
			PendingComponent,
			// Token: 0x0400083F RID: 2111
			PendingComponentOrServer,
			// Token: 0x04000840 RID: 2112
			Server,
			// Token: 0x04000841 RID: 2113
			ServerFqdn
		}

		// Token: 0x02000174 RID: 372
		private enum LatencyType
		{
			// Token: 0x04000843 RID: 2115
			LocalServer,
			// Token: 0x04000844 RID: 2116
			EndToEnd
		}
	}
}
