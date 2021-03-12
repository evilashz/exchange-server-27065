using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200006B RID: 107
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetMatchesTask : BaseRpcTask
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00008448 File Offset: 0x00006648
		public NspiGetMatchesTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiGetMatchesTaskTitle, Strings.NspiGetMatchesTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.PrimarySmtpAddress.GetOnly(),
			ContextPropertySchema.NspiMinimalIds.SetOnly()
		})
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008604 File Offset: 0x00006804
		protected override IEnumerator<ITask> Process()
		{
			int[] minimalIds = null;
			AsyncTask asyncGetMatchesTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.nspiClient.BeginGetMatches(base.Get<string>(ContextPropertySchema.PrimarySmtpAddress), base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), (IAsyncResult asyncResult) => this.ApplyCallResult(this.nspiClient.EndGetMatches(asyncResult, out minimalIds)));
			yield return asyncGetMatchesTask;
			base.Result = asyncGetMatchesTask.Result;
			if (base.Result == TaskResult.Success && minimalIds != null)
			{
				base.Set<int[]>(ContextPropertySchema.NspiMinimalIds, minimalIds);
			}
			yield break;
		}

		// Token: 0x0400014D RID: 333
		private readonly INspiClient nspiClient;
	}
}
