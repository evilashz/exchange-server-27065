using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200046E RID: 1134
	[Cmdlet("Set", "MailboxAssociationReplicationState", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxAssociationReplicationState : SetRecipientObjectTask<MailboxIdParameter, MailboxAssociationReplicationStatePresentationObject, ADUser>
	{
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0009E081 File Offset: 0x0009C281
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x0009E089 File Offset: 0x0009C289
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x06002814 RID: 10260 RVA: 0x0009E094 File Offset: 0x0009C294
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurable configurable = base.PrepareDataObject();
			CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, (ADUser)configurable, false, this.ConfirmationMessage, null);
			return configurable;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0009E0C2 File Offset: 0x0009C2C2
		protected override void StampChangesOn(IConfigurable dataObject)
		{
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0009E0C4 File Offset: 0x0009C2C4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(this.DataObject, null);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=Set-MailboxAssociationReplicationState"))
				{
					if (this.Instance.NextReplicationTime != null)
					{
						LocalAssociationStore.SaveMailboxSyncStatus(mailboxSession, this.Instance.NextReplicationTime, null);
					}
				}
			}
			catch (StorageTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, this.Identity);
			}
			catch (StoragePermanentException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ReadError, this.Identity);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x04001DD2 RID: 7634
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;
	}
}
