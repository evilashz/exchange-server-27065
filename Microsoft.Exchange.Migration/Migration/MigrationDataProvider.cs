using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C4 RID: 196
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationDataProvider : DisposeTrackableBase, IMigrationDataProvider, IDisposable
	{
		// Token: 0x06000A47 RID: 2631 RVA: 0x0002A5AC File Offset: 0x000287AC
		private MigrationDataProvider(MigrationADProvider activeDirectoryProvider, MailboxSession mailboxSession, MigrationFolder folder, bool ownSession)
		{
			MigrationUtil.ThrowOnNullArgument(activeDirectoryProvider, "activeDirectoryProvider");
			MigrationUtil.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			MigrationUtil.ThrowOnNullArgument(folder, "folder");
			this.activeDirectoryProvider = activeDirectoryProvider;
			this.MailboxSession = mailboxSession;
			this.migrationFolder = folder;
			this.ownSession = ownSession;
			this.runspaceProxy = new Lazy<IMigrationRunspaceProxy>(() => MigrationServiceFactory.Instance.CreateRunspaceForDatacenterAdmin(this.OrganizationId));
			this.orgConfigContext = new OrganizationSettingsContext(this.OrganizationId, null).Activate();
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002A634 File Offset: 0x00028834
		public string TenantName
		{
			get
			{
				IExchangePrincipal mailboxOwner = this.MailboxSession.MailboxOwner;
				if (mailboxOwner == null || !(mailboxOwner.MailboxInfo.OrganizationId != null))
				{
					return null;
				}
				if (mailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit != null)
				{
					return mailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit.Name;
				}
				IOrganizationIdForEventLog organizationId = mailboxOwner.MailboxInfo.OrganizationId;
				return organizationId.IdForEventLog;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0002A69F File Offset: 0x0002889F
		public string MailboxName
		{
			get
			{
				return string.Format("{0} :: {1}", this.TenantName, this.MailboxSession.MailboxOwnerLegacyDN);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002A6BC File Offset: 0x000288BC
		public IMigrationADProvider ADProvider
		{
			get
			{
				return this.activeDirectoryProvider;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002A6C4 File Offset: 0x000288C4
		public Guid MdbGuid
		{
			get
			{
				return this.mailboxSession.MdbGuid;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002A6D1 File Offset: 0x000288D1
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0002A6D9 File Offset: 0x000288D9
		public MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
			private set
			{
				this.mailboxSession = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002A6E2 File Offset: 0x000288E2
		public IMigrationStoreObject Folder
		{
			get
			{
				return this.migrationFolder;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002A6EA File Offset: 0x000288EA
		public ADObjectId OwnerId
		{
			get
			{
				return this.MailboxSession.MailboxOwner.ObjectId;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002A6FC File Offset: 0x000288FC
		public OrganizationId OrganizationId
		{
			get
			{
				return this.MailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0002A713 File Offset: 0x00028913
		public IMigrationRunspaceProxy RunspaceProxy
		{
			get
			{
				return this.runspaceProxy.Value;
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002A720 File Offset: 0x00028920
		public static MigrationDataProvider CreateProviderForMigrationMailbox(OrganizationId orgId, string migrationMailboxLegDn)
		{
			return MigrationDataProvider.CreateProviderForMigrationMailbox(TenantPartitionHint.FromOrganizationId(orgId), migrationMailboxLegDn);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002A744 File Offset: 0x00028944
		public static MigrationDataProvider CreateProviderForMigrationMailbox(TenantPartitionHint tenantPartitionHint, string migrationMailboxLegacyDN)
		{
			MigrationUtil.ThrowOnNullArgument(tenantPartitionHint, "tenantPartitionHint");
			MigrationUtil.ThrowOnNullOrEmptyArgument(migrationMailboxLegacyDN, "migrationMailboxLegacyDN");
			return MigrationDataProvider.CreateProviderForMailboxSession(new MigrationADProvider(tenantPartitionHint), MigrationFolderName.SyncMigration, (MigrationADProvider provider) => MigrationDataProvider.OpenLocalMigrationMailboxSession(provider, migrationMailboxLegacyDN));
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002A7B0 File Offset: 0x000289B0
		public static MigrationDataProvider CreateProviderForMigrationMailbox(string action, IRecipientSession recipientSession, ADUser partitionMailbox)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(action, "action");
			MigrationUtil.ThrowOnNullArgument(recipientSession, "recipientSession");
			MigrationUtil.ThrowOnNullArgument(partitionMailbox, "partitionMailbox");
			return MigrationDataProvider.CreateProviderForMailboxSession(new MigrationADProvider(recipientSession), MigrationFolderName.SyncMigration, (MigrationADProvider provider) => MigrationDataProvider.OpenMigrationMailboxSession(provider, action, partitionMailbox));
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002A830 File Offset: 0x00028A30
		public static MigrationDataProvider CreateProviderForReportMailbox(string action, IRecipientSession recipientSession, ADUser partitionMailbox)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(action, "action");
			MigrationUtil.ThrowOnNullArgument(recipientSession, "recipientSession");
			MigrationUtil.ThrowOnNullArgument(partitionMailbox, "partitionMailbox");
			return MigrationDataProvider.CreateProviderForMailboxSession(new MigrationADProvider(recipientSession), MigrationFolderName.SyncMigrationReports, (MigrationADProvider provider) => MigrationDataProvider.OpenMigrationMailboxSession(provider, action, partitionMailbox));
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002A8AC File Offset: 0x00028AAC
		public static MigrationDataProvider CreateProviderForSystemMailbox(Guid mdbGuid)
		{
			MigrationUtil.ThrowOnGuidEmptyArgument(mdbGuid, "mdbGuid");
			return MigrationDataProvider.CreateProviderForMailboxSession(MigrationADProvider.GetRootOrgADProvider(), MigrationFolderName.SyncMigration, (MigrationADProvider provider) => MigrationDataProvider.OpenLocalSystemMailboxSession(provider, mdbGuid));
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002A8ED File Offset: 0x00028AED
		public IMigrationMessageItem CreateMessage()
		{
			return new MigrationMessageItem(MessageItem.CreateAssociated(this.MailboxSession, this.migrationFolder.Id));
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002A90C File Offset: 0x00028B0C
		public bool MoveMessageItems(StoreObjectId[] itemsToMove, MigrationFolderName folderName)
		{
			bool result;
			using (MigrationFolder folder = MigrationFolder.GetFolder(this.MailboxSession, folderName))
			{
				GroupOperationResult groupOperationResult = this.migrationFolder.Folder.MoveItems(folder.Folder.Id, itemsToMove);
				result = (groupOperationResult.OperationResult == OperationResult.Succeeded);
			}
			return result;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002A96C File Offset: 0x00028B6C
		public IMigrationEmailMessageItem CreateEmailMessage()
		{
			return new MigrationEmailMessageItem(this, MessageItem.Create(this.MailboxSession, this.MailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts)));
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002A98C File Offset: 0x00028B8C
		public void RemoveMessage(StoreObjectId messageId)
		{
			MigrationUtil.ThrowOnNullArgument(messageId, "messageId");
			try
			{
				this.MailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					messageId
				});
			}
			catch (ObjectNotFoundException exception)
			{
				MigrationLogger.Log(MigrationEventType.Error, exception, "Encountered an object not found  exception when deleting a migration job cache entry message", new object[0]);
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002AA48 File Offset: 0x00028C48
		public int CountMessages(QueryFilter filter, SortBy[] sortBy)
		{
			return MigrationUtil.RunTimedOperation<int>(delegate()
			{
				int estimatedRowCount;
				using (QueryResult queryResult = this.migrationFolder.Folder.ItemQuery(ItemQueryType.Associated, filter, sortBy, MigrationHelper.ItemIdProperties))
				{
					estimatedRowCount = queryResult.EstimatedRowCount;
				}
				return estimatedRowCount;
			}, filter);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002AA8C File Offset: 0x00028C8C
		public object[] QueryRow(QueryFilter filter, SortBy[] sortBy, PropertyDefinition[] propertyDefinitions)
		{
			using (IEnumerator<object[]> enumerator = this.QueryRows(filter, sortBy, propertyDefinitions, 1).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002AD58 File Offset: 0x00028F58
		public IEnumerable<object[]> QueryRows(QueryFilter filter, SortBy[] sortBy, PropertyDefinition[] propertyDefinitions, int pageSize)
		{
			if (pageSize == 0)
			{
				pageSize = 100;
			}
			QueryResult itemQueryResult = this.migrationFolder.Folder.ItemQuery(ItemQueryType.Associated, filter, sortBy, propertyDefinitions);
			for (;;)
			{
				object[][] rows = itemQueryResult.GetRows(pageSize);
				if (rows == null || rows.Length == 0)
				{
					break;
				}
				foreach (object[] row in rows)
				{
					yield return row;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002B07C File Offset: 0x0002927C
		public IEnumerable<StoreObjectId> FindMessageIds(QueryFilter filter, PropertyDefinition[] properties, SortBy[] sortBy, MigrationRowSelector rowSelectorPredicate, int? maxCount)
		{
			MigrationUtil.ThrowOnCollectionEmptyArgument(sortBy, "sortBy");
			if (maxCount == null || maxCount.Value > 0)
			{
				if (properties == null)
				{
					properties = new PropertyDefinition[0];
				}
				PropertyDefinition[] columns = new PropertyDefinition[1 + properties.Length];
				columns[0] = ItemSchema.Id;
				Array.Copy(properties, 0, columns, 1, properties.Length);
				using (QueryResult itemQueryResult = this.migrationFolder.Folder.ItemQuery(ItemQueryType.Associated, filter, sortBy, columns))
				{
					foreach (StoreObjectId id in MigrationDataProvider.ProcessQueryRows(columns, itemQueryResult, rowSelectorPredicate, 0, maxCount))
					{
						yield return id;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002B504 File Offset: 0x00029704
		public IEnumerable<StoreObjectId> FindMessageIds(MigrationEqualityFilter primaryFilter, PropertyDefinition[] filterColumns, SortBy[] additionalSorts, MigrationRowSelector rowSelectorPredicate, int? maxCount)
		{
			if (maxCount == null || maxCount.Value > 0)
			{
				MigrationUtil.ThrowOnNullArgument(primaryFilter, "primaryFilter");
				if (filterColumns == null)
				{
					filterColumns = new PropertyDefinition[0];
				}
				if (additionalSorts == null)
				{
					additionalSorts = new SortBy[0];
				}
				PropertyDefinition[] propertyDefinitions = new PropertyDefinition[2 + filterColumns.Length];
				propertyDefinitions[0] = primaryFilter.Property;
				propertyDefinitions[1] = ItemSchema.Id;
				Array.Copy(filterColumns, 0, propertyDefinitions, 2, filterColumns.Length);
				SortBy[] sortBy = new SortBy[1 + additionalSorts.Length];
				sortBy[0] = new SortBy(primaryFilter.Property, SortOrder.Ascending);
				Array.Copy(additionalSorts, 0, sortBy, 1, additionalSorts.Length);
				using (QueryResult itemQueryResult = this.migrationFolder.Folder.ItemQuery(ItemQueryType.Associated, null, sortBy, propertyDefinitions))
				{
					SeekReference reference = SeekReference.OriginBeginning;
					if (!MigrationUtil.RunTimedOperation<bool>(() => itemQueryResult.SeekToCondition(reference, primaryFilter.Filter, SeekToConditionFlags.AllowExtendedFilters), primaryFilter.Filter))
					{
						yield break;
					}
					foreach (StoreObjectId id in MigrationDataProvider.ProcessQueryRows(propertyDefinitions, itemQueryResult, rowSelectorPredicate, 1, maxCount))
					{
						yield return id;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002B546 File Offset: 0x00029746
		public IMigrationMessageItem FindMessage(StoreObjectId messageId, PropertyDefinition[] properties)
		{
			MigrationUtil.ThrowOnNullArgument(messageId, "messageId");
			MigrationUtil.ThrowOnNullArgument(properties, "properties");
			return new MigrationMessageItem(this, messageId, properties);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002B568 File Offset: 0x00029768
		public IMigrationStoreObject GetRootFolder(PropertyDefinition[] properties)
		{
			MigrationUtil.ThrowOnNullArgument(properties, "properties");
			MigrationUtil.AssertOrThrow(this.MailboxSession != null, "Should have a MailboxSession", new object[0]);
			IMigrationStoreObject result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IMigrationStoreObject folder = MigrationFolder.GetFolder(this.MailboxSession, MigrationFolderName.SyncMigration);
				disposeGuard.Add<IMigrationStoreObject>(folder);
				folder.Load(properties);
				disposeGuard.Success();
				result = folder;
			}
			return result;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002B5EC File Offset: 0x000297EC
		public IMigrationDataProvider GetProviderForFolder(MigrationFolderName folderName)
		{
			IMigrationDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MigrationFolder folder = MigrationFolder.GetFolder(this.MailboxSession, folderName);
				disposeGuard.Add<MigrationFolder>(folder);
				MigrationDataProvider migrationDataProvider = new MigrationDataProvider(this.activeDirectoryProvider, this.MailboxSession, folder, false);
				disposeGuard.Success();
				result = migrationDataProvider;
			}
			return result;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002B658 File Offset: 0x00029858
		public Uri GetEcpUrl()
		{
			return MigrationADProvider.GetEcpUrl(this.MailboxSession.MailboxOwner);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002B6A4 File Offset: 0x000298A4
		public void FlushReport(ReportData reportData)
		{
			MigrationUtil.AssertOrThrow(reportData != null, "Cannot flush a null report data.", new object[0]);
			if (!reportData.HasNewEntries)
			{
				return;
			}
			CommonUtils.CatchKnownExceptions(delegate
			{
				reportData.Flush(this.MailboxSession.Mailbox.MapiStore);
			}, delegate(Exception failure)
			{
				MigrationApplication.NotifyOfIgnoredException(failure, "Flushing report data: ");
			});
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002B71D File Offset: 0x0002991D
		public void LoadReport(ReportData reportData)
		{
			MigrationUtil.AssertOrThrow(reportData != null, "Cannot load into a null report data.", new object[0]);
			reportData.Load(this.MailboxSession.Mailbox.MapiStore);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002B784 File Offset: 0x00029984
		public void DeleteReport(ReportData reportData)
		{
			MigrationUtil.AssertOrThrow(reportData != null, "Cannot delete using a null report data.", new object[0]);
			CommonUtils.CatchKnownExceptions(delegate
			{
				reportData.Delete(this.MailboxSession.Mailbox.MapiStore);
			}, delegate(Exception failure)
			{
				MigrationApplication.NotifyOfIgnoredException(failure, "Deleting report: ");
			});
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002B7F0 File Offset: 0x000299F0
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.orgConfigContext != null)
				{
					this.orgConfigContext.Dispose();
					this.orgConfigContext = null;
				}
				if (this.migrationFolder != null)
				{
					this.migrationFolder.Dispose();
				}
				this.migrationFolder = null;
				if (this.ownSession && this.MailboxSession != null)
				{
					this.MailboxSession.Dispose();
				}
				this.MailboxSession = null;
				if (this.runspaceProxy != null && this.runspaceProxy.IsValueCreated && this.runspaceProxy.Value != null)
				{
					this.runspaceProxy.Value.Dispose();
				}
				this.runspaceProxy = null;
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002B892 File Offset: 0x00029A92
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationDataProvider>(this);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002B89C File Offset: 0x00029A9C
		private static MigrationDataProvider CreateProviderForMailboxSession(MigrationADProvider activeDirectoryProvider, MigrationFolderName folderName, Func<MigrationADProvider, MailboxSession> mailboxSessionCreator)
		{
			MigrationUtil.ThrowOnNullArgument(mailboxSessionCreator, "mailboxSessionCreator");
			MigrationDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MailboxSession disposable = mailboxSessionCreator(activeDirectoryProvider);
				disposeGuard.Add<MailboxSession>(disposable);
				MigrationFolder folder = MigrationFolder.GetFolder(disposable, folderName);
				disposeGuard.Add<MigrationFolder>(folder);
				MigrationDataProvider migrationDataProvider = new MigrationDataProvider(activeDirectoryProvider, disposable, folder, true);
				disposeGuard.Success();
				result = migrationDataProvider;
			}
			return result;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002B914 File Offset: 0x00029B14
		private static MailboxSession OpenLocalSystemMailboxSession(MigrationADProvider activeDirectoryProvider, Guid mdbGuid)
		{
			MigrationUtil.ThrowOnNullArgument(activeDirectoryProvider, "activeDirectoryProvider");
			MigrationUtil.ThrowOnGuidEmptyArgument(mdbGuid, "mdbGuid");
			ExchangePrincipal localSystemMailboxOwner = activeDirectoryProvider.GetLocalSystemMailboxOwner(mdbGuid);
			return MigrationDataProvider.OpenLocalMigrationMailboxSession(activeDirectoryProvider, localSystemMailboxOwner);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002B948 File Offset: 0x00029B48
		private static MailboxSession OpenLocalMigrationMailboxSession(MigrationADProvider activeDirectoryProvider, string migrationMailboxLegacyDN)
		{
			MigrationUtil.ThrowOnNullArgument(activeDirectoryProvider, "activeDirectoryProvider");
			MigrationUtil.ThrowOnNullOrEmptyArgument(migrationMailboxLegacyDN, "migrationMailboxLegacyDN");
			ExchangePrincipal localMigrationMailboxOwner = activeDirectoryProvider.GetLocalMigrationMailboxOwner(migrationMailboxLegacyDN);
			return MigrationDataProvider.OpenLocalMigrationMailboxSession(activeDirectoryProvider, localMigrationMailboxOwner);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002B97C File Offset: 0x00029B7C
		private static MailboxSession OpenMigrationMailboxSession(MigrationADProvider activeDirectoryProvider, string action, ADUser partitionMailbox)
		{
			MigrationUtil.ThrowOnNullArgument(activeDirectoryProvider, "activeDirectoryProvider");
			MigrationUtil.ThrowOnNullOrEmptyArgument(action, "action");
			MigrationUtil.ThrowOnNullArgument(partitionMailbox, "partitionMailbox");
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(activeDirectoryProvider.RecipientSession.SessionSettings, partitionMailbox, RemotingOptions.AllowCrossSite);
			string connectionDescription = string.Format("Client=Management;Privilege:OpenAsSystemService;Action={0}", action);
			return MigrationDataProvider.OpenMigrationMailboxSession(mailboxOwner, connectionDescription);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002B9D0 File Offset: 0x00029BD0
		private static MailboxSession OpenMigrationMailboxSession(ExchangePrincipal mailboxOwner, string connectionDescription)
		{
			MigrationUtil.ThrowOnNullArgument(mailboxOwner, "mailboxOwner");
			MigrationUtil.ThrowOnNullArgument(connectionDescription, "connectionDescription");
			MailboxSession result;
			try
			{
				result = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, connectionDescription);
			}
			catch (DatabaseNotFoundException innerException)
			{
				throw new MigrationTransientException(Strings.MigrationTempMissingDatabase, innerException);
			}
			catch (StoragePermanentException ex)
			{
				if (ex.InnerException is MapiExceptionDuplicateObject || ex.InnerException is MapiExceptionMailboxInTransit)
				{
					throw new MigrationTransientException(Strings.MigrationTempMissingMigrationMailbox, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002BA58 File Offset: 0x00029C58
		private static MailboxSession OpenLocalMigrationMailboxSession(MigrationADProvider activeDirectoryProvider, ExchangePrincipal mailboxOwner)
		{
			MigrationUtil.ThrowOnNullArgument(activeDirectoryProvider, "activeDirectoryProvider");
			MigrationUtil.ThrowOnNullArgument(mailboxOwner, "mailboxOwner");
			activeDirectoryProvider.EnsureLocalMailbox(mailboxOwner, false);
			MailboxSession result;
			try
			{
				result = MigrationDataProvider.OpenMigrationMailboxSession(mailboxOwner, "Client=MSExchangeSimpleMigration;Privilege:OpenAsSystemService");
			}
			catch (ConnectionFailedPermanentException ex)
			{
				if (ex is WrongServerException || ex is MailboxCrossSiteFailoverException)
				{
					throw new ConnectionFailedTransientException(ServerStrings.PrincipalFromDifferentSite, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002BD3C File Offset: 0x00029F3C
		private static IEnumerable<StoreObjectId> ProcessQueryRows(PropertyDefinition[] propertyDefinitions, QueryResult itemQueryResult, MigrationRowSelector rowSelectorPredicate, int idColumnIndex, int? maxCount)
		{
			Dictionary<PropertyDefinition, object> rowData = new Dictionary<PropertyDefinition, object>(propertyDefinitions.Length);
			int matchingRowCount = 0;
			bool searchFinished = false;
			while (!searchFinished)
			{
				object[][] rows = itemQueryResult.GetRows(100);
				if (rows.Length == 0)
				{
					searchFinished = true;
					break;
				}
				foreach (object[] row in rows)
				{
					rowData.Clear();
					for (int j = 0; j < propertyDefinitions.Length; j++)
					{
						rowData[propertyDefinitions[j]] = row[j];
					}
					switch (rowSelectorPredicate(rowData))
					{
					case MigrationRowSelectorResult.AcceptRow:
						matchingRowCount++;
						yield return ((VersionedId)row[idColumnIndex]).ObjectId;
						if (maxCount != null && matchingRowCount == maxCount)
						{
							searchFinished = true;
						}
						break;
					case MigrationRowSelectorResult.RejectRowStopProcessing:
						searchFinished = true;
						break;
					}
					if (searchFinished)
					{
						break;
					}
				}
			}
			yield break;
		}

		// Token: 0x04000407 RID: 1031
		public const string ServiceletConnectionString = "Client=MSExchangeSimpleMigration;Privilege:OpenAsSystemService";

		// Token: 0x04000408 RID: 1032
		private const int DefaultBatchSize = 100;

		// Token: 0x04000409 RID: 1033
		private const string ManagementConnectionString = "Client=Management;Privilege:OpenAsSystemService;Action={0}";

		// Token: 0x0400040A RID: 1034
		private MigrationADProvider activeDirectoryProvider;

		// Token: 0x0400040B RID: 1035
		private MailboxSession mailboxSession;

		// Token: 0x0400040C RID: 1036
		private MigrationFolder migrationFolder;

		// Token: 0x0400040D RID: 1037
		private bool ownSession;

		// Token: 0x0400040E RID: 1038
		private Lazy<IMigrationRunspaceProxy> runspaceProxy;

		// Token: 0x0400040F RID: 1039
		private IDisposable orgConfigContext;
	}
}
