using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharePointSignalStore
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SharePointSignalRestSender : ISharePointSender<string>
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000258B File Offset: 0x0000078B
		public SharePointSignalRestSender(ICredentials credentials, string server, ILogger logger)
		{
			this.Timeout = 100000;
			this.credentials = credentials;
			this.server = server;
			this.logger = logger;
			this.UserAgentString = "OfficeGraphSignals-EXO-SystemUsage";
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000025BE File Offset: 0x000007BE
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000025C6 File Offset: 0x000007C6
		public int Timeout { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000025CF File Offset: 0x000007CF
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000025D7 File Offset: 0x000007D7
		public string UserAgentString { get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x000025E0 File Offset: 0x000007E0
		public static string GetFormDigestResponse(HttpWebRequest requestPost, ILogger logger)
		{
			HttpWebResponse httpWebResponse = null;
			try
			{
				httpWebResponse = (HttpWebResponse)requestPost.GetResponse();
				XDocument xdocument = XDocument.Load(httpWebResponse.GetResponseStream());
				XNamespace ns = "http://schemas.microsoft.com/ado/2007/08/dataservices";
				return xdocument.Descendants(ns + "FormDigestValue").First<XElement>().Value;
			}
			catch (WebException webException)
			{
				SharePointSignalRestSender.ThrowDetailedWebException(webException, logger);
			}
			finally
			{
				if (httpWebResponse != null)
				{
					httpWebResponse.Close();
				}
			}
			return null;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002668 File Offset: 0x00000868
		public static void CheckPostResponse(HttpWebRequest request, ILogger logger)
		{
			WebResponse webResponse = null;
			try
			{
				webResponse = request.GetResponse();
			}
			catch (WebException webException)
			{
				SharePointSignalRestSender.ThrowDetailedWebException(webException, logger);
			}
			finally
			{
				if (webResponse != null)
				{
					webResponse.Close();
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026B4 File Offset: 0x000008B4
		public static void WriteBodyToRequestStream(HttpWebRequest request, string body)
		{
			using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				streamWriter.Write(body);
				streamWriter.Flush();
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026F8 File Offset: 0x000008F8
		public static void ThrowDetailedWebException(WebException webException, ILogger logger)
		{
			string text = null;
			try
			{
				if (webException.Response != null)
				{
					HttpWebResponse httpWebResponse = (HttpWebResponse)webException.Response;
					logger.LogWarning("Http error code: {0}", new object[]
					{
						httpWebResponse.StatusCode
					});
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							text = streamReader.ReadToEnd();
						}
						goto IL_78;
					}
				}
				logger.LogWarning("Failed to retrieve detailed error message: HttpWebResponse not available", new object[0]);
				IL_78:;
			}
			catch (Exception ex)
			{
				logger.LogWarning("Failed to retrieve detailed error message: {0}", new object[]
				{
					ex
				});
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				logger.LogWarning("Http response: {0}", new object[]
				{
					text
				});
				throw new WebException(webException.Message + " " + text, webException);
			}
			throw webException;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002804 File Offset: 0x00000A04
		public HttpWebRequest CreateDigestHttpRequest(string server, ICredentials credentials, int timeout)
		{
			Uri requestUri = new Uri(new Uri(server), "/_api/contextinfo");
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			httpWebRequest.Credentials = credentials;
			httpWebRequest.Method = "POST";
			httpWebRequest.UserAgent = this.UserAgentString;
			httpWebRequest.ContentLength = 0L;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer");
			httpWebRequest.Timeout = timeout;
			return httpWebRequest;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002878 File Offset: 0x00000A78
		public HttpWebRequest CreatePostHttpRequest(string server, string digest, string body, ICredentials credentials, int timeout)
		{
			Uri requestUri = new Uri(new Uri(server), "/_api/signalstore/signals");
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			httpWebRequest.Method = "POST";
			httpWebRequest.Credentials = credentials;
			httpWebRequest.Accept = "application/json;odata=verbose";
			httpWebRequest.Headers.Add("x-requestdigest", digest);
			httpWebRequest.ContentType = "application/json;odata=verbose";
			httpWebRequest.UserAgent = this.UserAgentString;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer");
			httpWebRequest.ContentLength = (long)Encoding.UTF8.GetByteCount(body);
			httpWebRequest.Timeout = timeout;
			return httpWebRequest;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000291C File Offset: 0x00000B1C
		public void SetData(string data)
		{
			this.data = data;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002928 File Offset: 0x00000B28
		public void Send()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			string digest = this.GetDigest(this.server, this.credentials);
			stopwatch.Stop();
			this.logger.LogInfo("Retrieved form digest {0} (used {1} seconds)", new object[]
			{
				digest,
				stopwatch.Elapsed.TotalSeconds
			});
			stopwatch.Restart();
			this.Post(this.server, digest, this.data, this.credentials);
			stopwatch.Stop();
			this.logger.LogInfo("Signals posted (used {0} seconds)", new object[]
			{
				stopwatch.Elapsed.TotalSeconds
			});
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029E4 File Offset: 0x00000BE4
		public string GetDigest(string server, ICredentials credentials)
		{
			HttpWebRequest requestPost = this.CreateDigestHttpRequest(server, credentials, this.Timeout);
			return SharePointSignalRestSender.GetFormDigestResponse(requestPost, this.logger);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A0C File Offset: 0x00000C0C
		public void Post(string server, string digest, string signals, ICredentials credentials)
		{
			HttpWebRequest request = this.CreatePostHttpRequest(server, digest, signals, credentials, this.Timeout);
			SharePointSignalRestSender.WriteBodyToRequestStream(request, signals);
			SharePointSignalRestSender.CheckPostResponse(request, this.logger);
		}

		// Token: 0x0400001F RID: 31
		public const int DefaultTimeout = 100000;

		// Token: 0x04000020 RID: 32
		private const string DefaultUserAgentString = "OfficeGraphSignals-EXO-SystemUsage";

		// Token: 0x04000021 RID: 33
		private readonly string server;

		// Token: 0x04000022 RID: 34
		private readonly ILogger logger;

		// Token: 0x04000023 RID: 35
		private string data;

		// Token: 0x04000024 RID: 36
		private ICredentials credentials;
	}
}
