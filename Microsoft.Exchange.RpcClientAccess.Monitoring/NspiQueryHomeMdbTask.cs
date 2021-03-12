using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200006E RID: 110
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiQueryHomeMdbTask : BaseTask
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00008900 File Offset: 0x00006B00
		public NspiQueryHomeMdbTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiQueryHomeMDBTaskTitle, Strings.NspiQueryHomeMDBTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.NspiMinimalIds.SetOnly()
		})
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008A30 File Offset: 0x00006C30
		protected override IEnumerator<ITask> Process()
		{
			base.Set<int[]>(ContextPropertySchema.NspiMinimalIds, null);
			BaseTask compositeTask = new CompositeTask(base.CreateDerivedContext(), new ITask[]
			{
				new NspiGetMatchesTask(this.nspiClient, base.CreateDerivedContext()),
				new NspiQueryRowsTask(this.nspiClient, base.CreateDerivedContext())
			});
			yield return compositeTask;
			base.Result = compositeTask.Result;
			yield break;
		}

		// Token: 0x04000150 RID: 336
		private readonly INspiClient nspiClient;
	}
}
