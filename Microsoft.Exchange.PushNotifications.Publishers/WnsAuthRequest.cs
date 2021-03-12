using System;
using System.IO;
using System.Security;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D7 RID: 215
	internal class WnsAuthRequest : HttpSessionConfig, IDisposable
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x00015ED8 File Offset: 0x000140D8
		public WnsAuthRequest(Uri uri, string appSid, SecureString appSecretCode)
		{
			ArgumentValidator.ThrowIfNull("uri", uri);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("appSid", appSid);
			ArgumentValidator.ThrowIfNull("appSecretCode", appSecretCode);
			this.Uri = uri;
			base.Method = "POST";
			base.ContentType = "application/x-www-form-urlencoded";
			base.AllowAutoRedirect = false;
			base.KeepAlive = false;
			base.Pipelined = false;
			string s = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com", HttpUtility.UrlEncode(appSid), HttpUtility.UrlEncode(appSecretCode.AsUnsecureString()));
			base.RequestStream = new MemoryStream(Encoding.Default.GetBytes(s));
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00015F70 File Offset: 0x00014170
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00015F78 File Offset: 0x00014178
		public Uri Uri { get; private set; }

		// Token: 0x060006FF RID: 1791 RVA: 0x00015F81 File Offset: 0x00014181
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00015F90 File Offset: 0x00014190
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

		// Token: 0x04000383 RID: 899
		private const string AuthContentType = "application/x-www-form-urlencoded";

		// Token: 0x04000384 RID: 900
		private const string PayloadTemplate = "grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com";

		// Token: 0x04000385 RID: 901
		private bool isDisposed;
	}
}
