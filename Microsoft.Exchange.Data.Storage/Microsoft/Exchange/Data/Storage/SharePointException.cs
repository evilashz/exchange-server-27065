using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200098A RID: 2442
	[Serializable]
	public class SharePointException : LocalizedException
	{
		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x06005A10 RID: 23056 RVA: 0x00175688 File Offset: 0x00173888
		// (set) Token: 0x06005A11 RID: 23057 RVA: 0x00175690 File Offset: 0x00173890
		public string RequestUrl { get; private set; }

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x06005A12 RID: 23058 RVA: 0x00175699 File Offset: 0x00173899
		// (set) Token: 0x06005A13 RID: 23059 RVA: 0x001756A1 File Offset: 0x001738A1
		public string DiagnosticInfo { get; private set; }

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x001756AC File Offset: 0x001738AC
		private static string MachineName
		{
			get
			{
				if (SharePointException.machineName == null)
				{
					SharePointException.machineName = "Not available";
					try
					{
						SharePointException.machineName = Environment.MachineName;
					}
					catch (InvalidOperationException)
					{
					}
				}
				return SharePointException.machineName;
			}
		}

		// Token: 0x06005A15 RID: 23061 RVA: 0x001756F8 File Offset: 0x001738F8
		public SharePointException(string requestUrl, LocalizedString errorMessage) : base(errorMessage, null)
		{
			this.RequestUrl = requestUrl;
			this.DiagnosticInfo = errorMessage.ToString();
			this.DiagnosticInfo = string.Format("ErrorMessage:{0}, ClientMachine:{1}", errorMessage.ToString(), SharePointException.MachineName);
		}

		// Token: 0x06005A16 RID: 23062 RVA: 0x00175749 File Offset: 0x00173949
		public SharePointException(string requestUrl, ClientRequestException e) : base(new LocalizedString(e.Message), e)
		{
			this.RequestUrl = requestUrl;
			this.DiagnosticInfo = string.Format("SharePoint ClientRequestException - ErrorMessage:{0}, ClientMachine:{1}", e.Message, SharePointException.MachineName);
		}

		// Token: 0x06005A17 RID: 23063 RVA: 0x00175780 File Offset: 0x00173980
		public SharePointException(string requestUrl, ServerException e) : base(new LocalizedString(e.Message), e)
		{
			this.RequestUrl = requestUrl;
			this.DiagnosticInfo = string.Format("SharePoint ServerException - ErrorType:{0}; ErrorMessage:{1}; ErrorCode:{2}, ClientMachine:{3}", new object[]
			{
				e.ServerErrorTypeName,
				e.Message,
				e.ServerErrorCode,
				SharePointException.MachineName
			});
		}

		// Token: 0x06005A18 RID: 23064 RVA: 0x001757E8 File Offset: 0x001739E8
		public SharePointException(string requestUrl, WebException e, bool includeResponseStream = true) : base(new LocalizedString(e.Message), e)
		{
			this.RequestUrl = requestUrl;
			HttpWebResponse httpWebResponse = e.Response as HttpWebResponse;
			this.DiagnosticInfo = string.Format("WebException - Status:{0}; Message:{1};HttpStatusCode:{2};HttpStatusDescription:{3};HttpResponseUri:{4};Server{5};ClientMachine:{6};ResponseHeaders:{7}", new object[]
			{
				e.Status,
				e.Message,
				(httpWebResponse != null) ? httpWebResponse.StatusCode.ToString() : "N/A",
				(httpWebResponse != null) ? httpWebResponse.StatusDescription : "N/A",
				(httpWebResponse != null && httpWebResponse.ResponseUri != null) ? httpWebResponse.ResponseUri.ToString() : "N/A",
				(httpWebResponse != null) ? httpWebResponse.Server : "N/A",
				SharePointException.MachineName,
				this.GetResponseHeaderString(httpWebResponse)
			}) + (includeResponseStream ? (";ResponseStream:" + this.GetResponseString(httpWebResponse)) : string.Empty);
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x001758E4 File Offset: 0x00173AE4
		protected SharePointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x001758F0 File Offset: 0x00173AF0
		private string GetResponseHeaderString(HttpWebResponse httpResponse)
		{
			if (httpResponse == null || httpResponse.Headers == null)
			{
				return "N/A";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < httpResponse.Headers.Count; i++)
			{
				string key = httpResponse.Headers.GetKey(i);
				string value = httpResponse.Headers[key];
				stringBuilder.Append("{");
				stringBuilder.Append(key);
				stringBuilder.Append(":");
				stringBuilder.Append(value);
				stringBuilder.Append("}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005A1B RID: 23067 RVA: 0x00175980 File Offset: 0x00173B80
		private string GetResponseString(HttpWebResponse httpResponse)
		{
			if (httpResponse != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				try
				{
					using (Stream responseStream = httpResponse.GetResponseStream())
					{
						Encoding encoding = Encoding.GetEncoding("utf-8");
						using (StreamReader streamReader = new StreamReader(responseStream, encoding))
						{
							char[] array = new char[256];
							for (;;)
							{
								int num = streamReader.Read(array, 0, array.Length);
								if (num <= 0)
								{
									break;
								}
								stringBuilder.Append(array, 0, num);
							}
						}
					}
				}
				catch (ProtocolViolationException)
				{
					return string.Empty;
				}
				catch (IOException)
				{
					return string.Empty;
				}
				return stringBuilder.ToString();
			}
			return "N/A";
		}

		// Token: 0x040031A7 RID: 12711
		private static volatile string machineName;
	}
}
