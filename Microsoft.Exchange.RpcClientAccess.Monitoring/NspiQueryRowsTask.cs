using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200006F RID: 111
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiQueryRowsTask : BaseNspiRpcTask
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00008A4C File Offset: 0x00006C4C
		public NspiQueryRowsTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiQueryRowsTaskTitle, Strings.NspiQueryRowsTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.NspiMinimalIds.GetOnly(),
			ContextPropertySchema.HomeMdbLegacyDN.SetOnly(),
			ContextPropertySchema.UserLegacyDN.SetOnly()
		})
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008BD4 File Offset: 0x00006DD4
		protected override IEnumerator<ITask> Process()
		{
			AsyncTask asyncQueryRowsTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.nspiClient.BeginQueryRows(base.Get<int[]>(ContextPropertySchema.NspiMinimalIds), base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), delegate(IAsyncResult asyncResult)
			{
				string value = null;
				string value2 = null;
				NspiCallResult nspiCallResult = this.nspiClient.EndQueryRows(asyncResult, out value, out value2);
				if (nspiCallResult.IsSuccessful)
				{
					base.Set<string>(ContextPropertySchema.HomeMdbLegacyDN, value);
					base.Set<string>(ContextPropertySchema.UserLegacyDN, value2);
				}
				return base.ApplyCallResult(nspiCallResult);
			});
			yield return asyncQueryRowsTask;
			base.Result = asyncQueryRowsTask.Result;
			yield break;
		}

		// Token: 0x04000151 RID: 337
		private readonly INspiClient nspiClient;
	}
}
