using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007BC RID: 1980
	internal interface IHttpSession
	{
		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06002863 RID: 10339
		// (remove) Token: 0x06002864 RID: 10340
		event EventHandler<HttpWebEventArgs> SendingRequest;

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06002865 RID: 10341
		// (remove) Token: 0x06002866 RID: 10342
		event EventHandler<HttpWebEventArgs> ResponseReceived;

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06002867 RID: 10343
		// (remove) Token: 0x06002868 RID: 10344
		event EventHandler<TestEventArgs> TestStarted;

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06002869 RID: 10345
		// (remove) Token: 0x0600286A RID: 10346
		event EventHandler<TestEventArgs> TestFinished;

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x0600286B RID: 10347
		// (set) Token: 0x0600286C RID: 10348
		object EventState { get; set; }

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600286D RID: 10349
		// (set) Token: 0x0600286E RID: 10350
		string UserAgent { get; set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600286F RID: 10351
		IResponseTracker ResponseTracker { get; }

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002870 RID: 10352
		// (set) Token: 0x06002871 RID: 10353
		ExCookieContainer CookieContainer { get; set; }

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002872 RID: 10354
		Dictionary<string, string> PersistentHeaders { get; }

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002873 RID: 10355
		// (set) Token: 0x06002874 RID: 10356
		AuthenticationData? AuthenticationData { get; set; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002875 RID: 10357
		// (set) Token: 0x06002876 RID: 10358
		SslValidationOptions SslValidationOptions { get; set; }

		// Token: 0x06002877 RID: 10359
		void NotifyTestStarted(TestId testId);

		// Token: 0x06002878 RID: 10360
		void NotifyTestFinished(TestId testId);

		// Token: 0x06002879 RID: 10361
		IAsyncResult BeginGet(TestId stepId, string uri, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x0600287A RID: 10362
		T EndGet<T>(IAsyncResult result, Func<HttpWebResponseWrapper, T> processResponse);

		// Token: 0x0600287B RID: 10363
		T EndGet<T>(IAsyncResult result, HttpStatusCode[] expectedStatusCodes, Func<HttpWebResponseWrapper, T> processResponse);

		// Token: 0x0600287C RID: 10364
		IAsyncResult BeginPost(TestId stepId, string uri, RequestBody body, string contentType, Dictionary<string, string> headers, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x0600287D RID: 10365
		IAsyncResult BeginPost(TestId stepId, string uri, RequestBody body, string contentType, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x0600287E RID: 10366
		IAsyncResult BeginPostFollowingRedirections(TestId stepId, Uri uri, RequestBody body, string contentType, Dictionary<string, string> headers, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x0600287F RID: 10367
		IAsyncResult BeginPostFollowingRedirections(TestId stepId, string uri, RequestBody body, string contentType, Dictionary<string, string> headers, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x06002880 RID: 10368
		T EndPost<T>(IAsyncResult result, Func<HttpWebResponseWrapper, T> processResponse);

		// Token: 0x06002881 RID: 10369
		T EndPost<T>(IAsyncResult result, HttpStatusCode[] expectedStatusCodes, Func<HttpWebResponseWrapper, T> processResponse);

		// Token: 0x06002882 RID: 10370
		IAsyncResult BeginGetFollowingRedirections(TestId stepId, string uri, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x06002883 RID: 10371
		IAsyncResult BeginGetFollowingRedirections(TestId stepId, Uri uri, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x06002884 RID: 10372
		IAsyncResult BeginGetFollowingRedirections(TestId stepId, string uri, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x06002885 RID: 10373
		T EndGetFollowingRedirections<T>(IAsyncResult result, Func<HttpWebResponseWrapper, T> processResponse);

		// Token: 0x06002886 RID: 10374
		T EndPostFollowingRedirections<T>(IAsyncResult result, Func<HttpWebResponseWrapper, T> processResponse);

		// Token: 0x06002887 RID: 10375
		void CloseConnections();

		// Token: 0x06002888 RID: 10376
		ScenarioException VerifyScenarioExceededRunTime(TimeSpan? maxAllowedTime);

		// Token: 0x06002889 RID: 10377
		List<string> GetHostNames(RequestTarget requestTarget);
	}
}
