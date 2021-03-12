using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200007A RID: 122
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RfriTask : BaseTask
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00009EFC File Offset: 0x000080FC
		public RfriTask(IContext context) : base(context, Strings.RfriTaskTitle, Strings.RfriTaskDescription, TaskType.Knowledge, RpcHelper.DependenciesOfBuildCompleteBindingInfo.Concat(new ContextProperty[]
		{
			ContextPropertySchema.ActualBinding.SetOnly()
		}))
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A0F4 File Offset: 0x000082F4
		protected override IEnumerator<ITask> Process()
		{
			using (IRfriClient rfriClient = base.Environment.CreateRfriClient(RpcHelper.BuildCompleteBindingInfo(base.Properties, 6002)))
			{
				base.Set<string>(ContextPropertySchema.ActualBinding, rfriClient.BindingString);
				BaseTask composite = new CompositeTask(base.CreateDerivedContext(), new ITask[]
				{
					new RfriGetNewDsaTask(rfriClient, base.CreateDerivedContext()),
					new RfriGetFqdnTask(rfriClient, base.CreateDerivedContext())
				});
				yield return composite;
				base.Result = composite.Result;
			}
			yield break;
		}
	}
}
