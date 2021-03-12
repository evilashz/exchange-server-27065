using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200080C RID: 2060
	internal abstract class ServiceValidatorBase
	{
		// Token: 0x0600477A RID: 18298 RVA: 0x00125C58 File Offset: 0x00123E58
		public ServiceValidatorBase(string uri, NetworkCredential credentials)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (credentials == null)
			{
				throw new ArgumentNullException("credentials");
			}
			this.Uri = uri;
			this.Credentials = credentials;
			this.TraceResponseBody = true;
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x0600477B RID: 18299 RVA: 0x00125CA7 File Offset: 0x00123EA7
		// (set) Token: 0x0600477C RID: 18300 RVA: 0x00125CAF File Offset: 0x00123EAF
		public Task.TaskVerboseLoggingDelegate VerboseDelegate { get; set; }

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x0600477D RID: 18301 RVA: 0x00125CB8 File Offset: 0x00123EB8
		// (set) Token: 0x0600477E RID: 18302 RVA: 0x00125CC0 File Offset: 0x00123EC0
		public bool IgnoreSslCertError { get; set; }

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x00125CC9 File Offset: 0x00123EC9
		// (set) Token: 0x06004780 RID: 18304 RVA: 0x00125CD1 File Offset: 0x00123ED1
		public string UserAgent { get; set; }

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06004781 RID: 18305 RVA: 0x00125CDA File Offset: 0x00123EDA
		// (set) Token: 0x06004782 RID: 18306 RVA: 0x00125CE2 File Offset: 0x00123EE2
		public bool TraceResponseBody { get; set; }

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06004783 RID: 18307 RVA: 0x00125CEB File Offset: 0x00123EEB
		// (set) Token: 0x06004784 RID: 18308 RVA: 0x00125CF3 File Offset: 0x00123EF3
		public string Uri { get; private set; }

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06004785 RID: 18309 RVA: 0x00125CFC File Offset: 0x00123EFC
		// (set) Token: 0x06004786 RID: 18310 RVA: 0x00125D04 File Offset: 0x00123F04
		public NetworkCredential Credentials { get; private set; }

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06004787 RID: 18311 RVA: 0x00125D0D File Offset: 0x00123F0D
		// (set) Token: 0x06004788 RID: 18312 RVA: 0x00125D15 File Offset: 0x00123F15
		public Exception Error { get; private set; }

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06004789 RID: 18313 RVA: 0x00125D1E File Offset: 0x00123F1E
		// (set) Token: 0x0600478A RID: 18314 RVA: 0x00125D26 File Offset: 0x00123F26
		public long Latency { get; private set; }

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x0600478B RID: 18315 RVA: 0x00125D2F File Offset: 0x00123F2F
		public string Verbose
		{
			get
			{
				return string.Join("\n", this.verboseEntries.ToArray());
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x0600478C RID: 18316
		protected abstract string Name { get; }

		// Token: 0x0600478D RID: 18317 RVA: 0x00125D48 File Offset: 0x00123F48
		public bool Invoke()
		{
			try
			{
				this.PreCreateRequest();
				this.Error = this.InternalInvoke();
			}
			catch (WebException ex)
			{
				this.Error = ex;
				if (ex.Response != null && ex.Response.Headers != null)
				{
					this.TraceResponse(ServiceValidatorBase.FormatHeaders(ex.Response.Headers));
				}
			}
			catch (InvalidOperationException error)
			{
				this.Error = error;
			}
			if (this.Error != null)
			{
				this.TraceResponse(this.Error.ToString());
			}
			return this.Error == null;
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x00125DE8 File Offset: 0x00123FE8
		protected virtual void PreCreateRequest()
		{
			this.Error = null;
			this.Latency = 0L;
			this.verboseEntries.Clear();
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x00125E04 File Offset: 0x00124004
		protected virtual void FillRequestProperties(HttpWebRequest request)
		{
			request.ContentType = "text/xml;charset=utf-8";
			request.UserAgent = this.UserAgent;
			request.PreAuthenticate = true;
			request.CookieContainer = new CookieContainer();
			if (this.IgnoreSslCertError)
			{
				CertificateValidationManager.RegisterCallback(ServiceValidatorBase.ComponentId, new RemoteCertificateValidationCallback(ServiceValidatorBase.TrustAllCertValidationCallback));
				CertificateValidationManager.SetComponentId(request, ServiceValidatorBase.ComponentId);
			}
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00125E63 File Offset: 0x00124063
		protected virtual bool FillRequestStream(Stream requestStream)
		{
			return false;
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x00125E66 File Offset: 0x00124066
		protected virtual Exception ValidateResponse(Stream responseStream)
		{
			return null;
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00125E6C File Offset: 0x0012406C
		private Exception InternalInvoke()
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.Uri);
			httpWebRequest.Credentials = this.Credentials;
			this.FillRequestProperties(httpWebRequest);
			this.WriteVerbose(Strings.VerboseServiceValidatorUrl(this.Name, this.Uri));
			if (Datacenter.IsMultiTenancyEnabled())
			{
				this.WriteVerbose(Strings.VerboseServiceValidatorCredential(this.Credentials.UserName, this.Credentials.Password));
			}
			else
			{
				this.WriteVerbose(Strings.VerboseServiceValidatorCredential(this.Credentials.UserName, "******"));
			}
			string message = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (this.FillRequestStream(memoryStream))
				{
					byte[] array = memoryStream.ToArray();
					message = Encoding.UTF8.GetString(array);
					using (Stream requestStream = httpWebRequest.GetRequestStream())
					{
						requestStream.Write(array, 0, array.Length);
					}
				}
			}
			WebResponse webResponse = null;
			Exception result;
			try
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					webResponse = httpWebRequest.GetResponse();
				}
				finally
				{
					this.Latency = stopwatch.ElapsedMilliseconds;
					this.TraceRequest(ServiceValidatorBase.FormatHeaders(httpWebRequest.Headers));
					this.TraceRequest(message);
				}
				this.TraceResponse(ServiceValidatorBase.FormatHeaders(webResponse.Headers));
				using (Stream responseStream = webResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string text = streamReader.ReadToEnd();
						if (this.TraceResponseBody)
						{
							this.TraceResponse(text);
						}
						byte[] bytes = Encoding.UTF8.GetBytes(text);
						using (MemoryStream memoryStream2 = new MemoryStream(bytes))
						{
							result = this.ValidateResponse(memoryStream2);
						}
					}
				}
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

		// Token: 0x06004793 RID: 18323 RVA: 0x00126094 File Offset: 0x00124294
		private static string FormatHeaders(WebHeaderCollection headers)
		{
			string[] value = Array.ConvertAll<string, string>(headers.AllKeys, (string x) => string.Format("{0}: {1}", x, headers[x]));
			return string.Join("\n", value);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x001260D6 File Offset: 0x001242D6
		private void TraceRequest(string message)
		{
			this.WriteVerbose(Strings.VerboseServiceValidatorRequestTrace(this.Name, message));
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x001260EA File Offset: 0x001242EA
		private void TraceResponse(string message)
		{
			this.WriteVerbose(Strings.VerboseServiceValidatorResponseTrace(this.Name, this.TryFormatXml(message)));
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x00126104 File Offset: 0x00124304
		private void WriteVerbose(LocalizedString locString)
		{
			if (this.VerboseDelegate != null)
			{
				this.VerboseDelegate(locString);
			}
			this.verboseEntries.Add(string.Format("[{0}] {1}", ExDateTime.UtcNow.ToString("u"), locString));
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00126154 File Offset: 0x00124354
		private string TryFormatXml(string content)
		{
			if (string.IsNullOrEmpty(content))
			{
				return content;
			}
			string result;
			try
			{
				XmlDocument xmlDocument = new SafeXmlDocument();
				xmlDocument.LoadXml(content);
				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
				{
					Indent = true,
					Encoding = Encoding.UTF8
				}))
				{
					xmlDocument.WriteTo(xmlWriter);
				}
				result = stringBuilder.ToString();
			}
			catch (XmlException)
			{
				result = content;
			}
			return result;
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x001261E0 File Offset: 0x001243E0
		private static bool TrustAllCertValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x04002B51 RID: 11089
		public static readonly string ComponentId = "Management.ServiceValidator";

		// Token: 0x04002B52 RID: 11090
		private List<string> verboseEntries = new List<string>();
	}
}
