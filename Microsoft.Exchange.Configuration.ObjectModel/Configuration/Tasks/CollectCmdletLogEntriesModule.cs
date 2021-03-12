using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000276 RID: 630
	internal class CollectCmdletLogEntriesModule : TaskIOPipelineBase, ITaskModule, ICriticalFeature
	{
		// Token: 0x060015C3 RID: 5571 RVA: 0x000512FE File Offset: 0x0004F4FE
		public CollectCmdletLogEntriesModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0005130D File Offset: 0x0004F50D
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00051310 File Offset: 0x0004F510
		public void Init(ITaskEvent task)
		{
			bool? flag = (bool?)this.context.InvocationInfo.Fields["CmdletLogEntriesEnabled"];
			if (flag != null && flag.Value)
			{
				ExchangePropertyContainer.EnableCmdletLog(this.context.SessionState);
			}
			if ((flag != null && flag.Value) || ExchangePropertyContainer.IsCmdletLogEnabled(this.context.SessionState))
			{
				task.PreInit += this.Task_PreInit;
				task.Stop += this.DisableCmdletLog;
				task.Release += this.DisableCmdletLog;
				this.context.CommandShell.PrependTaskIOPipelineHandler(this);
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000513CA File Offset: 0x0004F5CA
		private void Task_PreInit(object sender, EventArgs e)
		{
			ExchangePropertyContainer.EnableCmdletLog(this.context.SessionState);
			this.cmdletLogEntries = ExchangePropertyContainer.GetCmdletLogEntries(this.context.SessionState);
			this.context.InvocationInfo.IsVerboseOn = true;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00051403 File Offset: 0x0004F603
		private void DisableCmdletLog(object sender, EventArgs e)
		{
			if (this.context == null)
			{
				return;
			}
			if (ExchangePropertyContainer.IsCmdletLogEnabled(this.context.SessionState))
			{
				ExchangePropertyContainer.DisableCmdletLog(this.context.SessionState);
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00051430 File Offset: 0x0004F630
		public void Dispose()
		{
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00051432 File Offset: 0x0004F632
		public override bool WriteVerbose(LocalizedString input, out LocalizedString output)
		{
			if (this.cmdletLogEntries != null)
			{
				this.cmdletLogEntries.AddEntry(input);
			}
			output = input;
			return true;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00051458 File Offset: 0x0004F658
		public override bool WriteWarning(LocalizedString input, string helpUrl, out LocalizedString output)
		{
			if (this.cmdletLogEntries != null)
			{
				this.cmdletLogEntries.AddEntry(new LocalizedString(string.Format("{0} ({1})", input, Strings.LogHelpUrl(helpUrl))));
			}
			output = input;
			return true;
		}

		// Token: 0x040006AC RID: 1708
		private TaskContext context;

		// Token: 0x040006AD RID: 1709
		private CmdletLogEntries cmdletLogEntries;
	}
}
