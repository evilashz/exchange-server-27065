using System;
using System.Collections.Generic;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000041 RID: 65
	public interface IHttpRequest
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000199 RID: 409
		// (set) Token: 0x0600019A RID: 410
		string AcceptLanguage { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600019B RID: 411
		// (set) Token: 0x0600019C RID: 412
		string UserAgent { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600019D RID: 413
		// (set) Token: 0x0600019E RID: 414
		ProcessCookies ProcessCookies { get; set; }

		// Token: 0x0600019F RID: 415
		ServerResponse SendGetRequest(string uri, bool sslValidation, string componentId, bool allowRedirects, int timeout, string authenticationType, string authenticationUser, string authenticationPassword, Dictionary<string, string> properties);

		// Token: 0x060001A0 RID: 416
		ServerResponse SendPostRequest(string uri, bool allowRedirects, bool getHiddenInputValues, ref PostData postData, string contentType, string formName = null, int timeout = 0);
	}
}
