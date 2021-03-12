using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200007E RID: 126
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class VerifyRpcProxyTimeoutTask : BaseTask
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public VerifyRpcProxyTimeoutTask(IContext context) : base(context, Strings.VerifyRpcProxyTaskTitle, Strings.VerifyRpcProxyTaskDescription, TaskType.Knowledge, RpcHelper.DependenciesOfBuildRpcProxyOnlyBindingInfo.Concat(new ContextProperty[]
		{
			ContextPropertySchema.Exception.SetOnly(),
			ContextPropertySchema.ErrorDetails.SetOnly()
		}))
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A93C File Offset: 0x00008B3C
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
