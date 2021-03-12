using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D91 RID: 3473
	[Cmdlet("Remove", "MailboxRepairRequest", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMailboxRepairRequest : SetObjectWithIdentityTaskBase<StoreIntegrityCheckJobIdParameter, StoreIntegrityCheckJob, StoreIntegrityCheckJob>
	{
		// Token: 0x17002987 RID: 10631
		// (get) Token: 0x0600857D RID: 34173 RVA: 0x00221DDA File Offset: 0x0021FFDA
		// (set) Token: 0x0600857E RID: 34174 RVA: 0x00221DE2 File Offset: 0x0021FFE2
		[Parameter(Mandatory = false)]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x17002988 RID: 10632
		// (get) Token: 0x0600857F RID: 34175 RVA: 0x00221DEB File Offset: 0x0021FFEB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMailboxRepairRequestDatabase(this.Identity.ToString());
			}
		}

		// Token: 0x06008580 RID: 34176 RVA: 0x00221DFD File Offset: 0x0021FFFD
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || (exception is ServerUnavailableException || exception is RpcException || exception is InvalidIntegrityCheckJobIdentity);
		}

		// Token: 0x06008581 RID: 34177 RVA: 0x00221E28 File Offset: 0x00220028
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null);
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			sessionSettings = ADSessionSettings.RescopeToSubtree(sessionSettings);
			this.readOnlyConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 121, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\OnlineIsInteg\\RemoveMailboxRepairRequest.cs");
			TaskLogger.LogExit();
			return this.readOnlyConfigSession;
		}

		// Token: 0x06008582 RID: 34178 RVA: 0x00221EA2 File Offset: 0x002200A2
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x06008583 RID: 34179 RVA: 0x00221EB4 File Offset: 0x002200B4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			StoreIntegrityCheckJobIdentity storeIntegrityCheckJobIdentity = new StoreIntegrityCheckJobIdentity(this.Identity.RawIdentity);
			DatabaseIdParameter databaseIdParameter = new DatabaseIdParameter(new ADObjectId(storeIntegrityCheckJobIdentity.DatabaseGuid));
			this.jobGuid = storeIntegrityCheckJobIdentity.JobGuid;
			this.database = (Database)base.GetDataObject<Database>(databaseIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(databaseIdParameter.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(databaseIdParameter.ToString())));
			TaskLogger.LogExit();
		}

		// Token: 0x06008584 RID: 34180 RVA: 0x00221F32 File Offset: 0x00220132
		protected override IConfigurable PrepareDataObject()
		{
			return new StoreIntegrityCheckJob();
		}

		// Token: 0x06008585 RID: 34181 RVA: 0x00221F3C File Offset: 0x0022013C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			StoreIntegrityCheckAdminRpc.RemoveStoreIntegrityCheckJob(this.database, this.jobGuid, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			TaskLogger.LogExit();
		}

		// Token: 0x04004070 RID: 16496
		private const string ParameterDatabase = "Database";

		// Token: 0x04004071 RID: 16497
		private const string ParameterRequest = "Request";

		// Token: 0x04004072 RID: 16498
		private IConfigurationSession readOnlyConfigSession;

		// Token: 0x04004073 RID: 16499
		private Database database;

		// Token: 0x04004074 RID: 16500
		private Guid jobGuid;
	}
}
