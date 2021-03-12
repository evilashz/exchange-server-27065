using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A7 RID: 167
	internal class GcmRequest : HttpSessionConfig, IDisposable
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00013044 File Offset: 0x00011244
		public GcmRequest(GcmNotification notification)
		{
			base.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
			base.Method = "POST";
			base.Pipelined = false;
			base.AllowAutoRedirect = false;
			base.KeepAlive = false;
			base.Headers = new WebHeaderCollection();
			base.Expect100Continue = new bool?(false);
			base.RequestStream = new MemoryStream(Encoding.UTF8.GetBytes(notification.ToGcmFormat()));
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000130B4 File Offset: 0x000112B4
		public void SetSenderAuthToken(string authToken)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("authToken", authToken);
			base.Headers[HttpRequestHeader.Authorization] = string.Format("key={0}", authToken);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000130D9 File Offset: 0x000112D9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000130E8 File Offset: 0x000112E8
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing && base.RequestStream != null)
				{
					base.RequestStream.Close();
				}
				base.RequestStream = null;
				this.isDisposed = true;
			}
		}

		// Token: 0x040002D2 RID: 722
		private const string GcmContentType = "application/x-www-form-urlencoded;charset=UTF-8";

		// Token: 0x040002D3 RID: 723
		private const string AuthorizationTemplate = "key={0}";

		// Token: 0x040002D4 RID: 724
		private bool isDisposed;
	}
}
