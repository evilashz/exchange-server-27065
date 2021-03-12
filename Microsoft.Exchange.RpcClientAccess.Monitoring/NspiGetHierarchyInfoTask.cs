using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200006A RID: 106
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetHierarchyInfoTask : BaseNspiRpcTask
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0000830C File Offset: 0x0000650C
		public NspiGetHierarchyInfoTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiGetHierarchyInfoTaskTitle, Strings.NspiGetHierarchyInfoTaskDescription, TaskType.Operation, new ContextProperty[0])
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000842C File Offset: 0x0000662C
		protected override IEnumerator<ITask> Process()
		{
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.nspiClient.BeginGetHierarchyInfo(base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), delegate(IAsyncResult asyncResult)
			{
				int num = -1;
				return base.ApplyCallResult(this.nspiClient.EndGetHierarchyInfo(asyncResult, out num));
			});
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x0400014C RID: 332
		private readonly INspiClient nspiClient;
	}
}
