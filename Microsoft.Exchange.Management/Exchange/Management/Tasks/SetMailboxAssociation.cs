using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200046D RID: 1133
	[Cmdlet("Set", "MailboxAssociation", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxAssociation : SetRecipientObjectTask<MailboxAssociationIdParameter, MailboxAssociationPresentationObject, ADUser>
	{
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x0009DD9E File Offset: 0x0009BF9E
		// (set) Token: 0x06002807 RID: 10247 RVA: 0x0009DDA6 File Offset: 0x0009BFA6
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override MailboxAssociationIdParameter Identity
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

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x0009DDAF File Offset: 0x0009BFAF
		// (set) Token: 0x06002809 RID: 10249 RVA: 0x0009DDDF File Offset: 0x0009BFDF
		[Parameter]
		public SwitchParameter UpdateSlavedData
		{
			get
			{
				if (base.Fields.IsChanged("UpdateSlaveData"))
				{
					return (SwitchParameter)base.Fields["UpdateSlaveData"];
				}
				return false;
			}
			set
			{
				base.Fields["UpdateSlaveData"] = value;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x0009DDF7 File Offset: 0x0009BFF7
		// (set) Token: 0x0600280B RID: 10251 RVA: 0x0009DE27 File Offset: 0x0009C027
		[Parameter]
		public SwitchParameter ReplicateMasteredData
		{
			get
			{
				if (base.Fields.IsChanged("ReplicateMasteredData"))
				{
					return (SwitchParameter)base.Fields["ReplicateMasteredData"];
				}
				return false;
			}
			set
			{
				base.Fields["ReplicateMasteredData"] = value;
			}
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x0009DE3F File Offset: 0x0009C03F
		protected override void StampChangesOn(IConfigurable dataObject)
		{
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0009DE41 File Offset: 0x0009C041
		protected override bool IsKnownException(Exception exception)
		{
			return exception is FormatException || exception is StorageTransientException || exception is StoragePermanentException || exception is AssociationNotFoundException || exception is MailboxNotFoundException || exception is RpcException || base.IsKnownException(exception);
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0009DE7C File Offset: 0x0009C07C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, this.DataObject, false, this.ConfirmationMessage, null);
			try
			{
				ADUser dataObject = this.DataObject;
				if (dataObject == null)
				{
					base.WriteError(new ObjectNotFoundException(Strings.MailboxAssociationMailboxNotFound), ExchangeErrorCategory.Client, this.DataObject);
				}
				else
				{
					IRecipientSession adSession = (IRecipientSession)base.DataSession;
					MailboxAssociationContext mailboxAssociationContext = new MailboxAssociationContext(adSession, dataObject, "Set-MailboxAssociation", this.Identity, false);
					mailboxAssociationContext.Execute(new Action<MailboxAssociationFromStore, IAssociationAdaptor, ADUser, IExtensibleLogger>(this.UpdateMailboxAssociation));
				}
			}
			catch (StorageTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, this.Identity.MailboxId);
			}
			catch (StoragePermanentException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ReadError, this.Identity.MailboxId);
			}
			catch (AssociationNotFoundException exception3)
			{
				base.WriteError(exception3, ErrorCategory.ReadError, this.Identity.MailboxId);
			}
			catch (MailboxNotFoundException exception4)
			{
				base.WriteError(exception4, ErrorCategory.ReadError, this.Identity.MailboxId);
			}
			catch (RpcException exception5)
			{
				base.WriteError(exception5, ErrorCategory.ConnectionError, this.Identity.MailboxId);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0009DFD4 File Offset: 0x0009C1D4
		private void UpdateMailboxAssociation(MailboxAssociationFromStore association, IAssociationAdaptor associationAdaptor, ADUser masterMailbox, IExtensibleLogger logger)
		{
			if (association == null)
			{
				SetMailboxAssociation.Tracer.TraceDebug((long)this.GetHashCode(), "SetMailboxAssocaition.UpdateMailboxAssociation. Skipping null MailboxAssociationFromStore.");
				return;
			}
			bool flag = this.Instance.UpdateAssociation(association, associationAdaptor);
			associationAdaptor.SaveAssociation(association, this.ReplicateMasteredData);
			if (this.UpdateSlavedData)
			{
				associationAdaptor.ReplicateAssociation(association);
			}
			if (flag)
			{
				associationAdaptor.SaveSyncState(association);
			}
			if (this.ReplicateMasteredData)
			{
				RpcAssociationReplicator rpcAssociationReplicator = new RpcAssociationReplicator(logger, associationAdaptor.AssociationStore.ServerFullyQualifiedDomainName);
				rpcAssociationReplicator.ReplicateAssociation(associationAdaptor, new MailboxAssociation[]
				{
					association
				});
			}
		}

		// Token: 0x04001DCF RID: 7631
		private const string UpdateSlavedDataFieldName = "UpdateSlaveData";

		// Token: 0x04001DD0 RID: 7632
		private const string ReplicateMasteredDataFieldName = "ReplicateMasteredData";

		// Token: 0x04001DD1 RID: 7633
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;
	}
}
