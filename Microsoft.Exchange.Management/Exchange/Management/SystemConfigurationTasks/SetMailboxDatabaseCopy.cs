using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000994 RID: 2452
	[Cmdlet("Set", "MailboxDatabaseCopy", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetMailboxDatabaseCopy : SetTopologySystemConfigurationObjectTask<DatabaseCopyIdParameter, DatabaseCopy>
	{
		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x0016DEAB File Offset: 0x0016C0AB
		// (set) Token: 0x060057A4 RID: 22436 RVA: 0x0016DEC2 File Offset: 0x0016C0C2
		[Parameter(Mandatory = true, ParameterSetName = "ClearHostServer", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override DatabaseCopyIdParameter Identity
		{
			get
			{
				return (DatabaseCopyIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x060057A5 RID: 22437 RVA: 0x0016DED5 File Offset: 0x0016C0D5
		// (set) Token: 0x060057A6 RID: 22438 RVA: 0x0016DEEC File Offset: 0x0016C0EC
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public uint ActivationPreference
		{
			get
			{
				return (uint)base.Fields["ActivationPreference"];
			}
			set
			{
				base.Fields["ActivationPreference"] = value;
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x060057A7 RID: 22439 RVA: 0x0016DF04 File Offset: 0x0016C104
		// (set) Token: 0x060057A8 RID: 22440 RVA: 0x0016DF2A File Offset: 0x0016C12A
		[Parameter(Mandatory = true, ParameterSetName = "ClearHostServer")]
		public SwitchParameter ClearHostServer
		{
			get
			{
				return (SwitchParameter)(base.Fields["ClearHostServer"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ClearHostServer"] = value;
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x060057A9 RID: 22441 RVA: 0x0016DF42 File Offset: 0x0016C142
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMailboxDatabaseCopy(this.Identity.ToString());
			}
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x0016DF54 File Offset: 0x0016C154
		protected override bool IsKnownException(Exception e)
		{
			return AmExceptionHelper.IsKnownClusterException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x0016DF68 File Offset: 0x0016C168
		protected override void InternalStateReset()
		{
			this.m_dbCopy = null;
			this.m_databaseCopies = null;
			base.InternalStateReset();
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x0016DF80 File Offset: 0x0016C180
		protected override IConfigurable ResolveDataObject()
		{
			this.Identity.AllowInvalid = true;
			this.m_dbCopy = (DatabaseCopy)base.GetDataObject<DatabaseCopy>(this.Identity, base.DataSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Identity.ToString())));
			if (base.Fields["ActivationPreference"] != null && (this.m_dbCopy.HostServerUnlinked || this.ClearHostServer))
			{
				base.WriteError(new ArgumentException(Strings.ErrorActivationPreferenceNotAllowedWhenHostServerUnlinked), ErrorCategory.InvalidOperation, this.m_dbCopy);
			}
			Server server;
			DatabaseTasksHelper.ValidateDatabaseCopyActionTask(this.m_dbCopy, true, false, base.DataSession, this.RootId, new Task.TaskErrorLoggingDelegate(base.WriteError), Strings.ErrorMailboxDatabaseNotUnique(this.m_dbCopy.Identity.ToString()), null, out server);
			this.m_databaseCopies = this.m_dbCopy.GetAllDatabaseCopies();
			return this.m_dbCopy;
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x0016E088 File Offset: 0x0016C288
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			if (!this.ClearHostServer && this.m_dbCopy.HostServer != null)
			{
				base.VerifyIsWithinScopes((IConfigurationSession)base.DataSession, DatabaseTasksHelper.GetServerObject(new ServerIdParameter(this.m_dbCopy.HostServer), (IConfigurationSession)base.DataSession, this.RootId, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<Server>)), true, new DataAccessTask<DatabaseCopy>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			}
			Database database = this.m_dbCopy.GetDatabase<Database>();
			if (database != null)
			{
				MapiTaskHelper.VerifyDatabaseIsWithinScope(base.SessionSettings, database, new Task.ErrorLoggerDelegate(base.WriteError));
				if (database.Servers != null && database.Servers.Length > 0)
				{
					MapiTaskHelper.VerifyServerIsWithinScope(database, new Task.ErrorLoggerDelegate(base.WriteError), (ITopologyConfigurationSession)this.ConfigurationSession);
				}
			}
			if (this.DataObject.IsModified(DatabaseCopySchema.ReplayLag) && this.m_dbCopy.ReplayLagTime != TimeSpan.FromSeconds(0.0))
			{
				this.WriteWarning(Strings.WarningReplayLagTimeMustBeLessThanSafetyNetHoldTime);
			}
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x0016E1AC File Offset: 0x0016C3AC
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			DatabaseCopy databaseCopy = dataObject as DatabaseCopy;
			databaseCopy.InvalidHostServerAllowed = true;
			if (base.Fields["ActivationPreference"] != null)
			{
				Database database = databaseCopy.GetDatabase<Database>();
				database.CompleteAllCalculatedProperties();
				if (this.ActivationPreference < 1U || (ulong)this.ActivationPreference > (ulong)((long)database.AllDatabaseCopies.Length))
				{
					base.WriteError(new ArgumentException(Strings.ErrorActivationPreferenceInvalid(this.ActivationPreference, database.AllDatabaseCopies.Length)), ErrorCategory.InvalidArgument, null);
				}
				DatabaseCopy databaseCopy2 = null;
				if (this.ActivationPreference == 1U)
				{
					databaseCopy2 = databaseCopy;
				}
				else if (this.ActivationPreference != 0U)
				{
					databaseCopy2 = SetMailboxDatabaseCopy.GetDatabaseCopyOfPreference1(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), database, databaseCopy);
				}
				databaseCopy.ActivationPreference = (int)this.ActivationPreference;
				this.ValidateHighestActivationPreferenceServerLocation(database);
				if (databaseCopy2 != null)
				{
					base.WriteVerbose(Strings.UpdatingLegDnForDatabaseCopy(databaseCopy.Name));
					ITopologyConfigurationSession adSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, base.SessionSettings, 295, "StampChangesOn", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\database\\SetMailboxDatabaseCopy.cs");
					SetMailboxDatabaseCopy.UpdateServerLegdnForDatabaseSite(new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), adSession, databaseCopy2);
				}
				ActivationPreferenceSetter<DatabaseCopy> activationPreferenceSetter = new ActivationPreferenceSetter<DatabaseCopy>(database.AllDatabaseCopies, databaseCopy, EntryAction.Modify);
				UpdateResult updateResult = activationPreferenceSetter.UpdateCachedValues();
				if (updateResult == UpdateResult.AllChanged)
				{
					activationPreferenceSetter.SaveAllUpdatedValues(base.DataSession);
				}
				else if (updateResult == UpdateResult.NoChange)
				{
					databaseCopy.ResetChangeTracking();
				}
			}
			this.ClearHostServer;
			base.StampChangesOn(dataObject);
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x0016E308 File Offset: 0x0016C508
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (this.m_dbCopy.IsHostServerPresent)
			{
				Server server = (Server)base.DataSession.Read<Server>(this.m_dbCopy.HostServer);
				Database database = this.m_dbCopy.GetDatabase<Database>();
				DatabaseTasksHelper.RunConfigurationUpdaterRpc(server.Fqdn, database, server.AdminDisplayVersion, ReplayConfigChangeHints.DbCopyAdded, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x0016E3AC File Offset: 0x0016C5AC
		private void ValidateHighestActivationPreferenceServerLocation(Database owningDatabase)
		{
			Server server = (Server)base.GetDataObject<Server>(new ServerIdParameter(owningDatabase.Server), base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(owningDatabase.Server.Name)), new LocalizedString?(Strings.ErrorServerNotUnique(owningDatabase.Server.Name)));
			string text = "<null>";
			string text2 = text;
			string text3 = text;
			if (server.MailboxProvisioningAttributes != null)
			{
				MailboxProvisioningAttribute mailboxProvisioningAttribute = server.MailboxProvisioningAttributes.Attributes.FirstOrDefault((MailboxProvisioningAttribute anAttribute) => string.Equals(anAttribute.Key, "Location"));
				if (mailboxProvisioningAttribute != null)
				{
					text2 = mailboxProvisioningAttribute.Value;
				}
			}
			if (owningDatabase.MailboxProvisioningAttributes != null)
			{
				MailboxProvisioningAttribute mailboxProvisioningAttribute2 = owningDatabase.MailboxProvisioningAttributes.Attributes.FirstOrDefault((MailboxProvisioningAttribute anAttribute) => string.Equals(anAttribute.Key, "Location"));
				if (mailboxProvisioningAttribute2 != null)
				{
					text3 = mailboxProvisioningAttribute2.Value;
				}
			}
			if (!string.Equals(text2, text3, StringComparison.OrdinalIgnoreCase))
			{
				this.WriteWarning(Strings.Error_DatabaseLocationDoesNotMatchHighestActivationPreferenceCopyLocation("Location", text2, text3));
			}
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x0016E4E4 File Offset: 0x0016C6E4
		internal static DatabaseCopy GetDatabaseCopyOfPreference1(Task.TaskVerboseLoggingDelegate writeVerbose, Database owningDatabase, DatabaseCopy databaseCopy)
		{
			DatabaseCopy databaseCopy2 = null;
			KeyValuePair<ADObjectId, int>[] activationPreference = owningDatabase.ActivationPreference;
			ADObjectId highestpriority = activationPreference.FirstOrDefault((KeyValuePair<ADObjectId, int> kvp) => !kvp.Key.Equals(databaseCopy.Id)).Key;
			if (highestpriority != null)
			{
				databaseCopy2 = owningDatabase.DatabaseCopies.FirstOrDefault((DatabaseCopy dbCopy) => dbCopy.HostServer.Equals(highestpriority));
				writeVerbose(Strings.OtherDatabaseCopyHasHighestPreference(databaseCopy2.Name));
			}
			return databaseCopy2;
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x0016E568 File Offset: 0x0016C768
		internal static void UpdateServerLegdnForDatabaseSite(Task.TaskErrorLoggingDelegate writeErrorDelegate, Task.TaskVerboseLoggingDelegate writeVerboseDelegate, ITopologyConfigurationSession adSession, DatabaseCopy dbCopy)
		{
			ADObjectId hostServer = dbCopy.HostServer;
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				ServerSchema.ServerSite
			};
			MiniServer miniServer = adSession.ReadMiniServer(hostServer, properties);
			if (miniServer == null)
			{
				writeErrorDelegate(new ADServerNotFoundException(hostServer.ToString()), ErrorCategory.InvalidArgument, null);
			}
			IADToplogyConfigurationSession adSession2 = ADSessionFactory.CreateWrapper(adSession);
			SimpleAdObjectLookup<IADClientAccessArray> findClientAccessArray = new SimpleAdObjectLookup<IADClientAccessArray>(adSession2);
			SimpleMiniClientAccessServerOrArrayLookup findMiniClientAccessServer = new SimpleMiniClientAccessServerOrArrayLookup(adSession);
			ADObjectId serverSite = miniServer.ServerSite;
			LegacyDN legacyDN = ActiveManagerImplementation.FindClientAccessArrayOrServerFromSite(serverSite, miniServer.Id, findClientAccessArray, findMiniClientAccessServer, AdObjectLookupFlags.ReadThrough);
			ADObjectId parent = dbCopy.Id.Parent;
			Database database = adSession.Read<Database>(parent);
			if (legacyDN != null)
			{
				LegacyDN databaseLegacyDNFromRcaLegacyDN = Database.GetDatabaseLegacyDNFromRcaLegacyDN(legacyDN, database.IsPublicFolderDatabase);
				database.ExchangeLegacyDN = databaseLegacyDNFromRcaLegacyDN.ToString();
				writeVerboseDelegate(Strings.UpdatingLegDnForDatabaseToServer(database.Name, legacyDN.ToString(), legacyDN.ToString()));
				adSession.Save(database);
				return;
			}
			ExTraceGlobals.CmdletsTracer.TraceDebug<ADObjectId>(0L, "Could not find a new CAS machines for site '{0}'. Leaving the database's legdn unchanged.", serverSite);
		}

		// Token: 0x04003283 RID: 12931
		private const string ClearHostServerParamName = "ClearHostServer";

		// Token: 0x04003284 RID: 12932
		private const string ClearHostServerParamSetName = "ClearHostServer";

		// Token: 0x04003285 RID: 12933
		private DatabaseCopy m_dbCopy;

		// Token: 0x04003286 RID: 12934
		private DatabaseCopy[] m_databaseCopies;
	}
}
