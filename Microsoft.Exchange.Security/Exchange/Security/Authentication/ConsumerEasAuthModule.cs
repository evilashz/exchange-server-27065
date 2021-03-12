using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000064 RID: 100
	public class ConsumerEasAuthModule : IHttpModule
	{
		// Token: 0x06000349 RID: 841 RVA: 0x0001ADDD File Offset: 0x00018FDD
		void IHttpModule.Init(HttpApplication application)
		{
			application.AddOnAuthenticateRequestAsync(new BeginEventHandler(this.BeginOnAuthenticate), new EndEventHandler(this.EndOnAuthenticate));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001ADFD File Offset: 0x00018FFD
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001ADFF File Offset: 0x00018FFF
		private IAsyncResult BeginOnAuthenticate(object source, EventArgs args, AsyncCallback callback, object state)
		{
			return this.InternalBeginOnAuthenticate(source, args, callback, state);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001AF10 File Offset: 0x00019110
		private IAsyncResult InternalBeginOnAuthenticate(object source, EventArgs args, AsyncCallback callback, object state)
		{
			this.TraceEnterFunction("BeginOnAuthenticate");
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				this.application = (HttpApplication)source;
				HttpContext context = this.application.Context;
				if (this.asyncOp != null)
				{
					ExTraceGlobals.AuthenticationTracer.TraceError((long)this.GetHashCode(), "BeginOnAuthenticate called with existing asyncOp");
					throw new InvalidOperationException("this.asyncOp should be null");
				}
				this.asyncOp = new LazyAsyncResult(this, state, callback);
				string text = context.Request.Headers["Authorization"];
				if (!string.IsNullOrEmpty(text) && text.StartsWith("RpsToken ", StringComparison.OrdinalIgnoreCase))
				{
					GenericIdentity identity = new GenericIdentity(AuthCommon.MemberNameNullSid.ToString(), "RpsTokenAuth");
					this.application.Context.User = new GenericPrincipal(identity, null);
				}
				this.asyncOp.InvokeCallback();
			}, (object exception) => true, ReportOptions.DoNotCollectDumps | ReportOptions.DeepStackTraceHash);
			this.TraceExitFunction("BeginOnAuthenticate");
			return this.asyncOp;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001AF8B File Offset: 0x0001918B
		private void EndOnAuthenticate(IAsyncResult asyncResult)
		{
			this.InternalEndOnAuthenticate(asyncResult);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001AF94 File Offset: 0x00019194
		private void InternalEndOnAuthenticate(IAsyncResult asyncResult)
		{
			this.TraceEnterFunction("EndOnAuthenticate");
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			if (!lazyAsyncResult.IsCompleted)
			{
				lazyAsyncResult.InternalWaitForCompletion();
			}
			this.asyncOp = null;
			this.TraceExitFunction("EndOnAuthenticate");
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001AFE2 File Offset: 0x000191E2
		private void TraceEnterFunction(string functionName)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction<string>((long)this.GetHashCode(), "Enter Function: LiveIdBasicAuthModule.{0}.", functionName);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001AFFB File Offset: 0x000191FB
		private void TraceExitFunction(string functionName)
		{
			ExTraceGlobals.AuthenticationTracer.TraceFunction<string>((long)this.GetHashCode(), "Exit Function: LiveIdBasicAuthModule.{0}.", functionName);
		}

		// Token: 0x0400034E RID: 846
		internal const string RpsTokenAuthType = "RpsTokenAuth";

		// Token: 0x0400034F RID: 847
		private const string RpsTokenAuthHeaderType = "RpsToken ";

		// Token: 0x04000350 RID: 848
		private HttpApplication application;

		// Token: 0x04000351 RID: 849
		private LazyAsyncResult asyncOp;
	}
}
