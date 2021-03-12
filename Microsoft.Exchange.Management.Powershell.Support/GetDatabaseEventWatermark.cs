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
	// Token: 0x02000031 RID: 49
	[Cmdlet("Get", "DatabaseEventWatermark")]
	public sealed class GetDatabaseEventWatermark : GetMapiObjectTask<DatabaseIdParameter, DatabaseEventWatermark>
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B316 File Offset: 0x00009516
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000B31E File Offset: 0x0000951E
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B327 File Offset: 0x00009527
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000B33E File Offset: 0x0000953E
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000B351 File Offset: 0x00009551
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000B377 File Offset: 0x00009577
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000B38F File Offset: 0x0000958F
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000B3A6 File Offset: 0x000095A6
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000B3B9 File Offset: 0x000095B9
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000B3C1 File Offset: 0x000095C1
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000B3CA File Offset: 0x000095CA
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000B3E1 File Offset: 0x000095E1
		[Parameter(Mandatory = false)]
		public Guid? MailboxGuid
		{
			get
			{
				return (Guid?)base.Fields["MailboxGuid"];
			}
			set
			{
				base.Fields["MailboxGuid"] = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000B3F9 File Offset: 0x000095F9
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000B401 File Offset: 0x00009601
		[Parameter(Mandatory = false)]
		public Guid? ConsumerGuid
		{
			get
			{
				return this.consumerGuid;
			}
			set
			{
				this.consumerGuid = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000B40A File Offset: 0x0000960A
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return GetDatabaseEventWatermark.defaultResultSize;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B414 File Offset: 0x00009614
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

		// Token: 0x06000286 RID: 646 RVA: 0x0000B494 File Offset: 0x00009694
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

		// Token: 0x06000287 RID: 647 RVA: 0x0000B507 File Offset: 0x00009707
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.FreeMapiSession();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B519 File Offset: 0x00009719
		private void FreeMapiSession()
		{
			if (this.mapiSession != null)
			{
				this.mapiSession.Dispose();
				this.mapiSession = null;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B538 File Offset: 0x00009738
		private void ResolveParameters()
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

		// Token: 0x0600028A RID: 650 RVA: 0x0000B74F File Offset: 0x0000994F
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.server = null;
			this.FreeMapiSession();
			this.ResolveParameters();
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B774 File Offset: 0x00009974
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			foreach (Database database in this.databases)
			{
				try
				{
					IEnumerable<DatabaseEventWatermark> enumerable = this.ReadWatermarks(this.mapiSession, database, this.consumerGuid, this.MailboxGuid);
					foreach (DatabaseEventWatermark dataObject in enumerable)
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
			TaskLogger.LogExit();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B854 File Offset: 0x00009A54
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || DataAccessHelper.IsDataAccessKnownException(e);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C094 File Offset: 0x0000A294
		internal IEnumerable<DatabaseEventWatermark> ReadWatermarks(MapiSession mapiSession, Database database, Guid? consumerGuid, Guid? mailboxGuid)
		{
			if (mapiSession == null)
			{
				throw new ArgumentException("mapiSession");
			}
			if (database == null)
			{
				throw new ArgumentException("database");
			}
			Watermark[] wms = null;
			long lastCounter = 0L;
			DatabaseId databaseId = MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(database);
			if (consumerGuid != null)
			{
				MapiEventManager eventManager = MapiEventManager.Create(mapiSession.Administration, consumerGuid.Value, databaseId.Guid);
				mapiSession.InvokeWithWrappedException(delegate()
				{
					lastCounter = eventManager.ReadLastEvent().EventCounter;
					if (mailboxGuid != null)
					{
						Watermark watermark = eventManager.GetWatermark(mailboxGuid.Value);
						wms = ((watermark == null) ? new Watermark[0] : new Watermark[]
						{
							watermark
						});
						return;
					}
					wms = eventManager.GetWatermarks();
				}, Strings.ErrorCannotReadDatabaseWatermarks(databaseId.ToString()), databaseId);
				foreach (Watermark wm in wms)
				{
					yield return new DatabaseEventWatermark(wm, databaseId, lastCounter, this.server, database.Server.ObjectGuid == this.server.Guid);
				}
			}
			else
			{
				GetDatabaseEventWatermark.<>c__DisplayClass7 CS$<>8__locals3 = new GetDatabaseEventWatermark.<>c__DisplayClass7();
				CS$<>8__locals3.eventManager = MapiEventManager.Create(mapiSession.Administration, Guid.Empty, databaseId.Guid);
				if (mailboxGuid != null)
				{
					CS$<>8__locals3.mailboxGuids = new Guid[]
					{
						mailboxGuid.Value
					};
				}
				else if (database.IsPublicFolderDatabase)
				{
					CS$<>8__locals3.mailboxGuids = new Guid[]
					{
						Guid.Empty
					};
				}
				else
				{
					PropValue[][] mailboxTable = null;
					mapiSession.InvokeWithWrappedException(delegate()
					{
						mailboxTable = mapiSession.Administration.GetMailboxTable(databaseId.Guid, new PropTag[]
						{
							PropTag.UserGuid
						});
					}, Strings.ErrorCannotReadDatabaseWatermarks(databaseId.ToString()), databaseId);
					CS$<>8__locals3.mailboxGuids = new Guid[mailboxTable.Length + 1];
					CS$<>8__locals3.mailboxGuids[0] = Guid.Empty;
					for (int j = 0; j < mailboxTable.Length; j++)
					{
						CS$<>8__locals3.mailboxGuids[j + 1] = new Guid(mailboxTable[j][0].GetBytes());
					}
				}
				CS$<>8__locals3.databaseVersionGuid = Guid.Empty;
				int i;
				for (i = 0; i < CS$<>8__locals3.mailboxGuids.Length; i++)
				{
					mapiSession.InvokeWithWrappedException(delegate()
					{
						lastCounter = CS$<>8__locals3.eventManager.ReadLastEvent().EventCounter;
						wms = mapiSession.Administration.GetWatermarksForMailbox(databaseId.Guid, ref CS$<>8__locals3.databaseVersionGuid, CS$<>8__locals3.mailboxGuids[i]);
					}, Strings.ErrorCannotReadDatabaseWatermarks(databaseId.ToString()), databaseId);
					foreach (Watermark wm2 in wms)
					{
						yield return new DatabaseEventWatermark(wm2, databaseId, lastCounter, this.server, database.Server.ObjectGuid == this.server.Guid);
					}
				}
			}
			yield break;
		}

		// Token: 0x040000E9 RID: 233
		private static readonly Unlimited<uint> defaultResultSize = new Unlimited<uint>(1000U);

		// Token: 0x040000EA RID: 234
		private MapiAdministrationSession mapiSession;

		// Token: 0x040000EB RID: 235
		private List<Database> databases = new List<Database>();

		// Token: 0x040000EC RID: 236
		private Server server;

		// Token: 0x040000ED RID: 237
		private Guid? consumerGuid;
	}
}
