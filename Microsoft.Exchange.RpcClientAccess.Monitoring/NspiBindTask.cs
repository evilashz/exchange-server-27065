using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000068 RID: 104
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiBindTask : BaseNspiRpcTask
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00008008 File Offset: 0x00006208
		public NspiBindTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiBindTaskTitle, Strings.NspiBindTaskDescription, TaskType.Operation, new ContextProperty[0])
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008118 File Offset: 0x00006318
		protected override IEnumerator<ITask> Process()
		{
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.nspiClient.BeginBind(base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), (IAsyncResult asyncResult) => base.ApplyCallResult(this.nspiClient.EndBind(asyncResult)));
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x0400014A RID: 330
		private readonly INspiClient nspiClient;
	}
}
