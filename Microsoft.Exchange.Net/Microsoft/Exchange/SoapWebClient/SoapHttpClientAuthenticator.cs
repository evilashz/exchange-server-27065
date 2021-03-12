using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Net.WSSecurity;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006DB RID: 1755
	internal sealed class SoapHttpClientAuthenticator
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0004118E File Offset: 0x0003F38E
		public SoapHeaderCollection AdditionalSoapHeaders
		{
			get
			{
				return this.soapAuthenticator.AdditionalSoapHeaders;
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0004119B File Offset: 0x0003F39B
		public static SoapHttpClientAuthenticator CreateNone()
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.None, SoapAuthenticator.CreateNone());
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000411AC File Offset: 0x0003F3AC
		public static SoapHttpClientAuthenticator CreateAnonymous()
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.None, SoapAuthenticator.CreateAnonymous());
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000411BD File Offset: 0x0003F3BD
		public static SoapHttpClientAuthenticator CreateNetworkService()
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.NetworkService, SoapAuthenticator.CreateNone());
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000411CE File Offset: 0x0003F3CE
		public static SoapHttpClientAuthenticator CreateNetworkServiceForSoap()
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.NetworkService, SoapAuthenticator.CreateAnonymous());
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000411DF File Offset: 0x0003F3DF
		public static SoapHttpClientAuthenticator Create(ICredentials credentials)
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.Create(credentials), SoapAuthenticator.CreateNone());
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000411F1 File Offset: 0x0003F3F1
		public static SoapHttpClientAuthenticator CreateForSoap(ICredentials credentials)
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.Create(credentials), SoapAuthenticator.CreateAnonymous());
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x00041203 File Offset: 0x0003F403
		public static SoapHttpClientAuthenticator Create(CommonAccessToken commonAccessToken)
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.Create(commonAccessToken), SoapAuthenticator.CreateNone());
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00041215 File Offset: 0x0003F415
		public static SoapHttpClientAuthenticator Create(RequestedToken token)
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.None, SoapAuthenticator.Create(token));
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00041227 File Offset: 0x0003F427
		public static SoapHttpClientAuthenticator Create(X509Certificate2 certificate)
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.None, SoapAuthenticator.Create(certificate));
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00041239 File Offset: 0x0003F439
		public static SoapHttpClientAuthenticator Create(WSSecurityHeader header)
		{
			return new SoapHttpClientAuthenticator(HttpAuthenticator.None, SoapAuthenticator.Create(header));
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0004124B File Offset: 0x0003F44B
		public XmlReader GetReaderForMessage(XmlReader reader, SoapClientMessage message)
		{
			return this.soapAuthenticator.GetReaderForMessage(reader, message);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0004125A File Offset: 0x0003F45A
		public XmlWriter GetWriterForMessage(XmlNamespaceDefinition[] predefinedNamespaces, XmlWriter writer, SoapClientMessage message)
		{
			return this.soapAuthenticator.GetWriterForMessage(predefinedNamespaces, writer, message);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0004126A File Offset: 0x0003F46A
		public T AuthenticateAndExecute<T>(CustomSoapHttpClientProtocol client, AuthenticateAndExecuteHandler<T> handler)
		{
			return this.httpAuthenticator.AuthenticateAndExecute<T>(client, handler);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00041279 File Offset: 0x0003F479
		private SoapHttpClientAuthenticator(HttpAuthenticator httpAuthenticator, SoapAuthenticator soapAuthenticator)
		{
			this.httpAuthenticator = httpAuthenticator;
			this.soapAuthenticator = soapAuthenticator;
		}

		// Token: 0x04001F72 RID: 8050
		private SoapAuthenticator soapAuthenticator;

		// Token: 0x04001F73 RID: 8051
		private HttpAuthenticator httpAuthenticator;
	}
}
