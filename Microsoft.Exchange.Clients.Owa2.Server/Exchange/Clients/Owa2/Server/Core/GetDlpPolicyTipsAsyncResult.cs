using System;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C6 RID: 966
	internal class GetDlpPolicyTipsAsyncResult : AsyncResult
	{
		// Token: 0x06001F04 RID: 7940 RVA: 0x00076DDB File Offset: 0x00074FDB
		public GetDlpPolicyTipsAsyncResult(AsyncCallback callback, object context) : base(callback, context)
		{
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x00076DE5 File Offset: 0x00074FE5
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x00076DED File Offset: 0x00074FED
		public GetDlpPolicyTipsResponse Response { get; set; }

		// Token: 0x06001F07 RID: 7943 RVA: 0x00076EBC File Offset: 0x000750BC
		public void GetDlpPolicyTipsCommand(GetDlpPolicyTipsRequest request)
		{
			if (Interlocked.Increment(ref GetDlpPolicyTipsAsyncResult.PendingRequestCount) > 50)
			{
				Interlocked.Decrement(ref GetDlpPolicyTipsAsyncResult.PendingRequestCount);
				this.Response = GetDlpPolicyTipsResponse.TooManyPendingRequestResponse;
				base.Callback(this);
				return;
			}
			CallContext callContext = CallContext.Current;
			ThreadPool.QueueUserWorkItem(delegate(object obj)
			{
				try
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						CallContext.SetCurrent(callContext);
						this.Response = new GetDlpPolicyTipsCommand(callContext, request).Execute();
						this.Callback(this);
					});
				}
				catch (GrayException arg)
				{
					ExTraceGlobals.OwaRulesEngineTracer.TraceError<GrayException>(0L, "GetDlpPolicyTipsAsyncResult GrayException occured while evaluating for Dlp.  exception {0}", arg);
				}
				finally
				{
					CallContext.SetCurrent(null);
					Interlocked.Decrement(ref GetDlpPolicyTipsAsyncResult.PendingRequestCount);
				}
			}, base.AsyncState);
		}

		// Token: 0x04001182 RID: 4482
		public static int PendingRequestCount;
	}
}
