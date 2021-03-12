using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Net.SharePoint
{
	// Token: 0x0200096E RID: 2414
	public class WebDavSharepointSession : ISharePointSession
	{
		// Token: 0x06003437 RID: 13367 RVA: 0x0007FC44 File Offset: 0x0007DE44
		public WebDavSharepointSession(ICredentials webServiceCreds, bool enableHttpDebugProxy, TimeSpan requestTimeout)
		{
			if (webServiceCreds == null)
			{
				throw new ArgumentNullException("webServiceCreds");
			}
			this.webServiceCreds = webServiceCreds;
			this.enableHttpDebugProxy = enableHttpDebugProxy;
			this.requestTimeout = requestTimeout;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x0007FC98 File Offset: 0x0007DE98
		public WebDavSharepointSession(ICredentials webServiceCreds, bool enableHttpDebugProxy, TimeSpan requestTimeout, int heartBeatInterval, int copyStreamBufferSize, int maxFilesToListPerFolder) : this(webServiceCreds, enableHttpDebugProxy, requestTimeout)
		{
			this.heartBeatInterval = heartBeatInterval;
			this.copyStreamBufferSize = copyStreamBufferSize;
			this.maxFilesToListPerFolder = maxFilesToListPerFolder;
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x0007FCBC File Offset: 0x0007DEBC
		public static string GetDetailedWebExceptionMessage(WebException e, string reqUrl)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			HttpWebResponse httpWebResponse = e.Response as HttpWebResponse;
			return string.Format("ReqUrl:{0}; Status{1}; Message:{2}; HttpResponse.StatusCode:{3}; HttpResponse.Method:{4}; HttpResponse.ResponseUri:{5}", new object[]
			{
				reqUrl,
				e.Status,
				e.Message,
				(httpWebResponse != null) ? httpWebResponse.StatusCode.ToString() : string.Empty,
				(httpWebResponse != null) ? httpWebResponse.Method : string.Empty,
				(httpWebResponse != null) ? httpWebResponse.ResponseUri.ToString() : string.Empty
			});
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x0007FD58 File Offset: 0x0007DF58
		public bool DoesFileExist(string fileUrl)
		{
			if (string.IsNullOrEmpty(fileUrl))
			{
				throw new ArgumentNullException("fileUrl");
			}
			WebRequest webRequest = this.CreateWebRequest(fileUrl, "HEAD");
			bool result;
			try
			{
				WebResponse response = webRequest.GetResponse();
				response.Close();
				result = true;
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse == null || httpWebResponse.StatusCode != HttpStatusCode.NotFound)
				{
					throw;
				}
				httpWebResponse.Close();
				result = false;
			}
			return result;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x0007FDD4 File Offset: 0x0007DFD4
		public void DeleteFile(string fileUrl)
		{
			if (string.IsNullOrEmpty(fileUrl))
			{
				throw new ArgumentNullException("fileUrl");
			}
			WebRequest webRequest = this.CreateWebRequest(fileUrl, "DELETE");
			WebResponse response = webRequest.GetResponse();
			response.Close();
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x0007FE10 File Offset: 0x0007E010
		public string UploadFile(string fileUrl, Stream inStream, Action heartbeat, out NameValueCollection propertyBag)
		{
			if (string.IsNullOrEmpty(fileUrl))
			{
				throw new ArgumentNullException("fileUrl");
			}
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			WebRequest webRequest = this.CreateWebRequest(fileUrl, "PUT");
			using (Stream requestStream = webRequest.GetRequestStream())
			{
				this.CopyStream(inStream, requestStream, heartbeat);
			}
			WebResponse response = webRequest.GetResponse();
			propertyBag = response.Headers;
			string result = response.ResponseUri.ToString();
			response.Close();
			return result;
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x0007FE9C File Offset: 0x0007E09C
		public void DownloadFile(string fileUrl, SharepointFileDownloadHelper writeStream)
		{
			if (string.IsNullOrEmpty(fileUrl))
			{
				throw new ArgumentNullException("fileUrl");
			}
			if (writeStream == null)
			{
				throw new ArgumentNullException("writeStream");
			}
			WebRequest webRequest = this.CreateWebRequest(fileUrl, "GET");
			using (WebResponse response = webRequest.GetResponse())
			{
				int num = (response.ContentLength >= 2147483647L) ? int.MaxValue : ((int)response.ContentLength);
				if (num == 2147483647)
				{
					num = int.MaxValue;
				}
				using (Stream responseStream = response.GetResponseStream())
				{
					writeStream(fileUrl, responseStream, num, response.ContentType);
				}
			}
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x0007FF54 File Offset: 0x0007E154
		public ICollection<SharepointFileInfo> ListFolderContents(string folderUrl)
		{
			if (string.IsNullOrEmpty(folderUrl))
			{
				throw new ArgumentNullException("folderUrl");
			}
			WebRequest webRequest = this.CreateWebRequest(folderUrl, "PROPFIND");
			webRequest.Headers.Add("Depth", "1");
			webRequest.Headers.Add(HttpRequestHeader.Translate, "f");
			SafeXmlDocument safeXmlDocument = null;
			using (WebResponse response = webRequest.GetResponse())
			{
				safeXmlDocument = new SafeXmlDocument();
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						safeXmlDocument.Load(streamReader);
					}
				}
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(safeXmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("D", "DAV:");
			List<SharepointFileInfo> list = new List<SharepointFileInfo>(100);
			int num = 0;
			bool flag = false;
			foreach (object obj in safeXmlDocument.SelectNodes("/D:multistatus/D:response", xmlNamespaceManager))
			{
				XmlNode node = (XmlNode)obj;
				if (num++ > this.maxFilesToListPerFolder)
				{
					break;
				}
				SharepointFileInfo sharepointFileInfo = SharepointFileInfo.ParseNode(node, xmlNamespaceManager);
				if (!flag && folderUrl.EndsWith(sharepointFileInfo.DisplayName))
				{
					flag = true;
				}
				else
				{
					list.Add(sharepointFileInfo);
				}
			}
			return list;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000800D8 File Offset: 0x0007E2D8
		private WebRequest CreateWebRequest(string requestUrl, string method)
		{
			WebRequest webRequest = WebRequest.Create(requestUrl);
			webRequest.Method = method;
			if (this.requestTimeout > TimeSpan.Zero)
			{
				webRequest.Timeout = (int)this.requestTimeout.TotalMilliseconds;
			}
			if (this.enableHttpDebugProxy)
			{
				webRequest.Proxy = new WebProxy("127.0.0.1", 8888);
			}
			webRequest.Credentials = this.webServiceCreds;
			return webRequest;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x00080144 File Offset: 0x0007E344
		private void CopyStream(Stream inStream, Stream outStream, Action heartbeat)
		{
			byte[] array = new byte[this.copyStreamBufferSize];
			long num = 0L;
			for (;;)
			{
				int num2 = inStream.Read(array, 0, array.Length);
				if (num2 <= 0)
				{
					break;
				}
				num += (long)num2;
				outStream.Write(array, 0, num2);
				if (heartbeat != null && num % (long)this.heartBeatInterval == 0L)
				{
					heartbeat();
				}
			}
		}

		// Token: 0x04002C4A RID: 11338
		private readonly ICredentials webServiceCreds;

		// Token: 0x04002C4B RID: 11339
		private readonly int heartBeatInterval = 100;

		// Token: 0x04002C4C RID: 11340
		private readonly int copyStreamBufferSize = 1024;

		// Token: 0x04002C4D RID: 11341
		private readonly int maxFilesToListPerFolder = 10000;

		// Token: 0x04002C4E RID: 11342
		private readonly bool enableHttpDebugProxy;

		// Token: 0x04002C4F RID: 11343
		private readonly TimeSpan requestTimeout;
	}
}
