using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C13 RID: 3091
	internal class EsoRequest : HttpSessionConfig, IDisposable
	{
		// Token: 0x060043B2 RID: 17330 RVA: 0x000B5E2B File Offset: 0x000B402B
		static EsoRequest()
		{
			CertificateValidationManager.RegisterCallback("ExchangeServiceToOwa", new RemoteCertificateValidationCallback(EsoRequest.ValidateRemoteCertificate));
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x000B5E44 File Offset: 0x000B4044
		public EsoRequest(string action, string userAgentBinder, string payload)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("action", action);
			this.uri = new Uri("https://127.0.0.1:444/owa/service.svc?action=" + action);
			base.Method = "POST";
			base.UserAgent = "EsoRequest/" + userAgentBinder;
			base.ContentType = "application/json; charset=UTF-8";
			base.UnsafeAuthenticatedConnectionSharing = true;
			base.PreAuthenticate = true;
			base.AllowAutoRedirect = false;
			base.Credentials = CredentialCache.DefaultNetworkCredentials;
			base.Headers = new WebHeaderCollection();
			base.Headers.Add("Action", action);
			base.Headers.Add("ActionId", EsoRequest.GetNextID().ToString());
			CertificateValidationManager.SetComponentId(base.Headers, "ExchangeServiceToOwa");
			base.RequestStream = new MemoryStream(string.IsNullOrEmpty(payload) ? new byte[0] : Encoding.UTF8.GetBytes(payload));
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000B5F2E File Offset: 0x000B412E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000B5F3D File Offset: 0x000B413D
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

		// Token: 0x060043B6 RID: 17334 RVA: 0x000B5F6C File Offset: 0x000B416C
		public static bool IsEsoRequest(HttpRequest request)
		{
			string value = request.Headers["Action"];
			return !string.IsNullOrWhiteSpace(value) && EsoRequest.WellKnownActions.All.Contains(value);
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000B5FA0 File Offset: 0x000B41A0
		public ICancelableAsyncResult BeginSend()
		{
			HttpClient httpClient = new HttpClient();
			return httpClient.BeginDownload(this.uri, this, null, httpClient);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x000B5FC4 File Offset: 0x000B41C4
		public virtual DownloadResult EndSend(ICancelableAsyncResult asyncResult)
		{
			DownloadResult result;
			using (HttpClient httpClient = (HttpClient)asyncResult.AsyncState)
			{
				result = httpClient.EndDownload(asyncResult);
			}
			return result;
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x000B6004 File Offset: 0x000B4204
		private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return true;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x000B6007 File Offset: 0x000B4207
		private static long GetNextID()
		{
			return Interlocked.Increment(ref EsoRequest.idCounter);
		}

		// Token: 0x040039A3 RID: 14755
		private const string ComponentId = "ExchangeServiceToOwa";

		// Token: 0x040039A4 RID: 14756
		private const string LocalhostBackendUri = "https://127.0.0.1:444/owa/service.svc?action=";

		// Token: 0x040039A5 RID: 14757
		private const string EsoContentType = "application/json; charset=UTF-8";

		// Token: 0x040039A6 RID: 14758
		private const string UserAgentPrefix = "EsoRequest/";

		// Token: 0x040039A7 RID: 14759
		private const string ActionHeader = "Action";

		// Token: 0x040039A8 RID: 14760
		private const string ActionIdHeader = "ActionId";

		// Token: 0x040039A9 RID: 14761
		private static long idCounter;

		// Token: 0x040039AA RID: 14762
		private Uri uri;

		// Token: 0x040039AB RID: 14763
		private bool isDisposed;

		// Token: 0x02000C14 RID: 3092
		internal class WellKnownActions
		{
			// Token: 0x060043BB RID: 17339 RVA: 0x000B6014 File Offset: 0x000B4214
			static WellKnownActions()
			{
				string[] list = new string[]
				{
					"PublishO365Notification"
				};
				EsoRequest.WellKnownActions.All = new ReadOnlyCollection<string>(list);
			}

			// Token: 0x040039AC RID: 14764
			public const string PublishO365Notification = "PublishO365Notification";

			// Token: 0x040039AD RID: 14765
			public static readonly ReadOnlyCollection<string> All;
		}
	}
}
