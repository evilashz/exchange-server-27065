using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.ServiceModel;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x0200010C RID: 268
	public abstract class CustomSoapHttpClientProtocol : SoapHttpClientProtocol
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00017826 File Offset: 0x00015A26
		protected CustomSoapHttpClientProtocol()
		{
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00017854 File Offset: 0x00015A54
		protected virtual bool SuppressMustUnderstand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00017857 File Offset: 0x00015A57
		internal virtual XmlNamespaceDefinition[] PredefinedNamespaces
		{
			get
			{
				return CustomSoapHttpClientProtocol.NoPredefinedNamespaces;
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001785E File Offset: 0x00015A5E
		protected CustomSoapHttpClientProtocol(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback, bool normalization) : this(componentId, remoteCertificateValidationCallback, normalization, true)
		{
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001786C File Offset: 0x00015A6C
		protected CustomSoapHttpClientProtocol(RemoteCertificateValidationCallback remoteCertificateValidationCallback, bool normalization)
		{
			this.normalization = normalization;
			this.remoteCertificateValidationCallback = remoteCertificateValidationCallback;
			this.checkInvalidCharacters = true;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000178BC File Offset: 0x00015ABC
		protected CustomSoapHttpClientProtocol(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback, bool normalization, bool checkInvalidCharacters)
		{
			this.normalization = normalization;
			this.remoteCertificateValidationCallback = remoteCertificateValidationCallback;
			this.checkInvalidCharacters = checkInvalidCharacters;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001790C File Offset: 0x00015B0C
		protected override WebRequest GetWebRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)base.GetWebRequest(uri);
			httpWebRequest.ServerCertificateValidationCallback = this.remoteCertificateValidationCallback;
			if (this.keepAlive != null)
			{
				httpWebRequest.KeepAlive = this.keepAlive.Value;
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.httpHeaders)
			{
				httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			}
			httpWebRequest.ServicePoint.Expect100Continue = false;
			return httpWebRequest;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000179B8 File Offset: 0x00015BB8
		protected override WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse webResponse = base.GetWebResponse(request);
			this.TraceRequestAndResponse(request, webResponse);
			this.PopulateResponseHttpHeaders(webResponse);
			return webResponse;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000179E0 File Offset: 0x00015BE0
		protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			WebResponse webResponse = base.GetWebResponse(request, result);
			this.TraceRequestAndResponse(request, webResponse);
			this.PopulateResponseHttpHeaders(webResponse);
			return webResponse;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00017A08 File Offset: 0x00015C08
		private void PopulateResponseHttpHeaders(WebResponse webResponse)
		{
			this.responseHttpHeaders.Clear();
			foreach (string text in webResponse.Headers.AllKeys)
			{
				this.responseHttpHeaders[text] = webResponse.Headers[text];
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00017A58 File Offset: 0x00015C58
		private void TraceRequestAndResponse(WebRequest request, WebResponse response)
		{
			if (CustomSoapHttpClientProtocol.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (request != null)
				{
					HttpWebRequest httpWebRequest = request as HttpWebRequest;
					if (httpWebRequest != null)
					{
						CustomSoapHttpClientProtocol.Tracer.TraceDebug<Uri>((long)this.GetHashCode(), "Request HTTP URI: {0}", httpWebRequest.RequestUri);
					}
					this.TraceHttpHeaderCollection("Request", request.Headers);
				}
				if (response != null)
				{
					this.TraceHttpHeaderCollection("Response", response.Headers);
					HttpWebResponse httpWebResponse = response as HttpWebResponse;
					if (httpWebResponse != null)
					{
						CustomSoapHttpClientProtocol.Tracer.TraceDebug<HttpStatusCode, string>((long)this.GetHashCode(), "Response HTTP status: {0} - {1}", httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
					}
				}
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00017AEC File Offset: 0x00015CEC
		private void TraceHttpHeaderCollection(string name, WebHeaderCollection headers)
		{
			StringBuilder stringBuilder = new StringBuilder(headers.Count * 40);
			foreach (string text in headers.AllKeys)
			{
				stringBuilder.Append(text + "=" + headers[text] + ";");
			}
			CustomSoapHttpClientProtocol.Tracer.TraceDebug<string, StringBuilder>((long)this.GetHashCode(), "{0} HTTP headers: {1}", name, stringBuilder);
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00017B57 File Offset: 0x00015D57
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x00017B5F File Offset: 0x00015D5F
		internal bool? KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.keepAlive = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00017B68 File Offset: 0x00015D68
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00017B70 File Offset: 0x00015D70
		internal SoapHttpClientAuthenticator Authenticator
		{
			get
			{
				return this.authenticator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Authenticator");
				}
				this.authenticator = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00017B87 File Offset: 0x00015D87
		public Dictionary<string, string> HttpHeaders
		{
			get
			{
				return this.httpHeaders;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00017B8F File Offset: 0x00015D8F
		public Dictionary<string, string> ResponseHttpHeaders
		{
			get
			{
				return this.responseHttpHeaders;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00017B98 File Offset: 0x00015D98
		protected override XmlReader GetReaderForMessage(SoapClientMessage message, int bufferSize)
		{
			if (!message.Stream.CanRead)
			{
				throw new ChannelTerminatedException();
			}
			XmlReader reader;
			try
			{
				if (!this.checkInvalidCharacters)
				{
					XmlReaderSettings settings = new XmlReaderSettings
					{
						CheckCharacters = false
					};
					reader = XmlReader.Create(message.Stream, settings);
				}
				else
				{
					reader = base.GetReaderForMessage(message, bufferSize);
				}
			}
			catch (ArgumentException ex)
			{
				throw new ChannelTerminatedException(ex.Message, ex);
			}
			XmlReader readerForMessage = this.authenticator.GetReaderForMessage(reader, message);
			SoapHttpClientXmlReader soapHttpClientXmlReader = readerForMessage as SoapHttpClientXmlReader;
			if (soapHttpClientXmlReader != null && soapHttpClientXmlReader.SupportsNormalization)
			{
				soapHttpClientXmlReader.Normalization = this.normalization;
			}
			return readerForMessage;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00017C3C File Offset: 0x00015E3C
		protected override XmlWriter GetWriterForMessage(SoapClientMessage message, int bufferSize)
		{
			XmlWriter writerForMessage = this.authenticator.GetWriterForMessage(this.PredefinedNamespaces, base.GetWriterForMessage(message, bufferSize), message);
			if (this.SuppressMustUnderstand)
			{
				foreach (object obj in message.Headers)
				{
					SoapHeader soapHeader = (SoapHeader)obj;
					soapHeader.MustUnderstand = false;
				}
			}
			return writerForMessage;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00017CEC File Offset: 0x00015EEC
		protected new IAsyncResult BeginInvoke(string methodName, object[] parameters, AsyncCallback callback, object asyncState)
		{
			return this.authenticator.AuthenticateAndExecute<IAsyncResult>(this, () => this.BeginInvoke(methodName, parameters, callback, asyncState));
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00017D5C File Offset: 0x00015F5C
		protected new virtual object[] Invoke(string methodName, object[] parameters)
		{
			return this.authenticator.AuthenticateAndExecute<object[]>(this, () => this.Invoke(methodName, parameters));
		}

		// Token: 0x04000578 RID: 1400
		private static readonly XmlNamespaceDefinition[] NoPredefinedNamespaces = new XmlNamespaceDefinition[0];

		// Token: 0x04000579 RID: 1401
		private SoapHttpClientAuthenticator authenticator = SoapHttpClientAuthenticator.CreateNone();

		// Token: 0x0400057A RID: 1402
		private bool? keepAlive;

		// Token: 0x0400057B RID: 1403
		private readonly Dictionary<string, string> httpHeaders = new Dictionary<string, string>();

		// Token: 0x0400057C RID: 1404
		private readonly RemoteCertificateValidationCallback remoteCertificateValidationCallback;

		// Token: 0x0400057D RID: 1405
		private readonly bool normalization;

		// Token: 0x0400057E RID: 1406
		private readonly Dictionary<string, string> responseHttpHeaders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400057F RID: 1407
		private readonly bool checkInvalidCharacters;

		// Token: 0x04000580 RID: 1408
		private static readonly Trace Tracer = ExTraceGlobals.EwsClientTracer;
	}
}
