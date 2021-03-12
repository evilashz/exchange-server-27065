using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MapiHttpClient;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ClientSessionContext
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003DAC File Offset: 0x00001FAC
		public ClientSessionContext(MapiHttpBindingInfo bindingInfo, string vdirPath, IntPtr contextHandle)
		{
			Util.ThrowOnNullArgument(bindingInfo, "bindingInfo");
			Util.ThrowOnNullArgument(vdirPath, "vdirPath");
			if (contextHandle == IntPtr.Zero)
			{
				throw new ArgumentException("contextHandle cannot be IntPtr.Zero");
			}
			this.bindingInfo = bindingInfo;
			this.contextHandle = contextHandle;
			this.clientInfo = string.Format("{0}:{1}", ClientSessionContext.clientInfoGroupId, this.contextHandle.ToInt64().ToString());
			string text = vdirPath;
			this.requestPath = this.bindingInfo.BuildRequestPath(ref text);
			this.vdirPath = text;
			if (this.bindingInfo.AdditionalHttpHeaders != null && this.bindingInfo.AdditionalHttpHeaders.Count > 0)
			{
				this.additionalHttpHeaders = this.bindingInfo.AdditionalHttpHeaders;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003EFC File Offset: 0x000020FC
		public IntPtr ContextHandle
		{
			get
			{
				return this.contextHandle;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003F04 File Offset: 0x00002104
		public string VdirPath
		{
			get
			{
				return this.vdirPath;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003F0C File Offset: 0x0000210C
		public string RequestPath
		{
			get
			{
				return this.requestPath;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003F14 File Offset: 0x00002114
		public bool NeedsRefresh
		{
			get
			{
				return ExDateTime.UtcNow >= this.nextRefresh;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003F26 File Offset: 0x00002126
		public TimeSpan? DesiredExpiration
		{
			get
			{
				return this.bindingInfo.Expiration;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003F33 File Offset: 0x00002133
		public TimeSpan? ActualExpiration
		{
			get
			{
				return this.actualExpiration;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003F3B File Offset: 0x0000213B
		public ExDateTime? LastCall
		{
			get
			{
				return this.lastCall;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003F43 File Offset: 0x00002143
		public TimeSpan? LastElapsedTime
		{
			get
			{
				return this.lastElapsedTime;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003F4B File Offset: 0x0000214B
		public ExDateTime? Expires
		{
			get
			{
				return this.expires;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003F53 File Offset: 0x00002153
		public string RequestGroupId
		{
			get
			{
				return this.requestGroupId;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003F5B File Offset: 0x0000215B
		public Dictionary<string, string> Cookies
		{
			get
			{
				return new Dictionary<string, string>(this.cookieValues);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003F68 File Offset: 0x00002168
		public WebHeaderCollection ResponseHeaders
		{
			get
			{
				return new WebHeaderCollection
				{
					this.responseHeaders
				};
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003F88 File Offset: 0x00002188
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003F90 File Offset: 0x00002190
		public HttpStatusCode? LastResponseStatusCode { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003F99 File Offset: 0x00002199
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003FA1 File Offset: 0x000021A1
		public string LastResponseStatusDescription { get; private set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00003FAC File Offset: 0x000021AC
		public void SetAdditionalRequestHeaders(HttpWebRequest request)
		{
			if (this.additionalHttpHeaders != null && this.additionalHttpHeaders.Count > 0)
			{
				foreach (string name in this.additionalHttpHeaders.AllKeys)
				{
					request.Headers.Remove(name);
				}
				request.Headers.Add(this.additionalHttpHeaders);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000400C File Offset: 0x0000220C
		internal HttpWebRequest CreateRequest(out string requestId)
		{
			requestId = string.Empty;
			HttpWebRequest httpWebRequest = WebRequest.CreateHttp(new Uri(this.requestPath));
			httpWebRequest.ConnectionGroupName = this.connectionGroupName;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/octet-stream";
			httpWebRequest.UserAgent = "MapiHttpClient";
			httpWebRequest.KeepAlive = true;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Expect = null;
			if (this.bindingInfo.Credentials != null)
			{
				httpWebRequest.Credentials = this.bindingInfo.Credentials;
			}
			else
			{
				httpWebRequest.UseDefaultCredentials = true;
			}
			if (this.bindingInfo.IgnoreCertificateErrors)
			{
				httpWebRequest.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ClientSessionContext.CertificateValidationCallback);
			}
			ServicePoint servicePoint = httpWebRequest.ServicePoint;
			servicePoint.UseNagleAlgorithm = false;
			servicePoint.ReceiveBufferSize = 262144;
			servicePoint.Expect100Continue = false;
			servicePoint.ConnectionLimit = 65000;
			httpWebRequest.CookieContainer = new CookieContainer();
			lock (this.updateLock)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.cookieValues)
				{
					if (!string.IsNullOrWhiteSpace(keyValuePair.Value))
					{
						string value = ClientSessionContext.EscapeCookieIfNeeded(keyValuePair.Value);
						httpWebRequest.CookieContainer.Add(new Cookie(keyValuePair.Key, value, this.vdirPath, this.bindingInfo.ServerFqdn));
					}
				}
			}
			int num = Interlocked.Increment(ref this.nextRequestNumber);
			requestId = string.Format("{0}:{1}", this.requestGroupId, num);
			httpWebRequest.Headers.Add("X-RequestId", requestId);
			httpWebRequest.Headers.Add("X-ClientInfo", this.clientInfo);
			this.nextRefresh = ExDateTime.UtcNow + Constants.SessionContextRefreshInterval;
			this.actualExpiration = null;
			this.lastCall = new ExDateTime?(ExDateTime.UtcNow);
			return httpWebRequest;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004220 File Offset: 0x00002420
		internal void Update(HttpWebResponse response)
		{
			lock (this.updateLock)
			{
				this.LastResponseStatusCode = new HttpStatusCode?(response.StatusCode);
				this.LastResponseStatusDescription = response.StatusDescription;
				StringBuilder stringBuilder = null;
				string arg = string.Empty;
				if (ExTraceGlobals.ClientSessionContextTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					stringBuilder = new StringBuilder();
				}
				if (response.Cookies != null)
				{
					foreach (object obj2 in response.Cookies)
					{
						Cookie cookie = (Cookie)obj2;
						if (!string.IsNullOrWhiteSpace(cookie.Value))
						{
							this.cookieValues[cookie.Name] = cookie.Value;
							if (stringBuilder != null)
							{
								stringBuilder.Append(string.Format("{0}{1}={2}", arg, cookie.Name, cookie.Value));
								arg = ", ";
							}
						}
						else
						{
							this.cookieValues.Remove(cookie.Name);
						}
					}
				}
				if (response.Headers != null)
				{
					this.responseHeaders.Clear();
					this.responseHeaders.Add(response.Headers);
				}
				if (stringBuilder != null)
				{
					ExTraceGlobals.ClientSessionContextTracer.TraceInformation<IntPtr, string, string>(44960, 0L, "ClientSessionContext: Update cookies; ContextHandle={0}, RequestGroupId={1}, Cookies=[{2}]", this.contextHandle, this.requestGroupId, stringBuilder.ToString());
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000043B0 File Offset: 0x000025B0
		internal void UpdateRefresh(TimeSpan expiration)
		{
			this.actualExpiration = new TimeSpan?(expiration);
			this.nextRefresh = ExDateTime.UtcNow.AddMilliseconds(expiration.TotalMilliseconds / 2.0);
			this.expires = this.lastCall + expiration;
			ExTraceGlobals.ClientSessionContextTracer.TraceInformation<IntPtr, string, ExDateTime?>(61344, 0L, "ClientSessionContext: Update expiration information; ContextHandle={0}, RequestGroupId={1}, Expires={2}", this.contextHandle, this.requestGroupId, this.expires);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000444B File Offset: 0x0000264B
		internal void UpdateElapsedTime(TimeSpan? elapsedTime)
		{
			this.lastElapsedTime = elapsedTime;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004454 File Offset: 0x00002654
		internal void Reset()
		{
			ServicePoint servicePoint = ServicePointManager.FindServicePoint(new Uri(this.requestPath));
			if (servicePoint != null)
			{
				servicePoint.CloseConnectionGroup(this.connectionGroupName);
			}
			string text = Guid.NewGuid().ToString("D");
			ExTraceGlobals.ClientSessionContextTracer.TraceInformation(33184, 0L, "ClientSessionContext: Reset connection group; ContextHandle={0}, RequestGroupId={1}, ConnectionGroupName={2}, NewConnectionGroupName={3}", new object[]
			{
				this.contextHandle,
				this.requestGroupId,
				this.connectionGroupName,
				text
			});
			this.connectionGroupName = text;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000044E0 File Offset: 0x000026E0
		private static string EscapeCookieIfNeeded(string cookieValue)
		{
			if (!string.IsNullOrWhiteSpace(cookieValue) && cookieValue.Length >= 2 && cookieValue[0] != '"' && cookieValue[cookieValue.Length - 1] != '"' && cookieValue.IndexOfAny(ClientSessionContext.cookieCharsRequiringQuoteWrap) != -1)
			{
				cookieValue = string.Format("\"{0}\"", cookieValue);
			}
			return cookieValue;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004537 File Offset: 0x00002737
		private static bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x04000018 RID: 24
		private static readonly char[] cookieCharsRequiringQuoteWrap = new char[]
		{
			';',
			','
		};

		// Token: 0x04000019 RID: 25
		private static readonly string clientInfoGroupId = Guid.NewGuid().ToString("D");

		// Token: 0x0400001A RID: 26
		private readonly MapiHttpBindingInfo bindingInfo;

		// Token: 0x0400001B RID: 27
		private readonly IntPtr contextHandle;

		// Token: 0x0400001C RID: 28
		private readonly string vdirPath;

		// Token: 0x0400001D RID: 29
		private readonly string requestPath;

		// Token: 0x0400001E RID: 30
		private readonly object updateLock = new object();

		// Token: 0x0400001F RID: 31
		private readonly Dictionary<string, string> cookieValues = new Dictionary<string, string>();

		// Token: 0x04000020 RID: 32
		private readonly WebHeaderCollection responseHeaders = new WebHeaderCollection();

		// Token: 0x04000021 RID: 33
		private readonly string requestGroupId = Guid.NewGuid().ToString("D");

		// Token: 0x04000022 RID: 34
		private readonly WebHeaderCollection additionalHttpHeaders;

		// Token: 0x04000023 RID: 35
		private readonly string clientInfo;

		// Token: 0x04000024 RID: 36
		private string connectionGroupName = Guid.NewGuid().ToString("D");

		// Token: 0x04000025 RID: 37
		private int nextRequestNumber;

		// Token: 0x04000026 RID: 38
		private ExDateTime nextRefresh = ExDateTime.MaxValue;

		// Token: 0x04000027 RID: 39
		private TimeSpan? actualExpiration = null;

		// Token: 0x04000028 RID: 40
		private ExDateTime? lastCall = null;

		// Token: 0x04000029 RID: 41
		private TimeSpan? lastElapsedTime = null;

		// Token: 0x0400002A RID: 42
		private ExDateTime? expires = null;
	}
}
