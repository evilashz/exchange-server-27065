using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AnchorDataProvider : DisposeTrackableBase, IAnchorDataProvider, IDisposable
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00006D20 File Offset: 0x00004F20
		private AnchorDataProvider(AnchorContext anchorContext, AnchorADProvider activeDirectoryProvider, MailboxSession mailboxSession, AnchorFolder folder, bool ownSession)
		{
			AnchorUtil.ThrowOnNullArgument(anchorContext, "anchorContext");
			AnchorUtil.ThrowOnNullArgument(activeDirectoryProvider, "activeDirectoryProvider");
			AnchorUtil.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			AnchorUtil.ThrowOnNullArgument(folder, "folder");
			this.anchorContext = anchorContext;
			this.activeDirectoryProvider = activeDirectoryProvider;
			this.MailboxSession = mailboxSession;
			this.folder = folder;
			this.ownSession = ownSession;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006D85 File Offset: 0x00004F85
		public AnchorContext AnchorContext
		{
			get
			{
				return this.anchorContext;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006D90 File Offset: 0x00004F90
		public string TenantName
		{
			get
			{
				IExchangePrincipal mailboxOwner = this.MailboxSession.MailboxOwner;
				if (mailboxOwner == null || !(mailboxOwner.MailboxInfo.OrganizationId != null))
				{
					return null;
				}
				if (mailboxOwner.MailboxInfo.OrganizationId.ConfigurationUnit != null)
				{
					return mailboxOwner.MailboxInfo.OrganizationId.ConfigurationUnit.ToString();
				}
				IOrganizationIdForEventLog organizationId = mailboxOwner.MailboxInfo.OrganizationId;
				return organizationId.IdForEventLog;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006DFB File Offset: 0x00004FFB
		public string MailboxName
		{
			get
			{
				return string.Format("{0} :: {1}", this.TenantName, this.MailboxSession.MailboxOwner.MailboxInfo.MailboxGuid);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006E27 File Offset: 0x00005027
		public IAnchorADProvider ADProvider
		{
			get
			{
				return this.activeDirectoryProvider;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00006E2F File Offset: 0x0000502F
		public Guid MdbGuid
		{
			get
			{
				return this.mailboxSession.MailboxOwner.MailboxInfo.GetDatabaseGuid();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006E46 File Offset: 0x00005046
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00006E4E File Offset: 0x0000504E
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006E57 File Offset: 0x00005057
		public IAnchorStoreObject Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006E5F File Offset: 0x0000505F
		public ADObjectId OwnerId
		{
			get
			{
				return this.MailboxSession.MailboxOwner.ObjectId;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006E71 File Offset: 0x00005071
		public OrganizationId OrganizationId
		{
			get
			{
				return this.MailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006E88 File Offset: 0x00005088
		public static AnchorDataProvider CreateProviderForMigrationMailboxFolder(AnchorContext context, AnchorADProvider activeDirectoryProvider, string folderName)
		{
			AnchorUtil.ThrowOnNullArgument(context, "context");
			AnchorUtil.ThrowOnNullArgument(activeDirectoryProvider, "activeDirectoryProvider");
			AnchorUtil.ThrowOnNullArgument(folderName, "folderName");
			return AnchorDataProvider.CreateProviderForMailboxSession(context, activeDirectoryProvider, folderName, new Func<ExchangePrincipal, MailboxSession>(AnchorDataProvider.OpenMailboxSession));
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006EBF File Offset: 0x000050BF
		public IAnchorMessageItem CreateMessage()
		{
			return new AnchorMessageItem(this.anchorContext, MessageItem.CreateAssociated(this.MailboxSession, this.folder.Id));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006EE4 File Offset: 0x000050E4
		public bool MoveMessageItems(StoreObjectId[] itemsToMove, string folderName)
		{
			bool result;
			using (AnchorFolder anchorFolder = AnchorFolder.GetFolder(this.anchorContext, this.MailboxSession, folderName))
			{
				GroupOperationResult groupOperationResult = this.folder.Folder.MoveItems(anchorFolder.Folder.Id, itemsToMove);
				result = (groupOperationResult.OperationResult == OperationResult.Succeeded);
			}
			return result;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006F48 File Offset: 0x00005148
		public IAnchorEmailMessageItem CreateEmailMessage()
		{
			return new AnchorEmailMessageItem(this.anchorContext, this.ADProvider, MessageItem.Create(this.MailboxSession, this.MailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts)));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006F74 File Offset: 0x00005174
		public void RemoveMessage(StoreObjectId messageId)
		{
			AnchorUtil.ThrowOnNullArgument(messageId, "messageId");
			try
			{
				this.MailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					messageId
				});
			}
			catch (ObjectNotFoundException exception)
			{
				this.anchorContext.Logger.Log(MigrationEventType.Error, exception, "Encountered an object not found  exception when deleting a migration job cache entry message", new object[0]);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000072C0 File Offset: 0x000054C0
		public IEnumerable<StoreObjectId> FindMessageIds(QueryFilter queryFilter, PropertyDefinition[] properties, SortBy[] sortBy, AnchorRowSelector rowSelector, int? maxCount)
		{
			AnchorUtil.ThrowOnCollectionEmptyArgument(sortBy, "sortBy");
			if (maxCount == null || maxCount.Value > 0)
			{
				if (properties == null)
				{
					properties = new PropertyDefinition[0];
				}
				PropertyDefinition[] columns = new PropertyDefinition[1 + properties.Length];
				columns[0] = ItemSchema.Id;
				Array.Copy(properties, 0, columns, 1, properties.Length);
				using (IQueryResult itemQueryResult = this.folder.Folder.IItemQuery(ItemQueryType.Associated, queryFilter, sortBy, columns))
				{
					foreach (StoreObjectId id in AnchorDataProvider.ProcessQueryRows(columns, itemQueryResult, rowSelector, 0, maxCount))
					{
						yield return id;
					}
				}
			}
			yield break;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007302 File Offset: 0x00005502
		public IAnchorMessageItem FindMessage(StoreObjectId messageId, PropertyDefinition[] properties)
		{
			AnchorUtil.ThrowOnNullArgument(messageId, "messageId");
			AnchorUtil.ThrowOnNullArgument(properties, "properties");
			return new AnchorMessageItem(this.mailboxSession, messageId, properties);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007328 File Offset: 0x00005528
		public IAnchorStoreObject GetFolderByName(string folderName, PropertyDefinition[] properties)
		{
			AnchorUtil.ThrowOnNullArgument(properties, "properties");
			AnchorUtil.AssertOrThrow(this.MailboxSession != null, "Should have a MailboxSession", new object[0]);
			IAnchorStoreObject result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IAnchorStoreObject anchorStoreObject = AnchorFolder.GetFolder(this.anchorContext, this.MailboxSession, folderName);
				disposeGuard.Add<IAnchorStoreObject>(anchorStoreObject);
				anchorStoreObject.Load(properties);
				disposeGuard.Success();
				result = anchorStoreObject;
			}
			return result;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000073B4 File Offset: 0x000055B4
		public IAnchorDataProvider GetProviderForFolder(AnchorContext context, string folderName)
		{
			IAnchorDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				AnchorFolder disposable = AnchorFolder.GetFolder(this.anchorContext, this.MailboxSession, folderName);
				disposeGuard.Add<AnchorFolder>(disposable);
				AnchorDataProvider anchorDataProvider = new AnchorDataProvider(context, this.activeDirectoryProvider, this.MailboxSession, disposable, false);
				disposeGuard.Success();
				result = anchorDataProvider;
			}
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007424 File Offset: 0x00005624
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.folder != null)
				{
					this.folder.Dispose();
				}
				this.folder = null;
				if (this.ownSession && this.MailboxSession != null)
				{
					this.MailboxSession.Dispose();
				}
				this.MailboxSession = null;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007470 File Offset: 0x00005670
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AnchorDataProvider>(this);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007478 File Offset: 0x00005678
		private static AnchorDataProvider CreateProviderForMailboxSession(AnchorContext context, AnchorADProvider activeDirectoryProvider, string folderName, Func<ExchangePrincipal, MailboxSession> mailboxSessionCreator)
		{
			AnchorUtil.ThrowOnNullArgument(mailboxSessionCreator, "mailboxSessionCreator");
			AnchorDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				ExchangePrincipal mailboxOwner = activeDirectoryProvider.GetMailboxOwner(AnchorDataProvider.GetMailboxFilter(context.AnchorCapability));
				MailboxSession disposable = mailboxSessionCreator(mailboxOwner);
				disposeGuard.Add<MailboxSession>(disposable);
				AnchorFolder disposable2 = AnchorFolder.GetFolder(context, disposable, folderName);
				disposeGuard.Add<AnchorFolder>(disposable2);
				AnchorDataProvider anchorDataProvider = new AnchorDataProvider(context, activeDirectoryProvider, disposable, disposable2, true);
				disposeGuard.Success();
				result = anchorDataProvider;
			}
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007508 File Offset: 0x00005708
		private static QueryFilter GetMailboxFilter(OrganizationCapability anchorCapability)
		{
			return QueryFilter.AndTogether(new QueryFilter[]
			{
				OrganizationMailbox.OrganizationMailboxFilterBase,
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RawCapabilities, anchorCapability)
			});
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000753E File Offset: 0x0000573E
		private static MailboxSession OpenMailboxSession(ExchangePrincipal mailboxOwner)
		{
			return MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=AnchorService;Privilege:OpenAsSystemService");
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007844 File Offset: 0x00005A44
		private static IEnumerable<StoreObjectId> ProcessQueryRows(PropertyDefinition[] propertyDefinitions, IQueryResult itemQueryResult, AnchorRowSelector rowSelectorPredicate, int idColumnIndex, int? maxCount)
		{
			Dictionary<PropertyDefinition, object> rowData = new Dictionary<PropertyDefinition, object>(propertyDefinitions.Length);
			int matchingRowCount = 0;
			bool mightBeMoreRows = true;
			while (mightBeMoreRows)
			{
				object[][] rows = itemQueryResult.GetRows(100, out mightBeMoreRows);
				if (!mightBeMoreRows)
				{
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
					case AnchorRowSelectorResult.AcceptRow:
						matchingRowCount++;
						yield return ((VersionedId)row[idColumnIndex]).ObjectId;
						if (maxCount != null && matchingRowCount == maxCount)
						{
							mightBeMoreRows = false;
						}
						break;
					case AnchorRowSelectorResult.RejectRowContinueProcessing:
						break;
					case AnchorRowSelectorResult.RejectRowStopProcessing:
						mightBeMoreRows = false;
						break;
					default:
						mightBeMoreRows = false;
						break;
					}
					if (!mightBeMoreRows)
					{
						break;
					}
				}
			}
			yield break;
		}

		// Token: 0x04000088 RID: 136
		public const string ServiceletConnectionString = "Client=AnchorService;Privilege:OpenAsSystemService";

		// Token: 0x04000089 RID: 137
		private const int DefaultBatchSize = 100;

		// Token: 0x0400008A RID: 138
		private readonly bool ownSession;

		// Token: 0x0400008B RID: 139
		private readonly AnchorADProvider activeDirectoryProvider;

		// Token: 0x0400008C RID: 140
		private readonly AnchorContext anchorContext;

		// Token: 0x0400008D RID: 141
		private MailboxSession mailboxSession;

		// Token: 0x0400008E RID: 142
		private AnchorFolder folder;
	}
}
