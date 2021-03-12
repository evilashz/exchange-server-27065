using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbLogonTask : EmsmdbOperationBaseTask
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000797C File Offset: 0x00005B7C
		public EmsmdbLogonTask(IEmsmdbClient emsmdbClient, IContext context) : base(context, Strings.EmsmdbLogonTaskTitle, Strings.EmsmdbLogonTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.MailboxLegacyDN.GetOnly()
		})
		{
			this.emsmdbClient = emsmdbClient;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007AB0 File Offset: 0x00005CB0
		protected override IEnumerator<ITask> Process()
		{
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.emsmdbClient.BeginLogon(base.Get<string>(ContextPropertySchema.MailboxLegacyDN), base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), (IAsyncResult asyncResult) => this.ApplyCallResult(this.emsmdbClient.EndLogon(asyncResult)));
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007ACC File Offset: 0x00005CCC
		private TaskResult ApplyCallResult(LogonCallResult callResult)
		{
			if (callResult.LogonErrorCode != ErrorCode.None)
			{
				base.Set<RopExecutionException>(ContextPropertySchema.Exception, new RopExecutionException(Strings.RpcCallResultErrorCodeDescription(callResult.GetType().Name), callResult.LogonErrorCode));
			}
			return base.ApplyCallResult(callResult);
		}

		// Token: 0x0400013F RID: 319
		private readonly IEmsmdbClient emsmdbClient;
	}
}
