using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000470 RID: 1136
	[Cmdlet("Remove", "MailboxAssociation", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMailboxAssociation : RemoveRecipientObjectTask<MailboxAssociationIdParameter, ADUser>
	{
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x0009E2E6 File Offset: 0x0009C4E6
		// (set) Token: 0x06002820 RID: 10272 RVA: 0x0009E2EE File Offset: 0x0009C4EE
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

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x0009E2F7 File Offset: 0x0009C4F7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMailboxAssociation(this.Identity.ToString());
			}
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0009E309 File Offset: 0x0009C509
		protected override void InternalValidate()
		{
			if (this.Identity.AssociationIdType == null)
			{
				base.WriteError(new MailboxAssociationInvalidOperationException(Strings.NoMailboxAssociationIdentityProvided), ExchangeErrorCategory.Client, this.Identity);
			}
			base.InternalValidate();
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x0009E339 File Offset: 0x0009C539
		protected override bool IsKnownException(Exception exception)
		{
			return exception is FormatException || exception is StorageTransientException || exception is StoragePermanentException || exception is AssociationNotFoundException || exception is MailboxNotFoundException || base.IsKnownException(exception);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0009E36C File Offset: 0x0009C56C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, base.DataObject, false, this.ConfirmationMessage, null);
			try
			{
				ADUser dataObject = base.DataObject;
				if (dataObject == null)
				{
					base.WriteError(new ObjectNotFoundException(Strings.MailboxAssociationMailboxNotFound), ExchangeErrorCategory.Client, base.DataObject);
				}
				else
				{
					IRecipientSession adSession = (IRecipientSession)base.DataSession;
					MailboxAssociationContext mailboxAssociationContext = new MailboxAssociationContext(adSession, dataObject, "Remove-MailboxAssociation", this.Identity, false);
					mailboxAssociationContext.Execute(new Action<MailboxAssociationFromStore, IAssociationAdaptor, ADUser, IExtensibleLogger>(this.DeleteMailboxAssociation));
				}
			}
			catch (StorageTransientException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerTransient, this.Identity.MailboxId);
			}
			catch (StoragePermanentException exception2)
			{
				base.WriteError(exception2, ExchangeErrorCategory.ServerOperation, this.Identity.MailboxId);
			}
			catch (AssociationNotFoundException exception3)
			{
				base.WriteError(exception3, ExchangeErrorCategory.ServerOperation, this.Identity.MailboxId);
			}
			catch (MailboxNotFoundException exception4)
			{
				base.WriteError(exception4, ExchangeErrorCategory.ServerOperation, this.Identity.MailboxId);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0009E4AC File Offset: 0x0009C6AC
		private void DeleteMailboxAssociation(MailboxAssociationFromStore association, IAssociationAdaptor associationAdaptor, ADUser masterMailbox, IExtensibleLogger logger)
		{
			if (association != null)
			{
				associationAdaptor.DeleteAssociation(association);
				return;
			}
			base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.AssociationIdValue, typeof(MailboxAssociationPresentationObject).ToString(), this.Identity.MailboxId.ToString())), ExchangeErrorCategory.Client, this.Identity);
		}

		// Token: 0x04001DD5 RID: 7637
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;
	}
}
