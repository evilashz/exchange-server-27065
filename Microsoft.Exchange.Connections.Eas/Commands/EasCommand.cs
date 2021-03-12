using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.WBXml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class EasCommand<TRequest, TResponse> where TResponse : IHaveAnHttpStatus, new()
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002AB6 File Offset: 0x00000CB6
		protected internal EasCommand(Command command, EasConnectionSettings easConnectionSettings)
		{
			this.Command = command;
			this.EasConnectionSettings = easConnectionSettings;
			this.ProtocolVersion = EasCommand<TRequest, TResponse>.asVersionToStringDict[this.EasConnectionSettings.EasProtocolVersion];
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002AE7 File Offset: 0x00000CE7
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002AEF File Offset: 0x00000CEF
		internal EasConnectionSettings EasConnectionSettings { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002AF8 File Offset: 0x00000CF8
		internal virtual bool UseSsl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002AFB File Offset: 0x00000CFB
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002B03 File Offset: 0x00000D03
		internal string ProtocolVersion { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002B0C File Offset: 0x00000D0C
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002B14 File Offset: 0x00000D14
		internal Command Command { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002B1D File Offset: 0x00000D1D
		internal string CommandName
		{
			get
			{
				return this.Command.ToString();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002B2F File Offset: 0x00000D2F
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002B37 File Offset: 0x00000D37
		internal string UriString { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002B40 File Offset: 0x00000D40
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002B48 File Offset: 0x00000D48
		internal HttpWebRequest HttpWebRequest { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002B51 File Offset: 0x00000D51
		protected virtual string RequestMethodName
		{
			get
			{
				return "POST";
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002B60 File Offset: 0x00000D60
		protected List<int> ExpectedHttpStatusCodes { get; set; }

		// Token: 0x0600007F RID: 127 RVA: 0x00002B6C File Offset: 0x00000D6C
		internal virtual TResponse Execute(TRequest easRequest)
		{
			TResponse result;
			try
			{
				HttpWebRequest httpWebRequest = this.CreateWebRequest(this.GetUriString());
				this.AddWebRequestHeaders(httpWebRequest);
				this.LogInfoHeaders(httpWebRequest.Headers);
				this.AddWebRequestBody(httpWebRequest, easRequest);
				result = this.SendWebRequest(httpWebRequest, easRequest);
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					throw new EasWebException(httpWebResponse.StatusCode.ToString(), ex);
				}
				throw new EasWebException(ex.Status.ToString(), ex);
			}
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002BFC File Offset: 0x00000DFC
		protected static XmlDocument GetRequestXmlDocument(TRequest easRequest)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(TRequest));
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8)
				{
					Formatting = Formatting.Indented
				})
				{
					xmlSerializer.Serialize(xmlTextWriter, easRequest);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					xmlDocument.Load(memoryStream);
				}
			}
			return xmlDocument;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002C90 File Offset: 0x00000E90
		protected virtual string GetUriString()
		{
			string local = this.EasConnectionSettings.EasEndpointSettings.Local;
			string domain = this.EasConnectionSettings.EasEndpointSettings.Domain;
			return string.Format("{0}//{1}/Microsoft-Server-ActiveSync?Cmd={2}&User={3}&DeviceId={4}&DeviceType={5}{6}", new object[]
			{
				this.UseSsl ? "https:" : "http:",
				domain,
				this.CommandName,
				local,
				this.EasConnectionSettings.DeviceId,
				this.EasConnectionSettings.DeviceType,
				string.Empty
			});
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002D20 File Offset: 0x00000F20
		protected virtual void AddWebRequestHeaders(HttpWebRequest request)
		{
			request.Headers.Add("MS-ASProtocolVersion", this.ProtocolVersion);
			if (this.EasConnectionSettings.RequestCompression)
			{
				request.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
			}
			if (this.EasConnectionSettings.AcceptMultipart)
			{
				request.Headers.Add("MS-ASAcceptMultiPart: T");
			}
			if (!string.IsNullOrWhiteSpace(this.EasConnectionSettings.ClientLanguage))
			{
				request.Headers.Add("Accept-Language: " + this.EasConnectionSettings.ClientLanguage);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002DA8 File Offset: 0x00000FA8
		protected virtual void AddWebRequestBody(HttpWebRequest webRequest, TRequest easRequest)
		{
			XmlDocument requestXmlDocument = EasCommand<TRequest, TResponse>.GetRequestXmlDocument(easRequest);
			this.LogInfoXml(requestXmlDocument);
			using (Stream requestStream = webRequest.GetRequestStream())
			{
				new WBXmlWriter(requestStream).WriteXmlDocument(requestXmlDocument);
			}
			webRequest.ContentType = "application/vnd.ms-sync.wbxml";
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002E00 File Offset: 0x00001000
		protected virtual TResponse ExtractResponse(HttpWebResponse webResponse)
		{
			HttpStatus statusCode;
			XmlDocument xmlDocument;
			using (Stream responseStream = webResponse.GetResponseStream())
			{
				statusCode = (HttpStatus)webResponse.StatusCode;
				if (responseStream == null || webResponse.ContentLength == 0L)
				{
					TResponse result = (default(TResponse) == null) ? Activator.CreateInstance<TResponse>() : default(TResponse);
					result.HttpStatus = statusCode;
					return result;
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					responseStream.CopyTo(memoryStream);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					using (WBXmlReader wbxmlReader = new WBXmlReader(memoryStream))
					{
						xmlDocument = wbxmlReader.ReadXmlDocument();
					}
				}
			}
			this.LogInfoXml(xmlDocument);
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				NewLineChars = Environment.NewLine,
				NewLineHandling = NewLineHandling.Entitize
			};
			TResponse result2;
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream2, settings))
				{
					xmlDocument.WriteTo(xmlWriter);
					xmlWriter.Flush();
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(TResponse));
					memoryStream2.Seek(0L, SeekOrigin.Begin);
					TResponse tresponse = (TResponse)((object)xmlSerializer.Deserialize(memoryStream2));
					tresponse.HttpStatus = statusCode;
					result2 = tresponse;
				}
			}
			return result2;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected virtual TResponse SendWebRequest(HttpWebRequest webRequest, TRequest easRequest)
		{
			TResponse result;
			using (HttpWebResponse httpResponse = webRequest.GetHttpResponse(this.ExpectedHttpStatusCodes))
			{
				int statusCode = (int)httpResponse.StatusCode;
				this.EasConnectionSettings.Log.Info("{0} {1}", new object[]
				{
					statusCode,
					httpResponse.StatusDescription
				});
				int num = statusCode;
				if (num <= 301)
				{
					if (num == 200)
					{
						goto IL_D7;
					}
					if (num == 301)
					{
						return this.FollowRedirect(easRequest, httpResponse.Headers["Location"], statusCode);
					}
				}
				else
				{
					if (num == 451)
					{
						return this.FollowRedirect(easRequest, httpResponse.Headers["X-MS-Location"], statusCode);
					}
					if (num == 503)
					{
						this.ThrowRetryAfterException(httpResponse);
						goto IL_D7;
					}
				}
				string msg = string.Format("HTTP status code = '{0}'", httpResponse.StatusCode);
				throw new EasUnexpectedHttpStatusException(msg);
				IL_D7:
				result = this.ExtractResponse(httpResponse);
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000030AC File Offset: 0x000012AC
		protected void ThrowRetryAfterException(HttpWebResponse webResponse)
		{
			string s = webResponse.Headers["Retry-After"];
			int num;
			if (!int.TryParse(s, out num))
			{
				num = 3600;
			}
			throw new EasRetryAfterException(TimeSpan.FromSeconds((double)num), webResponse.StatusDescription);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000030EC File Offset: 0x000012EC
		protected HttpWebRequest CreateWebRequest(string uriString)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uriString);
			httpWebRequest = this.ConditionWebRequest(httpWebRequest);
			this.EasConnectionSettings.Log.Info("{0} {1}", new object[]
			{
				httpWebRequest.Method,
				httpWebRequest.RequestUri
			});
			return httpWebRequest;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003140 File Offset: 0x00001340
		protected virtual HttpWebRequest ConditionWebRequest(HttpWebRequest webRequest)
		{
			webRequest.Method = this.RequestMethodName;
			webRequest.Credentials = this.EasConnectionSettings.EasEndpointSettings.NetworkCredential;
			webRequest.UserAgent = this.EasConnectionSettings.UserAgent;
			webRequest.AllowAutoRedirect = false;
			webRequest.PreAuthenticate = true;
			return webRequest;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003190 File Offset: 0x00001390
		protected void InitializeExpectedHttpStatusCodes(Type statusType)
		{
			Array values = Enum.GetValues(statusType);
			this.ExpectedHttpStatusCodes = values.Cast<int>().ToList<int>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000031B8 File Offset: 0x000013B8
		protected void LogInfoXml(XmlDocument xmlDocument)
		{
			if (this.EasConnectionSettings.Log.IsEnabled(LogLevel.LogInfo))
			{
				using (StringWriter stringWriter = new StringWriter())
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						xmlTextWriter.Formatting = Formatting.Indented;
						xmlTextWriter.Indentation = 1;
						xmlDocument.WriteTo(xmlTextWriter);
						xmlTextWriter.Flush();
						string text = stringWriter.GetStringBuilder().ToString();
						this.EasConnectionSettings.Log.Info("{0}", new object[]
						{
							text
						});
					}
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003264 File Offset: 0x00001464
		protected void LogInfoHeaders(WebHeaderCollection headers)
		{
			if (this.EasConnectionSettings.Log.IsEnabled(LogLevel.LogInfo))
			{
				foreach (string text in headers.AllKeys)
				{
					this.EasConnectionSettings.Log.Info("{0}: {1}", new object[]
					{
						text,
						string.Join("|", headers.GetValues(text))
					});
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000032D4 File Offset: 0x000014D4
		private TResponse FollowRedirect(TRequest easRequest, string newLocationUrl, int statusCode)
		{
			if (string.IsNullOrWhiteSpace(newLocationUrl))
			{
				throw new EasMissingOrBadUrlOnRedirectException();
			}
			EasEndpointSettings easEndpointSettings = this.EasConnectionSettings.EasEndpointSettings;
			Uri uri = new Uri(newLocationUrl);
			easEndpointSettings.MostRecentDomain = uri.Host;
			if (statusCode == 301 || (statusCode != 307 && statusCode == 451))
			{
				easEndpointSettings.MostRecentEndpoint.ExplicitExpiration = new DateTime?(DateTime.MaxValue);
			}
			return this.Execute(easRequest);
		}

		// Token: 0x0400006F RID: 111
		protected const string UriStringFormat = "{0}//{1}/Microsoft-Server-ActiveSync?Cmd={2}&User={3}&DeviceId={4}&DeviceType={5}{6}";

		// Token: 0x04000070 RID: 112
		protected const string HeaderVersionString = "MS-ASProtocolVersion";

		// Token: 0x04000071 RID: 113
		protected const string XmlContentType = "text/xml";

		// Token: 0x04000072 RID: 114
		protected const string WBXmlContentType = "application/vnd.ms-sync.wbxml";

		// Token: 0x04000073 RID: 115
		private static Dictionary<EasProtocolVersion, string> asVersionToStringDict = new Dictionary<EasProtocolVersion, string>
		{
			{
				EasProtocolVersion.Version140,
				"14.0"
			}
		};
	}
}
