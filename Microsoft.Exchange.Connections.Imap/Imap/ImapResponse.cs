using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ImapResponse
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00008198 File Offset: 0x00006398
		internal ImapResponse(ILog log)
		{
			this.log = log;
			this.parseContext = new ImapResponse.ParseContext();
			this.responseBuffer = new BufferBuilder(1024);
			this.responseLines = new List<string>(20);
			this.responseLiterals = new List<Stream>(1);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000081E6 File Offset: 0x000063E6
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000081EE File Offset: 0x000063EE
		internal bool IsComplete { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000081F7 File Offset: 0x000063F7
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000081FF File Offset: 0x000063FF
		internal bool IsWaitingForLiteral { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008208 File Offset: 0x00006408
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00008210 File Offset: 0x00006410
		internal ImapStatus Status { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00008219 File Offset: 0x00006419
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00008221 File Offset: 0x00006421
		internal int LiteralBytesRemaining { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000822A File Offset: 0x0000642A
		internal int TotalLiteralBytesExpected
		{
			get
			{
				return this.curLiteralBytes;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00008232 File Offset: 0x00006432
		internal IList<string> ResponseLines
		{
			get
			{
				return this.responseLines.AsReadOnly();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008240 File Offset: 0x00006440
		internal static Exception Fail(string prefix, ImapCommand command, string context)
		{
			StringBuilder stringBuilder = new StringBuilder(40);
			if (prefix != null)
			{
				stringBuilder.Append(prefix);
				stringBuilder.Append(": ");
			}
			stringBuilder.Append(command.ToPiiCleanString());
			if (context != null)
			{
				stringBuilder.Append(": [");
				stringBuilder.Append(context);
				stringBuilder.Append("]");
			}
			return new ImapCommunicationException(stringBuilder.ToString(), RetryPolicy.Backoff);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000082A8 File Offset: 0x000064A8
		internal void Reset(ImapCommand newCommand)
		{
			if (newCommand == null)
			{
				this.commandId = null;
				this.checkForUnsolicitedByeResponse = true;
			}
			else
			{
				this.commandId = newCommand.CommandId;
				this.checkForUnsolicitedByeResponse = (newCommand.CommandType != ImapCommandType.Logout);
			}
			this.Status = ImapStatus.Unknown;
			this.IsComplete = false;
			this.IsWaitingForLiteral = false;
			this.lastByteRead = 0;
			this.LiteralBytesRemaining = 0;
			this.curLiteralBytes = 0;
			this.responseBuffer.Reset();
			this.responseLines.Clear();
			this.responseLiterals.Clear();
			if (this.inFlightLiteral != null)
			{
				this.inFlightLiteral.Dispose();
				this.inFlightLiteral = null;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000834C File Offset: 0x0000654C
		internal int AddData(byte[] data, int offset, int size)
		{
			int num = size + offset;
			int num2 = offset;
			while (num2 < num && !this.IsComplete)
			{
				if (this.LiteralBytesRemaining > 0)
				{
					int num3 = Math.Min(this.LiteralBytesRemaining, num - num2);
					this.log.Assert(this.inFlightLiteral != null, "inFlightLiteral is null.", new object[0]);
					this.inFlightLiteral.Write(data, num2, num3);
					num2 += num3 - 1;
					this.LiteralBytesRemaining -= num3;
					if (this.LiteralBytesRemaining == 0)
					{
						this.inFlightLiteral.Position = 0L;
						this.responseLiterals.Add(this.inFlightLiteral);
						this.inFlightLiteral = null;
						this.curLiteralBytes = 0;
					}
				}
				else
				{
					this.responseBuffer.Append(data[num2]);
					if (this.lastByteRead == 13 && data[num2] == 10)
					{
						string text = Encoding.ASCII.GetString(this.responseBuffer.GetBuffer(), 0, this.responseBuffer.Length).Trim();
						this.responseLines.Add(text);
						this.responseBuffer.Reset();
						this.ProcessLineDuringReading(text);
					}
					this.lastByteRead = data[num2];
				}
				num2++;
			}
			return num2 - offset;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008480 File Offset: 0x00006680
		internal bool TryParseIntoResult(ImapCommand command, ref ImapResultData result)
		{
			if (this.responseLines.Count == 0)
			{
				return false;
			}
			ImapResponse.TryProcessLine lineProcessor = this.GetLineProcessor(command);
			if (lineProcessor == null)
			{
				result.FailureException = ImapResponse.Fail("Unknown CommandType", command, null);
				return false;
			}
			string text = this.responseLines[this.responseLines.Count - 1];
			if (!text.StartsWith(command.CommandId + ' ', StringComparison.Ordinal))
			{
				result.FailureException = ImapResponse.Fail(null, command, text);
				return false;
			}
			ImapStatus imapStatus = ImapResponse.CheckAndReturnCommandCompletionCode(text, command.CommandId);
			if (imapStatus != ImapStatus.Ok)
			{
				return true;
			}
			this.parseContext.Reset(this.responseLines, this.responseLiterals);
			while (!this.parseContext.Error && this.parseContext.MoveNextLines())
			{
				string currentLine = this.parseContext.CurrentLine;
				if (currentLine.Length > 0 && !lineProcessor(command, result) && !ImapResponse.IsUntaggedResponseLine(currentLine) && !text.StartsWith(command.CommandId + ' ', StringComparison.Ordinal))
				{
					this.log.Debug("Unprocessed line [{0}] in response to command [{1}].", new object[]
					{
						currentLine,
						command.ToPiiCleanString()
					});
				}
			}
			return !this.parseContext.Error;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000085C4 File Offset: 0x000067C4
		internal string GetLastResponseLine()
		{
			if (this.responseLines.Count <= 0)
			{
				return null;
			}
			return this.responseLines[this.responseLines.Count - 1];
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000085F0 File Offset: 0x000067F0
		private static bool AllowWhitespaceInKey(string fetchResults, int idx, string key)
		{
			if (key == "BODY")
			{
				int num = idx + 1;
				while (num < fetchResults.Length && fetchResults[num] == ' ')
				{
					num++;
				}
				return num < fetchResults.Length && fetchResults[num] == '[';
			}
			return false;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008640 File Offset: 0x00006840
		private static bool TryParseLiteralSize(string details, out int sizeOfPendingLiteral)
		{
			bool result = false;
			sizeOfPendingLiteral = 0;
			Match match;
			if (ImapResponse.CheckResponse(details, ImapResponse.LiteralDelimiter, out match))
			{
				result = ImapResponse.SafeConvert(match, 1, out sizeOfPendingLiteral);
			}
			return result;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000866C File Offset: 0x0000686C
		private static ImapStatus CheckAndReturnCommandCompletionCode(string line, string commandId)
		{
			string text = ImapResponse.SafeSubstring(line, commandId.Length + 1);
			if (text.StartsWith("OK", StringComparison.OrdinalIgnoreCase))
			{
				return ImapStatus.Ok;
			}
			if (text.StartsWith("NO", StringComparison.OrdinalIgnoreCase))
			{
				return ImapStatus.No;
			}
			if (text.StartsWith("BAD", StringComparison.OrdinalIgnoreCase))
			{
				return ImapStatus.Bad;
			}
			return ImapStatus.Unknown;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000086B9 File Offset: 0x000068B9
		private static bool CheckResponse(string strToCheck, Regex expectedExpression, out Match match)
		{
			match = expectedExpression.Match(strToCheck);
			return match.Success;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000086CB File Offset: 0x000068CB
		private static bool IsUntaggedResponseLine(string line)
		{
			return line.Length > 0 && line[0] == '*';
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000086E3 File Offset: 0x000068E3
		private static bool IsServerWaitingForLiteralLine(string line)
		{
			return line.Length > 0 && line[0] == '+';
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000086FC File Offset: 0x000068FC
		private static bool SafeGet(Match match, int groupIdx, out string captureValue)
		{
			bool result = false;
			captureValue = null;
			if (match != null && match.Groups.Count > groupIdx)
			{
				captureValue = match.Groups[groupIdx].Value;
				result = true;
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008738 File Offset: 0x00006938
		private static bool SafeConvert(Match match, int groupIdx, out int converted)
		{
			bool result = false;
			converted = 0;
			string text;
			if (ImapResponse.SafeGet(match, groupIdx, out text) && !string.IsNullOrEmpty(text))
			{
				text = text.Trim();
				result = int.TryParse(text, out converted);
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008770 File Offset: 0x00006970
		private static bool SafeConvert(Match match, int groupIdx, out uint converted)
		{
			bool result = false;
			converted = 0U;
			string text;
			if (ImapResponse.SafeGet(match, groupIdx, out text) && !string.IsNullOrEmpty(text))
			{
				text = text.Trim();
				result = uint.TryParse(text, out converted);
			}
			return result;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000087A5 File Offset: 0x000069A5
		private static string SafeSubstring(string incoming, int idxToStart)
		{
			if (incoming == null || incoming.Length <= idxToStart)
			{
				return string.Empty;
			}
			return incoming.Substring(idxToStart);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000087C0 File Offset: 0x000069C0
		private void ProcessLineDuringReading(string responseLine)
		{
			this.IsWaitingForLiteral = false;
			if (!ImapResponse.TryParseLiteralSize(responseLine, out this.curLiteralBytes))
			{
				if (ImapResponse.IsUntaggedResponseLine(responseLine))
				{
					if (this.commandId == null)
					{
						if (ImapResponse.UnsolicitedOkResponse.IsMatch(responseLine))
						{
							this.IsComplete = true;
							this.Status = ImapStatus.Ok;
						}
						else if (ImapResponse.UnsolicitedNoResponse.IsMatch(responseLine))
						{
							this.IsComplete = true;
							this.Status = ImapStatus.No;
						}
						else if (ImapResponse.UnsolicitedBadResponse.IsMatch(responseLine))
						{
							this.IsComplete = true;
							this.Status = ImapStatus.Bad;
						}
					}
					if (this.checkForUnsolicitedByeResponse && ImapResponse.UnsolicitedByeResponse.IsMatch(responseLine))
					{
						this.IsComplete = true;
						this.Status = ImapStatus.Bye;
						return;
					}
				}
				else
				{
					if (ImapResponse.IsServerWaitingForLiteralLine(responseLine))
					{
						this.IsWaitingForLiteral = true;
						return;
					}
					if (this.commandId != null && responseLine.StartsWith(this.commandId + ' ', StringComparison.Ordinal))
					{
						this.IsComplete = true;
						this.Status = ImapResponse.CheckAndReturnCommandCompletionCode(responseLine, this.commandId);
					}
				}
				return;
			}
			if (this.curLiteralBytes > 0)
			{
				this.log.Assert(this.inFlightLiteral == null, "No literal should be in-flight.", new object[0]);
				this.LiteralBytesRemaining = this.curLiteralBytes;
				this.inFlightLiteral = TemporaryStorage.Create();
				return;
			}
			this.responseLiterals.Add(null);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008908 File Offset: 0x00006B08
		private bool SimpleResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			bool result = false;
			string currentLine = this.parseContext.CurrentLine;
			if (ImapResponse.UnsolicitedByeResponse.IsMatch(currentLine))
			{
				result = true;
				resultData.FailureException = ImapResponse.Fail("Server disconnected", command, currentLine);
			}
			return result;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008948 File Offset: 0x00006B48
		private bool CapabilityResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			bool flag = this.SimpleResponseProcessor(command, resultData);
			Match match;
			if (!flag && !this.parseContext.Error && ImapResponse.CheckResponse(this.parseContext.CurrentLine, ImapResponse.CapabilityResponse, out match))
			{
				string text;
				if (ImapResponse.SafeGet(match, 1, out text))
				{
					string[] capabilities = text.Split(new char[]
					{
						' '
					});
					resultData.Capabilities = new ImapServerCapabilities(capabilities);
					flag = true;
				}
				else
				{
					resultData.FailureException = ImapResponse.Fail("Failed to parse result from CAPABILITY", command, this.parseContext.CurrentLine);
					this.parseContext.Error = true;
				}
			}
			return flag;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000089E8 File Offset: 0x00006BE8
		private bool LogoutResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			string currentLine = this.parseContext.CurrentLine;
			return ImapResponse.UnsolicitedByeResponse.IsMatch(currentLine) || this.SimpleResponseProcessor(command, resultData);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008A20 File Offset: 0x00006C20
		private bool SelectResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			if (resultData.Mailboxes.Count == 0)
			{
				resultData.Mailboxes.Add((ImapMailbox)command.CommandParameters[0]);
			}
			bool flag = this.SimpleResponseProcessor(command, resultData);
			if (!flag && !this.parseContext.Error)
			{
				string currentLine = this.parseContext.CurrentLine;
				Match match;
				if (ImapResponse.CheckResponse(currentLine, ImapResponse.OkDataResponse, out match))
				{
					if (match.Groups.Count == 2)
					{
						string value = match.Groups[1].Value;
						Match match2;
						if (ImapResponse.CheckResponse(value, ImapResponse.UidValidity, out match2))
						{
							uint num;
							this.parseContext.Error = !ImapResponse.SafeConvert(match2, 1, out num);
							if (!this.parseContext.Error)
							{
								resultData.Mailboxes[0].UidValidity = new long?((long)((ulong)num));
							}
							else
							{
								resultData.FailureException = ImapResponse.Fail("Invalid UIDVALIDITY Response", command, match2.ToString());
							}
						}
						if (ImapResponse.CheckResponse(value, ImapResponse.UidNext, out match2))
						{
							uint num2;
							this.parseContext.Error = !ImapResponse.SafeConvert(match2, 1, out num2);
							if (!this.parseContext.Error)
							{
								resultData.Mailboxes[0].UidNext = new long?((long)((ulong)num2));
							}
							else
							{
								resultData.FailureException = ImapResponse.Fail("Invalid UIDNEXT Response", command, match2.ToString());
							}
						}
						if (ImapResponse.CheckResponse(value, ImapResponse.PermanentFlags, out match2))
						{
							string stringForm;
							this.parseContext.Error = !ImapResponse.SafeGet(match2, 1, out stringForm);
							if (!this.parseContext.Error)
							{
								resultData.Mailboxes[0].PermanentFlags = ImapUtilities.ConvertStringFormToFlags(stringForm);
							}
							else
							{
								resultData.FailureException = ImapResponse.Fail("Invalid PERMANENTFLAGS Response", command, match2.ToString());
							}
						}
					}
					else
					{
						resultData.FailureException = ImapResponse.Fail("Invalid OK Response", command, match.ToString());
					}
					flag = true;
				}
				else if (ImapResponse.CheckResponse(currentLine, ImapResponse.ExistsResponse, out match))
				{
					uint num3;
					this.parseContext.Error = !ImapResponse.SafeConvert(match, 1, out num3);
					if (!this.parseContext.Error && num3 < 2147483647U)
					{
						resultData.Mailboxes[0].NumberOfMessages = new int?((int)num3);
					}
					else
					{
						resultData.FailureException = ImapResponse.Fail("Invalid EXISTS Response", command, match.ToString());
					}
					flag = true;
				}
				else if (currentLine.StartsWith(command.CommandId, StringComparison.OrdinalIgnoreCase))
				{
					if (!currentLine.ToUpperInvariant().Contains("READ-WRITE"))
					{
						resultData.Mailboxes[0].IsWritable = false;
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008CB4 File Offset: 0x00006EB4
		private bool StatusResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			if (resultData.Mailboxes.Count == 0)
			{
				resultData.Mailboxes.Add((ImapMailbox)command.CommandParameters[0]);
			}
			bool flag = this.SimpleResponseProcessor(command, resultData);
			if (!flag && !this.parseContext.Error)
			{
				string currentLine = this.parseContext.CurrentLine;
				Match match;
				if (ImapResponse.CheckResponse(currentLine, ImapResponse.StatusResponse, out match))
				{
					if (match.Groups.Count == 3)
					{
						uint num;
						this.parseContext.Error = !ImapResponse.SafeConvert(match, 2, out num);
						if (!this.parseContext.Error)
						{
							resultData.Mailboxes[0].UidNext = new long?((long)((ulong)num));
						}
						else
						{
							resultData.FailureException = ImapResponse.Fail("Invalid STATUS UIDNEXT Response", command, match.ToString());
						}
					}
					else
					{
						resultData.FailureException = ImapResponse.Fail("Invalid STATUS Response", command, match.ToString());
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008DA4 File Offset: 0x00006FA4
		private bool FetchResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			bool flag = this.SimpleResponseProcessor(command, resultData);
			if (!flag && !this.parseContext.Error)
			{
				string currentLine = this.parseContext.CurrentLine;
				if (ImapResponse.IsUntaggedResponseLine(currentLine))
				{
					Match match;
					if (ImapResponse.CheckResponse(currentLine, ImapResponse.FetchResponse, out match))
					{
						this.ProcessFetch(command, match, resultData);
						flag = true;
					}
					else if (!ImapResponse.IsUntaggedResponseLine(currentLine))
					{
						resultData.FailureException = ImapResponse.Fail("Unexpected response", command, currentLine);
						this.parseContext.Error = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008E24 File Offset: 0x00007024
		private bool SearchResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			bool flag = this.SimpleResponseProcessor(command, resultData);
			if (!flag && !this.parseContext.Error)
			{
				string currentLine = this.parseContext.CurrentLine;
				Match match;
				if (ImapResponse.IsUntaggedResponseLine(currentLine) && ImapResponse.CheckResponse(currentLine, ImapResponse.SearchResponse, out match))
				{
					string text;
					if (ImapResponse.SafeGet(match, 1, out text))
					{
						string[] array = text.Split(new char[]
						{
							' '
						});
						foreach (string item in array)
						{
							resultData.MessageUids.Add(item);
						}
						this.log.Debug("SearchResponseProcessor parsed {0} MessageUids from SEARCH response", new object[]
						{
							array.Length
						});
						flag = true;
					}
					else
					{
						resultData.FailureException = ImapResponse.Fail("Invalid Search response", command, currentLine);
						this.parseContext.Error = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008F14 File Offset: 0x00007114
		private bool ListResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			bool flag = this.SimpleResponseProcessor(command, resultData);
			if (!flag && !this.parseContext.Error)
			{
				string currentLine = this.parseContext.CurrentLine;
				Match match;
				if (ImapResponse.IsUntaggedResponseLine(currentLine) && ImapResponse.CheckResponse(currentLine, ImapResponse.ListResponse, out match))
				{
					string text;
					string text2;
					string text3;
					if (ImapResponse.SafeGet(match, 1, out text) && ImapResponse.SafeGet(match, 2, out text2) && ImapResponse.SafeGet(match, 3, out text3))
					{
						string text4 = text.ToUpperInvariant();
						char? separator = null;
						if (string.IsNullOrEmpty(text3))
						{
							resultData.FailureException = ImapResponse.Fail("Invalid List response. Empty mailbox name.", command, currentLine);
							this.parseContext.Error = true;
							return true;
						}
						text3 = this.ConvertQuotedStringIfRequired(text3);
						if (string.IsNullOrEmpty(text3))
						{
							resultData.FailureException = ImapResponse.Fail("Invalid List response. Could not convert the mailbox name.", command, currentLine);
							this.parseContext.Error = true;
							return true;
						}
						if (text2.Length == 1)
						{
							separator = new char?(text2[0]);
						}
						else if (text2.Length == 2 && text2[0] == '\\')
						{
							separator = new char?(text2[1]);
						}
						if (separator != null && text3[text3.Length - 1] == separator.Value)
						{
							return true;
						}
						if (ImapResponse.LiteralDelimiter.IsMatch(text3))
						{
							if (!this.parseContext.MoveNextLiterals())
							{
								resultData.FailureException = ImapResponse.Fail("Failed to read literal mailbox name", command, currentLine);
								this.parseContext.Error = true;
								return true;
							}
							Stream currentLiteral = this.parseContext.CurrentLiteral;
							currentLiteral.Position = 0L;
							using (StreamReader streamReader = new StreamReader(currentLiteral))
							{
								text3 = streamReader.ReadToEnd();
							}
							this.log.Debug("Folder received as literal: {0} => {1}", new object[]
							{
								currentLine,
								text3
							});
						}
						ImapMailbox imapMailbox = new ImapMailbox(text3);
						imapMailbox.Separator = separator;
						if (text4.Contains("\\NOSELECT"))
						{
							imapMailbox.IsSelectable = false;
						}
						if (text4.Contains("\\HASNOCHILDREN"))
						{
							imapMailbox.HasChildren = new bool?(false);
						}
						if (text4.Contains("\\HASCHILDREN"))
						{
							imapMailbox.HasChildren = new bool?(true);
						}
						if (text4.Contains("\\NOINFERIORS") || text2 == "NIL")
						{
							imapMailbox.NoInferiors = true;
							imapMailbox.HasChildren = new bool?(false);
						}
						resultData.Mailboxes.Add(imapMailbox);
						flag = true;
					}
					else
					{
						resultData.FailureException = ImapResponse.Fail("Invalid List response. Could not parse it.", command, currentLine);
						this.parseContext.Error = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000091CC File Offset: 0x000073CC
		private bool AppendResponseProcessor(ImapCommand command, ImapResultData resultData)
		{
			bool flag = this.SimpleResponseProcessor(command, resultData);
			if (!flag && !this.parseContext.Error)
			{
				string currentLine = this.parseContext.CurrentLine;
				Match match;
				if (currentLine.StartsWith(command.CommandId, StringComparison.OrdinalIgnoreCase) && ImapResponse.CheckResponse(currentLine, ImapResponse.AppendUidResponse, out match))
				{
					uint num;
					this.parseContext.Error = !ImapResponse.SafeConvert(match, 2, out num);
					if (!this.parseContext.Error)
					{
						resultData.MessageUids.Add(num.ToString());
						this.log.Debug("AppendResponseProcessor parsed uidMessage {0} from APPENDUID response", new object[]
						{
							num
						});
					}
					else
					{
						this.log.Info("Invalid APPENDUID uidMessage {0}. Ignoring response.", new object[]
						{
							num
						});
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000092AC File Offset: 0x000074AC
		private ImapResponse.TryProcessLine GetLineProcessor(ImapCommand command)
		{
			ImapCommandType commandType = command.CommandType;
			ImapResponse.TryProcessLine result;
			switch (commandType)
			{
			case ImapCommandType.None:
			case ImapCommandType.Noop:
			case ImapCommandType.CreateMailbox:
			case ImapCommandType.DeleteMailbox:
			case ImapCommandType.RenameMailbox:
			case ImapCommandType.Store:
			case ImapCommandType.Expunge:
			case ImapCommandType.Id:
			case ImapCommandType.Starttls:
			case ImapCommandType.Authenticate:
				result = new ImapResponse.TryProcessLine(this.SimpleResponseProcessor);
				break;
			case ImapCommandType.Login:
			case ImapCommandType.Capability:
				result = new ImapResponse.TryProcessLine(this.CapabilityResponseProcessor);
				break;
			case ImapCommandType.Logout:
				result = new ImapResponse.TryProcessLine(this.LogoutResponseProcessor);
				break;
			case ImapCommandType.Select:
				result = new ImapResponse.TryProcessLine(this.SelectResponseProcessor);
				break;
			case ImapCommandType.Fetch:
			{
				IList<string> list = (IList<string>)command.CommandParameters[3];
				this.fetchMessageIds = list.Contains("BODY.PEEK[HEADER.FIELDS (Message-ID)]");
				this.fetchMessageSizes = list.Contains("RFC822.SIZE");
				this.fetchMessageInternalDates = list.Contains("INTERNALDATE");
				result = new ImapResponse.TryProcessLine(this.FetchResponseProcessor);
				break;
			}
			case ImapCommandType.Append:
				result = new ImapResponse.TryProcessLine(this.AppendResponseProcessor);
				break;
			case ImapCommandType.Search:
				result = new ImapResponse.TryProcessLine(this.SearchResponseProcessor);
				break;
			case ImapCommandType.List:
				result = new ImapResponse.TryProcessLine(this.ListResponseProcessor);
				break;
			case ImapCommandType.Status:
				result = new ImapResponse.TryProcessLine(this.StatusResponseProcessor);
				break;
			default:
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unknown command type {0}, no line processor.", new object[]
				{
					commandType
				}));
			}
			return result;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00009420 File Offset: 0x00007620
		private void ProcessFetch(ImapCommand command, Match match, ImapResultData resultData)
		{
			string text;
			string text2;
			if (ImapResponse.SafeGet(match, 1, out text) && ImapResponse.SafeGet(match, 2, out text2))
			{
				text2 = text2.Trim();
				if (text2.StartsWith("(", StringComparison.Ordinal))
				{
					if (!resultData.MessageSeqNumsHashSet.Contains(text))
					{
						int num;
						if (int.TryParse(text, out num))
						{
							resultData.MessageSeqNumsHashSet.Add(text);
							resultData.MessageSeqNums.Add(num);
							resultData.UidAlreadySeen = false;
							if (text2.EndsWith(")", StringComparison.Ordinal))
							{
								this.ProcessFetchResults(command, text2, resultData);
							}
							else
							{
								StringBuilder cachedBuilder = this.parseContext.CachedBuilder;
								cachedBuilder.Length = 0;
								cachedBuilder.Append(text2);
								bool flag;
								do
								{
									if (this.parseContext.MoveNextLines())
									{
										string currentLine = this.parseContext.CurrentLine;
										cachedBuilder.Append(" ");
										cachedBuilder.Append(currentLine);
										flag = currentLine.EndsWith(")", StringComparison.Ordinal);
									}
									else
									{
										resultData.FailureException = ImapResponse.Fail("Read past end of response", command, cachedBuilder.ToString());
										this.parseContext.Error = true;
										flag = true;
									}
								}
								while (!flag);
								this.ProcessFetchResults(command, cachedBuilder.ToString(), resultData);
							}
							if (!this.parseContext.Error && (resultData.LowestSequenceNumber == null || num < resultData.LowestSequenceNumber))
							{
								resultData.LowestSequenceNumber = new int?(num);
							}
						}
						else
						{
							resultData.FailureException = ImapResponse.Fail("Invalid FETCH data, cannot parse the sequence number.", command, text2.ToString());
							this.parseContext.Error = true;
						}
					}
					else
					{
						this.log.Error("Invalid FETCH data, already processed a line with the same message sequence number: {0}, command: {1}, fetchContents: {2}", new object[]
						{
							text,
							command.ToPiiCleanString(),
							text2
						});
					}
				}
				else
				{
					this.parseContext.Error = true;
					resultData.FailureException = ImapResponse.Fail("Missing FETCH data", command, text2);
				}
			}
			else
			{
				this.parseContext.Error = true;
				resultData.FailureException = ImapResponse.Fail("Parse error", command, match.ToString());
			}
			if (resultData.FailureException != null)
			{
				this.log.Error("Error encountered in ProcessFetch: {0}", new object[]
				{
					resultData.FailureException
				});
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000966C File Offset: 0x0000786C
		private void ProcessFetchResults(ImapCommand command, string fetchResults, ImapResultData resultData)
		{
			int count = resultData.MessageUids.Count;
			int num = 0;
			string text = null;
			StringBuilder cachedBuilder = this.parseContext.CachedBuilder;
			cachedBuilder.Length = 0;
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < fetchResults.Length; i++)
			{
				char c = fetchResults[i];
				if (!flag3 && (c == '(' || c == '['))
				{
					stringBuilder.Append(c);
					flag2 = false;
					if (num > 0)
					{
						cachedBuilder.Append(c);
					}
					num++;
				}
				else if (!flag3 && (c == ')' || c == ']'))
				{
					stringBuilder.Append(c);
					flag2 = false;
					num--;
					if (num > 0)
					{
						cachedBuilder.Append(c);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append(c);
					flag2 = false;
					flag3 = !flag3;
				}
				else if (!flag3 && c == ' ' && num == 1)
				{
					stringBuilder.Append(c);
					flag2 = false;
					string text2 = cachedBuilder.ToString();
					if (text == null)
					{
						if (ImapResponse.AllowWhitespaceInKey(fetchResults, i, text2))
						{
							cachedBuilder.Append(c);
						}
						else
						{
							text = text2;
							cachedBuilder.Length = 0;
							flag = false;
						}
					}
					else
					{
						this.ProcessFetchKeyValueToResult(command, resultData, text, text2);
						text = null;
						flag = true;
						cachedBuilder.Length = 0;
					}
				}
				else
				{
					if (flag && cachedBuilder.Length < 4)
					{
						stringBuilder.Append(c);
					}
					else if (!flag2)
					{
						stringBuilder.Append('#');
						flag2 = true;
					}
					cachedBuilder.Append(c);
				}
			}
			if (text != null)
			{
				string value = cachedBuilder.ToString();
				this.ProcessFetchKeyValueToResult(command, resultData, text, value);
			}
			if (num != 0)
			{
				this.parseContext.Error = true;
				resultData.FailureException = ImapResponse.Fail("Unbalanced FETCH response", command, fetchResults);
			}
			if (this.fetchMessageIds && resultData.MessageIds.Count == count)
			{
				this.log.Debug("No MessageId found in this line of the FETCH result: {0}", new object[]
				{
					stringBuilder.ToString()
				});
				resultData.MessageIds.Add(null);
			}
			if (this.fetchMessageSizes && resultData.MessageSizes.Count == count)
			{
				resultData.MessageSizes.Add(0L);
			}
			if (resultData.MessageUids.Count == count)
			{
				this.log.Error("No UID element found (which is mandatory) in this line of the FETCH result: {0}", new object[]
				{
					stringBuilder.ToString()
				});
				resultData.MessageUids.Add(null);
			}
			if (resultData.MessageFlags.Count == count)
			{
				resultData.MessageFlags.Add(ImapMailFlags.None);
			}
			if (this.fetchMessageInternalDates && resultData.MessageInternalDates.Count == count)
			{
				this.log.Debug("No INTERNALDATE found in this line of the FETCH result: {0}", new object[]
				{
					stringBuilder.ToString()
				});
				resultData.MessageInternalDates.Add(null);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000993C File Offset: 0x00007B3C
		private void ProcessFetchKeyValueToResult(ImapCommand command, ImapResultData resultData, string key, string value)
		{
			IList<string> list = (IList<string>)command.CommandParameters[3];
			if (list != null && (list.Contains(key) || ImapResponse.HeaderFetch.IsMatch(key) || ImapResponse.BodyKeyRegex.IsMatch(key)))
			{
				if ("UID".Equals(key, StringComparison.OrdinalIgnoreCase) && !resultData.UidAlreadySeen)
				{
					resultData.MessageUids.Add(value);
					resultData.UidAlreadySeen = true;
					return;
				}
				if ("RFC822.SIZE".Equals(key, StringComparison.OrdinalIgnoreCase))
				{
					long item = 0L;
					if (!long.TryParse(value, out item))
					{
						resultData.FailureException = ImapResponse.Fail("Could not parse message size.", command, key);
						this.parseContext.Error = true;
					}
					resultData.MessageSizes.Add(item);
					return;
				}
				if ("FLAGS".Equals(key, StringComparison.OrdinalIgnoreCase))
				{
					resultData.MessageFlags.Add(ImapUtilities.ConvertStringFormToFlags(value));
					return;
				}
				if ("INTERNALDATE".Equals(key, StringComparison.OrdinalIgnoreCase))
				{
					DateTime dateTime = MailUtilities.ToDateTime(value);
					resultData.MessageInternalDates.Add(new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, dateTime)));
					return;
				}
				Match match;
				if (ImapResponse.BodyKeyRegex.IsMatch(key))
				{
					if (this.parseContext.MoveNextLiterals())
					{
						resultData.MessageStream = this.parseContext.CurrentLiteral;
						return;
					}
					resultData.FailureException = ImapResponse.Fail("More literal references than literals", command, key);
					this.parseContext.Error = true;
					return;
				}
				else if (ImapResponse.CheckResponse(key, ImapResponse.HeaderFetch, out match))
				{
					this.ProcessFetchHeaderDataToResult(command, resultData, key);
					return;
				}
			}
			else
			{
				this.log.Debug("Unexpected token while processing FETCH data: {0} = {1}", new object[]
				{
					key,
					value
				});
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00009AD8 File Offset: 0x00007CD8
		private void ProcessFetchHeaderDataToResult(ImapCommand command, ImapResultData resultData, string key)
		{
			if (!this.parseContext.MoveNextLiterals() || this.parseContext.CurrentLiteral == null)
			{
				this.log.Debug("No message id literal for command: {0}, key: {1}", new object[]
				{
					command,
					key
				});
				return;
			}
			Stream currentLiteral = this.parseContext.CurrentLiteral;
			currentLiteral.Position = 0L;
			string text = null;
			bool flag = false;
			this.responseBuffer.Reset();
			int num;
			while ((num = currentLiteral.ReadByte()) != -1)
			{
				if (flag && num == 10 && text != null && this.responseBuffer.Length > 0)
				{
					string @string = Encoding.ASCII.GetString(this.responseBuffer.GetBuffer(), 0, this.responseBuffer.Length);
					if ("Message-ID".Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						resultData.MessageIds.Add(@string.Trim());
					}
					else
					{
						this.log.Debug("Ignoring header field: {0}: {1}", new object[]
						{
							text,
							@string.Trim()
						});
					}
					text = null;
					this.responseBuffer.Reset();
				}
				if (num == 13)
				{
					flag = true;
				}
				else
				{
					flag = false;
					if (num == 58 && text == null && this.responseBuffer.Length > 0)
					{
						text = Encoding.ASCII.GetString(this.responseBuffer.GetBuffer(), 0, this.responseBuffer.Length);
						this.responseBuffer.Reset();
					}
					else if ((num != 32 && num != 9) || this.responseBuffer.Length != 0)
					{
						this.responseBuffer.Append((byte)num);
					}
				}
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00009C74 File Offset: 0x00007E74
		private string ConvertQuotedStringIfRequired(string incoming)
		{
			if (incoming != null && incoming.Length > 1 && incoming[0] == '"' && incoming[incoming.Length - 1] == '"')
			{
				StringBuilder cachedBuilder = this.parseContext.CachedBuilder;
				cachedBuilder.Length = 0;
				for (int i = 1; i < incoming.Length - 1; i++)
				{
					if (incoming[i] == '\\')
					{
						i++;
						if (i == incoming.Length - 1)
						{
							return null;
						}
					}
					cachedBuilder.Append(incoming[i]);
				}
				if (!cachedBuilder.ToString().Equals(incoming))
				{
					this.log.Debug("Incoming string {0} converted to {1}", new object[]
					{
						incoming,
						cachedBuilder.ToString()
					});
				}
				return cachedBuilder.ToString();
			}
			return incoming;
		}

		// Token: 0x040000A9 RID: 169
		internal const string UIDValidityKey = "UIDVALIDITY";

		// Token: 0x040000AA RID: 170
		internal const string UIDNextKey = "UIDNEXT";

		// Token: 0x040000AB RID: 171
		internal const string UIDKey = "UID";

		// Token: 0x040000AC RID: 172
		internal const string MessageSizeEstimateKey = "RFC822.SIZE";

		// Token: 0x040000AD RID: 173
		internal const string FlagsKey = "FLAGS";

		// Token: 0x040000AE RID: 174
		internal const string InternalDateKey = "INTERNALDATE";

		// Token: 0x040000AF RID: 175
		internal const string Body = "BODY";

		// Token: 0x040000B0 RID: 176
		internal const string BodyKey = "BODY.PEEK[]";

		// Token: 0x040000B1 RID: 177
		internal const string BodyHeaderFieldsMessageId = "BODY.PEEK[HEADER.FIELDS (Message-ID)]";

		// Token: 0x040000B2 RID: 178
		private const string OkResponsePrefix = "OK";

		// Token: 0x040000B3 RID: 179
		private const string NoResponsePrefix = "NO";

		// Token: 0x040000B4 RID: 180
		private const string BadResponsePrefix = "BAD";

		// Token: 0x040000B5 RID: 181
		private const string NonSelectableFolderFlag = "\\NOSELECT";

		// Token: 0x040000B6 RID: 182
		private const string HasChildrenFolderFlag = "\\HASCHILDREN";

		// Token: 0x040000B7 RID: 183
		private const string HasNoChildrenFolderFlag = "\\HASNOCHILDREN";

		// Token: 0x040000B8 RID: 184
		private const string NoInferiorsFolderFlag = "\\NOINFERIORS";

		// Token: 0x040000B9 RID: 185
		private const string MessageIdHeader = "Message-ID";

		// Token: 0x040000BA RID: 186
		private const string ReadWriteFlag = "READ-WRITE";

		// Token: 0x040000BB RID: 187
		private const int DefaultBufferSize = 1024;

		// Token: 0x040000BC RID: 188
		private const int DefaultLineCollectionSize = 20;

		// Token: 0x040000BD RID: 189
		private const int DefaultLiteralCollectionSize = 1;

		// Token: 0x040000BE RID: 190
		private const char ServerLiteralRequestIndicator = '+';

		// Token: 0x040000BF RID: 191
		private const char TokenDelimiter = ' ';

		// Token: 0x040000C0 RID: 192
		private const int MaxKeyCharactersLogged = 4;

		// Token: 0x040000C1 RID: 193
		private static readonly Regex OkDataResponse = new Regex("\\* OK \\[(.*)\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000C2 RID: 194
		private static readonly Regex UnsolicitedOkResponse = new Regex("\\* OK(.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000C3 RID: 195
		private static readonly Regex UnsolicitedNoResponse = new Regex("\\* NO(.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000C4 RID: 196
		private static readonly Regex UnsolicitedBadResponse = new Regex("\\* BAD(.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000C5 RID: 197
		private static readonly Regex UnsolicitedByeResponse = new Regex("\\* BYE(.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000C6 RID: 198
		private static readonly Regex FetchResponse = new Regex("\\* ([0-9]+) FETCH (.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000C7 RID: 199
		private static readonly Regex StatusResponse = new Regex("\\* {1,2}STATUS (.*) \\(UIDNEXT ([0-9]+)\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000C8 RID: 200
		private static readonly Regex ListResponse = new Regex("\\* LIST \\((.*)\\) \"?(NIL|\\\\?[^ \"]?)\"? (.+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000C9 RID: 201
		private static readonly Regex SearchResponse = new Regex("\\* SEARCH (.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000CA RID: 202
		private static readonly Regex AppendUidResponse = new Regex(".+ .+ \\[APPENDUID ([0-9]+) ([0-9]+)\\].*", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000CB RID: 203
		private static readonly Regex ExistsResponse = new Regex("\\* ([0-9]+) EXISTS", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000CC RID: 204
		private static readonly Regex CapabilityResponse = new Regex("\\* CAPABILITY (.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000CD RID: 205
		private static readonly Regex UidValidity = new Regex("UIDVALIDITY ([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000CE RID: 206
		private static readonly Regex UidNext = new Regex("UIDNEXT ([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000CF RID: 207
		private static readonly Regex StatusUidNext = new Regex("UIDNEXT \\(([0-9]+)\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000D0 RID: 208
		private static readonly Regex PermanentFlags = new Regex("PERMANENTFLAGS \\((.*)\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000D1 RID: 209
		private static readonly Regex LiteralDelimiter = new Regex("{([0-9]+)}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000D2 RID: 210
		private static readonly Regex BodyKeyRegex = new Regex("BODY\\s*\\[\\s*\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000D3 RID: 211
		private static readonly Regex HeaderFetch = new Regex("BODY\\s*\\[HEADER.FIELDS \\((.+)\\)\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000D4 RID: 212
		private readonly List<Stream> responseLiterals;

		// Token: 0x040000D5 RID: 213
		private readonly BufferBuilder responseBuffer;

		// Token: 0x040000D6 RID: 214
		private readonly List<string> responseLines;

		// Token: 0x040000D7 RID: 215
		private readonly ImapResponse.ParseContext parseContext;

		// Token: 0x040000D8 RID: 216
		private readonly ILog log;

		// Token: 0x040000D9 RID: 217
		private string commandId;

		// Token: 0x040000DA RID: 218
		private byte lastByteRead;

		// Token: 0x040000DB RID: 219
		private Stream inFlightLiteral;

		// Token: 0x040000DC RID: 220
		private bool checkForUnsolicitedByeResponse;

		// Token: 0x040000DD RID: 221
		private int curLiteralBytes;

		// Token: 0x040000DE RID: 222
		private bool fetchMessageIds;

		// Token: 0x040000DF RID: 223
		private bool fetchMessageInternalDates;

		// Token: 0x040000E0 RID: 224
		private bool fetchMessageSizes;

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000182 RID: 386
		private delegate bool TryProcessLine(ImapCommand command, ImapResultData resultData);

		// Token: 0x02000014 RID: 20
		private sealed class ParseContext
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000185 RID: 389 RVA: 0x00009ECD File Offset: 0x000080CD
			// (set) Token: 0x06000186 RID: 390 RVA: 0x00009ED5 File Offset: 0x000080D5
			internal bool Error { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000187 RID: 391 RVA: 0x00009EDE File Offset: 0x000080DE
			internal string CurrentLine
			{
				get
				{
					return this.lineItr.Current;
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000188 RID: 392 RVA: 0x00009EEB File Offset: 0x000080EB
			internal Stream CurrentLiteral
			{
				get
				{
					return this.literalItr.Current;
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000189 RID: 393 RVA: 0x00009EF8 File Offset: 0x000080F8
			internal StringBuilder CachedBuilder
			{
				get
				{
					return this.cachedBuilder;
				}
			}

			// Token: 0x0600018A RID: 394 RVA: 0x00009F00 File Offset: 0x00008100
			internal void Reset(IEnumerable<string> responseLines, IEnumerable<Stream> responseLiterals)
			{
				this.lineItr = responseLines.GetEnumerator();
				this.literalItr = responseLiterals.GetEnumerator();
				this.Error = false;
				this.cachedBuilder.Length = 0;
			}

			// Token: 0x0600018B RID: 395 RVA: 0x00009F2D File Offset: 0x0000812D
			internal bool MoveNextLines()
			{
				return this.lineItr.MoveNext();
			}

			// Token: 0x0600018C RID: 396 RVA: 0x00009F3A File Offset: 0x0000813A
			internal bool MoveNextLiterals()
			{
				return this.literalItr.MoveNext();
			}

			// Token: 0x040000E5 RID: 229
			private readonly StringBuilder cachedBuilder = new StringBuilder();

			// Token: 0x040000E6 RID: 230
			private IEnumerator<string> lineItr;

			// Token: 0x040000E7 RID: 231
			private IEnumerator<Stream> literalItr;
		}
	}
}
