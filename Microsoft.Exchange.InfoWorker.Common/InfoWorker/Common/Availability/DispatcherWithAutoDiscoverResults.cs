using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000074 RID: 116
	internal sealed class DispatcherWithAutoDiscoverResults : BaseRequestDispatcher
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000D8FC File Offset: 0x0000BAFC
		public DispatcherWithAutoDiscoverResults(Application application, QueryList queryList, IList<AutoDiscoverQueryItem> autoDiscoverQueryItems, ProxyAuthenticator proxyAuthenticator, RequestType requestType, DispatcherWithAutoDiscoverResults.CreateRequestWithQueryListDelegate createRequestDelegate)
		{
			DispatcherWithAutoDiscoverResults <>4__this = this;
			this.createRequestDelegate = createRequestDelegate;
			for (int i = 0; i < queryList.Count; i++)
			{
				BaseQuery baseQuery = queryList[i];
				AutoDiscoverResult autoDiscoverResult = autoDiscoverQueryItems[i].Result;
				if (autoDiscoverResult.Exception != null)
				{
					DispatcherWithAutoDiscoverResults.RequestRoutingTracer.TraceError<object, EmailAddress>((long)this.GetHashCode(), "{0}: autodiscover for {1} failed and it will not be dispatched for query", TraceContext.Get(), baseQuery.Email);
					baseQuery.SetResultOnFirstCall(application.CreateQueryResult(autoDiscoverResult.Exception));
				}
				else
				{
					string key = autoDiscoverResult.WebServiceUri.Uri.ToString();
					if (autoDiscoverResult.WebServiceUri.EmailAddress != null)
					{
						baseQuery.RecipientData.EmailAddress = autoDiscoverResult.WebServiceUri.EmailAddress;
					}
					base.Add(key, baseQuery, requestType, (QueryList perRequestQueryList) => <>4__this.createRequestDelegate(perRequestQueryList, proxyAuthenticator ?? autoDiscoverResult.ProxyAuthenticator, autoDiscoverResult.WebServiceUri, UriSource.EmailDomain));
				}
			}
		}

		// Token: 0x040001CD RID: 461
		private DispatcherWithAutoDiscoverResults.CreateRequestWithQueryListDelegate createRequestDelegate;

		// Token: 0x040001CE RID: 462
		private static readonly Trace RequestRoutingTracer = ExTraceGlobals.RequestRoutingTracer;

		// Token: 0x02000075 RID: 117
		// (Invoke) Token: 0x060002ED RID: 749
		public delegate AsyncRequest CreateRequestWithQueryListDelegate(QueryList queryList, ProxyAuthenticator proxyAuthenticator, WebServiceUri webServiceUri, UriSource source);
	}
}
