using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class VerifyRpcProxyNlbAffinityTask : BaseTask
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000A230 File Offset: 0x00008430
		public VerifyRpcProxyNlbAffinityTask(IContext context) : base(context, Strings.VerifyRpcProxyTaskTitle, Strings.VerifyRpcProxyTaskDescription, TaskType.Knowledge, RpcHelper.DependenciesOfBuildRpcProxyOnlyBindingInfo.Concat(new ContextProperty[]
		{
			ContextPropertySchema.Exception.SetOnly(),
			ContextPropertySchema.ErrorDetails.SetOnly()
		}))
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A3C8 File Offset: 0x000085C8
		protected override IEnumerator<ITask> Process()
		{
			IVerifyRpcProxyClient rpcProxyClient = base.Environment.CreateVerifyRpcProxyClient(RpcHelper.BuildRpcProxyOnlyBindingInfo(base.Properties));
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback callback, object asyncState) => rpcProxyClient.BeginVerifyRpcProxy(true, callback, asyncState), delegate(IAsyncResult asyncResult)
			{
				VerifyRpcProxyResult verifyRpcProxyResult = rpcProxyClient.EndVerifyRpcProxy(asyncResult);
				return RpcHelper.FigureOutErrorInformation(this.Properties, verifyRpcProxyResult);
			});
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}
	}
}
