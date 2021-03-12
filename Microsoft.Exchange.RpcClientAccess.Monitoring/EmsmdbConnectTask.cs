using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000062 RID: 98
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbConnectTask : EmsmdbOperationBaseTask
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00007754 File Offset: 0x00005954
		public EmsmdbConnectTask(IEmsmdbClient emsmdbClient, IContext context) : base(context, Strings.EmsmdbConnectTaskTitle, Strings.EmsmdbConnectTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.UserLegacyDN.GetOnly(),
			ContextPropertySchema.UseMonitoringContext.GetOnly(),
			ContextPropertySchema.RespondingRpcClientAccessServerVersion.SetOnly(),
			ContextPropertySchema.RequestUrl.SetOnly(),
			ContextPropertySchema.RespondingHttpServer.SetOnly(),
			ContextPropertySchema.RespondingRpcProxyServer.SetOnly()
		})
		{
			this.emsmdbClient = emsmdbClient;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000078D4 File Offset: 0x00005AD4
		protected override IEnumerator<ITask> Process()
		{
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.emsmdbClient.BeginConnect(base.Get<string>(ContextPropertySchema.UserLegacyDN), base.Get<TimeSpan>(BaseTask.Timeout), base.Get<bool>(ContextPropertySchema.UseMonitoringContext), asyncCallback, asyncState), (IAsyncResult asyncResult) => this.ApplyCallResult(this.emsmdbClient.EndConnect(asyncResult)));
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000078F0 File Offset: 0x00005AF0
		private TaskResult ApplyCallResult(ConnectCallResult callResult)
		{
			base.Set<string>(ContextPropertySchema.RequestUrl, this.emsmdbClient.GetConnectionUriString());
			if (callResult.ServerVersion != null)
			{
				base.Set<MapiVersion>(ContextPropertySchema.RespondingRpcClientAccessServerVersion, callResult.ServerVersion.Value);
			}
			base.Set<string>(ContextPropertySchema.RespondingHttpServer, callResult.HttpResponseHeaders[Constants.ClientAccessServerHeaderName]);
			base.Set<string>(ContextPropertySchema.RespondingRpcProxyServer, callResult.HttpResponseHeaders[Constants.BackendServerHeaderName]);
			return base.ApplyCallResult(callResult);
		}

		// Token: 0x0400013E RID: 318
		private readonly IEmsmdbClient emsmdbClient;
	}
}
