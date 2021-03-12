using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.TextProcessing.Boomerang;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200041C RID: 1052
	internal static class Util
	{
		// Token: 0x0600305E RID: 12382 RVA: 0x000C0CC0 File Offset: 0x000BEEC0
		public static bool TryGetNextHopFqdnProperty(IDictionary<string, object> mailItemProperties, out string nextHopFqdn)
		{
			ArgumentValidator.ThrowIfNull("mailItemProperties", mailItemProperties);
			object obj;
			if (mailItemProperties.TryGetValue("Microsoft.Exchange.Transport.Proxy.NextHopFqdn", out obj))
			{
				nextHopFqdn = (obj as string);
				return !string.IsNullOrWhiteSpace(nextHopFqdn);
			}
			nextHopFqdn = null;
			return false;
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000C0D00 File Offset: 0x000BEF00
		public static IReadOnlyList<IPAddress> DetermineLocalIPAddresses(IExEventLog eventLog)
		{
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			List<IPAddress> result;
			NetworkInformationException ex;
			if (!LocalComputer.TryGetIPAddresses(out result, out ex))
			{
				eventLog.LogEvent(TransportEventLogConstants.Tuple_NetworkAdapterIPQueryFailed, null, new object[]
				{
					ex
				});
				return new List<IPAddress>();
			}
			return result;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000C0D43 File Offset: 0x000BEF43
		public static string FormatMailboxDeliveryReceiveConnectorName(string localServerName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("localServerName", localServerName);
			return string.Format("Default Mailbox Delivery {0}", localServerName);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000C0D5C File Offset: 0x000BEF5C
		public static ISmtpReceivePerfCounters CreateReceivePerfCounters(ReceiveConnector receiveConnector, ProcessTransportRole role)
		{
			ArgumentValidator.ThrowIfNull("receiveConnector", receiveConnector);
			string instanceName = VariantConfiguration.InvariantNoFlightingSnapshot.Transport.SmtpReceiveCountersStripServerName.Enabled ? Util.CounterInstanceNameFromConnector(receiveConnector) : receiveConnector.Name;
			if (role == ProcessTransportRole.FrontEnd)
			{
				return new SmtpReceivePerfCountersFrontendWrapper(instanceName);
			}
			SmtpReceivePerfCounters.SetCategoryName(Util.PerfCounterCategoryMap[role]);
			return new SmtpReceivePerfCountersWrapper(instanceName);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000C0DE0 File Offset: 0x000BEFE0
		public static ISmtpAvailabilityPerfCounters GetOrCreateAvailabilityPerfCounters(ConcurrentDictionary<string, ISmtpAvailabilityPerfCounters> perfCounterCache, ReceiveConnector receiveConnector, ProcessTransportRole role, int minimumAvailabilityConnectionsToMonitor)
		{
			ArgumentValidator.ThrowIfNull("perfCounterCache", perfCounterCache);
			ArgumentValidator.ThrowIfNull("receiveConnector", receiveConnector);
			string instanceName = Util.CounterInstanceNameFromConnector(receiveConnector);
			return perfCounterCache.GetOrAdd(instanceName, (string key) => new SmtpAvailabilityPerfCountersWrapper(role, instanceName, minimumAvailabilityConnectionsToMonitor));
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000C0E68 File Offset: 0x000BF068
		public static IPRangeRemote[] FilterIpRangesByAddressFamily(IPRange[] ranges, AddressFamily addressFamily)
		{
			ArgumentValidator.ThrowIfNull("ranges", ranges);
			if (ranges.Length == 0)
			{
				return new IPRangeRemote[0];
			}
			int num = ranges.Count((IPRange r) => addressFamily == r.LowerBound.AddressFamily);
			if (num > 0)
			{
				IPRangeRemote[] array = new IPRangeRemote[num];
				num = 0;
				foreach (IPRange iprange in ranges)
				{
					if (addressFamily == iprange.LowerBound.AddressFamily)
					{
						array[num++] = new IPRangeRemote(iprange);
					}
				}
				return array;
			}
			return new IPRangeRemote[0];
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000C0F28 File Offset: 0x000BF128
		public static List<ReceiveConnector> EnabledReceiveConnectorsForRole(IEnumerable<ReceiveConnector> connectors, ProcessTransportRole role)
		{
			ArgumentValidator.ThrowIfNull("connectors", connectors);
			ServerRole requiredServerRole = Util.ServerRoleFromTransportRole(role);
			return (from connector in connectors
			where connector.Enabled && (connector.TransportRole & requiredServerRole) != ServerRole.None
			select connector).ToList<ReceiveConnector>();
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000C0FA4 File Offset: 0x000BF1A4
		public static List<IPEndPoint> BindingsFromReceiveConnectors(IEnumerable<ReceiveConnector> connectors, ProcessTransportRole role)
		{
			ArgumentValidator.ThrowIfNull("connectors", connectors);
			ServerRole requiredServerRole = Util.ServerRoleFromTransportRole(role);
			List<IPEndPoint> list = new List<IPEndPoint>();
			foreach (ReceiveConnector receiveConnector in from connector in connectors
			where connector.Enabled && (connector.TransportRole & requiredServerRole) != ServerRole.None
			select connector)
			{
				list.AddRange(receiveConnector.Bindings.Select((IPBinding binding) => new IPEndPoint(binding.Address, binding.Port)));
			}
			return list;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000C1048 File Offset: 0x000BF248
		public static ServerRole ServerRoleFromTransportRole(ProcessTransportRole transportRole)
		{
			switch (transportRole)
			{
			case ProcessTransportRole.Hub:
			case ProcessTransportRole.Edge:
				return ServerRole.HubTransport;
			case ProcessTransportRole.FrontEnd:
				return ServerRole.FrontendTransport;
			case ProcessTransportRole.MailboxSubmission:
			case ProcessTransportRole.MailboxDelivery:
				return ServerRole.Mailbox;
			default:
				throw new ArgumentOutOfRangeException("transportRole");
			}
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000C1088 File Offset: 0x000BF288
		public static SmtpResponse SmtpBanner(ReceiveConnector receiveConnector, Func<string> getDefaultServerName, Version adminDisplayVersion, DateTime utcNow, bool isModernServer = false)
		{
			ArgumentValidator.ThrowIfNull("receiveConnector", receiveConnector);
			ArgumentValidator.ThrowIfNull("getDefaultServerName", getDefaultServerName);
			ArgumentValidator.ThrowIfNull("adminDisplayVersion", adminDisplayVersion);
			return SmtpResponse.Banner((receiveConnector.Fqdn == null) ? getDefaultServerName() : receiveConnector.Fqdn.Domain, adminDisplayVersion.ToString(), new DateHeader("Unused", utcNow.ToLocalTime()).Value, isModernServer);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000C10F4 File Offset: 0x000BF2F4
		public static string ExtractAuthUsernameToLog(AuthenticationContext authenticationContext)
		{
			ArgumentValidator.ThrowIfNull("authenticationContext", authenticationContext);
			string text = null;
			if (authenticationContext.UserNameBytes != null)
			{
				text = Encoding.ASCII.GetString(authenticationContext.UserNameBytes);
			}
			if (!string.IsNullOrEmpty(text))
			{
				return Util.RedactUserName(text);
			}
			return "NULL";
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000C113B File Offset: 0x000BF33B
		public static bool IsReadFailure(NetworkConnection.LazyAsyncResultWithTimeout readResult)
		{
			ArgumentValidator.ThrowIfNull("readResult", readResult);
			return readResult.Result != null || readResult.Buffer == null;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000C115C File Offset: 0x000BF35C
		public static bool LoadDirectTrustCertificate(ReceiveConnector receiveConnector, long connectionId, string thumbprint, DateTime utcNow, ICertificateCache certificateCache, IExEventLog eventLog, ITracer tracer, out IX509Certificate2 certificate)
		{
			ArgumentValidator.ThrowIfNull("receiveConnector", receiveConnector);
			ArgumentValidator.ThrowIfNull("certificateCache", certificateCache);
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			certificate = null;
			bool result = true;
			if ((receiveConnector.AuthMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None)
			{
				if (!string.IsNullOrEmpty(thumbprint))
				{
					tracer.TraceDebug<long>(connectionId, "SmtpInSession(id={0}). Loading Internal Transport Certificate for use with Exchange Server Auth.", connectionId);
					certificate = certificateCache.GetInternalTransportCertificate(thumbprint, eventLog);
					bool flag = false;
					if (certificate != null)
					{
						flag = CertificateExpiryCheck.CheckCertificateExpiry(certificate, eventLog, SmtpSessionCertificateUse.DirectTrust, null, utcNow);
					}
					else
					{
						tracer.TraceError<long>(connectionId, "SmtpInSession(id={0}). Internal Transport Certificate could not be loaded.", connectionId);
					}
					result = (certificate != null && !flag);
				}
				else
				{
					tracer.TraceError<long>(connectionId, "SmtpInSession(id={0}). this.SmtpInServer.ServerConfiguration.InternalTransportCertificateThumbprint is null or empty. Examine AD/ADAM server object", connectionId);
				}
			}
			return result;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000C1210 File Offset: 0x000BF410
		public static bool LoadStartTlsCertificate(ReceiveConnector receiveConnector, string advertisedFQDN, long connectionId, bool oneLevelWildcardMatch, DateTime utcNow, ICertificateCache certificateCache, IExEventLog eventLog, ITracer tracer, out IX509Certificate2 certificate)
		{
			ArgumentValidator.ThrowIfNull("receiveConnector", receiveConnector);
			ArgumentValidator.ThrowIfNull("certificateCache", certificateCache);
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			certificate = null;
			bool result = true;
			if ((receiveConnector.AuthMechanism & AuthMechanisms.Tls) != AuthMechanisms.None)
			{
				if (receiveConnector.TlsCertificateName != null)
				{
					certificateCache.TryFind(receiveConnector.TlsCertificateName, out certificate);
				}
				else
				{
					List<string> names = new List<string>
					{
						advertisedFQDN
					};
					if (!certificateCache.TryFind(names, false, WildcardMatchType.OneLevel, out certificate))
					{
						certificateCache.TryFind(names, true, oneLevelWildcardMatch ? WildcardMatchType.OneLevel : WildcardMatchType.MultiLevel, out certificate);
					}
				}
				bool flag = false;
				if (certificate == null)
				{
					Util.LogCertificateError((receiveConnector.TlsCertificateName != null) ? receiveConnector.TlsCertificateName.ToString() : advertisedFQDN, connectionId, receiveConnector.Name, eventLog, tracer);
				}
				else
				{
					flag = CertificateExpiryCheck.CheckCertificateExpiry(certificate, eventLog, SmtpSessionCertificateUse.STARTTLS, advertisedFQDN, utcNow);
				}
				result = (certificate != null && !flag);
			}
			return result;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000C12F8 File Offset: 0x000BF4F8
		private static void LogCertificateError(string tlsCertificateName, long connectionId, string receiveConnectorName, IExEventLog eventLog, ITracer tracer)
		{
			string text = string.Format("SmtpInSession(id={0}). Can't load STARTTLS certificate for {1}", connectionId, tlsCertificateName);
			tracer.TraceError(connectionId, text);
			eventLog.LogEvent(TransportEventLogConstants.Tuple_CannotLoadSTARTTLSCertificateFromStore, receiveConnectorName, new object[]
			{
				tlsCertificateName,
				receiveConnectorName
			});
			EventNotificationItem.PublishPeriodic(ExchangeComponent.Transport.Name, "CannotLoadSTARTTLSCertificateFromStore", null, string.Format("Connector: '{0}' Error: '{1}'", receiveConnectorName, text), "CannotLoadSTARTTLSCertificateFromStore", TimeSpan.FromMinutes(5.0), ResultSeverityLevel.Error, false);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000C1374 File Offset: 0x000BF574
		public static int CalculateHoursBetween(DateTime now, DateTime then)
		{
			return Convert.ToInt32(then.Subtract(now).TotalHours);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000C1398 File Offset: 0x000BF598
		public static void CloseMessageWriteStream(Stream messageWriteStream, TransportMailItem transportMailItem, ITracer tracer, int traceCorrelation)
		{
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			try
			{
				if (messageWriteStream != null)
				{
					if (transportMailItem != null)
					{
						transportMailItem.ResetMimeParserEohCallback();
					}
					messageWriteStream.Close();
				}
			}
			catch (ExchangeDataException arg)
			{
				tracer.TraceError<ExchangeDataException>((long)traceCorrelation, "MessageWriteStream.Close threw exception: {0}", arg);
			}
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000C13E8 File Offset: 0x000BF5E8
		public static SmtpReceiveCapabilities SessionCapabilitiesFromTlsAndNonTlsCapabilities(SecureState secureState, SmtpReceiveCapabilities nonTlsCapabilities, SmtpReceiveCapabilities? tlsDomainCapabilities)
		{
			if (secureState != SecureState.StartTls)
			{
				return nonTlsCapabilities;
			}
			if (tlsDomainCapabilities == null)
			{
				return SmtpReceiveCapabilities.None;
			}
			return tlsDomainCapabilities.Value | nonTlsCapabilities;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000C1404 File Offset: 0x000BF604
		public static Permission AddSessionPermissions(SmtpReceiveCapabilities capabilities, Permission existingPermissions, IAuthzAuthorization authzAuthorization, RawSecurityDescriptor connectorSecurityDescriptor, IProtocolLogSession protocolLogSession, ITracer tracer, int hashcode)
		{
			ArgumentValidator.ThrowIfNull("authzAuthorization", authzAuthorization);
			ArgumentValidator.ThrowIfNull("connectorSecurityDescriptor", connectorSecurityDescriptor);
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			Permission permission = existingPermissions;
			if (SmtpInSessionUtils.HasAcceptOrgHeadersCapability(capabilities))
			{
				permission |= Permission.AcceptOrganizationHeaders;
			}
			if (SmtpInSessionUtils.HasAcceptCloudServicesMailCapability(capabilities))
			{
				permission |= (Permission.SMTPSubmit | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.AcceptRoutingHeaders);
			}
			if (SmtpInSessionUtils.HasAllowSubmitCapability(capabilities))
			{
				permission |= (Permission.SMTPSubmit | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.AcceptRoutingHeaders);
			}
			if (SmtpInSessionUtils.HasAcceptCrossForestMailCapability(capabilities))
			{
				permission |= SmtpInSessionUtils.GetPermissions(authzAuthorization, WellKnownSids.ExternallySecuredServers, connectorSecurityDescriptor);
				permission |= (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe);
			}
			if (permission != existingPermissions)
			{
				tracer.TraceDebug<Permission>((long)hashcode, "TLS Capabilities granted the following permissions: {0}", permission);
				protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, Util.AsciiStringToBytes(Util.GetPermissionString(permission)), "Set Session Permissions");
			}
			return permission;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000C14BD File Offset: 0x000BF6BD
		public static bool TryDetermineTlsDomainCapabilities(ICertificateValidator certificateValidator, X509Certificate2 tlsRemoteCertificate, ChainValidityStatus tlsRemoteCertificateChainValidationStatus, SmtpReceiveConnectorStub receiveConnectorStub, IProtocolLogSession protocolLogSession, ExEventLog eventLog, ITracer tracer, out SmtpReceiveCapabilities tlsDomainCapabilities)
		{
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			return Util.TryDetermineTlsDomainCapabilities(certificateValidator, (tlsRemoteCertificate == null) ? null : new X509Certificate2Wrapper(tlsRemoteCertificate), tlsRemoteCertificateChainValidationStatus, receiveConnectorStub, protocolLogSession, new ExEventLogWrapper(eventLog), tracer, out tlsDomainCapabilities);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000C14EC File Offset: 0x000BF6EC
		public static bool TryDetermineTlsDomainCapabilities(ICertificateValidator certificateValidator, IX509Certificate2 tlsRemoteCertificate, ChainValidityStatus tlsRemoteCertificateChainValidationStatus, SmtpReceiveConnectorStub receiveConnectorStub, IProtocolLogSession protocolLogSession, IExEventLog eventLogger, ITracer tracer, out SmtpReceiveCapabilities tlsDomainCapabilities)
		{
			ArgumentValidator.ThrowIfNull("certificateValidator", certificateValidator);
			ArgumentValidator.ThrowIfNull("receiveConnectorStub", receiveConnectorStub);
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			ArgumentValidator.ThrowIfNull("eventLogger", eventLogger);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			tlsDomainCapabilities = SmtpReceiveCapabilities.None;
			if (tlsRemoteCertificate == null)
			{
				protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "TlsDomainCapabilities='None'; Status='NoRemoteCertificate'");
				tracer.TraceDebug(0L, "Setting TlsDomainCapabilities to None because no remote certificate is present");
				return true;
			}
			SmtpReceiveDomainCapabilities smtpReceiveDomainCapabilities;
			if (receiveConnectorStub.TryGetTlsDomainCapabilities(certificateValidator, tlsRemoteCertificate, protocolLogSession, out smtpReceiveDomainCapabilities) && smtpReceiveDomainCapabilities.Capabilities != SmtpReceiveCapabilities.None && tlsRemoteCertificateChainValidationStatus != ChainValidityStatus.Valid)
			{
				tracer.TraceError<SmtpReceiveDomainCapabilities, string, ChainValidityStatus>(0L, "Failed to confirm TLS domain capabilities '{0}' on connector '{1}' because the TLS client certificate chain failed to validate and returned status '{2}'.", smtpReceiveDomainCapabilities, receiveConnectorStub.Connector.Name, tlsRemoteCertificateChainValidationStatus);
				eventLogger.LogEvent(TransportEventLogConstants.Tuple_TlsDomainCapabilitiesCertificateValidationFailure, smtpReceiveDomainCapabilities.Domain.Domain, new object[]
				{
					smtpReceiveDomainCapabilities,
					receiveConnectorStub.Connector.Name,
					tlsRemoteCertificateChainValidationStatus,
					smtpReceiveDomainCapabilities.Domain.Domain
				});
				return false;
			}
			if (smtpReceiveDomainCapabilities != null)
			{
				tlsDomainCapabilities = smtpReceiveDomainCapabilities.Capabilities;
			}
			protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "TlsDomainCapabilities='{0}'; Status='Success'; Domain='{1}'", new object[]
			{
				tlsDomainCapabilities,
				(smtpReceiveDomainCapabilities == null) ? string.Empty : smtpReceiveDomainCapabilities.Domain.Domain
			});
			tracer.TraceDebug<SmtpReceiveDomainCapabilities, string>(0L, "Using session TLS domain capabilities '{0}' for connector '{1}'.", smtpReceiveDomainCapabilities, receiveConnectorStub.Connector.Name);
			return true;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000C1637 File Offset: 0x000BF837
		public static string AdvertisedDomainFromReceiveConnector(ReceiveConnector receiveConnector, Func<string> serverFqdn)
		{
			ArgumentValidator.ThrowIfNull("receiveConnector", receiveConnector);
			ArgumentValidator.ThrowIfNull("serverFqdn", serverFqdn);
			if (receiveConnector.Fqdn != null)
			{
				return receiveConnector.Fqdn.Domain;
			}
			return serverFqdn();
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000C166C File Offset: 0x000BF86C
		public static string EnsureAscii(string input)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("input", input);
			return Encoding.ASCII.GetString(Encoding.Convert(Encoding.Unicode, Encoding.GetEncoding(Encoding.ASCII.EncodingName, new EncoderReplacementFallback(string.Empty), new DecoderExceptionFallback()), Encoding.Unicode.GetBytes(input)));
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000C16C4 File Offset: 0x000BF8C4
		public static Task<object> WriteToClientAsync(INetworkConnection networkConnection, SmtpResponse smtpResponse)
		{
			ArgumentValidator.ThrowIfNull("networkConnection", networkConnection);
			if (smtpResponse.IsEmpty)
			{
				return Util.CompletedNullTask;
			}
			byte[] array = smtpResponse.ToByteArray();
			return networkConnection.WriteAsync(array, 0, array.Length);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000C1700 File Offset: 0x000BF900
		public static DisconnectReason DisconnectReasonFromError(object error)
		{
			ArgumentValidator.ThrowIfNull("error", error);
			DisconnectReason result = DisconnectReason.Local;
			if (error is SocketError)
			{
				result = Util.DisconnectReasonFromSocketError((SocketError)error);
			}
			else if (error is SecurityStatus)
			{
				result = DisconnectReason.DroppedSession;
			}
			return result;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000C173C File Offset: 0x000BF93C
		public static DisconnectReason DisconnectReasonFromSocketError(SocketError socketError)
		{
			switch (socketError)
			{
			case SocketError.Shutdown:
				return DisconnectReason.Local;
			case SocketError.TimedOut:
				return DisconnectReason.Timeout;
			}
			return DisconnectReason.Remote;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000C176C File Offset: 0x000BF96C
		public static ChainValidityStatus CalculateTlsRemoteCertificateChainValidationStatus(bool clientCertificateChainValidationEnabled, ICertificateValidator certificateValidator, X509Certificate2 tlsRemoteCertificate, IProtocolLogSession protocolLogSession, ExEventLog eventLogger)
		{
			ArgumentValidator.ThrowIfNull("certificateValidator", certificateValidator);
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			ArgumentValidator.ThrowIfNull("eventLogger", eventLogger);
			if (!clientCertificateChainValidationEnabled)
			{
				protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Client certificate chain validation status: 'TestBypassed'");
				return ChainValidityStatus.Valid;
			}
			if (tlsRemoteCertificate == null)
			{
				return ChainValidityStatus.EmptyCertificate;
			}
			return Util.CalculateTlsRemoteCertificateChainValidationStatus(true, certificateValidator, new X509Certificate2Wrapper(tlsRemoteCertificate), protocolLogSession, new ExEventLogWrapper(eventLogger));
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000C17C8 File Offset: 0x000BF9C8
		public static ChainValidityStatus CalculateTlsRemoteCertificateChainValidationStatus(bool clientCertificateChainValidationEnabled, ICertificateValidator certificateValidator, IX509Certificate2 tlsRemoteCertificate, IProtocolLogSession protocolLogSession, IExEventLog eventLog)
		{
			ArgumentValidator.ThrowIfNull("certificateValidator", certificateValidator);
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			if (!clientCertificateChainValidationEnabled)
			{
				protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Client certificate chain validation status: 'TestBypassed'");
				return ChainValidityStatus.Valid;
			}
			ChainValidityStatus chainValidityStatus = certificateValidator.ChainValidateAsAnonymous(tlsRemoteCertificate, true);
			if (chainValidityStatus == ChainValidityStatus.Valid)
			{
				protocolLogSession.LogCertificate("Validated received certificate", tlsRemoteCertificate);
				return ChainValidityStatus.Valid;
			}
			if (certificateValidator.ShouldTreatValidationResultAsSuccess(chainValidityStatus))
			{
				protocolLogSession.LogCertificate(string.Format(CultureInfo.InvariantCulture, "CRL validation failed with status {0}. Treating the failure as success.", new object[]
				{
					chainValidityStatus
				}), tlsRemoteCertificate);
				eventLog.LogEvent(TransportEventLogConstants.Tuple_CertificateRevocationListCheckTrasientFailureTreatedAsSuccess, tlsRemoteCertificate.SerialNumber, new object[]
				{
					chainValidityStatus.ToString(),
					tlsRemoteCertificate.SerialNumber,
					tlsRemoteCertificate.Subject,
					tlsRemoteCertificate.Issuer,
					tlsRemoteCertificate.Thumbprint,
					"SmtpIn"
				});
				return ChainValidityStatus.Valid;
			}
			protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Client certificate chain validation status: '{0}'", new object[]
			{
				chainValidityStatus
			});
			return chainValidityStatus;
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000C18C9 File Offset: 0x000BFAC9
		public static bool IsFrontEndRole(ProcessTransportRole role)
		{
			return role == ProcessTransportRole.FrontEnd;
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000C18CF File Offset: 0x000BFACF
		public static bool IsHubOrFrontEndRole(ProcessTransportRole role)
		{
			return role == ProcessTransportRole.Hub || Util.IsFrontEndRole(role);
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000C18DC File Offset: 0x000BFADC
		public static bool IsMailboxTransportRole(ProcessTransportRole role)
		{
			return role == ProcessTransportRole.MailboxSubmission || role == ProcessTransportRole.MailboxDelivery;
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000C18E8 File Offset: 0x000BFAE8
		public static void LogTlsSuccessResult(IProtocolLogSession logSession, ConnectionInfo tlsConnectionInfo, IX509Certificate2 remoteCertificate)
		{
			ArgumentValidator.ThrowIfNull("logSession", logSession);
			ArgumentValidator.ThrowIfNull("tlsConnectionInfo", tlsConnectionInfo);
			if (remoteCertificate != null)
			{
				logSession.LogCertificate("Remote certificate", remoteCertificate);
			}
			logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, Util.FormatTlsSuccessResult(tlsConnectionInfo));
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000C1920 File Offset: 0x000BFB20
		public static string FormatTlsSuccessResult(ConnectionInfo tlsConnectionInfo)
		{
			ArgumentValidator.ThrowIfNull("tlsConnectionInfo", tlsConnectionInfo);
			return string.Format("TLS protocol {0} negotiation succeeded using bulk encryption algorithm {1} with strength {2} bits, MAC hash algorithm {3} with strength {4} bits and key exchange algorithm {5} with strength {6} bits", new object[]
			{
				(SchannelProtocols)tlsConnectionInfo.Protocol,
				(AlgorithmId)tlsConnectionInfo.Cipher,
				tlsConnectionInfo.CipherStrength,
				(AlgorithmId)tlsConnectionInfo.MACHashAlgorithm,
				tlsConnectionInfo.MACHashStrength,
				(AlgorithmId)tlsConnectionInfo.KeyExchangeAlgorithm,
				tlsConnectionInfo.KeyExchangeStrength
			});
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000C19AC File Offset: 0x000BFBAC
		internal static bool IsDataRedactionNecessary()
		{
			return MultiTenantTransport.MultiTenancyEnabled;
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000C19B4 File Offset: 0x000BFBB4
		internal static byte[] CreateRedactedCommand(byte[] prefixPart, RoutingAddress addressPartInCommand, string tailPartInCommand, bool redactionNecessary)
		{
			byte[] array = ByteString.StringToBytes(Parse821.FormatAddressLine(SmtpCommand.GetBracketedString(Util.RedactIfNecessary(addressPartInCommand, redactionNecessary)), tailPartInCommand), true);
			byte[] array2 = new byte[prefixPart.Length + array.Length];
			Array.Copy(prefixPart, array2, prefixPart.Length);
			Array.Copy(array, 0, array2, prefixPart.Length, array.Length);
			return array2;
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000C1A00 File Offset: 0x000BFC00
		internal static string Redact(RoutingAddress input)
		{
			return Util.RedactIfNecessary(input, Util.IsDataRedactionNecessary());
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000C1A0D File Offset: 0x000BFC0D
		internal static string Redact(string input)
		{
			return Util.RedactIfNecessary(input, Util.IsDataRedactionNecessary());
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000C1A1C File Offset: 0x000BFC1C
		internal static string RedactIfNecessary(RoutingAddress input, bool redactionNecessary)
		{
			if (redactionNecessary)
			{
				return SuppressingPiiData.Redact(input).ToString();
			}
			return input.ToString();
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000C1A4E File Offset: 0x000BFC4E
		internal static string RedactIfNecessary(string input, bool redactionNecessary)
		{
			if (redactionNecessary)
			{
				return SuppressingPiiData.Redact(input);
			}
			return input;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000C1A5C File Offset: 0x000BFC5C
		internal static void SplitUsernameIfRequired(string username, out string domainAndSlash, out string usernameWithoutDomain)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("username", username);
			domainAndSlash = string.Empty;
			usernameWithoutDomain = username;
			int num = username.LastIndexOf('\\');
			if (num == -1)
			{
				num = username.LastIndexOf('/');
			}
			if (num != -1 && num != username.Length - 1)
			{
				domainAndSlash = username.Substring(0, num + 1);
				usernameWithoutDomain = username.Substring(num + 1);
			}
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000C1ABC File Offset: 0x000BFCBC
		internal static string RedactUserName(string input)
		{
			if (string.IsNullOrEmpty(input) || !Util.IsDataRedactionNecessary())
			{
				return input;
			}
			string str;
			string input2;
			Util.SplitUsernameIfRequired(input, out str, out input2);
			return str + Util.Redact(input2);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000C1AF0 File Offset: 0x000BFCF0
		internal static void ConvertMessageClassificationsFromTnefToHeaders(TransportMailItem transportMailItem)
		{
			string text = transportMailItem.Message.MapiProperties.GetProperty(EmailMessageHelpers.TnefNameTagClassificationGuid) as string;
			if (!string.IsNullOrEmpty(text))
			{
				ClassificationUtils.PromoteIfUnclassified(transportMailItem.RootPart.Headers, text);
				transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagIsClassified, null);
				transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagClassification, null);
				transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagClassificationDescription, null);
				transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagClassificationGuid, null);
				transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagClassificationKeep, null);
			}
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000C1BA0 File Offset: 0x000BFDA0
		internal static void SetMessageClassificationTnef(TransportMailItem transportMailItem, bool isClassified, string classification, string classificationDescription, string classificationGuid, bool retainClassification)
		{
			transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagIsClassified, isClassified);
			transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagClassification, classification);
			transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagClassificationDescription, classificationDescription);
			transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagClassificationGuid, classificationGuid);
			transportMailItem.Message.MapiProperties.SetProperty(EmailMessageHelpers.TnefNameTagClassificationKeep, retainClassification);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000C1C28 File Offset: 0x000BFE28
		internal static byte[] AsciiStringToBytes(string value)
		{
			byte[] array = new byte[value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				array[i] = ((value[i] < '\u0080') ? ((byte)value[i]) : 63);
			}
			return array;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000C1C70 File Offset: 0x000BFE70
		internal static byte[] AsciiStringToBytes(string value, int startIndex, int length)
		{
			byte[] array = new byte[length];
			if (value.Length < startIndex + length)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			int num = 0;
			for (int i = startIndex; i < startIndex + length; i++)
			{
				array[num++] = ((value[i] < '\u0080') ? ((byte)value[i]) : 63);
			}
			return array;
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000C1CCC File Offset: 0x000BFECC
		internal static byte[] AsciiStringToBytesAndAppendCRLF(string value)
		{
			byte[] array = new byte[value.Length + 2];
			int i;
			for (i = 0; i < value.Length; i++)
			{
				array[i] = ((value[i] < '\u0080') ? ((byte)value[i]) : 63);
			}
			array[i++] = 13;
			array[i++] = 10;
			return array;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000C1D28 File Offset: 0x000BFF28
		internal static void PatchHeaders(HeaderList headerList, ReceivedHeader receivedHeader, RoutingAddress fromAddress, DateTime dateReceived, string fqdn, bool isHubTransportServer, out string msgId)
		{
			if (receivedHeader != null)
			{
				headerList.PrependChild(receivedHeader);
			}
			Header header = headerList.FindFirst("Message-ID");
			if (header == null || header.Value == null)
			{
				if (string.IsNullOrEmpty(fqdn))
				{
					fqdn = "localhost";
				}
				msgId = BoomerangProvider.Instance.GenerateBoomerangMessageId(fromAddress.ToString(), fqdn, Guid.Empty);
				if (header == null)
				{
					header = Header.Create("Message-ID");
					headerList.AppendChild(header);
				}
				header.Value = msgId;
			}
			else
			{
				msgId = header.Value;
			}
			if (!Util.SeenFrom(headerList))
			{
				header = Header.Create(HeaderId.From);
				MimeRecipient newChild = MimeRecipient.Parse((string)fromAddress, AddressParserFlags.IgnoreComments);
				header.AppendChild(newChild);
				headerList.AppendChild(header);
			}
			if (!Util.SeenRcpt(headerList))
			{
				header = Header.Create(HeaderId.To);
				header.AppendChild(new MimeGroup("Undisclosed recipients"));
				headerList.AppendChild(header);
			}
			if (headerList.FindFirst(HeaderId.ReturnPath) == null)
			{
				header = Header.Create(HeaderId.ReturnPath);
				header.Value = fromAddress.ToString();
				headerList.AppendChild(header);
			}
			DateHeader dateHeader = headerList.FindFirst(HeaderId.Date) as DateHeader;
			if (dateHeader == null)
			{
				dateHeader = new DateHeader("Date", dateReceived.ToLocalTime());
				headerList.AppendChild(dateHeader);
			}
			else if (string.IsNullOrEmpty(dateHeader.Value))
			{
				dateHeader.DateTime = dateReceived.ToLocalTime();
			}
			if (headerList.FindFirst("X-MS-Exchange-Organization-OriginalArrivalTime") == null)
			{
				header = new AsciiTextHeader("X-MS-Exchange-Organization-OriginalArrivalTime", Util.FormatOrganizationalMessageArrivalTime(dateReceived));
				headerList.AppendChild(header);
			}
			if (isHubTransportServer && headerList.FindFirst("X-MS-Exchange-Forest-ArrivalHubServer") == null)
			{
				header = new AsciiTextHeader("X-MS-Exchange-Forest-ArrivalHubServer", fqdn);
				headerList.AppendChild(header);
			}
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000C1ECC File Offset: 0x000C00CC
		internal static IPAddress ExtractFromIPAddress(ReceivedHeader receivedHeader)
		{
			IPAddress ipaddress = null;
			string[] array = new string[]
			{
				receivedHeader.FromTcpInfo,
				receivedHeader.From
			};
			foreach (string tcpInfo in array)
			{
				ipaddress = Util.ExtractIPAddress(tcpInfo, '[', ']');
				if (ipaddress == null)
				{
					ipaddress = Util.ExtractIPAddress(tcpInfo, '(', ')');
				}
				if (ipaddress != null)
				{
					break;
				}
			}
			return ipaddress;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000C1F30 File Offset: 0x000C0130
		internal static bool ExtractEmailAddressesFromString(string compositeString, out List<string> emailAddressMatches)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("compositeString", compositeString);
			emailAddressMatches = null;
			Regex regex = new Regex(Util.EmailAddressPattern, RegexOptions.IgnoreCase);
			MatchCollection matchCollection = regex.Matches(compositeString);
			if (matchCollection.Count == 0)
			{
				return false;
			}
			emailAddressMatches = new List<string>();
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				emailAddressMatches.Add(match.Value);
			}
			return true;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000C1FC4 File Offset: 0x000C01C4
		internal static bool IsReceivedHeaderFromAddressTrusted(IList<IPRange> internalSmtpServers, ReceivedHeader receivedHeader)
		{
			IPAddress ipAddress = Util.ExtractFromIPAddress(receivedHeader);
			return Util.IsTrustedIP(ipAddress, internalSmtpServers);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000C1FE0 File Offset: 0x000C01E0
		internal static bool IsTrustedIP(IPAddress ipAddress, IList<IPRange> internalSmtpServers)
		{
			if (ipAddress != null && internalSmtpServers != null)
			{
				foreach (IPRange iprange in internalSmtpServers)
				{
					if (iprange.Contains(ipAddress))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000C2038 File Offset: 0x000C0238
		internal static bool IsPickupReceivedHeader(ReceivedHeader receivedHeader)
		{
			string from = receivedHeader.From;
			return !string.IsNullOrEmpty(from) && (LatencyTracker.TrustExternalPickupReceivedHeaders && (from.Equals("pickup", StringComparison.OrdinalIgnoreCase) || from.Equals("mail pickup service", StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000C207C File Offset: 0x000C027C
		internal static IPAddress FindOriginatingIPFromHeadersAndStampOriginalClientServerIP(Header[] receivedHeaders, IPAddress localEndPointIpAddress, HeaderList headers, IList<IPRange> internalSmtpServers)
		{
			IPAddress ipaddress = localEndPointIpAddress;
			IPAddress ipaddress2 = null;
			foreach (ReceivedHeader receivedHeader in receivedHeaders)
			{
				IPAddress ipaddress3 = Util.ExtractFromIPAddress(receivedHeader);
				if (Util.IsTrustedIP(ipaddress3, internalSmtpServers))
				{
					ipaddress = ipaddress3;
				}
				else if (!Util.IsPickupReceivedHeader(receivedHeader))
				{
					ipaddress2 = ipaddress3;
					break;
				}
			}
			if (ipaddress2 != null && ipaddress != null && (!Util.HeaderValueIsParsableIPAddress(headers, "X-MS-Exchange-Organization-OriginalClientIPAddress") || !Util.HeaderValueIsParsableIPAddress(headers, "X-MS-Exchange-Organization-OriginalServerIPAddress")))
			{
				Util.SetAsciiHeader(headers, "X-MS-Exchange-Organization-OriginalClientIPAddress", ipaddress2.ToString());
				Util.SetAsciiHeader(headers, "X-MS-Exchange-Organization-OriginalServerIPAddress", ipaddress.ToString());
			}
			return ipaddress2;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000C2113 File Offset: 0x000C0313
		internal static bool SeenFrom(HeaderList headerList)
		{
			return Util.IsOneOfHeadersPresent(headerList, Util.fromHeaders);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000C2120 File Offset: 0x000C0320
		internal static bool SeenRcpt(HeaderList headerList)
		{
			return Util.IsOneOfHeadersPresent(headerList, Util.toHeaders);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000C2130 File Offset: 0x000C0330
		internal static bool IsOneOfHeadersPresent(HeaderList headerList, string[] headers)
		{
			foreach (string name in headers)
			{
				if (headerList.FindFirst(name) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000C2164 File Offset: 0x000C0364
		internal static string FormatOrganizationalMessageArrivalTime(DateTime time)
		{
			return time.ToUniversalTime().ToString("dd MMM yyyy HH\\:mm\\:ss\\.ffff \\(\\U\\T\\C\\)", DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000C218A File Offset: 0x000C038A
		internal static bool TryParseOrganizationalMessageArrivalTime(string value, out DateTime time)
		{
			return DateTime.TryParseExact(value, "dd MMM yyyy HH\\:mm\\:ss\\.ffff \\(\\U\\T\\C\\)", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeUniversal, out time);
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000C21A0 File Offset: 0x000C03A0
		internal static bool TryGetOrganizationalMessageArrivalTime(IReadOnlyMailItem mailItem, out DateTime orgArrivalTime)
		{
			orgArrivalTime = DateTime.MinValue;
			Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-OriginalArrivalTime");
			if (header == null)
			{
				return false;
			}
			string value;
			try
			{
				value = header.Value;
			}
			catch (ExchangeDataException)
			{
				return false;
			}
			return Util.TryParseOrganizationalMessageArrivalTime(value, out orgArrivalTime);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000C2200 File Offset: 0x000C0400
		internal static bool TryGetLastResubmitTime(IReadOnlyMailItem mailItem, out DateTime lastResubmitTime)
		{
			DateTime dateTime = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
			lastResubmitTime = dateTime;
			for (Header header = mailItem.RootPart.Headers.FindFirst(HeaderId.Received); header != null; header = mailItem.RootPart.Headers.FindNext(header))
			{
				ReceivedHeader receivedHeader = header as ReceivedHeader;
				if (receivedHeader != null && (receivedHeader.With == "MailboxResubmission" || receivedHeader.With == "ShadowRedundancy"))
				{
					lastResubmitTime = DateHeader.ParseDateHeaderValue(receivedHeader.Date);
					break;
				}
			}
			return lastResubmitTime != dateTime;
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000C2298 File Offset: 0x000C0498
		internal static string GetPermissionString(Permission permissions)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			bool flag = true;
			foreach (KeyValuePair<Permission, string> keyValuePair in Util.PermissionList)
			{
				Permission key = keyValuePair.Key;
				if ((permissions & key) != Permission.None)
				{
					stringBuilder.Append(keyValuePair.Value);
					stringBuilder.Append(" ");
					if (flag)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				stringBuilder.Append("None");
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000C2338 File Offset: 0x000C0538
		internal static bool TryGetP2Sender(HeaderList headers, out RoutingAddress p2SenderAddress)
		{
			p2SenderAddress = Util.RetrieveRoutingAddress(headers, HeaderId.Sender);
			if (!p2SenderAddress.IsValid || p2SenderAddress == RoutingAddress.NullReversePath)
			{
				p2SenderAddress = Util.RetrieveRoutingAddress(headers, HeaderId.From);
				if (!p2SenderAddress.IsValid || p2SenderAddress == RoutingAddress.NullReversePath)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000C2398 File Offset: 0x000C0598
		internal static RoutingAddress GetPurportedResponsibleAddress(HeaderList headerList)
		{
			Header header = Util.SelectPRAHeader(headerList);
			return Util.ExtractPRAFromHeader(header);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000C23B4 File Offset: 0x000C05B4
		internal static RoutingAddress RetrieveRoutingAddress(HeaderList headers, HeaderId headerId)
		{
			RoutingAddress result = RoutingAddress.Empty;
			AddressHeader addressHeader = headers.FindFirst(headerId) as AddressHeader;
			if (addressHeader != null)
			{
				foreach (AddressItem addressItem in addressHeader)
				{
					MimeRecipient mimeRecipient = addressItem as MimeRecipient;
					if (mimeRecipient != null)
					{
						result = (RoutingAddress)mimeRecipient.Email;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000C242C File Offset: 0x000C062C
		internal static string GetHeaderValue(Header header)
		{
			string text;
			if (header.TryGetValue(out text))
			{
				return text.Trim();
			}
			ExTraceGlobals.GeneralTracer.TraceError<string>(0L, "Failed to extract value of header '{0}'.", header.Name);
			return string.Empty;
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000C2466 File Offset: 0x000C0666
		internal static bool SetAsciiHeader(HeaderList headers, string name, string value)
		{
			return Util.SetHeader(headers, name, value, true);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000C2474 File Offset: 0x000C0674
		internal static bool SetHeader(HeaderList headers, string name, string value, bool onlyAscii)
		{
			Header header = headers.FindFirst(name);
			if (header != null && headers.FindNext(header) != null)
			{
				header = null;
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Removed multiple instances of header {0}", name);
			}
			if (header == null)
			{
				headers.AppendChild(onlyAscii ? new AsciiTextHeader(name, value) : new TextHeader(name, value));
				ExTraceGlobals.GeneralTracer.TraceDebug<string, string>(0L, "Added header {0}: {1}", name, value);
				return true;
			}
			if (!string.Equals(header.Value, value, StringComparison.OrdinalIgnoreCase))
			{
				header.Value = value;
				ExTraceGlobals.GeneralTracer.TraceDebug<string, string>(0L, "Set header {0}: {1}", name, value);
				return true;
			}
			return false;
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000C2508 File Offset: 0x000C0708
		internal static void AppendAsciiHeader(HeaderList headers, string name, string value)
		{
			Header header = headers.FindFirst(name);
			for (Header header2 = header; header2 != null; header2 = headers.FindNext(header2))
			{
				header = header2;
			}
			headers.InsertAfter(new AsciiTextHeader(name, value), header ?? headers.LastChild);
			ExTraceGlobals.GeneralTracer.TraceDebug<string, string>(0L, "Appended header {0}: {1}", name, value);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000C255A File Offset: 0x000C075A
		internal static bool IsLongAddress(RoutingAddress address)
		{
			return address.Length > 571 || (address.Length > 316 && address.LocalPart.Length > 315);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000C258F File Offset: 0x000C078F
		internal static bool IsLongAddressForE2k3(RoutingAddress address)
		{
			return Util.IsLongAddressForE2k3(address.ToString());
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000C25A3 File Offset: 0x000C07A3
		internal static bool IsLongAddressForE2k3(string address)
		{
			return address.Length > 314;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000C25B4 File Offset: 0x000C07B4
		internal static bool IsValidInnerAddress(RoutingAddress address)
		{
			X400ProxyAddress x400ProxyAddress;
			return Util.TryDeencapsulateX400(address, out x400ProxyAddress);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000C25CC File Offset: 0x000C07CC
		internal static bool TryDeencapsulateX400(RoutingAddress address, out X400ProxyAddress x400Address)
		{
			ProxyAddress proxyAddress;
			if (!SmtpProxyAddress.TryDeencapsulate(address.ToString(), out proxyAddress))
			{
				x400Address = null;
				return false;
			}
			x400Address = (proxyAddress as X400ProxyAddress);
			return x400Address != null;
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000C2604 File Offset: 0x000C0804
		internal static bool IsValidAddress(RoutingAddress address)
		{
			return address.IsValid && (!Util.IsLongAddress(address) || Util.IsValidInnerAddress(address));
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000C2624 File Offset: 0x000C0824
		internal static bool TryGetShortAddress(RoutingAddress longAddress, out RoutingAddress shortAddress)
		{
			shortAddress = RoutingAddress.Empty;
			X400ProxyAddress x400ProxyAddress;
			if (!Util.TryDeencapsulateX400(longAddress, out x400ProxyAddress))
			{
				return false;
			}
			string text;
			X400AddressParser.GetCanonical(x400ProxyAddress.AddressString, true, out text);
			string domainPart = longAddress.DomainPart;
			SmtpProxyAddress smtpProxyAddress;
			if (longAddress.Length > text.Length && SmtpProxyAddress.TryEncapsulate(ProxyAddressPrefix.X400.PrimaryPrefix, text, domainPart, out smtpProxyAddress) && !Util.IsLongAddressForE2k3(smtpProxyAddress.SmtpAddress))
			{
				shortAddress = (RoutingAddress)smtpProxyAddress.SmtpAddress;
			}
			if (shortAddress == RoutingAddress.Empty)
			{
				shortAddress = Util.GetHashAddress(x400ProxyAddress.AddressString, domainPart);
			}
			return true;
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000C26C8 File Offset: 0x000C08C8
		internal static void SetOorgHeaders(HeaderList headers, string oorg)
		{
			headers.RemoveAll("X-MS-Exchange-Organization-OriginatorOrganization");
			foreach (string name in MimeConstant.XOriginatorOrganization)
			{
				headers.RemoveAll(name);
			}
			if (!string.IsNullOrEmpty(oorg))
			{
				Util.SetAsciiHeader(headers, "X-MS-Exchange-Organization-OriginatorOrganization", oorg);
				Util.SetAsciiHeader(headers, MimeConstant.PreferredXOriginatorOrganization, oorg);
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000C2724 File Offset: 0x000C0924
		internal static void SetScopeHeaders(HeaderList headers, Guid? scope)
		{
			if (scope == null)
			{
				headers.RemoveAll("X-MS-Exchange-Organization-MessageScope");
				headers.RemoveAll("X-MS-Exchange-Forest-MessageScope");
				return;
			}
			Util.SetAsciiHeader(headers, "X-MS-Exchange-Organization-MessageScope", scope.Value.ToString());
			Util.SetAsciiHeader(headers, "X-MS-Exchange-Forest-MessageScope", scope.Value.ToString());
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000C2794 File Offset: 0x000C0994
		internal static bool IsLocalHopCountExceeded(IEnumerable<Header> receivedHeaders, string advertisedDomain, int localLoopSubdomainDepth, int maxLocalHopCount, out int localHopCount)
		{
			localHopCount = 0;
			foreach (Header header in receivedHeaders)
			{
				ReceivedHeader receivedHeader = header as ReceivedHeader;
				if (receivedHeader != null && !string.IsNullOrEmpty(receivedHeader.By) && !string.IsNullOrEmpty(receivedHeader.With) && receivedHeader.With.StartsWith("Microsoft SMTP Server", StringComparison.OrdinalIgnoreCase) && ((localLoopSubdomainDepth > 0 && Util.CompareSubdomains(localLoopSubdomainDepth, receivedHeader.By, advertisedDomain)) || (localLoopSubdomainDepth == 0 && string.Equals(receivedHeader.By, advertisedDomain, StringComparison.OrdinalIgnoreCase))))
				{
					localHopCount++;
					if (localHopCount > maxLocalHopCount)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000C284C File Offset: 0x000C0A4C
		internal static RoutingAddress FixSmtpAddressWithInvalidLocalPart(string invalidAddress)
		{
			if (string.IsNullOrEmpty(invalidAddress))
			{
				return RoutingAddress.Empty;
			}
			int num = invalidAddress.IndexOf('@');
			if (num == -1 || num == 0 || num > 315)
			{
				return RoutingAddress.Empty;
			}
			string text = invalidAddress.Substring(0, num);
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < num; i++)
			{
				if (text[i] == '.')
				{
					if (i == num - 1 && num > 2 && text[i - 1] != '.')
					{
						flag2 = true;
						break;
					}
					if (i > 2 && text[i - 1] == '.')
					{
						if (flag)
						{
							flag2 = false;
							break;
						}
						flag = true;
					}
				}
				else if (flag)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				return new RoutingAddress(string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
				{
					text
				}), invalidAddress.Substring(num + 1));
			}
			return RoutingAddress.Empty;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000C2923 File Offset: 0x000C0B23
		internal static void FixP2HeadersWithInvalidLocalPart(HeaderList msgHeaders)
		{
			Util.FixHeadersIfNeeded(msgHeaders);
			Util.FixReturnPathIfNeeded(msgHeaders);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000C2931 File Offset: 0x000C0B31
		internal static bool DowngradeCustomPermanentFailure(ErrorPolicies errorPolicies, SmtpResponse response, ITransportAppConfig transportAppConfig)
		{
			return Util.IsDowngradeErrorPolicySpecified(errorPolicies) && Util.ListContainsMatchingSmtpResponse(transportAppConfig.SmtpSendConfiguration.DowngradedResponses, response, new Util.IsMatchingStatusText(Util.IsExactMatchingStatusText));
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000C295A File Offset: 0x000C0B5A
		internal static bool UpgradeCustomPermanentFailure(ErrorPolicies errorPolicies, SmtpResponse response, ITransportAppConfig transportAppConfig)
		{
			return (transportAppConfig.SmtpSendConfiguration.TreatTransientErrorsAsPermanentErrors || Util.IsUpgradeErrorPolicySpecified(errorPolicies)) && Util.ListContainsMatchingSmtpResponse(transportAppConfig.SmtpSendConfiguration.UpgradedResponses, response, new Util.IsMatchingStatusText(Util.IsWildcardMatchingStatusTextFound));
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000C2990 File Offset: 0x000C0B90
		internal static void EncodeAndSetPriorityAsHeader(HeaderList headers, DeliveryPriority priority, string reason)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = stringBuilder;
			int num = (int)priority;
			stringBuilder2.Append(num.ToString());
			if (priority != DeliveryPriority.Normal)
			{
				stringBuilder.Append(":");
				stringBuilder.Append(reason);
			}
			Util.SetHeader(headers, "X-MS-Exchange-Organization-Prioritization", stringBuilder.ToString(), false);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000C29E0 File Offset: 0x000C0BE0
		internal static bool DecodePriorityHeader(Header priorityHeader, out DeliveryPriority priority, out string reason)
		{
			if (priorityHeader != null && !string.IsNullOrEmpty(priorityHeader.Value))
			{
				string text = priorityHeader.Value;
				string text2 = string.Empty;
				int num = text.IndexOf(':');
				if (num != -1)
				{
					text = priorityHeader.Value.Substring(0, num);
					text2 = priorityHeader.Value.Substring(num + 1);
				}
				DeliveryPriority deliveryPriority;
				if (EnumValidator<DeliveryPriority>.TryParse(text, EnumParseOptions.AllowNumericConstants | EnumParseOptions.IgnoreCase, out deliveryPriority))
				{
					reason = text2;
					priority = deliveryPriority;
					return true;
				}
			}
			reason = string.Empty;
			priority = DeliveryPriority.Normal;
			return false;
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000C2A54 File Offset: 0x000C0C54
		internal static bool MatchSmtpResponse(SmtpResponse pattern, SmtpResponse response, Util.IsMatchingStatusText statusTextMatcher)
		{
			return Util.IsMatchingStatusCode(pattern.StatusCode, response.StatusCode) && Util.IsMatchingStatusCode(pattern.EnhancedStatusCode, response.EnhancedStatusCode) && statusTextMatcher(pattern.StatusText, response.StatusText);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000C2AA1 File Offset: 0x000C0CA1
		internal static bool InterlockedEquals(ref int lhs, int rhs)
		{
			return Interlocked.CompareExchange(ref lhs, rhs, rhs) == rhs;
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000C2AB0 File Offset: 0x000C0CB0
		public static bool TryGetUserPrincipalNameForXproxy(TransportMiniRecipient principal, bool smtpXproxyConstructUpnFromSamAccountNameAndParitionFqdn, out string userPrincipalName)
		{
			ArgumentValidator.ThrowIfNull("principal", principal);
			if (smtpXproxyConstructUpnFromSamAccountNameAndParitionFqdn)
			{
				try
				{
					if (null != principal.OrganizationId)
					{
						userPrincipalName = string.Format("{0}@{1}", principal.SamAccountName, principal.OrganizationId.PartitionId.ForestFQDN);
						return true;
					}
				}
				catch (ValueNotPresentException)
				{
				}
				userPrincipalName = null;
				return false;
			}
			userPrincipalName = principal.UserPrincipalName;
			return !string.IsNullOrEmpty(userPrincipalName);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000C2B30 File Offset: 0x000C0D30
		internal static Permission GetPermissionsForSid(SecurityIdentifier sid, RawSecurityDescriptor securityDescriptor, IAuthzAuthorization authzAuthorization, string sidDescriptionForLog, string connectorName, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("sid", sid);
			ArgumentValidator.ThrowIfNull("authzAuthorization", authzAuthorization);
			ArgumentValidator.ThrowIfNullOrEmpty("sidDescriptionForLog", sidDescriptionForLog);
			ArgumentValidator.ThrowIfNullOrEmpty("connectorName", connectorName);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			Permission permission = Permission.None;
			try
			{
				if (securityDescriptor != null)
				{
					permission = authzAuthorization.CheckPermissions(sid, securityDescriptor, null);
					tracer.TraceDebug<string, Permission>(0L, "{0} is granted the following permissions {1}", sidDescriptionForLog, permission);
				}
				else
				{
					tracer.TraceError<string>(0L, "Connector {0} security descriptor is null", connectorName);
				}
			}
			catch (Win32Exception ex)
			{
				tracer.TraceError<int>(0L, "AuthzAuthorization.CheckPermissions failed with {0}.", ex.NativeErrorCode);
			}
			return permission;
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000C2BD4 File Offset: 0x000C0DD4
		private static Header SelectPRAHeader(HeaderList headers)
		{
			ArgumentValidator.ThrowIfNull("headers", headers);
			Header header = null;
			Header header2 = null;
			Header header3 = null;
			Header header4 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (Header header5 in headers)
			{
				try
				{
					if (!header5.HasChildren && header5.Value == null)
					{
						continue;
					}
				}
				catch (ExchangeDataException)
				{
					continue;
				}
				HeaderId headerId = header5.HeaderId;
				switch (headerId)
				{
				case HeaderId.Received:
					goto IL_98;
				case HeaderId.Date:
				case HeaderId.Subject:
					break;
				case HeaderId.From:
					if (header4 == null)
					{
						header4 = header5;
					}
					else
					{
						flag3 = true;
					}
					break;
				case HeaderId.Sender:
					if (header3 == null)
					{
						header3 = header5;
					}
					else
					{
						flag2 = true;
					}
					break;
				default:
					if (headerId == HeaderId.ReturnPath)
					{
						goto IL_98;
					}
					switch (headerId)
					{
					case HeaderId.ResentSender:
						header = header5;
						flag = true;
						break;
					case HeaderId.ResentFrom:
						header2 = header5;
						break;
					}
					break;
				}
				IL_B8:
				if (!flag)
				{
					continue;
				}
				break;
				IL_98:
				if (header2 != null)
				{
					flag = true;
					goto IL_B8;
				}
				goto IL_B8;
			}
			Header result = null;
			if (header != null)
			{
				result = header;
			}
			else if (header2 != null)
			{
				result = header2;
			}
			else if (header3 != null)
			{
				if (!flag2)
				{
					result = header3;
				}
			}
			else if (header4 != null && !flag3)
			{
				result = header4;
			}
			return result;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000C2D00 File Offset: 0x000C0F00
		private static RoutingAddress ExtractPRAFromHeader(Header header)
		{
			RoutingAddress routingAddress = Util.NoPRA;
			if (header != null)
			{
				foreach (AddressItem addressItem in ((AddressHeader)header))
				{
					MimeRecipient mimeRecipient = addressItem as MimeRecipient;
					if (mimeRecipient != null)
					{
						if (routingAddress != Util.NoPRA)
						{
							return Util.NoPRA;
						}
						routingAddress = (RoutingAddress)mimeRecipient.Email;
					}
					else
					{
						MimeGroup mimeGroup = addressItem as MimeGroup;
						if (mimeGroup != null)
						{
							foreach (MimeRecipient mimeRecipient2 in mimeGroup)
							{
								if (routingAddress != Util.NoPRA)
								{
									return Util.NoPRA;
								}
								routingAddress = (RoutingAddress)mimeRecipient2.Email;
							}
						}
					}
				}
				return routingAddress;
			}
			return routingAddress;
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000C2E18 File Offset: 0x000C1018
		private static bool ListContainsMatchingSmtpResponse(IEnumerable<SmtpResponse> patternList, SmtpResponse response, Util.IsMatchingStatusText statusTextMatcher)
		{
			return patternList.Any((SmtpResponse pattern) => Util.MatchSmtpResponse(pattern, response, statusTextMatcher));
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000C2E4C File Offset: 0x000C104C
		private static void FixHeadersIfNeeded(HeaderList msgHeaders)
		{
			foreach (HeaderId headerId in Util.P2HeadersToFix)
			{
				Header[] array = msgHeaders.FindAll(headerId);
				if (array != null)
				{
					foreach (Header header in array)
					{
						Util.FixHeader(header);
					}
				}
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000C2EA4 File Offset: 0x000C10A4
		private static void FixHeader(Header header)
		{
			if (header == null)
			{
				return;
			}
			for (MimeNode mimeNode = header.FirstChild; mimeNode != null; mimeNode = mimeNode.NextSibling)
			{
				MimeRecipient mimeRecipient = mimeNode as MimeRecipient;
				if (mimeRecipient != null && !string.IsNullOrEmpty(mimeRecipient.Email) && !RoutingAddress.IsValidAddress(mimeRecipient.Email))
				{
					RoutingAddress value = Util.FixSmtpAddressWithInvalidLocalPart(mimeRecipient.Email);
					if (value != RoutingAddress.Empty && value.IsValid)
					{
						mimeRecipient.Email = value.ToString();
					}
				}
			}
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000C2F30 File Offset: 0x000C1130
		private static void FixReturnPathIfNeeded(HeaderList msgHeaders)
		{
			AsciiTextHeader asciiTextHeader = msgHeaders.FindFirst(HeaderId.ReturnPath) as AsciiTextHeader;
			if (asciiTextHeader != null)
			{
				char[] trimChars = new char[]
				{
					'<',
					'>',
					' ',
					'\t'
				};
				string text = asciiTextHeader.Value.Trim(trimChars);
				if (!RoutingAddress.IsValidAddress(text))
				{
					RoutingAddress value = Util.FixSmtpAddressWithInvalidLocalPart(text);
					if (value != RoutingAddress.Empty && value.IsValid)
					{
						asciiTextHeader.Value = string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[]
						{
							value.ToString()
						});
					}
				}
			}
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000C2FC4 File Offset: 0x000C11C4
		private static bool CompareSubdomains(int subdomainDepth, string domain1, string domain2)
		{
			if (string.IsNullOrEmpty(domain1) || string.IsNullOrEmpty(domain2) || subdomainDepth <= 0)
			{
				return false;
			}
			int num = domain1.Length - 1;
			int num2 = domain2.Length - 1;
			if (domain1[num] == '.')
			{
				num--;
			}
			if (domain2[num2] == '.')
			{
				num2--;
			}
			int num3 = 0;
			while (num >= 0 && num2 >= 0)
			{
				if (domain1[num] == '.')
				{
					if (domain2[num2] != '.')
					{
						return false;
					}
					num3++;
					if (num3 == subdomainDepth + 1)
					{
						return true;
					}
				}
				else if (char.ToLowerInvariant(domain1[num]) != char.ToLowerInvariant(domain2[num2]))
				{
					return false;
				}
				num--;
				num2--;
			}
			return num3 == subdomainDepth && ((num == -1 && num2 == -1) || (num == -1 && num2 > 0 && domain2[num2] == '.') || (num2 == -1 && num > 0 && domain1[num] == '.'));
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000C30A0 File Offset: 0x000C12A0
		private static RoutingAddress GetHashAddress(string x400Address, string domain)
		{
			return new RoutingAddress(string.Format(CultureInfo.InvariantCulture, "DMS_ENCAPSULATED_{0:X}", new object[]
			{
				x400Address.GetHashCode()
			}), domain);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000C30D8 File Offset: 0x000C12D8
		private static bool HeaderValueIsParsableIPAddress(HeaderList headers, string headerName)
		{
			Header header = headers.FindFirst(headerName);
			IPAddress ipaddress;
			return header != null && !string.IsNullOrEmpty(header.Value) && IPAddress.TryParse(header.Value, out ipaddress);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000C3110 File Offset: 0x000C1310
		private static IPAddress ExtractIPAddress(string tcpInfo, char startDelimiter, char endDelimiter)
		{
			IPAddress result = null;
			if (!string.IsNullOrEmpty(tcpInfo) && !IPAddress.TryParse(tcpInfo, out result))
			{
				int num = -1;
				for (int i = 0; i < tcpInfo.Length; i++)
				{
					if (tcpInfo[i] == startDelimiter)
					{
						num = i + 1;
					}
					else if (tcpInfo[i] == endDelimiter && num != -1)
					{
						if (num != i)
						{
							string ipString = tcpInfo.Substring(num, i - num);
							if (IPAddress.TryParse(ipString, out result))
							{
								break;
							}
							num = -1;
						}
						else
						{
							num = -1;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000C3184 File Offset: 0x000C1384
		private static string ConstructRedactionFailureAddressToLog(RoutingAddress input)
		{
			return new RoutingAddress("RedactionFailed", input.DomainPart).ToString();
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000C31B0 File Offset: 0x000C13B0
		private static bool IsMatchingStatusCode(string expectedCode, string actualCode)
		{
			return string.IsNullOrEmpty(expectedCode) || string.Equals(expectedCode, actualCode, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000C31E0 File Offset: 0x000C13E0
		private static bool IsExactMatchingStatusText(string[] expectedStatusText, string[] actualStatusText)
		{
			if (expectedStatusText != null && expectedStatusText.Length != 0)
			{
				if (actualStatusText != null && expectedStatusText.Length == actualStatusText.Length)
				{
					if (!expectedStatusText.Where((string expectedValue, int i) => !Util.IsExactMatchingStatusText(expectedValue, actualStatusText[i])).Any<string>())
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000C3268 File Offset: 0x000C1468
		private static bool IsWildcardMatchingStatusTextFound(string[] expectedStatusText, string[] actualStatusText)
		{
			if (expectedStatusText != null && expectedStatusText.Length != 0)
			{
				if (actualStatusText == null || actualStatusText.Length == 0)
				{
					return false;
				}
				if (expectedStatusText.Length == 1)
				{
					return actualStatusText.Any((string status) => Util.IsWildcardMatchingStatusText(expectedStatusText[0], status));
				}
				if (expectedStatusText.Length == actualStatusText.Length)
				{
					if (!expectedStatusText.Where((string expectedValue, int i) => !Util.IsWildcardMatchingStatusText(expectedValue, actualStatusText[i])).Any<string>())
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000C3313 File Offset: 0x000C1513
		private static bool IsExactMatchingStatusText(string expectedStatusText, string actualStatusText)
		{
			return string.Equals(expectedStatusText, actualStatusText, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000C3320 File Offset: 0x000C1520
		private static bool IsWildcardMatchingStatusText(string expectedStatusText, string actualStatusText)
		{
			Regex regex = new Regex(expectedStatusText, RegexOptions.IgnoreCase);
			return regex.IsMatch(actualStatusText);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000C333C File Offset: 0x000C153C
		private static bool IsDowngradeErrorPolicySpecified(ErrorPolicies errorPolicies)
		{
			return (errorPolicies & ErrorPolicies.DowngradeCustomFailures) != ErrorPolicies.Default;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000C3347 File Offset: 0x000C1547
		private static bool IsUpgradeErrorPolicySpecified(ErrorPolicies errorPolicies)
		{
			return (errorPolicies & ErrorPolicies.UpgradeCustomFailures) != ErrorPolicies.Default;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000C3354 File Offset: 0x000C1554
		private static string CounterInstanceNameFromConnector(ReceiveConnector connector)
		{
			string text = connector.Name;
			string name = connector.Server.Name;
			if (text.EndsWith(name, StringComparison.OrdinalIgnoreCase))
			{
				int num = text.Length - name.Length - 1;
				if (num > 0 && text[num].Equals(' '))
				{
					text = text.Remove(num);
				}
			}
			return text;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000C33B0 File Offset: 0x000C15B0
		private static List<KeyValuePair<Permission, string>> GetPermissionList()
		{
			List<KeyValuePair<Permission, string>> list = new List<KeyValuePair<Permission, string>>();
			foreach (object obj in Enum.GetValues(typeof(Permission)))
			{
				Permission permission = (Permission)obj;
				if (permission != Permission.All)
				{
					string name = Enum.GetName(typeof(Permission), permission);
					list.Add(new KeyValuePair<Permission, string>(permission, name));
				}
			}
			return list;
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000C35D4 File Offset: 0x000C17D4
		public static async Task<NetworkConnection.LazyAsyncResultWithTimeout> ReadLineAsync(SmtpInSessionState sessionState)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			NetworkConnection.LazyAsyncResultWithTimeout readResult = await sessionState.NetworkConnection.ReadLineAsync();
			NetworkConnection.LazyAsyncResultWithTimeout result;
			if (Util.IsReadFailure(readResult))
			{
				result = readResult;
			}
			else
			{
				bool overflow = readResult.Result is SocketError && (SocketError)readResult.Result == SocketError.MessageSize;
				ISmtpReceivePerfCounters perfCounters = sessionState.ReceivePerfCounters;
				if (perfCounters != null)
				{
					perfCounters.TotalBytesReceived.IncrementBy((long)(readResult.Size + (overflow ? 0 : 2)));
				}
				result = readResult;
			}
			return result;
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000C3764 File Offset: 0x000C1964
		public static async Task<NetworkConnection.LazyAsyncResultWithTimeout> ReadAsync(SmtpInSessionState sessionState)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			NetworkConnection.LazyAsyncResultWithTimeout readResult = await sessionState.NetworkConnection.ReadAsync();
			NetworkConnection.LazyAsyncResultWithTimeout result;
			if (Util.IsReadFailure(readResult))
			{
				result = readResult;
			}
			else
			{
				ISmtpReceivePerfCounters perfCounters = sessionState.ReceivePerfCounters;
				if (perfCounters != null)
				{
					perfCounters.TotalBytesReceived.IncrementBy((long)readResult.Size);
				}
				result = readResult;
			}
			return result;
		}

		// Token: 0x040017A2 RID: 6050
		internal const string BackpressureTarpitThrottling = "Back Pressure";

		// Token: 0x040017A3 RID: 6051
		internal const string CRLF = "\r\n";

		// Token: 0x040017A4 RID: 6052
		internal const int MillisecsPerMinute = 60000;

		// Token: 0x040017A5 RID: 6053
		internal const string OrganizationalMessageArrivalTimeHeader = "X-MS-Exchange-Organization-OriginalArrivalTime";

		// Token: 0x040017A6 RID: 6054
		private const string OrganizationalMessageArrivalTimeFormat = "dd MMM yyyy HH\\:mm\\:ss\\.ffff \\(\\U\\T\\C\\)";

		// Token: 0x040017A7 RID: 6055
		private const string UndisclosedRecipients = "Undisclosed recipients";

		// Token: 0x040017A8 RID: 6056
		private const string RedactionFailed = "RedactionFailed";

		// Token: 0x040017A9 RID: 6057
		internal static readonly byte[] AsciiCRLF = Encoding.ASCII.GetBytes("\r\n");

		// Token: 0x040017AA RID: 6058
		private static readonly Task<object> CompletedNullTask = System.Threading.Tasks.Task.FromResult<object>(null);

		// Token: 0x040017AB RID: 6059
		internal static readonly string EmailAddressPattern = "[a-zA-Z0-9-_.!#$%&*+-/=?^_|~]+@[a-zA-Z0-9-_.]+\\.[a-zA-Z]+";

		// Token: 0x040017AC RID: 6060
		internal static readonly RoutingAddress NoPRA = new RoutingAddress("invalid address");

		// Token: 0x040017AD RID: 6061
		internal static readonly byte[] LowerC = new byte[]
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			62,
			63,
			64,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
			91,
			92,
			93,
			94,
			95,
			96,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
			123,
			124,
			125,
			126,
			127,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x040017AE RID: 6062
		private static readonly string[] fromHeaders = new string[]
		{
			"from",
			"sender",
			"resent-sender",
			"resent-from",
			"resent-reply-to",
			"reply-to",
			"return-receipt-to",
			"errors-to"
		};

		// Token: 0x040017AF RID: 6063
		private static readonly string[] toHeaders = new string[]
		{
			"to",
			"resent-to",
			"cc",
			"resent-cc",
			"bcc",
			"resent-bcc",
			"apparently-to"
		};

		// Token: 0x040017B0 RID: 6064
		private static readonly HeaderId[] P2HeadersToFix = new HeaderId[]
		{
			HeaderId.From,
			HeaderId.To,
			HeaderId.Cc,
			HeaderId.Sender,
			HeaderId.ReplyTo,
			HeaderId.ReturnReceiptTo,
			HeaderId.DispositionNotificationTo,
			HeaderId.ResentFrom,
			HeaderId.ResentTo
		};

		// Token: 0x040017B1 RID: 6065
		private static readonly IReadOnlyDictionary<ProcessTransportRole, string> PerfCounterCategoryMap = new Dictionary<ProcessTransportRole, string>
		{
			{
				ProcessTransportRole.Edge,
				"MSExchangeTransport SmtpReceive"
			},
			{
				ProcessTransportRole.Hub,
				"MSExchangeTransport SmtpReceive"
			},
			{
				ProcessTransportRole.FrontEnd,
				"MSExchangeFrontEndTransport SmtpReceive"
			},
			{
				ProcessTransportRole.MailboxDelivery,
				"MSExchange Delivery SmtpReceive"
			}
		};

		// Token: 0x040017B2 RID: 6066
		private static readonly List<KeyValuePair<Permission, string>> PermissionList = Util.GetPermissionList();

		// Token: 0x0200041D RID: 1053
		// (Invoke) Token: 0x060030CF RID: 12495
		internal delegate bool IsMatchingStatusText(string[] expectedStatusText, string[] actualStatusText);
	}
}
