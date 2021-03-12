using System;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200007A RID: 122
	internal class AcsAuthRequest : HttpSessionConfig, IDisposable
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0000E194 File Offset: 0x0000C394
		public AcsAuthRequest(Uri uri, string userName, SecureString password, string scope)
		{
			ArgumentValidator.ThrowIfNull("uri", uri);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("userName", userName);
			ArgumentValidator.ThrowIfNull("password", password);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("scope", scope);
			this.Uri = uri;
			base.Method = "POST";
			base.ContentType = "application/x-www-form-urlencoded";
			base.AllowAutoRedirect = false;
			base.KeepAlive = false;
			base.Pipelined = false;
			base.Headers = new WebHeaderCollection();
			base.RequestStream = new MemoryStream(this.GetContent(new string[]
			{
				"wrap_name",
				userName,
				"wrap_password",
				password.AsUnsecureString(),
				"wrap_scope",
				scope
			}));
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000E254 File Offset: 0x0000C454
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0000E25C File Offset: 0x0000C45C
		public Uri Uri { get; private set; }

		// Token: 0x06000449 RID: 1097 RVA: 0x0000E265 File Offset: 0x0000C465
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000E274 File Offset: 0x0000C474
		public string ToTraceString()
		{
			if (this.cachedToTraceString == null)
			{
				this.cachedToTraceString = string.Format("{{Target-Uri:{0}; Method:{1}; Content-Type:{2}; Headers:[{3}]; }}", new object[]
				{
					this.Uri,
					base.Method,
					base.ContentType,
					base.Headers.ToTraceString(new string[]
					{
						HttpRequestHeader.Authorization.ToString()
					})
				});
			}
			return this.cachedToTraceString;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000E2E7 File Offset: 0x0000C4E7
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

		// Token: 0x0600044C RID: 1100 RVA: 0x0000E318 File Offset: 0x0000C518
		private byte[] GetContent(params string[] properties)
		{
			string value = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < properties.Length; i += 2)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(HttpUtility.UrlEncode(properties[i]));
				stringBuilder.Append("=");
				stringBuilder.Append(HttpUtility.UrlEncode(properties[i + 1]));
				value = "&";
			}
			return Encoding.ASCII.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x04000201 RID: 513
		private const string AuthContentType = "application/x-www-form-urlencoded";

		// Token: 0x04000202 RID: 514
		private const string WrapPropertyName = "wrap_name";

		// Token: 0x04000203 RID: 515
		private const string WrapPasswordPropertyName = "wrap_password";

		// Token: 0x04000204 RID: 516
		private const string WrapScopePropertyName = "wrap_scope";

		// Token: 0x04000205 RID: 517
		private bool isDisposed;

		// Token: 0x04000206 RID: 518
		private string cachedToTraceString;
	}
}
