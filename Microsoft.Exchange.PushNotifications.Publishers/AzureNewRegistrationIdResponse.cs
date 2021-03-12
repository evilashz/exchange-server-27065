using System;
using System.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000075 RID: 117
	internal class AzureNewRegistrationIdResponse : AzureResponse
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		public AzureNewRegistrationIdResponse(string responseBody, WebHeaderCollection responseHeaders) : base(responseBody, responseHeaders)
		{
			this.ProcessResponse();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		public AzureNewRegistrationIdResponse(Exception exception, Uri targetUri, string responseBody) : base(exception, targetUri, responseBody)
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000DC97 File Offset: 0x0000BE97
		public AzureNewRegistrationIdResponse(WebException webException, Uri targetUri, string responseBody) : base(webException, targetUri, responseBody)
		{
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000DCA2 File Offset: 0x0000BEA2
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000DCAA File Offset: 0x0000BEAA
		public string RegistrationId { get; private set; }

		// Token: 0x06000424 RID: 1060 RVA: 0x0000DCB3 File Offset: 0x0000BEB3
		protected override string InternalToTraceString()
		{
			return string.Format("RegistrationId: {0}", this.RegistrationId);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		private void ProcessResponse()
		{
			if (!base.HasSucceeded)
			{
				return;
			}
			if (base.ResponseHeaders != null)
			{
				string text = base.ResponseHeaders["Content-Location"];
				if (string.IsNullOrWhiteSpace(text))
				{
					text = base.ResponseHeaders["Location"];
				}
				if (string.IsNullOrWhiteSpace(text))
				{
					return;
				}
				int num = text.LastIndexOf("/");
				text = ((text.Length > ++num) ? text.Substring(num, text.Length - num) : null);
				num = text.IndexOf("?");
				if (num > 0)
				{
					text = text.Substring(0, num);
				}
				this.RegistrationId = text;
			}
		}

		// Token: 0x040001E4 RID: 484
		public const string ContentLocationHeaderName = "Content-Location";

		// Token: 0x040001E5 RID: 485
		public const string ContentAltLocationHeaderName = "Location";
	}
}
