using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000073 RID: 115
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OutlookTask : BaseTask
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00009190 File Offset: 0x00007390
		public OutlookTask(IContext context) : base(context, Strings.OutlookTaskTitle, Strings.OutlookTaskDescription, TaskType.Knowledge, new ContextProperty[0])
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000092B4 File Offset: 0x000074B4
		protected override IEnumerator<ITask> Process()
		{
			ITask task = new CompositeTask(base.CreateDerivedContext(), new ITask[]
			{
				new DiscoverWebProxyTask(base.CreateDerivedContext()),
				new VerifyRpcProxyTask(base.CreateDerivedContext()),
				new NspiTask(base.CreateDerivedContext()),
				new RfriTask(base.CreateDerivedContext()),
				new EmsmdbTask(base.CreateDerivedContext())
			});
			yield return task;
			base.Result = task.Result;
			yield break;
		}
	}
}
