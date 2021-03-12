using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.GroupMailbox.Consistency;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcAssociationReplicator : IAssociationReplicator
	{
		// Token: 0x06000184 RID: 388 RVA: 0x0000BC52 File Offset: 0x00009E52
		public RpcAssociationReplicator(IExtensibleLogger logger, string replicationServerFqdn)
		{
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.Logger = logger;
			this.replicationAssistantInvoker = new ReplicationAssistantInvoker((!string.IsNullOrWhiteSpace(replicationServerFqdn)) ? replicationServerFqdn : LocalServerCache.LocalServerFqdn, this.Logger);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BC90 File Offset: 0x00009E90
		public bool ReplicateAssociation(IAssociationAdaptor masterAdaptor, params MailboxAssociation[] associations)
		{
			ArgumentValidator.ThrowIfNull("associations", associations);
			ArgumentValidator.ThrowIfZeroOrNegative("associations.Length", associations.Length);
			RpcAssociationReplicator.Tracer.TraceDebug((long)this.GetHashCode(), "RpcAssociationReplicator::ReplicateAssociations");
			try
			{
				return this.replicationAssistantInvoker.Invoke("RpcAssociationReplicator", masterAdaptor, associations);
			}
			catch (GrayException ex)
			{
				this.LogError(ex.ToString());
				masterAdaptor.AssociationStore.SaveMailboxAsOutOfSync();
			}
			return false;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BD10 File Offset: 0x00009F10
		private void LogError(string errorDescription)
		{
			RpcAssociationReplicator.Tracer.TraceError((long)this.GetHashCode(), errorDescription);
			this.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Error>
			{
				{
					MailboxAssociationLogSchema.Error.Context,
					"RpcAssociationReplicator"
				},
				{
					MailboxAssociationLogSchema.Error.Exception,
					errorDescription
				}
			});
		}

		// Token: 0x040000C4 RID: 196
		protected readonly IExtensibleLogger Logger;

		// Token: 0x040000C5 RID: 197
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationTracer;

		// Token: 0x040000C6 RID: 198
		private readonly ReplicationAssistantInvoker replicationAssistantInvoker;
	}
}
