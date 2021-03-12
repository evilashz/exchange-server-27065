using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000075 RID: 117
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderMailboxLogonTask : BaseRpcTask
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00009524 File Offset: 0x00007724
		public PublicFolderMailboxLogonTask(IEmsmdbClient emsmdbClient, IContext context) : base(context, Strings.PFEmsmdbTaskTitle, Strings.PFEmsmdbTaskDescription, TaskType.Operation, new ContextProperty[]
		{
			context.Properties.IsPropertyFound(ContextPropertySchema.MailboxLegacyDN) ? ContextPropertySchema.MailboxLegacyDN.GetOnly() : null
		})
		{
			this.emsmdbClient = emsmdbClient;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000096DC File Offset: 0x000078DC
		protected override IEnumerator<ITask> Process()
		{
			string mbxLegDn = null;
			try
			{
				mbxLegDn = base.Get<string>(ContextPropertySchema.MailboxLegacyDN);
			}
			catch (Exception)
			{
			}
			AsyncTask asyncTask = new AsyncTask(base.CreateDerivedContext(), (AsyncCallback asyncCallback, object asyncState) => this.emsmdbClient.BeginPublicLogon(mbxLegDn, this.Get<TimeSpan>(BaseTask.Timeout), asyncCallback, asyncState), (IAsyncResult asyncResult) => base.ApplyCallResult(this.emsmdbClient.EndPublicLogon(asyncResult)));
			yield return asyncTask;
			base.Result = asyncTask.Result;
			yield break;
		}

		// Token: 0x04000155 RID: 341
		private readonly IEmsmdbClient emsmdbClient;
	}
}
