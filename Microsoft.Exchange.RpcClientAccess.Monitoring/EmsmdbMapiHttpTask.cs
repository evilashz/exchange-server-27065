using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000064 RID: 100
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbMapiHttpTask : BaseTask
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00007B08 File Offset: 0x00005D08
		public EmsmdbMapiHttpTask(IContext context) : this(context, true)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007B14 File Offset: 0x00005D14
		public EmsmdbMapiHttpTask(IContext context, bool shouldCallLogon) : base(context, Strings.EmsmdbTaskTitle, Strings.EmsmdbTaskDescription, TaskType.Knowledge, RpcHelper.DependenciesOfBuildMapiHttpBindingInfo.Concat(new ContextProperty[]
		{
			ContextPropertySchema.ActualBinding.SetOnly()
		}))
		{
			this.shouldCallLogon = shouldCallLogon;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007B59 File Offset: 0x00005D59
		public override BaseTask Copy()
		{
			return new EmsmdbMapiHttpTask(base.CreateContextCopy(), this.shouldCallLogon);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007D5C File Offset: 0x00005F5C
		protected override IEnumerator<ITask> Process()
		{
			using (IEmsmdbClient emsmdbHttpClient = base.Environment.CreateEmsmdbClient(RpcHelper.BuildCompleteMapiHttpBindingInfo(base.Properties)))
			{
				IContext derivedContext = base.CreateDerivedContext();
				if (!derivedContext.Properties.IsPropertyFound(ContextPropertySchema.MailboxLegacyDN))
				{
					derivedContext.Properties.Set(ContextPropertySchema.MailboxLegacyDN, derivedContext.Properties.Get(ContextPropertySchema.UserLegacyDN));
				}
				IContext context = derivedContext;
				ITask[] array = new ITask[2];
				array[0] = new EmsmdbConnectTask(emsmdbHttpClient, derivedContext);
				ITask[] array2 = array;
				int num = 1;
				ITask task2;
				if (!this.shouldCallLogon)
				{
					ITask task = new NullTask();
					task2 = task;
				}
				else
				{
					task2 = new EmsmdbLogonTask(emsmdbHttpClient, derivedContext);
				}
				array2[num] = task2;
				BaseTask composite = new CompositeTask(context, array);
				yield return composite;
				base.Result = composite.Result;
			}
			yield break;
		}

		// Token: 0x04000140 RID: 320
		private readonly bool shouldCallLogon;
	}
}
