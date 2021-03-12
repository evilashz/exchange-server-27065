using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A4E RID: 2638
	internal class BingRequestAsyncState
	{
		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06004AB6 RID: 19126 RVA: 0x00104A26 File Offset: 0x00102C26
		// (set) Token: 0x06004AB7 RID: 19127 RVA: 0x00104A2E File Offset: 0x00102C2E
		public HttpWebRequest Request { get; set; }

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x00104A37 File Offset: 0x00102C37
		// (set) Token: 0x06004AB9 RID: 19129 RVA: 0x00104A3F File Offset: 0x00102C3F
		public Exception Exception { get; private set; }

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x00104A48 File Offset: 0x00102C48
		// (set) Token: 0x06004ABB RID: 19131 RVA: 0x00104A50 File Offset: 0x00102C50
		public FindPlacesMetadata StatusCodeTag { get; private set; }

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06004ABC RID: 19132 RVA: 0x00104A59 File Offset: 0x00102C59
		// (set) Token: 0x06004ABD RID: 19133 RVA: 0x00104A61 File Offset: 0x00102C61
		public FindPlacesMetadata ResultsCountTag { get; private set; }

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06004ABE RID: 19134 RVA: 0x00104A6A File Offset: 0x00102C6A
		// (set) Token: 0x06004ABF RID: 19135 RVA: 0x00104A72 File Offset: 0x00102C72
		public FindPlacesMetadata ErrorCodeTag { get; private set; }

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06004AC0 RID: 19136 RVA: 0x00104A7B File Offset: 0x00102C7B
		// (set) Token: 0x06004AC1 RID: 19137 RVA: 0x00104A83 File Offset: 0x00102C83
		public FindPlacesMetadata ErrorMessageTag { get; private set; }

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06004AC2 RID: 19138 RVA: 0x00104A8C File Offset: 0x00102C8C
		// (set) Token: 0x06004AC3 RID: 19139 RVA: 0x00104A94 File Offset: 0x00102C94
		public FindPlacesMetadata RequestFailedTag { get; private set; }

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06004AC4 RID: 19140 RVA: 0x00104A9D File Offset: 0x00102C9D
		// (set) Token: 0x06004AC5 RID: 19141 RVA: 0x00104AA5 File Offset: 0x00102CA5
		public List<Persona> ResultsList { get; private set; }

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00104AB0 File Offset: 0x00102CB0
		public BingRequestAsyncState(FindPlacesMetadata statusCodeTag, FindPlacesMetadata resultsCountTag, FindPlacesMetadata latencyTag, FindPlacesMetadata errorCodeTag, FindPlacesMetadata errorMessageTag, FindPlacesMetadata requestFailedTag, Action onRequestCompletedCallback)
		{
			this.StatusCodeTag = statusCodeTag;
			this.ResultsCountTag = resultsCountTag;
			this.latencyTag = latencyTag;
			this.ErrorCodeTag = errorCodeTag;
			this.RequestFailedTag = requestFailedTag;
			this.ErrorMessageTag = errorMessageTag;
			this.onRequestCompletedCallback = onRequestCompletedCallback;
			this.ResultsList = new List<Persona>();
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x00104B04 File Offset: 0x00102D04
		public void Abort()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<BingRequestAsyncState>((long)this.GetHashCode(), "{0}: BingRequestAsyncState::Abort() called.", this);
			this.aborted = true;
			HttpWebRequest request = this.Request;
			if (request != null)
			{
				request.Abort();
			}
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00104B3F File Offset: 0x00102D3F
		public void Begin()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<BingRequestAsyncState>((long)this.GetHashCode(), "{0}: BingRequestAsyncState::Begin() called.", this);
			this.stopWatch = Stopwatch.StartNew();
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x00104B63 File Offset: 0x00102D63
		public void InProgress(CallContext callContext, Exception exception)
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<BingRequestAsyncState>((long)this.GetHashCode(), "{0}: BingRequestAsyncState::InProgress called.", this);
			if (exception != null)
			{
				ExTraceGlobals.FindPlacesCallTracer.TraceDebug<BingRequestAsyncState, Exception>((long)this.GetHashCode(), "{0}: BingRequestAsyncState::InProgress() already failed {1}", this, exception);
				this.End(callContext, exception);
			}
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x00104BA0 File Offset: 0x00102DA0
		public void End(CallContext callContext, Exception exception)
		{
			if (this.aborted)
			{
				ExTraceGlobals.FindPlacesCallTracer.TraceDebug<BingRequestAsyncState>((long)this.GetHashCode(), "{0}: BingRequestAsyncState::End() called after request was aborted.", this);
				return;
			}
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<BingRequestAsyncState>((long)this.GetHashCode(), "{0}: BingRequestAsyncState::End() called.", this);
			this.Request = null;
			this.stopWatch.Stop();
			callContext.ProtocolLog.Set(this.latencyTag, this.stopWatch.ElapsedMilliseconds);
			this.Exception = exception;
			if (exception != null)
			{
				callContext.ProtocolLog.Set(this.RequestFailedTag, "True");
				callContext.ProtocolLog.Set(this.ErrorMessageTag, exception.Message);
				WebException ex = exception as WebException;
				if (ex != null)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						HttpStatusCode statusCode = ((HttpWebResponse)ex.Response).StatusCode;
						callContext.ProtocolLog.Set(this.StatusCodeTag, (int)statusCode);
						callContext.ProtocolLog.Set(this.ErrorCodeTag, (int)statusCode);
					}
				}
				else
				{
					SerializationException ex2 = exception as SerializationException;
					if (ex2 != null)
					{
						callContext.ProtocolLog.Set(this.ErrorCodeTag, "600");
					}
				}
			}
			this.onRequestCompletedCallback();
		}

		// Token: 0x04002A8C RID: 10892
		private readonly FindPlacesMetadata latencyTag;

		// Token: 0x04002A8D RID: 10893
		private readonly Action onRequestCompletedCallback;

		// Token: 0x04002A8E RID: 10894
		private bool aborted;

		// Token: 0x04002A8F RID: 10895
		private Stopwatch stopWatch;
	}
}
