using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200007D RID: 125
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class VerifyRpcProxyTask : BaseTask
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000A3E4 File Offset: 0x000085E4
		public VerifyRpcProxyTask(IContext context) : base(context, Strings.VerifyRpcProxyTaskTitle, Strings.VerifyRpcProxyTaskDescription, TaskType.Knowledge, RpcHelper.DependenciesOfBuildRpcProxyOnlyBindingInfo.Concat(new ContextProperty[]
		{
			ContextPropertySchema.Latency.SetOnly(),
			ContextPropertySchema.RequestUrl.SetOnly(),
			ContextPropertySchema.ResponseStatusCode.SetOnly(),
			ContextPropertySchema.CertificateValidationErrors.SetOnly(),
			ContextPropertySchema.RequestedRpcProxyAuthenticationTypes.SetOnly(),
			ContextPropertySchema.RespondingWebProxyServer.SetOnly(),
			ContextPropertySchema.RespondingHttpServer.SetOnly(),
			ContextPropertySchema.RespondingRpcProxyServer.SetOnly(),
			ContextPropertySchema.ResponseHeaderCollection.SetOnly()
		}))
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A48B File Offset: 0x0000868B
		public override BaseTask Copy()
		{
			return new VerifyRpcProxyTask(base.CreateContextCopy());
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A6C0 File Offset: 0x000088C0
		protected override IEnumerator<ITask> Process()
		{
			IVerifyRpcProxyClient rpcProxyClient = base.Environment.CreateVerifyRpcProxyClient(RpcHelper.BuildRpcProxyOnlyBindingInfo(base.Properties));
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback callback, object asyncState) => rpcProxyClient.BeginVerifyRpcProxy(false, callback, asyncState), delegate(IAsyncResult asyncResult)
			{
				VerifyRpcProxyResult verifyRpcProxyResult = rpcProxyClient.EndVerifyRpcProxy(asyncResult);
				this.Set<TimeSpan>(ContextPropertySchema.Latency, verifyRpcProxyResult.Latency);
				this.Set<string>(ContextPropertySchema.RequestUrl, verifyRpcProxyResult.RpcProxyUrl);
				if (verifyRpcProxyResult.ResponseStatusCode != null)
				{
					this.Set<HttpStatusCode?>(ContextPropertySchema.ResponseStatusCode, verifyRpcProxyResult.ResponseStatusCode);
				}
				if (verifyRpcProxyResult.RequestedRpcProxyAuthenticationTypes.Length != 0)
				{
					this.Set<string[]>(ContextPropertySchema.RequestedRpcProxyAuthenticationTypes, verifyRpcProxyResult.RequestedRpcProxyAuthenticationTypes);
				}
				if (verifyRpcProxyResult.ServerCertificateValidationError != null)
				{
					this.Set<CertificateValidationError>(ContextPropertySchema.CertificateValidationErrors, verifyRpcProxyResult.ServerCertificateValidationError);
				}
				if (verifyRpcProxyResult.ResponseWebHeaderCollection != null)
				{
					this.Set<WebHeaderCollection>(ContextPropertySchema.ResponseHeaderCollection, verifyRpcProxyResult.ResponseWebHeaderCollection);
					this.FigureOutHttpProxyUsage(verifyRpcProxyResult);
					this.FigureOutRespondingServers(verifyRpcProxyResult);
				}
				if (!verifyRpcProxyResult.IsSuccessful)
				{
					RpcHelper.FigureOutErrorInformation(this.Properties, verifyRpcProxyResult);
				}
				if (!verifyRpcProxyResult.IsSuccessful)
				{
					return TaskResult.Failed;
				}
				return TaskResult.Success;
			});
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A6DC File Offset: 0x000088DC
		private void FigureOutRespondingServers(VerifyRpcProxyResult verifyRpcProxyResult)
		{
			string text = verifyRpcProxyResult.ResponseWebHeaderCollection[Constants.ClientAccessServerHeaderName];
			string text2 = verifyRpcProxyResult.ResponseWebHeaderCollection[Constants.BackendServerHeaderName];
			string text3 = verifyRpcProxyResult.ResponseWebHeaderCollection[Constants.DiagInfoHeaderName];
			if (!string.IsNullOrWhiteSpace(text) || !string.IsNullOrWhiteSpace(text3))
			{
				base.Set<string>(ContextPropertySchema.RespondingHttpServer, text ?? text3);
			}
			if (!string.IsNullOrWhiteSpace(text2) || !string.IsNullOrWhiteSpace(text3))
			{
				base.Set<string>(ContextPropertySchema.RespondingRpcProxyServer, text2 ?? text3);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A760 File Offset: 0x00008960
		private void FigureOutHttpProxyUsage(VerifyRpcProxyResult verifyRpcProxyResult)
		{
			string text = verifyRpcProxyResult.ResponseWebHeaderCollection["Via"];
			if (text != null)
			{
				int num = text.LastIndexOf(' ');
				if (num != -1)
				{
					base.Set<string>(ContextPropertySchema.RespondingWebProxyServer, text.Substring(num + 1));
				}
			}
		}
	}
}
