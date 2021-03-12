using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200047F RID: 1151
	internal static class MailSmtpCommandParser
	{
		// Token: 0x060034D4 RID: 13524 RVA: 0x000D6E50 File Offset: 0x000D5050
		public static ParseResult Parse(CommandContext commandContext, SmtpInSessionState sessionState, out MailParseOutput parseOutput)
		{
			ArgumentValidator.ThrowIfNull("commandContext", commandContext);
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			RoutingAddress fromAddress;
			byte[] data;
			string value;
			SmtpResponse smtpResponse;
			bool flag = MailSmtpCommandParser.TryParseAddress(commandContext, sessionState, sessionState.ServerState.IsDataRedactionNecessary, out fromAddress, out data, out value, out smtpResponse);
			sessionState.ProtocolLogSession.LogReceive(data);
			if (!flag)
			{
				parseOutput = null;
				return new ParseResult(ParsingStatus.ProtocolError, smtpResponse, false);
			}
			parseOutput = new MailParseOutput(fromAddress);
			if (string.IsNullOrEmpty(value))
			{
				return ParseResult.ParsingComplete;
			}
			commandContext.PushBackOffset(ByteString.StringToBytesCount(value, true));
			return MailSmtpCommandParser.ParseOptionalArgumentsInternal(commandContext, sessionState, sessionState.Tracer, sessionState.Configuration.TransportConfiguration.MailSmtpCommandConfig, parseOutput);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000D6EF0 File Offset: 0x000D50F0
		public static ParseResult Parse(CommandContext context, SmtpInSessionState sessionState, ICertificateValidator certificateValidator, bool isDataRedactionNecessary, MailSmtpCommandParser.ValidateSequence validateSequence, MailSmtpCommandParser.UpdateState updateState, MailSmtpCommandParser.IsPoisonMessage isMessagePoison, IExEventLog eventLogger, MailSmtpCommandParser.PublishNotification publishNotification, ref bool shouldDisconnect, ref TimeSpan tarpitInterval, out RoutingAddress fromAddress, out MailParseOutput parseOutput)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ArgumentValidator.ThrowIfNull("certificateValidator", certificateValidator);
			ArgumentValidator.ThrowIfNull("validateSequence", validateSequence);
			ArgumentValidator.ThrowIfNull("updateState", updateState);
			ArgumentValidator.ThrowIfNull("isMessagePoison", isMessagePoison);
			ArgumentValidator.ThrowIfNull("eventLogger", eventLogger);
			ArgumentValidator.ThrowIfNull("publishNotification", publishNotification);
			parseOutput = null;
			byte[] data;
			string text;
			SmtpResponse smtpResponse;
			bool flag = MailSmtpCommandParser.TryParseAddress(context, sessionState, isDataRedactionNecessary, out fromAddress, out data, out text, out smtpResponse);
			sessionState.ProtocolLogSession.LogReceive(data);
			ParseResult result = validateSequence();
			if (result.IsFailed)
			{
				return result;
			}
			if (!flag)
			{
				return new ParseResult(ParsingStatus.ProtocolError, smtpResponse, false);
			}
			if (!MailCommandHelpers.ValidateSecureState(sessionState))
			{
				shouldDisconnect = true;
				return ParseResult.RequireTlsToSendMail;
			}
			if (sessionState.TransportMailItem != null)
			{
				return ParseResult.MailFromAlreadySpecified;
			}
			updateState(sessionState);
			if (MailCommandHelpers.HasMessageRateLimitExceeded(sessionState))
			{
				shouldDisconnect = true;
				return ParseResult.MessageRateLimitExceeded;
			}
			SmtpResponse smtpResponse2;
			if (CommandParsingHelper.ShouldRejectMailItem(fromAddress, sessionState, false, out smtpResponse2))
			{
				return new ParseResult(ParsingStatus.Error, smtpResponse2, false);
			}
			SmtpResponse smtpResponse3;
			if (!MailCommandHelpers.CanSubmitMessage(fromAddress, sessionState, out smtpResponse3))
			{
				if (string.Equals(SmtpResponse.UnableToAcceptAnonymousSession.ToString(), smtpResponse3.ToString()) || string.Equals(SmtpResponse.SubmitDenied.ToString(), smtpResponse3.ToString()))
				{
					shouldDisconnect = true;
				}
				else if (string.Equals(SmtpResponse.NotAuthenticated.ToString(), smtpResponse3.ToString()) && tarpitInterval < MailSmtpCommandParser.BadCertificateTarpitInterval)
				{
					tarpitInterval = MailSmtpCommandParser.BadCertificateTarpitInterval;
				}
				return new ParseResult(ParsingStatus.ProtocolError, smtpResponse3, false);
			}
			if (text != null)
			{
				context.PushBackOffset(ByteString.StringToBytesCount(text, true));
			}
			parseOutput = new MailParseOutput(fromAddress);
			result = MailSmtpCommandParser.ParseOptionalArgumentsInternal(context, sessionState, sessionState.Tracer, sessionState.Configuration.TransportConfiguration.MailSmtpCommandConfig, parseOutput);
			if (result.IsFailed)
			{
				parseOutput.ConsumerMailOptionalArguments.Clear();
				return result;
			}
			if (!MailCommandHelpers.IsValidXShadow(parseOutput.XShadow, sessionState))
			{
				return ParseResult.InvalidArguments;
			}
			ParseResult result2 = MailCommandHelpers.CheckArgumentPermissions(sessionState, parseOutput);
			if (result2.IsFailed)
			{
				return result2;
			}
			if (parseOutput.SmtpUtf8 && !sessionState.AdvertisedEhloOptions.SmtpUtf8)
			{
				return ParseResult.CommandNotImplementedProtocolError;
			}
			if (!string.IsNullOrEmpty(parseOutput.InternetMessageId))
			{
				if (!sessionState.AdvertisedEhloOptions.XMsgId)
				{
					return ParseResult.CommandNotImplementedProtocolError;
				}
				if (sessionState.AuthMethod != MultilevelAuthMechanism.MUTUALGSSAPI)
				{
					return ParseResult.NotAuthorized;
				}
				if (isMessagePoison(parseOutput.InternetMessageId))
				{
					sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Rejecting Message-ID: {0} because it was detected as poison", new object[]
					{
						parseOutput.InternetMessageId
					});
					return ParseResult.TooManyRelatedErrors;
				}
			}
			if (!MailCommandHelpers.AreAllMandatoryBlobsPresent(parseOutput.MessageContextParameters, sessionState))
			{
				return ParseResult.XMessageEPropNotFoundInMailCommand;
			}
			if (!MailCommandHelpers.AreSpecifiedBlobsAdvertised(parseOutput.MessageContextParameters, sessionState))
			{
				return ParseResult.InvalidArguments;
			}
			if (parseOutput.XAttrOrgId != null && parseOutput.XAttrOrgId.InternalOrgId != null && (sessionState.CombinedPermissions & Permission.AcceptForestHeaders) == Permission.None)
			{
				parseOutput.XAttrOrgId.InternalOrgId = null;
				sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Ignored provided internal organization id.");
			}
			parseOutput.ShadowMessageId = MailCommandHelpers.ExtractShadowMessageId(sessionState, parseOutput);
			if (!MailCommandHelpers.ValidateSizeRestrictions(sessionState, parseOutput.Size))
			{
				return ParseResult.MessageTooLarge;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000D7267 File Offset: 0x000D5467
		public static ParseResult ParseOptionalArguments(RoutingAddress fromAddress, CommandContext commandContext, ITracer tracer, TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig, out MailParseOutput parseOutput, SmtpInSessionState sessionState = null)
		{
			parseOutput = new MailParseOutput(fromAddress);
			return MailSmtpCommandParser.ParseOptionalArgumentsInternal(commandContext, sessionState, tracer, mailCommandConfig, parseOutput);
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000D7280 File Offset: 0x000D5480
		private static ParseResult ParseOptionalArgumentsInternal(CommandContext commandContext, SmtpInSessionState sessionState, ITracer tracer, TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig, MailParseOutput parseOutput)
		{
			ArgumentValidator.ThrowIfNull("commandContext", commandContext);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			ArgumentValidator.ThrowIfNull("mailCommandConfig", mailCommandConfig);
			bool smtpUtf8Parsed = false;
			bool flag = sessionState != null && sessionState.TlsDomainCapabilities != null && SmtpInSessionUtils.ShouldAllowConsumerMail(sessionState.TlsDomainCapabilities.Value);
			commandContext.TrimLeadingWhitespace();
			ParseResult result = ParseResult.ParsingComplete;
			while (!commandContext.IsEndOfCommand)
			{
				Offset offset;
				commandContext.GetNextArgumentOffset(out offset);
				int num = CommandParsingHelper.GetNameValuePairSeparatorIndex(commandContext.Command, offset, 61);
				if (num == offset.Start || num >= offset.End - 1)
				{
					return ParseResult.InvalidArguments;
				}
				string text = string.Empty;
				Offset offset2 = new Offset(0, 0);
				string text2;
				if (num == -1)
				{
					text2 = ByteString.BytesToString(commandContext.Command, offset.Start, offset.Length, false).ToLower(CultureInfo.InvariantCulture);
					if (!MailSmtpCommandParser.IsValueLessExtension(text2))
					{
						return ParseResult.InvalidArguments;
					}
				}
				else
				{
					text2 = ByteString.BytesToString(commandContext.Command, offset.Start, num - offset.Start, false).ToLower(CultureInfo.InvariantCulture);
					num++;
					text = ByteString.BytesToString(commandContext.Command, num, offset.End - num, true).ToLower(CultureInfo.InvariantCulture);
					offset2.Start = num;
					offset2.End = offset.End;
				}
				if (flag)
				{
					if (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text))
					{
						return ParseResult.InvalidArguments;
					}
					if (parseOutput.ConsumerMailOptionalArguments.ContainsKey(text2))
					{
						parseOutput.ConsumerMailOptionalArguments[text2] = text;
					}
					else
					{
						parseOutput.ConsumerMailOptionalArguments.Add(text2, text);
					}
				}
				string key;
				if ((key = text2) == null)
				{
					goto IL_4C5;
				}
				if (<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60032fc-1 == null)
				{
					<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60032fc-1 = new Dictionary<string, int>(14)
					{
						{
							"size",
							0
						},
						{
							"smtputf8",
							1
						},
						{
							"body",
							2
						},
						{
							"ret",
							3
						},
						{
							"envid",
							4
						},
						{
							"auth",
							5
						},
						{
							"xshadow",
							6
						},
						{
							"xoorg",
							7
						},
						{
							"xmessagecontext",
							8
						},
						{
							"xattrorgid",
							9
						},
						{
							"xattrdirect",
							10
						},
						{
							"xsysprobeid",
							11
						},
						{
							"xmsgid",
							12
						},
						{
							"xorigfrom",
							13
						}
					};
				}
				int num2;
				if (!<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60032fc-1.TryGetValue(key, out num2))
				{
					goto IL_4C5;
				}
				switch (num2)
				{
				case 0:
					parseOutput.Size = MailSmtpCommandParser.ParseSizeArg(text, parseOutput.Size, out result);
					break;
				case 1:
					parseOutput.SmtpUtf8 = MailSmtpCommandParser.ParseSmtpUtf8Arg(text, smtpUtf8Parsed, sessionState != null && sessionState.SmtpUtf8Supported, out result);
					smtpUtf8Parsed = true;
					break;
				case 2:
					parseOutput.MailBodyType = MailSmtpCommandParser.HandleBodyExtension(text, parseOutput.MailBodyType, out result);
					break;
				case 3:
					parseOutput.DsnFormat = MailSmtpCommandParser.HandleRetExtension(text, parseOutput.DsnFormat, out result);
					break;
				case 4:
					parseOutput.EnvelopeId = MailSmtpCommandParser.GetStringWithSizeRestriction(commandContext.Command, offset2, 100, parseOutput.EnvelopeId, out result);
					break;
				case 5:
					parseOutput.Auth = MailSmtpCommandParser.GetStringWithSizeRestriction(commandContext.Command, offset2, 500, parseOutput.Auth, out result);
					break;
				case 6:
					parseOutput.XShadow = MailSmtpCommandParser.GetStringWithSizeRestriction(commandContext.Command, offset2, 255, parseOutput.XShadow, out result);
					break;
				case 7:
					parseOutput.Oorg = MailSmtpCommandParser.HandleOorgExtension(commandContext.Command, offset2, parseOutput.Oorg, out result);
					break;
				case 8:
				{
					MailSmtpCommandParser.TryGetOrderedListOfBlobsToReceive tryGetOrderedListOfBlobsToReceive;
					if (sessionState != null)
					{
						tryGetOrderedListOfBlobsToReceive = new MailSmtpCommandParser.TryGetOrderedListOfBlobsToReceive(sessionState.MessageContextBlob.TryGetOrderedListOfBlobsToReceive);
					}
					else
					{
						tryGetOrderedListOfBlobsToReceive = new MailSmtpCommandParser.TryGetOrderedListOfBlobsToReceive(SmtpMessageContextBlob.TryGetOrderedListOfBlobsToReceive);
					}
					parseOutput.MessageContextParameters = MailSmtpCommandParser.HandleMessageContextParameters(commandContext.Command, offset2, tryGetOrderedListOfBlobsToReceive, tracer, parseOutput.MessageContextParameters, out result);
					break;
				}
				case 9:
					parseOutput.XAttrOrgId = MailSmtpCommandParser.HandleXattrOrgIdExtension(commandContext.Command, offset2, tracer, mailCommandConfig, parseOutput.XAttrOrgId, out result);
					break;
				case 10:
					parseOutput.Directionality = MailSmtpCommandParser.HandleXattrDirectExtension(commandContext.Command, offset2, parseOutput.Directionality, out result);
					break;
				case 11:
					parseOutput.SystemProbeId = MailSmtpCommandParser.HandleXsysProbeIdDirectExtension(commandContext.Command, offset2, parseOutput.SystemProbeId, out result);
					break;
				case 12:
					parseOutput.InternetMessageId = MailSmtpCommandParser.GetStringWithSizeRestriction(commandContext.Command, offset2, 500, parseOutput.InternetMessageId, out result);
					break;
				case 13:
					parseOutput.OriginalFromAddress = MailSmtpCommandParser.HandleXOrigFromExtension(commandContext.Command, offset2, parseOutput.OriginalFromAddress, sessionState != null && sessionState.SmtpUtf8Supported, out result);
					break;
				default:
					goto IL_4C5;
				}
				IL_4CE:
				if (result.IsFailed)
				{
					return result;
				}
				commandContext.TrimLeadingWhitespace();
				continue;
				IL_4C5:
				if (!flag)
				{
					result = ParseResult.InvalidArguments;
					goto IL_4CE;
				}
				goto IL_4CE;
			}
			return result;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000D778C File Offset: 0x000D598C
		private static long ParseSizeArg(string sizeArgValue, long currentSize, out ParseResult result)
		{
			result = ParseResult.ParsingComplete;
			if (currentSize > 0L || sizeArgValue.Length > 20)
			{
				result = ParseResult.InvalidArguments;
				return -1L;
			}
			long num;
			if (long.TryParse(sizeArgValue, out num) && num >= 0L)
			{
				return num;
			}
			result = ((from digit in sizeArgValue
			select digit >= '0' && digit <= '9').All((bool digit) => digit) ? ParseResult.MessageTooLarge : ParseResult.InvalidArguments);
			return -1L;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000D782D File Offset: 0x000D5A2D
		private static bool ParseSmtpUtf8Arg(string smtpUtf8ArgValue, bool smtpUtf8Parsed, bool smtpUtf8Supported, out ParseResult result)
		{
			result = ParseResult.ParsingComplete;
			if (smtpUtf8Parsed || !smtpUtf8Supported || !string.IsNullOrEmpty(smtpUtf8ArgValue))
			{
				result = ParseResult.InvalidArguments;
				return false;
			}
			return true;
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000D7858 File Offset: 0x000D5A58
		private static Microsoft.Exchange.Transport.BodyType HandleBodyExtension(string bodyArgValue, Microsoft.Exchange.Transport.BodyType currentBodyType, out ParseResult result)
		{
			result = ParseResult.ParsingComplete;
			if (currentBodyType != Microsoft.Exchange.Transport.BodyType.Default)
			{
				result = ParseResult.InvalidArguments;
				return Microsoft.Exchange.Transport.BodyType.Default;
			}
			if (bodyArgValue != null)
			{
				if (bodyArgValue == "7bit")
				{
					return Microsoft.Exchange.Transport.BodyType.SevenBit;
				}
				if (bodyArgValue == "8bitmime")
				{
					return Microsoft.Exchange.Transport.BodyType.EightBitMIME;
				}
				if (bodyArgValue == "binarymime")
				{
					return Microsoft.Exchange.Transport.BodyType.BinaryMIME;
				}
			}
			result = ParseResult.UnsupportedBodyType;
			return Microsoft.Exchange.Transport.BodyType.Default;
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000D78C0 File Offset: 0x000D5AC0
		private static DsnFormat HandleRetExtension(string retArgValue, DsnFormat currentDsnFormat, out ParseResult result)
		{
			result = ParseResult.ParsingComplete;
			if (currentDsnFormat != DsnFormat.Default)
			{
				result = ParseResult.InvalidArguments;
				return DsnFormat.Default;
			}
			if (retArgValue != null)
			{
				if (retArgValue == "full")
				{
					return DsnFormat.Full;
				}
				if (retArgValue == "hdrs")
				{
					return DsnFormat.Headers;
				}
			}
			result = ParseResult.InvalidArguments;
			return DsnFormat.Default;
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000D791C File Offset: 0x000D5B1C
		private static string HandleOorgExtension(byte[] protocolCommand, Offset valueOffset, string currentOorgValue, out ParseResult result)
		{
			string stringWithSizeRestriction = MailSmtpCommandParser.GetStringWithSizeRestriction(protocolCommand, valueOffset, valueOffset.Length, currentOorgValue, out result);
			if (result.IsFailed)
			{
				return currentOorgValue;
			}
			if (stringWithSizeRestriction == null || !RoutingAddress.IsValidDomain(stringWithSizeRestriction))
			{
				result = ParseResult.InvalidArguments;
				return currentOorgValue;
			}
			return stringWithSizeRestriction;
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000D795C File Offset: 0x000D5B5C
		private static MailCommandMessageContextParameters HandleMessageContextParameters(byte[] protocolCommand, Offset valueOffset, MailSmtpCommandParser.TryGetOrderedListOfBlobsToReceive tryGetOrderedListOfBlobsToReceive, ITracer tracer, MailCommandMessageContextParameters currentMessageContextParameters, out ParseResult result)
		{
			result = ParseResult.ParsingComplete;
			if (currentMessageContextParameters != null)
			{
				tracer.TraceError(0L, "MessageContext blob appeared more than once in Mail command parameter.");
				result = ParseResult.InvalidArguments;
				return null;
			}
			if (valueOffset.Length > 500)
			{
				result = ParseResult.InvalidArguments;
				return null;
			}
			string text = SmtpUtils.FromXtextString(protocolCommand, valueOffset.Start, valueOffset.Length, false);
			MailCommandMessageContextParameters result2;
			if (tryGetOrderedListOfBlobsToReceive(text, out result2))
			{
				return result2;
			}
			tracer.TraceError<string>(0L, "Encountered error while parsing {0}", text);
			result = ParseResult.InvalidArguments;
			return null;
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000D79EC File Offset: 0x000D5BEC
		private static MailParseOutput.XAttrOrgIdData HandleXattrOrgIdExtension(byte[] protocolCommand, Offset valueOffset, ITracer tracer, TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig, MailParseOutput.XAttrOrgIdData currentXAttrOrgIdData, out ParseResult result)
		{
			if (currentXAttrOrgIdData != null)
			{
				result = ParseResult.InvalidArguments;
				return currentXAttrOrgIdData;
			}
			MailParseOutput.XAttrOrgIdData result2;
			if (MailSmtpCommandParser.XAttrHelper.TryParseXorgId(mailCommandConfig, SmtpUtils.FromXtextString(protocolCommand, valueOffset.Start, valueOffset.Length, false), tracer, out result2))
			{
				result = ParseResult.ParsingComplete;
			}
			else
			{
				result = ParseResult.InvalidArguments;
			}
			return result2;
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000D7A48 File Offset: 0x000D5C48
		private static MailDirectionality HandleXattrDirectExtension(byte[] protocolCommand, Offset valueOffset, MailDirectionality currentDirectionality, out ParseResult result)
		{
			result = ParseResult.ParsingComplete;
			if (currentDirectionality != MailDirectionality.Undefined)
			{
				result = ParseResult.InvalidArguments;
				return currentDirectionality;
			}
			MailDirectionality result2;
			if (MultiTenantTransport.TryParseDirectionality(SmtpUtils.FromXtextString(protocolCommand, valueOffset.Start, valueOffset.Length, false), out result2))
			{
				return result2;
			}
			result = ParseResult.InvalidArguments;
			return result2;
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000D7A9C File Offset: 0x000D5C9C
		private static Guid HandleXsysProbeIdDirectExtension(byte[] protocolCommand, Offset valueOffset, Guid currentSystemProbeId, out ParseResult result)
		{
			result = ParseResult.ParsingComplete;
			if (currentSystemProbeId != Guid.Empty)
			{
				result = ParseResult.InvalidArguments;
				return currentSystemProbeId;
			}
			Guid guid;
			if (Guid.TryParse(SmtpUtils.FromXtextString(protocolCommand, valueOffset.Start, valueOffset.Length, false), out guid) && guid != Guid.Empty)
			{
				return guid;
			}
			result = ParseResult.InvalidArguments;
			return guid;
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000D7B08 File Offset: 0x000D5D08
		private static string GetStringWithSizeRestriction(byte[] protocolCommand, Offset valueOffset, int maxLen, string currentValue, out ParseResult result)
		{
			result = ParseResult.ParsingComplete;
			if (string.IsNullOrEmpty(currentValue) && valueOffset.Length <= maxLen && !valueOffset.Empty)
			{
				string text = SmtpUtils.FromXtextString(protocolCommand, valueOffset.Start, valueOffset.Length, false);
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			result = ParseResult.InvalidArguments;
			return currentValue;
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000D7B68 File Offset: 0x000D5D68
		private static RoutingAddress HandleXOrigFromExtension(byte[] protocolCommand, Offset valueOffset, RoutingAddress currentValue, bool allowUTF8, out ParseResult result)
		{
			if (currentValue != RoutingAddress.Empty)
			{
				result = ParseResult.InvalidArguments;
				return currentValue;
			}
			RoutingAddress routingAddress = new RoutingAddress(SmtpUtils.FromXtextString(protocolCommand, valueOffset.Start, valueOffset.Length, allowUTF8));
			if (!Util.IsValidAddress(routingAddress))
			{
				result = ParseResult.InvalidArguments;
				return currentValue;
			}
			result = ParseResult.ParsingComplete;
			return routingAddress;
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000D7BD0 File Offset: 0x000D5DD0
		private static bool TryParseAddress(CommandContext context, SmtpInSessionState sessionState, bool isDataRedactionNecessary, out RoutingAddress address, out byte[] commandToLog, out string tail, out SmtpResponse response)
		{
			address = RoutingAddress.Empty;
			tail = null;
			response = SmtpResponse.Empty;
			if (!context.ParseTokenAndVerifyCommand(MailSmtpCommandParser.From, 58))
			{
				response = SmtpResponse.UnrecognizedParameter;
				commandToLog = MailSmtpCommandParser.CreateCommandToLog(context);
				return false;
			}
			int offset = context.Offset;
			if (context.IsEndOfCommand)
			{
				response = SmtpResponse.InvalidSenderAddress;
				commandToLog = MailSmtpCommandParser.CreateCommandToLog(context);
				return false;
			}
			string text;
			context.GetCommandArguments(out text);
			bool flag = text.Split(new char[]
			{
				' '
			}).Contains("smtputf8", StringComparer.OrdinalIgnoreCase);
			if (flag && !sessionState.AdvertisedEhloOptions.SmtpUtf8)
			{
				response = SmtpResponse.CommandNotImplemented;
				commandToLog = MailSmtpCommandParser.CreateCommandToLog(context);
				return false;
			}
			Parse821.TryParseAddressLine(text, out address, out tail);
			if (!Util.IsValidAddress(address))
			{
				commandToLog = MailSmtpCommandParser.CreateCommandToLog(context);
				bool flag2 = false;
				if (sessionState.Configuration.TransportConfiguration.AcceptAndFixSmtpAddressWithInvalidLocalPart)
				{
					RoutingAddress routingAddress = Util.FixSmtpAddressWithInvalidLocalPart(address.ToString());
					if (routingAddress != RoutingAddress.Empty)
					{
						address = routingAddress;
						flag2 = true;
					}
				}
				if (!flag2)
				{
					if (sessionState.ReceiveConnector.DefaultDomain == null || string.IsNullOrEmpty(sessionState.ReceiveConnector.DefaultDomain.ToString()) || !string.IsNullOrEmpty(address.DomainPart))
					{
						response = SmtpResponse.InvalidSenderAddress;
						return false;
					}
					address = new RoutingAddress(address.ToString(), sessionState.ReceiveConnector.DefaultDomain.ToString());
				}
				if (!Util.IsValidAddress(address))
				{
					response = SmtpResponse.InvalidSenderAddress;
					return false;
				}
			}
			else if (isDataRedactionNecessary)
			{
				byte[] array = new byte[offset - context.OriginalOffset];
				Buffer.BlockCopy(context.Command, context.OriginalOffset, array, 0, array.Length);
				commandToLog = Util.CreateRedactedCommand(array, address, tail, true);
			}
			else
			{
				commandToLog = MailSmtpCommandParser.CreateCommandToLog(context);
			}
			if (Util.IsLongAddress(address) && !sessionState.AdvertisedEhloOptions.XLongAddr)
			{
				response = SmtpResponse.LongSenderAddress;
				return false;
			}
			if (address.IsUTF8 && !sessionState.AdvertisedEhloOptions.SmtpUtf8)
			{
				response = SmtpResponse.Utf8SenderAddress;
				return false;
			}
			if (address.IsUTF8 && !flag)
			{
				response = SmtpResponse.SmtpUtf8ArgumentNotProvided;
				return false;
			}
			sessionState.SmtpUtf8Supported = flag;
			return true;
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000D7E3A File Offset: 0x000D603A
		private static bool IsValueLessExtension(string extensionName)
		{
			return MailSmtpCommandParser.ValueLessExtensionNames.Contains(extensionName, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000D7E4C File Offset: 0x000D604C
		private static byte[] CreateCommandToLog(CommandContext context)
		{
			byte[] array = new byte[context.OriginalLength];
			Buffer.BlockCopy(context.Command, context.OriginalOffset, array, 0, context.OriginalLength);
			return array;
		}

		// Token: 0x04001AE0 RID: 6880
		public const string CommandKeyword = "MAIL";

		// Token: 0x04001AE1 RID: 6881
		public const int MaxEnvIdLen = 100;

		// Token: 0x04001AE2 RID: 6882
		public const int MaxAuthLen = 500;

		// Token: 0x04001AE3 RID: 6883
		public const int MaxMessageIdLength = 500;

		// Token: 0x04001AE4 RID: 6884
		private const int MaxMessageContextLen = 500;

		// Token: 0x04001AE5 RID: 6885
		private const int MaxXShadowValueLen = 255;

		// Token: 0x04001AE6 RID: 6886
		private static readonly TimeSpan BadCertificateTarpitInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04001AE7 RID: 6887
		private static readonly byte[] From = Util.AsciiStringToBytes("from");

		// Token: 0x04001AE8 RID: 6888
		private static readonly List<string> ValueLessExtensionNames = new List<string>
		{
			"smtputf8"
		};

		// Token: 0x02000480 RID: 1152
		// (Invoke) Token: 0x060034EA RID: 13546
		public delegate ParseResult ValidateSequence();

		// Token: 0x02000481 RID: 1153
		// (Invoke) Token: 0x060034EE RID: 13550
		public delegate void UpdateState(SmtpInSessionState sessionState);

		// Token: 0x02000482 RID: 1154
		// (Invoke) Token: 0x060034F2 RID: 13554
		public delegate bool IsPoisonMessage(string internetMessageId);

		// Token: 0x02000483 RID: 1155
		// (Invoke) Token: 0x060034F6 RID: 13558
		public delegate void PublishNotification(string serviceName, string component, string tag, string notificationReason, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false);

		// Token: 0x02000484 RID: 1156
		// (Invoke) Token: 0x060034FA RID: 13562
		private delegate bool TryGetOrderedListOfBlobsToReceive(string mailCommandParameter, out MailCommandMessageContextParameters messageContextInfo);

		// Token: 0x02000485 RID: 1157
		internal static class XAttrHelper
		{
			// Token: 0x060034FD RID: 13565 RVA: 0x000D7EC8 File Offset: 0x000D60C8
			public static bool TryParseXorgId(TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig, string xorgId, ITracer tracer, out MailParseOutput.XAttrOrgIdData xAttrOrgIdData)
			{
				if (string.IsNullOrEmpty(xorgId))
				{
					xAttrOrgIdData = null;
					return false;
				}
				xAttrOrgIdData = new MailParseOutput.XAttrOrgIdData();
				bool flag = true;
				bool flag2 = false;
				string[] array = xorgId.Split(new char[]
				{
					';'
				});
				if (array.Length > 0)
				{
					foreach (string text in array)
					{
						string[] array3 = text.Split(new char[]
						{
							':'
						});
						string a;
						if (array3.Length == 2 && (a = array3[0]) != null)
						{
							if (!(a == "xorgid"))
							{
								OrganizationId internalOrgId;
								if (!(a == "intorgid"))
								{
									if (!(a == "eaf"))
									{
										if (a == "etc")
										{
											if (MailSmtpCommandParser.XAttrHelper.UseAdditionalTenantDataFromXAttr(mailCommandConfig))
											{
												if (!string.IsNullOrEmpty(xAttrOrgIdData.ExoTenantContainer) || string.IsNullOrEmpty(array3[1]))
												{
													flag = false;
												}
												xAttrOrgIdData.ExoTenantContainer = array3[1];
											}
										}
									}
									else if (MailSmtpCommandParser.XAttrHelper.UseAdditionalTenantDataFromXAttr(mailCommandConfig))
									{
										if (!string.IsNullOrEmpty(xAttrOrgIdData.ExoAccountForest) || string.IsNullOrEmpty(array3[1]))
										{
											flag = false;
										}
										xAttrOrgIdData.ExoAccountForest = array3[1];
									}
								}
								else if (xAttrOrgIdData.InternalOrgId == null && MailSmtpCommandParser.XAttrHelper.TryParseInternalOrganizationId(array3[1], tracer, out internalOrgId))
								{
									xAttrOrgIdData.InternalOrgId = internalOrgId;
								}
								else
								{
									flag = false;
								}
							}
							else
							{
								Guid externalOrgId;
								if (!flag2 && Guid.TryParse(array3[1], out externalOrgId))
								{
									xAttrOrgIdData.ExternalOrgId = externalOrgId;
								}
								else
								{
									flag = false;
								}
								flag2 = true;
							}
						}
						if (!flag)
						{
							break;
						}
					}
				}
				return flag && xAttrOrgIdData.ExternalOrgId != Guid.Empty;
			}

			// Token: 0x060034FE RID: 13566 RVA: 0x000D8064 File Offset: 0x000D6264
			public static string GetOrgIdString(TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig, Guid externalOrganizationId, OrganizationId internalOrganizationId, string exoAccountForest, string exoTenantContainer)
			{
				ArgumentValidator.ThrowIfNull("configuration", mailCommandConfig);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("xorgid");
				stringBuilder.Append(':');
				stringBuilder.Append(externalOrganizationId);
				if (internalOrganizationId != null)
				{
					stringBuilder.Append(';');
					stringBuilder.Append("intorgid");
					stringBuilder.Append(':');
					stringBuilder.Append(MailSmtpCommandParser.XAttrHelper.GetInternalOrgIdString(internalOrganizationId));
				}
				if (MailSmtpCommandParser.XAttrHelper.TransferAdditionalTenantDataThroughXAttr(mailCommandConfig) && !string.IsNullOrEmpty(exoAccountForest) && !string.IsNullOrEmpty(exoTenantContainer))
				{
					stringBuilder.Append(';');
					stringBuilder.Append("eaf");
					stringBuilder.Append(':');
					stringBuilder.Append(exoAccountForest);
					stringBuilder.Append(';');
					stringBuilder.Append("etc");
					stringBuilder.Append(':');
					stringBuilder.Append(exoTenantContainer);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060034FF RID: 13567 RVA: 0x000D8144 File Offset: 0x000D6344
			public static string GetDirectionalityString(MailDirectionality directionality)
			{
				string result;
				switch (directionality)
				{
				case MailDirectionality.Originating:
					result = MultiTenantTransport.OriginatingStr;
					break;
				case MailDirectionality.Incoming:
					result = MultiTenantTransport.IncomingStr;
					break;
				default:
					throw new InvalidOperationException("GetDirectionalityString called when directionality was not originating or incoming");
				}
				return result;
			}

			// Token: 0x06003500 RID: 13568 RVA: 0x000D8184 File Offset: 0x000D6384
			private static string GetInternalOrgIdString(OrganizationId orgId)
			{
				byte[] bytes = orgId.GetBytes(Encoding.UTF8);
				string text = Convert.ToBase64String(bytes);
				return text.Replace('=', '-');
			}

			// Token: 0x06003501 RID: 13569 RVA: 0x000D81B0 File Offset: 0x000D63B0
			private static bool TryParseInternalOrganizationId(string orgIdStr, ITracer tracer, out OrganizationId orgId)
			{
				orgId = null;
				if (string.IsNullOrEmpty(orgIdStr))
				{
					tracer.TraceError(0L, "Empty base64 string for internal organization id");
					return false;
				}
				orgIdStr = orgIdStr.Replace('-', '=');
				byte[] bytes;
				try
				{
					bytes = Convert.FromBase64String(orgIdStr);
				}
				catch (FormatException arg)
				{
					tracer.TraceError<string, FormatException>(0L, "Invalid base64 string for internal organization id: {0}; exception: {1}", orgIdStr, arg);
					return false;
				}
				if (!OrganizationId.TryCreateFromBytes(bytes, Encoding.UTF8, out orgId))
				{
					tracer.TraceError(0L, "Failed to parse organization id bytes");
					return false;
				}
				return true;
			}

			// Token: 0x06003502 RID: 13570 RVA: 0x000D8230 File Offset: 0x000D6430
			private static bool TransferAdditionalTenantDataThroughXAttr(TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig)
			{
				return mailCommandConfig.TransferAdditionalTenantDataThroughXAttr && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Transport.TransferAdditionalTenantDataThroughXATTR.Enabled;
			}

			// Token: 0x06003503 RID: 13571 RVA: 0x000D8268 File Offset: 0x000D6468
			private static bool UseAdditionalTenantDataFromXAttr(TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig)
			{
				return mailCommandConfig.UseAdditionalTenantDataFromXAttr && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Transport.UseAdditionalTenantDataFromXATTR.Enabled;
			}

			// Token: 0x04001AEB RID: 6891
			private const string XattrExternalOrganizationIdKey = "xorgid";

			// Token: 0x04001AEC RID: 6892
			private const string XattrInternalExchangeOrganizationIdKey = "intorgid";

			// Token: 0x04001AED RID: 6893
			private const string XattrExoAccountForestKey = "eaf";

			// Token: 0x04001AEE RID: 6894
			private const string XattrExoTenantContainerKey = "etc";

			// Token: 0x04001AEF RID: 6895
			private const char KeyValuePairSeparator = ';';

			// Token: 0x04001AF0 RID: 6896
			private const char KeyValueSeparator = ':';
		}
	}
}
