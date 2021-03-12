using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200006C RID: 108
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetNetworkAddressesPropertyTask : BaseNspiRpcTask
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00008620 File Offset: 0x00006820
		public NspiGetNetworkAddressesPropertyTask(INspiClient nspiClient, IContext context) : base(context, Strings.NspiGetNetworkAddressesPropertyTaskTitle, Strings.NspiGetNetworkAddressesPropertyTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			ContextPropertySchema.NspiMinimalIds.GetOnly()
		})
		{
			this.nspiClient = nspiClient;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008798 File Offset: 0x00006998
		protected override IEnumerator<ITask> Process()
		{
			string[] networkAddresses = null;
			AsyncTask asyncGetNetworkAddressesPropertyTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.nspiClient.BeginGetNetworkAddresses(base.Get<int[]>(ContextPropertySchema.NspiMinimalIds), base.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), (IAsyncResult asyncResult) => this.ApplyCallResult(this.nspiClient.EndGetNetworkAddresses(asyncResult, out networkAddresses)));
			yield return asyncGetNetworkAddressesPropertyTask;
			base.Result = asyncGetNetworkAddressesPropertyTask.Result;
			yield break;
		}

		// Token: 0x0400014E RID: 334
		private readonly INspiClient nspiClient;
	}
}
