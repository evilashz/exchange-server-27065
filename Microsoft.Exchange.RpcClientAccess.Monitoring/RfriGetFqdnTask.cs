using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000078 RID: 120
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RfriGetFqdnTask : BaseRpcTask
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public RfriGetFqdnTask(IRfriClient rfriClient, IContext context) : base(context, Strings.RfriGetFqdnTaskTitle, Strings.RfriGetFqdnTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.RpcServerLegacyDN.GetOnly(),
			ContextPropertySchema.RpcServer.SetOnly()
		})
		{
			this.rfriClient = rfriClient;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009D5C File Offset: 0x00007F5C
		protected override IEnumerator<ITask> Process()
		{
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.rfriClient.BeginGetFqdnFromLegacyDn(base.Get<string>(ContextPropertySchema.RpcServerLegacyDN), base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), delegate(IAsyncResult asyncResult)
			{
				string value = null;
				RfriCallResult rfriCallResult = this.rfriClient.EndGetFqdnFromLegacyDn(asyncResult, out value);
				if (rfriCallResult.IsSuccessful)
				{
					base.Set<string>(ContextPropertySchema.RpcServer, value);
				}
				return base.ApplyCallResult(rfriCallResult);
			});
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x0400015A RID: 346
		private readonly IRfriClient rfriClient;
	}
}
