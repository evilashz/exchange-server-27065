using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Directory.ExchangeDirectory;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Provisioning.LoadBalancing;

namespace Microsoft.Exchange.MailboxLoadBalance.Providers
{
	// Token: 0x020000C3 RID: 195
	internal class DirectoryProvider : IDirectoryProvider
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0001044C File Offset: 0x0000E64C
		public DirectoryProvider(IClientFactory clientFactory, Server localServer, ILoadBalanceSettings settings, IEnumerable<IDirectoryListener> listeners, ILogger logger, LoadBalanceAnchorContext context)
		{
			this.clientFactory = clientFactory;
			this.localServer = localServer;
			this.settings = settings;
			this.logger = logger;
			this.context = context;
			this.listeners = new List<IDirectoryListener>(listeners ?? ((IEnumerable<IDirectoryListener>)Array<IDirectoryListener>.Empty));
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001049F File Offset: 0x0000E69F
		private TopologyConfigurationSessionAdapter ConfigurationSession
		{
			get
			{
				return TopologyConfigurationSessionAdapter.Instance.Value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000104AB File Offset: 0x0000E6AB
		private RecipientSessionAdapter RecipientSession
		{
			get
			{
				return RecipientSessionAdapter.Instance.Value;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000104B8 File Offset: 0x0000E6B8
		public IRequest CreateRequestToMove(DirectoryMailbox directoryMailbox, DirectoryIdentity targetIdentity, string batchName, ILogger requestLogger)
		{
			ADObjectId id = this.RecipientSession.FindRecipient(directoryMailbox.Identity).Id;
			return new InjectMoveRequest(batchName, directoryMailbox, requestLogger, false, RequestPriority.Lowest, id, targetIdentity, this.settings, this.context.CmdletPool);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00010514 File Offset: 0x0000E714
		public IEnumerable<DirectoryServer> GetActivationPreferenceForDatabase(DirectoryDatabase database)
		{
			Database database2 = this.ConfigurationSession.Read<Database>(database.Identity.ADObjectId);
			if (database2 == null)
			{
				throw new DatabaseNotFoundPermanentException(database.Name);
			}
			return from ap in database2.ActivationPreference
			orderby ap.Value
			select ap into activationPreference
			select this.GetServer(activationPreference.Key);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00010580 File Offset: 0x0000E780
		public IEnumerable<DirectoryDatabase> GetCachedDatabasesForProvisioning()
		{
			List<MailboxDatabase> allCachedDatabasesForProvisioning = PhysicalResourceLoadBalancing.GetAllCachedDatabasesForProvisioning(null, new LogMessageDelegate(this.LogVerboseMessage));
			return allCachedDatabasesForProvisioning.Select(new Func<MailboxDatabase, DirectoryDatabase>(this.DirectoryDatabaseFromDatabase));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000105B4 File Offset: 0x0000E7B4
		public DirectoryDatabase GetDatabase(Guid guid)
		{
			MailboxDatabase mailboxDatabase = this.ConfigurationSession.Read<MailboxDatabase>(new ADObjectId(guid));
			if (mailboxDatabase == null)
			{
				throw new DatabaseNotFoundException(guid.ToString());
			}
			return this.DirectoryDatabaseFromDatabase(mailboxDatabase);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000107D4 File Offset: 0x0000E9D4
		public IEnumerable<DirectoryDatabaseAvailabilityGroup> GetDatabaseAvailabilityGroups()
		{
			bool returnedDags = false;
			foreach (DatabaseAvailabilityGroup dag in this.ConfigurationSession.FindAllPaged<DatabaseAvailabilityGroup>())
			{
				returnedDags = true;
				yield return new DirectoryDatabaseAvailabilityGroup(this, DirectoryIdentity.CreateFromADObjectId(dag.Id, DirectoryObjectType.DatabaseAvailabilityGroup));
			}
			if (!returnedDags)
			{
				yield return new DummyDatabaseAvailabilityGroup(this);
			}
			yield break;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public DirectoryDatabase GetDatabaseForMailbox(DirectoryIdentity identity)
		{
			LoadBalancingMiniRecipient loadBalancingMiniRecipient = this.RecipientSession.FindRecipient(identity);
			if (loadBalancingMiniRecipient.Database == null)
			{
				return null;
			}
			return this.GetDatabase(loadBalancingMiniRecipient.Database.ObjectGuid);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00010AE0 File Offset: 0x0000ECE0
		public IEnumerable<DirectoryDatabase> GetDatabasesOwnedByServer(DirectoryServer server)
		{
			IOperationRetryManager retryManager = LoadBalanceOperationRetryManager.Create(this.logger);
			foreach (MailboxDatabase database in this.ConfigurationSession.GetDatabasesOnServer(server.Identity))
			{
				DirectoryDatabase result = null;
				MailboxDatabase databaseCopy = database;
				OperationRetryManagerResult operationResult = retryManager.TryRun(delegate
				{
					result = this.DirectoryDatabaseFromDatabase(databaseCopy);
				});
				if (!operationResult.Succeeded)
				{
					this.logger.LogError(operationResult.Exception, "Could not retrieve database {0} for server {1}.", new object[]
					{
						databaseCopy.Name,
						server.Name
					});
				}
				else if (result != null)
				{
					yield return result;
				}
			}
			yield break;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00010B24 File Offset: 0x0000ED24
		public DirectoryObject GetDirectoryObject(DirectoryIdentity directoryObjectIdentity)
		{
			AnchorUtil.ThrowOnNullArgument(directoryObjectIdentity, "directoryObjectIdentity");
			switch (directoryObjectIdentity.ObjectType)
			{
			case DirectoryObjectType.Forest:
				return this.GetLocalForest();
			case DirectoryObjectType.DatabaseAvailabilityGroup:
				return this.GetDatabaseAvailabilityGroups().FirstOrDefault((DirectoryDatabaseAvailabilityGroup d) => d.Guid == directoryObjectIdentity.Guid);
			case DirectoryObjectType.Server:
				return this.GetServer(directoryObjectIdentity.Guid);
			case DirectoryObjectType.Database:
				return this.GetDatabase(directoryObjectIdentity.Guid);
			case DirectoryObjectType.Mailbox:
			case DirectoryObjectType.CloudArchive:
			case DirectoryObjectType.ConsumerMailbox:
				return this.GetMailboxObject(directoryObjectIdentity);
			case DirectoryObjectType.ConstraintSet:
				return this.GetConstraintSetObject(directoryObjectIdentity);
			}
			throw new NotSupportedException(string.Format("ObjectType '{0}' is not supported.", directoryObjectIdentity.ObjectType));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00010E20 File Offset: 0x0000F020
		public IEnumerable<NonConnectedMailbox> GetDisconnectedMailboxesForDatabase(DirectoryDatabase database)
		{
			using (IPhysicalDatabase physicalDatabase = this.clientFactory.GetPhysicalDatabaseConnection(database))
			{
				foreach (NonConnectedMailbox disconnectedMailbox in this.GetDisconnectedMailboxesForDatabaseInternal(database, physicalDatabase))
				{
					yield return disconnectedMailbox;
				}
			}
			yield break;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00010E44 File Offset: 0x0000F044
		public DirectoryForest GetLocalForest()
		{
			return new DirectoryForest(this, DirectoryIdentity.CreateForestIdentity(this.localServer.Domain));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00010E5C File Offset: 0x0000F05C
		public DirectoryServer GetLocalServer()
		{
			return this.DirectoryServerFromADServer(this.localServer);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00011448 File Offset: 0x0000F648
		public IEnumerable<DirectoryMailbox> GetMailboxesForDatabase(DirectoryDatabase database)
		{
			this.logger.LogVerbose("Getting mailboxes for database '{0}'", new object[]
			{
				database.Name
			});
			DirectoryIdentity databaseIdentity = database.Identity;
			using (IPhysicalDatabase physicalDatabase = this.clientFactory.GetPhysicalDatabaseConnection(database))
			{
				physicalDatabase.LoadMailboxes();
				IOperationRetryManager retryManager = LoadBalanceOperationRetryManager.Create(this.logger);
				foreach (LoadBalancingMiniRecipient recipient in this.RecipientSession.FindAllUsersLinkedToDatabase(databaseIdentity.ADObjectId))
				{
					DirectoryMailbox mailbox = null;
					LoadBalancingMiniRecipient miniRecipient = recipient;
					retryManager.TryRun(delegate
					{
						mailbox = this.GetMailboxFromMiniRecipient(miniRecipient, this.RecipientSession.GetExternalDirectoryOrganizationId(miniRecipient), physicalDatabase);
						mailbox.Parent = database;
						this.NotifyObjectLoaded(mailbox);
					});
					if (mailbox != null)
					{
						yield return mailbox;
					}
				}
				foreach (IPhysicalMailbox consumerMailbox in physicalDatabase.GetConsumerMailboxes())
				{
					DirectoryIdentity consumerIdentity = DirectoryIdentity.CreateConsumerMailboxIdentity(consumerMailbox.Guid, physicalDatabase.DatabaseGuid, consumerMailbox.OrganizationId);
					yield return new DirectoryMailbox(this, consumerIdentity, new IPhysicalMailbox[]
					{
						consumerMailbox
					}, DirectoryMailboxType.Consumer);
				}
				foreach (NonConnectedMailbox disconnectedMailbox in this.GetDisconnectedMailboxesForDatabaseInternal(database, physicalDatabase))
				{
					yield return disconnectedMailbox;
				}
			}
			yield break;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001146C File Offset: 0x0000F66C
		public DirectoryServer GetServer(Guid serverGuid)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, serverGuid);
			Server[] array = this.ConfigurationSession.Find<Server>(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length == 0)
			{
				throw new ServerNotFoundException(serverGuid.ToString());
			}
			return this.DirectoryServerFromADServer(array[0]);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000114C0 File Offset: 0x0000F6C0
		public DirectoryServer GetServerByFqdn(Fqdn fqdn)
		{
			return this.DirectoryServerFromADServer(this.ConfigurationSession.FindServerByFqdn(fqdn));
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000114D4 File Offset: 0x0000F6D4
		public IEnumerable<DirectoryServer> GetServers(DirectoryIdentity dagIdentity)
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.DatabaseAvailabilityGroup, dagIdentity.ADObjectId);
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				DirectoryProvider.E15OrHigherMailboxServerFilter
			});
			IEnumerable<Server> source = this.ConfigurationSession.FindPaged<Server>(filter, null, true, null, 100);
			return source.Select(new Func<Server, DirectoryServer>(this.DirectoryServerFromADServer));
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00011534 File Offset: 0x0000F734
		public IEnumerable<DirectoryServer> GetServers()
		{
			IEnumerable<Server> source = this.ConfigurationSession.FindPaged<Server>(DirectoryProvider.E15OrHigherMailboxServerFilter, null, true, null, 100);
			return source.Select(new Func<Server, DirectoryServer>(this.DirectoryServerFromADServer));
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001156C File Offset: 0x0000F76C
		private void AddPhysicalMailboxToList(Guid mailboxGuid, Guid databaseGuid, IPhysicalDatabase physicalDatabaseConnection, List<IPhysicalMailbox> physicalMailboxes, bool isArchive)
		{
			IPhysicalMailbox physicalMailbox = null;
			if (physicalDatabaseConnection == null)
			{
				physicalMailbox = new VirtualPhysicalMailbox(this.clientFactory, this.GetDatabase(databaseGuid), mailboxGuid, this.logger, isArchive);
			}
			else if (physicalDatabaseConnection.DatabaseGuid == databaseGuid)
			{
				physicalMailbox = physicalDatabaseConnection.GetMailbox(mailboxGuid);
			}
			if (physicalMailbox != null)
			{
				physicalMailboxes.Add(physicalMailbox);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000115C0 File Offset: 0x0000F7C0
		private void ApplyLoadBalanceSettingValuesToDatabase(MailboxDatabase database, DirectoryDatabase directoryDatabase)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, database.MasterServerOrAvailabilityGroup);
			DatabaseAvailabilityGroup ownerDag = this.ConfigurationSession.Find<DatabaseAvailabilityGroup>(null, QueryScope.SubTree, filter, null, 1).FirstOrDefault<DatabaseAvailabilityGroup>();
			directoryDatabase.RelativeLoadCapacity = this.GetRelativeLoadCapacityForDatabase(database, ownerDag);
			directoryDatabase.MaximumSize = this.GetMaximumFileSizeForDatabase(database, ownerDag);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00011614 File Offset: 0x0000F814
		private DirectoryDatabase DirectoryDatabaseFromDatabase(MailboxDatabase database)
		{
			bool isExcludedFromProvisioning = (bool)database[MailboxDatabaseSchema.IsExcludedFromProvisioning];
			bool isExcludedFromInitialProvisioning = (bool)database[MailboxDatabaseSchema.IsExcludedFromInitialProvisioning];
			DirectoryIdentity identity = DirectoryIdentity.CreateFromADObjectId(database.Id, DirectoryObjectType.Database);
			DirectoryDatabase directoryDatabase = new DirectoryDatabase(this, identity, this.clientFactory, isExcludedFromProvisioning, isExcludedFromInitialProvisioning, database.MailboxProvisioningAttributes);
			this.ApplyLoadBalanceSettingValuesToDatabase(database, directoryDatabase);
			return directoryDatabase;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001166F File Offset: 0x0000F86F
		private DirectoryServer DirectoryServerFromADServer(Server server)
		{
			return new DirectoryServer(this, DirectoryIdentity.CreateFromADObjectId(server.Id, DirectoryObjectType.Server), server.Fqdn);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00011689 File Offset: 0x0000F889
		private DirectoryObject GetConstraintSetObject(DirectoryIdentity directoryIdentity)
		{
			return new DirectoryObject(this, directoryIdentity);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000119AC File Offset: 0x0000FBAC
		private IEnumerable<NonConnectedMailbox> GetDisconnectedMailboxesForDatabaseInternal(DirectoryDatabase database, IPhysicalDatabase physicalDatabase)
		{
			this.logger.LogVerbose("Getting soft deleted mailboxes for database '{0}'", new object[]
			{
				database.Name
			});
			IOperationRetryManager retryManager = LoadBalanceOperationRetryManager.Create(this.logger);
			foreach (IPhysicalMailbox disconnectedMailbox in physicalDatabase.GetNonConnectedMailboxes())
			{
				NonConnectedMailbox nonConnectedMailbox = null;
				IPhysicalMailbox mailbox = disconnectedMailbox;
				retryManager.TryRun(delegate
				{
					nonConnectedMailbox = new NonConnectedMailbox(this, DirectoryIdentity.CreateNonConnectedMailboxIdentity(mailbox.Guid, mailbox.OrganizationId), new IPhysicalMailbox[]
					{
						mailbox
					});
					nonConnectedMailbox.Parent = database;
					this.NotifyObjectLoaded(nonConnectedMailbox);
				});
				if (disconnectedMailbox != null)
				{
					yield return nonConnectedMailbox;
				}
			}
			yield break;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000119D8 File Offset: 0x0000FBD8
		private DirectoryMailbox GetMailboxFromMiniRecipient(LoadBalancingMiniRecipient recipient, Guid organizationId, IPhysicalDatabase physicalDatabaseConnection)
		{
			List<IPhysicalMailbox> physicalMailboxes = new List<IPhysicalMailbox>(2);
			Guid exchangeGuid = recipient.ExchangeGuid;
			if (recipient.Database != null && exchangeGuid != Guid.Empty)
			{
				Guid objectGuid = recipient.Database.ObjectGuid;
				this.AddPhysicalMailboxToList(exchangeGuid, objectGuid, physicalDatabaseConnection, physicalMailboxes, false);
			}
			if (recipient.ArchiveGuid != Guid.Empty && recipient.ArchiveDatabase != null)
			{
				this.AddPhysicalMailboxToList(recipient.ArchiveGuid, recipient.ArchiveDatabase.ObjectGuid, physicalDatabaseConnection, physicalMailboxes, true);
			}
			DirectoryMailbox directoryMailbox;
			if (recipient.RecipientType == RecipientType.MailUser)
			{
				directoryMailbox = new CloudArchive(this, DirectoryIdentity.CreateMailboxIdentity(recipient.Guid, organizationId, DirectoryObjectType.CloudArchive), physicalMailboxes);
			}
			else
			{
				directoryMailbox = new DirectoryMailbox(this, DirectoryIdentity.CreateMailboxIdentity(recipient.Guid, organizationId, DirectoryObjectType.Mailbox), physicalMailboxes, DirectoryMailboxType.Organization);
				if (recipient.propertyBag.Contains(MiniRecipientSchema.ConfigurationXML) && recipient.ConfigXML != null)
				{
					directoryMailbox.MailboxProvisioningConstraints = recipient.ConfigXML.MailboxProvisioningConstraints;
				}
			}
			if (recipient.MailboxMoveFlags.HasFlag(RequestFlags.IntraOrg) && (recipient.MailboxMoveStatus == RequestStatus.Queued || recipient.MailboxMoveStatus == RequestStatus.InProgress))
			{
				BatchName batchName = BatchName.FromString(recipient.MailboxMoveBatchName);
				if (batchName.IsLoadBalancingBatch)
				{
					directoryMailbox.IsBeingLoadBalanced = true;
				}
			}
			return directoryMailbox;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00011AFC File Offset: 0x0000FCFC
		private DirectoryMailbox GetMailboxObject(DirectoryIdentity identity)
		{
			LoadBalancingMiniRecipient recipient = this.RecipientSession.FindRecipient(identity);
			return this.GetMailboxFromMiniRecipient(recipient, identity.OrganizationId, null);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00011B24 File Offset: 0x0000FD24
		private ByteQuantifiedSize GetMaximumDatabaseFileSizeForDag(DatabaseAvailabilityGroup dag)
		{
			ByteQuantifiedSize result;
			if (dag != null && dag.MailboxLoadBalanceMaximumEdbFileSize != null)
			{
				result = dag.MailboxLoadBalanceMaximumEdbFileSize.Value;
			}
			else
			{
				result = ByteQuantifiedSize.FromGB((ulong)this.settings.DefaultDatabaseMaxSizeGb);
			}
			return result;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00011B68 File Offset: 0x0000FD68
		private ByteQuantifiedSize GetMaximumFileSizeForDatabase(MailboxDatabase database, DatabaseAvailabilityGroup ownerDag)
		{
			if (database.MailboxLoadBalanceMaximumEdbFileSize != null)
			{
				return database.MailboxLoadBalanceMaximumEdbFileSize.Value;
			}
			return this.GetMaximumDatabaseFileSizeForDag(ownerDag);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00011B9C File Offset: 0x0000FD9C
		private int GetRelativeLoadCapacityForDatabase(MailboxDatabase database, DatabaseAvailabilityGroup ownerDag)
		{
			if (database.MailboxLoadBalanceRelativeLoadCapacity != null)
			{
				return database.MailboxLoadBalanceRelativeLoadCapacity.Value;
			}
			if (ownerDag != null && ownerDag.MailboxLoadBalanceRelativeLoadCapacity != null)
			{
				return ownerDag.MailboxLoadBalanceRelativeLoadCapacity.Value;
			}
			return this.settings.DefaultDatabaseRelativeLoadCapacity;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00011BF8 File Offset: 0x0000FDF8
		private DirectoryServer GetServer(ADObjectId objectId)
		{
			MiniServer miniServer = this.ConfigurationSession.ReadMiniServer(objectId, new ADPropertyDefinition[]
			{
				ServerSchema.Fqdn,
				ADObjectSchema.Id,
				ADObjectSchema.Name
			});
			if (miniServer == null)
			{
				throw new ServerNotFoundException(objectId.ObjectGuid.ToString());
			}
			return new DirectoryServer(this, DirectoryIdentity.CreateFromADObjectId(objectId, DirectoryObjectType.Server), miniServer.Fqdn);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00011C64 File Offset: 0x0000FE64
		private void LogVerboseMessage(string message)
		{
			this.logger.LogVerbose("{0}", new object[]
			{
				message
			});
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00011C90 File Offset: 0x0000FE90
		private void NotifyObjectLoaded(DirectoryObject loadedObject)
		{
			foreach (IDirectoryListener directoryListener in this.listeners)
			{
				directoryListener.ObjectLoaded(loadedObject);
			}
		}

		// Token: 0x04000253 RID: 595
		private static readonly QueryFilter E15OrHigherMailboxServerFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.E15MinVersion),
			new BitMaskAndFilter(ServerSchema.CurrentServerRole, 2UL)
		});

		// Token: 0x04000254 RID: 596
		private readonly IClientFactory clientFactory;

		// Token: 0x04000255 RID: 597
		private readonly LoadBalanceAnchorContext context;

		// Token: 0x04000256 RID: 598
		private readonly IList<IDirectoryListener> listeners;

		// Token: 0x04000257 RID: 599
		private readonly Server localServer;

		// Token: 0x04000258 RID: 600
		private readonly ILogger logger;

		// Token: 0x04000259 RID: 601
		private readonly ILoadBalanceSettings settings;
	}
}
