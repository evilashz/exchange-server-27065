using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000988 RID: 2440
	[Cmdlet("Move", "DatabasePath", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class MoveDatabasePath : MoveStoreFilesTask<DatabaseIdParameter, Database>
	{
		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x06005711 RID: 22289 RVA: 0x00167B94 File Offset: 0x00165D94
		// (set) Token: 0x06005712 RID: 22290 RVA: 0x00167BBA File Offset: 0x00165DBA
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x06005713 RID: 22291 RVA: 0x00167BD2 File Offset: 0x00165DD2
		// (set) Token: 0x06005714 RID: 22292 RVA: 0x00167BE9 File Offset: 0x00165DE9
		[Parameter]
		public EdbFilePath EdbFilePath
		{
			get
			{
				return (EdbFilePath)base.Fields["EdbFilePath"];
			}
			set
			{
				value.ValidateEdbFileExtension();
				base.Fields["EdbFilePath"] = value;
			}
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06005715 RID: 22293 RVA: 0x00167C02 File Offset: 0x00165E02
		// (set) Token: 0x06005716 RID: 22294 RVA: 0x00167C19 File Offset: 0x00165E19
		[Parameter]
		public NonRootLocalLongFullPath LogFolderPath
		{
			get
			{
				return (NonRootLocalLongFullPath)base.Fields["LogFolderPath"];
			}
			set
			{
				base.Fields["LogFolderPath"] = value;
			}
		}

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06005717 RID: 22295 RVA: 0x00167C2C File Offset: 0x00165E2C
		private EdbFilePath OriginalEdbFilePath
		{
			get
			{
				return this.originalEdbFilePath;
			}
		}

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06005718 RID: 22296 RVA: 0x00167C34 File Offset: 0x00165E34
		private EdbFilePath TargetEdbFilePath
		{
			get
			{
				return this.EdbFilePath ?? this.OriginalEdbFilePath;
			}
		}

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x06005719 RID: 22297 RVA: 0x00167C46 File Offset: 0x00165E46
		// (set) Token: 0x0600571A RID: 22298 RVA: 0x00167C4E File Offset: 0x00165E4E
		private NonRootLocalLongFullPath OldLogFolderPath
		{
			get
			{
				return this.oldLogFolderPath;
			}
			set
			{
				this.oldLogFolderPath = value;
			}
		}

		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x0600571B RID: 22299 RVA: 0x00167C57 File Offset: 0x00165E57
		private bool IsDatabaseFilesCreated
		{
			get
			{
				return this.DataObject.DatabaseCreated;
			}
		}

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x0600571C RID: 22300 RVA: 0x00167C64 File Offset: 0x00165E64
		private bool IsEdbFilePathChanged
		{
			get
			{
				return this.isEdbFilePathChanged;
			}
		}

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x0600571D RID: 22301 RVA: 0x00167C6C File Offset: 0x00165E6C
		protected override Server OwnerServer
		{
			get
			{
				if (this.ownerServer == null)
				{
					ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
					DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(this.DataObject.Guid);
					this.ownerServer = (Server)base.GetDataObject<Server>(ServerIdParameter.Parse(serverForDatabase.ServerFqdn), base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(serverForDatabase.ServerFqdn)), new LocalizedString?(Strings.ErrorServerNotUnique(serverForDatabase.ServerFqdn)));
				}
				return this.ownerServer;
			}
		}

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x0600571E RID: 22302 RVA: 0x00167CE7 File Offset: 0x00165EE7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMoveDatabasePath(this.Identity.ToString());
			}
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x00167CFC File Offset: 0x00165EFC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			Database database = (Database)base.PrepareDataObject();
			this.originalEdbFilePath = database.EdbFilePath;
			this.isEdbFilePathChanged = (null != this.EdbFilePath && this.EdbFilePath != database.EdbFilePath);
			database.EdbFilePath = this.TargetEdbFilePath;
			this.isLogFolderPathChanged = (this.LogFolderPath != null && this.LogFolderPath != database.LogFolderPath);
			if (this.isLogFolderPathChanged)
			{
				this.OldLogFolderPath = database.LogFolderPath;
				database.LogFolderPath = this.LogFolderPath;
				database.SystemFolderPath = this.LogFolderPath;
			}
			TaskLogger.LogExit();
			return database;
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x00167DB4 File Offset: 0x00165FB4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.OwnerServer == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorDBOwningServerNotFound(this.DataObject.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			base.VerifyIsWithinScopes((IConfigurationSession)base.DataSession, this.OwnerServer, true, new DataAccessTask<Database>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			if (this.DataObject != null)
			{
				MapiTaskHelper.VerifyDatabaseAndItsOwningServerInScope(base.SessionSettings, this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			ADObjectId adobjectId = new ADObjectId(this.OwnerServer.Guid);
			if (null == this.EdbFilePath && (base.Fields.IsModified("EdbFilePath") || base.Fields.IsChanged("EdbFilePath")))
			{
				base.WriteError(new ArgumentException(Strings.ErrorInvalidParameterValue("EdbFilePath", "null"), "EdbFilePath"), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (!this.IsEdbFilePathChanged && !this.isLogFolderPathChanged)
			{
				this.WriteWarning(Strings.FileLocationNotChanged);
				TaskLogger.LogExit();
				return;
			}
			this.needReportProgress = !base.ConfigurationOnly;
			this.shouldContinueToDoConfigurationOnly = true;
			try
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, this.DataObject.Guid);
				ADObjectId rootId = this.DataObject.IsExchange2009OrLater ? this.DataObject.AdministrativeGroup.GetChildId("Databases") : this.DataObject.Server;
				IEnumerable<Database> collection = base.DataSession.FindPaged<Database>(filter, rootId, true, null, 0);
				List<Database> databases = new List<Database>(collection);
				if (this.IsEdbFilePathChanged)
				{
					if (this.DataObject.IsExchange2009OrLater)
					{
						base.WriteVerbose(Strings.VerboseEdbFileLocationUniqueUnderDAGCondition(this.EdbFilePath.PathName));
						ADObjectId[] serversId;
						if (this.DataObject.Servers != null && this.DataObject.Servers.Length != 0)
						{
							serversId = this.DataObject.Servers;
						}
						else
						{
							serversId = new ADObjectId[]
							{
								this.DataObject.Server
							};
						}
						if (!new EdbFileLocationUniqueUnderDAGCondition(this.EdbFilePath.PathName, adobjectId, serversId, databases).Verify())
						{
							base.WriteError(new ArgumentException(Strings.ErrorEdbFileLocationNotUniqueUnderSameDAG(this.EdbFilePath.PathName), "EdbFilePath"), ErrorCategory.InvalidArgument, this.Identity);
						}
					}
					else
					{
						base.WriteVerbose(Strings.VerboseEdbFileLocationUniqueUnderServerCondition(base.OwnerServerName, this.EdbFilePath.PathName));
						if (!new EdbFileLocationUniqueUnderServerCondition(this.EdbFilePath.PathName, adobjectId, databases).Verify())
						{
							base.WriteError(new ArgumentException(Strings.ErrorEdbFileLocationNotUniqueUnderSameNode(this.EdbFilePath.PathName, base.OwnerServerName), "EdbFilePath"), ErrorCategory.InvalidArgument, this.Identity);
						}
					}
				}
				if (this.isLogFolderPathChanged)
				{
					if (this.DataObject.IsExchange2009OrLater)
					{
						ADObjectId[] serversId2;
						if (this.DataObject.Servers != null && this.DataObject.Servers.Length != 0)
						{
							serversId2 = this.DataObject.Servers;
						}
						else
						{
							serversId2 = new ADObjectId[]
							{
								this.DataObject.Server
							};
						}
						if (!new DbLogLocationUniqueUnderDAGCondition(this.LogFolderPath.PathName, adobjectId, serversId2, databases).Verify())
						{
							base.WriteError(new ArgumentException(Strings.ErrorLogFolderPathNotUniqueUnderSameDAG(this.LogFolderPath.PathName), "LogFolderPath"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
						}
					}
					else if (!new DbLogLocationUniqueUnderServerCondition(this.LogFolderPath.PathName, adobjectId, databases).Verify())
					{
						base.WriteError(new ArgumentException(Strings.ErrorLogFolderPathNotUniqueUnderTheSameNode(this.LogFolderPath.PathName, base.OwnerServerName), "LogFolderPath"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					}
				}
				if (base.ConfigurationOnly)
				{
					this.moveCatalogs = false;
					if (!this.Force && !(this.shouldContinueToDoConfigurationOnly = base.ShouldContinue(Strings.WarningUseConfigurationOnly)))
					{
						TaskLogger.LogExit();
						return;
					}
					if (this.IsEdbFilePathChanged)
					{
						base.WriteVerbose(Strings.VerboseCheckFileExistenceCondition(base.OwnerServerName, this.TargetEdbFilePath.PathName));
						if (new FileNotExistCondition(this.OwnerServer.Fqdn, this.TargetEdbFilePath.PathName).Verify())
						{
							this.WriteWarning(Strings.WarningEdbFileLocationNotExists(this.EdbFilePath.PathName));
						}
					}
					TaskLogger.LogExit();
					return;
				}
				else
				{
					if (this.needReportProgress)
					{
						base.WriteProgress(Strings.ProgressValidatingFileLocations, Strings.ProgressMoveDatabasePath(this.Identity.ToString()), 5);
					}
					if (this.IsEdbFilePathChanged)
					{
						if (this.OriginalEdbFilePath == null)
						{
							base.WriteError(new InvalidOperationException(Strings.ErrorOriginalEdbFilePathMissed(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.Identity);
						}
						if (this.IsDatabaseFilesCreated)
						{
							base.WriteVerbose(Strings.VerboseCheckFileExistenceCondition(base.OwnerServerName, this.OriginalEdbFilePath.PathName));
							if (new FileNotExistCondition(this.OwnerServer.Fqdn, this.OriginalEdbFilePath.PathName).Verify())
							{
								base.WriteError(new InvalidOperationException(Strings.ErrorSourceFileNotFound(base.OwnerServerName, this.OriginalEdbFilePath.PathName)), ErrorCategory.InvalidOperation, this.Identity);
							}
						}
						base.WriteVerbose(Strings.VerbosePathOnFixedOrNetworkDriveCondition(base.OwnerServerName, this.EdbFilePath.PathName));
						if (!new PathOnFixedOrNetworkDriveCondition(this.OwnerServer.Fqdn, this.EdbFilePath.PathName).Verify())
						{
							base.WriteError(new ArgumentException(Strings.ErrorEdbFileLocationNotOnFixedDrive(this.EdbFilePath.PathName), "EdbFilePath"), ErrorCategory.InvalidArgument, this.Identity);
						}
						base.WriteVerbose(Strings.VerboseCheckFileExistenceCondition(base.OwnerServerName, this.EdbFilePath.PathName));
						if (!new FileNotExistCondition(this.OwnerServer.Fqdn, this.EdbFilePath.PathName).Verify())
						{
							base.WriteError(new ArgumentException(Strings.ErrorEdbFilePathOccupiedByFile(this.EdbFilePath.PathName, base.OwnerServerName), "EdbFilePath"), ErrorCategory.InvalidArgument, this.Identity);
						}
						base.WriteVerbose(Strings.VerboseCheckDirectoryExistenceCondition(base.OwnerServerName, this.EdbFilePath.PathName));
						if (!new DirectoryNotExistCondition(this.OwnerServer.Fqdn, this.EdbFilePath.PathName).Verify())
						{
							base.WriteError(new ArgumentException(Strings.ErrorEdbFilePathOccupiedByDirectory(this.EdbFilePath.PathName, base.OwnerServerName), "EdbFilePath"), ErrorCategory.InvalidArgument, this.Identity);
						}
						string directoryName = Path.GetDirectoryName(this.EdbFilePath.PathName);
						if (this.IsDatabaseFilesCreated && !SystemConfigurationTasksHelper.TryCreateDirectory(this.OwnerServer.Fqdn, directoryName, Database_Directory.GetDomainWidePermissions(), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning)))
						{
							base.WriteError(new ArgumentException(Strings.ErrorEdbFileDirectoryNotExist(directoryName, base.OwnerServerName), "EdbFilePath"), ErrorCategory.InvalidOperation, this.Identity);
						}
						if (this.moveCatalogs)
						{
							this.moveCatalogs = this.DataObject.ObjectClass.Contains("msExchPrivateMDB");
							if (this.moveCatalogs)
							{
								string directoryName2 = Path.GetDirectoryName(this.OriginalEdbFilePath.PathName);
								this.originalCatalogsPath = this.GenerateCatalogPath(directoryName2, this.DataObject.Guid);
								base.WriteVerbose(Strings.VerboseCheckDirectoryExistenceCondition(base.OwnerServerName, this.originalCatalogsPath));
								this.moveCatalogs = WmiWrapper.IsDirectoryExisting(this.OwnerServer.Fqdn, this.originalCatalogsPath);
								if (this.moveCatalogs)
								{
									string directoryName3 = Path.GetDirectoryName(this.EdbFilePath.PathName);
									this.targetCatalogsPath = this.GenerateCatalogPath(directoryName3, this.DataObject.Guid);
									this.moveCatalogs = (this.originalCatalogsPath != this.targetCatalogsPath);
								}
							}
						}
					}
					else
					{
						this.moveCatalogs = false;
					}
					if (base.ConfigurationOnly)
					{
						TaskLogger.LogExit();
						return;
					}
					if (this.isLogFolderPathChanged)
					{
						if (this.OldLogFolderPath == null)
						{
							base.WriteError(new InvalidOperationException(Strings.ErrorOriginalLogFolderPathIsMissed(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
						}
						base.WriteVerbose(Strings.VerbosePathOnFixedOrNetworkDriveCondition(this.OwnerServer.Fqdn, this.LogFolderPath.PathName));
						if (!new PathOnFixedOrNetworkDriveCondition(this.OwnerServer.Fqdn, this.LogFolderPath.PathName).Verify())
						{
							base.WriteError(new ArgumentException(Strings.ErrorPathIsNotOnFixedDrive("LogFolderPath")), ErrorCategory.InvalidArgument, this.DataObject.Identity);
						}
						base.WriteVerbose(Strings.VerboseLogLocationAvailableCondition(this.OwnerServer.Fqdn, this.LogFolderPath.PathName));
						if (!new LogLocationAvailableCondition(this.OwnerServer.Fqdn, this.LogFolderPath.PathName, this.DataObject.LogFilePrefix).Verify())
						{
							base.WriteError(new ArgumentException(Strings.ErrorLogFolderPathNotAvailable, "LogFolderPath"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
						}
						base.WriteVerbose(Strings.VerboseCheckLogFileExistingInPath(this.OwnerServer.Fqdn, this.oldLogFolderPath.PathName));
						if (!WmiWrapper.IsFileExistingInPath(this.OwnerServer.Fqdn, this.oldLogFolderPath.PathName, new WmiWrapper.FileFilter(this.LogFileFilter)))
						{
							base.WriteError(new InvalidOperationException(Strings.ErrorMoveDatabasePathAsSourceFileNotExist(this.oldLogFolderPath.PathName)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
						}
					}
				}
			}
			catch (WmiException ex)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorFailedToConnectToServer(base.OwnerServerName, ex.Message)), ErrorCategory.InvalidOperation, this.Identity);
			}
			catch (UnauthorizedAccessException ex2)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorFailedToConnectToServer(base.OwnerServerName, ex2.Message)), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (this.DataObject.ReplicationType == ReplicationType.Remote && !this.DataObject.Recovery)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorMoveDatabasePathInvalidOnReplicated), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x00168834 File Offset: 0x00166A34
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			bool flag4 = false;
			bool flag5 = false;
			Server server = this.OwnerServer;
			ADObjectId id = new ADObjectId(this.OwnerServer.Guid);
			if (server == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorDBOwningServerNotFound(this.DataObject.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			try
			{
				if ((!this.IsEdbFilePathChanged && !this.isLogFolderPathChanged) || (base.ConfigurationOnly && !this.shouldContinueToDoConfigurationOnly))
				{
					TaskLogger.LogExit();
				}
				else
				{
					if (this.needReportProgress)
					{
						base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressCheckingReplayState, 10);
					}
					base.WriteVerbose(Strings.VerboseConnectionAdminRpcInterface(this.OwnerServer.Fqdn));
					MdbStatus mdbStatus = AmStoreHelper.GetMdbStatus(this.OwnerServer.Fqdn, this.DataObject.Guid);
					if (mdbStatus == null)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorFailedToGetDatabaseStatus(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.Identity);
					}
					if ((mdbStatus.Status & MdbStatusFlags.Backup) != MdbStatusFlags.Offline)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorBackupInProgress(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.Identity);
					}
					else if ((mdbStatus.Status & MdbStatusFlags.Online) != MdbStatusFlags.Offline)
					{
						if (!this.Force && !base.ShouldContinue(base.ConfigurationOnly ? Strings.WarningDismountDatabaseToDoConfigurationOnly(this.Identity.ToString()) : Strings.WarningDismountDatabaseToContinue(this.Identity.ToString())))
						{
							TaskLogger.LogExit();
							return;
						}
						if (this.needReportProgress)
						{
							base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressDismountingDatabase(this.Identity.ToString()), 20);
						}
						base.WriteVerbose(TaskVerboseStringHelper.GetReadObjectVerboseString(id, base.DataSession, typeof(Server)));
						try
						{
							base.WriteVerbose(Strings.VerboseUnmountDatabase(this.DataObject.Identity.ToString()));
							AmRpcClientHelper.DismountDatabase(ADObjectWrapperFactory.CreateWrapper(this.DataObject), 0);
						}
						catch (AmServerException ex)
						{
							Exception ex2;
							if (ex.TryGetInnerExceptionOfType(out ex2))
							{
								TaskLogger.Trace("MoveDatabasePath.InternalProcessRecord raises exception while unmounting database: {0}", new object[]
								{
									ex2.Message
								});
							}
							else if (ex.TryGetInnerExceptionOfType(out ex2))
							{
								TaskLogger.Trace("MoveDatabasePath.InternalProcessRecord raises exception while unmounting database: {0}", new object[]
								{
									ex2.Message
								});
							}
							else if (ex is AmDatabaseNeverMountedException)
							{
								TaskLogger.Trace("MoveDatabasePath.InternalProcessRecord raises exception while unmounting database: {0}", new object[]
								{
									ex.Message
								});
							}
							else
							{
								TaskLogger.Trace("MoveDatabasePath.InternalProcessRecord raises exception while unmounting database: {0}", new object[]
								{
									ex.Message
								});
								base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject.Identity);
							}
						}
						catch (AmServerTransientException ex3)
						{
							TaskLogger.Trace("MoveDatabasePath.InternalProcessRecord raises exception while dismounting database: {0}", new object[]
							{
								ex3.Message
							});
							base.WriteError(ex3, ErrorCategory.InvalidOperation, this.DataObject.Identity);
						}
						flag = true;
					}
					if (!base.ConfigurationOnly)
					{
						if (this.IsDatabaseFilesCreated && this.IsEdbFilePathChanged)
						{
							if (this.needReportProgress)
							{
								base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressCopyingEdbFile, 25);
							}
							if (!this.TryCopyFile(this.OriginalEdbFilePath.PathName, this.TargetEdbFilePath.PathName))
							{
								base.WriteError(new InvalidOperationException(Strings.ErrorFailedToMoveEdbFile(this.OriginalEdbFilePath.PathName, this.TargetEdbFilePath.PathName)), ErrorCategory.InvalidOperation, this.Identity);
							}
							flag2 = true;
						}
						if (this.isLogFolderPathChanged)
						{
							if (this.needReportProgress)
							{
								base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressCopyingLogFiles, 45);
							}
							base.WriteVerbose(Strings.VerboseCopyDatabaseLogFiles(base.OwnerServerName, this.OldLogFolderPath.PathName, this.LogFolderPath.PathName));
							if (!this.TryCopyPath(this.OldLogFolderPath.PathName, this.LogFolderPath.PathName, new WmiWrapper.FileFilter(this.LogFileFilter)))
							{
								base.WriteError(new InvalidOperationException(Strings.ErrorFailedToMoveDatabaseLogFiles(this.OldLogFolderPath.PathName, this.LogFolderPath.PathName)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
							}
							flag4 = true;
						}
					}
					if (this.needReportProgress)
					{
						base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressSavingADObject, 69);
					}
					base.InternalProcessRecord();
					flag3 = false;
					if (flag2)
					{
						if (this.needReportProgress)
						{
							base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressDeletingEdbFile, 72);
						}
						if (!this.TryDeleteFile(this.OriginalEdbFilePath.PathName))
						{
							this.WriteWarning(Strings.FailedToDeleteOldEdbFile(this.OriginalEdbFilePath.PathName));
							TaskLogger.Trace("MoveDatabasePath: delete edb \"{0}\" file failed", new object[]
							{
								this.OriginalEdbFilePath.PathName
							});
						}
					}
					if (flag4)
					{
						if (this.needReportProgress)
						{
							base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressDeletingLogFiles, 81);
						}
						base.WriteVerbose(Strings.VerboseDeleteDatabaseLogFiles(this.OwnerServer.Fqdn, this.OldLogFolderPath.PathName));
						if (!WmiWrapper.DeleteFilesInDirectory(this.OwnerServer.Fqdn, this.OldLogFolderPath.PathName, new WmiWrapper.FileFilter(this.LogPathFilter)))
						{
							this.WriteWarning(Strings.FailedDeleteOldDatabaseLogFiles(this.OwnerServer.Fqdn, this.OldLogFolderPath.PathName));
							TaskLogger.Trace("Failed to delete some of the orignal log files.", new object[0]);
						}
					}
					if (this.moveCatalogs && SystemConfigurationTasksHelper.TryCreateDirectory(this.OwnerServer.Fqdn, this.targetCatalogsPath, null, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning)))
					{
						if (this.needReportProgress)
						{
							base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressCopyingCatalog, 87);
						}
						try
						{
							base.WriteVerbose(Strings.VerboseCopyDatabaseCatalogFiles(this.OwnerServer.Fqdn, this.originalCatalogsPath, this.targetCatalogsPath));
							if (WmiWrapper.CopyFilesInDirectory(this.OwnerServer.Fqdn, this.originalCatalogsPath, this.targetCatalogsPath, new WmiWrapper.FileFilter(MoveDatabasePath.ReturnAllFiles)))
							{
								if (this.needReportProgress)
								{
									base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressDeletingCatalog, 92);
								}
								base.WriteVerbose(Strings.VerboseDeleteDatabaseCatalogFiles(this.OwnerServer.Fqdn, this.originalCatalogsPath));
								WmiWrapper.DeleteFilesInDirectory(this.OwnerServer.Fqdn, this.originalCatalogsPath, new WmiWrapper.FileFilter(MoveDatabasePath.ReturnAllFiles));
								base.WriteVerbose(Strings.VerboseDeleteDirectory(this.OwnerServer.Fqdn, this.originalCatalogsPath));
								WmiWrapper.RemoveDirectory(this.OwnerServer.Fqdn, this.originalCatalogsPath);
							}
						}
						catch (ManagementException ex4)
						{
							TaskLogger.Trace("MoveDatabasePath raised exception {0} while moving catalog files", new object[]
							{
								ex4.ToString()
							});
							this.WriteWarning(Strings.ErrorMovingCatalogs(this.Identity.ToString(), ex4.Message));
						}
						catch (ArgumentException ex5)
						{
							TaskLogger.Trace("MoveDatabasePath raised exception {0} while moving catalog files", new object[]
							{
								ex5.ToString()
							});
							this.WriteWarning(Strings.ErrorMovingCatalogs(this.Identity.ToString(), ex5.Message));
						}
					}
					this.SendNotificationRpcToReplayService();
					flag5 = true;
				}
			}
			catch (WmiException ex6)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorFailedToConnectToServer(base.OwnerServerName, ex6.Message)), ErrorCategory.InvalidOperation, this.Identity);
			}
			catch (UnauthorizedAccessException ex7)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorFailedToConnectToServer(base.OwnerServerName, ex7.Message)), ErrorCategory.InvalidOperation, this.Identity);
			}
			finally
			{
				if (flag3)
				{
					if (flag2 && !this.TryDeleteFile(this.TargetEdbFilePath.PathName))
					{
						this.WriteWarning(Strings.FailedToDeleteTempEdbFile(this.TargetEdbFilePath.PathName));
						TaskLogger.Trace("MoveDatabasePath: delete edb \"{0}\" file failed", new object[]
						{
							this.TargetEdbFilePath.PathName
						});
					}
					if (flag4)
					{
						TaskLogger.Trace("Error occurs when Copying path. delete copied log files", new object[0]);
						base.WriteVerbose(Strings.VerboseDeleteDatabaseLogFiles(this.OwnerServer.Fqdn, this.LogFolderPath.PathName));
						if (!WmiWrapper.DeleteFilesInDirectory(this.OwnerServer.Fqdn, this.LogFolderPath.PathName, new WmiWrapper.FileFilter(this.LogFileFilter)))
						{
							this.WriteWarning(Strings.FailedDeleteTempDatabaseLogFiles(base.OwnerServerName, this.LogFolderPath.PathName));
							TaskLogger.Trace("Failed to delete some of the copied log files.", new object[0]);
						}
					}
				}
				if (!base.ConfigurationOnly && flag)
				{
					if (this.needReportProgress)
					{
						base.WriteProgress(Strings.ProgressMoveDatabasePath(this.Identity.ToString()), Strings.ProgressRestoringDatabaseStatus, 95);
					}
					base.WriteVerbose(TaskVerboseStringHelper.GetReadObjectVerboseString(id, base.DataSession, typeof(Server)));
					try
					{
						if (!flag5)
						{
							this.SendNotificationRpcToReplayService();
							flag5 = true;
						}
						base.WriteVerbose(Strings.VerboseMountDatabase(this.Identity.ToString()));
						AmRpcClientHelper.MountDatabase(ADObjectWrapperFactory.CreateWrapper(this.DataObject), 0, 0, 0);
					}
					catch (AmServerException ex8)
					{
						string message = ex8.Message;
						TaskLogger.Trace("MoveDatabasePath.InternalProcessRecord raises exception while re-mounting Mdb status: {0}", new object[]
						{
							message
						});
						this.WriteWarning(Strings.ErrorFailedToRestoreDatabaseStatus(this.Identity.ToString(), message));
					}
					catch (AmServerTransientException ex9)
					{
						string message2 = ex9.Message;
						TaskLogger.Trace("MoveDatabasePath.InternalProcessRecord raises exception while re-mounting Mdb status: {0}", new object[]
						{
							message2
						});
						this.WriteWarning(Strings.ErrorFailedToRestoreDatabaseStatus(this.Identity.ToString(), message2));
					}
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x00169310 File Offset: 0x00167510
		private void SendNotificationRpcToReplayService()
		{
			DatabaseTasksHelper.RunConfigurationUpdaterRpc(this.OwnerServer.Fqdn, this.DataObject, this.OwnerServer.AdminDisplayVersion, ReplayConfigChangeHints.MoveDatabasePathConfigChanged, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x00169350 File Offset: 0x00167550
		private bool TryCopyFile(string fromFilePath, string toFilePath)
		{
			bool result;
			try
			{
				base.WriteVerbose(Strings.VerboseCopyFile(this.OwnerServer.Fqdn, fromFilePath, toFilePath));
				result = WmiWrapper.CopyFileIfExists(this.OwnerServer.Fqdn, fromFilePath, toFilePath);
			}
			catch (ManagementException ex)
			{
				base.WriteVerbose(Strings.VerboseEatUpException(ex.Message));
				TaskLogger.Trace("MoveDatabasePath.TryCopyFile raises exception: {0}", new object[]
				{
					ex.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x001693CC File Offset: 0x001675CC
		private bool TryDeleteFile(string targetFilePath)
		{
			bool result;
			try
			{
				base.WriteVerbose(Strings.VerboseDeleteFile(base.OwnerServerName, targetFilePath));
				result = WmiWrapper.DeleteFileIfExists(this.OwnerServer.Fqdn, targetFilePath);
			}
			catch (ManagementException ex)
			{
				base.WriteVerbose(Strings.VerboseEatUpException(ex.Message));
				TaskLogger.Trace("MoveDatabasePath.TryDeleteFile raises exception: {0}", new object[]
				{
					ex.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x00169444 File Offset: 0x00167644
		private string GenerateCatalogPath(string basePath, Guid mdbGuid)
		{
			Guid guid = this.OwnerServer.Guid;
			return Path.Combine(basePath, "CatalogData-" + mdbGuid.ToString() + "-" + guid.ToString()).ToLower();
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x00169491 File Offset: 0x00167691
		private static bool ReturnAllFiles(string name, string ext)
		{
			return true;
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x00169494 File Offset: 0x00167694
		private bool TryCopyPath(string originalPath, string newPath, WmiWrapper.FileFilter filter)
		{
			bool result = true;
			try
			{
				if (SystemConfigurationTasksHelper.TryCreateDirectory(this.ownerServer.Fqdn, newPath, Database_Directory.GetDomainWidePermissions(), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning)))
				{
					base.WriteVerbose(Strings.VerboseCheckDirectoryExistenceCondition(this.OwnerServer.Fqdn, originalPath));
					if (WmiWrapper.IsDirectoryExisting(this.OwnerServer.Fqdn, originalPath))
					{
						base.WriteVerbose(Strings.VerboseCopyDirectory(this.OwnerServer.Fqdn, originalPath, newPath));
						if (!WmiWrapper.CopyFilesInDirectory(this.OwnerServer.Fqdn, originalPath, newPath, filter))
						{
							result = false;
						}
					}
					else
					{
						TaskLogger.Trace("Original directory does not exist, nothing to copy.", new object[0]);
					}
				}
				else
				{
					result = false;
				}
			}
			catch (ManagementException ex)
			{
				base.WriteVerbose(Strings.VerboseEatUpException(ex.Message));
				TaskLogger.Trace("Error occurs when copying path: {0}", new object[]
				{
					ex.Message
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x00169588 File Offset: 0x00167788
		private bool LogPathFilter(string name, string ext)
		{
			return name.StartsWith(this.DataObject.LogFilePrefix, StringComparison.InvariantCultureIgnoreCase) && (ext.Equals("log", StringComparison.InvariantCultureIgnoreCase) || ext.Equals("jrs", StringComparison.InvariantCultureIgnoreCase) || ext.Equals("chk", StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x001695D5 File Offset: 0x001677D5
		private bool LogFileFilter(string name, string ext)
		{
			return name.StartsWith(this.DataObject.LogFilePrefix, StringComparison.InvariantCultureIgnoreCase) && ext.Equals("log", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x001695F9 File Offset: 0x001677F9
		private bool ChkFileFilter(string name, string ext)
		{
			return ext.Equals("chk", StringComparison.InvariantCultureIgnoreCase) && name.Equals(this.DataObject.LogFilePrefix, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x04003232 RID: 12850
		private const int ProgressValidatingFilePaths = 5;

		// Token: 0x04003233 RID: 12851
		private const int ProgressCheckingReplayState = 10;

		// Token: 0x04003234 RID: 12852
		private const int ProgressDismountingDatabase = 20;

		// Token: 0x04003235 RID: 12853
		private const int ProgressCopyingEdbFile = 25;

		// Token: 0x04003236 RID: 12854
		private const int ProgressCopyingLogFiles = 45;

		// Token: 0x04003237 RID: 12855
		private const int ProgressSavingADObject = 69;

		// Token: 0x04003238 RID: 12856
		private const int ProgressDeletingEdbFile = 72;

		// Token: 0x04003239 RID: 12857
		private const int ProgressDeletingLogFiles = 81;

		// Token: 0x0400323A RID: 12858
		private const int ProgressCopyingCatalog = 87;

		// Token: 0x0400323B RID: 12859
		private const int ProgressDeletingCatalog = 92;

		// Token: 0x0400323C RID: 12860
		private const int ProgressRestoringDatabaseStatus = 95;

		// Token: 0x0400323D RID: 12861
		private const int ProgressRestoringReplayState = 98;

		// Token: 0x0400323E RID: 12862
		internal const string paramForce = "Force";

		// Token: 0x0400323F RID: 12863
		internal const string paramEdbFilePath = "EdbFilePath";

		// Token: 0x04003240 RID: 12864
		internal const string paramLogFolderPath = "LogFolderPath";

		// Token: 0x04003241 RID: 12865
		private const string logFileExtension = "log";

		// Token: 0x04003242 RID: 12866
		private const string jrsFileExtension = "jrs";

		// Token: 0x04003243 RID: 12867
		private const string chkFileExtension = "chk";

		// Token: 0x04003244 RID: 12868
		private const string tmpFileName = "tmp";

		// Token: 0x04003245 RID: 12869
		private const string edbFileExtension = "edb";

		// Token: 0x04003246 RID: 12870
		private EdbFilePath originalEdbFilePath;

		// Token: 0x04003247 RID: 12871
		private bool isEdbFilePathChanged;

		// Token: 0x04003248 RID: 12872
		private bool shouldContinueToDoConfigurationOnly;

		// Token: 0x04003249 RID: 12873
		private bool isLogFolderPathChanged;

		// Token: 0x0400324A RID: 12874
		private NonRootLocalLongFullPath oldLogFolderPath;

		// Token: 0x0400324B RID: 12875
		private string originalCatalogsPath;

		// Token: 0x0400324C RID: 12876
		private string targetCatalogsPath;

		// Token: 0x0400324D RID: 12877
		private bool moveCatalogs = true;

		// Token: 0x0400324E RID: 12878
		private bool needReportProgress = true;

		// Token: 0x0400324F RID: 12879
		private Server ownerServer;
	}
}
