using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000497 RID: 1175
	internal class XProxyToSmtpCommandParser
	{
		// Token: 0x06003561 RID: 13665 RVA: 0x000D9DE4 File Offset: 0x000D7FE4
		public XProxyToSmtpCommandParser(ITransportConfiguration transportConfiguration)
		{
			this.sendConnector = new ProxySendConnector("Outbound proxy virtual send connector", transportConfiguration.LocalServer.TransportServer, transportConfiguration.LocalServer.TransportServer.HomeRoutingGroup, null)
			{
				UseExternalDNSServersEnabled = true
			};
			this.tlsConfiguration = new TlsSendConfiguration();
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x000D9E37 File Offset: 0x000D8037
		public bool LastCommandSeen
		{
			get
			{
				return this.lastCommandSeen;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x000D9E3F File Offset: 0x000D803F
		public bool IsProbeConnection
		{
			get
			{
				return this.isProbeConnection;
			}
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000D9E48 File Offset: 0x000D8048
		public void GetProxySettings(out SmtpSendConnectorConfig theProxyConnector, out TlsSendConfiguration theTlsConfiguration, out RiskLevel theRiskLevel, out int theOutboundIPPool, out IEnumerable<INextHopServer> theProxyDestinations, out string theNextHopDomain, out string theSessionId)
		{
			if (!this.lastCommandSeen)
			{
				throw new InvalidOperationException("Proxy settings cannot be obtained before the last command has been received");
			}
			if (this.proxyDestinations == null)
			{
				throw new InvalidOperationException("Proxy settings cannot be obtained before destinations have been received");
			}
			theProxyConnector = this.sendConnector;
			theTlsConfiguration = this.tlsConfiguration;
			theRiskLevel = this.riskLevel;
			theOutboundIPPool = this.outboundIPPool;
			theProxyDestinations = this.proxyDestinations;
			theNextHopDomain = this.nextHopDomain;
			theSessionId = this.sessionId;
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000D9EB8 File Offset: 0x000D80B8
		public bool TryParseArguments(CommandContext commandContext, IExEventLog eventLogger, IEventNotificationItem eventNotificationItem, out SmtpResponse smtpResponse)
		{
			if (this.lastCommandSeen)
			{
				throw new InvalidOperationException("Cannot parse further commands after last command");
			}
			XProxyToParseOutput parseOutput;
			if (!XProxyToSmtpCommandParser.TryParseArguments(commandContext, eventLogger, eventNotificationItem, out parseOutput))
			{
				smtpResponse = SmtpResponse.TransientInvalidArguments;
				return false;
			}
			this.CopyParsedValuesToSavedState(parseOutput);
			if (this.lastCommandSeen && (this.proxyDestinations == null || this.proxyDestinations.Count == 0))
			{
				smtpResponse = SmtpResponse.NoDestinationsReceivedResponse;
				return false;
			}
			smtpResponse = SmtpResponse.Empty;
			return true;
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000D9F34 File Offset: 0x000D8134
		public static bool TryParseArguments(CommandContext commandContext, IExEventLog eventLogger, IEventNotificationItem eventNotificationItem, out XProxyToParseOutput parseOutput)
		{
			ArgumentValidator.ThrowIfNull("commandContext", commandContext);
			ArgumentValidator.ThrowIfNull("eventLogger", eventLogger);
			ArgumentValidator.ThrowIfNull("eventNotificationItem", eventNotificationItem);
			commandContext.TrimLeadingWhitespace();
			if (commandContext.IsEndOfCommand)
			{
				parseOutput = null;
				return false;
			}
			parseOutput = new XProxyToParseOutput();
			while (!commandContext.IsEndOfCommand)
			{
				Offset offset;
				if (!commandContext.GetNextArgumentOffset(out offset))
				{
					parseOutput = null;
					return false;
				}
				int nameValuePairSeparatorIndex = CommandParsingHelper.GetNameValuePairSeparatorIndex(commandContext.Command, offset, 61);
				if (nameValuePairSeparatorIndex <= 0 || nameValuePairSeparatorIndex >= offset.End - 1)
				{
					parseOutput = null;
					return false;
				}
				int count = nameValuePairSeparatorIndex - offset.Start;
				int argValueLen = offset.End - (nameValuePairSeparatorIndex + 1);
				bool flag = false;
				switch (Util.LowerC[(int)commandContext.Command[offset.Start]])
				{
				case 99:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.CertificateSubjectKeywordBytes, commandContext.Command, offset.Start, count))
					{
						string decodedCertificateSubject;
						if (!XProxyToSmtpCommandParser.TryGetCertificateSubject(commandContext, nameValuePairSeparatorIndex, argValueLen, eventLogger, eventNotificationItem, out decodedCertificateSubject))
						{
							flag = true;
						}
						else
						{
							parseOutput.DecodedCertificateSubject = decodedCertificateSubject;
						}
					}
					break;
				case 100:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.DestinationsKeywordBytes, commandContext.Command, offset.Start, count))
					{
						List<XProxyToNextHopServer> collection;
						if (!XProxyToSmtpCommandParser.GetMultiValuedParameter<XProxyToNextHopServer>(commandContext, nameValuePairSeparatorIndex, argValueLen, new XProxyToSmtpCommandParser.TryParseParameter<XProxyToNextHopServer>(XProxyToNextHopServer.TryParse), out collection))
						{
							flag = true;
						}
						else if (parseOutput.Destinations == null)
						{
							parseOutput.Destinations = new List<INextHopServer>(collection);
						}
						else
						{
							parseOutput.Destinations.AddRange(collection);
						}
					}
					else if (BufferParser.CompareArg(XProxyToSmtpCommandParser.DnsRoutingEnabledKeywordBytes, commandContext.Command, offset.Start, count))
					{
						parseOutput.IsDnsRoutingEnabled = XProxyToSmtpCommandParser.IsBooleanParameterEnabled(commandContext, nameValuePairSeparatorIndex, argValueLen);
					}
					break;
				case 101:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.ErrorPoliciesKeywordBytes, commandContext.Command, offset.Start, count))
					{
						ErrorPolicies? errorPolicies;
						if (!XProxyToSmtpCommandParser.GetEnumValue<ErrorPolicies>(commandContext, nameValuePairSeparatorIndex, argValueLen, out errorPolicies))
						{
							flag = true;
						}
						else
						{
							parseOutput.ErrorPolicies = errorPolicies;
						}
					}
					break;
				case 102:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.ForceHeloKeywordBytes, commandContext.Command, offset.Start, count))
					{
						parseOutput.ForceHelo = new bool?(XProxyToSmtpCommandParser.IsBooleanParameterEnabled(commandContext, nameValuePairSeparatorIndex, argValueLen));
					}
					else if (BufferParser.CompareArg(XProxyToSmtpCommandParser.FqdnKeywordBytes, commandContext.Command, offset.Start, count))
					{
						Fqdn fqdn;
						if (!XProxyToSmtpCommandParser.GetFqdn(commandContext, nameValuePairSeparatorIndex, argValueLen, out fqdn))
						{
							flag = true;
						}
						else
						{
							parseOutput.Fqdn = fqdn;
						}
					}
					break;
				case 108:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.LastKeywordBytes, commandContext.Command, offset.Start, count))
					{
						parseOutput.IsLast = new bool?(XProxyToSmtpCommandParser.IsBooleanParameterEnabled(commandContext, nameValuePairSeparatorIndex, argValueLen));
					}
					break;
				case 110:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.NextHopDomainKeywordBytes, commandContext.Command, offset.Start, count))
					{
						string text;
						if (!XProxyToSmtpCommandParser.GetStringValue(commandContext, nameValuePairSeparatorIndex, argValueLen, 2147483647, out text))
						{
							flag = true;
						}
						else
						{
							parseOutput.NextHopDomain = text;
						}
					}
					break;
				case 111:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.OutboundIPPoolKeywordBytes, commandContext.Command, offset.Start, count))
					{
						int? num;
						if (!XProxyToSmtpCommandParser.GetIntegerValue(commandContext, nameValuePairSeparatorIndex, argValueLen, 0, 65535, 2147483647, out num))
						{
							flag = true;
						}
						else
						{
							parseOutput.OutboundIPPool = num;
						}
					}
					break;
				case 112:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.PortKeywordBytes, commandContext.Command, offset.Start, count))
					{
						int? port;
						if (!XProxyToSmtpCommandParser.GetIntegerValue(commandContext, nameValuePairSeparatorIndex, argValueLen, 0, 65535, XProxyParserUtils.MaxClientPortLength, out port))
						{
							flag = true;
						}
						else
						{
							parseOutput.Port = port;
						}
					}
					else if (BufferParser.CompareArg(XProxyToSmtpCommandParser.ProbeKeywordBytes, commandContext.Command, offset.Start, count))
					{
						string a;
						if (!XProxyToSmtpCommandParser.GetStringValue(commandContext, nameValuePairSeparatorIndex, argValueLen, 1, out a))
						{
							flag = true;
						}
						else if (a == "1")
						{
							parseOutput.IsProbeConnection = true;
						}
					}
					break;
				case 114:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.RequireTlsKeywordBytes, commandContext.Command, offset.Start, count))
					{
						parseOutput.RequireTls = new bool?(XProxyToSmtpCommandParser.IsBooleanParameterEnabled(commandContext, nameValuePairSeparatorIndex, argValueLen));
					}
					else if (BufferParser.CompareArg(XProxyToSmtpCommandParser.RequireOorgKeywordBytes, commandContext.Command, offset.Start, count))
					{
						parseOutput.RequireOorg = new bool?(XProxyToSmtpCommandParser.IsBooleanParameterEnabled(commandContext, nameValuePairSeparatorIndex, argValueLen));
					}
					else if (BufferParser.CompareArg(XProxyToSmtpCommandParser.RiskLevelKeywordBytes, commandContext.Command, offset.Start, count))
					{
						RiskLevel? risk;
						if (!XProxyToSmtpCommandParser.GetEnumValue<RiskLevel>(commandContext, nameValuePairSeparatorIndex, argValueLen, out risk))
						{
							flag = true;
						}
						else
						{
							parseOutput.Risk = risk;
						}
					}
					break;
				case 115:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.ShouldSkipTlsKeywordBytes, commandContext.Command, offset.Start, count))
					{
						parseOutput.ShouldSkipTls = new bool?(XProxyToSmtpCommandParser.IsBooleanParameterEnabled(commandContext, nameValuePairSeparatorIndex, argValueLen));
					}
					else if (BufferParser.CompareArg(XProxyToSmtpCommandParser.SessionIdKeywordBytes, commandContext.Command, offset.Start, count))
					{
						string text2;
						if (!XProxyToSmtpCommandParser.GetStringValue(commandContext, nameValuePairSeparatorIndex, argValueLen, XProxyParserUtils.MaxSessionIdLength, out text2))
						{
							flag = true;
						}
						else
						{
							parseOutput.SessionId = text2;
						}
					}
					break;
				case 116:
					if (BufferParser.CompareArg(XProxyToSmtpCommandParser.TlsAuthLevelKeywordBytes, commandContext.Command, offset.Start, count))
					{
						RequiredTlsAuthLevel? tlsAuthLevel;
						if (!XProxyToSmtpCommandParser.GetEnumValue<RequiredTlsAuthLevel>(commandContext, nameValuePairSeparatorIndex, argValueLen, out tlsAuthLevel))
						{
							flag = true;
						}
						else
						{
							parseOutput.TlsAuthLevel = tlsAuthLevel;
						}
					}
					else if (BufferParser.CompareArg(XProxyToSmtpCommandParser.TlsDomainsKeywordBytes, commandContext.Command, offset.Start, count))
					{
						List<SmtpDomainWithSubdomains> collection2;
						if (!XProxyToSmtpCommandParser.GetMultiValuedParameter<SmtpDomainWithSubdomains>(commandContext, nameValuePairSeparatorIndex, argValueLen, new XProxyToSmtpCommandParser.TryParseParameter<SmtpDomainWithSubdomains>(SmtpDomainWithSubdomains.TryParse), out collection2))
						{
							flag = true;
						}
						else if (parseOutput.TlsDomains == null)
						{
							parseOutput.TlsDomains = new List<SmtpDomainWithSubdomains>(collection2);
						}
						else
						{
							parseOutput.TlsDomains.AddRange(collection2);
						}
					}
					break;
				}
				if (flag)
				{
					parseOutput = null;
					return false;
				}
				commandContext.TrimLeadingWhitespace();
			}
			return true;
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000DA4DC File Offset: 0x000D86DC
		private void CopyParsedValuesToSavedState(XProxyToParseOutput parseOutput)
		{
			if (parseOutput.Destinations != null && parseOutput.Destinations.Count > 0)
			{
				if (this.proxyDestinations == null)
				{
					this.proxyDestinations = new List<INextHopServer>(parseOutput.Destinations.Count);
				}
				foreach (INextHopServer item in parseOutput.Destinations)
				{
					this.proxyDestinations.Add(item);
				}
			}
			if (parseOutput.ForceHelo != null)
			{
				this.sendConnector.ForceHELO = parseOutput.ForceHelo.Value;
			}
			if (parseOutput.IsLast != null)
			{
				this.lastCommandSeen = parseOutput.IsLast.Value;
			}
			if (parseOutput.Port != null)
			{
				this.sendConnector.Port = parseOutput.Port.Value;
			}
			if (parseOutput.Fqdn != null)
			{
				this.sendConnector.Fqdn = parseOutput.Fqdn;
			}
			if (parseOutput.RequireTls != null)
			{
				this.tlsConfiguration.RequireTls = parseOutput.RequireTls.Value;
			}
			if (parseOutput.RequireOorg != null)
			{
				this.sendConnector.RequireOorg = parseOutput.RequireOorg.Value;
			}
			if (parseOutput.Risk != null)
			{
				this.riskLevel = parseOutput.Risk.Value;
			}
			if (parseOutput.OutboundIPPool != null)
			{
				this.outboundIPPool = parseOutput.OutboundIPPool.Value;
			}
			if (parseOutput.ShouldSkipTls != null)
			{
				this.tlsConfiguration.ShouldSkipTls = parseOutput.ShouldSkipTls.Value;
			}
			if (parseOutput.TlsAuthLevel != null)
			{
				this.tlsConfiguration.TlsAuthLevel = new RequiredTlsAuthLevel?(parseOutput.TlsAuthLevel.Value);
			}
			if (parseOutput.TlsDomains != null && parseOutput.TlsDomains.Count > 0)
			{
				if (this.tlsConfiguration.TlsDomains == null)
				{
					this.tlsConfiguration.TlsDomains = new List<SmtpDomainWithSubdomains>(parseOutput.TlsDomains.Count);
				}
				foreach (SmtpDomainWithSubdomains item2 in parseOutput.TlsDomains)
				{
					this.tlsConfiguration.TlsDomains.Add(item2);
				}
			}
			if (!string.IsNullOrEmpty(parseOutput.DecodedCertificateSubject))
			{
				SmtpX509Identifier tlsCertificateName;
				if (SmtpX509Identifier.TryParse(parseOutput.DecodedCertificateSubject, out tlsCertificateName))
				{
					this.tlsConfiguration.TlsCertificateName = tlsCertificateName;
				}
				else
				{
					this.tlsConfiguration.TlsCertificateFqdn = parseOutput.DecodedCertificateSubject;
				}
			}
			if (parseOutput.ErrorPolicies != null)
			{
				this.sendConnector.ErrorPolicies = parseOutput.ErrorPolicies.Value;
			}
			this.sendConnector.DNSRoutingEnabled = parseOutput.IsDnsRoutingEnabled;
			this.isProbeConnection = parseOutput.IsProbeConnection;
			this.nextHopDomain = parseOutput.NextHopDomain;
			this.sessionId = parseOutput.SessionId;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000DA820 File Offset: 0x000D8A20
		private static bool TryGetCertificateSubject(CommandContext commandContext, int separatorIndex, int argValueLen, IExEventLog eventLogger, IEventNotificationItem eventNotificationItem, out string decodedCertificate)
		{
			string text = SmtpUtils.FromXtextString(commandContext.Command, separatorIndex + 1, argValueLen, false);
			if (string.IsNullOrEmpty(text))
			{
				decodedCertificate = null;
				return false;
			}
			try
			{
				byte[] bytes = Convert.FromBase64String(text);
				decodedCertificate = Encoding.UTF7.GetString(bytes);
			}
			catch (FormatException ex)
			{
				eventLogger.LogEvent(TransportEventLogConstants.Tuple_XProxyToCommandInvalidEncodedCertificateSubject, null, new object[]
				{
					text,
					ex.ToString()
				});
				string notificationReason = string.Format("XProxyTo command expected a based64 encoded certificate subject, but the input certificate subject is not a valid one: {0}.  Exception: {1}", text, ex);
				eventNotificationItem.Publish(ExchangeComponent.Transport.Name, "XProxyToCommandInvalidEncodedCertificateSubject", null, notificationReason, ResultSeverityLevel.Error, false);
				decodedCertificate = null;
				return false;
			}
			return true;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000DA8D0 File Offset: 0x000D8AD0
		private static bool GetMultiValuedParameter<T>(CommandContext commandContext, int separatorIndex, int argValueLen, XProxyToSmtpCommandParser.TryParseParameter<T> tryParse, out List<T> parsedValues)
		{
			string text = SmtpUtils.FromXtextString(commandContext.Command, separatorIndex + 1, argValueLen, false);
			if (string.IsNullOrEmpty(text))
			{
				parsedValues = null;
				return false;
			}
			string[] array = text.Split(new char[]
			{
				','
			});
			parsedValues = new List<T>(array.Length);
			bool flag = false;
			foreach (string value in array)
			{
				T item;
				if (!tryParse(value, out item))
				{
					flag = true;
					break;
				}
				parsedValues.Add(item);
			}
			if (flag)
			{
				parsedValues = null;
				return false;
			}
			return true;
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000DA960 File Offset: 0x000D8B60
		private static bool IsBooleanParameterEnabled(CommandContext commandContext, int separatorIndex, int argValueLen)
		{
			string value = SmtpUtils.FromXtextString(commandContext.Command, separatorIndex + 1, argValueLen, false);
			bool flag;
			return !string.IsNullOrEmpty(value) && bool.TryParse(value, out flag) && flag;
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000DA998 File Offset: 0x000D8B98
		private static bool GetEnumValue<T>(CommandContext commandContext, int separatorIndex, int argValueLen, out T? parsedValue) where T : struct
		{
			string value = SmtpUtils.FromXtextString(commandContext.Command, separatorIndex + 1, argValueLen, false);
			if (string.IsNullOrEmpty(value))
			{
				parsedValue = null;
				return false;
			}
			T value2;
			if (Enum.TryParse<T>(value, out value2))
			{
				parsedValue = new T?(value2);
				return true;
			}
			parsedValue = null;
			return true;
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000DA9E8 File Offset: 0x000D8BE8
		private static bool GetIntegerValue(CommandContext commandContext, int separatorIndex, int argValueLen, int minValue, int maxValue, int maxValueLength, out int? parsedValue)
		{
			if (argValueLen > maxValueLength)
			{
				parsedValue = null;
				return false;
			}
			string text = SmtpUtils.FromXtextString(commandContext.Command, separatorIndex + 1, argValueLen, false);
			if (string.IsNullOrEmpty(text))
			{
				parsedValue = null;
				return false;
			}
			int num;
			if (!int.TryParse(text, out num) || num < minValue || num > maxValue)
			{
				parsedValue = null;
				return false;
			}
			parsedValue = new int?(num);
			return true;
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000DAA52 File Offset: 0x000D8C52
		private static bool GetStringValue(CommandContext commandContext, int separatorIndex, int argValueLen, int maxValueLength, out string parsedValue)
		{
			if (argValueLen > maxValueLength)
			{
				parsedValue = null;
				return false;
			}
			parsedValue = SmtpUtils.FromXtextString(commandContext.Command, separatorIndex + 1, argValueLen, false);
			return !string.IsNullOrEmpty(parsedValue);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000DAA7C File Offset: 0x000D8C7C
		private static bool GetFqdn(CommandContext commandContext, int separatorIndex, int argValueLen, out Fqdn parsedValue)
		{
			string text = SmtpUtils.FromXtextString(commandContext.Command, separatorIndex + 1, argValueLen, false);
			if (string.IsNullOrEmpty(text))
			{
				parsedValue = null;
				return false;
			}
			return Fqdn.TryParse(text, out parsedValue);
		}

		// Token: 0x04001B4B RID: 6987
		public const char MultiValueDelimiter = ',';

		// Token: 0x04001B4C RID: 6988
		public const string CommandKeyword = "XPROXYTO";

		// Token: 0x04001B4D RID: 6989
		public const string LastKeyword = "LAST";

		// Token: 0x04001B4E RID: 6990
		public const string ForceHeloKeyword = "FORCEHELO";

		// Token: 0x04001B4F RID: 6991
		public const string ShouldSkipTlsKeyword = "SHOULDSKIPTLS";

		// Token: 0x04001B50 RID: 6992
		public const string PortKeyword = "PORT";

		// Token: 0x04001B51 RID: 6993
		public const string ProbeKeyword = "PROBE";

		// Token: 0x04001B52 RID: 6994
		public const string RequireTlsKeyword = "REQUIRETLS";

		// Token: 0x04001B53 RID: 6995
		public const string RequireOorgKeyword = "REQUIREOORG";

		// Token: 0x04001B54 RID: 6996
		public const string TlsAuthLevelKeyword = "TLSAUTHLEVEL";

		// Token: 0x04001B55 RID: 6997
		public const string TlsDomainsKeyword = "TLSDOMAINS";

		// Token: 0x04001B56 RID: 6998
		public const string DestinationsKeyword = "DESTINATIONS";

		// Token: 0x04001B57 RID: 6999
		public const string RiskLevelKeyword = "RISK";

		// Token: 0x04001B58 RID: 7000
		public const string OutboundIPPoolKeyword = "OUTBOUNDIPPOOL";

		// Token: 0x04001B59 RID: 7001
		public const string SessionIdKeyword = "SESSIONID";

		// Token: 0x04001B5A RID: 7002
		public const string CertificateSubjectKeyword = "CERTSUBJECT";

		// Token: 0x04001B5B RID: 7003
		public const string NextHopDomainKeyword = "NEXTHOPDOMAIN";

		// Token: 0x04001B5C RID: 7004
		public const string FqdnKeyword = "FQDN";

		// Token: 0x04001B5D RID: 7005
		public const string ErrorPoliciesKeyword = "ERRORPOLICIES";

		// Token: 0x04001B5E RID: 7006
		public const string DnsRoutingEnabledKeyword = "DNSROUTING";

		// Token: 0x04001B5F RID: 7007
		public const string ProbeKeywordProbeConnectionValue = "1";

		// Token: 0x04001B60 RID: 7008
		private const string VirtualConnectorName = "Outbound proxy virtual send connector";

		// Token: 0x04001B61 RID: 7009
		private const int MaxProbeKeywordValueLength = 1;

		// Token: 0x04001B62 RID: 7010
		public static readonly int MaxCommandLength = 2000;

		// Token: 0x04001B63 RID: 7011
		private static readonly byte[] LastKeywordBytes = Util.AsciiStringToBytes("LAST".ToLower());

		// Token: 0x04001B64 RID: 7012
		private static readonly byte[] ForceHeloKeywordBytes = Util.AsciiStringToBytes("FORCEHELO".ToLower());

		// Token: 0x04001B65 RID: 7013
		private static readonly byte[] ShouldSkipTlsKeywordBytes = Util.AsciiStringToBytes("SHOULDSKIPTLS".ToLower());

		// Token: 0x04001B66 RID: 7014
		private static readonly byte[] PortKeywordBytes = Util.AsciiStringToBytes("PORT".ToLower());

		// Token: 0x04001B67 RID: 7015
		private static readonly byte[] ProbeKeywordBytes = Util.AsciiStringToBytes("PROBE".ToLower());

		// Token: 0x04001B68 RID: 7016
		private static readonly byte[] RequireTlsKeywordBytes = Util.AsciiStringToBytes("REQUIRETLS".ToLower());

		// Token: 0x04001B69 RID: 7017
		private static readonly byte[] RequireOorgKeywordBytes = Util.AsciiStringToBytes("REQUIREOORG".ToLower());

		// Token: 0x04001B6A RID: 7018
		private static readonly byte[] TlsAuthLevelKeywordBytes = Util.AsciiStringToBytes("TLSAUTHLEVEL".ToLower());

		// Token: 0x04001B6B RID: 7019
		private static readonly byte[] TlsDomainsKeywordBytes = Util.AsciiStringToBytes("TLSDOMAINS".ToLower());

		// Token: 0x04001B6C RID: 7020
		private static readonly byte[] DestinationsKeywordBytes = Util.AsciiStringToBytes("DESTINATIONS".ToLower());

		// Token: 0x04001B6D RID: 7021
		private static readonly byte[] RiskLevelKeywordBytes = Util.AsciiStringToBytes("RISK".ToLower());

		// Token: 0x04001B6E RID: 7022
		private static readonly byte[] OutboundIPPoolKeywordBytes = Util.AsciiStringToBytes("OUTBOUNDIPPOOL".ToLower());

		// Token: 0x04001B6F RID: 7023
		private static readonly byte[] SessionIdKeywordBytes = Util.AsciiStringToBytes("SESSIONID".ToLower());

		// Token: 0x04001B70 RID: 7024
		private static readonly byte[] CertificateSubjectKeywordBytes = Util.AsciiStringToBytes("CERTSUBJECT".ToLower());

		// Token: 0x04001B71 RID: 7025
		private static readonly byte[] NextHopDomainKeywordBytes = Util.AsciiStringToBytes("NEXTHOPDOMAIN".ToLower());

		// Token: 0x04001B72 RID: 7026
		private static readonly byte[] FqdnKeywordBytes = Util.AsciiStringToBytes("FQDN".ToLower());

		// Token: 0x04001B73 RID: 7027
		private static readonly byte[] ErrorPoliciesKeywordBytes = Util.AsciiStringToBytes("ERRORPOLICIES".ToLower());

		// Token: 0x04001B74 RID: 7028
		private static readonly byte[] DnsRoutingEnabledKeywordBytes = Util.AsciiStringToBytes("DNSROUTING".ToLower());

		// Token: 0x04001B75 RID: 7029
		private readonly SmtpSendConnectorConfig sendConnector;

		// Token: 0x04001B76 RID: 7030
		private readonly TlsSendConfiguration tlsConfiguration;

		// Token: 0x04001B77 RID: 7031
		private RiskLevel riskLevel;

		// Token: 0x04001B78 RID: 7032
		private int outboundIPPool;

		// Token: 0x04001B79 RID: 7033
		private List<INextHopServer> proxyDestinations;

		// Token: 0x04001B7A RID: 7034
		private string nextHopDomain;

		// Token: 0x04001B7B RID: 7035
		private string sessionId;

		// Token: 0x04001B7C RID: 7036
		private bool lastCommandSeen;

		// Token: 0x04001B7D RID: 7037
		private bool isProbeConnection;

		// Token: 0x02000498 RID: 1176
		// (Invoke) Token: 0x06003571 RID: 13681
		private delegate bool TryParseParameter<T>(string value, out T parsedValues);
	}
}
