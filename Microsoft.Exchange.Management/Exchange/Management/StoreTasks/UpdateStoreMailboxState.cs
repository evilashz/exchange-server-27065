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
	// Token: 0x020007A6 RID: 1958
	[Cmdlet("Update", "StoreMailboxState", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
	public sealed class UpdateStoreMailboxState : DataAccessTask<ADUser>
	{
		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x0011B910 File Offset: 0x00119B10
		// (set) Token: 0x06004501 RID: 17665 RVA: 0x0011B927 File Offset: 0x00119B27
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

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x0011B93A File Offset: 0x00119B3A
		// (set) Token: 0x06004503 RID: 17667 RVA: 0x0011B951 File Offset: 0x00119B51
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

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x0011B964 File Offset: 0x00119B64
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateStoreMailboxStateMailboxIdentity(this.Database.ToString(), this.Identity.ToString());
			}
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x0011B981 File Offset: 0x00119B81
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 92, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\Mailbox\\UpdateStoreMailboxState.cs");
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x0011B9A4 File Offset: 0x00119BA4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
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
				if (!Guid.TryParse(this.Identity.ToString(), out this.guidMailbox))
				{
					base.WriteError(new MdbAdminTaskException(Strings.ErrorInvalidGuidFormat), ErrorCategory.InvalidArgument, this.Identity.ToString());
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

		// Token: 0x06004507 RID: 17671 RVA: 0x0011BB68 File Offset: 0x00119D68
		protected override void InternalProvisioningValidation()
		{
			ProvisioningValidationError[] array = ProvisioningLayer.Validate(this, null);
			if (array != null && array.Length > 0)
			{
				foreach (ProvisioningValidationError provisioningValidationError in array)
				{
					ProvisioningValidationException exception = new ProvisioningValidationException(provisioningValidationError.Description, provisioningValidationError.AgentName, provisioningValidationError.Exception);
					this.WriteError(exception, (ErrorCategory)provisioningValidationError.ErrorCategory, null, false);
				}
			}
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x0011BBCC File Offset: 0x00119DCC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.VerboseUpdateStoreMailboxState(this.guidMailbox.ToString(), this.guidMdb.ToString()));
				}
				this.mapiSession.Administration.SyncMailboxWithDS(this.guidMdb, this.guidMailbox);
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

		// Token: 0x06004509 RID: 17673 RVA: 0x0011BC94 File Offset: 0x00119E94
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeObjects();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x0011BCA6 File Offset: 0x00119EA6
		private void DisposeObjects()
		{
			if (this.mapiSession != null)
			{
				this.mapiSession.Dispose();
				this.mapiSession = null;
			}
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x0011BCC2 File Offset: 0x00119EC2
		protected override void InternalEndProcessing()
		{
			this.DisposeObjects();
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x0011BCCA File Offset: 0x00119ECA
		protected override void InternalStopProcessing()
		{
			this.DisposeObjects();
		}

		// Token: 0x04002A9F RID: 10911
		private const string ParameterDatabase = "Database";

		// Token: 0x04002AA0 RID: 10912
		private const string ParameterStoreMailboxIdentity = "Identity";

		// Token: 0x04002AA1 RID: 10913
		private MapiAdministrationSession mapiSession;

		// Token: 0x04002AA2 RID: 10914
		private Guid guidMdb = Guid.Empty;

		// Token: 0x04002AA3 RID: 10915
		private Guid guidMailbox = Guid.Empty;
	}
}
