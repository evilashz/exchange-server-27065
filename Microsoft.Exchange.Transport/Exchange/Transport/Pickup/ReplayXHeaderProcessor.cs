using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Encoders;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.Pickup
{
	// Token: 0x02000529 RID: 1321
	internal class ReplayXHeaderProcessor
	{
		// Token: 0x06003D94 RID: 15764 RVA: 0x0010248C File Offset: 0x0010068C
		public ReplayXHeaderProcessor(TransportMailItem transportMailItem)
		{
			this.transportMailItem = transportMailItem;
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x0010249C File Offset: 0x0010069C
		public bool ProcessXHeaders(HeaderList headerList, out ReplayFileMailer.XHeaderType xheadersProcessed, out string messageCreator, bool isInitiationMsgResubmit)
		{
			xheadersProcessed = ReplayFileMailer.XHeaderType.None;
			messageCreator = null;
			Header header2;
			for (Header header = headerList.FirstChild as Header; header != null; header = header2)
			{
				header2 = (header.NextSibling as Header);
				if (!header.Name.StartsWith("x-", StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
				if (!this.ParseXHeader(header) && !isInitiationMsgResubmit)
				{
					return false;
				}
				headerList.RemoveChild(header);
				if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.End) == ReplayFileMailer.XHeaderType.End)
				{
					break;
				}
			}
			if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.Receiver) == ReplayFileMailer.XHeaderType.None || (this.xheadersProcessed & ReplayFileMailer.XHeaderType.Sender) == ReplayFileMailer.XHeaderType.None)
			{
				return false;
			}
			messageCreator = this.messageCreator;
			xheadersProcessed = this.xheadersProcessed;
			return true;
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x00102534 File Offset: 0x00100734
		private static bool TryGetBinaryFromHeader(Header header, out byte[] blob)
		{
			blob = null;
			string value;
			if (!ReplayXHeaderProcessor.TryGetXHeaderValue(header, out value))
			{
				return false;
			}
			blob = Util.AsciiStringToBytes(value);
			return true;
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x00102559 File Offset: 0x00100759
		private static bool TryGetXHeaderValue(Header header, out string value)
		{
			return ReplayXHeaderProcessor.TryGetXHeaderValue(header, out value, DecodingOptions.None);
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x00102568 File Offset: 0x00100768
		private static bool TryGetXHeaderValue(Header header, out string value, DecodingOptions decodingOptions)
		{
			TextHeader textHeader = header as TextHeader;
			if (textHeader == null)
			{
				value = null;
				return false;
			}
			DecodingResults decodingResults;
			return textHeader.TryGetValue(decodingOptions, out decodingResults, out value) && !string.IsNullOrEmpty(value);
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x0010259C File Offset: 0x0010079C
		private bool ParseXHeader(Header header)
		{
			string name = header.Name;
			ExTraceGlobals.PickupTracer.TraceDebug<string>((long)this.GetHashCode(), "X-Header name is {0}", name);
			char c = char.ToLowerInvariant(name[2]);
			char c2 = c;
			if (c2 <= 'h')
			{
				switch (c2)
				{
				case 'c':
					if (header.Name.Equals("X-CreatedBy", StringComparison.OrdinalIgnoreCase))
					{
						if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.CreatedBy) == ReplayFileMailer.XHeaderType.None && ReplayXHeaderProcessor.TryGetXHeaderValue(header, out this.messageCreator, ReplayXHeaderProcessor.Rfc2047DecodingOptions))
						{
							this.xheadersProcessed |= ReplayFileMailer.XHeaderType.CreatedBy;
							return true;
						}
						return false;
					}
					break;
				case 'd':
					break;
				case 'e':
					if (name.Equals("X-ExtendedProps", StringComparison.OrdinalIgnoreCase))
					{
						if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.ExtendedMessageProps) == ReplayFileMailer.XHeaderType.None && this.ParseMessageXExtendedProps(header))
						{
							this.xheadersProcessed |= ReplayFileMailer.XHeaderType.ExtendedMessageProps;
							return true;
						}
						return false;
					}
					else if (name.Equals("X-EndOfInjectedXHeaders", StringComparison.OrdinalIgnoreCase))
					{
						if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.End) != ReplayFileMailer.XHeaderType.None)
						{
							throw new InvalidOperationException("hitting End again");
						}
						this.ParseP2MessageSize(header);
						this.xheadersProcessed |= ReplayFileMailer.XHeaderType.End;
						return true;
					}
					break;
				default:
					if (c2 == 'h')
					{
						if (header.Name.Equals("X-HeloDomain", StringComparison.OrdinalIgnoreCase))
						{
							string heloDomain;
							if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.HeloDomain) == ReplayFileMailer.XHeaderType.None && ReplayXHeaderProcessor.TryGetXHeaderValue(header, out heloDomain))
							{
								this.xheadersProcessed |= ReplayFileMailer.XHeaderType.HeloDomain;
								this.transportMailItem.HeloDomain = heloDomain;
								return true;
							}
							return false;
						}
					}
					break;
				}
			}
			else if (c2 != 'l')
			{
				switch (c2)
				{
				case 'r':
					if (name.Equals("X-Receiver", StringComparison.OrdinalIgnoreCase))
					{
						if (this.ParseXReceiver(header))
						{
							this.xheadersProcessed |= ReplayFileMailer.XHeaderType.Receiver;
							return true;
						}
						return false;
					}
					break;
				case 's':
					if (name.Equals("X-sender", StringComparison.OrdinalIgnoreCase))
					{
						if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.Sender) == ReplayFileMailer.XHeaderType.None && this.ParseXSender(header))
						{
							this.xheadersProcessed |= ReplayFileMailer.XHeaderType.Sender;
							return true;
						}
						return false;
					}
					else if (name.Equals("X-Source", StringComparison.OrdinalIgnoreCase))
					{
						string text;
						if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.Source) == ReplayFileMailer.XHeaderType.None && ReplayXHeaderProcessor.TryGetXHeaderValue(header, out text, ReplayXHeaderProcessor.Rfc2047DecodingOptions))
						{
							this.xheadersProcessed |= ReplayFileMailer.XHeaderType.Source;
							string text2 = "Replay:";
							if (!text.StartsWith(text2, StringComparison.Ordinal))
							{
								text = text2 + text;
							}
							this.transportMailItem.ReceiveConnectorName = text;
							return true;
						}
						return false;
					}
					else if (name.Equals("X-SourceIPAddress", StringComparison.OrdinalIgnoreCase))
					{
						if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.SourceIPAddress) == ReplayFileMailer.XHeaderType.None && this.ParseXSouceIPAddress(header))
						{
							this.xheadersProcessed |= ReplayFileMailer.XHeaderType.SourceIPAddress;
							return true;
						}
						return false;
					}
					break;
				}
			}
			else if (header.Name.Equals("X-LegacyExch50", StringComparison.OrdinalIgnoreCase))
			{
				if ((this.xheadersProcessed & ReplayFileMailer.XHeaderType.LegacyExch50) == ReplayFileMailer.XHeaderType.None && this.ParseXLegacyMessageExch50(header))
				{
					this.xheadersProcessed |= ReplayFileMailer.XHeaderType.LegacyExch50;
					return true;
				}
				return false;
			}
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Not a known x-header");
			return true;
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x001028A0 File Offset: 0x00100AA0
		private bool ParseXSender(Header header)
		{
			string protocolText;
			if (!ReplayXHeaderProcessor.TryGetXHeaderValue(header, out protocolText))
			{
				return false;
			}
			RoutingAddress routingAddress;
			string value;
			Parse821.TryParseAddressLine(protocolText, out routingAddress, out value);
			if (!Util.IsValidAddress(routingAddress))
			{
				ExTraceGlobals.PickupTracer.TraceError((long)this.GetHashCode(), "Invalid sender");
				return false;
			}
			if (string.IsNullOrEmpty(value))
			{
				ExTraceGlobals.PickupTracer.TraceDebug<string>((long)this.GetHashCode(), "Parse done. sender {0}, no arguments", (string)routingAddress);
				this.transportMailItem.From = routingAddress;
				return true;
			}
			byte[] bytes = Util.AsciiStringToBytes(value);
			MailParseOutput parseOutput;
			ParseResult parseResult = MailSmtpCommandParser.ParseOptionalArguments(routingAddress, CommandContext.FromByteArrayLegacyCodeOnly(bytes, 0), ExTraceGlobals.SmtpReceiveTracer, Components.Configuration.AppConfig.SmtpMailCommandConfiguration, out parseOutput, null);
			if (parseResult.IsFailed || parseOutput.Size != 0L)
			{
				ExTraceGlobals.PickupTracer.TraceError<SmtpResponse, long>((long)this.GetHashCode(), "Parse arguments failed. Response is {0}, size is {1}", parseResult.SmtpResponse, parseOutput.Size);
				return false;
			}
			this.transportMailItem.From = routingAddress;
			this.transportMailItem.BodyType = parseOutput.MailBodyType;
			this.transportMailItem.DsnFormat = parseOutput.DsnFormat;
			this.transportMailItem.Auth = parseOutput.Auth;
			this.transportMailItem.EnvId = parseOutput.EnvelopeId;
			if (parseOutput.XAttrOrgId != null && parseOutput.XAttrOrgId.ExternalOrgId != Guid.Empty && parseOutput.Directionality != MailDirectionality.Undefined)
			{
				this.transportMailItem.ExternalOrganizationId = parseOutput.XAttrOrgId.ExternalOrgId;
				this.transportMailItem.Directionality = parseOutput.Directionality;
				if (parseOutput.XAttrOrgId.InternalOrgId != null)
				{
					ADNotificationAdapter.TryRunADOperation(delegate()
					{
						MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(this.transportMailItem, parseOutput.XAttrOrgId.InternalOrgId);
					}, 0);
				}
			}
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Parsed address:{0}, BodyType:{1}, DsnFormat:{2}, Auth:{3}", new object[]
			{
				(string)routingAddress,
				this.transportMailItem.BodyType,
				this.transportMailItem.DsnFormat,
				this.transportMailItem.Auth
			});
			return true;
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x00102B0C File Offset: 0x00100D0C
		private bool ParseXSouceIPAddress(Header header)
		{
			string ipString;
			IPAddress sourceIPAddress;
			if (!ReplayXHeaderProcessor.TryGetXHeaderValue(header, out ipString) || !IPAddress.TryParse(ipString, out sourceIPAddress))
			{
				return false;
			}
			this.transportMailItem.SourceIPAddress = sourceIPAddress;
			return true;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x00102B3C File Offset: 0x00100D3C
		private bool ParseP2MessageSize(Header header)
		{
			string s;
			return ReplayXHeaderProcessor.TryGetXHeaderValue(header, out s) && long.TryParse(s, out this.expectedP2MessageSize);
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x00102B64 File Offset: 0x00100D64
		private bool ParseXReceiver(Header header)
		{
			string xreceiverLine;
			if (!ReplayXHeaderProcessor.TryGetXHeaderValue(header, out xreceiverLine))
			{
				return false;
			}
			ExTraceGlobals.PickupTracer.TraceDebug<Header>((long)this.GetHashCode(), "parsing {0}", header);
			string protocolText;
			string text;
			this.ParseExtendedPropsInXReceiver(xreceiverLine, out protocolText, out text);
			RoutingAddress address;
			string value;
			Parse821.TryParseAddressLine(protocolText, out address, out value);
			if (!Util.IsValidAddress(address))
			{
				ExTraceGlobals.PickupTracer.TraceError((long)this.GetHashCode(), "Invalid recipient");
				return false;
			}
			MailRecipient mailRecipient = this.transportMailItem.Recipients.Add((string)address);
			if (!string.IsNullOrEmpty(value))
			{
				byte[] bytes = Util.AsciiStringToBytes(value);
				DsnRequestedFlags dsnRequested;
				string orcpt;
				RoutingAddress routingAddress;
				string text2;
				byte[] array;
				ParseResult parseResult = RcptSmtpCommandParser.ParseOptionalArguments(CommandContext.FromByteArrayLegacyCodeOnly(bytes, 0), true, false, out dsnRequested, out orcpt, out routingAddress, out text2, out array, null);
				if (parseResult.IsFailed)
				{
					ExTraceGlobals.PickupTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "Parse arguments failed. Response is {0}", parseResult.SmtpResponse);
					return false;
				}
				mailRecipient.DsnRequested = dsnRequested;
				mailRecipient.ORcpt = orcpt;
				if (routingAddress != RoutingAddress.Empty)
				{
					OrarGenerator.SetOrarAddress(mailRecipient, routingAddress);
				}
			}
			if (text != null)
			{
				this.xheadersProcessed |= ReplayFileMailer.XHeaderType.ReceiverExtendedProps;
				text = text.Trim();
				if (text.Length - "X-ExtendedProps=".Length <= 0 || !this.ParseXExtendedProps(Util.AsciiStringToBytes(text), mailRecipient.ExtendedProperties))
				{
					ExTraceGlobals.PickupTracer.TraceError<string>((long)this.GetHashCode(), "Unexpected ExtendedProps in '{0}'", text);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00102CCC File Offset: 0x00100ECC
		private void ParseExtendedPropsInXReceiver(string xreceiverLine, out string rcptProtocolLine, out string xextendedProps)
		{
			xextendedProps = null;
			rcptProtocolLine = null;
			int num = xreceiverLine.LastIndexOf("X-ExtendedProps=", StringComparison.OrdinalIgnoreCase);
			if (num <= 0)
			{
				rcptProtocolLine = xreceiverLine;
				return;
			}
			xextendedProps = xreceiverLine.Substring(num + "X-ExtendedProps=".Length);
			int i = num - 1;
			while (i > 0)
			{
				char c = xreceiverLine[i];
				if (!char.IsWhiteSpace(c))
				{
					if (c != ';')
					{
						ExTraceGlobals.PickupTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected character before separator ';' is found. Line is {0}. Treating entire line as rcpt-to", xreceiverLine);
						rcptProtocolLine = xreceiverLine;
						return;
					}
					break;
				}
				else
				{
					i--;
				}
			}
			if (i <= 0)
			{
				ExTraceGlobals.PickupTracer.TraceDebug<string>((long)this.GetHashCode(), "Cannot find separator between the protocol line and the extended properties for recipient. Line is {0}", xreceiverLine);
				rcptProtocolLine = xreceiverLine;
				return;
			}
			rcptProtocolLine = xreceiverLine.Substring(0, i);
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x00102D70 File Offset: 0x00100F70
		private bool ParseMessageXExtendedProps(Header header)
		{
			byte[] base64Blob;
			return ReplayXHeaderProcessor.TryGetBinaryFromHeader(header, out base64Blob) && this.ParseXExtendedProps(base64Blob, this.transportMailItem.ExtendedProperties);
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x00102D9C File Offset: 0x00100F9C
		private bool ParseXExtendedProps(byte[] base64Blob, IExtendedPropertyCollection extendedProps)
		{
			using (EncoderStream encoderStream = new EncoderStream(new MemoryStream(base64Blob, false), new Base64Decoder(), EncoderStreamAccess.Read))
			{
				try
				{
					extendedProps.Deserialize(encoderStream, int.MaxValue, true);
				}
				catch (FormatException arg)
				{
					ExTraceGlobals.PickupTracer.TraceError<FormatException>((long)this.GetHashCode(), "ExtendedProps Exception {0}", arg);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x00102E14 File Offset: 0x00101014
		private bool ParseXLegacyMessageExch50(Header header)
		{
			byte[] legacyXexch50Blob = null;
			if (!this.TryGetExch50FromBase64Header(header, out legacyXexch50Blob))
			{
				return false;
			}
			this.transportMailItem.LegacyXexch50Blob = legacyXexch50Blob;
			return true;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x00102E40 File Offset: 0x00101040
		private bool TryGetExch50FromBase64Header(Header header, out byte[] parsedBlob)
		{
			parsedBlob = null;
			byte[] array;
			if (!ReplayXHeaderProcessor.TryGetBinaryFromHeader(header, out array))
			{
				return false;
			}
			bool result;
			using (Base64Decoder base64Decoder = new Base64Decoder())
			{
				byte[] array2 = new byte[base64Decoder.GetMaxByteCount(array.Length)];
				int num = 0;
				int num2 = 0;
				bool flag;
				do
				{
					int num3;
					int num4;
					base64Decoder.Convert(array, num, array.Length - num, array2, num2, array2.Length - num2, true, out num3, out num4, out flag);
					num += num3;
					num2 += num4;
				}
				while (!flag);
				try
				{
					MdbefPropertyCollection mdbefPropertyCollection = MdbefPropertyCollection.Create(array2, 0, num2);
					parsedBlob = mdbefPropertyCollection.GetBytes();
					result = true;
				}
				catch (MdbefException arg)
				{
					ExTraceGlobals.PickupTracer.TraceError<MdbefException>((long)this.GetHashCode(), "exch50 Exception {0}", arg);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04001F76 RID: 8054
		private const string XDash = "x-";

		// Token: 0x04001F77 RID: 8055
		private static readonly DecodingOptions Rfc2047DecodingOptions = new DecodingOptions(DecodingFlags.Rfc2047);

		// Token: 0x04001F78 RID: 8056
		private TransportMailItem transportMailItem;

		// Token: 0x04001F79 RID: 8057
		private ReplayFileMailer.XHeaderType xheadersProcessed;

		// Token: 0x04001F7A RID: 8058
		private long expectedP2MessageSize;

		// Token: 0x04001F7B RID: 8059
		private string messageCreator;
	}
}
