using System;
using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000141 RID: 321
	public class PSCommandWrapper : IPSCommandWrapper
	{
		// Token: 0x06002127 RID: 8487 RVA: 0x00064290 File Offset: 0x00062490
		public PSCommandWrapper(PSCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this.command = command;
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x000642AD File Offset: 0x000624AD
		public CommandCollection Commands
		{
			get
			{
				return this.command.Commands;
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000642BA File Offset: 0x000624BA
		public PowerShellResults<O> Invoke<O>(RunspaceMediator runspaceMediator, IEnumerable pipelineInput, WebServiceParameters parameters, CmdletActivity activity, bool isGetListAsync)
		{
			return this.command.Invoke(runspaceMediator, pipelineInput, parameters, activity, isGetListAsync);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000642CE File Offset: 0x000624CE
		public IPSCommandWrapper AddCommand(string commandText)
		{
			return new PSCommandWrapper(this.command.AddCommand(commandText));
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000642E1 File Offset: 0x000624E1
		public IPSCommandWrapper AddCommand(Command command)
		{
			return new PSCommandWrapper(this.command.AddCommand(command));
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000642F4 File Offset: 0x000624F4
		public IPSCommandWrapper AddParameter(string name)
		{
			return new PSCommandWrapper(this.command.AddParameter(name));
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00064307 File Offset: 0x00062507
		public IPSCommandWrapper AddParameter(string name, object value)
		{
			return new PSCommandWrapper(this.command.AddParameter(name, value));
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0006431B File Offset: 0x0006251B
		public override string ToString()
		{
			return this.command.ToTraceString();
		}

		// Token: 0x04001D11 RID: 7441
		private PSCommand command;
	}
}
