using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007A2 RID: 1954
	[Cmdlet("Remove", "StoreMailbox", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveStoreMailbox : DataAccessTask<ADUser>
	{
		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x060044D1 RID: 17617 RVA: 0x0011A905 File Offset: 0x00118B05
		// (set) Token: 0x060044D2 RID: 17618 RVA: 0x0011A91C File Offset: 0x00118B1C
		[Parameter(Mandatory = true, ParameterSetName = "Identity")]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x060044D3 RID: 17619 RVA: 0x0011A92F File Offset: 0x00118B2F
		// (set) Token: 0x060044D4 RID: 17620 RVA: 0x0011A946 File Offset: 0x00118B46
		[Parameter(Mandatory = true, ParameterSetName = "Identity")]
		public StoreMailboxIdParameter Identity
		{
			get
			{
				return (StoreMailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x060044D5 RID: 17621 RVA: 0x0011A959 File Offset: 0x00118B59
		// (set) Token: 0x060044D6 RID: 17622 RVA: 0x0011A970 File Offset: 0x00118B70
		[Parameter(Mandatory = true, ParameterSetName = "Identity")]
		public MailboxStateParameter MailboxState
		{
			get
			{
				return (MailboxStateParameter)base.Fields["MailboxState"];
			}
			set
			{
				base.Fields["MailboxState"] = value;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x060044D7 RID: 17623 RVA: 0x0011A988 File Offset: 0x00118B88
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMailboxStoreMailboxIdentity(this.Database.ToString(), this.Identity.ToString());
			}
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x0011A9A5 File Offset: 0x00118BA5
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 121, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\Mailbox\\RemoveStoreMailbox.cs");
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x0011A9C8 File Offset: 0x00118BC8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				MailboxState? mailboxState = null;
				Database database = null;
				database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, base.DataSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Database.ToString())), ExchangeErrorCategory.Client);
				if (database.Recovery)
				{
					base.WriteError(new TaskArgumentException(Strings.ErrorInvalidOperationOnRecoveryMailboxDatabase(this.Database.ToString())), ExchangeErrorCategory.Client, this.Identity);
				}
				DatabaseLocationInfo databaseLocationInfo = null;
				try
				{
					databaseLocationInfo = ActiveManager.GetActiveManagerInstance().GetServerForDatabase(database.Id.ObjectGuid);
				}
				catch (ObjectNotFoundException exception)
				{
					base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
				}
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseConnectionAdminRpcInterface(databaseLocationInfo.ServerFqdn));
				}
				this.mapiSession = new MapiAdministrationSession(databaseLocationInfo.ServerLegacyDN, Fqdn.Parse(databaseLocationInfo.ServerFqdn));
				this.guidMdb = database.Guid;
				this.Identity.Flags |= 1UL;
				this.mailboxStatistics = (MailboxStatistics)base.GetDataObject<MailboxStatistics>(this.Identity, this.mapiSession, MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(database), new LocalizedString?(Strings.ErrorStoreMailboxNotFound(this.Identity.ToString(), this.Database.ToString())), new LocalizedString?(Strings.ErrorStoreMailboxNotUnique(this.Identity.ToString(), this.Database.ToString())));
				this.guidMailbox = this.mailboxStatistics.MailboxGuid;
				mailboxState = this.mailboxStatistics.DisconnectReason;
				if (mailboxState == null)
				{
					this.mapiSession.Administration.SyncMailboxWithDS(this.guidMdb, this.guidMailbox);
					this.mailboxStatistics.Dispose();
					this.mailboxStatistics = null;
					this.mailboxStatistics = (MailboxStatistics)base.GetDataObject<MailboxStatistics>(this.Identity, this.mapiSession, MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(database), new LocalizedString?(Strings.ErrorStoreMailboxNotFound(this.Identity.ToString(), this.Database.ToString())), new LocalizedString?(Strings.ErrorStoreMailboxNotUnique(this.Identity.ToString(), this.Database.ToString())));
					mailboxState = this.mailboxStatistics.DisconnectReason;
					if (mailboxState == null)
					{
						base.WriteError(new RemoveNotDisconnectedStoreMailboxPermanentException(this.Identity.ToString()), ErrorCategory.InvalidArgument, this.Identity);
					}
				}
				if (MailboxStateParameter.SoftDeleted == this.MailboxState && Microsoft.Exchange.Data.Mapi.MailboxState.SoftDeleted == mailboxState)
				{
					this.deleteMailboxFlags = 18;
				}
				else if (MailboxStateParameter.Disabled == this.MailboxState && Microsoft.Exchange.Data.Mapi.MailboxState.Disabled == mailboxState)
				{
					this.deleteMailboxFlags = 2;
				}
				else
				{
					base.WriteError(new UnexpectedRemoveStoreMailboxStatePermanentException(this.Identity.ToString(), mailboxState.ToString(), this.MailboxState.ToString()), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
			catch (MapiPermanentException exception2)
			{
				base.WriteError(exception2, ExchangeErrorCategory.ServerOperation, this.Identity);
			}
			catch (MapiRetryableException exception3)
			{
				base.WriteError(exception3, ExchangeErrorCategory.ServerTransient, this.Identity);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x0011AD58 File Offset: 0x00118F58
		protected override void InternalProvisioningValidation()
		{
			ProvisioningValidationError[] array = ProvisioningLayer.Validate(this, this.ConvertDataObjectToPresentationObject(this.mailboxStatistics));
			if (array != null && array.Length > 0)
			{
				foreach (ProvisioningValidationError provisioningValidationError in array)
				{
					ProvisioningValidationException exception = new ProvisioningValidationException(provisioningValidationError.Description, provisioningValidationError.AgentName, provisioningValidationError.Exception);
					this.WriteError(exception, (ErrorCategory)provisioningValidationError.ErrorCategory, null, false);
				}
			}
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x0011ADC8 File Offset: 0x00118FC8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseDeleteMailboxInStore(this.guidMailbox.ToString(), this.guidMdb.ToString()));
				}
				this.mapiSession.Administration.DeletePrivateMailbox(this.guidMdb, this.guidMailbox, this.deleteMailboxFlags);
			}
			catch (MapiPermanentException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerOperation, this.Identity);
			}
			catch (MapiRetryableException exception2)
			{
				base.WriteError(exception2, ExchangeErrorCategory.ServerTransient, this.Identity);
			}
			finally
			{
				this.DisposeObjects();
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x0011AE94 File Offset: 0x00119094
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeObjects();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x0011AEA6 File Offset: 0x001190A6
		private void DisposeObjects()
		{
			if (this.mapiSession != null)
			{
				this.mapiSession.Dispose();
				this.mapiSession = null;
			}
			if (this.mailboxStatistics != null)
			{
				this.mailboxStatistics.Dispose();
				this.mailboxStatistics = null;
			}
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x0011AEDC File Offset: 0x001190DC
		protected override void InternalEndProcessing()
		{
			this.DisposeObjects();
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x0011AEE4 File Offset: 0x001190E4
		protected override void InternalStopProcessing()
		{
			this.DisposeObjects();
		}

		// Token: 0x04002A88 RID: 10888
		private const string ParameterDatabase = "Database";

		// Token: 0x04002A89 RID: 10889
		private const string ParameterStoreMailboxIdentity = "Identity";

		// Token: 0x04002A8A RID: 10890
		private const string ParameterStoreMailboxState = "MailboxState";

		// Token: 0x04002A8B RID: 10891
		private MapiAdministrationSession mapiSession;

		// Token: 0x04002A8C RID: 10892
		private MailboxStatistics mailboxStatistics;

		// Token: 0x04002A8D RID: 10893
		private Guid guidMdb = Guid.Empty;

		// Token: 0x04002A8E RID: 10894
		private Guid guidMailbox = Guid.Empty;

		// Token: 0x04002A8F RID: 10895
		private int deleteMailboxFlags;
	}
}
