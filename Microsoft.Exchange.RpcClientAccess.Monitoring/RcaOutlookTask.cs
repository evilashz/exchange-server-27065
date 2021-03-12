using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RcaOutlookTask : BaseTask
	{
		// Token: 0x0600024D RID: 589 RVA: 0x000096F8 File Offset: 0x000078F8
		public RcaOutlookTask(IContext context) : base(context, Strings.RcaOutlookTaskTitle, Strings.RcaOutlookTaskDescription, TaskType.Knowledge, new ContextProperty[0])
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000981C File Offset: 0x00007A1C
		protected override IEnumerator<ITask> Process()
		{
			ITask task = new CompositeTask(base.CreateDerivedContext(), new ITask[]
			{
				new DiscoverWebProxyTask(base.CreateDerivedContext()),
				new VerifyRpcProxyTask(base.CreateDerivedContext()),
				new EmsmdbTask(base.CreateDerivedContext()),
				new NspiTask(base.CreateDerivedContext()),
				new RfriTask(base.CreateDerivedContext())
			});
			yield return task;
			base.Result = task.Result;
			yield break;
		}
	}
}
