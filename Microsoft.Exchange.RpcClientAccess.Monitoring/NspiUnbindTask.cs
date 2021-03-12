using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000071 RID: 113
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiUnbindTask : BaseNspiRpcTask
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00008FF0 File Offset: 0x000071F0
		public NspiUnbindTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiUnbindTaskTitle, Strings.NspiUnbindTaskDescription, TaskType.Operation, new ContextProperty[0])
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009100 File Offset: 0x00007300
		protected override IEnumerator<ITask> Process()
		{
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.nspiClient.BeginUnbind(base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), (IAsyncResult asyncResult) => base.ApplyCallResult(this.nspiClient.EndUnbind(asyncResult)));
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x04000154 RID: 340
		private readonly INspiClient nspiClient;
	}
}
