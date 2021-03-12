using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000030 RID: 48
	[Cmdlet("Get", "DatabaseEvent")]
	public sealed class GetDatabaseEvent : GetMapiObjectTask<DatabaseIdParameter, DatabaseEvent>
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A796 File Offset: 0x00008996
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000A79E File Offset: 0x0000899E
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Database")]
		public override DatabaseIdParameter Identity
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A7A7 File Offset: 0x000089A7
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000A7BE File Offset: 0x000089BE
		[Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = "Server")]
		public override ServerIdParameter Server
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A7D1 File Offset: 0x000089D1
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000A7F7 File Offset: 0x000089F7
		[Parameter(Mandatory = false, ParameterSetName = "Server")]
		public SwitchParameter IncludePassive
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludePassive"] ?? false);
			}
			set
			{
				base.Fields["IncludePassive"] = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A80F File Offset: 0x00008A0F
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000A826 File Offset: 0x00008A26
		[Parameter(Mandatory = false, ParameterSetName = "Database")]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter CopyOnServer
		{
			get
			{
				return (ServerIdParameter)base.Fields["CopyOnServer"];
			}
			set
			{
				base.Fields["CopyOnServer"] = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A839 File Offset: 0x00008A39
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000A841 File Offset: 0x00008A41
		[ValidateRange(1L, 9223372036854775807L)]
		[Parameter(Mandatory = false)]
		public long StartCounter
		{
			get
			{
				return this.startCounter;
			}
			set
			{
				this.startCounter = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A84A File Offset: 0x00008A4A
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000A852 File Offset: 0x00008A52
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000A85B File Offset: 0x00008A5B
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000A880 File Offset: 0x00008A80
		[Parameter(Mandatory = false)]
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)(base.Fields["MailboxGuid"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["MailboxGuid"] = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A898 File Offset: 0x00008A98
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000A8B9 File Offset: 0x00008AB9
		[Parameter(Mandatory = false)]
		public DatabaseEventNames EventNames
		{
			get
			{
				return (DatabaseEventNames)(base.Fields["EventNames"] ?? 0);
			}
			set
			{
				base.Fields["EventNames"] = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A8D1 File Offset: 0x00008AD1
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000A8DE File Offset: 0x00008ADE
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeMoveDestinationEvents
		{
			get
			{
				return this.includeMoveDestinationEvents;
			}
			set
			{
				this.includeMoveDestinationEvents = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A8EC File Offset: 0x00008AEC
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return GetDatabaseEvent.defaultResultSize;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A8F3 File Offset: 0x00008AF3
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskBaseModuleFactory();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A8FC File Offset: 0x00008AFC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (!this.ResultSize.IsUnlimited && (this.ResultSize.Value == 0U || 2147483647U < this.ResultSize.Value))
			{
				base.ThrowTerminatingError(new InvalidOperationException(Strings.ErrorResultSizeOutOfRange(1.ToString(), int.MaxValue.ToString())), ErrorCategory.InvalidArgument, null);
			}
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A97C File Offset: 0x00008B7C
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			if (this.mapiSession == null)
			{
				this.mapiSession = new MapiAdministrationSession(this.server.ExchangeLegacyDN, Fqdn.Parse(this.server.Fqdn));
			}
			else
			{
				this.mapiSession.RedirectServer(this.server.ExchangeLegacyDN, Fqdn.Parse(this.server.Fqdn));
			}
			TaskLogger.LogExit();
			return this.mapiSession;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A9EF File Offset: 0x00008BEF
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.FreeMapiSession();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000AA01 File Offset: 0x00008C01
		private void FreeMapiSession()
		{
			if (this.mapiSession != null)
			{
				this.mapiSession.Dispose();
				this.mapiSession = null;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000AA20 File Offset: 0x00008C20
		private void ResolveDatabaseAndServer()
		{
			ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
			if (this.Server != null)
			{
				this.server = MapiTaskHelper.GetMailboxServer(this.Server, (ITopologyConfigurationSession)this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			else if (this.Identity != null)
			{
				DatabaseIdParameter identity = this.Identity;
				identity.AllowLegacy = false;
				Database database = (Database)base.GetDataObject<Database>(identity, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(identity.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(identity.ToString())));
				ServerIdParameter serverIdParameter;
				if (this.CopyOnServer != null)
				{
					serverIdParameter = this.CopyOnServer;
				}
				else
				{
					if (database.Server == null)
					{
						base.WriteError(new MdbAdminTaskException(Strings.ErrorInvalidObjectMissingCriticalProperty(typeof(Database).Name, identity.ToString(), DatabaseSchema.Server.Name)), ErrorCategory.InvalidArgument, database);
					}
					DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(database.Guid);
					serverIdParameter = ServerIdParameter.Parse(serverForDatabase.ServerFqdn);
				}
				this.server = (Server)base.GetDataObject<Server>(serverIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
				if (!this.server.IsExchange2007OrLater || !this.server.IsMailboxServer)
				{
					base.WriteError(new MdbAdminTaskException(Strings.ErrorLocalServerIsNotMailboxServer), ErrorCategory.InvalidArgument, this.server);
				}
				this.databases = new List<Database>(new Database[]
				{
					database
				});
			}
			else
			{
				ServerIdParameter serverIdParameter2 = new ServerIdParameter();
				this.server = (Server)base.GetDataObject<Server>(serverIdParameter2, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorLocalMachineIsNotExchangeServer), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter2.ToString())));
				if (!this.server.IsExchange2007OrLater || !this.server.IsMailboxServer)
				{
					base.WriteError(new MdbAdminTaskException(Strings.ErrorLocalServerIsNotMailboxServer), ErrorCategory.InvalidArgument, this.server);
				}
			}
			if (this.databases.Count == 0)
			{
				this.databases = StoreCommon.PopulateDatabasesFromServer(activeManagerInstance, this.server, this.IncludePassive);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000AC37 File Offset: 0x00008E37
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.server = null;
			this.FreeMapiSession();
			this.ResolveDatabaseAndServer();
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000AC5C File Offset: 0x00008E5C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			Restriction restriction = this.BuildRestriction();
			int num = (int)(this.ResultSize.IsUnlimited ? uint.MaxValue : this.ResultSize.Value);
			if ((0 < num && 2147483647 > num) || -1 == num)
			{
				foreach (Database database in this.databases)
				{
					try
					{
						IEnumerable<DatabaseEvent> enumerable = this.ReadEvents(this.mapiSession, database, this.startCounter, restriction, this.includeMoveDestinationEvents, num);
						foreach (DatabaseEvent dataObject in enumerable)
						{
							this.WriteResult(dataObject);
						}
					}
					catch (DatabaseUnavailableException ex)
					{
						if (this.Identity == null)
						{
							base.WriteWarning(ex.Message);
						}
						else
						{
							base.WriteError(ex, ErrorCategory.ResourceUnavailable, database);
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AD84 File Offset: 0x00008F84
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || DataAccessHelper.IsDataAccessKnownException(e);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000AD98 File Offset: 0x00008F98
		private Restriction BuildRestriction()
		{
			if (this.IsFieldSet("MailboxGuid") && this.IsFieldSet("EventNames"))
			{
				return Restriction.And(new Restriction[]
				{
					Restriction.EQ(PropTag.EventMailboxGuid, this.MailboxGuid.ToByteArray()),
					Restriction.BitMaskNonZero(PropTag.EventMask, (int)this.EventNames)
				});
			}
			if (this.IsFieldSet("MailboxGuid"))
			{
				return Restriction.EQ(PropTag.EventMailboxGuid, this.MailboxGuid.ToByteArray());
			}
			if (this.IsFieldSet("EventNames"))
			{
				return Restriction.BitMaskNonZero(PropTag.EventMask, (int)this.EventNames);
			}
			return null;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000AE40 File Offset: 0x00009040
		private bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000B2A0 File Offset: 0x000094A0
		internal IEnumerable<DatabaseEvent> ReadEvents(MapiSession mapiSession, Database database, long startCounter, Restriction restriction, bool includeMoveDestinationEvents, int resultSize)
		{
			if (mapiSession == null)
			{
				throw new ArgumentException("mapiSession");
			}
			if (database == null)
			{
				throw new ArgumentException("database");
			}
			int count = 0;
			long endCounter = (0L < startCounter) ? (startCounter - 1L) : 0L;
			MapiEvent[] events = null;
			DatabaseId databaseId = MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(database);
			MapiEventManager eventManager = MapiEventManager.Create(mapiSession.Administration, Guid.NewGuid(), databaseId.Guid);
			for (;;)
			{
				startCounter = endCounter + 1L;
				ReadEventsFlags flags = includeMoveDestinationEvents ? ReadEventsFlags.IncludeMoveDestinationEvents : ReadEventsFlags.None;
				mapiSession.InvokeWithWrappedException(delegate()
				{
					events = eventManager.ReadEvents(startCounter, (0 < resultSize) ? resultSize : 1000, 0, restriction, flags, out endCounter);
				}, Strings.ErrorCannotReadDatabaseEvents(databaseId.ToString()), databaseId);
				foreach (MapiEvent mapiEvent in events)
				{
					yield return new DatabaseEvent(mapiEvent, databaseId, this.server, database.Server.ObjectGuid == this.server.Guid);
					count++;
					if (0 < resultSize && count == resultSize)
					{
						goto Block_6;
					}
				}
				if (endCounter == startCounter)
				{
					goto Block_8;
				}
			}
			Block_6:
			yield break;
			Block_8:
			yield break;
		}

		// Token: 0x040000E1 RID: 225
		private const string ParameterMailboxGuid = "MailboxGuid";

		// Token: 0x040000E2 RID: 226
		private const string ParameterEventNames = "EventNames";

		// Token: 0x040000E3 RID: 227
		private static readonly Unlimited<uint> defaultResultSize = new Unlimited<uint>(1000U);

		// Token: 0x040000E4 RID: 228
		private MapiAdministrationSession mapiSession;

		// Token: 0x040000E5 RID: 229
		private Server server;

		// Token: 0x040000E6 RID: 230
		private long startCounter = 1L;

		// Token: 0x040000E7 RID: 231
		private bool includeMoveDestinationEvents;

		// Token: 0x040000E8 RID: 232
		private List<Database> databases = new List<Database>();
	}
}
