using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200006D RID: 109
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetNetworkAddressesTask : BaseTask
	{
		// Token: 0x06000233 RID: 563 RVA: 0x000087B4 File Offset: 0x000069B4
		public NspiGetNetworkAddressesTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiGetNetworkAddressesTaskTitle, Strings.NspiGetNetworkAddressesTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.NspiMinimalIds.SetOnly()
		})
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000088E4 File Offset: 0x00006AE4
		protected override IEnumerator<ITask> Process()
		{
			base.Set<int[]>(ContextPropertySchema.NspiMinimalIds, null);
			BaseTask compositeTask = new CompositeTask(base.CreateDerivedContext(), new ITask[]
			{
				new NspiDNToEphTask(this.nspiClient, base.CreateDerivedContext()),
				new NspiGetNetworkAddressesPropertyTask(this.nspiClient, base.CreateDerivedContext())
			});
			yield return compositeTask;
			base.Result = compositeTask.Result;
			yield break;
		}

		// Token: 0x0400014F RID: 335
		private readonly INspiClient nspiClient;
	}
}
