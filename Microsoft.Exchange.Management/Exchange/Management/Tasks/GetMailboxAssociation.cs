using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000462 RID: 1122
	[Cmdlet("Get", "MailboxAssociation", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxAssociation : GetRecipientObjectTask<MailboxAssociationIdParameter, ADUser>
	{
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x0009C5A9 File Offset: 0x0009A7A9
		// (set) Token: 0x06002792 RID: 10130 RVA: 0x0009C5B1 File Offset: 0x0009A7B1
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x0009C5BA File Offset: 0x0009A7BA
		// (set) Token: 0x06002794 RID: 10132 RVA: 0x0009C5EA File Offset: 0x0009A7EA
		[Parameter]
		public SwitchParameter IncludeNotPromotedProperties
		{
			get
			{
				if (base.Fields.IsChanged("IncludeNotPromotedProperties"))
				{
					return (SwitchParameter)base.Fields["IncludeNotPromotedProperties"];
				}
				return false;
			}
			set
			{
				base.Fields["IncludeNotPromotedProperties"] = value;
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0009C602 File Offset: 0x0009A802
		protected override bool IsKnownException(Exception exception)
		{
			return exception is FormatException || exception is StorageTransientException || exception is StoragePermanentException || exception is AssociationNotFoundException || exception is MailboxNotFoundException || exception is ManagementObjectNotFoundException || base.IsKnownException(exception);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0009C640 File Offset: 0x0009A840
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			try
			{
				ADUser aduser = (ADUser)dataObject;
				MailboxAssociationPresentationObject mailboxAssociationPresentationObject = new MailboxAssociationPresentationObject();
				if (!CmdletProxy.TryToProxyOutputObject(mailboxAssociationPresentationObject, base.CurrentTaskContext, aduser, false, this.ConfirmationMessage, null))
				{
					IRecipientSession adSession = (IRecipientSession)base.DataSession;
					MailboxAssociationContext mailboxAssociationContext = new MailboxAssociationContext(adSession, aduser, "Get-MailboxAssociation", this.Identity, this.IncludeNotPromotedProperties);
					mailboxAssociationContext.Execute(new Action<MailboxAssociationFromStore, IAssociationAdaptor, ADUser, IExtensibleLogger>(this.WriteMailboxAssociation));
				}
				else
				{
					base.WriteResult(mailboxAssociationPresentationObject);
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
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0009C76C File Offset: 0x0009A96C
		private void WriteMailboxAssociation(MailboxAssociationFromStore association, IAssociationAdaptor associationAdaptor, ADUser masterMailbox, IExtensibleLogger logger)
		{
			if (association != null)
			{
				ObjectId objectId = new MailboxStoreObjectId(masterMailbox.ObjectId, association.ItemId.ObjectId);
				MailboxLocator slaveMailboxLocator = associationAdaptor.GetSlaveMailboxLocator(association);
				if (base.NeedSuppressingPiiData)
				{
					objectId = SuppressingPiiData.RedactMailboxStoreObjectId(objectId);
				}
				base.WriteResult(new MailboxAssociationPresentationObject
				{
					Identity = objectId,
					ExternalId = slaveMailboxLocator.ExternalId,
					LegacyDn = slaveMailboxLocator.LegacyDn,
					IsMember = association.IsMember,
					JoinedBy = association.JoinedBy,
					GroupSmtpAddress = association.GroupSmtpAddress,
					UserSmtpAddress = association.UserSmtpAddress,
					IsPin = association.IsPin,
					ShouldEscalate = association.ShouldEscalate,
					IsAutoSubscribed = association.IsAutoSubscribed,
					JoinDate = association.JoinDate,
					LastVisitedDate = association.LastVisitedDate,
					PinDate = association.PinDate,
					LastModified = association.LastModified,
					CurrentVersion = association.CurrentVersion,
					SyncedVersion = association.SyncedVersion,
					LastSyncError = association.LastSyncError,
					SyncAttempts = association.SyncAttempts,
					SyncedSchemaVersion = association.SyncedSchemaVersion
				});
				return;
			}
			GetMailboxAssociation.Tracer.TraceDebug((long)this.GetHashCode(), "GetMailboxAssocaition.WriteMailboxAssociation. Skipping null MailboxAssociationFromStore.");
		}

		// Token: 0x04001DA4 RID: 7588
		private const string IncludeNotPromotedPropertiesFieldName = "IncludeNotPromotedProperties";

		// Token: 0x04001DA5 RID: 7589
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;
	}
}
