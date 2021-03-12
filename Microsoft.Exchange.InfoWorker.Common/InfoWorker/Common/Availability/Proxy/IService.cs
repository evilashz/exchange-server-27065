using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020000A5 RID: 165
	internal interface IService
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000384 RID: 900
		// (set) Token: 0x06000385 RID: 901
		CookieContainer CookieContainer { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000386 RID: 902
		// (set) Token: 0x06000387 RID: 903
		IWebProxy Proxy { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000388 RID: 904
		// (set) Token: 0x06000389 RID: 905
		ICredentials Credentials { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600038A RID: 906
		// (set) Token: 0x0600038B RID: 907
		bool EnableDecompression { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600038C RID: 908
		// (set) Token: 0x0600038D RID: 909
		string UserAgent { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600038E RID: 910
		string Url { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600038F RID: 911
		// (set) Token: 0x06000390 RID: 912
		int Timeout { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000391 RID: 913
		// (set) Token: 0x06000392 RID: 914
		RequestTypeHeader requestTypeValue { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000393 RID: 915
		// (set) Token: 0x06000394 RID: 916
		RequestServerVersion RequestServerVersionValue { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000395 RID: 917
		int ServiceVersion { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000396 RID: 918
		Dictionary<string, string> HttpHeaders { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000397 RID: 919
		bool SupportsProxyAuthentication { get; }

		// Token: 0x06000398 RID: 920
		void Abort();

		// Token: 0x06000399 RID: 921
		void Dispose();

		// Token: 0x0600039A RID: 922
		IAsyncResult BeginGetUserPhoto(PhotoRequest request, PhotosConfiguration configuration, AsyncCallback callback, object asyncState);

		// Token: 0x0600039B RID: 923
		GetUserPhotoResponseMessageType EndGetUserPhoto(IAsyncResult asyncResult);

		// Token: 0x0600039C RID: 924
		IAsyncResult BeginGetUserAvailability(GetUserAvailabilityRequest request, AsyncCallback callback, object asyncState);

		// Token: 0x0600039D RID: 925
		GetUserAvailabilityResponse EndGetUserAvailability(IAsyncResult asyncResult);

		// Token: 0x0600039E RID: 926
		IAsyncResult BeginGetMailTips(GetMailTipsType getMailTips1, AsyncCallback callback, object asyncState);

		// Token: 0x0600039F RID: 927
		GetMailTipsResponseMessageType EndGetMailTips(IAsyncResult asyncResult);

		// Token: 0x060003A0 RID: 928
		IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportRequestType findMessageTrackingReport1, AsyncCallback callback, object asyncState);

		// Token: 0x060003A1 RID: 929
		FindMessageTrackingReportResponseMessageType EndFindMessageTrackingReport(IAsyncResult asyncResult);

		// Token: 0x060003A2 RID: 930
		IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportRequestType getMessageTrackingReport1, AsyncCallback callback, object asyncState);

		// Token: 0x060003A3 RID: 931
		GetMessageTrackingReportResponseMessageType EndGetMessageTrackingReport(IAsyncResult asyncResult);
	}
}
