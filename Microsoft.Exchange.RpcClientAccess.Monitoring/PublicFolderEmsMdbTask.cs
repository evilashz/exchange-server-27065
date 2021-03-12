using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000074 RID: 116
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderEmsMdbTask : BaseTask
	{
		// Token: 0x06000248 RID: 584 RVA: 0x000092D0 File Offset: 0x000074D0
		public PublicFolderEmsMdbTask(IContext context) : base(context, Strings.PFEmsmdbTaskTitle, Strings.PFEmsmdbTaskDescription, TaskType.Knowledge, RpcHelper.DependenciesOfBuildCompleteBindingInfo.Concat(new ContextProperty[]
		{
			ContextPropertySchema.ActualBinding.SetOnly()
		}))
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009508 File Offset: 0x00007708
		protected override IEnumerator<ITask> Process()
		{
			using (IEmsmdbClient emsmdbClient = base.Environment.CreateEmsmdbClient(RpcHelper.BuildCompleteBindingInfo(base.Properties, 6001)))
			{
				base.Set<string>(ContextPropertySchema.ActualBinding, emsmdbClient.BindingString);
				IContext derivedContext = base.CreateDerivedContext();
				if (!derivedContext.Properties.IsPropertyFound(ContextPropertySchema.MailboxLegacyDN))
				{
					derivedContext.Properties.Set(ContextPropertySchema.MailboxLegacyDN, derivedContext.Properties.Get(ContextPropertySchema.UserLegacyDN));
				}
				BaseTask composite = new CompositeTask(derivedContext, new ITask[]
				{
					new EmsmdbConnectTask(emsmdbClient, derivedContext),
					new PublicFolderMailboxLogonTask(emsmdbClient, derivedContext)
				});
				yield return composite;
				base.Result = composite.Result;
			}
			yield break;
		}
	}
}
