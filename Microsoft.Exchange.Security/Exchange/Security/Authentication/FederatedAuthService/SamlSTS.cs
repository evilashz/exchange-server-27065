using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200008A RID: 138
	internal class SamlSTS : STSBase
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0002746B File Offset: 0x0002566B
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0002747E File Offset: 0x0002567E
		public int MaxResponseSize
		{
			get
			{
				if (this.maxResponseSize >= 1)
				{
					return this.maxResponseSize;
				}
				return 1;
			}
			set
			{
				this.maxResponseSize = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00027487 File Offset: 0x00025687
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0002748E File Offset: 0x0002568E
		public static bool VerifySamlXml { get; set; } = true;

		// Token: 0x060004C6 RID: 1222 RVA: 0x000274D1 File Offset: 0x000256D1
		private SamlSTS()
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000274E4 File Offset: 0x000256E4
		public SamlSTS(int traceId, LiveIdInstanceType instance, NamespaceStats stats) : base(traceId, instance, stats)
		{
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000274FA File Offset: 0x000256FA
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00027502 File Offset: 0x00025702
		public string ShibbolethLogonURI { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0002750B File Offset: 0x0002570B
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00027513 File Offset: 0x00025713
		public string TokenIssuerURI { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0002751C File Offset: 0x0002571C
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00027524 File Offset: 0x00025724
		public string AssertionConsumerService { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0002752D File Offset: 0x0002572D
		public override string StsTag
		{
			get
			{
				return "SHIBB";
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00027548 File Offset: 0x00025748
		public IAsyncResult StartRequestChain(byte[] ansiUserName, byte[] ansiPassword, AsyncCallback callback, object state)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering SamlSTS.StartRequestChain()");
			if (ansiUserName == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError((long)this.traceId, "ansiUserName is null");
				throw new ArgumentNullException("ansiUserName");
			}
			if (ansiPassword == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError((long)this.traceId, "ansiPassword is null");
				throw new ArgumentNullException("ansiPassword");
			}
			this.traceUserName = Encoding.Default.GetString(ansiUserName);
			Interlocked.Increment(ref SamlSTS.numberOfOutgoingRequests);
			STSBase.counters.NumberOfOutgoingSamlStsRequests.RawValue = (long)SamlSTS.numberOfOutgoingRequests;
			ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Creating http logon request to shibboleth STS '{0}' for \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
			this.stopwatch = Stopwatch.StartNew();
			byte[] array = null;
			byte[] array2 = null;
			char[] array3 = null;
			try
			{
				byte atSign = Convert.ToByte('@');
				int num = Array.FindIndex<byte>(ansiUserName, (byte s) => s == atSign);
				if (num == -1)
				{
					num = ansiUserName.Length;
				}
				array = new byte[num];
				Array.Copy(ansiUserName, array, num);
				int num2 = SamlSTS.httpAuthBytesP2.Length + array.Length + ansiPassword.Length;
				array2 = new byte[num2];
				array.CopyTo(array2, 0);
				SamlSTS.httpAuthBytesP2.CopyTo(array2, array.Length);
				ansiPassword.CopyTo(array2, array.Length + SamlSTS.httpAuthBytesP2.Length);
				array3 = new char[SamlSTS.httpAuthCharsP1.Length + (num2 + 2) / 3 * 4];
				SamlSTS.httpAuthCharsP1.CopyTo(array3, 0);
				Convert.ToBase64CharArray(array2, 0, array2.Length, array3, SamlSTS.httpAuthCharsP1.Length);
				this.shibbRequest = (HttpWebRequest)WebRequest.Create(this.ShibbolethLogonURI);
				this.shibbRequest.Method = "POST";
				this.shibbRequest.ContentType = "text/xml; charset=utf-8";
				this.shibbRequest.ServicePoint.Expect100Continue = false;
				this.shibbRequest.Headers[HttpRequestHeader.Authorization] = new string(array3);
				this.shibbRequest.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AuthService.CertificateValidationCallBack);
				if (base.ExtraHeaders != null)
				{
					this.shibbRequest.Headers.Add(base.ExtraHeaders);
				}
			}
			finally
			{
				if (array != null)
				{
					Array.Clear(array, 0, array.Length);
				}
				if (array2 != null)
				{
					Array.Clear(array2, 0, array2.Length);
				}
				if (array3 != null)
				{
					Array.Clear(array3, 0, array2.Length);
				}
			}
			IAsyncResult result = this.shibbRequest.BeginGetRequestStream(callback, state);
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving SamlSTS.StartRequestChain()");
			return result;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000277D0 File Offset: 0x000259D0
		public IAsyncResult ProcessRequest(IAsyncResult asyncResult, AsyncCallback callback, object state)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering SamlSTS.ProcessRequest()");
			Stream stream = this.shibbRequest.EndGetRequestStream(asyncResult);
			string s = string.Format("<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\"><S:Body><samlp:AuthnRequest xmlns:samlp=\"urn:oasis:names:tc:SAML:2.0:protocol\" xmlns:saml=\"urn:oasis:names:tc:SAML:2.0:assertion\" ID=\"_{0}\" IssueInstant=\"{1}\" Version=\"2.0\" {2}><saml:Issuer>{3}</saml:Issuer></samlp:AuthnRequest></S:Body></S:Envelope>", new object[]
			{
				Guid.NewGuid().ToString(),
				(DateTime.UtcNow + base.ClockSkew).ToString("o"),
				this.AssertionConsumerService,
				this.TokenIssuerURI
			});
			STSBase.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
			stream.Close();
			IAsyncResult result = this.shibbRequest.BeginGetResponse(callback, state);
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving SamlSTS.ProcessRequest()");
			return result;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0002789C File Offset: 0x00025A9C
		public byte[] ProcessResponse(IAsyncResult asyncResult)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering SamlSTS.ProcessResponse()");
			ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Processing response to http logon request to Saml STS '{0}' for \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
			byte[] array = null;
			WebResponse webResponse = null;
			byte[] result;
			try
			{
				try
				{
					try
					{
						webResponse = this.shibbRequest.EndGetResponse(asyncResult);
					}
					catch (WebException ex)
					{
						if (ex.Status == WebExceptionStatus.ProtocolError)
						{
							switch (((HttpWebResponse)ex.Response).StatusCode)
							{
							case HttpStatusCode.Unauthorized:
							case HttpStatusCode.Forbidden:
								webResponse = ex.Response;
								base.IsBadCredentials = true;
								goto IL_DC;
							}
							throw;
						}
						string str;
						if (ex.Status == WebExceptionStatus.TrustFailure && AuthService.CertErrorCache.TryGetValue(this.shibbRequest, out str))
						{
							base.ErrorString += str;
							AuthService.CertErrorCache.Remove(this.shibbRequest);
						}
						throw;
					}
					IL_DC:;
				}
				finally
				{
					this.stopwatch.Stop();
					base.Latency = this.stopwatch.ElapsedMilliseconds;
				}
				STSBase.counters.AverageSamlStsResponseTime.IncrementBy(this.stopwatch.ElapsedMilliseconds);
				STSBase.counters.AverageSamlStsResponseTimeBase.Increment();
				ExTraceGlobals.AuthenticationTracer.TracePerformance<string, long>((long)this.traceId, "Shibboleth STS '{0}' responded in {1}ms", this.ShibbolethLogonURI, this.stopwatch.ElapsedMilliseconds);
				HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
				TimeSpan clockSkew = base.ClockSkew;
				if (base.CalculateClockSkew(httpWebResponse))
				{
					ExTraceGlobals.AuthenticationTracer.TraceDebug<string, int>((long)this.traceId, "Clock skew between Shibb server {0} and local server is {1} seconds", this.ShibbolethLogonURI, base.ClockSkew.Seconds);
				}
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
					{
						SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
						safeXmlDocument.PreserveWhitespace = true;
						char[] array2 = new char[this.MaxResponseSize];
						int num = -1;
						int num2 = 0;
						while (!streamReader.EndOfStream && num2 < this.MaxResponseSize && num != 0)
						{
							num = streamReader.Read(array2, num2, this.MaxResponseSize - num2);
							num2 += num;
						}
						if (!streamReader.EndOfStream)
						{
							Interlocked.Increment(ref this.namespaceStats.TokenSize);
							this.namespaceStats.User = this.traceUserName;
							ExTraceGlobals.AuthenticationTracer.TraceError<int>((long)this.traceId, "Shibboleth STS returning more data than expected. More than {0} bytes", num2);
							base.ErrorString = "Shibboleth STS returned too much data";
							return array;
						}
						string text = new string(array2, 0, num2);
						ExTraceGlobals.AuthenticationTracer.TraceDebug<HttpStatusCode, string>((long)this.traceId, "Shibboleth STS returned response {0:d} {1}", httpWebResponse.StatusCode, text);
						if (httpWebResponse.StatusCode != HttpStatusCode.OK)
						{
							base.ErrorString = ((int)httpWebResponse.StatusCode).ToString();
							Interlocked.Increment(ref this.namespaceStats.BadPassword);
							this.namespaceStats.User = this.traceUserName;
							ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Logon failed to Shibboleth STS '{0}' for \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
							ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving SamlSTS.ProcessResponse()");
							return array;
						}
						try
						{
							safeXmlDocument.LoadXml(text);
						}
						catch (XmlException ex2)
						{
							base.ErrorString = string.Format("Shibboleth STS '{0}' has malformed RST response for user \"{1}\".  Exception {2} XML response {3}", new object[]
							{
								this.ShibbolethLogonURI,
								this.traceUserName,
								ex2.ToString(),
								text
							});
							ExTraceGlobals.AuthenticationTracer.TraceError((long)this.traceId, base.ErrorString);
							throw;
						}
						XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(safeXmlDocument.NameTable);
						xmlNamespaceManager.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
						xmlNamespaceManager.AddNamespace("saml2p", "urn:oasis:names:tc:SAML:2.0:protocol");
						xmlNamespaceManager.AddNamespace("saml2", "urn:oasis:names:tc:SAML:2.0:assertion");
						xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
						XmlNode xmlNode = safeXmlDocument.SelectSingleNode("/s:Envelope/s:Body/saml2p:Response", xmlNamespaceManager);
						if (xmlNode == null)
						{
							if (safeXmlDocument.SelectSingleNode("/s:Envelope", xmlNamespaceManager) == null)
							{
								base.ErrorString = "SamlSTS response is XML but not SOAP " + text;
							}
							else
							{
								xmlNode = safeXmlDocument.SelectSingleNode("/s:Envelope/s:Body/s:Fault", xmlNamespaceManager);
								if (xmlNode != null)
								{
									base.ErrorString = xmlNode.OuterXml;
								}
								else
								{
									base.ErrorString = "SamlSTS response is missing saml2p:Response" + text;
								}
								if ((base.ClockSkew - clockSkew).Duration() >= base.ClockSkewThreshold)
								{
									ExTraceGlobals.AuthenticationTracer.Information<string, int>((long)this.traceId, "Clock skew for shibboleth endpoint '{0}' changed more than '{1}' minutes - possible clock skew failure", this.ShibbolethLogonURI, base.ClockSkew.Minutes);
									base.PossibleClockSkew = true;
								}
							}
							Interlocked.Increment(ref this.namespaceStats.Failed);
							this.namespaceStats.User = this.traceUserName;
							ExTraceGlobals.AuthenticationTracer.TraceWarning<string, string, string>((long)this.traceId, "Shibboleth STS '{0}' has missing saml assertion for user \"{1}\". XML = {2}", this.ShibbolethLogonURI, this.traceUserName, text);
							ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving SamlSTS.ProcessResponse()");
							return array;
						}
						array = Encoding.UTF8.GetBytes(xmlNode.OuterXml);
						ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Successfully processed response to http logon request to Shibboleth STS '{0}' for \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
						if (SamlSTS.VerifySamlXml)
						{
							XmlNode xmlNode2 = safeXmlDocument.SelectSingleNode("/s:Envelope/s:Body/saml2p:Response/ds:Signature", xmlNamespaceManager);
							XmlNode xmlNode3 = safeXmlDocument.SelectSingleNode("/s:Envelope/s:Body/saml2p:Response/saml2:Assertion/ds:Signature", xmlNamespaceManager);
							SignedXml signedXml = new SignedXml(safeXmlDocument);
							if (xmlNode2 != null)
							{
								signedXml.LoadXml((XmlElement)xmlNode2);
								if (!signedXml.CheckSignature())
								{
									base.ErrorString = "Saml Response has invalid signature" + text;
									Interlocked.Increment(ref this.namespaceStats.Failed);
									this.namespaceStats.User = this.traceUserName;
									ExTraceGlobals.AuthenticationTracer.TraceError<string, string>((long)this.traceId, "SAML Response failed signature check for server {0} and user \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
									ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving SamlSTS.ProcessResponse()");
									return null;
								}
								ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>((long)this.traceId, "SAML Response passed signature check for server {0} and user \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
							}
							if (xmlNode3 != null)
							{
								signedXml.LoadXml((XmlElement)xmlNode3);
								if (!signedXml.CheckSignature())
								{
									base.ErrorString = "Saml Assertion has invalid signature" + text;
									Interlocked.Increment(ref this.namespaceStats.Failed);
									this.namespaceStats.User = this.traceUserName;
									ExTraceGlobals.AuthenticationTracer.TraceError<string, string>((long)this.traceId, "SAML Assertion failed signature check for server {0} and user \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
									ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving SamlSTS.ProcessResponse()");
									return null;
								}
								ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>((long)this.traceId, "SAML Assertion passed signature check for server {0} and user \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
							}
						}
					}
				}
				ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving SamlSTS.ProcessResponse()");
				result = array;
			}
			finally
			{
				if (webResponse != null)
				{
					((IDisposable)webResponse).Dispose();
				}
			}
			return result;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0002803C File Offset: 0x0002623C
		public void Abort()
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Entering SamlSTS.Abort()");
			if (this.shibbRequest != null)
			{
				ExTraceGlobals.AuthenticationTracer.Information<string, string>((long)this.traceId, "Aborting http logon request to Shibboleth STS '{0}' for \"{1}\"", this.ShibbolethLogonURI, this.traceUserName);
				this.shibbRequest.Abort();
			}
			ExTraceGlobals.AuthenticationTracer.TraceFunction((long)this.traceId, "Leaving SamlSTS.Abort()");
		}

		// Token: 0x04000528 RID: 1320
		private const string shibbBody = "<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\"><S:Body><samlp:AuthnRequest xmlns:samlp=\"urn:oasis:names:tc:SAML:2.0:protocol\" xmlns:saml=\"urn:oasis:names:tc:SAML:2.0:assertion\" ID=\"_{0}\" IssueInstant=\"{1}\" Version=\"2.0\" {2}><saml:Issuer>{3}</saml:Issuer></samlp:AuthnRequest></S:Body></S:Envelope>";

		// Token: 0x04000529 RID: 1321
		private static int numberOfOutgoingRequests;

		// Token: 0x0400052A RID: 1322
		private static readonly char[] httpAuthCharsP1 = "Basic ".ToCharArray();

		// Token: 0x0400052B RID: 1323
		private static readonly byte[] httpAuthBytesP2 = new byte[]
		{
			Convert.ToByte(':')
		};

		// Token: 0x0400052C RID: 1324
		private int maxResponseSize = 131072;

		// Token: 0x0400052D RID: 1325
		private HttpWebRequest shibbRequest;

		// Token: 0x0400052E RID: 1326
		private Stopwatch stopwatch;

		// Token: 0x0400052F RID: 1327
		private string traceUserName;
	}
}
