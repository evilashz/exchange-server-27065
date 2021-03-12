using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200005F RID: 95
	internal sealed class AutoDiscoverRequest : AsyncWebRequest
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0000A420 File Offset: 0x00008620
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"AutoDiscoverRequest for ",
				this.emailAddress.ToString(),
				" using target URL ",
				this.targetUri.ToString(),
				" and UriSource ",
				this.uriSource
			});
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000A47C File Offset: 0x0000867C
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000A484 File Offset: 0x00008684
		internal bool BypassProxy
		{
			get
			{
				return this.bypassProxy;
			}
			set
			{
				this.bypassProxy = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000A48D File Offset: 0x0000868D
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000A495 File Offset: 0x00008695
		internal TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000A49E File Offset: 0x0000869E
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000A4A6 File Offset: 0x000086A6
		internal AutoDiscoverRequestResult Result { get; private set; }

		// Token: 0x06000221 RID: 545 RVA: 0x0000A4B0 File Offset: 0x000086B0
		internal AutoDiscoverRequest(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, EmailAddress emailAddress, ICredentials credentialsForDiscovery, UriSource uriSource) : base(application, clientContext, requestLogger, "AutoDiscoverRequest")
		{
			this.uriSource = uriSource;
			this.emailAddress = emailAddress;
			this.credentialsForDiscovery = credentialsForDiscovery;
			this.targetUri = targetUri;
			CertificateValidationManager.RegisterCallback(Globals.CertificateValidationComponentId, new RemoteCertificateValidationCallback(CertificateErrorHandler.CertValidationCallback));
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A518 File Offset: 0x00008718
		public override void Abort()
		{
			base.Abort();
			if (this.request != null)
			{
				try
				{
					this.request.Abort();
				}
				catch (WebException)
				{
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000A554 File Offset: 0x00008754
		protected override bool IsImpersonating
		{
			get
			{
				return this.impersonating;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A55C File Offset: 0x0000875C
		protected override IAsyncResult BeginInvoke()
		{
			this.timer = Stopwatch.StartNew();
			string xmlBody = this.GetXmlBody();
			byte[] bytes = Encoding.UTF8.GetBytes(xmlBody);
			WindowsImpersonationContext windowsImpersonationContext = null;
			Exception ex = null;
			try
			{
				this.request = (HttpWebRequest)WebRequest.Create(this.targetUri);
				this.request.Method = "POST";
				this.request.ContentType = "text/xml";
				this.request.Headers.Add("X-AnchorMailbox", this.emailAddress.Address);
				if (this.credentialsForDiscovery == null)
				{
					if (Testability.WebServiceCredentials == null)
					{
						windowsImpersonationContext = NetworkServiceImpersonator.Impersonate();
						this.impersonating = true;
					}
					this.credentialsForDiscovery = CredentialCache.DefaultCredentials;
					this.request.Credentials = this.credentialsForDiscovery;
				}
				else
				{
					CredentialCache credentialCache = new CredentialCache();
					credentialCache.Add(this.targetUri, "Basic", (NetworkCredential)this.credentialsForDiscovery);
					this.request.Credentials = credentialCache;
					this.request.PreAuthenticate = true;
				}
				this.request.Timeout = (int)this.timeout.TotalMilliseconds;
				this.request.UserAgent = this.GetUserAgent();
				this.request.AllowAutoRedirect = false;
				this.request.ContentLength = (long)bytes.Length;
				if (this.bypassProxy)
				{
					this.request.Proxy = new WebProxy();
				}
				CertificateValidationManager.SetComponentId(this.request, Globals.CertificateValidationComponentId);
				using (Stream requestStream = this.request.GetRequestStream())
				{
					requestStream.Write(bytes, 0, bytes.Length);
				}
				AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Sending request to AutoDiscover service, xml body: {1}", TraceContext.Get(), xmlBody);
				if (base.Aborted)
				{
					this.request.Abort();
				}
				return this.request.BeginGetResponse(new AsyncCallback(base.Complete), null);
			}
			catch (ProtocolViolationException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (ArgumentException ex4)
			{
				ex = ex4;
			}
			catch (InvalidOperationException ex5)
			{
				ex = ex5;
			}
			catch (NotSupportedException ex6)
			{
				ex = ex6;
			}
			catch (XmlException ex7)
			{
				ex = ex7;
			}
			catch (XPathException ex8)
			{
				ex = ex8;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (windowsImpersonationContext != null)
				{
					windowsImpersonationContext.Dispose();
					this.impersonating = false;
				}
			}
			if (ex != null)
			{
				this.HandleException(ex);
			}
			return null;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A880 File Offset: 0x00008A80
		protected override void EndInvoke(IAsyncResult asyncResult)
		{
			WebResponse webResponse = null;
			try
			{
				AutoDiscoverRequest.FaultInjectionTracer.TraceTest(2263231805U);
				webResponse = this.request.EndGetResponse(asyncResult);
				if (webResponse != null)
				{
					this.headers = webResponse.Headers;
					HttpWebResponse httpWebResponse = webResponse as HttpWebResponse;
					if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.Found)
					{
						string text = this.headers["Location"];
						if (string.IsNullOrEmpty(text))
						{
							this.HandleException(Strings.descAutoDiscoverBadRedirectLocation(this.emailAddress.ToString(), "null"), 50492U);
						}
						else
						{
							AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, string, Uri>((long)this.GetHashCode(), "{0}: Got a redirect from AutoDiscover to {1}, original location was {1}.", TraceContext.Get(), text, this.targetUri);
							Uri uri;
							try
							{
								uri = new Uri(text);
							}
							catch (UriFormatException exception)
							{
								this.HandleException(Strings.descAutoDiscoverBadRedirectLocation(this.emailAddress.ToString(), text), exception);
								return;
							}
							UriBuilder uriBuilder = new UriBuilder(uri);
							uriBuilder.Scheme = (Configuration.UseSSLForAutoDiscoverRequests ? "https" : "http");
							this.Result = new AutoDiscoverRequestResult(this.targetUri, null, uriBuilder.Uri, null, this.headers[WellKnownHeader.XFEServer], this.headers[WellKnownHeader.XBEServer]);
						}
					}
					else
					{
						this.responseText = this.GetResponseText(webResponse);
						if (this.responseText == null || this.responseText.Length == 0)
						{
							this.HandleException(Strings.descNullAutoDiscoverResponse, new XmlException());
						}
						else
						{
							string text2 = this.FindError();
							if (text2 != null)
							{
								this.HandleException(Strings.descAutoDiscoverFailedWithException(this.emailAddress.ToString(), text2), 47420U);
							}
							else
							{
								string text3 = this.FindRedirectAddress();
								if (text3 != null)
								{
									string frontEndServerName = null;
									string backEnderServerName = null;
									if (this.headers != null)
									{
										frontEndServerName = this.headers[WellKnownHeader.XFEServer];
										backEnderServerName = this.headers[WellKnownHeader.XBEServer];
									}
									this.Result = new AutoDiscoverRequestResult(this.targetUri, text3, null, null, frontEndServerName, backEnderServerName);
								}
								else
								{
									WebServiceUri webServiceUri = this.FindWebServiceUrlForProtocol("EXPR");
									if (webServiceUri != null)
									{
										this.Result = new AutoDiscoverRequestResult(this.targetUri, null, null, webServiceUri, null, null);
									}
									else
									{
										webServiceUri = this.FindWebServiceUrlForProtocol("EXCH");
										if (webServiceUri != null)
										{
											this.Result = new AutoDiscoverRequestResult(this.targetUri, null, null, webServiceUri, null, null);
										}
										else
										{
											this.HandleException(Strings.descProtocolNotFoundInAutoDiscoverResponse("EXCH", this.targetUri.ToString()), null);
										}
									}
								}
							}
						}
					}
				}
			}
			catch (WebException exception2)
			{
				this.HandleException(exception2);
			}
			catch (ArgumentNullException exception3)
			{
				this.HandleException(exception3);
			}
			catch (ArgumentException exception4)
			{
				this.HandleException(exception4);
			}
			catch (InvalidOperationException exception5)
			{
				this.HandleException(exception5);
			}
			catch (NotSupportedException exception6)
			{
				this.HandleException(exception6);
			}
			catch (XmlException exception7)
			{
				this.HandleException(exception7);
			}
			catch (XPathException exception8)
			{
				this.HandleException(exception8);
			}
			catch (UriFormatException exception9)
			{
				this.HandleException(exception9);
			}
			finally
			{
				if (webResponse != null)
				{
					webResponse.Close();
				}
				this.timer.Stop();
				base.RequestLogger.Add(RequestStatistics.Create(RequestStatisticsType.AutoDiscoverRequest, this.timer.ElapsedMilliseconds, this.targetUri.ToString()));
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000AC98 File Offset: 0x00008E98
		private string GetHttpRequestString()
		{
			return string.Format("Discovery URL : {0}, EmailAddress : {1}", this.targetUri.ToString(), this.emailAddress);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		private WebServiceUri FindWebServiceUrlForProtocol(string protocol)
		{
			using (StringReader stringReader = new StringReader(this.responseText))
			{
				AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Trying to find web service Url from auto-discover response for protocol '{1}'", TraceContext.Get(), protocol);
				XPathDocument xpathDocument = SafeXmlFactory.CreateXPathDocument(stringReader);
				XPathNavigator xpathNavigator = xpathDocument.CreateNavigator();
				xpathNavigator.MoveToChild(XPathNodeType.Element);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xpathNavigator.NameTable);
				xmlNamespaceManager.AddNamespace("rs", "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a");
				string xpath = string.Format("//{0}:Protocol[{0}:Type='{1}']", "rs", protocol);
				XPathExpression xpathExpression = xpathNavigator.Compile(xpath);
				xpathExpression.SetContext(xmlNamespaceManager);
				XPathNodeIterator xpathNodeIterator = xpathNavigator.Select(xpathExpression);
				if (xpathNodeIterator == null || xpathNodeIterator.Count == 0)
				{
					AutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, string, string>((long)this.GetHashCode(), "{0}: could not find any nodes of protocol type '{1}' in the auto discover response: {2}.", TraceContext.Get(), protocol, this.responseText);
					return null;
				}
				while (xpathNodeIterator.MoveNext())
				{
					string xpath2 = string.Format("{0}:ASUrl", "rs");
					XPathNavigator xpathNavigator2 = xpathNodeIterator.Current.SelectSingleNode(xpath2, xmlNamespaceManager);
					if (xpathNavigator2 != null)
					{
						string value = xpathNavigator2.Value;
						if (value != null)
						{
							int serverVersion = 0;
							if ("EXCH".Equals(protocol, StringComparison.OrdinalIgnoreCase))
							{
								string xpath3 = string.Format("{0}:ServerVersion", "rs");
								XPathNavigator xpathNavigator3 = xpathNodeIterator.Current.SelectSingleNode(xpath3, xmlNamespaceManager);
								if (xpathNavigator3 != null)
								{
									string value2 = xpathNavigator3.Value;
									if (!int.TryParse(value2, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out serverVersion))
									{
										AutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: ServerVersion '{1}' cannot be parsed as a hex number.", TraceContext.Get(), value2);
									}
								}
							}
							return new WebServiceUri(value, protocol, this.uriSource, serverVersion);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000AE80 File Offset: 0x00009080
		private string FindRedirectAddress()
		{
			using (StringReader stringReader = new StringReader(this.responseText))
			{
				AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, EmailAddress>((long)this.GetHashCode(), "{0}: Trying to find if there is any redirect address in the response for email {1}.", TraceContext.Get(), this.emailAddress);
				XPathDocument xpathDocument = SafeXmlFactory.CreateXPathDocument(stringReader);
				XPathNavigator xpathNavigator = xpathDocument.CreateNavigator();
				xpathNavigator.MoveToChild(XPathNodeType.Element);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xpathNavigator.NameTable);
				xmlNamespaceManager.AddNamespace("rs", "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a");
				string xpath = string.Format("//{0}:RedirectAddr", "rs");
				XPathExpression xpathExpression = xpathNavigator.Compile(xpath);
				xpathExpression.SetContext(xmlNamespaceManager);
				XPathNodeIterator xpathNodeIterator = xpathNavigator.Select(xpathExpression);
				if (xpathNodeIterator != null && xpathNodeIterator.Count != 0)
				{
					while (xpathNodeIterator.MoveNext())
					{
						string.Format("{0}:RedirectAddr", "rs");
						XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
						if (xpathNavigator2 != null)
						{
							AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, string, EmailAddress>((long)this.GetHashCode(), "{0}: Found redirect address {1} in the response for original email {2}.", TraceContext.Get(), xpathNavigator2.Value, this.emailAddress);
							return xpathNavigator2.Value;
						}
					}
				}
			}
			AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, EmailAddress, string>((long)this.GetHashCode(), "{0}: Could not find any nodes containing redirection address for email {1} in the auto discovery response: {2}", TraceContext.Get(), this.emailAddress, this.responseText);
			return null;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000AFC8 File Offset: 0x000091C8
		private string FindError()
		{
			using (StringReader stringReader = new StringReader(this.responseText))
			{
				AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, EmailAddress>((long)this.GetHashCode(), "{0}: Trying to find if there is any error in the response for email {1}.", TraceContext.Get(), this.emailAddress);
				XPathDocument xpathDocument = SafeXmlFactory.CreateXPathDocument(stringReader);
				XPathNavigator xpathNavigator = xpathDocument.CreateNavigator();
				xpathNavigator.MoveToChild(XPathNodeType.Element);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xpathNavigator.NameTable);
				xmlNamespaceManager.AddNamespace("rs", "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006");
				XPathExpression xpathExpression = xpathNavigator.Compile(AutoDiscoverRequest.ErrorSearchString);
				xpathExpression.SetContext(xmlNamespaceManager);
				XPathNodeIterator xpathNodeIterator = xpathNavigator.Select(xpathExpression);
				if (xpathNodeIterator != null && xpathNodeIterator.Count != 0)
				{
					string text = null;
					string text2 = null;
					if (xpathNodeIterator.MoveNext())
					{
						XPathNavigator xpathNavigator2 = xpathNodeIterator.Current.SelectSingleNode(AutoDiscoverRequest.ErrorCodeString, xmlNamespaceManager);
						if (xpathNavigator2 != null)
						{
							text = xpathNavigator2.Value;
							AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, string, EmailAddress>((long)this.GetHashCode(), "{0}: Found error code {1} in the response for original email {2}.", TraceContext.Get(), text, this.emailAddress);
						}
						XPathNavigator xpathNavigator3 = xpathNodeIterator.Current.SelectSingleNode(AutoDiscoverRequest.ErrorMessageString, xmlNamespaceManager);
						if (xpathNavigator3 != null)
						{
							text2 = xpathNavigator3.Value;
							AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, string, EmailAddress>((long)this.GetHashCode(), "{0}: Found error message {1} in the response for original email {2}.", TraceContext.Get(), text2, this.emailAddress);
						}
					}
					if (text != null)
					{
						if (text2 != null)
						{
							return "ErrorCode=" + text + ", Message=" + text2;
						}
						return "ErrorCode=" + text;
					}
				}
			}
			AutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, EmailAddress, string>((long)this.GetHashCode(), "{0}: Could not find any nodes containing erroremail {1} in the auto discovery response: {2}", TraceContext.Get(), this.emailAddress, this.responseText);
			return null;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000B184 File Offset: 0x00009384
		protected override void HandleException(Exception exception)
		{
			if (exception is LocalizedException)
			{
				this.HandleException(((LocalizedException)exception).LocalizedString, exception);
				return;
			}
			this.HandleException(Strings.descAutoDiscoverRequestError(exception.Message, this.GetHttpRequestString()), exception);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B1B9 File Offset: 0x000093B9
		private void HandleException(LocalizedString message, Exception exception)
		{
			AutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, AutoDiscoverRequest, LocalizedString>((long)this.GetHashCode(), "{0}: Exception occurred while sending auto discover request {1}. The exception message is {2}", TraceContext.Get(), this, message);
			this.CreateAutoDiscoverResultWithException(new AutoDiscoverFailedException(message, exception));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B1E5 File Offset: 0x000093E5
		private void HandleException(LocalizedString message, uint locationIdentifier)
		{
			AutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, AutoDiscoverRequest, LocalizedString>((long)this.GetHashCode(), "{0}: Error occurred while sending auto discover request {1}. Error Message is {2}", TraceContext.Get(), this, message);
			this.CreateAutoDiscoverResultWithException(new AutoDiscoverFailedException(message, locationIdentifier));
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B214 File Offset: 0x00009414
		private void CreateAutoDiscoverResultWithException(AutoDiscoverFailedException ex)
		{
			string frontEndServerName = null;
			string backEnderServerName = null;
			if (this.headers != null)
			{
				frontEndServerName = this.headers[WellKnownHeader.XFEServer];
				backEnderServerName = this.headers[WellKnownHeader.XBEServer];
			}
			this.Result = new AutoDiscoverRequestResult(this.targetUri, ex, frontEndServerName, backEnderServerName);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B264 File Offset: 0x00009464
		private string GetXmlBody()
		{
			StringBuilder stringBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			stringBuilder.Append("<Autodiscover xmlns=\"http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006\">");
			stringBuilder.Append("<Request>");
			stringBuilder.AppendFormat("<EMailAddress>{0}</EMailAddress>", this.emailAddress.Address);
			stringBuilder.Append("<AcceptableResponseSchema>http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a</AcceptableResponseSchema>");
			stringBuilder.Append("</Request>");
			stringBuilder.Append("</Autodiscover>");
			return stringBuilder.ToString();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B2D8 File Offset: 0x000094D8
		private string GetResponseText(WebResponse response)
		{
			Stream responseStream = response.GetResponseStream();
			string result = null;
			Encoding encoding = Encoding.GetEncoding("utf-8");
			using (StreamReader streamReader = new StreamReader(responseStream, encoding))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B328 File Offset: 0x00009528
		private string GetUserAgent()
		{
			UserAgent userAgent = new UserAgent("ASAutoDiscover", "CrossForest", (this.uriSource == UriSource.Directory) ? "Directory" : "EmailDomain", null);
			return userAgent.ToString();
		}

		// Token: 0x04000157 RID: 343
		private const string RequestSchemaNamespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006";

		// Token: 0x04000158 RID: 344
		private const string ResponseSchemaNamespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a";

		// Token: 0x04000159 RID: 345
		private const string GeneralResponseSchemaNamespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006";

		// Token: 0x0400015A RID: 346
		private const string ResponseSchemaNamespacePrefix = "rs";

		// Token: 0x0400015B RID: 347
		private const string ContentType = "text/xml";

		// Token: 0x0400015C RID: 348
		private const string Method = "POST";

		// Token: 0x0400015D RID: 349
		private const string XmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

		// Token: 0x0400015E RID: 350
		private const string AutoDiscoverOpenTag = "<Autodiscover xmlns=\"http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006\">";

		// Token: 0x0400015F RID: 351
		private const string ResponseSchemaElement = "<AcceptableResponseSchema>http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a</AcceptableResponseSchema>";

		// Token: 0x04000160 RID: 352
		private const string RequestOpenTag = "<Request>";

		// Token: 0x04000161 RID: 353
		private const string RequestCloseTag = "</Request>";

		// Token: 0x04000162 RID: 354
		private const string EmailAddressElementFormat = "<EMailAddress>{0}</EMailAddress>";

		// Token: 0x04000163 RID: 355
		private const string AutoDiscoverCloseTag = "</Autodiscover>";

		// Token: 0x04000164 RID: 356
		internal const string ExchangeRpc = "EXCH";

		// Token: 0x04000165 RID: 357
		internal const string ExchangeHttp = "EXPR";

		// Token: 0x04000166 RID: 358
		private const string UriSchemeHttps = "https";

		// Token: 0x04000167 RID: 359
		private const string UriSchemeHttp = "http";

		// Token: 0x04000168 RID: 360
		private EmailAddress emailAddress;

		// Token: 0x04000169 RID: 361
		private ICredentials credentialsForDiscovery;

		// Token: 0x0400016A RID: 362
		private string responseText;

		// Token: 0x0400016B RID: 363
		private UriSource uriSource;

		// Token: 0x0400016C RID: 364
		private Uri targetUri;

		// Token: 0x0400016D RID: 365
		private bool bypassProxy = Configuration.BypassProxyForCrossForestRequests;

		// Token: 0x0400016E RID: 366
		private TimeSpan timeout = Configuration.WebRequestTimeoutInSeconds;

		// Token: 0x0400016F RID: 367
		private Stopwatch timer;

		// Token: 0x04000170 RID: 368
		private bool impersonating;

		// Token: 0x04000171 RID: 369
		private HttpWebRequest request;

		// Token: 0x04000172 RID: 370
		private WebHeaderCollection headers;

		// Token: 0x04000173 RID: 371
		private static readonly string ErrorSearchString = string.Format("//{0}:Error", "rs");

		// Token: 0x04000174 RID: 372
		private static readonly string ErrorCodeString = string.Format("{0}:ErrorCode", "rs");

		// Token: 0x04000175 RID: 373
		private static readonly string ErrorMessageString = string.Format("{0}:Message", "rs");

		// Token: 0x04000176 RID: 374
		private static readonly FaultInjectionTrace FaultInjectionTracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch.ExTraceGlobals.FaultInjectionTracer;

		// Token: 0x04000177 RID: 375
		private static readonly Microsoft.Exchange.Diagnostics.Trace AutoDiscoverTracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability.ExTraceGlobals.AutoDiscoverTracer;
	}
}
