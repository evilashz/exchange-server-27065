using System;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Assistants;

namespace Microsoft.Exchange.Data.GroupMailbox.Consistency
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ReplicationAssistantInvoker : IReplicationAssistantInvoker
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000B66C File Offset: 0x0000986C
		public ReplicationAssistantInvoker(string targetServerFqdn, IExtensibleLogger logger)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("targetServerFqdn", targetServerFqdn);
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.targetServerFqdn = targetServerFqdn;
			this.logger = logger;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B7F0 File Offset: 0x000099F0
		public bool Invoke(string command, IAssociationAdaptor masterAdaptor, params MailboxAssociation[] associations)
		{
			ArgumentValidator.ThrowIfNull("masterAdaptor", masterAdaptor);
			ADUser masterMailbox = masterAdaptor.MasterLocator.FindAdUser();
			bool isRpcCallSuccessful = false;
			GrayException.MapAndReportGrayExceptions(delegate()
			{
				try
				{
					MailboxLocator[] array = associations.Select(new Func<MailboxAssociation, MailboxLocator>(masterAdaptor.GetSlaveMailboxLocator)).ToArray<MailboxLocator>();
					this.logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.CommandExecution>
					{
						{
							MailboxAssociationLogSchema.CommandExecution.Command,
							command
						},
						{
							MailboxAssociationLogSchema.CommandExecution.GroupMailbox,
							masterAdaptor.MasterLocator
						},
						{
							MailboxAssociationLogSchema.CommandExecution.UserMailboxes,
							array
						}
					});
					RpcAssociationReplicatorRunNowParameters rpcAssociationReplicatorRunNowParameters = new RpcAssociationReplicatorRunNowParameters
					{
						SlaveMailboxes = array
					};
					ReplicationAssistantInvoker.Tracer.TraceDebug<string, RpcAssociationReplicatorRunNowParameters>((long)this.GetHashCode(), "ReplicationAssistantInvoker::ReplicateAssociations. Calling RpcAssociationReplicator in '{0}' with parameter: '{1}'", this.targetServerFqdn, rpcAssociationReplicatorRunNowParameters);
					using (AssistantsRpcClient assistantsRpcClient = new AssistantsRpcClient(this.targetServerFqdn))
					{
						assistantsRpcClient.StartWithParams("MailboxAssociationReplicationAssistant", masterMailbox.ExchangeGuid, masterMailbox.Database.ObjectGuid, rpcAssociationReplicatorRunNowParameters.ToString());
					}
					isRpcCallSuccessful = true;
				}
				catch (RpcException ex)
				{
					this.LogError(Strings.RpcReplicationCallFailed(ex.ErrorCode));
					masterAdaptor.AssociationStore.SaveMailboxAsOutOfSync();
				}
			});
			return isRpcCallSuccessful;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B864 File Offset: 0x00009A64
		private void LogError(string errorDescription)
		{
			ReplicationAssistantInvoker.Tracer.TraceError((long)this.GetHashCode(), errorDescription);
			this.logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Error>
			{
				{
					MailboxAssociationLogSchema.Error.Context,
					"ReplicationAssistantInvoker"
				},
				{
					MailboxAssociationLogSchema.Error.Exception,
					errorDescription
				}
			});
		}

		// Token: 0x040000BB RID: 187
		private const string ReplicationAssistantName = "MailboxAssociationReplicationAssistant";

		// Token: 0x040000BC RID: 188
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationTracer;

		// Token: 0x040000BD RID: 189
		private readonly IExtensibleLogger logger;

		// Token: 0x040000BE RID: 190
		private readonly string targetServerFqdn;
	}
}
