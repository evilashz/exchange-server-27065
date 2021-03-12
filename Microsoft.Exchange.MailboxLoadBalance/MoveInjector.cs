using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Constraints;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.Provisioning;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MoveInjector
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000310C File Offset: 0x0000130C
		public MoveInjector(LoadBalanceAnchorContext serviceContext)
		{
			this.serviceContext = serviceContext;
			this.queueManager = serviceContext.QueueManager;
			this.settings = serviceContext.Settings;
			this.directoryProvider = serviceContext.Directory;
			this.logger = serviceContext.Logger;
			this.clientFactory = serviceContext.ClientFactory;
			this.databaseSelector = serviceContext.DatabaseSelector;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003170 File Offset: 0x00001370
		public virtual void InjectMoveForMailbox(DirectoryMailbox mailbox, BatchName batchName)
		{
			if (LoadBalanceADSettings.Instance.Value.UseDatabaseSelectorForMoveInjection)
			{
				LoadEntity loadEntity = this.serviceContext.GetTopologyExtractorFactoryContext().GetEntitySelectorFactory().GetExtractor(mailbox).ExtractEntity();
				MailboxProvisioningResult database = this.databaseSelector.GetDatabase(new MailboxProvisioningData(mailbox.PhysicalSize, mailbox.MailboxProvisioningConstraints, loadEntity.ConsumedLoad));
				database.ValidateSelection();
				DirectoryIdentity database2 = database.Database;
				DirectoryDatabase database3 = (DirectoryDatabase)this.directoryProvider.GetDirectoryObject(database2);
				using (IInjectorService injectorClientForDatabase = this.clientFactory.GetInjectorClientForDatabase(database3))
				{
					injectorClientForDatabase.InjectSingleMove(database2.Guid, batchName.ToString(), new LoadEntity(mailbox));
					return;
				}
			}
			IRequest request = mailbox.CreateRequestToMove(null, batchName.ToString(), this.logger);
			this.queueManager.MainProcessingQueue.EnqueueRequest(request);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000325C File Offset: 0x0000145C
		public void InjectMoves(Guid targetDatabase, BatchName batchName, IList<LoadEntity> loadEntityList, bool throwIfNotValid = false)
		{
			this.logger.Log(MigrationEventType.Information, "Injecting {0} moves into database '{1}' with batch name '{2}'.", new object[]
			{
				loadEntityList.Count,
				targetDatabase,
				batchName
			});
			TopologyExtractorFactoryContextPool topologyExtractorFactoryContextPool = this.serviceContext.TopologyExtractorFactoryContextPool;
			IList<Guid> nonMovableOrgsList = LoadBalanceUtils.GetNonMovableOrgsList(this.settings);
			TopologyExtractorFactoryContext context = topologyExtractorFactoryContextPool.GetContext(this.clientFactory, null, nonMovableOrgsList, this.logger);
			TopologyExtractorFactory entitySelectorFactory = context.GetEntitySelectorFactory();
			LoadContainer database = entitySelectorFactory.GetExtractor(this.directoryProvider.GetDatabase(targetDatabase)).ExtractTopology();
			this.InjectMoves(database, batchName, loadEntityList, throwIfNotValid);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000032FC File Offset: 0x000014FC
		public void InjectMovesOnCompatibilityMode(LoadContainer targetDatabase, BatchName batchName, IEnumerable<LoadEntity> mailboxes, bool throwIfNotValid)
		{
			IList<LoadEntity> list = (mailboxes as IList<LoadEntity>) ?? mailboxes.ToList<LoadEntity>();
			this.logger.Log(MigrationEventType.Information, "Injecting {0} moves into database '{1}' with batch name '{2}' in backwards compatibility mode.", new object[]
			{
				list.Count,
				targetDatabase.Guid,
				batchName
			});
			this.InjectMoves(targetDatabase, batchName, list, throwIfNotValid);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000034C0 File Offset: 0x000016C0
		protected virtual void InjectMoves(LoadContainer database, BatchName batchName, IEnumerable<LoadEntity> mailboxes, bool throwIfNotValid)
		{
			IRequestQueue injectorQueue = this.queueManager.GetInjectionQueue((DirectoryDatabase)database.DirectoryObject);
			IOperationRetryManager operationRetryManager = LoadBalanceOperationRetryManager.Create(this.logger);
			using (IEnumerator<LoadEntity> enumerator = mailboxes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LoadEntity mailbox = enumerator.Current;
					if (mailbox.DirectoryObject == null)
					{
						this.logger.Log(MigrationEventType.Warning, "Not injecting move for {0} because its DirectoryObject is null", new object[]
						{
							mailbox
						});
					}
					else
					{
						OperationRetryManagerResult operationRetryManagerResult = operationRetryManager.TryRun(delegate
						{
							DirectoryObject directoryObject = mailbox.DirectoryObject;
							ConstraintValidationResult constraintValidationResult = database.Constraint.Accept(mailbox);
							if (constraintValidationResult.Accepted)
							{
								database.CommittedLoad += mailbox.ConsumedLoad;
								if (directoryObject.SupportsMoving)
								{
									DirectoryObject directoryObject2 = database.DirectoryObject;
									IRequest request = directoryObject.CreateRequestToMove(directoryObject2.Identity, batchName.ToString(), this.logger);
									injectorQueue.EnqueueRequest(request);
									return;
								}
								if (throwIfNotValid)
								{
									throw new ObjectCannotBeMovedException(mailbox.DirectoryObjectIdentity.ObjectType.ToString(), mailbox.DirectoryObjectIdentity.ToString());
								}
							}
							else
							{
								this.logger.Log(MigrationEventType.Warning, "Not injecting move for {0} because it violates the target database constraints: {1}", new object[]
								{
									mailbox,
									constraintValidationResult
								});
								if (throwIfNotValid)
								{
									constraintValidationResult.Constraint.ValidateAccepted(mailbox);
								}
							}
						});
						if (!operationRetryManagerResult.Succeeded && throwIfNotValid)
						{
							throw operationRetryManagerResult.Exception;
						}
					}
				}
			}
		}

		// Token: 0x04000018 RID: 24
		private readonly IClientFactory clientFactory;

		// Token: 0x04000019 RID: 25
		private readonly DatabaseSelector databaseSelector;

		// Token: 0x0400001A RID: 26
		private readonly IDirectoryProvider directoryProvider;

		// Token: 0x0400001B RID: 27
		private readonly ILogger logger;

		// Token: 0x0400001C RID: 28
		private readonly IRequestQueueManager queueManager;

		// Token: 0x0400001D RID: 29
		private readonly LoadBalanceAnchorContext serviceContext;

		// Token: 0x0400001E RID: 30
		private readonly ILoadBalanceSettings settings;
	}
}
