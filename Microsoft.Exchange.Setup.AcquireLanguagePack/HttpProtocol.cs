using System;
using System.IO;
using System.Net;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200001C RID: 28
	internal sealed class HttpProtocol : IDisposable
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003C40 File Offset: 0x00001E40
		public HttpProtocol()
		{
			ServicePointManager.DefaultConnectionLimit = int.MaxValue;
			this.disposed = false;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003C5C File Offset: 0x00001E5C
		public static void QueryFileNameSize(ref DownloadFileInfo download)
		{
			Logger.LoggerMessage("Attempting to connect to the remote server and getting the filesize...");
			WebResponse webResponse = null;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(download.UriLink);
				httpWebRequest.KeepAlive = false;
				httpWebRequest.Timeout = 60000;
				webResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				download.FilePath = HttpProtocol.GetFilenameFromUri(webResponse.ResponseUri.AbsoluteUri);
				download.FileSize = webResponse.ContentLength;
			}
			finally
			{
				if (webResponse != null)
				{
					webResponse.Close();
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003CE8 File Offset: 0x00001EE8
		private static string GetFilenameFromUri(string absoluteUri)
		{
			if (string.IsNullOrEmpty(absoluteUri))
			{
				return string.Empty;
			}
			int num = absoluteUri.LastIndexOf("/");
			if (num == -1)
			{
				return string.Empty;
			}
			int num2 = num + 1;
			if (num2 >= absoluteUri.Length)
			{
				return string.Empty;
			}
			return absoluteUri.Substring(num2);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003D34 File Offset: 0x00001F34
		public Stream GetStream(int startPosition, int endPosition, Uri downloadUrl, int numberOfThreads)
		{
			this.request = (HttpWebRequest)WebRequest.Create(downloadUrl);
			this.request.Timeout = 60000;
			this.request.KeepAlive = false;
			if (numberOfThreads > 1)
			{
				this.request.AddRange(startPosition, endPosition);
				this.response = (HttpWebResponse)this.request.GetResponse();
				if (this.response.StatusCode == HttpStatusCode.PartialContent)
				{
					return this.response.GetResponseStream();
				}
				this.request.Abort();
				return null;
			}
			else
			{
				this.response = (HttpWebResponse)this.request.GetResponse();
				if (this.response.StatusCode == HttpStatusCode.OK)
				{
					return this.response.GetResponseStream();
				}
				this.request.Abort();
				return null;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003E01 File Offset: 0x00002001
		public void CancelDownload()
		{
			this.request.Abort();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003E0E File Offset: 0x0000200E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003E1D File Offset: 0x0000201D
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.response != null)
				{
					this.response.Close();
				}
				this.response = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000049 RID: 73
		private const int TimeoutValue = 60000;

		// Token: 0x0400004A RID: 74
		private HttpWebResponse response;

		// Token: 0x0400004B RID: 75
		private HttpWebRequest request;

		// Token: 0x0400004C RID: 76
		private bool disposed;
	}
}
