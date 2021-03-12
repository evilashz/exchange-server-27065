using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000991 RID: 2449
	[Cmdlet("Remove", "MailboxDatabaseCopy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMailboxDatabaseCopy : RemoveSystemConfigurationObjectTask<DatabaseCopyIdParameter, DatabaseCopy>
	{
		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x06005780 RID: 22400 RVA: 0x0016CC78 File Offset: 0x0016AE78
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMailboxDatabaseCopy(this.m_database.Name, this.m_serverName);
			}
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x0016CC90 File Offset: 0x0016AE90
		protected override bool IsKnownException(Exception e)
		{
			return AmExceptionHelper.IsKnownClusterException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x0016CCA4 File Offset: 0x0016AEA4
		protected override void InternalStateReset()
		{
			this.m_database = null;
			this.m_server = null;
			this.m_serverName = null;
			this.m_databaseCopy = null;
			base.InternalStateReset();
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x0016CCC8 File Offset: 0x0016AEC8
		private void ValidateDatabaseCopy()
		{
			this.m_databaseCopy = base.DataObject;
			this.m_database = this.m_databaseCopy.GetDatabase<Database>();
			DatabaseTasksHelper.ValidateDatabaseCopyActionTask(this.m_databaseCopy, true, true, base.DataSession, this.RootId, new Task.TaskErrorLoggingDelegate(base.WriteError), Strings.ErrorMailboxDatabaseNotUnique(this.m_database.Identity.ToString()), new LocalizedString?(Strings.ErrorSingleDatabaseCopyRemove(this.m_database.Identity.ToString(), this.m_databaseCopy.HostServerName)), out this.m_server);
			if (this.m_server == null)
			{
				this.m_serverName = this.m_databaseCopy.Name;
			}
			else
			{
				this.m_serverName = this.m_server.Name;
			}
			DatabaseCopy[] allDatabaseCopies = this.m_database.AllDatabaseCopies;
			this.m_originalCopiesLength = allDatabaseCopies.Length;
			if (this.m_originalCopiesLength == 2 && this.m_database.CircularLoggingEnabled)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorInvalidRemoveOperationOnDBCopyForCircularLoggingEnabledDB(this.m_database.Name)), ErrorCategory.InvalidOperation, base.DataObject.Identity);
			}
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x0016CDDC File Offset: 0x0016AFDC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Identity != null)
			{
				this.Identity.AllowInvalid = true;
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			this.ValidateDatabaseCopy();
			if (RemoteReplayConfiguration.IsServerRcrSource(ADObjectWrapperFactory.CreateWrapper(this.m_database), this.m_serverName, (ITopologyConfigurationSession)this.ConfigurationSession, out this.m_dbLocationInfo))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorDbMountedOnServer(this.m_database.Identity.ToString(), this.m_serverName)), ErrorCategory.InvalidOperation, this.m_database.Identity);
			}
			if (this.m_server != null)
			{
				base.VerifyIsWithinScopes((IConfigurationSession)base.DataSession, this.m_server, true, new DataAccessTask<DatabaseCopy>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			}
			if (this.m_database != null)
			{
				MapiTaskHelper.VerifyDatabaseIsWithinScope(base.SessionSettings, this.m_database, new Task.ErrorLoggerDelegate(base.WriteError));
				if (this.m_database.Servers != null && this.m_database.Servers.Length > 0)
				{
					MapiTaskHelper.VerifyServerIsWithinScope(this.m_database, new Task.ErrorLoggerDelegate(base.WriteError), (ITopologyConfigurationSession)this.ConfigurationSession);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x0016CF14 File Offset: 0x0016B114
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			DatabaseCopy dataObject = base.DataObject;
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			this.m_database.InvalidDatabaseCopiesAllowed = true;
			this.RunConfigurationUpdaterRpc();
			try
			{
				ReplayState.DeleteState((this.m_server != null) ? this.m_server.Fqdn : this.m_serverName, this.m_database);
			}
			catch (SecurityException ex)
			{
				this.WriteWarning(Strings.ErrorCannotDeleteReplayState(this.m_database.Name, this.m_serverName, ex.Message));
			}
			catch (UnauthorizedAccessException ex2)
			{
				this.WriteWarning(Strings.ErrorCannotDeleteReplayState(this.m_database.Name, this.m_serverName, ex2.Message));
			}
			catch (IOException ex3)
			{
				this.WriteWarning(Strings.ErrorCannotDeleteReplayState(this.m_database.Name, this.m_serverName, ex3.Message));
			}
			catch (RemoteRegistryTimedOutException ex4)
			{
				this.WriteWarning(Strings.ErrorCannotDeleteReplayState(this.m_database.Name, this.m_serverName, ex4.Message));
			}
			string pathName = this.m_database.EdbFilePath.PathName;
			if (string.IsNullOrEmpty(pathName))
			{
				this.WriteWarning(Strings.NeedRemoveCopyLogFileManuallyAfterCopyDisabledRcr(this.m_database.Name, this.m_database.LogFolderPath.PathName, this.m_serverName));
			}
			else
			{
				this.WriteWarning(Strings.NeedRemoveCopyFileManuallyAfterCopyDisabledRcr(this.m_database.Name, this.m_database.LogFolderPath.PathName, pathName, this.m_serverName));
			}
			DatabaseTasksHelper.UpdateDataGuaranteeConstraint((ITopologyConfigurationSession)base.DataSession, this.m_database, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			TaskLogger.LogExit();
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x0016D0E8 File Offset: 0x0016B2E8
		private void RunConfigurationUpdaterRpc()
		{
			if (this.m_server != null)
			{
				string fqdn = this.m_server.Fqdn;
				DatabaseTasksHelper.RunConfigurationUpdaterRpc(fqdn, this.m_database, this.m_server.AdminDisplayVersion, ReplayConfigChangeHints.DbCopyRemoved, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			string serverFqdn = this.m_dbLocationInfo.ServerFqdn;
			DatabaseTasksHelper.RunConfigurationUpdaterRpc(serverFqdn, this.m_database, new ServerVersion(this.m_dbLocationInfo.ServerVersion), ReplayConfigChangeHints.DbCopyRemoved, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
		}

		// Token: 0x04003274 RID: 12916
		private Server m_server;

		// Token: 0x04003275 RID: 12917
		private string m_serverName;

		// Token: 0x04003276 RID: 12918
		private Database m_database;

		// Token: 0x04003277 RID: 12919
		private DatabaseCopy m_databaseCopy;

		// Token: 0x04003278 RID: 12920
		private DatabaseLocationInfo m_dbLocationInfo;

		// Token: 0x04003279 RID: 12921
		private int m_originalCopiesLength;
	}
}
