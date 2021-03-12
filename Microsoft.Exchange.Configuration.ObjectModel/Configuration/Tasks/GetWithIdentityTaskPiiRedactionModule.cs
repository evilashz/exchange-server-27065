using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200027D RID: 637
	internal class GetWithIdentityTaskPiiRedactionModule : GetTaskPiiRedactionModule
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x00051E0E File Offset: 0x0005000E
		public GetWithIdentityTaskPiiRedactionModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00051E17 File Offset: 0x00050017
		public override void Init(ITaskEvent task)
		{
			base.Init(task);
			if (base.CurrentTaskContext.ExchangeRunspaceConfig != null && base.CurrentTaskContext.ExchangeRunspaceConfig.NeedSuppressingPiiData)
			{
				task.PreIterate += this.Task_PreIterate;
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00051E54 File Offset: 0x00050054
		private void Task_PreIterate(object sender, EventArgs e)
		{
			ADIdParameter adidParameter = base.CurrentTaskContext.InvocationInfo.Fields["Identity"] as ADIdParameter;
			if (adidParameter != null && adidParameter.HasRedactedPiiData)
			{
				if (adidParameter.IsRedactedPiiResolved)
				{
					base.CurrentTaskContext.CommandShell.WriteDebug(new LocalizedString("Redacted PII value in the Identity has been resolved."));
					return;
				}
				if (base.CurrentTaskContext.ExchangeRunspaceConfig != null && base.CurrentTaskContext.ExchangeRunspaceConfig.EnablePiiMap)
				{
					base.CurrentTaskContext.CommandShell.WriteWarning(new LocalizedString("The Identity looks like containing redacted PII value. Unfortunately it failed to resolve the redacted PII value in the Identity."));
				}
			}
		}
	}
}
