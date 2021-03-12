using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000069 RID: 105
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiDNToEphTask : BaseRpcTask
	{
		// Token: 0x06000226 RID: 550 RVA: 0x00008134 File Offset: 0x00006334
		public NspiDNToEphTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiDNToEphTaskTitle, Strings.NspiDNToEphTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.RpcServerLegacyDN.GetOnly(),
			ContextPropertySchema.NspiMinimalIds.SetOnly()
		})
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000082F0 File Offset: 0x000064F0
		protected override IEnumerator<ITask> Process()
		{
			int[] minimalIds = null;
			AsyncTask asyncDNToEphTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.nspiClient.BeginDNToEph(base.Get<string>(ContextPropertySchema.RpcServerLegacyDN), base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), (IAsyncResult asyncResult) => this.ApplyCallResult(this.nspiClient.EndDNToEph(asyncResult, out minimalIds)));
			yield return asyncDNToEphTask;
			base.Result = asyncDNToEphTask.Result;
			if (base.Result == TaskResult.Success && minimalIds != null)
			{
				base.Set<int[]>(ContextPropertySchema.NspiMinimalIds, minimalIds);
			}
			yield break;
		}

		// Token: 0x0400014B RID: 331
		private readonly INspiClient nspiClient;
	}
}
