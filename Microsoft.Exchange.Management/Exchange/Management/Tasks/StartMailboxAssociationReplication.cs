using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Assistants;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200046F RID: 1135
	[Cmdlet("Start", "MailboxAssociationReplication", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
	public sealed class StartMailboxAssociationReplication : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x0009E1A8 File Offset: 0x0009C3A8
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x0009E1B0 File Offset: 0x0009C3B0
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0009E1B9 File Offset: 0x0009C3B9
		protected override bool IsKnownException(Exception exception)
		{
			return exception is FormatException || exception is StorageTransientException || exception is StoragePermanentException || exception is MailboxNotFoundException || exception is RpcException || exception is RpcClientException || base.IsKnownException(exception);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0009E1F4 File Offset: 0x0009C3F4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ADUser dataObject = this.DataObject;
				if (dataObject == null)
				{
					base.WriteError(new ObjectNotFoundException(Strings.MailboxAssociationMailboxNotFound), ExchangeErrorCategory.Client, this.DataObject);
				}
				else
				{
					int num;
					string remoteServerForADUser = TaskHelper.GetRemoteServerForADUser(dataObject, new Task.TaskVerboseLoggingDelegate(base.CurrentTaskContext.CommandShell.WriteVerbose), out num);
					base.WriteVerbose(Strings.MailboxAssociationReplicationRpcRequest(dataObject.Id.ToString(), remoteServerForADUser));
					using (AssistantsRpcClient assistantsRpcClient = new AssistantsRpcClient(remoteServerForADUser))
					{
						assistantsRpcClient.StartWithParams(StartMailboxAssociationReplication.MailboxAssociationReplicationAssistantName, dataObject.ExchangeGuid, dataObject.Database.ObjectGuid, string.Empty);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x04001DD3 RID: 7635
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;

		// Token: 0x04001DD4 RID: 7636
		private static readonly string MailboxAssociationReplicationAssistantName = "MailboxAssociationReplicationAssistant";
	}
}
