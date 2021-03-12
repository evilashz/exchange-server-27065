using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006BE RID: 1726
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AutodiscoverClient : IDisposable
	{
		// Token: 0x06002028 RID: 8232 RVA: 0x0003E70E File Offset: 0x0003C90E
		public void Dispose()
		{
			this.binding.Dispose();
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0003E71B File Offset: 0x0003C91B
		public StringList AllowedHostnames
		{
			get
			{
				return this.allowedHostnames;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0003E723 File Offset: 0x0003C923
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x0003E730 File Offset: 0x0003C930
		public SoapHttpClientAuthenticator Authenticator
		{
			get
			{
				return this.binding.Authenticator;
			}
			set
			{
				this.binding.Authenticator = value;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x0003E73E File Offset: 0x0003C93E
		// (set) Token: 0x0600202D RID: 8237 RVA: 0x0003E74B File Offset: 0x0003C94B
		public string UserAgent
		{
			get
			{
				return this.binding.UserAgent;
			}
			set
			{
				this.binding.UserAgent = value;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0003E759 File Offset: 0x0003C959
		// (set) Token: 0x0600202F RID: 8239 RVA: 0x0003E770 File Offset: 0x0003C970
		public string AnchorMailbox
		{
			get
			{
				return this.binding.HttpHeaders[WellKnownHeader.AnchorMailbox];
			}
			set
			{
				this.binding.HttpHeaders[WellKnownHeader.AnchorMailbox] = value;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0003E788 File Offset: 0x0003C988
		// (set) Token: 0x06002031 RID: 8241 RVA: 0x0003E790 File Offset: 0x0003C990
		public IWebProxy Proxy { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0003E799 File Offset: 0x0003C999
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x0003E7A6 File Offset: 0x0003C9A6
		public RequestedServerVersion RequestedServerVersion
		{
			get
			{
				return this.binding.RequestedServerVersionValue;
			}
			set
			{
				this.binding.RequestedServerVersionValue = value;
			}
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0003E7B4 File Offset: 0x0003C9B4
		public AutodiscoverClient()
		{
			this.binding = new DefaultBinding_Autodiscover("AutodiscoverClient", new RemoteCertificateValidationCallback(AutodiscoverClient.InvalidCertificateHandler));
			this.binding.UserAgent = "AutodiscoverClient";
			this.binding.Timeout = 30000;
			this.binding.KeepAlive = new bool?(false);
			this.binding.AllowAutoRedirect = false;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0003E898 File Offset: 0x0003CA98
		public IEnumerable<AutodiscoverResultData> InvokeWithDiscovery(InvokeDelegate invokeDelegate, string domain)
		{
			string autodiscoverDomain = string.Format("autodiscover.{0}", domain);
			AutodiscoverClient.AutoDiscoverStep[] array = new AutodiscoverClient.AutoDiscoverStep[]
			{
				() => this.InvokeForHost(invokeDelegate, autodiscoverDomain),
				() => this.InvokeForHost(invokeDelegate, domain),
				() => this.InvokeForUnsecureRedirect(invokeDelegate, autodiscoverDomain),
				() => this.InvokeForUnsecureRedirect(invokeDelegate, domain)
			};
			Stopwatch stopwatch = Stopwatch.StartNew();
			IEnumerable<AutodiscoverResultData> result;
			try
			{
				List<AutodiscoverResultData> list = new List<AutodiscoverResultData>(4);
				foreach (AutodiscoverClient.AutoDiscoverStep autoDiscoverStep in array)
				{
					AutodiscoverResultData autodiscoverResultData = autoDiscoverStep();
					list.Add(autodiscoverResultData);
					if (autodiscoverResultData.Type == AutodiscoverResult.Success)
					{
						break;
					}
				}
				result = list;
			}
			finally
			{
				AutodiscoverClient.Tracer.TraceDebug<long>((long)this.GetHashCode(), "Discovery time: {0}ms", stopwatch.ElapsedMilliseconds);
			}
			return result;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0003E9A4 File Offset: 0x0003CBA4
		public AutodiscoverResultData InvokeWithEndpoint(InvokeDelegate invokeDelegate, Uri url)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			AutodiscoverResultData result;
			try
			{
				result = this.InvokeForUrl(invokeDelegate, url);
			}
			finally
			{
				AutodiscoverClient.Tracer.TraceDebug<long>((long)this.GetHashCode(), "Discovery time: {0}ms", stopwatch.ElapsedMilliseconds);
			}
			return result;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0003E9F0 File Offset: 0x0003CBF0
		private AutodiscoverResultData InvokeForHost(InvokeDelegate invokeDelegate, string host)
		{
			UriBuilder uriBuilder = new UriBuilder
			{
				Scheme = Uri.UriSchemeHttps,
				Host = host,
				Path = "/autodiscover/autodiscover.svc"
			};
			return this.InvokeForUrl(invokeDelegate, uriBuilder.Uri);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0003EA30 File Offset: 0x0003CC30
		private AutodiscoverResultData InvokeForUrl(InvokeDelegate invokeDelegate, Uri url)
		{
			AutodiscoverClient.Tracer.TraceDebug<Uri>((long)this.GetHashCode(), "Sending autodiscover request to {0}", url);
			AutodiscoverClient.staticInvalidHostnames = null;
			Exception ex = null;
			AutodiscoverResultData autodiscoverResultData = null;
			StringList stringList;
			try
			{
				autodiscoverResultData = this.InvokeAndFollowSecureRedirects(invokeDelegate, url);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (WebException ex3)
			{
				ex = ex3;
			}
			catch (SoapException ex4)
			{
				ex = ex4;
			}
			catch (InvalidOperationException ex5)
			{
				ex = ex5;
			}
			finally
			{
				stringList = AutodiscoverClient.staticInvalidHostnames;
				AutodiscoverClient.staticInvalidHostnames = null;
			}
			if (ex != null)
			{
				AutodiscoverClient.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to make autodiscover request due exception {0}", ex);
				return new AutodiscoverResultData
				{
					Type = AutodiscoverResult.Failure,
					Url = url,
					Exception = ex
				};
			}
			if (stringList != null && !this.IsAllowedHostname(stringList))
			{
				AutodiscoverClient.Tracer.TraceError((long)this.GetHashCode(), "Autodiscover request had SSL certificate hostname mismatch");
				return new AutodiscoverResultData
				{
					Type = AutodiscoverResult.InvalidSslHostname,
					Url = url,
					SslCertificateHostnames = stringList,
					Alternate = autodiscoverResultData
				};
			}
			return autodiscoverResultData;
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0003EBAC File Offset: 0x0003CDAC
		private AutodiscoverResultData InvokeAndFollowSecureRedirects(InvokeDelegate invokeDelegate, Uri url)
		{
			Uri uri = url;
			int num = 0;
			AutodiscoverResultData result;
			for (;;)
			{
				this.binding.Url = uri.ToString();
				try
				{
					AutodiscoverResponse response = null;
					this.InvokeWithWebProxy(uri.ToString(), delegate(IWebProxy webProxy)
					{
						this.binding.Proxy = webProxy;
						response = invokeDelegate(this.binding);
					});
					if (response == null)
					{
						AutodiscoverClient.Tracer.TraceError((long)this.GetHashCode(), "Response is empty");
						result = new AutodiscoverResultData
						{
							Type = AutodiscoverResult.Failure,
							Url = uri,
							Exception = new AutodiscoverClientException(NetException.EmptyResponse)
						};
					}
					else
					{
						result = new AutodiscoverResultData
						{
							Type = AutodiscoverResult.Success,
							Url = uri,
							Response = response
						};
					}
				}
				catch (WebException ex)
				{
					HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
					if (httpWebResponse == null)
					{
						AutodiscoverClient.Tracer.TraceError<string>((long)this.GetHashCode(), "WebException doesn't contain HttpWebResponse: {0}", (ex.Response == null) ? "<null>" : ex.Response.ToString());
						throw;
					}
					if (httpWebResponse.StatusCode != HttpStatusCode.Found)
					{
						AutodiscoverClient.Tracer.TraceError<HttpStatusCode>((long)this.GetHashCode(), "The StatusCode in WebException is not an redirect: {0}", httpWebResponse.StatusCode);
						throw;
					}
					num++;
					if (num > 5)
					{
						AutodiscoverClient.Tracer.TraceError<int>((long)this.GetHashCode(), "Stopped following redirects because it exceeded maximum {0}", 5);
						throw;
					}
					string text = httpWebResponse.Headers[HttpResponseHeader.Location];
					if (!Uri.IsWellFormedUriString(text, UriKind.Absolute))
					{
						AutodiscoverClient.Tracer.TraceError<string>((long)this.GetHashCode(), "Not a valid redirect URL: {0}", text);
						throw;
					}
					Uri uri2 = new Uri(text, UriKind.Absolute);
					if (uri2.Scheme != Uri.UriSchemeHttps)
					{
						AutodiscoverClient.Tracer.TraceError<string>((long)this.GetHashCode(), "Not a secure redirect URL: {0}", text);
						throw;
					}
					string path;
					if (!EwsWsSecurityUrl.IsWsSecurity(url.AbsoluteUri))
					{
						path = "/autodiscover/autodiscover.svc";
					}
					else
					{
						path = EwsWsSecurityUrl.Fix("/autodiscover/autodiscover.svc");
					}
					UriBuilder uriBuilder = new UriBuilder
					{
						Scheme = Uri.UriSchemeHttps,
						Host = uri2.Host,
						Path = path
					};
					AutodiscoverClient.Tracer.TraceDebug<Uri, Uri>((long)this.GetHashCode(), "Handling secure redirect from URL {0} to URL {1}", uri, uriBuilder.Uri);
					uri = uriBuilder.Uri;
					continue;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0003EE24 File Offset: 0x0003D024
		private static bool InvalidCertificateHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (sslPolicyErrors != SslPolicyErrors.None)
			{
				AutodiscoverClient.Tracer.TraceError<SslPolicyErrors>(0L, "SSL certificate errors: {0}", sslPolicyErrors);
			}
			if (SslConfiguration.AllowExternalUntrustedCerts)
			{
				AutodiscoverClient.Tracer.TraceDebug(0L, "Configuration tells to ignore certificate errors from external source");
				return true;
			}
			if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				AutodiscoverClient.Tracer.TraceDebug(0L, "SSL certificate has hostname mismatch");
				AutodiscoverClient.staticInvalidHostnames = AutodiscoverClient.GetCertificateHostnames(certificate);
				return true;
			}
			return false;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0003EE88 File Offset: 0x0003D088
		private static StringList GetCertificateHostnames(X509Certificate certificate)
		{
			StringList stringList = new StringList();
			if (certificate.Subject != null)
			{
				string[] array = certificate.Subject.Split(AutodiscoverClient.CertificateSubjectDelimiters, StringSplitOptions.RemoveEmptyEntries);
				if (array != null)
				{
					foreach (string line in array)
					{
						AutodiscoverClient.ParseCertificateHostnames("CN", line, stringList);
					}
				}
			}
			X509Certificate2 x509Certificate = certificate as X509Certificate2;
			if (x509Certificate != null && x509Certificate.Extensions != null)
			{
				X509Extension x509Extension = x509Certificate.Extensions["Subject Alternative Name"];
				if (x509Extension != null)
				{
					AsnEncodedData asnEncodedData = new AsnEncodedData(x509Extension.Oid, x509Extension.RawData);
					AsnEncodedDataCollection asnEncodedDataCollection = new AsnEncodedDataCollection(asnEncodedData);
					foreach (AsnEncodedData asnEncodedData2 in asnEncodedDataCollection)
					{
						AutodiscoverClient.ParseCertificateHostnames("DNS Name", asnEncodedData2.Format(false), stringList);
					}
				}
			}
			return stringList;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0003EF74 File Offset: 0x0003D174
		private static void ParseCertificateHostnames(string key, string line, StringList hostnames)
		{
			string[] array = line.Split(AutodiscoverClient.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
			if (array != null)
			{
				foreach (string text in array)
				{
					string[] array3 = text.Split(AutodiscoverClient.EqualDelimiter, StringSplitOptions.RemoveEmptyEntries);
					if (array3 != null && array3.Length == 2)
					{
						string x = array3[0].Trim();
						if (StringComparer.OrdinalIgnoreCase.Equals(x, key))
						{
							string hostname = array3[1].Trim();
							if (!string.IsNullOrEmpty(hostname))
							{
								if (!hostnames.Exists((string item) => StringComparer.OrdinalIgnoreCase.Equals(item, hostname)))
								{
									AutodiscoverClient.Tracer.TraceDebug<string, string>(0L, "From item {0} in SSL certificate identified hostname: {1}", text, hostname);
									hostnames.Add(hostname);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x0003F054 File Offset: 0x0003D254
		private AutodiscoverResultData InvokeForUnsecureRedirect(InvokeDelegate invokeDelegate, string host)
		{
			AutodiscoverResultData autodiscoverResultData = this.DiscoverUnsecuredRedirect(host);
			AutodiscoverClient.Tracer.TraceDebug<AutodiscoverResultData>((long)this.GetHashCode(), "Unsecure redirect result: {0}", autodiscoverResultData);
			if (autodiscoverResultData.Type != AutodiscoverResult.Success)
			{
				return autodiscoverResultData;
			}
			AutodiscoverResultData autodiscoverResultData2 = this.InvokeForHost(invokeDelegate, autodiscoverResultData.RedirectUrl.Host);
			AutodiscoverClient.Tracer.TraceDebug<AutodiscoverResultData>((long)this.GetHashCode(), "Result retrieved from unsecure redirect: {0}", autodiscoverResultData2);
			if (autodiscoverResultData2.Type != AutodiscoverResult.Success)
			{
				return new AutodiscoverResultData
				{
					Type = AutodiscoverResult.Failure,
					Url = autodiscoverResultData.Url,
					RedirectUrl = autodiscoverResultData.RedirectUrl,
					Alternate = autodiscoverResultData2
				};
			}
			if (this.IsAllowedHostname(autodiscoverResultData.RedirectUrl.Host))
			{
				return autodiscoverResultData2;
			}
			return new AutodiscoverResultData
			{
				Type = AutodiscoverResult.UnsecuredRedirect,
				Url = autodiscoverResultData.Url,
				RedirectUrl = autodiscoverResultData.RedirectUrl,
				Alternate = autodiscoverResultData2
			};
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x0003F198 File Offset: 0x0003D398
		private AutodiscoverResultData DiscoverUnsecuredRedirect(string host)
		{
			UriBuilder uriBuilder = new UriBuilder
			{
				Scheme = Uri.UriSchemeHttp,
				Host = host,
				Path = "/autodiscover/autodiscover.xml"
			};
			Uri url = uriBuilder.Uri;
			AutodiscoverClient.Tracer.TraceDebug<Uri>(0L, "Calling {0}", url);
			HttpWebResponse response = null;
			Exception ex = null;
			try
			{
				this.InvokeWithWebProxy(url.ToString(), delegate(IWebProxy webProxy)
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url.ToString());
					httpWebRequest.Proxy = webProxy;
					httpWebRequest.UserAgent = "AutodiscoverClient";
					httpWebRequest.Timeout = 30000;
					httpWebRequest.AllowAutoRedirect = false;
					httpWebRequest.ServicePoint.Expect100Continue = false;
					response = (HttpWebResponse)httpWebRequest.GetResponse();
				});
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (WebException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				AutodiscoverClient.Tracer.TraceError<Uri, Exception>(0L, "Unable to connect to {0}. Exception: {1}", url, ex);
				return new AutodiscoverResultData
				{
					Type = AutodiscoverResult.Failure,
					Url = url,
					Exception = ex
				};
			}
			if (response == null)
			{
				AutodiscoverClient.Tracer.TraceError<Uri>(0L, "No response from {0}.", url);
				return new AutodiscoverResultData
				{
					Type = AutodiscoverResult.Failure,
					Url = url,
					Exception = new InvalidDataException("No response")
				};
			}
			AutodiscoverResultData result;
			using (response)
			{
				if (response.StatusCode != HttpStatusCode.Found)
				{
					AutodiscoverClient.Tracer.TraceError<Uri, HttpStatusCode>(0L, "Request to {0} did not result in 302 Found. Status: {1}", url, response.StatusCode);
					result = new AutodiscoverResultData
					{
						Type = AutodiscoverResult.Failure,
						Url = url,
						Exception = new AutodiscoverClientException(NetException.UnexpectedStatusCode(response.StatusCode.ToString()))
					};
				}
				else
				{
					string text = response.Headers[HttpResponseHeader.Location];
					if (!Uri.IsWellFormedUriString(text, UriKind.Absolute))
					{
						AutodiscoverClient.Tracer.TraceError<Uri, string>(0L, "302 from {0} returned malformed URL for location header: {1}", url, text);
						result = new AutodiscoverResultData
						{
							Type = AutodiscoverResult.Failure,
							Url = url,
							Exception = new AutodiscoverClientException(NetException.MalformedLocationHeader(text))
						};
					}
					else
					{
						Uri uri = new Uri(text);
						if (uri.Scheme != Uri.UriSchemeHttps)
						{
							AutodiscoverClient.Tracer.TraceError<Uri, string>(0L, "302 from {0} returned non-HTTPS URL: {1}", url, text);
							result = new AutodiscoverResultData
							{
								Type = AutodiscoverResult.Failure,
								Url = url,
								Exception = new AutodiscoverClientException(NetException.NonHttpsLocationHeader(text))
							};
						}
						else if (!StringComparer.OrdinalIgnoreCase.Equals(uri.PathAndQuery, "/autodiscover/autodiscover.xml"))
						{
							AutodiscoverClient.Tracer.TraceError<Uri, string>(0L, "302 from {0} returned unexpected URL: {1}", url, text);
							result = new AutodiscoverResultData
							{
								Type = AutodiscoverResult.Failure,
								Url = url,
								Exception = new AutodiscoverClientException(NetException.UnexpectedPathLocationHeader(text))
							};
						}
						else
						{
							AutodiscoverClient.Tracer.TraceDebug<Uri, string>(0L, "302 from {0} returned URL: {1}", url, text);
							result = new AutodiscoverResultData
							{
								Type = AutodiscoverResult.Success,
								Url = url,
								RedirectUrl = uri
							};
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0003F548 File Offset: 0x0003D748
		private void InvokeWithWebProxy(string url, AutodiscoverClient.InvokeWithWebProxyDelegate invokeWithWebProxy)
		{
			if (this.Proxy != null)
			{
				AutodiscoverClient.Tracer.TraceDebug<string, IWebProxy>((long)this.GetHashCode(), "Trying to connect to {0} endpoint via web proxy {1}", url, this.Proxy);
				try
				{
					invokeWithWebProxy(this.Proxy);
					return;
				}
				catch (WebException ex)
				{
					AutodiscoverClient.Tracer.TraceDebug<string, IWebProxy, WebException>((long)this.GetHashCode(), "Failed to connect to {0} endpoint via web proxy {1} due exception: {2}", url, this.Proxy, ex);
					Exception ex2 = null;
					try
					{
						AutodiscoverClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Trying to connect to {0} endpoint without web proxy", url);
						invokeWithWebProxy(AutodiscoverClient.NoProxy);
					}
					catch (WebException ex3)
					{
						ex2 = ex3;
					}
					catch (IOException ex4)
					{
						ex2 = ex4;
					}
					if (ex2 != null)
					{
						AutodiscoverClient.Tracer.TraceError<string, WebException>((long)this.GetHashCode(), "Failed to connect to {0} endpoint without web proxy due exception: {1}", url, ex);
						throw;
					}
					return;
				}
			}
			AutodiscoverClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Trying to connect to {0} endpoint without web proxy", url);
			invokeWithWebProxy(null);
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0003F644 File Offset: 0x0003D844
		private bool IsAllowedHostname(IEnumerable<string> hostnames)
		{
			foreach (string hostname in hostnames)
			{
				if (this.IsAllowedHostname(hostname))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0003F698 File Offset: 0x0003D898
		private bool IsAllowedHostname(string hostname)
		{
			foreach (string allowedHostname in this.allowedHostnames)
			{
				if (this.IsHostnameMatch(allowedHostname, hostname))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
		private bool IsHostnameMatch(string allowedHostname, string hostname)
		{
			if (!allowedHostname.StartsWith("*.", StringComparison.OrdinalIgnoreCase))
			{
				return StringComparer.OrdinalIgnoreCase.Equals(allowedHostname, hostname);
			}
			string x = allowedHostname.Substring(2);
			if (StringComparer.OrdinalIgnoreCase.Equals(x, hostname))
			{
				return true;
			}
			string value = allowedHostname.Substring(1);
			return hostname.EndsWith(value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04001F16 RID: 7958
		private const int WebRequestTimeout = 30000;

		// Token: 0x04001F17 RID: 7959
		private const string CertificateSubjectKey = "CN";

		// Token: 0x04001F18 RID: 7960
		private const string CertificateSubjectAlternativeNameKey = "DNS Name";

		// Token: 0x04001F19 RID: 7961
		private const string CertificateSubjectAlternativeName = "Subject Alternative Name";

		// Token: 0x04001F1A RID: 7962
		private const string AgentName = "AutodiscoverClient";

		// Token: 0x04001F1B RID: 7963
		private const string AutodiscoverPath = "/autodiscover/";

		// Token: 0x04001F1C RID: 7964
		private const string SoapAutodiscoverPath = "/autodiscover/autodiscover.svc";

		// Token: 0x04001F1D RID: 7965
		private const string XmlAutodiscoverPath = "/autodiscover/autodiscover.xml";

		// Token: 0x04001F1E RID: 7966
		private const string AutoDiscoverHostnameFormat = "autodiscover.{0}";

		// Token: 0x04001F1F RID: 7967
		private const int MaximumRedirects = 5;

		// Token: 0x04001F20 RID: 7968
		private static readonly WebProxy NoProxy = new WebProxy();

		// Token: 0x04001F21 RID: 7969
		[ThreadStatic]
		private static StringList staticInvalidHostnames;

		// Token: 0x04001F22 RID: 7970
		private DefaultBinding_Autodiscover binding;

		// Token: 0x04001F23 RID: 7971
		private StringList allowedHostnames = new StringList();

		// Token: 0x04001F24 RID: 7972
		private static readonly char[] EqualDelimiter = new char[]
		{
			'='
		};

		// Token: 0x04001F25 RID: 7973
		private static readonly char[] CommaDelimiter = new char[]
		{
			','
		};

		// Token: 0x04001F26 RID: 7974
		private static readonly char[] CertificateSubjectDelimiters = new char[]
		{
			'\n',
			'\r'
		};

		// Token: 0x04001F27 RID: 7975
		private static Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.EwsClientTracer;

		// Token: 0x020006BF RID: 1727
		// (Invoke) Token: 0x06002045 RID: 8261
		private delegate AutodiscoverResultData AutoDiscoverStep();

		// Token: 0x020006C0 RID: 1728
		// (Invoke) Token: 0x06002049 RID: 8265
		private delegate void InvokeWithWebProxyDelegate(IWebProxy webProxy);
	}
}
