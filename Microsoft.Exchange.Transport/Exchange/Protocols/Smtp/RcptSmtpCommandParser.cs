using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.ExSmtpClient;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200048C RID: 1164
	internal static class RcptSmtpCommandParser
	{
		// Token: 0x06003528 RID: 13608 RVA: 0x000D84B0 File Offset: 0x000D66B0
		public static ParseResult Parse(CommandContext commandContext, SmtpInSessionState sessionState, bool isDataRedactionNecessary, ISmtpReceiveConfiguration smtpReceiveConfiguration, bool acceptAnyRecipient, out RcptParseOutput rcptParseOutput)
		{
			ArgumentValidator.ThrowIfNull("CommandContext", commandContext);
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ArgumentValidator.ThrowIfNull("smtpReceiveConfiguration", smtpReceiveConfiguration);
			ArgumentValidator.ThrowIfNull("sessionState.AdvertisedEhloOptions", sessionState.AdvertisedEhloOptions);
			ArgumentValidator.ThrowIfNull("sessionState.ReceiveConnector", sessionState.ReceiveConnector);
			rcptParseOutput = new RcptParseOutput();
			int num;
			RoutingAddress routingAddress;
			string text;
			ParseResult result = RcptSmtpCommandParser.TryParseAddress(commandContext, sessionState.AdvertisedDomain, (sessionState.ReceiveConnector.DefaultDomain == null) ? null : sessionState.ReceiveConnector.DefaultDomain.ToString(), sessionState.AdvertisedEhloOptions.XLongAddr, sessionState.SmtpUtf8Supported, out num, out routingAddress, out text);
			if (result.IsFailed)
			{
				commandContext.LogReceivedCommand(sessionState.ProtocolLogSession);
				return result;
			}
			Dictionary<string, string> consumerMailOptionalArguments;
			DsnRequestedFlags notify;
			string orcpt;
			RoutingAddress routingAddress2;
			string text2;
			byte[] bytes;
			result = RcptSmtpCommandParser.ParseOptionalArguments(commandContext, sessionState.AdvertisedEhloOptions.XLongAddr, sessionState.SmtpUtf8Supported, sessionState.TlsDomainCapabilities, out consumerMailOptionalArguments, out notify, out orcpt, out routingAddress2, out text2, out bytes);
			if (result.IsFailed)
			{
				commandContext.LogReceivedCommand(sessionState.ProtocolLogSession);
				rcptParseOutput.ConsumerMailOptionalArguments.Clear();
				return result;
			}
			if (isDataRedactionNecessary)
			{
				byte[] array = new byte[num - commandContext.OriginalOffset];
				Buffer.BlockCopy(commandContext.Command, commandContext.OriginalOffset, array, 0, array.Length);
				sessionState.ProtocolLogSession.LogReceive(Util.CreateRedactedCommand(array, routingAddress, ByteString.BytesToString(bytes, true), true));
			}
			else
			{
				commandContext.LogReceivedCommand(sessionState.ProtocolLogSession);
			}
			rcptParseOutput.RecipientAddress = routingAddress;
			if (!SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(sessionState.CombinedPermissions) && sessionState.TransportMailItem.Recipients.Count >= sessionState.ReceiveConnector.MaxRecipientsPerMessage)
			{
				rcptParseOutput.TooManyRecipientsResponseCount++;
				if (sessionState.AdvertisedEhloOptions.Pipelining && rcptParseOutput.TooManyRecipientsResponseCount <= 1000)
				{
					rcptParseOutput.LowAuthenticationLevelTarpitOverride = TarpitAction.DoNotTarpit;
				}
				return ParseResult.TooManyRecipients;
			}
			if (!SmtpInSessionUtils.HasSMTPAcceptAnyRecipientPermission(sessionState.CombinedPermissions) && !smtpReceiveConfiguration.TransportConfiguration.FirstOrgAcceptedDomainTable.CheckAccepted(SmtpDomain.GetDomainPart(routingAddress)) && !acceptAnyRecipient)
			{
				sessionState.Tracer.TraceDebug(0L, "Rejected recipient domain (relay)");
				return ParseResult.RcptRelayNotPermitted;
			}
			rcptParseOutput.Notify = notify;
			rcptParseOutput.ORcpt = orcpt;
			if (routingAddress2 != RoutingAddress.Empty)
			{
				if (!sessionState.AdvertisedEhloOptions.XOrar || (Util.IsLongAddress(routingAddress2) && !sessionState.AdvertisedEhloOptions.XLongAddr))
				{
					return ParseResult.InvalidArguments;
				}
				if (!SmtpInSessionUtils.HasSMTPAcceptOrarPermission(sessionState.CombinedPermissions))
				{
					return ParseResult.OrarNotAuthorized;
				}
				rcptParseOutput.Orar = routingAddress2;
			}
			if (text2 != null)
			{
				if (!sessionState.AdvertisedEhloOptions.XRDst)
				{
					return ParseResult.InvalidArguments;
				}
				if (!SmtpInSessionUtils.HasSMTPAcceptRDstPermission(sessionState.CombinedPermissions))
				{
					return ParseResult.RDstNotAuthorized;
				}
				rcptParseOutput.RDst = text2;
			}
			rcptParseOutput.ConsumerMailOptionalArguments = consumerMailOptionalArguments;
			return ParseResult.ParsingComplete;
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000D8760 File Offset: 0x000D6960
		private static ParseResult TryParseAddress(CommandContext commandContext, string advertisedDomain, string receiveConnectorDefaultDomain, bool acceptLongAddresses, bool smtpUtf8Supported, out int dataOffset, out RoutingAddress address, out string tail)
		{
			dataOffset = 0;
			address = RoutingAddress.Empty;
			tail = null;
			if (!commandContext.ParseTokenAndVerifyCommand(RcptSmtpCommandParser.TO, 58))
			{
				return ParseResult.UnrecognizedParameter;
			}
			commandContext.TrimLeadingWhitespace();
			dataOffset = commandContext.Offset;
			string protocolText;
			if (!commandContext.GetCommandArguments(out protocolText))
			{
				return ParseResult.InvalidAddress;
			}
			Parse821.TryParseAddressLine(protocolText, out address, out tail);
			if (tail != null)
			{
				commandContext.PushBackOffset(ByteString.StringToBytesCount(tail, true));
			}
			if (RcptSmtpCommandParser.IsAddressPostMaster(address))
			{
				address = new RoutingAddress(RoutingAddress.PostMasterAddress.ToString(), advertisedDomain);
			}
			if (!Util.IsValidAddress(address))
			{
				if (string.IsNullOrEmpty(receiveConnectorDefaultDomain) || !string.IsNullOrEmpty(address.DomainPart))
				{
					return ParseResult.InvalidAddress;
				}
				address = new RoutingAddress(address.ToString(), receiveConnectorDefaultDomain);
				if (!Util.IsValidAddress(address))
				{
					return ParseResult.InvalidAddress;
				}
			}
			if (address == RoutingAddress.NullReversePath)
			{
				return ParseResult.InvalidAddress;
			}
			if (Util.IsLongAddress(address) && !acceptLongAddresses)
			{
				return ParseResult.LongAddress;
			}
			if (address.IsUTF8 && !smtpUtf8Supported)
			{
				return ParseResult.Utf8Address;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000D88A4 File Offset: 0x000D6AA4
		public static ParseResult ParseOptionalArguments(CommandContext commandContext, bool acceptLongAddresses, bool smtpUtf8Supported, out DsnRequestedFlags notify, out string orcpt, out RoutingAddress orar, out string rdst, out byte[] tailToLogArray, SmtpReceiveCapabilities? tlsDomainCapabilities = null)
		{
			Dictionary<string, string> dictionary;
			return RcptSmtpCommandParser.ParseOptionalArguments(commandContext, acceptLongAddresses, smtpUtf8Supported, tlsDomainCapabilities, out dictionary, out notify, out orcpt, out orar, out rdst, out tailToLogArray);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000D88C8 File Offset: 0x000D6AC8
		public static bool IsValidOrcpt(string orcpt, bool acceptLongAddresses, int maxAddrTypeLength, int maxOrcptLength, out int semicolonIndex)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("orcpt", orcpt);
			semicolonIndex = orcpt.IndexOf(';');
			return semicolonIndex > 0 && semicolonIndex < orcpt.Length - 1 && (!acceptLongAddresses || (semicolonIndex <= maxAddrTypeLength && orcpt.Length - semicolonIndex - 1 <= maxOrcptLength));
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000D891C File Offset: 0x000D6B1C
		public static ParseResult ParseOptionalArguments(CommandContext commandContext, bool acceptLongAddresses, bool smtpUtf8Supported, SmtpReceiveCapabilities? tlsDomainCapabilities, out Dictionary<string, string> consumerMailOptionalArguments, out DsnRequestedFlags notify, out string orcpt, out RoutingAddress orar, out string rdst, out byte[] tailToLogArray)
		{
			ArgumentValidator.ThrowIfNull("commandContext", commandContext);
			consumerMailOptionalArguments = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
			notify = DsnRequestedFlags.Default;
			orcpt = null;
			orar = RoutingAddress.Empty;
			rdst = null;
			tailToLogArray = null;
			List<byte> list = new List<byte>();
			byte item = Convert.ToByte(' ');
			bool flag = tlsDomainCapabilities != null && SmtpInSessionUtils.ShouldAllowConsumerMail(tlsDomainCapabilities.Value);
			commandContext.TrimLeadingWhitespace();
			while (!commandContext.IsEndOfCommand)
			{
				Offset offset;
				if (!commandContext.GetNextArgumentOffset(out offset))
				{
					return ParseResult.InvalidArguments;
				}
				int nameValuePairSeparatorIndex = CommandParsingHelper.GetNameValuePairSeparatorIndex(commandContext.Command, offset, 61);
				if (nameValuePairSeparatorIndex <= 0 || nameValuePairSeparatorIndex >= offset.End - 1)
				{
					return ParseResult.InvalidArguments;
				}
				int num = nameValuePairSeparatorIndex - offset.Start;
				int num2 = offset.End - (nameValuePairSeparatorIndex + 1);
				if (list.Any<byte>())
				{
					list.Add(item);
				}
				if (flag)
				{
					string text = SmtpUtils.FromXtextString(commandContext.Command, offset.Start, num, false);
					string value = SmtpUtils.FromXtextString(commandContext.Command, nameValuePairSeparatorIndex + 1, num2, false);
					if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(value))
					{
						return ParseResult.InvalidArguments;
					}
					if (consumerMailOptionalArguments.ContainsKey(text))
					{
						consumerMailOptionalArguments[text] = value;
					}
					else
					{
						consumerMailOptionalArguments.Add(text, value);
					}
					for (int i = offset.Start; i < offset.End; i++)
					{
						list.Add(commandContext.Command[i]);
					}
				}
				char c = (char)Util.LowerC[(int)commandContext.Command[offset.Start]];
				char c2 = c;
				switch (c2)
				{
				case 'n':
					if (!SmtpCommand.CompareArg(RcptSmtpCommandParser.NOTIFY, commandContext.Command, offset.Start, num) || notify != DsnRequestedFlags.Default || !RcptSmtpCommandParser.ParseNotify(commandContext.Command, nameValuePairSeparatorIndex + 1, num2, out notify))
					{
						goto IL_42D;
					}
					for (int j = offset.Start; j < offset.End; j++)
					{
						list.Add(commandContext.Command[j]);
					}
					break;
				case 'o':
				{
					if (!SmtpCommand.CompareArg(RcptSmtpCommandParser.ORCPT, commandContext.Command, offset.Start, num) || orcpt != null || (!acceptLongAddresses && num2 > 500))
					{
						goto IL_42D;
					}
					orcpt = SmtpUtils.FromXtextString(commandContext.Command, nameValuePairSeparatorIndex + 1, num2, smtpUtf8Supported);
					int num3;
					if (orcpt == null || !RcptSmtpCommandParser.IsValidOrcpt(orcpt, acceptLongAddresses, 500, 1860, out num3))
					{
						goto IL_42D;
					}
					for (int k = offset.Start; k <= nameValuePairSeparatorIndex; k++)
					{
						list.Add(commandContext.Command[k]);
					}
					list.AddRange(ByteString.StringToBytes(orcpt.Substring(0, num3 + 1) + Util.Redact(orcpt.Substring(num3 + 1)), smtpUtf8Supported));
					break;
				}
				default:
					if (c2 != 'x')
					{
						goto IL_42D;
					}
					if (SmtpCommand.CompareArg(RcptSmtpCommandParser.ORAR, commandContext.Command, offset.Start, num))
					{
						if (orar != RoutingAddress.Empty)
						{
							goto IL_42D;
						}
						string text2 = SmtpUtils.FromXtextString(commandContext.Command, nameValuePairSeparatorIndex + 1, num2, false);
						if (text2 == null || text2.Length > 1860)
						{
							goto IL_42D;
						}
						orar = new RoutingAddress(text2);
						if (orar == RoutingAddress.Empty || orar == RoutingAddress.NullReversePath || !Util.IsValidAddress(orar))
						{
							goto IL_42D;
						}
						for (int l = offset.Start; l <= nameValuePairSeparatorIndex; l++)
						{
							list.Add(commandContext.Command[l]);
						}
						list.AddRange(ByteString.StringToBytes(Util.Redact(orar), false));
					}
					else
					{
						if (!SmtpCommand.CompareArg(RcptSmtpCommandParser.RDST, commandContext.Command, offset.Start, num) || rdst != null || num2 > 255)
						{
							goto IL_42D;
						}
						rdst = SmtpUtils.FromXtextString(commandContext.Command, nameValuePairSeparatorIndex + 1, num2, false);
						if (string.IsNullOrEmpty(rdst) || !RoutingAddress.IsValidDomain(rdst))
						{
							goto IL_42D;
						}
						for (int m = offset.Start; m < offset.End; m++)
						{
							list.Add(commandContext.Command[m]);
						}
					}
					break;
				}
				IL_436:
				commandContext.TrimLeadingWhitespace();
				continue;
				IL_42D:
				if (!flag)
				{
					return ParseResult.InvalidArguments;
				}
				goto IL_436;
			}
			tailToLogArray = list.ToArray();
			return ParseResult.ParsingComplete;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000D8D80 File Offset: 0x000D6F80
		private static bool ParseNotify(byte[] command, int beginIndex, int argValueLen, out DsnRequestedFlags notify)
		{
			if (BufferParser.CompareArg(RcptSmtpCommandParser.NEVER, command, beginIndex, argValueLen))
			{
				notify = DsnRequestedFlags.Never;
				return true;
			}
			notify = DsnRequestedFlags.Default;
			int num3;
			for (int num = beginIndex; num != -1; num = ((num3 != -1) ? (num3 + 1) : -1))
			{
				int num2 = beginIndex + argValueLen - num;
				num3 = Array.IndexOf<byte>(command, 44, num, num2);
				int count;
				if (num3 == -1)
				{
					count = num2;
				}
				else
				{
					if (num3 == beginIndex + argValueLen - 1)
					{
						return false;
					}
					count = num3 - num;
				}
				char c = (char)Util.LowerC[(int)command[num]];
				char c2 = c;
				switch (c2)
				{
				case 'd':
					if (!BufferParser.CompareArg(RcptSmtpCommandParser.DELAY, command, num, count))
					{
						return false;
					}
					notify |= DsnRequestedFlags.Delay;
					break;
				case 'e':
					return false;
				case 'f':
					if (!BufferParser.CompareArg(RcptSmtpCommandParser.FAILURE, command, num, count))
					{
						return false;
					}
					notify |= DsnRequestedFlags.Failure;
					break;
				default:
					if (c2 != 's')
					{
						return false;
					}
					if (!BufferParser.CompareArg(RcptSmtpCommandParser.SUCCESS, command, num, count))
					{
						return false;
					}
					notify |= DsnRequestedFlags.Success;
					break;
				}
			}
			return true;
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000D8E60 File Offset: 0x000D7060
		private static bool IsAddressPostMaster(RoutingAddress address)
		{
			return address.Equals(RoutingAddress.PostMasterAddress) || (RoutingAddress.IsDomainIPLiteral(address.DomainPart) && string.Equals(address.LocalPart, RoutingAddress.PostMasterAddress.ToString(), StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x04001B03 RID: 6915
		public const string CommandKeyword = "RCPT";

		// Token: 0x04001B04 RID: 6916
		private const int MaxPipelineToleranceForTooManyRecipients = 1000;

		// Token: 0x04001B05 RID: 6917
		public const int MaxORcptLen = 500;

		// Token: 0x04001B06 RID: 6918
		private static readonly byte[] ORCPT = Util.AsciiStringToBytes("orcpt");

		// Token: 0x04001B07 RID: 6919
		private static readonly byte[] NOTIFY = Util.AsciiStringToBytes("notify");

		// Token: 0x04001B08 RID: 6920
		private static readonly byte[] ORAR = Util.AsciiStringToBytes("xorar");

		// Token: 0x04001B09 RID: 6921
		private static readonly byte[] RDST = Util.AsciiStringToBytes("xrdst");

		// Token: 0x04001B0A RID: 6922
		private static readonly byte[] NEVER = Util.AsciiStringToBytes("never");

		// Token: 0x04001B0B RID: 6923
		private static readonly byte[] SUCCESS = Util.AsciiStringToBytes("success");

		// Token: 0x04001B0C RID: 6924
		private static readonly byte[] FAILURE = Util.AsciiStringToBytes("failure");

		// Token: 0x04001B0D RID: 6925
		private static readonly byte[] DELAY = Util.AsciiStringToBytes("delay");

		// Token: 0x04001B0E RID: 6926
		private static readonly byte[] TO = Util.AsciiStringToBytes("to");
	}
}
