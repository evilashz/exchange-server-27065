using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000D8 RID: 216
	internal class CmdletExecutionRequest<TCmdletResult> : BaseRequest
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x00013028 File Offset: 0x00011228
		protected CmdletExecutionRequest(string cmdletName, CmdletExecutionPool cmdletPool, ILogger logger)
		{
			this.cmdletPool = cmdletPool;
			this.logger = logger;
			this.Command = new PSCommand();
			this.Command.AddCommand(cmdletName);
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00013056 File Offset: 0x00011256
		public override Exception Exception
		{
			get
			{
				Exception result;
				if ((result = base.Exception) == null)
				{
					if (this.Error != null)
					{
						return this.Error.Exception;
					}
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00013077 File Offset: 0x00011277
		public override bool IsBlocked
		{
			get
			{
				return !this.cmdletPool.HasRunspacesAvailable;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00013087 File Offset: 0x00011287
		public ErrorRecord Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001308F File Offset: 0x0001128F
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x00013097 File Offset: 0x00011297
		public TCmdletResult Result { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x000130A0 File Offset: 0x000112A0
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x000130A8 File Offset: 0x000112A8
		private protected PSCommand Command { protected get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000130B4 File Offset: 0x000112B4
		protected string CommandString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Command command in this.Command.Commands)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("; ");
					}
					stringBuilder.Append(command.CommandText);
					foreach (CommandParameter commandParameter in command.Parameters)
					{
						stringBuilder.AppendFormat(" -{0}", commandParameter.Name);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001317C File Offset: 0x0001137C
		public override RequestDiagnosticData GetDiagnosticData(bool verbose)
		{
			CmdletRequestDiagnosticData cmdletRequestDiagnosticData = (CmdletRequestDiagnosticData)base.GetDiagnosticData(verbose);
			cmdletRequestDiagnosticData.ErrorRecord = this.error;
			cmdletRequestDiagnosticData.Exception = this.Exception;
			cmdletRequestDiagnosticData.Command = PowershellCommandDiagnosticData.FromPSCommand(this.Command, verbose);
			return cmdletRequestDiagnosticData;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000131C1 File Offset: 0x000113C1
		protected override RequestDiagnosticData CreateDiagnosticData()
		{
			return new CmdletRequestDiagnosticData();
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000131C8 File Offset: 0x000113C8
		protected override void ProcessRequest()
		{
			using (RunspaceReservation runspaceReservation = this.cmdletPool.AcquireRunspace())
			{
				this.logger.Log(MigrationEventType.Verbose, "About to execute powershell command {0}", new object[]
				{
					this.CommandString
				});
				this.Result = runspaceReservation.Runspace.RunPSCommandSingleOrDefault<TCmdletResult>(this.Command, out this.error);
				if (this.error != null && this.error.Exception != null)
				{
					this.error.Exception.PreserveExceptionStack();
				}
				this.logger.Log(MigrationEventType.Verbose, "Finished executing powershell request. Error: {0}", new object[]
				{
					this.error
				});
			}
		}

		// Token: 0x0400028C RID: 652
		private readonly CmdletExecutionPool cmdletPool;

		// Token: 0x0400028D RID: 653
		private readonly ILogger logger;

		// Token: 0x0400028E RID: 654
		private ErrorRecord error;
	}
}
