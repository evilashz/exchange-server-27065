using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000065 RID: 101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbTask : BaseTask
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00007D78 File Offset: 0x00005F78
		public EmsmdbTask(IContext context) : this(context, true)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007D84 File Offset: 0x00005F84
		public EmsmdbTask(IContext context, bool shouldCallLogon) : base(context, Strings.EmsmdbTaskTitle, Strings.EmsmdbTaskDescription, TaskType.Knowledge, RpcHelper.DependenciesOfBuildCompleteBindingInfo.Concat(new ContextProperty[]
		{
			ContextPropertySchema.ActualBinding.SetOnly()
		}))
		{
			this.shouldCallLogon = shouldCallLogon;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007DC9 File Offset: 0x00005FC9
		public override BaseTask Copy()
		{
			return new EmsmdbTask(base.CreateContextCopy(), this.shouldCallLogon);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007FEC File Offset: 0x000061EC
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
				IContext context = derivedContext;
				ITask[] array = new ITask[2];
				array[0] = new EmsmdbConnectTask(emsmdbClient, derivedContext);
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
					task2 = new EmsmdbLogonTask(emsmdbClient, derivedContext);
				}
				array2[num] = task2;
				BaseTask composite = new CompositeTask(context, array);
				yield return composite;
				base.Result = composite.Result;
			}
			yield break;
		}

		// Token: 0x04000141 RID: 321
		private readonly bool shouldCallLogon;
	}
}
