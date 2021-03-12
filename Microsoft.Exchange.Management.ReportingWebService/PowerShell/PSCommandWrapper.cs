using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x02000011 RID: 17
	internal class PSCommandWrapper : IPSCommandWrapper
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002E41 File Offset: 0x00001041
		public PSCommandWrapper() : this(new PSCommand())
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E4E File Offset: 0x0000104E
		public PSCommandWrapper(PSCommand command)
		{
			this.command = command;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002E5D File Offset: 0x0000105D
		public CommandCollection Commands
		{
			get
			{
				return this.command.Commands;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E6A File Offset: 0x0000106A
		public PowerShellResults Invoke(RunspaceMediator runspaceMediator)
		{
			return this.command.Invoke(runspaceMediator);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E78 File Offset: 0x00001078
		public IPSCommandWrapper AddCommand(string commandText)
		{
			return new PSCommandWrapper(this.command.AddCommand(commandText));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E8B File Offset: 0x0000108B
		public IPSCommandWrapper AddCommand(Command command)
		{
			return new PSCommandWrapper(this.command.AddCommand(command));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E9E File Offset: 0x0000109E
		public IPSCommandWrapper AddParameter(string name)
		{
			return new PSCommandWrapper(this.command.AddParameter(name));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002EB1 File Offset: 0x000010B1
		public IPSCommandWrapper AddParameter(string name, object value)
		{
			return new PSCommandWrapper(this.command.AddParameter(name, value));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EC5 File Offset: 0x000010C5
		public override string ToString()
		{
			return this.command.ToString();
		}

		// Token: 0x04000036 RID: 54
		private PSCommand command;
	}
}
