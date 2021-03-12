using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000032 RID: 50
	public abstract class GetFolderObjectBase<TDataObject> : GetTaskBase<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000C0F2 File Offset: 0x0000A2F2
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000C109 File Offset: 0x0000A309
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["DatabaseId"];
			}
			set
			{
				base.Fields["DatabaseId"] = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C11C File Offset: 0x0000A31C
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000C133 File Offset: 0x0000A333
		[Parameter(Mandatory = true)]
		public MapiEntryId FolderEntryId
		{
			get
			{
				return (MapiEntryId)base.Fields["FolderEntryId"];
			}
			set
			{
				base.Fields["FolderEntryId"] = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C146 File Offset: 0x0000A346
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000C175 File Offset: 0x0000A375
		[Parameter(Mandatory = true)]
		public Guid MailboxGuid
		{
			get
			{
				if (!base.Fields.IsModified("MailboxGuid"))
				{
					return Guid.Empty;
				}
				return (Guid)base.Fields["MailboxGuid"];
			}
			set
			{
				base.Fields["MailboxGuid"] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000C18D File Offset: 0x0000A38D
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000C195 File Offset: 0x0000A395
		[Parameter]
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

		// Token: 0x06000298 RID: 664 RVA: 0x0000C19E File Offset: 0x0000A39E
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(MapiPermanentException).IsInstanceOfType(exception) || typeof(MapiRetryableException).IsInstanceOfType(exception);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		internal override IConfigurationSession ConfigurationSession
		{
			get
			{
				return this.configurationSession;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		internal MapiAdministrationSession MapiSession
		{
			get
			{
				return this.mapiSession;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		internal Server TargetServer
		{
			get
			{
				return this.targetServer;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		internal Database TargetDatabase
		{
			get
			{
				return this.targetDatabase;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			if (this.mapiSession == null)
			{
				this.mapiSession = new MapiAdministrationSession(this.TargetServer.ExchangeLegacyDN, Fqdn.Parse(this.TargetServer.Fqdn));
			}
			else
			{
				this.mapiSession.RedirectServer(this.TargetServer.ExchangeLegacyDN, Fqdn.Parse(this.TargetServer.Fqdn));
			}
			TaskLogger.LogExit();
			return this.mapiSession;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C268 File Offset: 0x0000A468
		private void ResolveDatabaseAndServer()
		{
			DatabaseIdParameter database = this.Database;
			database.AllowLegacy = false;
			Database database2 = (Database)base.GetDataObject<Database>(database, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(database.ToString())));
			if (database2.Server == null)
			{
				base.WriteError(new MdbAdminTaskException(Strings.ErrorInvalidObjectMissingCriticalProperty(typeof(Database).Name, database.ToString(), DatabaseSchema.Server.Name)), ErrorCategory.InvalidArgument, database2);
			}
			ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
			DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(database2.Guid);
			ServerIdParameter serverIdParameter = ServerIdParameter.Parse(serverForDatabase.ServerFqdn);
			this.targetServer = (Server)base.GetDataObject<Server>(serverIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
			if (!this.TargetServer.IsE14OrLater || !this.TargetServer.IsMailboxServer)
			{
				base.WriteError(new MdbAdminTaskException(Strings.ErrorLocalServerIsNotMailboxServer), ErrorCategory.InvalidArgument, this.TargetServer);
			}
			this.targetDatabase = database2;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C384 File Offset: 0x0000A584
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.FreeMapiSession();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C396 File Offset: 0x0000A596
		private void FreeMapiSession()
		{
			if (this.mapiSession != null)
			{
				this.mapiSession.Dispose();
				this.mapiSession = null;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 236, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Support\\Store\\GetFolderObjectBase.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C402 File Offset: 0x0000A602
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.targetServer = null;
			this.targetDatabase = null;
			this.FreeMapiSession();
			this.ResolveDatabaseAndServer();
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x040000EE RID: 238
		private const string ParameterDatabaseId = "DatabaseId";

		// Token: 0x040000EF RID: 239
		private const string ParameterFolderEntryId = "FolderEntryId";

		// Token: 0x040000F0 RID: 240
		private const string ParameterMailboxId = "MailboxGuid";

		// Token: 0x040000F1 RID: 241
		private MapiAdministrationSession mapiSession;

		// Token: 0x040000F2 RID: 242
		private IConfigurationSession configurationSession;

		// Token: 0x040000F3 RID: 243
		private Server targetServer;

		// Token: 0x040000F4 RID: 244
		private Database targetDatabase;
	}
}
