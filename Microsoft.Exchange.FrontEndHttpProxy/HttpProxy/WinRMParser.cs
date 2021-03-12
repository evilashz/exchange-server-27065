using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.EventLogs;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200007E RID: 126
	internal class WinRMParser
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00016AFC File Offset: 0x00014CFC
		public WinRMParser(int traceContext)
		{
			this.TraceContext = traceContext;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00016B0B File Offset: 0x00014D0B
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00016B13 File Offset: 0x00014D13
		private int TraceContext { get; set; }

		// Token: 0x060003D9 RID: 985 RVA: 0x00016B1C File Offset: 0x00014D1C
		internal bool TryParseStream(Stream stream, out WinRMInfo winRMInfo, out string failureHint)
		{
			failureHint = null;
			winRMInfo = null;
			if (stream == null)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WinRMParser::TryParseStream] Context={0}, stream = null.", this.TraceContext);
				failureHint = "Stream = null";
				return false;
			}
			bool result;
			try
			{
				byte[] array = new byte[10000];
				int num = stream.Read(array, 0, array.Length);
				if (num <= 0)
				{
					ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WinRMParser::TryParseStream] Context={0}, bytesRead <= 0", this.TraceContext);
					failureHint = "BytesRead <= 0";
					result = false;
				}
				else
				{
					string @string = Encoding.UTF8.GetString(array, 0, Math.Min(num, WinRMHelper.MaxBytesToPeekIntoRequestStream.Value));
					string text = null;
					string text2 = null;
					string text3 = null;
					string text4 = null;
					string commandName = null;
					string text5 = null;
					int num2 = 0;
					this.TryMatch(@string, WinRMParser.actionRegex, "action", ref num2, out text);
					if (text == null)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WinRMParser::TryParseStream] No Action found in the input string", this.TraceContext);
						failureHint = "RawAction = null";
						result = false;
					}
					else
					{
						this.TryMatch(@string, WinRMParser.sessionIdRegx, "sessionId", ref num2, out text2);
						if (text2 == null)
						{
							ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WinRMParser::TryParseStream] No SessionId found in the input string", this.TraceContext);
						}
						else if (text2.StartsWith("uuid:"))
						{
							text2 = text2.Substring(5);
						}
						this.TryMatch(@string, WinRMParser.shellIdRegx, "shellId", ref num2, out text3);
						if (text3 == null && !"http://schemas.xmlsoap.org/ws/2004/09/transfer/Create".Equals(text, StringComparison.OrdinalIgnoreCase))
						{
							ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WinRMParser::TryParseStream] No ShellId found in the input string and the current action is " + text, this.TraceContext);
							failureHint = "ShellId = null";
							result = false;
						}
						else
						{
							this.TryMatch(@string, WinRMParser.commandIdRegx, "commandId", ref num2, out text4);
							if (text4 != null)
							{
								this.TryMatch(@string, WinRMParser.commandNameRegx, "commandName", ref num2, out commandName);
							}
							if ("http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Signal".Equals(text, StringComparison.OrdinalIgnoreCase))
							{
								this.TryMatch(@string, WinRMParser.signalCodeRegex, "signalCode", ref num2, out text5);
							}
							string text6 = this.GenerateUserFriendlyAction(text, text4, commandName, text5);
							this.UpdateCommandIdToCommandNameCache(text, text4, commandName, text5);
							ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[WinRMParser::TryParseStream] Context={0}, Action=\"{1}\", RawAction=\"{2}\", SessionId=\"{3}\", ShellId=\"{4}\", CommandId=\"{5}\", SignalCode=\"{6}\".", new object[]
							{
								this.TraceContext,
								text6,
								text,
								text2,
								text3,
								text4,
								text5
							});
							winRMInfo = new WinRMInfo
							{
								Action = text6,
								RawAction = text,
								SessionId = text2,
								ShellId = text3,
								CommandId = text4,
								CommandName = commandName,
								SignalCode = text5
							};
							result = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Diagnostics.ReportException(ex, FrontEndHttpProxyEventLogConstants.Tuple_InternalServerError, null, "Exception from WinRMParser::TryParseStream event: {0}");
				failureHint = ex.ToString();
				result = false;
			}
			return result;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00016E10 File Offset: 0x00015010
		private string GenerateUserFriendlyAction(string rawAction, string commandId, string commandName, string signalCode)
		{
			string text;
			if (!WinRMInfo.KnownActions.TryGetValue(rawAction, out text))
			{
				text = rawAction;
			}
			if ("http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Signal".Equals(rawAction, StringComparison.OrdinalIgnoreCase) && "http://schemas.microsoft.com/wbem/wsman/1/windows/shell/signal/terminate".Equals(signalCode, StringComparison.OrdinalIgnoreCase))
			{
				text = "Terminate";
			}
			if (!string.IsNullOrEmpty(commandId) && string.IsNullOrEmpty(commandName))
			{
				WinRMParser.commandIdToCommandName.TryGetValue(commandId, out commandName);
			}
			if (!string.IsNullOrEmpty(commandName))
			{
				text = commandName + ":" + text;
			}
			else if ("Receive".Equals(text, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(commandId))
			{
				text = "Command:Receive";
			}
			return text;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00016EA4 File Offset: 0x000150A4
		private void UpdateCommandIdToCommandNameCache(string rawAction, string commandId, string commandName, string signalCode)
		{
			if (string.IsNullOrEmpty(commandId))
			{
				return;
			}
			if ("http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Command".Equals(rawAction, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(commandName))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, string, string>((long)this.GetHashCode(), "[WinRMParser::UpdateCommandIdToCommandNameCache] Context={0}, Add CommandId to cache. CommandId={1}, CommandName={2}", this.TraceContext, commandId, commandName);
				WinRMParser.commandIdToCommandName.TryAdd(commandId, commandName);
				return;
			}
			if ("http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Signal".Equals(rawAction, StringComparison.OrdinalIgnoreCase) && "http://schemas.microsoft.com/wbem/wsman/1/windows/shell/signal/terminate".Equals(signalCode, StringComparison.OrdinalIgnoreCase))
			{
				string text;
				bool flag = WinRMParser.commandIdToCommandName.TryRemove(commandId, out text);
				ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[WinRMParser::UpdateCommandIdToCommandNameCache] Context={0}, Remove CommandId from cache. CommandId={1}, CommandName={2}, removeResult={3}", new object[]
				{
					this.TraceContext,
					commandId,
					text,
					flag
				});
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00016F64 File Offset: 0x00015164
		private bool TryMatch(string requestString, Regex regex, string groupName, ref int startAt, out string value)
		{
			value = null;
			Match match = regex.Match(requestString);
			if (match.Success && match.Groups.Count > 0)
			{
				Group group = match.Groups[groupName];
				if (group.Success)
				{
					value = group.Value;
					if (value != null)
					{
						value = value.Trim();
					}
					startAt += group.Index + group.Length;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040002C7 RID: 711
		private static ConcurrentDictionary<string, string> commandIdToCommandName = new ConcurrentDictionary<string, string>();

		// Token: 0x040002C8 RID: 712
		private static Regex actionRegex = new Regex("<a:Action(.)*>(?<action>(.)*)</a:Action>", RegexOptions.Compiled);

		// Token: 0x040002C9 RID: 713
		private static Regex sessionIdRegx = new Regex("<p:SessionId(.)*>(?<sessionId>(.)*)</p:SessionId>", RegexOptions.Compiled);

		// Token: 0x040002CA RID: 714
		private static Regex shellIdRegx = new Regex("<w:Selector Name=\"ShellId\"(.)*>(?<shellId>(.)*)</w:Selector>", RegexOptions.Compiled);

		// Token: 0x040002CB RID: 715
		private static Regex commandIdRegx = new Regex("<(.)*CommandId=\"(?<commandId>[^\"]*)\"[^>]*>", RegexOptions.Compiled);

		// Token: 0x040002CC RID: 716
		private static Regex commandNameRegx = new Regex("<rsp:Command(.)*>(?<commandName>(.)*)</rsp:Command>", RegexOptions.Compiled);

		// Token: 0x040002CD RID: 717
		private static Regex signalCodeRegex = new Regex("<rsp:Code>(?<signalCode>(.)*)</rsp:Code>", RegexOptions.Compiled);
	}
}
