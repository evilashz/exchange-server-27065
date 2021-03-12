using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000463 RID: 1123
	[Cmdlet("Get", "MailboxAssociationReplicationState", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxAssociationReplicationState : GetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x0009C8C4 File Offset: 0x0009AAC4
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x0009C8CC File Offset: 0x0009AACC
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

		// Token: 0x0600279C RID: 10140 RVA: 0x0009C8D8 File Offset: 0x0009AAD8
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser aduser = (ADUser)dataObject;
			MailboxAssociationReplicationStatePresentationObject mailboxAssociationReplicationStatePresentationObject = new MailboxAssociationReplicationStatePresentationObject
			{
				Identity = aduser.Identity
			};
			if (CmdletProxy.TryToProxyOutputObject(mailboxAssociationReplicationStatePresentationObject, base.CurrentTaskContext, aduser, this.Identity == null, this.ConfirmationMessage, CmdletProxy.AppendIdentityToProxyCmdlet(aduser)))
			{
				return mailboxAssociationReplicationStatePresentationObject;
			}
			try
			{
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(aduser, null);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=Get-MailboxAssociationReplicationState"))
				{
					mailboxSession.Mailbox.Load(GetMailboxAssociationReplicationState.MailboxProperties);
					return new MailboxAssociationReplicationStatePresentationObject
					{
						Identity = aduser.Identity,
						NextReplicationTime = new ExDateTime?(mailboxSession.Mailbox.GetValueOrDefault<ExDateTime>(MailboxSchema.MailboxAssociationNextReplicationTime, ExDateTime.MinValue))
					};
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
			return null;
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x0009CA04 File Offset: 0x0009AC04
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StorageTransientException || exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x04001DA6 RID: 7590
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;

		// Token: 0x04001DA7 RID: 7591
		private static readonly PropertyDefinition[] MailboxProperties = new PropertyDefinition[]
		{
			MailboxSchema.MailboxAssociationNextReplicationTime
		};
	}
}
