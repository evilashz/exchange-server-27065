using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000079 RID: 121
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RfriGetNewDsaTask : BaseRpcTask
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00009D78 File Offset: 0x00007F78
		public RfriGetNewDsaTask(IRfriClient rfriClient, IContext context) : base(context, Strings.RfriGetNewDsaTaskTitle, Strings.RfriGetNewDsaTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.UserLegacyDN.GetOnly(),
			ContextPropertySchema.DirectoryServer.SetOnly()
		})
		{
			this.rfriClient = rfriClient;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009EE0 File Offset: 0x000080E0
		protected override IEnumerator<ITask> Process()
		{
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.rfriClient.BeginGetNewDsa(base.Get<string>(ContextPropertySchema.UserLegacyDN), base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), delegate(IAsyncResult asyncResult)
			{
				string value;
				RfriCallResult rfriCallResult = this.rfriClient.EndGetNewDsa(asyncResult, out value);
				if (rfriCallResult.IsSuccessful)
				{
					base.Set<string>(ContextPropertySchema.DirectoryServer, value);
				}
				return base.ApplyCallResult(rfriCallResult);
			});
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x0400015B RID: 347
		private readonly IRfriClient rfriClient;
	}
}
