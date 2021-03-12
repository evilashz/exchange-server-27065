using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200098B RID: 2443
	public abstract class NewDatabaseTask<TDataObject> : NewFixedNameSystemConfigurationObjectTask<TDataObject> where TDataObject : Database, new()
	{
		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x0600572E RID: 22318 RVA: 0x00169748 File Offset: 0x00167948
		// (set) Token: 0x0600572F RID: 22319 RVA: 0x0016976C File Offset: 0x0016796C
		protected string Name
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.Name;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.Name = value;
			}
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x06005730 RID: 22320 RVA: 0x0016978E File Offset: 0x0016798E
		// (set) Token: 0x06005731 RID: 22321 RVA: 0x001697A5 File Offset: 0x001679A5
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x06005732 RID: 22322 RVA: 0x001697B8 File Offset: 0x001679B8
		// (set) Token: 0x06005733 RID: 22323 RVA: 0x001697DC File Offset: 0x001679DC
		[Parameter]
		public EdbFilePath EdbFilePath
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.EdbFilePath;
			}
			set
			{
				if (null != value)
				{
					value.ValidateEdbFileExtension();
					TDataObject dataObject = this.DataObject;
					dataObject.EdbFilePath = value;
				}
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x00169810 File Offset: 0x00167A10
		// (set) Token: 0x06005735 RID: 22325 RVA: 0x00169834 File Offset: 0x00167A34
		[Parameter]
		public NonRootLocalLongFullPath LogFolderPath
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.LogFolderPath;
			}
			set
			{
				if (null != value)
				{
					TDataObject dataObject = this.DataObject;
					dataObject.LogFolderPath = value;
				}
			}
		}

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x0016985F File Offset: 0x00167A5F
		// (set) Token: 0x06005737 RID: 22327 RVA: 0x00169885 File Offset: 0x00167A85
		[Parameter]
		public SwitchParameter SkipDatabaseLogFolderCreation
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipDatabaseLogFolderCreation"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipDatabaseLogFolderCreation"] = value;
			}
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x0016989D File Offset: 0x00167A9D
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || SystemConfigurationTasksHelper.IsKnownWmiException(exception) || SystemConfigurationTasksHelper.IsKnownClusterUpdateDatabaseResourceException(exception);
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x001698C0 File Offset: 0x00167AC0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			Database database = (Database)base.PrepareDataObject();
			database.InvalidDatabaseCopiesAllowed = true;
			if (this.preExistingDatabase != null)
			{
				database = this.preExistingDatabase;
				database.InvalidDatabaseCopiesAllowed = true;
				TaskLogger.LogExit();
				return database;
			}
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.GlobalConfigSession, typeof(PublicFolderDatabase), null, this.OwnerServer.Identity, true));
			this.ownerServerPublicFolderDatabases = this.OwnerServer.GetPublicFolderDatabases();
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.GlobalConfigSession, typeof(Database), null, this.OwnerServer.Identity, true));
			this.ownerServerDatabases = this.OwnerServer.GetDatabases();
			string logFilePrefix = this.CalculateLogFilePrefix();
			database.LogFilePrefix = logFilePrefix;
			if (null != this.LogFolderPath)
			{
				try
				{
					this.ValidateLogFolderPath();
				}
				catch (WmiException ex)
				{
					Exception exception = new InvalidOperationException(Strings.ErrorFailedToConnectToServer(this.OwnerServer.Name, ex.Message));
					ErrorCategory category = ErrorCategory.InvalidOperation;
					TDataObject dataObject = this.DataObject;
					base.WriteError(exception, category, dataObject.Identity);
				}
				catch (UnauthorizedAccessException ex2)
				{
					Exception exception2 = new InvalidOperationException(Strings.ErrorFailedToConnectToServer(this.OwnerServer.Name, ex2.Message));
					ErrorCategory category2 = ErrorCategory.InvalidOperation;
					TDataObject dataObject2 = this.DataObject;
					base.WriteError(exception2, category2, dataObject2.Identity);
				}
			}
			database.SetId(base.GlobalConfigSession.GetDatabasesContainerId().GetChildId(this.Name));
			database.Name = this.Name;
			database.AdminDisplayName = this.Name;
			database.Server = (ADObjectId)this.OwnerServer.Identity;
			TDataObject dataObject3 = this.DataObject;
			dataObject3.DataMoveReplicationConstraint = DataMoveReplicationConstraintParameter.None;
			if (this.OwnerServer.DatabaseAvailabilityGroup != null)
			{
				database.MasterServerOrAvailabilityGroup = this.OwnerServer.DatabaseAvailabilityGroup;
			}
			else
			{
				database.MasterServerOrAvailabilityGroup = (ADObjectId)this.OwnerServer.Identity;
			}
			string text = new ClientAccessArrayTaskHelper(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError)).FindRpcClientAccessArrayOrServer((ITopologyConfigurationSession)this.ConfigurationSession, database.Server);
			if (text != null)
			{
				database.RpcClientAccessServerLegacyDN = text;
			}
			else
			{
				database.RpcClientAccessServerLegacyDN = this.OwnerServer.ExchangeLegacyDN;
			}
			TaskLogger.LogExit();
			return database;
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x00169B30 File Offset: 0x00167D30
		private string CalculateLogFilePrefix()
		{
			string text = null;
			for (int i = 0; i < 256; i++)
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, "E{0:X2}", new object[]
				{
					i
				});
				bool flag = false;
				foreach (Database database in this.OwnerServerDatabases)
				{
					if (database.LogFilePrefix.Equals(text2, StringComparison.InvariantCultureIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					text = text2;
					break;
				}
			}
			if (text == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorExceededMaxiumNumberOfDatabasesPerServer), ErrorCategory.InvalidOperation, this.Server);
			}
			return text;
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x00169BD4 File Offset: 0x00167DD4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DatabaseSchema.Name, this.Name);
			Database[] array = base.GlobalConfigSession.Find<Database>(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length == 1)
			{
				DatabaseCopy[] databaseCopies = array[0].GetDatabaseCopies();
				if (databaseCopies != null && databaseCopies.Length > 0)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorDatabaseNotUnique(this.Name)), ErrorCategory.InvalidOperation, this.Name);
				}
				else
				{
					Database database = array[0];
					this.preExistingDatabase = (TDataObject)((object)base.DataSession.Read<TDataObject>(database.Id));
				}
			}
			else if (array != null && array.Length > 1)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorDatabaseNotUnique(this.Name)), ErrorCategory.InvalidOperation, this.Name);
			}
			this.ownerServer = (Server)base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			MapiTaskHelper.VerifyIsWithinConfigWriteScope(base.SessionSettings, this.ownerServer, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			if (!this.ownerServer.IsE14OrLater)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorModifyE12ServerNotAllowed), ErrorCategory.InvalidOperation, this.ownerServer.Identity);
			}
			if (!this.ownerServer.IsMailboxServer)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorOperationOnlyOnMailboxServer(this.ownerServer.Name)), ErrorCategory.InvalidOperation, this.ownerServer.Identity);
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x0600573C RID: 22332 RVA: 0x00169D74 File Offset: 0x00167F74
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (!this.SkipDatabaseLogFolderCreation)
			{
				string directoryName = Path.GetDirectoryName(this.EdbFilePath.PathName);
				SystemConfigurationTasksHelper.TryCreateDirectory(this.ownerServer.Fqdn, directoryName, Database_Directory.GetDomainWidePermissions(), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				SystemConfigurationTasksHelper.TryCreateDirectory(this.ownerServer.Fqdn, this.LogFolderPath.PathName, Database_Directory.GetDomainWidePermissions(), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x00169E1C File Offset: 0x0016801C
		protected DatabaseCopy SaveDBCopy()
		{
			TaskLogger.LogEnter();
			DatabaseCopy databaseCopy = null;
			if (this.preExistingDatabase != null)
			{
				foreach (DatabaseCopy databaseCopy2 in this.preExistingDatabase.InvalidDatabaseCopies)
				{
					if (databaseCopy2.Name.Equals(this.OwnerServer.Name, StringComparison.OrdinalIgnoreCase))
					{
						databaseCopy = databaseCopy2;
						break;
					}
				}
			}
			DatabaseCopy databaseCopy3 = databaseCopy ?? new DatabaseCopy();
			databaseCopy3.HostServer = (ADObjectId)this.OwnerServer.Identity;
			if (databaseCopy == null)
			{
				databaseCopy3.ActivationPreference = 1;
			}
			else
			{
				databaseCopy3.ActivationPreference = databaseCopy3.ActivationPreference;
			}
			ADRawEntry adrawEntry = databaseCopy3;
			TDataObject dataObject = this.DataObject;
			adrawEntry.SetId(dataObject.Id.GetChildId(this.OwnerServer.Name));
			databaseCopy3.ParentObjectClass = ((this.DatabaseType == NewDatabaseTask<TDataObject>.ExchangeDatabaseType.Public) ? PublicFolderDatabase.MostDerivedClass : MailboxDatabase.MostDerivedClass);
			TDataObject dataObject2 = this.DataObject;
			ActivationPreferenceSetter<DatabaseCopy> activationPreferenceSetter = new ActivationPreferenceSetter<DatabaseCopy>(dataObject2.AllDatabaseCopies, databaseCopy3, (databaseCopy == null) ? EntryAction.Insert : EntryAction.Modify);
			UpdateResult updateResult = activationPreferenceSetter.UpdateCachedValues();
			if (updateResult == UpdateResult.AllChanged)
			{
				activationPreferenceSetter.SaveAllUpdatedValues(base.DataSession);
			}
			base.DataSession.Save(databaseCopy3);
			this.forcedReplicationSites = DagTaskHelper.DetermineRemoteSites(base.GlobalConfigSession, databaseCopy3.OriginatingServer, this.OwnerServer);
			if (this.forcedReplicationSites != null)
			{
				ITopologyConfigurationSession session = (ITopologyConfigurationSession)base.DataSession;
				TDataObject dataObject3 = this.DataObject;
				string objectIdentity = dataObject3.Identity.ToString();
				if (DagTaskHelper.ForceReplication(session, this.DataObject, this.forcedReplicationSites, objectIdentity, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)))
				{
					DagTaskHelper.ForceReplication(session, databaseCopy3, this.forcedReplicationSites, objectIdentity, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				}
			}
			TaskLogger.LogExit();
			return databaseCopy3;
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x0600573E RID: 22334 RVA: 0x0016A003 File Offset: 0x00168203
		protected Server OwnerServer
		{
			get
			{
				return this.ownerServer;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x0016A00B File Offset: 0x0016820B
		protected Database[] OwnerServerPublicFolderDatabases
		{
			get
			{
				return this.ownerServerPublicFolderDatabases;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x0016A013 File Offset: 0x00168213
		protected Database[] OwnerServerDatabases
		{
			get
			{
				return this.ownerServerDatabases;
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x06005741 RID: 22337 RVA: 0x0016A01C File Offset: 0x0016821C
		internal virtual IRecipientSession RecipientSessionForSystemMailbox
		{
			get
			{
				if (this.recipientSessionForSystemMailbox == null)
				{
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 647, "RecipientSessionForSystemMailbox", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\database\\NewDatabase.cs");
					tenantOrRootOrgRecipientSession.UseGlobalCatalog = false;
					this.recipientSessionForSystemMailbox = tenantOrRootOrgRecipientSession;
				}
				return this.recipientSessionForSystemMailbox;
			}
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x0016A074 File Offset: 0x00168274
		protected void ValidateFilePaths(bool recovery)
		{
			try
			{
				this.ValidateEdbFile(recovery);
				this.ValidateLogFolderPath();
			}
			catch (WmiException ex)
			{
				Exception exception = new InvalidOperationException(Strings.ErrorFailedToConnectToServer(this.OwnerServer.Name, ex.Message));
				ErrorCategory category = ErrorCategory.InvalidOperation;
				TDataObject dataObject = this.DataObject;
				base.WriteError(exception, category, dataObject.Identity);
			}
			catch (UnauthorizedAccessException ex2)
			{
				Exception exception2 = new InvalidOperationException(Strings.ErrorFailedToConnectToServer(this.OwnerServer.Name, ex2.Message));
				ErrorCategory category2 = ErrorCategory.InvalidOperation;
				TDataObject dataObject2 = this.DataObject;
				base.WriteError(exception2, category2, dataObject2.Identity);
			}
		}

		// Token: 0x06005743 RID: 22339 RVA: 0x0016A128 File Offset: 0x00168328
		protected void PrepareFilePaths(string databaseName, bool recovery, Database dataObject)
		{
			if (null == dataObject.LogFolderPath)
			{
				dataObject.LogFolderPath = NewDatabaseTask<TDataObject>.GetDefaultLogFolderPath(databaseName, this.ownerServer.DataPath.PathName, (ADObjectId)this.ownerServer.Identity, this.ownerServerDatabases, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (null == dataObject.SystemFolderPath)
			{
				dataObject.SystemFolderPath = dataObject.LogFolderPath;
			}
			if (null == dataObject.EdbFilePath)
			{
				string fileName = dataObject.Name + ".edb";
				try
				{
					dataObject.EdbFilePath = EdbFilePath.Parse(Path.Combine(LocalLongFullPath.ConvertInvalidCharactersInPathName(dataObject.LogFolderPath.PathName), LocalLongFullPath.ConvertInvalidCharactersInFileName(fileName)));
				}
				catch (ArgumentException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidOperation, dataObject);
				}
			}
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x0016A200 File Offset: 0x00168400
		private void ValidateLogFolderPath()
		{
			base.WriteVerbose(Strings.VerboseLogFolderPathUniqueUnderDAGCondition(this.LogFolderPath.PathName));
			if (!new DbLogLocationUniqueUnderDAGCondition(this.LogFolderPath.PathName, (ADObjectId)this.OwnerServer.Identity, new ADObjectId[]
			{
				(ADObjectId)this.OwnerServer.Identity
			}, this.ownerServerDatabases).Verify())
			{
				Exception exception = new ArgumentException(Strings.ErrorLogFolderPathNotUniqueUnderSameDAG(this.LogFolderPath.PathName), "LogFolderPath");
				ErrorCategory category = ErrorCategory.InvalidArgument;
				TDataObject dataObject = this.DataObject;
				base.WriteError(exception, category, dataObject.Identity);
			}
			if (!new PathOnFixedOrNetworkDriveCondition(this.OwnerServer.Fqdn, this.LogFolderPath.PathName).Verify())
			{
				Exception exception2 = new ArgumentException(Strings.ErrorPathIsNotOnFixedDrive("LogFolderPath"));
				ErrorCategory category2 = ErrorCategory.InvalidArgument;
				TDataObject dataObject2 = this.DataObject;
				base.WriteError(exception2, category2, dataObject2.Identity);
			}
			string fqdn = this.OwnerServer.Fqdn;
			string pathName = this.LogFolderPath.PathName;
			TDataObject dataObject3 = this.DataObject;
			if (!new LogLocationAvailableCondition(fqdn, pathName, dataObject3.LogFilePrefix).Verify())
			{
				Exception exception3 = new ArgumentException(Strings.ErrorLogFolderPathNotAvailable, "LogFolderPath");
				ErrorCategory category3 = ErrorCategory.InvalidArgument;
				TDataObject dataObject4 = this.DataObject;
				base.WriteError(exception3, category3, dataObject4.Identity);
			}
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x0016A35C File Offset: 0x0016855C
		private void ValidateEdbFile(bool recovery)
		{
			base.WriteVerbose(Strings.VerboseEdbFileLocationUniqueUnderDAGCondition(this.EdbFilePath.PathName));
			if (!new EdbFileLocationUniqueUnderDAGCondition(this.EdbFilePath.PathName, (ADObjectId)this.OwnerServer.Identity, new ADObjectId[]
			{
				(ADObjectId)this.OwnerServer.Identity
			}, this.ownerServerDatabases).Verify())
			{
				Exception exception = new ArgumentException(Strings.ErrorEdbFileLocationNotUniqueUnderSameDAG(this.EdbFilePath.PathName), "EdbFilePath");
				ErrorCategory category = ErrorCategory.InvalidArgument;
				TDataObject dataObject = this.DataObject;
				base.WriteError(exception, category, dataObject.Identity);
			}
			base.WriteVerbose(Strings.VerbosePathOnFixedOrNetworkDriveCondition(this.OwnerServer.Fqdn, this.EdbFilePath.PathName));
			if (!new PathOnFixedOrNetworkDriveCondition(this.OwnerServer.Fqdn, this.EdbFilePath.PathName).Verify())
			{
				Exception exception2 = new ArgumentException(Strings.ErrorEdbFileLocationNotOnFixedDrive(this.EdbFilePath.PathName), "EdbFilePath");
				ErrorCategory category2 = ErrorCategory.InvalidArgument;
				TDataObject dataObject2 = this.DataObject;
				base.WriteError(exception2, category2, dataObject2.Identity);
			}
			base.WriteVerbose(Strings.VerboseCheckDirectoryExistenceCondition(this.OwnerServer.Name, this.EdbFilePath.PathName));
			if (!new DirectoryNotExistCondition(this.OwnerServer.Fqdn, this.EdbFilePath.PathName).Verify())
			{
				Exception exception3 = new ArgumentException(Strings.ErrorEdbFilePathOccupiedByDirectory(this.EdbFilePath.PathName, this.OwnerServer.Name), "EdbFilePath");
				ErrorCategory category3 = ErrorCategory.InvalidArgument;
				TDataObject dataObject3 = this.DataObject;
				base.WriteError(exception3, category3, dataObject3.Identity);
			}
			base.WriteVerbose(Strings.VerboseCheckFileExistenceCondition(this.OwnerServer.Name, this.EdbFilePath.PathName));
			if (!new FileNotExistCondition(this.OwnerServer.Fqdn, this.EdbFilePath.PathName).Verify())
			{
				if (!recovery)
				{
					Exception exception4 = new ArgumentException(Strings.ErrorEdbFilePathOccupiedByFile(this.EdbFilePath.PathName, this.OwnerServer.Name), "EdbFilePath");
					ErrorCategory category4 = ErrorCategory.InvalidArgument;
					TDataObject dataObject4 = this.DataObject;
					base.WriteError(exception4, category4, dataObject4.Identity);
					return;
				}
				this.WriteWarning(Strings.RestoreUsingExistingFile(this.Name, this.EdbFilePath.PathName));
			}
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x0016A5B0 File Offset: 0x001687B0
		internal static string GetDefaultEdbFolderPath(ExchangeServer ownerServer, string databaseName)
		{
			string text = Path.Combine(ownerServer.DataPath.PathName, LocalLongFullPath.ConvertInvalidCharactersInFileName(databaseName));
			string path = string.Format("{0}{1}{2}", databaseName, "0000", ".edb");
			EdbFilePath edbFilePath = null;
			if (!EdbFilePath.TryParse(Path.Combine(text, path), out edbFilePath))
			{
				text = ownerServer.DataPath.PathName;
				if (!EdbFilePath.TryParse(Path.Combine(text, path), out edbFilePath))
				{
					text = EdbFilePath.DefaultEdbFilePath;
				}
			}
			return text;
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x0016A620 File Offset: 0x00168820
		internal static NonRootLocalLongFullPath GetDefaultLogFolderPath(string databaseName, string serverDataPath, ADObjectId OwnerServerId, IEnumerable<Database> existingDatabases, Task.TaskErrorLoggingDelegate logError)
		{
			string value = LocalLongFullPath.ConvertInvalidCharactersInFileName(databaseName);
			StringBuilder stringBuilder = new StringBuilder(serverDataPath);
			stringBuilder.Append(Path.DirectorySeparatorChar.ToString()).Append(value);
			string text = null;
			NonRootLocalLongFullPath nonRootLocalLongFullPath = null;
			Exception ex = null;
			try
			{
				text = stringBuilder.ToString();
				nonRootLocalLongFullPath = NonRootLocalLongFullPath.Parse(text);
				nonRootLocalLongFullPath.ValidateDirectoryPathLength();
				for (int i = 0; i < 200; i++)
				{
					bool flag = false;
					foreach (Database database in existingDatabases)
					{
						if (nonRootLocalLongFullPath == database.LogFolderPath)
						{
							if (database.Servers != null && database.Servers.Length != 0)
							{
								foreach (ADObjectId adobjectId in database.Servers)
								{
									if (OwnerServerId == adobjectId)
									{
										flag = true;
										break;
									}
								}
							}
							else if (OwnerServerId == database.Server)
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						break;
					}
					text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						stringBuilder,
						i + 1
					});
					nonRootLocalLongFullPath = NonRootLocalLongFullPath.Parse(text);
					nonRootLocalLongFullPath.ValidateDirectoryPathLength();
				}
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (FormatException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				nonRootLocalLongFullPath = null;
				if (logError != null)
				{
					logError(new InvalidOperationException(Strings.ErrorFailToParseLocalLongFullPath(databaseName, DatabaseSchema.LogFolderPath.Name, text, ex.Message)), ErrorCategory.InvalidOperation, databaseName);
				}
			}
			return nonRootLocalLongFullPath;
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x0016A7C8 File Offset: 0x001689C8
		internal static string GetDefaultEdbFileName(string databaseName)
		{
			return LocalLongFullPath.ConvertInvalidCharactersInFileName(databaseName + ".edb");
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x06005749 RID: 22345
		protected abstract NewDatabaseTask<TDataObject>.ExchangeDatabaseType DatabaseType { get; }

		// Token: 0x04003256 RID: 12886
		internal const string paramServer = "Server";

		// Token: 0x04003257 RID: 12887
		internal const string paramName = "Name";

		// Token: 0x04003258 RID: 12888
		internal const string paramEdbFilePath = "EdbFilePath";

		// Token: 0x04003259 RID: 12889
		internal const string paramLogFolderPath = "LogFolderPath";

		// Token: 0x0400325A RID: 12890
		protected ADObjectId[] forcedReplicationSites;

		// Token: 0x0400325B RID: 12891
		protected TDataObject preExistingDatabase;

		// Token: 0x0400325C RID: 12892
		protected DatabaseCopy dbCopy;

		// Token: 0x0400325D RID: 12893
		private Server ownerServer;

		// Token: 0x0400325E RID: 12894
		private Database[] ownerServerPublicFolderDatabases;

		// Token: 0x0400325F RID: 12895
		private Database[] ownerServerDatabases;

		// Token: 0x04003260 RID: 12896
		private IRecipientSession recipientSessionForSystemMailbox;

		// Token: 0x0200098C RID: 2444
		protected enum ExchangeDatabaseType
		{
			// Token: 0x04003262 RID: 12898
			Private,
			// Token: 0x04003263 RID: 12899
			Public
		}
	}
}
