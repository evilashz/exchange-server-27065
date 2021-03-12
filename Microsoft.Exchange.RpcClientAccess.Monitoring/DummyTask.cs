using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200005E RID: 94
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class DummyTask : BaseRpcTask
	{
		// Token: 0x06000207 RID: 519 RVA: 0x000073D4 File Offset: 0x000055D4
		public DummyTask(IContext context, params ContextProperty[] dependentProperties) : base(context, Strings.DummyTaskTitle, Strings.DummyTaskDescription, TaskType.Operation, dependentProperties.Concat(new ContextProperty[]
		{
			ContextPropertySchema.ActualBinding.SetOnly()
		}))
		{
		}

		// Token: 0x06000208 RID: 520
		protected abstract IEmsmdbClient CreateEmsmdbClient();

		// Token: 0x06000209 RID: 521 RVA: 0x0000762C File Offset: 0x0000582C
		protected override IEnumerator<ITask> Process()
		{
			using (IEmsmdbClient dummyClient = this.CreateEmsmdbClient())
			{
				AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => dummyClient.BeginDummy(this.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), delegate(IAsyncResult asyncResult)
				{
					DummyCallResult callResult = dummyClient.EndDummy(asyncResult);
					return this.ApplyCallResult(callResult);
				});
				yield return asyncTask;
				base.Result = asyncTask.Result;
			}
			yield break;
		}
	}
}
