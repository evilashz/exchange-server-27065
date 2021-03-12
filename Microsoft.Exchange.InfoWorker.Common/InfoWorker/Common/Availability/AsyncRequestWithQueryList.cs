using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000053 RID: 83
	internal abstract class AsyncRequestWithQueryList : AsyncRequest
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00009970 File Offset: 0x00007B70
		protected AsyncRequestWithQueryList(Application application, ClientContext clientContext, RequestType requestType, RequestLogger requestLogger, QueryList queryList) : base(application, clientContext, requestLogger)
		{
			this.QueryList = queryList;
			this.RequestType = requestType;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000998B File Offset: 0x00007B8B
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00009993 File Offset: 0x00007B93
		public RequestType RequestType { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000999C File Offset: 0x00007B9C
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000099A4 File Offset: 0x00007BA4
		public QueryList QueryList { get; private set; }

		// Token: 0x060001E3 RID: 483 RVA: 0x000099AD File Offset: 0x00007BAD
		public override void Abort()
		{
			base.Abort();
			this.SetExceptionInResultList(AsyncRequestWithQueryList.TimeoutExpiredException);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000099C0 File Offset: 0x00007BC0
		protected void SetExceptionInResultList(LocalizedException exception)
		{
			AsyncRequestWithQueryList.RequestRoutingTracer.TraceDebug<object, LocalizedException>((long)this.GetHashCode(), "{0}: Setting exception to all queries: {1}", TraceContext.Get(), exception);
			this.QueryList.SetResultInAllQueries(base.Application.CreateQueryResult(exception));
		}

		// Token: 0x04000133 RID: 307
		private static readonly LocalizedException TimeoutExpiredException = new TimeoutExpiredException("Waiting-For-Request-Completion");

		// Token: 0x04000134 RID: 308
		private static readonly Trace RequestRoutingTracer = ExTraceGlobals.RequestRoutingTracer;
	}
}
