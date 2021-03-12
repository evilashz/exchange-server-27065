using System;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.Net.WSSecurity;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000B2 RID: 178
	internal sealed class ProxyAuthenticator
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000109DE File Offset: 0x0000EBDE
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x000109E6 File Offset: 0x0000EBE6
		public AuthenticatorType AuthenticatorType { get; private set; }

		// Token: 0x060003FD RID: 1021 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public static ProxyAuthenticator Create(NetworkCredential credentials, SerializedSecurityContext serializedContext, string messageId)
		{
			SoapHttpClientAuthenticator soapHttpClientAuthenticator;
			if (credentials == null)
			{
				ProxyAuthenticator.SecurityTracer.TraceDebug(0L, "{0}: creating ProxyAuthenticator for network service", new object[]
				{
					TraceContext.Get()
				});
				soapHttpClientAuthenticator = SoapHttpClientAuthenticator.CreateNetworkService();
			}
			else
			{
				ProxyAuthenticator.SecurityTracer.TraceDebug<object, string, string>(0L, "{0}: creating ProxyAuthenticator for credentials: {1}\\{2}", TraceContext.Get(), credentials.Domain, credentials.UserName);
				soapHttpClientAuthenticator = SoapHttpClientAuthenticator.Create(credentials);
			}
			if (serializedContext != null)
			{
				soapHttpClientAuthenticator.AdditionalSoapHeaders.Add(serializedContext);
			}
			ProxyAuthenticator.SetMessageId(soapHttpClientAuthenticator, messageId);
			return new ProxyAuthenticator(soapHttpClientAuthenticator, AuthenticatorType.NetworkCredentials);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010A70 File Offset: 0x0000EC70
		public static ProxyAuthenticator Create(CredentialCache cache, SerializedSecurityContext serializedContext, string messageId)
		{
			SoapHttpClientAuthenticator soapHttpClientAuthenticator;
			if (cache == null)
			{
				ProxyAuthenticator.SecurityTracer.TraceDebug(0L, "{0}: creating ProxyAuthenticator for network service", new object[]
				{
					TraceContext.Get()
				});
				soapHttpClientAuthenticator = SoapHttpClientAuthenticator.CreateNetworkService();
			}
			else
			{
				ProxyAuthenticator.SecurityTracer.TraceDebug<object, CredentialCache>(0L, "{0}: creating ProxyAuthenticator for credential cache: {1}", TraceContext.Get(), cache);
				soapHttpClientAuthenticator = SoapHttpClientAuthenticator.Create(cache);
			}
			if (serializedContext != null)
			{
				soapHttpClientAuthenticator.AdditionalSoapHeaders.Add(serializedContext);
			}
			ProxyAuthenticator.SetMessageId(soapHttpClientAuthenticator, messageId);
			return new ProxyAuthenticator(soapHttpClientAuthenticator, AuthenticatorType.NetworkCredentials);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010AE8 File Offset: 0x0000ECE8
		public static ProxyAuthenticator Create(OAuthCredentials credentials, string messageId, bool isAutodiscoverRequest)
		{
			ProxyAuthenticator.SecurityTracer.TraceDebug<object, OAuthCredentials>(0L, "{0}: creating ProxyAuthenticator for OAuthCredentials: {1}", TraceContext.Get(), credentials);
			SoapHttpClientAuthenticator soapHttpClientAuthenticator = isAutodiscoverRequest ? SoapHttpClientAuthenticator.CreateForSoap(credentials) : SoapHttpClientAuthenticator.Create(credentials);
			ProxyAuthenticator.SetMessageId(soapHttpClientAuthenticator, messageId);
			return new ProxyAuthenticator(soapHttpClientAuthenticator, AuthenticatorType.OAuth);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public static ProxyAuthenticator Create(CommonAccessToken commonAccessToken, string messageId)
		{
			ProxyAuthenticator.SecurityTracer.TraceDebug<object, CommonAccessToken>(0L, "{0}: creating ProxyAuthenticator for CommonAccessToken: {1}", TraceContext.Get(), commonAccessToken);
			SoapHttpClientAuthenticator soapHttpClientAuthenticator = SoapHttpClientAuthenticator.Create(commonAccessToken);
			ProxyAuthenticator.SetMessageId(soapHttpClientAuthenticator, messageId);
			return new ProxyAuthenticator(soapHttpClientAuthenticator, AuthenticatorType.OAuth);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010B68 File Offset: 0x0000ED68
		public static ProxyAuthenticator Create(RequestedToken token, SmtpAddress sharingKey, string messageId)
		{
			ProxyAuthenticator.SecurityTracer.TraceDebug(0L, "{0}: creating ProxyAuthenticator for WS-Security", new object[]
			{
				TraceContext.Get()
			});
			XmlElement xmlElement = null;
			if (sharingKey != SmtpAddress.Empty)
			{
				xmlElement = SharingKeyHandler.Encrypt(sharingKey, token.ProofToken);
			}
			SoapHttpClientAuthenticator soapHttpClientAuthenticator = SoapHttpClientAuthenticator.Create(token);
			if (xmlElement != null)
			{
				soapHttpClientAuthenticator.AdditionalSoapHeaders.Add(new SharingSecurityHeader(xmlElement));
			}
			ProxyAuthenticator.SetMessageId(soapHttpClientAuthenticator, messageId);
			return new ProxyAuthenticator(soapHttpClientAuthenticator, AuthenticatorType.WSSecurity);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public static ProxyAuthenticator Create(WSSecurityHeader wsSecurityHeader, SharingSecurityHeader sharingSecurityHeader, string messageId)
		{
			ProxyAuthenticator.SecurityTracer.TraceDebug(0L, "{0}: creating ProxyAuthenticator for WS-Security", new object[]
			{
				TraceContext.Get()
			});
			SoapHttpClientAuthenticator soapHttpClientAuthenticator = SoapHttpClientAuthenticator.Create(wsSecurityHeader);
			if (sharingSecurityHeader != null)
			{
				soapHttpClientAuthenticator.AdditionalSoapHeaders.Add(sharingSecurityHeader);
			}
			ProxyAuthenticator.SetMessageId(soapHttpClientAuthenticator, messageId);
			return new ProxyAuthenticator(soapHttpClientAuthenticator, AuthenticatorType.WSSecurity);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010C30 File Offset: 0x0000EE30
		public static ProxyAuthenticator CreateForSoap(string messageId)
		{
			ProxyAuthenticator.SecurityTracer.TraceDebug(0L, "{0}: creating ProxyAuthenticator for network service", new object[]
			{
				TraceContext.Get()
			});
			SoapHttpClientAuthenticator soapHttpClientAuthenticator = SoapHttpClientAuthenticator.CreateNetworkServiceForSoap();
			ProxyAuthenticator.SetMessageId(soapHttpClientAuthenticator, messageId);
			return new ProxyAuthenticator(soapHttpClientAuthenticator, AuthenticatorType.NetworkCredentials);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00010C74 File Offset: 0x0000EE74
		public void Authenticate(CustomSoapHttpClientProtocol client)
		{
			ProxyAuthenticator.SecurityTracer.TraceDebug<object, AuthenticatorType>((long)this.GetHashCode(), "{0}: Authenticating client with {1}", TraceContext.Get(), this.AuthenticatorType);
			client.Authenticator = this.authenticator;
			if (this.AuthenticatorType == AuthenticatorType.WSSecurity)
			{
				client.Url = EwsWsSecurityUrl.Fix(client.Url);
				client.ConnectionGroupName = "WS>";
				return;
			}
			client.Url = EwsWsSecurityUrl.FixForAnonymous(client.Url);
			client.UnsafeAuthenticatedConnectionSharing = Configuration.UnsafeAuthenticatedConnectionSharing.Value;
			client.ConnectionGroupName = "NC>";
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010D00 File Offset: 0x0000EF00
		public override string ToString()
		{
			return this.AuthenticatorType.ToString();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010D12 File Offset: 0x0000EF12
		private ProxyAuthenticator(SoapHttpClientAuthenticator authenticator, AuthenticatorType authenticatorType)
		{
			this.authenticator = authenticator;
			this.AuthenticatorType = authenticatorType;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00010D28 File Offset: 0x0000EF28
		private static void SetMessageId(SoapHttpClientAuthenticator authenticator, string messageId)
		{
			if (messageId != null)
			{
				authenticator.AdditionalSoapHeaders.Add(WSAddressingMessageIDHeader.Create(messageId));
			}
		}

		// Token: 0x0400026D RID: 621
		private SoapHttpClientAuthenticator authenticator;

		// Token: 0x0400026E RID: 622
		private static readonly Trace SecurityTracer = ExTraceGlobals.SecurityTracer;
	}
}
