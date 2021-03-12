using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020007A3 RID: 1955
	public abstract class MailboxQuarantineTaskBase : DataAccessTask<ADUser>
	{
		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x0011AF0A File Offset: 0x0011910A
		// (set) Token: 0x060044E2 RID: 17634 RVA: 0x0011AF12 File Offset: 0x00119112
		internal Database Database
		{
			get
			{
				return this.database;
			}
			set
			{
				this.database = value;
			}
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x0011AF1B File Offset: 0x0011911B
		// (set) Token: 0x060044E4 RID: 17636 RVA: 0x0011AF23 File Offset: 0x00119123
		internal Guid ExchangeGuid
		{
			get
			{
				return this.exchangeGuid;
			}
			set
			{
				this.exchangeGuid = value;
			}
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x0011AF2C File Offset: 0x0011912C
		// (set) Token: 0x060044E6 RID: 17638 RVA: 0x0011AF34 File Offset: 0x00119134
		internal string Server
		{
			get
			{
				return this.server;
			}
			set
			{
				this.server = value;
			}
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x0011AF3D File Offset: 0x0011913D
		// (set) Token: 0x060044E8 RID: 17640 RVA: 0x0011AF45 File Offset: 0x00119145
		internal DatabaseLocationInfo DbLocationInfo
		{
			get
			{
				return this.dbLocationInfo;
			}
			set
			{
				this.dbLocationInfo = value;
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x0011AF4E File Offset: 0x0011914E
		// (set) Token: 0x060044EA RID: 17642 RVA: 0x0011AF56 File Offset: 0x00119156
		internal RegistryKey RegistryKeyHive
		{
			get
			{
				return this.registryKeyHive;
			}
			set
			{
				this.registryKeyHive = value;
			}
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x0011AF5F File Offset: 0x0011915F
		// (set) Token: 0x060044EC RID: 17644 RVA: 0x0011AF76 File Offset: 0x00119176
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public GeneralMailboxIdParameter Identity
		{
			get
			{
				return (GeneralMailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x0011AF89 File Offset: 0x00119189
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || exception is StorageTransientException || DataAccessHelper.IsDataAccessKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x0011AFAC File Offset: 0x001191AC
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(null, null);
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			sessionSettings = ADSessionSettings.RescopeToSubtree(sessionSettings);
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 176, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\Mailbox\\MailboxQuarantineTaskBase.cs");
			this.recipientSession.UseGlobalCatalog = true;
			this.systemConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 186, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\Mailbox\\MailboxQuarantineTaskBase.cs");
			TaskLogger.LogExit();
			return this.systemConfigSession;
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x0011B044 File Offset: 0x00119244
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			ADObjectId adobjectId = null;
			ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.Identity, this.recipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.Identity.ToString())));
			ADUser aduser = adrecipient as ADUser;
			if (aduser != null)
			{
				this.exchangeGuid = aduser.ExchangeGuid;
				adobjectId = aduser.Database;
			}
			else
			{
				ADSystemMailbox adsystemMailbox = adrecipient as ADSystemMailbox;
				if (adsystemMailbox != null)
				{
					this.exchangeGuid = adsystemMailbox.ExchangeGuid;
					adobjectId = adsystemMailbox.Database;
				}
			}
			if (adobjectId == null)
			{
				base.WriteError(new MdbAdminTaskException(Strings.ErrorInvalidObjectMissingCriticalProperty(adrecipient.GetType().Name, this.Identity.ToString(), IADMailStorageSchema.Database.Name)), ErrorCategory.InvalidArgument, adrecipient);
			}
			DatabaseIdParameter id = new DatabaseIdParameter(adobjectId);
			this.database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(id, this.systemConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(adobjectId.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(adobjectId.ToString())));
			try
			{
				this.dbLocationInfo = ActiveManager.GetActiveManagerInstance().GetServerForDatabase(this.database.Id.ObjectGuid);
				this.server = this.DbLocationInfo.ServerFqdn.Split(new char[]
				{
					'.'
				})[0];
			}
			catch (DatabaseNotFoundException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
			}
			catch (ObjectNotFoundException exception2)
			{
				base.WriteError(exception2, ExchangeErrorCategory.ServerOperation, null);
			}
			try
			{
				if (this.registryKeyHive == null)
				{
					this.registryKeyHive = this.OpenHive(this.dbLocationInfo.ServerFqdn);
				}
			}
			catch (IOException ex)
			{
				base.WriteError(new FailedMailboxQuarantineException(this.Identity.ToString(), ex.ToString()), ErrorCategory.ObjectNotFound, null);
			}
			catch (SecurityException ex2)
			{
				base.WriteError(new FailedMailboxQuarantineException(this.Identity.ToString(), ex2.ToString()), ErrorCategory.SecurityError, null);
			}
			catch (UnauthorizedAccessException ex3)
			{
				base.WriteError(new FailedMailboxQuarantineException(this.Identity.ToString(), ex3.ToString()), ErrorCategory.PermissionDenied, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x0011B294 File Offset: 0x00119494
		internal RegistryKey OpenHive(string server)
		{
			if (string.IsNullOrEmpty(server))
			{
				throw new ArgumentNullException("server");
			}
			return RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, server);
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x0011B2B4 File Offset: 0x001194B4
		internal bool GetMailboxQuarantineStatus()
		{
			if (this.mapiSession == null)
			{
				this.mapiSession = new MapiAdministrationSession(this.DbLocationInfo.ServerLegacyDN, Fqdn.Parse(this.DbLocationInfo.ServerFqdn));
			}
			StoreMailboxIdParameter id = StoreMailboxIdParameter.Parse(this.exchangeGuid.ToString());
			bool isQuarantined;
			using (MailboxStatistics mailboxStatistics = (MailboxStatistics)base.GetDataObject<MailboxStatistics>(id, this.mapiSession, MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(this.database), new LocalizedString?(Strings.ErrorStoreMailboxNotFound(this.Identity.ToString(), this.Database.Identity.ToString())), new LocalizedString?(Strings.ErrorStoreMailboxNotUnique(this.Identity.ToString(), this.Database.Identity.ToString()))))
			{
				isQuarantined = mailboxStatistics.IsQuarantined;
			}
			return isQuarantined;
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x0011B394 File Offset: 0x00119594
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.registryKeyHive != null)
				{
					this.registryKeyHive.Close();
					this.RegistryKeyHive = null;
				}
				if (this.mapiSession != null)
				{
					this.mapiSession.Dispose();
					this.mapiSession = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04002A90 RID: 10896
		internal const string ParameterIdentity = "Identity";

		// Token: 0x04002A91 RID: 10897
		private Database database;

		// Token: 0x04002A92 RID: 10898
		private Guid exchangeGuid;

		// Token: 0x04002A93 RID: 10899
		private string server;

		// Token: 0x04002A94 RID: 10900
		private DatabaseLocationInfo dbLocationInfo;

		// Token: 0x04002A95 RID: 10901
		private RegistryKey registryKeyHive;

		// Token: 0x04002A96 RID: 10902
		private IRecipientSession recipientSession;

		// Token: 0x04002A97 RID: 10903
		private IConfigurationSession systemConfigSession;

		// Token: 0x04002A98 RID: 10904
		private MapiAdministrationSession mapiSession;

		// Token: 0x04002A99 RID: 10905
		internal static readonly string QuarantineBaseRegistryKey = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS";
	}
}
