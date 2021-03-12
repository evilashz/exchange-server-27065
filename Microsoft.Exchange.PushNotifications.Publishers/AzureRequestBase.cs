using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000053 RID: 83
	internal abstract class AzureRequestBase : HttpSessionConfig, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0000AED0 File Offset: 0x000090D0
		protected AzureRequestBase(string contentType, string httpMethod, AcsAccessToken acsKey, string resourceUri, string additionalParameters = "") : this(contentType, httpMethod, resourceUri, additionalParameters)
		{
			ArgumentValidator.ThrowIfNull("acsKey", acsKey);
			this.Authorization = acsKey.ToAzureAuthorizationString();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000AEF5 File Offset: 0x000090F5
		protected AzureRequestBase(string contentType, string httpMethod, AzureSasToken sasToken, string resourceUri, string additionalParameters = "") : this(contentType, httpMethod, resourceUri, additionalParameters)
		{
			ArgumentValidator.ThrowIfNull("sasToken", sasToken);
			this.sasToken = sasToken;
			this.Authorization = this.sasToken.ToAzureAuthorizationString();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000AF3C File Offset: 0x0000913C
		private AzureRequestBase(string contentType, string httpMethod, string resourceUri, string additionalParameters = "")
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("contentType", contentType);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("httpMethod", httpMethod);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("resourceUri", resourceUri);
			ArgumentValidator.ThrowIfInvalidValue<string>("resourceUri", resourceUri, (string X) => Uri.IsWellFormedUriString(resourceUri, UriKind.Absolute));
			base.Pipelined = false;
			base.AllowAutoRedirect = false;
			base.KeepAlive = false;
			base.ReadWebExceptionResponseStream = true;
			base.Headers = new WebHeaderCollection();
			base.Headers["x-ms-version"] = "2013-08";
			base.ContentType = contentType;
			base.Method = httpMethod;
			string arg = PushNotificationsCrimsonEvents.AzureNotificationResponse.IsEnabled(PushNotificationsCrimsonEvent.Provider) ? "test" : string.Empty;
			this.Uri = new Uri(string.Format("{0}/?{1}&api-version=2013-08{2}", resourceUri, arg, additionalParameters));
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B037 File Offset: 0x00009237
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000B03F File Offset: 0x0000923F
		public Uri Uri { get; protected set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000B048 File Offset: 0x00009248
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000B057 File Offset: 0x00009257
		public string Authorization
		{
			get
			{
				return base.Headers[HttpRequestHeader.Authorization];
			}
			protected set
			{
				base.Headers[HttpRequestHeader.Authorization] = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B067 File Offset: 0x00009267
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000B06F File Offset: 0x0000926F
		public string RequestBody
		{
			get
			{
				return this.requestBody;
			}
			protected set
			{
				this.requestBody = value;
				base.RequestStream = new MemoryStream(Encoding.UTF8.GetBytes(value));
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B090 File Offset: 0x00009290
		public string ToTraceString()
		{
			if (this.cachedToTraceString == null)
			{
				this.cachedToTraceString = string.Format("{{Request:'{0}'; Target-Uri:{1}; Method:{2}; Content-Type:{3}; Headers:[{4}]; SasToken:{5}; Body-Content:{6}}}", new object[]
				{
					base.GetType().Name,
					this.Uri,
					base.Method,
					base.ContentType,
					base.Headers.ToTraceString(new string[]
					{
						HttpRequestHeader.Authorization.ToString()
					}),
					this.sasToken,
					this.RequestBody
				});
			}
			return this.cachedToTraceString;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B123 File Offset: 0x00009323
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AzureRequestBase>(this);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B12B File Offset: 0x0000932B
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000B140 File Offset: 0x00009340
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000B150 File Offset: 0x00009350
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					if (base.RequestStream != null)
					{
						base.RequestStream.Close();
						base.RequestStream = null;
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000151 RID: 337
		public const string HeaderVersionName = "x-ms-version";

		// Token: 0x04000152 RID: 338
		public const string HeaderVersion = "2013-08";

		// Token: 0x04000153 RID: 339
		private readonly AzureSasToken sasToken;

		// Token: 0x04000154 RID: 340
		private DisposeTracker disposeTracker;

		// Token: 0x04000155 RID: 341
		private bool isDisposed;

		// Token: 0x04000156 RID: 342
		private string requestBody;

		// Token: 0x04000157 RID: 343
		private string cachedToTraceString;
	}
}
