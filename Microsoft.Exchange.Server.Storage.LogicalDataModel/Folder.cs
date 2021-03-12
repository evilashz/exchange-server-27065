using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000046 RID: 70
	public class Folder : SharedObjectPropertyBag, IStateObject
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0003C9E4 File Offset: 0x0003ABE4
		protected Folder(Context context, Mailbox mailbox, Folder parentFolder, ExchangeId id, bool newFolder, bool inheritBacklinks, ExchangeId originalParentFolderId, ExchangeId originalFolderId) : base(context, Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(mailbox.Database).Table, mailbox, newFolder, true, SharedObjectPropertyBagDataCache.GetCacheForMailbox(mailbox.SharedState), id, new ColumnValue[]
		{
			new ColumnValue(Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(mailbox.Database).MailboxPartitionNumber, mailbox.MailboxPartitionNumber),
			new ColumnValue(Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(mailbox.Database).FolderId, id.To26ByteArray())
		})
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(mailbox.Database);
				this.aggregates = new Folder.AggregateColumns[]
				{
					new Folder.AggregateColumns(this.folderTable.UnreadMessageCount, this.folderTable.MessageCount, this.folderTable.MessageSize, this.folderTable.MessageHasAttachCount, this.folderTable.MessageAttachCount),
					new Folder.AggregateColumns(this.folderTable.UnreadHiddenItemCount, this.folderTable.HiddenItemCount, this.folderTable.HiddenItemSize, this.folderTable.HiddenItemHasAttachCount, this.folderTable.HiddenItemAttachCount)
				};
				if (newFolder)
				{
					if (context.PerfInstance != null)
					{
						context.PerfInstance.FoldersCreatedRate.Increment();
					}
					if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId, string>(0L, "Creating new folder {0} under {1}", id, (parentFolder == null) ? "<null>" : parentFolder.GetId(context).ToString());
					}
					ExchangeId nextChangeNumber = mailbox.GetNextChangeNumber(context);
					base.SetColumn(context, this.folderTable.LcnCurrent, nextChangeNumber.To26ByteArray());
					if (parentFolder != null)
					{
						base.SetColumn(context, this.folderTable.ParentFolderId, parentFolder.GetId(context).To26ByteArray());
						this.SetProperty(context, PropTag.Folder.IPMFolder, parentFolder.IsIpmFolder(context));
						if (inheritBacklinks)
						{
							IList<ExchangeId> searchBacklinks = parentFolder.GetSearchBacklinks(context, true);
							if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.FolderTracer.TraceDebug<string>(0L, "Inheriting recursive backlinks from parent: {0}", searchBacklinks.GetAsString<IList<ExchangeId>>());
							}
							if (searchBacklinks.Count > 0)
							{
								this.SetSearchBacklinks(context, searchBacklinks, true);
							}
						}
					}
					else
					{
						base.SetColumn(context, this.folderTable.ParentFolderId, ExchangeId.Zero.To26ByteArray());
						this.SetProperty(context, PropTag.Folder.IPMFolder, false);
					}
					if (mailbox.SharedState.UnifiedState != null)
					{
						int num = (parentFolder != null) ? ((int)parentFolder.GetPropertyValue(context, PropTag.Folder.MailboxNum)) : mailbox.MailboxNumber;
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num == mailbox.MailboxNumber, "Mailbox object used to create folder is wrong");
						if (context.PrimaryMailboxContext != null && context.PrimaryMailboxContext.MailboxNumber != mailbox.MailboxNumber)
						{
							throw new StoreException((LID)50908U, ErrorCodeValue.NotSupported);
						}
						if (mailbox.MailboxNumber != mailbox.MailboxPartitionNumber)
						{
							base.SetColumn(context, this.folderTable.MailboxNumber, mailbox.MailboxNumber);
						}
					}
					PCL pcl = default(PCL);
					pcl.Add(nextChangeNumber);
					base.SetColumn(context, this.folderTable.VersionHistory, pcl.DumpBinaryLXCN());
					base.SetColumn(context, this.folderTable.CreatorSid, new byte[0]);
					base.SetColumn(context, this.folderTable.LastModifierSid, new byte[0]);
					base.SetColumn(context, this.folderTable.ConversationCount, 0L);
					base.SetColumn(context, this.folderTable.UnreadHiddenItemCount, 0L);
					PropertySchemaPopulation.InitializeFolder(context, this);
					base.SetColumn(context, this.folderTable.SpecialFolderNumber, 0);
					base.SetColumn(context, this.folderTable.ReservedMessageIdGlobCntCurrent, 0L);
					base.SetColumn(context, this.folderTable.ReservedMessageIdGlobCntMax, 0L);
					base.SetColumn(context, this.folderTable.ReservedMessageCnGlobCntCurrent, 0L);
					base.SetColumn(context, this.folderTable.ReservedMessageCnGlobCntMax, 0L);
					base.SetColumn(context, this.folderTable.NextArticleNumber, 1);
					base.SetColumn(context, this.folderTable.AclTableAndSecurityDescriptor, FolderSecurity.AclTableAndSecurityDescriptorProperty.GetEmpty());
					this.SetProperty(context, PropTag.Folder.HierarchyChangeNumber, 0);
					this.SetProperty(context, PropTag.Folder.FolderInternetId, mailbox.GetNextFolderInternetId(context));
					this.originalParentFolderId = originalParentFolderId;
					this.originalFolderId = originalFolderId;
					if (mailbox.IsPublicFolderMailbox && parentFolder != null)
					{
						object propertyValue = parentFolder.GetPropertyValue(context, PropTag.Folder.StorageQuota);
						this.SetProperty(context, PropTag.Folder.StorageQuota, propertyValue);
						propertyValue = parentFolder.GetPropertyValue(context, PropTag.Folder.PFOverHardQuotaLimit);
						this.SetProperty(context, PropTag.Folder.PFOverHardQuotaLimit, propertyValue);
						propertyValue = parentFolder.GetPropertyValue(context, PropTag.Folder.PFMsgSizeLimit);
						this.SetProperty(context, PropTag.Folder.PFMsgSizeLimit, propertyValue);
					}
				}
				else
				{
					if (mailbox.SharedState.UnifiedState != null)
					{
						int num2 = (int)this.GetPropertyValue(context, PropTag.Folder.MailboxNum);
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num2 == -1 || num2 == mailbox.MailboxNumber, "Wrong mailbox object is used to open folder");
					}
					if (context.PerfInstance != null)
					{
						context.PerfInstance.FoldersOpenedRate.Increment();
					}
					if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Opening folder {0}", id);
					}
				}
				this.UpdatePerUserReadUnreadTrackingEnabled(context);
				disposeGuard.Success();
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0003CFBC File Offset: 0x0003B1BC
		public bool HasDoNotDeleteReferences
		{
			get
			{
				if (base.IsDead)
				{
					return false;
				}
				HashSet<object> hashSet = (HashSet<object>)base.GetComponentData(Folder.doNotDeleteReferencesDataSlot);
				if (hashSet != null)
				{
					using (LockManager.Lock(hashSet, LockManager.LockType.LeafMonitorLock))
					{
						return hashSet.Count > 0;
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0003D020 File Offset: 0x0003B220
		public bool IsPerUserReadUnreadTrackingEnabled
		{
			get
			{
				return !base.IsDead && base.GetComponentData(Folder.isPerUserReadUnreadTrackingEnabledSlot) != null;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0003D049 File Offset: 0x0003B249
		public override bool NeedsToPublishNotification
		{
			get
			{
				return !base.IsDead && (base.NeedsToPublishNotification || this.unreadNormalMessageCountForEvent != -1 || this.totalNormalMessageCountForEvent != -1);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0003D074 File Offset: 0x0003B274
		public FolderTable FolderTable
		{
			get
			{
				return this.folderTable;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0003D07C File Offset: 0x0003B27C
		public override ObjectPropertySchema Schema
		{
			get
			{
				if (this.propertySchema == null)
				{
					this.propertySchema = PropertySchema.GetObjectSchema(base.Mailbox.Database, ObjectType.Folder);
				}
				return this.propertySchema;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0003D0A3 File Offset: 0x0003B2A3
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0003D0AB File Offset: 0x0003B2AB
		public bool TrackFolderChange
		{
			get
			{
				return this.trackFolderChange;
			}
			set
			{
				this.trackFolderChange = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0003D0B4 File Offset: 0x0003B2B4
		public object AclTableVersionCookie
		{
			get
			{
				return base.GetComponentData(Folder.aclTableVersionCookieDataSlot);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0003D0C1 File Offset: 0x0003B2C1
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0003D0D3 File Offset: 0x0003B2D3
		private HashSet<StorePropTag> NormalItemPromotedProperties
		{
			get
			{
				return (HashSet<StorePropTag>)base.GetComponentData(Folder.normalItemPromotedPropertiesDataSlot);
			}
			set
			{
				base.SetComponentData(Folder.normalItemPromotedPropertiesDataSlot, value);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0003D0E1 File Offset: 0x0003B2E1
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0003D0F3 File Offset: 0x0003B2F3
		private HashSet<StorePropTag> HiddenItemPromotedProperties
		{
			get
			{
				return (HashSet<StorePropTag>)base.GetComponentData(Folder.hiddenItemPromotedPropertiesDataSlot);
			}
			set
			{
				base.SetComponentData(Folder.hiddenItemPromotedPropertiesDataSlot, value);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0003D101 File Offset: 0x0003B301
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x0003D113 File Offset: 0x0003B313
		private HashSet<ushort> NormalItemPromotedPropertyIdsCache
		{
			get
			{
				return (HashSet<ushort>)base.GetComponentData(Folder.normalItemPromotedPropertyIdsCacheDataSlot);
			}
			set
			{
				base.SetComponentData(Folder.normalItemPromotedPropertyIdsCacheDataSlot, value);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0003D121 File Offset: 0x0003B321
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0003D133 File Offset: 0x0003B333
		private HashSet<ushort> HiddenItemPromotedPropertyIdsCache
		{
			get
			{
				return (HashSet<ushort>)base.GetComponentData(Folder.hiddenItemPromotedPropertyIdsCacheDataSlot);
			}
			set
			{
				base.SetComponentData(Folder.hiddenItemPromotedPropertyIdsCacheDataSlot, value);
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0003D1A4 File Offset: 0x0003B3A4
		internal static void Initialize()
		{
			if (Folder.normalItemPromotedPropertiesDataSlot == -1)
			{
				Folder.normalItemPromotedPropertiesDataSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				Folder.hiddenItemPromotedPropertiesDataSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				Folder.normalItemPromotedPropertyIdsCacheDataSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				Folder.hiddenItemPromotedPropertyIdsCacheDataSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				Folder.aclTableVersionCookieDataSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				Folder.propertyPromotionStateSlot = MailboxState.AllocateComponentDataSlot(false);
				Folder.isPerUserReadUnreadTrackingEnabledSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				Folder.doNotDeleteReferencesDataSlot = SharedObjectPropertyBag.AllocateComponentDataSlot();
				AddMidsetDeletedDelta.InitializeUpgraderAction(delegate(Context context)
				{
					FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(context.Database);
					folderTable.Table.AddColumn(context, folderTable.MidsetDeletedDelta);
				}, delegate(StoreDatabase database)
				{
					FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(database);
					folderTable.MidsetDeletedDelta.MinVersion = AddMidsetDeletedDelta.Instance.To.Value;
				});
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0003D24C File Offset: 0x0003B44C
		public static Folder OpenFolder(Context context, Mailbox mailbox, ExchangeId id)
		{
			Mailbox mailbox2 = mailbox;
			if (mailbox.SharedState.UnifiedState != null)
			{
				FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, mailbox, ExchangeShortId.Zero, FolderInformationType.Basic);
				IFolderInformation folderInformation = folderHierarchy.Find(context, id.ToExchangeShortId());
				if (folderInformation == null)
				{
					return null;
				}
				if (folderInformation.MailboxNumber != -1 && mailbox.MailboxNumber != folderInformation.MailboxNumber)
				{
					mailbox2 = (Mailbox)context.GetMailboxContext(folderInformation.MailboxNumber);
				}
			}
			Folder folder = (Folder)mailbox2.GetOpenedPropertyBag(context, id);
			if (folder == null)
			{
				folder = new Folder(context, mailbox2, null, id, false, false, ExchangeId.Null, ExchangeId.Null);
				if (folder.IsDead)
				{
					folder.Dispose();
					return null;
				}
				if (folder.IsSearchFolder(context))
				{
					folder.Dispose();
					folder = new SearchFolder(context, mailbox2, id);
				}
			}
			return folder;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0003D303 File Offset: 0x0003B503
		public static Folder CreateFolder(Context context, Folder parentFolder)
		{
			return Folder.CreateFolder(context, parentFolder, ExchangeId.Zero, null);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0003D312 File Offset: 0x0003B512
		public static Folder CreateFolder(Context context, Folder parentFolder, ExchangeId folderId, Folder sourceFolder)
		{
			return Folder.CreateFolder(context, parentFolder, folderId, sourceFolder, false);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0003D320 File Offset: 0x0003B520
		public static Folder CreateFolder(Context context, Folder parentFolder, ExchangeId folderId, Folder sourceFolder, bool internalAccess)
		{
			if (!parentFolder.CheckAlive(context))
			{
				throw new StoreException((LID)49656U, ErrorCodeValue.ObjectDeleted, "parentFolder is dead");
			}
			ExchangeId exchangeId = ExchangeId.Null;
			ExchangeId exchangeId2 = ExchangeId.Null;
			if (sourceFolder != null)
			{
				if (!sourceFolder.CheckAlive(context))
				{
					throw new StoreException((LID)49144U, ErrorCodeValue.ObjectDeleted, "sourceFolder is dead");
				}
				exchangeId2 = sourceFolder.GetId(context);
				exchangeId = ExchangeId.Zero;
				if (sourceFolder.GetParentFolder(context) != null)
				{
					exchangeId = sourceFolder.GetParentFolder(context).GetId(context);
				}
				Folder.CheckForFolderCycle(context, sourceFolder, parentFolder);
			}
			if (folderId.IsNullOrZero)
			{
				folderId = parentFolder.Mailbox.GetNextFolderId(context);
			}
			else
			{
				parentFolder.Mailbox.GetOpenedPropertyBag(context, folderId);
			}
			bool flag = internalAccess || parentFolder.IsInternalAccess(context);
			Folder folder = new Folder(context, parentFolder.Mailbox, parentFolder, folderId, true, !flag, exchangeId, exchangeId2);
			if (flag)
			{
				folder.SetProperty(context, PropTag.Folder.InternalAccess, true);
			}
			folder.Save(context);
			return folder;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0003D419 File Offset: 0x0003B619
		public static Folder CreateFolder(Context context, Mailbox mailbox)
		{
			return Folder.CreateFolder(context, mailbox, ExchangeId.Zero);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0003D428 File Offset: 0x0003B628
		public static Folder CreateFolder(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			if (folderId.IsNullOrZero)
			{
				folderId = mailbox.GetNextFolderId(context);
			}
			else
			{
				mailbox.GetOpenedPropertyBag(context, folderId);
			}
			Folder folder = new Folder(context, mailbox, null, folderId, true, false, ExchangeId.Null, ExchangeId.Null);
			folder.Save(context);
			return folder;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0003D470 File Offset: 0x0003B670
		[Conditional("DEBUG")]
		public void AssertProperExclusiveLockHeld(Context context)
		{
			SearchFolder searchFolder = this as SearchFolder;
			if (searchFolder != null)
			{
				searchFolder.GetNullableSearchGuid(context);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0003D490 File Offset: 0x0003B690
		internal static bool SetReadMessage(Context context, Folder source, ExchangeId itemId, bool read, out bool hasChanged, out ExchangeId readCn)
		{
			bool result;
			using (TopMessage topMessage = TopMessage.OpenMessage(context, source.Mailbox, source.GetId(context), itemId))
			{
				if (topMessage == null)
				{
					readCn = ExchangeId.Null;
					hasChanged = false;
					result = false;
				}
				else
				{
					Folder.SetReadMessage(context, topMessage, read, out hasChanged, out readCn);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0003D4F4 File Offset: 0x0003B6F4
		internal static void SetReadMessage(Context context, TopMessage msg, bool read, out bool hasChanged, out ExchangeId readCn)
		{
			hasChanged = msg.SetIsRead(context, read);
			if (hasChanged)
			{
				if (!msg.ParentFolder.IsPerUserReadUnreadTrackingEnabled)
				{
					msg.SaveChanges(context);
				}
				readCn = msg.GetLcnReadUnread(context);
				return;
			}
			readCn = ExchangeId.Null;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0003D534 File Offset: 0x0003B734
		public static string FolderNameForMaterializedRestriction(Context context, Mailbox mailbox, ExchangeId folderId, bool? hiddenItemView, bool instantSearch, Restriction restriction)
		{
			return Folder.FolderNameForMaterializedRestriction(mailbox, folderId, mailbox.SharedState.SupportsPerUserFeatures ? new Guid?(context.UserIdentity) : null, hiddenItemView, instantSearch, restriction);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0003D570 File Offset: 0x0003B770
		public static string FolderNameForMaterializedRestriction(Mailbox mailbox, ExchangeId folderId, Guid? userIdentity, bool? hiddenItemView, bool instantSearch, Restriction restriction)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.AppendFormat("{0:X16}-", folderId.ToLong());
			if (instantSearch)
			{
				stringBuilder.AppendFormat("I-{0:N}", Guid.NewGuid());
			}
			else
			{
				using (Md5Hasher md5Hasher = new Md5Hasher())
				{
					Guid guid = new Guid(md5Hasher.ComputeHash(restriction.Serialize()));
					stringBuilder.AppendFormat("R-{0}-{1:N}", (hiddenItemView != null) ? (hiddenItemView.Value ? "H" : "V") : "A", guid);
				}
				if (userIdentity != null)
				{
					stringBuilder.AppendFormat("-{0:N}", userIdentity.Value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0003D650 File Offset: 0x0003B850
		public static IList<Properties> GetRestrictionsProperties(Context context, Mailbox mailbox, ExchangeId folderId, ExchangeId parentId, StorePropTag[] storePropTags)
		{
			if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.AppendFormat("Entering Folder.GetRestrictionsProperties: folder {0}", folderId.ToString());
				stringBuilder.Append(" StorePropTag:{");
				foreach (StorePropTag storePropTag in storePropTags)
				{
					storePropTag.AppendToString(stringBuilder, true);
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("}");
				ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			List<Properties> list = new List<Properties>();
			if (Folder.OpenFolder(context, mailbox, folderId) == null)
			{
				return list;
			}
			ExchangeShortId exchangeShortId = parentId.ToExchangeShortId();
			FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, mailbox, exchangeShortId, FolderInformationType.Basic);
			if (folderHierarchy == null)
			{
				return list;
			}
			IFolderInformation parent = folderHierarchy.Find(context, exchangeShortId);
			string text = string.Format("{0:X16}-R-V", folderId.ToLong());
			int num;
			folderHierarchy.FindByName(context, exchangeShortId, text, context.Culture.CompareInfo, true, out num);
			if (num < 0)
			{
				return list;
			}
			IList<IFolderInformation> children = folderHierarchy.GetChildren(context, parent);
			while (num < children.Count && children[num].DisplayName.StartsWith(text))
			{
				ExchangeId exchangeId = ExchangeId.CreateFromInternalShortId(context, mailbox.ReplidGuidMap, children[num].Fid);
				string displayName = children[num].DisplayName;
				if (displayName.Length == 53)
				{
					int num2 = 0;
					DateTime dateTime = DateTime.MinValue;
					SearchFolder searchFolder = (SearchFolder)Folder.OpenFolder(context, mailbox, exchangeId);
					int? logicalIndexNumber = searchFolder.GetLogicalIndexNumber(context);
					if (logicalIndexNumber != null)
					{
						LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, mailbox, exchangeId, logicalIndexNumber.Value);
						num2 = CultureHelper.GetLcidFromCulture(logicalIndex.GetCulture());
						dateTime = logicalIndex.LastReferenceDate;
					}
					Properties item = new Properties(storePropTags.Length);
					int j = 0;
					while (j < storePropTags.Length)
					{
						StorePropTag tag = storePropTags[j];
						uint propTag = tag.PropTag;
						object obj;
						if (propTag <= 906100739U)
						{
							if (propTag == 268370178U)
							{
								obj = exchangeId.To22ByteArray();
								goto IL_29A;
							}
							if (propTag != 805371935U && propTag != 906100739U)
							{
								goto IL_28A;
							}
							goto IL_26F;
						}
						else if (propTag <= 1736966147U)
						{
							if (propTag == 906166275U)
							{
								goto IL_26F;
							}
							if (propTag != 1736966147U)
							{
								goto IL_28A;
							}
							obj = num2;
							goto IL_29A;
						}
						else
						{
							if (propTag == 1739587837U)
							{
								byte[] array;
								IList<ExchangeId> list2;
								SearchState searchState;
								searchFolder.GetSearchCriteria(context, GetSearchCriteriaFlags.Restriction, out array, out list2, out searchState);
								obj = array;
								goto IL_29A;
							}
							if (propTag != 1743978560U)
							{
								goto IL_28A;
							}
							obj = dateTime;
							goto IL_29A;
						}
						IL_2A5:
						j++;
						continue;
						IL_28A:
						item.Add(Property.NotFoundError(tag));
						goto IL_2A5;
						IL_29A:
						item.Add(tag, obj);
						goto IL_2A5;
						IL_26F:
						obj = searchFolder.GetPropertyValue(context, StorePropTag.CreateWithoutInfo(tag.PropTag, ObjectType.Folder));
						if (obj == null)
						{
							goto IL_28A;
						}
						goto IL_29A;
					}
					list.Add(item);
				}
				num++;
			}
			return list;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0003D950 File Offset: 0x0003BB50
		public static IList<Properties> GetViewsProperties(Context context, Mailbox mailbox, ExchangeId folderId, StorePropTag[] storePropTags)
		{
			if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.AppendFormat("Entering Folder.GetViewsProperties: folder {0}", folderId.ToString());
				stringBuilder.Append(" StorePropTag:{");
				foreach (StorePropTag storePropTag in storePropTags)
				{
					storePropTag.AppendToString(stringBuilder, true);
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("}");
				ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			List<Properties> list = new List<Properties>();
			bool flag = false;
			bool flag2 = false;
			DateTime dateTime = DateTime.MinValue;
			DateTime dateTime2 = DateTime.MinValue;
			HashSet<int> hashSet = null;
			HashSet<int> hashSet2 = null;
			foreach (LogicalIndex logicalIndex in from li in LogicalIndexCache.GetIndicesForFolder(context, mailbox, folderId)
			orderby li.LogicalIndexNumber
			select li)
			{
				if (logicalIndex.IndexType == LogicalIndexType.SearchFolderBaseView || logicalIndex.IndexType == LogicalIndexType.ConversationDeleteHistory || logicalIndex.IsStale)
				{
					if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<int, LogicalIndexType, bool>(0L, "{1} index {0} skipped. Stale={2}", logicalIndex.LogicalIndexNumber, logicalIndex.IndexType, logicalIndex.IsStale);
					}
				}
				else
				{
					LogicalIndex logicalIndex2 = null;
					if (logicalIndex.IndexType == LogicalIndexType.CategoryHeaders)
					{
						logicalIndex2 = LogicalIndexCache.GetLogicalIndex(context, mailbox, folderId, logicalIndex.CategorizationInfo.BaseMessageViewLogicalIndexNumber);
						if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<int, int>(0L, "Index {0} is category headers, looking for sort order in base message view logical index {1}", logicalIndex.LogicalIndexNumber, logicalIndex2.LogicalIndexNumber);
						}
					}
					else
					{
						logicalIndex2 = logicalIndex;
						if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<int>(0L, "Looking for sort order from index {0}", logicalIndex.LogicalIndexNumber);
						}
					}
					if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<SortOrder>(0L, "Sort order: {0}", logicalIndex2.LogicalSortOrder);
					}
					bool flag3 = false;
					int num = 0;
					int num2 = 0;
					int num3 = (logicalIndex.IndexType == LogicalIndexType.CategoryHeaders) ? logicalIndex.CategorizationInfo.CategoryCount : 0;
					foreach (SortColumn sortColumn in logicalIndex2.LogicalSortOrder)
					{
						ExtendedPropertyColumn extendedPropertyColumn = sortColumn.Column as ExtendedPropertyColumn;
						if (extendedPropertyColumn != null)
						{
							if (num == 0 && extendedPropertyColumn.StorePropTag == PropTag.Message.Internal9ByteChangeNumber)
							{
								if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<int>(0L, "Index {0} is ICS index", logicalIndex2.LogicalIndexNumber);
								}
								flag3 = true;
							}
							num++;
							if (logicalIndex.IndexType == LogicalIndexType.CategoryHeaders && num2 < num3)
							{
								CategoryHeaderSortOverride categoryHeaderSortOverride = logicalIndex.CategorizationInfo.CategoryHeaderSortOverrides[num2];
								if (categoryHeaderSortOverride != null)
								{
									extendedPropertyColumn = (categoryHeaderSortOverride.Column as ExtendedPropertyColumn);
									if (extendedPropertyColumn != null)
									{
										num++;
									}
								}
								num2++;
							}
						}
					}
					if (num == 0)
					{
						if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<int, int>(0L, "Index {0} has 0 property column, skip", logicalIndex.LogicalIndexNumber, num);
						}
					}
					else
					{
						if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<int>(0L, "Number of property column of this view: {0}", num);
						}
						if (flag3)
						{
							HashSet<int> hashSet3;
							if (logicalIndex.IndexType == LogicalIndexType.Conversations)
							{
								if (!flag2)
								{
									flag2 = true;
									hashSet2 = new HashSet<int>();
								}
								if (dateTime2 < logicalIndex.LastReferenceDate)
								{
									dateTime2 = logicalIndex.LastReferenceDate;
								}
								hashSet3 = hashSet2;
							}
							else
							{
								if (!flag)
								{
									flag = true;
									hashSet = new HashSet<int>();
								}
								if (dateTime < logicalIndex.LastReferenceDate)
								{
									dateTime = logicalIndex.LastReferenceDate;
								}
								hashSet3 = hashSet;
							}
							if (logicalIndex.NonKeyColumns == null)
							{
								continue;
							}
							using (IEnumerator<Column> enumerator3 = logicalIndex.NonKeyColumns.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									Column column = enumerator3.Current;
									ExtendedPropertyColumn extendedPropertyColumn2 = column as ExtendedPropertyColumn;
									if (extendedPropertyColumn2 != null)
									{
										hashSet3.Add((int)extendedPropertyColumn2.StorePropTag.PropTag);
									}
								}
								continue;
							}
						}
						Properties item = new Properties(storePropTags.Length);
						foreach (StorePropTag storePropTag2 in storePropTags)
						{
							if (storePropTag2 == PropTag.ViewDefinition.SortOrder)
							{
								int num4 = 12 + 8 * num;
								byte[] array = new byte[num4];
								int num5 = 0;
								num5 += ParseSerialize.SerializeInt32(num, array, num5);
								num3 = ((logicalIndex.IndexType == LogicalIndexType.CategoryHeaders) ? logicalIndex.CategorizationInfo.CategoryCount : 0);
								if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<int>(0L, "Category count: {0}", num3);
								}
								num5 += ParseSerialize.SerializeInt32(num3, array, num5);
								num5 += ParseSerialize.SerializeInt32(0, array, num5);
								SortOrder sortOrder = logicalIndex2.LogicalSortOrder;
								if (logicalIndex.IndexType == LogicalIndexType.CategoryHeaders && logicalIndex.CategorizationInfo.BaseMessageViewInReverseOrder)
								{
									sortOrder = sortOrder.Reverse();
								}
								num2 = 0;
								foreach (SortColumn sortColumn2 in sortOrder)
								{
									ExtendedPropertyColumn extendedPropertyColumn3 = sortColumn2.Column as ExtendedPropertyColumn;
									if (extendedPropertyColumn3 != null)
									{
										num5 += ParseSerialize.SerializeInt32((int)extendedPropertyColumn3.StorePropTag.PropTag, array, num5);
										int num6 = sortColumn2.Ascending ? 0 : 1;
										num5 += ParseSerialize.SerializeInt32(num6, array, num5);
										if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
										{
											ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<StorePropTag, int>(0L, "Serialized sort PropTag {0} with flags {1}", extendedPropertyColumn3.StorePropTag, num6);
										}
										if (logicalIndex.IndexType == LogicalIndexType.CategoryHeaders && num2 < num3)
										{
											CategoryHeaderSortOverride categoryHeaderSortOverride2 = logicalIndex.CategorizationInfo.CategoryHeaderSortOverrides[num2];
											if (categoryHeaderSortOverride2 != null)
											{
												extendedPropertyColumn3 = (categoryHeaderSortOverride2.Column as ExtendedPropertyColumn);
												if (extendedPropertyColumn3 != null)
												{
													num5 += ParseSerialize.SerializeInt32((int)extendedPropertyColumn3.StorePropTag.PropTag, array, num5);
													num6 = (categoryHeaderSortOverride2.AggregateByMaxValue ? 4 : 8);
													num5 += ParseSerialize.SerializeInt32(num6, array, num5);
													if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
													{
														ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug<StorePropTag, int>(0L, "Serialized sort override PropTag {0} with flags {1}", extendedPropertyColumn3.StorePropTag, num6);
													}
												}
											}
											num2++;
										}
									}
								}
								item.Add(storePropTag2, array);
							}
							else if (storePropTag2 == PropTag.ViewDefinition.LCID)
							{
								item.Add(storePropTag2, CultureHelper.GetLcidFromCulture(logicalIndex.GetCulture()));
							}
							else if (storePropTag2 == PropTag.ViewDefinition.CategCount)
							{
								num3 = ((logicalIndex.IndexType == LogicalIndexType.CategoryHeaders) ? logicalIndex.CategorizationInfo.CategoryCount : 0);
								item.Add(storePropTag2, num3);
							}
							else if (storePropTag2 == PropTag.ViewDefinition.SoftDeletedFilter)
							{
								item.Add(storePropTag2, false);
							}
							else if (storePropTag2 == PropTag.ViewDefinition.AssociatedFilter)
							{
								item.Add(storePropTag2, logicalIndex.ConditionalIndexValue);
							}
							else if (storePropTag2 == PropTag.ViewDefinition.ConversationsFilter)
							{
								item.Add(storePropTag2, logicalIndex.IndexType == LogicalIndexType.Conversations);
							}
							else if (storePropTag2 == PropTag.ViewDefinition.ViewAccessTime)
							{
								item.Add(storePropTag2, logicalIndex.LastReferenceDate);
							}
							else if (storePropTag2 == PropTag.ViewDefinition.ICSViewFilter)
							{
								item.Add(storePropTag2, false);
							}
							else
							{
								item.Add(Property.NotFoundError(storePropTag2));
							}
						}
						list.Add(item);
					}
				}
			}
			if (flag)
			{
				Properties item2 = Folder.CreateICSViewProperties(context, dateTime, false, storePropTags, hashSet);
				list.Add(item2);
			}
			if (flag2)
			{
				Properties item3 = Folder.CreateICSViewProperties(context, dateTime2, true, storePropTags, hashSet2);
				list.Add(item3);
			}
			if (ExTraceGlobals.GetViewsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.GetViewsPropertiesTracer.TraceDebug(0L, "Leaving GetViewsPropeties");
			}
			return list;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0003E1E0 File Offset: 0x0003C3E0
		public static bool ComputePerUserDisabledState(bool isPublicFolderMailbox, bool isSearchFolder, MailboxInfo.MailboxTypeDetail mailboxTypeDetail, bool? isPerUserDisabled)
		{
			return isSearchFolder || (mailboxTypeDetail != MailboxInfo.MailboxTypeDetail.TeamMailbox && isPerUserDisabled.GetValueOrDefault(!isPublicFolderMailbox));
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
		public static string GetFolderPathName(Context context, Mailbox mailbox, ExchangeId folderId, char pathSeparator)
		{
			FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, mailbox, ExchangeShortId.Zero, FolderInformationType.Basic);
			ExchangeId[] specialFolders = SpecialFoldersCache.GetSpecialFolders(context, mailbox);
			string item = new string(pathSeparator, 1);
			Stack<string> stack = new Stack<string>(20);
			IFolderInformation folderInformation = folderHierarchy.Find(context, folderId.ToExchangeShortId());
			while (folderInformation != null)
			{
				stack.Push(folderInformation.DisplayName);
				stack.Push(item);
				folderInformation = folderHierarchy.GetParent(context, folderInformation);
				if (folderInformation != null && (folderInformation.Fid == specialFolders[1].ToExchangeShortId() || (folderInformation.Fid == specialFolders[9].ToExchangeShortId() && !mailbox.IsPublicFolderMailbox) || (folderInformation.Fid == specialFolders[7].ToExchangeShortId() && mailbox.IsPublicFolderMailbox)))
				{
					break;
				}
			}
			if (stack.Count == 0)
			{
				return string.Empty;
			}
			return string.Concat(stack.ToArray());
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		public static ExchangeId FindFolderIdByName(Context context, ExchangeId parentFid, string displayName, Mailbox mailbox)
		{
			if (!parentFid.IsNull)
			{
				CultureInfo cultureInfo = CultureHelper.TranslateLcid(mailbox.GetLCID(context));
				FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, mailbox, ExchangeShortId.Zero, FolderInformationType.Basic);
				IFolderInformation folderInformation = folderHierarchy.FindByName(context, parentFid.ToExchangeShortId(), displayName, cultureInfo.CompareInfo);
				if (folderInformation != null)
				{
					return ExchangeId.CreateFromInternalShortId(context, mailbox.ReplidGuidMap, folderInformation.Fid);
				}
			}
			return ExchangeId.Zero;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0003E346 File Offset: 0x0003C546
		internal static IDisposable SetPromotionTaskTestCallback(Action<Folder.PromotionExecutionType> callback)
		{
			return Folder.PromotionTaskTestCallback.SetTestHook(callback);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0003E353 File Offset: 0x0003C553
		internal static IDisposable SetPromotionTaskContentionCallback(Action callback)
		{
			return Folder.promotionTaskContentionCallback.SetTestHook(callback);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0003E360 File Offset: 0x0003C560
		internal static IDisposable SetUpdateAggregateUnreadCountTestHook(Action<Folder, long> callback)
		{
			return Folder.updateAggregateUnreadCountTestHook.SetTestHook(callback);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0003E36D File Offset: 0x0003C56D
		internal static bool IsAnyPromotionInProgress(Context context, Mailbox mailbox)
		{
			return Folder.PropertyPromotionState.IsAnyPromotionInProgress(context, mailbox);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0003E378 File Offset: 0x0003C578
		protected static string StorePropTagListToString(IList<StorePropTag> propertyTags, int resultMaxLength)
		{
			StringBuilder stringBuilder = new StringBuilder(resultMaxLength / 8);
			foreach (StorePropTag storePropTag in propertyTags)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(", ");
				}
				string text;
				if (storePropTag.IsNamedProperty)
				{
					text = storePropTag.DescriptiveName;
					if (text == null)
					{
						text = storePropTag.PropName.ToString();
					}
				}
				else
				{
					text = storePropTag.PropId.ToString("X4");
				}
				if (stringBuilder.Length + text.Length + 2 + 3 > resultMaxLength)
				{
					stringBuilder.Append("...");
					break;
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0003E444 File Offset: 0x0003C644
		private static object PromoteMessagePropertiesColumnFunction(object[] arguments)
		{
			byte[] onPageBlob = (byte[])arguments[0];
			byte[] offPageBlob = (byte[])arguments[1];
			short[] array = (short[])arguments[2];
			HashSet<ushort> hashSet = new HashSet<ushort>();
			foreach (short num in array)
			{
				hashSet.Add((ushort)num);
			}
			return PropertyBlob.PromoteProperties(onPageBlob, offPageBlob, hashSet);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0003E4A4 File Offset: 0x0003C6A4
		private static void CheckForFolderCycle(Context context, Folder sourceFolder, Folder destinationParentFolder)
		{
			ExchangeId id = sourceFolder.GetId(context);
			for (Folder folder = destinationParentFolder; folder != null; folder = folder.GetParentFolder(context))
			{
				if (id == folder.GetId(context))
				{
					throw new StoreException((LID)45048U, ErrorCodeValue.FolderCycle, "Source folder is ancestor of destination parent folder");
				}
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0003E618 File Offset: 0x0003C818
		private static IEnumerator<MailboxTaskQueue.TaskStepResult> BootstrapPromotionTask(MailboxTaskContext mailboxTaskContext, ExchangeId folderId, bool hiddenItemView, IList<StorePropTag> newPropertiesToPromote, bool promotePropertiesForMessages)
		{
			Folder folder = Folder.OpenFolder(mailboxTaskContext, mailboxTaskContext.Mailbox, folderId);
			if (folder != null)
			{
				bool flag = newPropertiesToPromote.Count == 0;
				if (!flag)
				{
					HashSet<ushort> promotedUniquePropertyIdsForMessages = folder.GetPromotedUniquePropertyIdsForMessages(mailboxTaskContext, hiddenItemView);
					for (int i = 0; i < newPropertiesToPromote.Count; i++)
					{
						if (!promotedUniquePropertyIdsForMessages.Contains(newPropertiesToPromote[i].PropId))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Promotion bootstrap task: starting promotion for folder {0}", folderId);
					}
					folder.DoPromoteProperties(mailboxTaskContext, hiddenItemView, newPropertiesToPromote, promotePropertiesForMessages);
				}
			}
			yield break;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0003EA78 File Offset: 0x0003CC78
		private static IEnumerator<MailboxTaskQueue.TaskStepResult> DoPromotePropertiesImpl(Context context, int mailboxPartitionNumber, byte[] folderId, bool promoteHidden, MessageTable messageTable, FunctionColumn promoteFunctionColumn, Func<bool> shouldTaskContinue)
		{
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailboxPartitionNumber,
				folderId,
				promoteHidden
			});
			bool completed = false;
			try
			{
				using (TableOperator messageTableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, null, new List<Column>
				{
					messageTable.OffPagePropertyBlob
				}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), true, true))
				{
					messageTableOperator.PrereadChunkSize = ConfigurationSchema.PromotionChunkSize.Value;
					using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(context.Culture, context, messageTableOperator, new Column[]
					{
						messageTable.PropertyBlob
					}, new object[]
					{
						promoteFunctionColumn
					}, true))
					{
						if (shouldTaskContinue != null)
						{
							updateOperator.EnableInterrupts(new WriteChunkingInterruptControl(ConfigurationSchema.PromotionChunkSize.Value, shouldTaskContinue));
						}
						int messagesUpdated = (int)updateOperator.ExecuteScalar();
						int messagesUpdatedTotal = messagesUpdated;
						while (updateOperator.Interrupted)
						{
							if (context.PerfInstance != null)
							{
								context.PerfInstance.PropertyPromotionMessageRate.IncrementBy((long)messagesUpdated);
							}
							if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.FolderTracer.TraceDebug<int>(0L, "Promotion chunk completed. Messages updated in this chunk: {0}", messagesUpdated);
							}
							yield return MailboxTaskQueue.TaskStepResult.Result(null);
							messagesUpdated = (int)updateOperator.ExecuteScalar();
							messagesUpdatedTotal += messagesUpdated;
						}
						if (context.PerfInstance != null)
						{
							context.PerfInstance.PropertyPromotionMessageRate.IncrementBy((long)messagesUpdated);
						}
						if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.FolderTracer.TraceDebug<int, int>(0L, "Promotion completed. Messages updated in this chunk: {0}. Total messages updated: {1}", messagesUpdated, messagesUpdatedTotal);
						}
						completed = true;
					}
				}
			}
			finally
			{
				if (!completed && ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.FolderTracer.TraceDebug(0L, "Promotion aborted");
				}
			}
			yield break;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0003EAC4 File Offset: 0x0003CCC4
		private static void GetPromoteFunctionColumn(Context context, Folder folder, bool promoteHidden, out MessageTable messageTable, out FunctionColumn promoteFunctionColumn)
		{
			messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(folder.Mailbox.Database);
			HashSet<ushort> promotedUniquePropertyIdsForMessages = folder.GetPromotedUniquePropertyIdsForMessages(context, promoteHidden);
			short[] array = new short[promotedUniquePropertyIdsForMessages.Count];
			int num = 0;
			foreach (ushort num2 in promotedUniquePropertyIdsForMessages)
			{
				array[num++] = (short)num2;
			}
			promoteFunctionColumn = Factory.CreateFunctionColumn("PromoteProperties", typeof(byte[]), 0, 1073741824, messageTable.Table, new Func<object[], object>(Folder.PromoteMessagePropertiesColumnFunction), "Exchange.promoteProperties", new Column[]
			{
				messageTable.PropertyBlob,
				messageTable.OffPagePropertyBlob,
				Factory.CreateConstantColumn(array)
			});
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0003EBA0 File Offset: 0x0003CDA0
		private static void DoPromotePropertiesSynchronously(Context context, Folder folder, bool promoteHidden)
		{
			if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId, long>(0L, "Synchronous promotion for folder {0} has started, {1}", folder.GetId(context), (context.PerfInstance == null) ? -1L : context.PerfInstance.PropertyPromotionTasks.RawValue);
			}
			if (context.PerfInstance != null)
			{
				context.PerfInstance.PropertyPromotionTasks.Increment();
			}
			try
			{
				MessageTable messageTable;
				FunctionColumn promoteFunctionColumn;
				Folder.GetPromoteFunctionColumn(context, folder, promoteHidden, out messageTable, out promoteFunctionColumn);
				using (IEnumerator<MailboxTaskQueue.TaskStepResult> enumerator = Folder.DoPromotePropertiesImpl(context, folder.Mailbox.MailboxPartitionNumber, folder.GetId(context).To26ByteArray(), promoteHidden, messageTable, promoteFunctionColumn, null))
				{
					while (enumerator.MoveNext())
					{
					}
				}
			}
			finally
			{
				if (context.PerfInstance != null)
				{
					context.PerfInstance.PropertyPromotionTasks.Decrement();
				}
				if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId, long>(0L, "Synchronous promotion for folder {0} has finished, {1}", folder.GetId(context), (context.PerfInstance == null) ? -1L : context.PerfInstance.PropertyPromotionTasks.RawValue);
				}
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0003F3D0 File Offset: 0x0003D5D0
		private static IEnumerator<MailboxTaskQueue.TaskStepResult> DoPromotePropertiesTask(MailboxTaskContext mailboxTaskContext, ExchangeId folderId, bool promoteHidden, DateTime promotionTimeStamp, Func<bool> shouldTaskContinue)
		{
			if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId, long>(0L, "Promotion task for folder {0} has started, {1}", folderId, (mailboxTaskContext.PerfInstance == null) ? -1L : mailboxTaskContext.PerfInstance.PropertyPromotionTasks.RawValue);
			}
			if (mailboxTaskContext.PerfInstance != null)
			{
				mailboxTaskContext.PerfInstance.PropertyPromotionTasks.Increment();
			}
			int mailboxNumber = mailboxTaskContext.Mailbox.SharedState.MailboxNumber;
			try
			{
				if (Folder.PropertyPromotionState.StartActivePromotion())
				{
					try
					{
						Folder folder = Folder.OpenFolder(mailboxTaskContext, mailboxTaskContext.Mailbox, folderId);
						if (folder != null)
						{
							if (Folder.PropertyPromotionState.HasPromotionChanged(mailboxTaskContext, mailboxTaskContext.Mailbox.SharedState, folderId, promoteHidden, promotionTimeStamp))
							{
								if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Promotion parameters for folder {0} changed, terminating promotion", folderId);
								}
								yield break;
							}
							MessageTable messageTable;
							FunctionColumn promoteFunctionColumn;
							Folder.GetPromoteFunctionColumn(mailboxTaskContext, folder, promoteHidden, out messageTable, out promoteFunctionColumn);
							if (Folder.promotionTaskContentionCallback.Value != null)
							{
								Folder.promotionTaskContentionCallback.Value();
							}
							using (IEnumerator<MailboxTaskQueue.TaskStepResult> stepResults = Folder.DoPromotePropertiesImpl(mailboxTaskContext, mailboxTaskContext.Mailbox.MailboxPartitionNumber, folderId.To26ByteArray(), promoteHidden, messageTable, promoteFunctionColumn, () => shouldTaskContinue() && !mailboxTaskContext.HasMailboxLockContention))
							{
								while (!Folder.IsMRSOnlyPreservingMailboxSignatureMove(mailboxTaskContext) && stepResults.MoveNext())
								{
									if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Promotion taks for folder {0} yielding", folderId);
									}
									Action postStepAction;
									if (Folder.PromotionTaskTestCallback.Value != null)
									{
										postStepAction = delegate()
										{
											Folder.PromotionTaskTestCallback.Value(Folder.PromotionExecutionType.Mid);
										};
									}
									else
									{
										postStepAction = null;
									}
									yield return MailboxTaskQueue.TaskStepResult.Result(postStepAction);
									if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Promotion taks for folder {0} continuing", folderId);
									}
									if (Folder.PropertyPromotionState.HasPromotionChanged(mailboxTaskContext, mailboxTaskContext.Mailbox.SharedState, folderId, promoteHidden, promotionTimeStamp))
									{
										if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
										{
											ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Promotion parameters for folder {0} changed, terminating promotion (#2)", folderId);
										}
										yield break;
									}
								}
							}
							if (Folder.IsMRSOnlyPreservingMailboxSignatureMove(mailboxTaskContext))
							{
								if (Folder.PromotionTaskTestCallback.Value != null)
								{
									Folder.PromotionTaskTestCallback.Value(Folder.PromotionExecutionType.Skip);
								}
								yield break;
							}
							if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Promotion for folder {0} is complete", folderId);
							}
							if (Folder.PromotionTaskTestCallback.Value != null)
							{
								Folder.PromotionTaskTestCallback.Value(Folder.PromotionExecutionType.Last);
							}
							Folder.PropertyPromotionState.MarkPromotionCompleted(mailboxTaskContext, folder, promoteHidden);
						}
					}
					finally
					{
						Folder.PropertyPromotionState.EndActivePromotion();
					}
				}
			}
			finally
			{
				bool flag = false;
				try
				{
					if (!mailboxTaskContext.IsMailboxOperationStarted)
					{
						if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Promotion task for folder {0} is terminating before completion (enumerable is disposed)", folderId);
						}
						ErrorCode first = mailboxTaskContext.StartMailboxOperationForFailureHandling();
						if (first == ErrorCode.NoError)
						{
							flag = true;
						}
					}
					if (mailboxTaskContext.IsMailboxOperationStarted)
					{
						MailboxState lockedMailboxState = mailboxTaskContext.LockedMailboxState;
						if (lockedMailboxState.IsAccessible)
						{
							Folder.PropertyPromotionState.ResetPromotionInProgress(mailboxTaskContext, lockedMailboxState, folderId, promoteHidden, promotionTimeStamp);
						}
						if (mailboxTaskContext.PerfInstance != null)
						{
							mailboxTaskContext.PerfInstance.PropertyPromotionTasks.Decrement();
						}
					}
					if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId, long>(0L, "Promotion task for folder {0} has finished, {1}", folderId, (mailboxTaskContext.PerfInstance == null) ? -1L : mailboxTaskContext.PerfInstance.PropertyPromotionTasks.RawValue);
					}
				}
				finally
				{
					if (flag)
					{
						mailboxTaskContext.EndMailboxOperation(false);
					}
				}
			}
			yield break;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0003F40C File Offset: 0x0003D60C
		private static bool IsMRSOnlyPreservingMailboxSignatureMove(MailboxTaskContext mailboxTaskContext)
		{
			return !mailboxTaskContext.Mailbox.GetPreservingMailboxSignature(mailboxTaskContext) && mailboxTaskContext.Mailbox.GetMRSPreservingMailboxSignature(mailboxTaskContext);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0003F438 File Offset: 0x0003D638
		private static Properties CreateICSViewProperties(Context context, DateTime lastReferenceDate, bool isConversationIndex, StorePropTag[] storePropTags, HashSet<int> coveringProperties)
		{
			Properties result = new Properties(storePropTags.Length);
			foreach (StorePropTag storePropTag in storePropTags)
			{
				if (storePropTag == PropTag.ViewDefinition.ConversationsFilter)
				{
					result.Add(storePropTag, isConversationIndex);
				}
				else if (storePropTag == PropTag.ViewDefinition.ViewAccessTime)
				{
					result.Add(storePropTag, lastReferenceDate);
				}
				else if (storePropTag == PropTag.ViewDefinition.ViewCoveringPropertyTags)
				{
					result.Add(storePropTag, coveringProperties.ToArray<int>());
				}
				else if (storePropTag == PropTag.ViewDefinition.ICSViewFilter)
				{
					result.Add(storePropTag, true);
				}
				else
				{
					result.Add(Property.NotFoundError(storePropTag));
				}
			}
			return result;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		public void AddDoNotDeleteReference(object value)
		{
			if (base.IsDead)
			{
				return;
			}
			HashSet<object> hashSet = (HashSet<object>)base.GetComponentData(Folder.doNotDeleteReferencesDataSlot);
			if (hashSet == null)
			{
				hashSet = new Folder.DoNotDeleteReferenceHolder();
				HashSet<object> hashSet2 = (HashSet<object>)base.CompareExchangeComponentData(Folder.doNotDeleteReferencesDataSlot, null, hashSet);
				if (hashSet2 != null)
				{
					hashSet = hashSet2;
				}
			}
			using (LockManager.Lock(hashSet, LockManager.LockType.LeafMonitorLock))
			{
				hashSet.Add(value);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0003F574 File Offset: 0x0003D774
		public void RemoveDoNotDeleteReference(object value)
		{
			if (base.IsDead)
			{
				return;
			}
			HashSet<object> hashSet = (HashSet<object>)base.GetComponentData(Folder.doNotDeleteReferencesDataSlot);
			if (hashSet == null)
			{
				return;
			}
			using (LockManager.Lock(hashSet, LockManager.LockType.LeafMonitorLock))
			{
				hashSet.Remove(value);
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0003F5D0 File Offset: 0x0003D7D0
		public UnlimitedBytes GetMaxPublicFolderItemSize(Context context)
		{
			return this.GetFolderQuotaProperty(context, 26402);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0003F5DE File Offset: 0x0003D7DE
		public void DirtyLastModificationTime(Context context)
		{
			base.DirtyColumn(context, this.folderTable.LastModificationTime);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0003F5F2 File Offset: 0x0003D7F2
		public bool IsSearchFolder(Context context)
		{
			return base.GetColumnValue(context, this.folderTable.QueryCriteria) != null;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0003F60C File Offset: 0x0003D80C
		public bool IsIpmFolder(Context context)
		{
			return ((bool?)this.GetPropertyValue(context, PropTag.Folder.IPMFolder)).GetValueOrDefault(true);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0003F633 File Offset: 0x0003D833
		public bool IsPartOfContentIndexing(Context context)
		{
			return (bool)this.GetPropertyValue(context, PropTag.Folder.PartOfContentIndexing);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0003F648 File Offset: 0x0003D848
		public void UpdatePerUserReadUnreadTrackingEnabled(Context context)
		{
			if (!base.IsDead)
			{
				MailboxInfo.MailboxTypeDetail mailboxTypeDetail = base.Mailbox.SharedState.MailboxTypeDetail;
				bool? isPerUserDisabled = (bool?)this.GetPropertyValue(context, PropTag.Folder.DisablePerUserRead);
				bool isSearchFolder = this.IsSearchFolder(context);
				bool isPublicFolderMailbox = base.Mailbox.IsPublicFolderMailbox;
				object value = (!Folder.ComputePerUserDisabledState(isPublicFolderMailbox, isSearchFolder, mailboxTypeDetail, isPerUserDisabled)) ? new SharedObjectPropertyBag.NonDiscardableComponentData<bool>(true) : null;
				base.SetComponentData(Folder.isPerUserReadUnreadTrackingEnabledSlot, value);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0003F6C4 File Offset: 0x0003D8C4
		public bool IsInternalAccess(Context context)
		{
			object propertyValue = this.GetPropertyValue(context, PropTag.Folder.InternalAccess);
			return propertyValue != null && (bool)propertyValue;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		public DateTime GetLastQuotaNotificationTime(Context context)
		{
			return ((DateTime?)this.GetPropertyValue(context, PropTag.Folder.LastQuotaNotificationTime)).GetValueOrDefault(DateTime.MinValue);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0003F717 File Offset: 0x0003D917
		public void SetLastQuotaNotificationTime(Context context, DateTime value)
		{
			this.SetProperty(context, PropTag.Folder.LastQuotaNotificationTime, value);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0003F72C File Offset: 0x0003D92C
		public void SetLocalCommitTimeMax(Context context, DateTime timeStamp)
		{
			base.SetColumn(context, this.folderTable.LocalCommitTimeMax, timeStamp);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0003F746 File Offset: 0x0003D946
		public void BumpAclTableVersion()
		{
			base.SetComponentData(Folder.aclTableVersionCookieDataSlot, new Folder.ACLTableVersionCookie());
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0003F758 File Offset: 0x0003D958
		public IList<ExchangeId> GetSearchBacklinks(Context context, bool recursive)
		{
			return this.GetSearchBacklinksNoAsserts(context, recursive);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0003F770 File Offset: 0x0003D970
		internal IList<ExchangeId> GetSearchBacklinksNoAsserts(Context context, bool recursive)
		{
			int num = 0;
			byte[] buff = (byte[])base.GetColumnValue(context, recursive ? this.FolderTable.RecursiveSearchBacklinks : this.FolderTable.NonRecursiveSearchBacklinks);
			return ExchangeIdListHelpers.ListFromBytes(context, base.Mailbox.ReplidGuidMap, buff, ref num);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0003F7BB File Offset: 0x0003D9BB
		public void SetSearchBacklinks(Context context, IList<ExchangeId> searchBacklinks, bool recursive)
		{
			this.SetSearchBacklinksNoAsserts(context, searchBacklinks, recursive);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0003F7C6 File Offset: 0x0003D9C6
		internal void SetSearchBacklinksNoAsserts(Context context, IList<ExchangeId> searchBacklinks, bool recursive)
		{
			base.SetColumn(context, recursive ? this.FolderTable.RecursiveSearchBacklinks : this.FolderTable.NonRecursiveSearchBacklinks, ExchangeIdListHelpers.BytesFromList(searchBacklinks, true));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0003F7F1 File Offset: 0x0003D9F1
		public void InvalidateIndexes(Context context, bool normal, bool associated)
		{
			this.InvalidateIndexes(context, normal, associated, DateTime.MaxValue);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0003F804 File Offset: 0x0003DA04
		public void InvalidateIndexes(Context context, bool normal, bool associated, DateTime lastReferenceDateThreshold)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
			if (normal)
			{
				LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, this.GetId(context), LogicalIndexType.Messages, messageTable.IsHidden, false, lastReferenceDateThreshold);
				LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, this.GetId(context), LogicalIndexType.SearchFolderMessages, messageTable.IsHidden, false, lastReferenceDateThreshold);
				LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, this.GetId(context), LogicalIndexType.Conversations, null, false, lastReferenceDateThreshold);
			}
			if (associated)
			{
				LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, this.GetId(context), LogicalIndexType.Messages, messageTable.IsHidden, true, lastReferenceDateThreshold);
				LogicalIndexCache.InvalidateIndexes(context, base.Mailbox, this.GetId(context), LogicalIndexType.SearchFolderMessages, messageTable.IsHidden, true, lastReferenceDateThreshold);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0003F8BC File Offset: 0x0003DABC
		public void InvalidateIndexesForFolderNameChange(Context context)
		{
			ISet<ExchangeId> set = new HashSet<ExchangeId>();
			set.Add(this.GetId(context));
			set.UnionWith(this.GetSearchBacklinks(context, false));
			set.UnionWith(this.GetSearchBacklinks(context, true));
			foreach (ExchangeId folderId in set)
			{
				LogicalIndexCache.InvalidateIndexesForFolderPropertyChange(context, base.Mailbox, folderId, PropTag.Message.ParentDisplay);
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0003F940 File Offset: 0x0003DB40
		public void RepopulateSearchFoldersForPerUserUpload(Context context)
		{
			ISet<ExchangeId> set = new HashSet<ExchangeId>();
			set.UnionWith(this.GetSearchBacklinks(context, false));
			set.UnionWith(this.GetSearchBacklinks(context, true));
			foreach (ExchangeId id in set)
			{
				SearchFolder searchFolder = (SearchFolder)Folder.OpenFolder(context, base.Mailbox, id);
				Guid? nullableSearchGuid = searchFolder.GetNullableSearchGuid(context);
				if (nullableSearchGuid != null && nullableSearchGuid.Value == context.UserIdentity)
				{
					searchFolder.Repopulate(context);
				}
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		public bool GetIsSpecialFolder(Context context)
		{
			return this.GetSpecialFolderNumber(context) != SpecialFolders.Regular;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0003F9F7 File Offset: 0x0003DBF7
		public bool GetIsReceiveFolder(Context context)
		{
			return this.GetPropertyValue(context, PropTag.Folder.SetReceiveCount) != null && (int)this.GetPropertyValue(context, PropTag.Folder.SetReceiveCount) != 0;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0003FA20 File Offset: 0x0003DC20
		public void PutPromotedProperties(Context context, HashSet<StorePropTag> promotedProperties, bool hiddenItemView)
		{
			if (hiddenItemView)
			{
				this.HiddenItemPromotedProperties = promotedProperties;
				this.HiddenItemPromotedPropertyIdsCache = null;
			}
			else
			{
				this.NormalItemPromotedProperties = promotedProperties;
				this.NormalItemPromotedPropertyIdsCache = null;
			}
			byte[] array = new byte[promotedProperties.Count * 4];
			int num = 0;
			foreach (StorePropTag storePropTag in promotedProperties)
			{
				ParseSerialize.SetDword(array, ref num, storePropTag.PropTag);
			}
			if (hiddenItemView)
			{
				base.SetColumn(context, this.folderTable.HiddenItemPromotedColumns, array);
				return;
			}
			base.SetColumn(context, this.folderTable.NormalItemPromotedColumns, array);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0003FAD4 File Offset: 0x0003DCD4
		public HashSet<StorePropTag> GetPromotedPropertiesForMessages(Context context, bool hiddenItemView)
		{
			HashSet<StorePropTag> hashSet;
			Column column;
			if (hiddenItemView)
			{
				hashSet = this.HiddenItemPromotedProperties;
				column = this.folderTable.HiddenItemPromotedColumns;
			}
			else
			{
				hashSet = this.NormalItemPromotedProperties;
				column = this.folderTable.NormalItemPromotedColumns;
			}
			if (hashSet == null)
			{
				hashSet = new HashSet<StorePropTag>();
				byte[] array = (byte[])base.GetColumnValue(context, column);
				if (array != null)
				{
					int num = array.Length / 4;
					int num2 = 0;
					for (int i = 0; i < num; i++)
					{
						uint dword = ParseSerialize.GetDword(array, ref num2, array.Length);
						hashSet.Add(base.Mailbox.GetStorePropTag(context, dword, ObjectType.Message));
					}
				}
				if (hiddenItemView)
				{
					this.HiddenItemPromotedProperties = hashSet;
				}
				else
				{
					this.NormalItemPromotedProperties = hashSet;
				}
			}
			return hashSet;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0003FB78 File Offset: 0x0003DD78
		public HashSet<ushort> GetPromotedUniquePropertyIdsForMessages(Context context, bool hiddenItemView)
		{
			HashSet<ushort> hashSet;
			if (hiddenItemView)
			{
				hashSet = this.HiddenItemPromotedPropertyIdsCache;
			}
			else
			{
				hashSet = this.NormalItemPromotedPropertyIdsCache;
			}
			if (hashSet == null)
			{
				hashSet = new HashSet<ushort>();
				foreach (StorePropTag storePropTag in this.GetPromotedPropertiesForMessages(context, hiddenItemView))
				{
					hashSet.Add(storePropTag.PropId);
				}
				if (hiddenItemView)
				{
					this.HiddenItemPromotedPropertyIdsCache = hashSet;
				}
				else
				{
					this.NormalItemPromotedPropertyIdsCache = hashSet;
				}
			}
			return hashSet;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0003FC04 File Offset: 0x0003DE04
		public bool HasLocalReplica(Context context)
		{
			if (!base.Mailbox.IsPublicFolderMailbox)
			{
				return true;
			}
			Guid[] replicaList = this.GetReplicaList(context);
			if (replicaList != null)
			{
				for (int i = 0; i < replicaList.Length; i++)
				{
					if (replicaList[i] == base.Mailbox.SharedState.MailboxGuid)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0003FC60 File Offset: 0x0003DE60
		public Guid[] GetReplicaList(Context context)
		{
			byte[] replicaBytes = (byte[])this.GetPropertyValue(context, PropTag.Folder.ReplicaList);
			return Folder.ConvertStorageReplicaListBlobToGuidArray(replicaBytes);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0003FC88 File Offset: 0x0003DE88
		public static Guid[] ConvertStorageReplicaListBlobToGuidArray(byte[] replicaBytes)
		{
			Guid[] array = null;
			if (replicaBytes != null)
			{
				if (replicaBytes.Length % 16 == 0)
				{
					array = new Guid[replicaBytes.Length / 16];
					for (int i = 0; i < replicaBytes.Length; i += 16)
					{
						array[i / 16] = ParseSerialize.ParseGuid(replicaBytes, i);
					}
				}
				else
				{
					if (replicaBytes.Length % 37 != 0)
					{
						throw new CorruptDataException((LID)62201U, "Not a valid replica list.");
					}
					array = new Guid[replicaBytes.Length / 37];
					for (int j = 0; j < replicaBytes.Length; j += 37)
					{
						array[j / 37] = Guid.Parse(ParseSerialize.ParseAsciiString(replicaBytes, j, 36));
					}
				}
			}
			return array;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0003FD30 File Offset: 0x0003DF30
		public ErrorCode SetReplicaList(Context context, Guid[] replicas)
		{
			byte[] array = new byte[replicas.Length * 16];
			for (int i = 0; i < replicas.Length; i++)
			{
				ParseSerialize.SerializeGuid(replicas[i], array, i * 16);
			}
			return this.SetProperty(context, PropTag.Folder.ReplicaList, array);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0003FD7C File Offset: 0x0003DF7C
		public void Move(Context context, Folder destination)
		{
			Folder parentFolder = this.GetParentFolder(context);
			if (parentFolder == null)
			{
				throw new StoreException((LID)52528U, ErrorCodeValue.FolderCycle, "Cannot move a root folder.");
			}
			Folder.CheckForFolderCycle(context, this, destination);
			if (destination.IsDumpsterMarkedFolder(context) && !base.Mailbox.IsPublicFolderMailbox)
			{
				throw new StoreException((LID)30552U, ErrorCodeValue.NoAccess, "Cannot move a folder to the dumpster.");
			}
			if (!this.IsSearchFolder(context))
			{
				SearchFolder.UpdateRecursiveSearchBacklinksForMove(context, this, parentFolder, destination);
			}
			this.originalParentFolderId = parentFolder.GetId(context);
			base.SetColumn(context, this.folderTable.ParentFolderId, destination.GetId(context).To26ByteArray());
			this.Save(context);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0003FE30 File Offset: 0x0003E030
		public override void Delete(Context context)
		{
			if (this.HasDoNotDeleteReferences)
			{
				return;
			}
			if (context.PerfInstance != null)
			{
				context.PerfInstance.FoldersDeletedRate.Increment();
			}
			if (base.IsSaved)
			{
				Folder parentFolder = this.GetParentFolder(context);
				if (parentFolder != null)
				{
					parentFolder.SetFolderCount(context, parentFolder.GetFolderCount(context) - 1L);
				}
			}
			SearchFolder.ProcessFolderDeletion(context, this);
			FolderDeletedNotificationEvent folderDeletedNotificationEvent = null;
			if (base.IsSaved)
			{
				folderDeletedNotificationEvent = NotificationEvents.CreateFolderDeletedEvent(context, this);
			}
			PerUser.DeleteAllResidentEntriesForFolder(context, this);
			ExchangeId id = this.GetId(context);
			IdSet folderIdsetIn = base.Mailbox.GetFolderIdsetIn(context);
			if (folderIdsetIn.Remove(id))
			{
				base.Mailbox.SetFolderIdsetIn(context, folderIdsetIn);
			}
			FolderHierarchy.OnFolderDeleted(context, this, this.GetParentFolderId(context).ToExchangeShortId());
			LogicalIndex.AddLogicalIndexMaintenanceBreadcrumb(context, base.Mailbox.MailboxPartitionNumber, LogicalIndex.LogicalOperation.FolderDeleted, new object[]
			{
				id.To26ByteArray(),
				this.GetParentFolderId(context).To26ByteArray()
			});
			this.InvalidateIndexes(context, true, true);
			base.Delete(context);
			base.Dispose();
			if (folderDeletedNotificationEvent != null)
			{
				context.RiseNotificationEvent(folderDeletedNotificationEvent);
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0003FF48 File Offset: 0x0003E148
		public virtual void Save(Context context)
		{
			bool needsToPublishNotification = this.NeedsToPublishNotification;
			bool flag = !base.IsSaved;
			ExtendedEventFlags extendedEventFlags = ExtendedEventFlags.None;
			if (this.IsDirty)
			{
				bool flag2 = false;
				if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.FolderTracer.TraceDebug<string, ExchangeId>(0L, "Saving {0}folder {1}", flag ? "new " : string.Empty, this.GetId(context));
				}
				ExchangeId nextChangeNumber = base.Mailbox.GetNextChangeNumber(context);
				DateTime utcNow = base.Mailbox.UtcNow;
				if (this.trackFolderChange)
				{
					if (!this.DataRow.ColumnDirty(this.folderTable.LastModifierSid) || base.GetColumnValue(context, this.folderTable.LastModifierSid) == null)
					{
						base.SetColumn(context, this.folderTable.LastModifierSid, new byte[0]);
					}
					if (!this.DataRow.ColumnDirty(this.folderTable.LcnCurrent) || base.GetColumnValue(context, this.folderTable.LcnCurrent) == null)
					{
						base.SetColumn(context, this.folderTable.LcnCurrent, nextChangeNumber.To26ByteArray());
					}
					if (!this.DataRow.ColumnDirty(this.folderTable.ChangeKey))
					{
						base.SetColumn(context, this.folderTable.ChangeKey, null);
					}
					if (!this.DataRow.ColumnDirty(this.folderTable.LastModificationTime) || base.GetColumnValue(context, this.folderTable.LastModificationTime) == null)
					{
						base.SetColumn(context, this.folderTable.LastModificationTime, utcNow);
					}
					if (!this.DataRow.ColumnDirty(this.folderTable.VersionHistory) || base.GetColumnValue(context, this.folderTable.VersionHistory) == null)
					{
						PCL pcl = default(PCL);
						pcl.LoadBinaryLXCN((byte[])base.GetColumnValue(context, this.folderTable.VersionHistory));
						pcl.Add(nextChangeNumber);
						base.SetColumn(context, this.folderTable.VersionHistory, pcl.DumpBinaryLXCN());
					}
					flag2 = this.DataRow.ColumnDirty(this.folderTable.DisplayName);
					this.trackFolderChange = false;
				}
				if (this.DataRow.ColumnDirty(this.folderTable.LcnCurrent))
				{
					IdSet folderCnsetIn = base.Mailbox.GetFolderCnsetIn(context);
					ExchangeId lcnCurrent = this.GetLcnCurrent(context);
					if (folderCnsetIn.Insert(lcnCurrent))
					{
						base.Mailbox.SetFolderCnsetIn(context, folderCnsetIn);
					}
				}
				this.SetLocalCommitTimeMax(context, utcNow);
				if (this.DataRow.ColumnDirty(this.folderTable.AclTableAndSecurityDescriptor))
				{
					extendedEventFlags |= ExtendedEventFlags.FolderPermissionChanged;
				}
				if (!flag)
				{
					FolderHierarchy.OnFolderChanged(context, this);
				}
				if (base.Mailbox.SharedState.UnifiedState != null)
				{
					FolderHierarchy.GetFolderHierarchy(context, base.Mailbox, ExchangeShortId.Zero, FolderInformationType.Basic);
				}
				base.Flush(context);
				if (flag2)
				{
					this.InvalidateIndexesForFolderNameChange(context);
				}
			}
			if (needsToPublishNotification)
			{
				ObjectNotificationEvent objectNotificationEvent = null;
				Folder parentFolder = this.GetParentFolder(context);
				if (flag)
				{
					if (parentFolder != null)
					{
						parentFolder.SetFolderCount(context, parentFolder.GetFolderCount(context) + 1L);
						IList<ExchangeId> searchBacklinks = this.GetSearchBacklinks(context, true);
						using (context.GrantPartitionFullAccess())
						{
							foreach (ExchangeId id in searchBacklinks)
							{
								SearchFolder searchFolder = (SearchFolder)Folder.OpenFolder(context, base.Mailbox, id);
								if (searchFolder != null)
								{
									searchFolder.InvalidateCachedQueryScope(context);
								}
							}
						}
					}
					ExchangeId id2 = this.GetId(context);
					IdSet folderIdsetIn = base.Mailbox.GetFolderIdsetIn(context);
					if (folderIdsetIn.Insert(id2))
					{
						base.Mailbox.SetFolderIdsetIn(context, folderIdsetIn);
					}
					FolderHierarchy.OnFolderCreated(context, this);
					LogicalIndex.AddLogicalIndexMaintenanceBreadcrumb(context, base.Mailbox.MailboxPartitionNumber, LogicalIndex.LogicalOperation.FolderCreated, new object[]
					{
						this.GetId(context).To26ByteArray(),
						this.GetParentFolderId(context).To26ByteArray()
					});
					if (this.GetId(context) != base.Mailbox.ConversationFolderId)
					{
						if (this.originalParentFolderId.IsValid && this.originalFolderId.IsValid)
						{
							objectNotificationEvent = NotificationEvents.CreateFolderCopiedEvent(context, this, this.originalFolderId, this.originalParentFolderId);
						}
						else
						{
							objectNotificationEvent = NotificationEvents.CreateFolderCreatedEvent(context, this);
						}
					}
				}
				else
				{
					if (this.originalParentFolderId.IsValid && this.originalParentFolderId != this.GetParentFolderId(context))
					{
						if (parentFolder != null)
						{
							parentFolder.SetFolderCount(context, parentFolder.GetFolderCount(context) + 1L);
						}
						Folder originalParentFolder = this.GetOriginalParentFolder(context);
						if (originalParentFolder != null)
						{
							originalParentFolder.SetFolderCount(context, originalParentFolder.GetFolderCount(context) - 1L);
						}
						FolderHierarchy.OnFolderMoved(context, this, this.originalParentFolderId.ToExchangeShortId());
						LogicalIndex.AddLogicalIndexMaintenanceBreadcrumb(context, base.Mailbox.MailboxPartitionNumber, LogicalIndex.LogicalOperation.FolderMoved, new object[]
						{
							this.GetId(context).To26ByteArray(),
							this.originalParentFolderId.To26ByteArray(),
							this.GetParentFolderId(context).To26ByteArray()
						});
					}
					if (this.GetId(context) != base.Mailbox.ConversationFolderId)
					{
						if (this.originalParentFolderId.IsValid)
						{
							if (this.originalParentFolderId == this.GetParentFolderId(context))
							{
								objectNotificationEvent = NotificationEvents.CreateFolderModifiedEvent(context, this, EventFlags.ModifiedByMove, extendedEventFlags, this.totalNormalMessageCountForEvent, this.unreadNormalMessageCountForEvent);
							}
							else
							{
								objectNotificationEvent = NotificationEvents.CreateFolderMovedEvent(context, this, this.originalParentFolderId);
							}
						}
						else
						{
							objectNotificationEvent = NotificationEvents.CreateFolderModifiedEvent(context, this, EventFlags.ContentOnly, extendedEventFlags, this.totalNormalMessageCountForEvent, this.unreadNormalMessageCountForEvent);
						}
					}
					this.totalNormalMessageCountForEvent = -1;
					this.unreadNormalMessageCountForEvent = -1;
				}
				this.originalFolderId = ExchangeId.Null;
				this.originalParentFolderId = ExchangeId.Null;
				if (objectNotificationEvent != null)
				{
					context.RiseNotificationEvent(objectNotificationEvent);
				}
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00040520 File Offset: 0x0003E720
		internal override void AutoSave(Context context)
		{
			this.Save(context);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0004052C File Offset: 0x0003E72C
		public void SetUnsent(Context context, TopMessage topMessage, bool unsent)
		{
			bool flag;
			if (unsent)
			{
				flag = topMessage.AdjustUncomputedMessageFlags(context, MessageFlags.Unsent, MessageFlags.None);
			}
			else
			{
				flag = topMessage.AdjustUncomputedMessageFlags(context, MessageFlags.None, MessageFlags.Unsent);
			}
			if (flag)
			{
				topMessage.SaveChanges(context, SaveMessageChangesFlags.SkipQuotaCheck);
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00040560 File Offset: 0x0003E760
		public void SetMessageStatus(Context context, ExchangeId messageId, int flagsToSet, int flagsToClear, out int originalStatus)
		{
			using (TopMessage topMessage = TopMessage.OpenMessage(context, base.Mailbox, this.GetId(context), messageId))
			{
				if (topMessage == null)
				{
					throw new StoreException((LID)58496U, ErrorCodeValue.NotFound);
				}
				this.SetMessageStatus(context, topMessage, flagsToSet, flagsToClear, out originalStatus);
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000405C4 File Offset: 0x0003E7C4
		public void SetMessageStatus(Context context, TopMessage msg, int flagsToSet, int flagsToClear, out int originalStatus)
		{
			originalStatus = (int)msg.GetPropertyValue(context, PropTag.Message.MsgStatus);
			int num = (originalStatus & ~flagsToClear) | flagsToSet;
			if (num == originalStatus)
			{
				return;
			}
			msg.SetProperty(context, PropTag.Message.MsgStatus, num);
			msg.SaveChanges(context, SaveMessageChangesFlags.SkipQuotaCheck);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00040613 File Offset: 0x0003E813
		public void SetSpecialFolderNumber(Context context, SpecialFolders specialFolderNumber)
		{
			base.SetColumn(context, this.folderTable.SpecialFolderNumber, (short)specialFolderNumber);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00040630 File Offset: 0x0003E830
		public Folder GetParentFolder(Context context)
		{
			ExchangeId parentFolderId = this.GetParentFolderId(context);
			if (parentFolderId.IsNullOrZero)
			{
				return null;
			}
			Folder folder = Folder.OpenFolder(context, base.Mailbox, parentFolderId);
			if (folder == null)
			{
				throw new StoreException((LID)57336U, ErrorCodeValue.ObjectDeleted);
			}
			return folder;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00040678 File Offset: 0x0003E878
		public Folder GetOriginalParentFolder(Context context)
		{
			Folder result = null;
			if (this.originalParentFolderId.IsValid)
			{
				result = Folder.OpenFolder(context, base.Mailbox, this.originalParentFolderId);
			}
			return result;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000406A8 File Offset: 0x0003E8A8
		public ExchangeId GetParentFolderId(Context context)
		{
			byte[] bytes = (byte[])base.GetColumnValue(context, this.folderTable.ParentFolderId);
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, bytes);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000406DF File Offset: 0x0003E8DF
		public SpecialFolders GetSpecialFolderNumber(Context context)
		{
			return (SpecialFolders)base.GetColumnValue(context, this.folderTable.SpecialFolderNumber);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000406F8 File Offset: 0x0003E8F8
		public long GetMessageCount(Context context)
		{
			return (long)base.GetColumnValue(context, this.folderTable.MessageCount);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00040711 File Offset: 0x0003E911
		public long GetHiddenItemCount(Context context)
		{
			return (long)base.GetColumnValue(context, this.folderTable.HiddenItemCount);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0004072C File Offset: 0x0003E92C
		public long GetUnreadMessageCount(Context context, long baseColumnValue)
		{
			if (this.IsPerUserReadUnreadTrackingEnabled && context.UserIdentity != Guid.Empty)
			{
				ExchangeId id = this.GetId(context);
				PerUser perUser = PerUser.LoadResident(context, base.Mailbox, context.UserIdentity, id);
				ulong num = 0UL;
				if (perUser != null)
				{
					IdSet cnsetIn = this.GetCnsetIn(context);
					using (LockManager.Lock(perUser, LockManager.LockType.PerUserShared, context.Diagnostics))
					{
						num = IdSet.Intersect(perUser.CNSet, cnsetIn).CountIds;
					}
				}
				return this.GetMessageCount(context) - (long)num;
			}
			return baseColumnValue;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000407CC File Offset: 0x0003E9CC
		public long GetConversationCount(Context context)
		{
			return (long)base.GetColumnValue(context, this.folderTable.ConversationCount);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000407E5 File Offset: 0x0003E9E5
		public long GetFolderCount(Context context)
		{
			return (long)base.GetColumnValue(context, this.folderTable.FolderCount);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00040800 File Offset: 0x0003EA00
		public string GetContainerClass(Context context)
		{
			string text = (string)base.GetColumnValue(context, this.folderTable.ContainerClass);
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0004082F File Offset: 0x0003EA2F
		public void SetContainerClass(Context context, string value)
		{
			base.SetColumn(context, this.folderTable.ContainerClass, value);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00040844 File Offset: 0x0003EA44
		public string GetComment(Context context)
		{
			string text = (string)base.GetColumnValue(context, this.folderTable.Comment);
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00040873 File Offset: 0x0003EA73
		public void SetComment(Context context, string value)
		{
			base.SetColumn(context, this.folderTable.Comment, value);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00040888 File Offset: 0x0003EA88
		public string GetName(Context context)
		{
			return (string)base.GetColumnValue(context, this.folderTable.DisplayName);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000408A4 File Offset: 0x0003EAA4
		public static string GetName(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			Folder folder = folderId.IsValid ? Folder.OpenFolder(context, mailbox, folderId) : null;
			if (folder == null)
			{
				return null;
			}
			return folder.GetName(context);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000408D2 File Offset: 0x0003EAD2
		public void SetName(Context context, string value)
		{
			base.SetColumn(context, this.folderTable.DisplayName, value);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000408E7 File Offset: 0x0003EAE7
		public DateTime GetCreationTime(Context context)
		{
			return (DateTime)this.GetPropertyValue(context, PropTag.Folder.CreationTime);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000408FA File Offset: 0x0003EAFA
		public void SetCreationTime(Context context, DateTime value)
		{
			base.SetColumn(context, this.folderTable.CreationTime, value);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00040914 File Offset: 0x0003EB14
		public DateTime GetLastModificationTime(Context context)
		{
			return (DateTime)this.GetPropertyValue(context, PropTag.Folder.LastModificationTime);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00040927 File Offset: 0x0003EB27
		public int? GetDisplayType(Context context)
		{
			return (int?)base.GetColumnValue(context, this.folderTable.DisplayType);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00040940 File Offset: 0x0003EB40
		public void SetDisplayType(Context context, int? value)
		{
			base.SetColumn(context, this.folderTable.DisplayType, value);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0004095A File Offset: 0x0003EB5A
		public long GetMessageSize(Context context)
		{
			return (long)base.GetColumnValue(context, this.folderTable.MessageSize);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00040973 File Offset: 0x0003EB73
		public long GetHiddenItemSize(Context context)
		{
			return (long)base.GetColumnValue(context, this.folderTable.HiddenItemSize);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0004098C File Offset: 0x0003EB8C
		internal SecurityDescriptor GetSecurityDescriptor(Context context)
		{
			byte[] array = (byte[])base.GetColumnValue(context, this.folderTable.AclTableAndSecurityDescriptor);
			if (array == null)
			{
				return null;
			}
			FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = AclTableHelper.Parse(context, array);
			return aclTableAndSecurityDescriptorProperty.SecurityDescriptor;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000409C4 File Offset: 0x0003EBC4
		internal IdSet GetMidsetDeleted(Context context)
		{
			byte[] array = (byte[])base.GetColumnValue(context, this.folderTable.MidsetDeleted);
			IdSet idSet;
			if (array != null)
			{
				idSet = IdSet.Parse(context, array);
			}
			else
			{
				idSet = new IdSet();
			}
			if (AddMidsetDeletedDelta.IsReady(context, context.Database))
			{
				byte[] array2 = (byte[])base.GetColumnValue(context, this.folderTable.MidsetDeletedDelta);
				if (array2 != null)
				{
					IdSet other = IdSet.Parse(context, array2);
					idSet.Insert(other);
				}
			}
			return idSet;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00040A36 File Offset: 0x0003EC36
		internal void ResetMidsetDeleted(Context context)
		{
			this.SetMidsetDeleted(context, new IdSet(), true);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00040A48 File Offset: 0x0003EC48
		internal void AddToMidsetDeletedWithSanitize(Context context, IdSet idSet)
		{
			object obj = base.Mailbox.MailboxPartitionNumber;
			byte[] array = this.GetId(context).To26ByteArray();
			List<KeyRange> list = new List<KeyRange>(idSet.CountRanges * 2);
			bool[] array2 = new bool[2];
			array2[0] = true;
			foreach (bool obj2 in array2)
			{
				foreach (object obj3 in ((IEnumerable)idSet))
				{
					GuidGlobCountSet guidGlobCountSet = (GuidGlobCountSet)obj3;
					ushort replidFromGuid = base.Mailbox.ReplidGuidMap.GetReplidFromGuid(context, guidGlobCountSet.Guid);
					foreach (GlobCountRange globCountRange in guidGlobCountSet.GlobCountSet)
					{
						byte[] array4 = ExchangeIdHelpers.To26ByteArray(replidFromGuid, guidGlobCountSet.Guid, globCountRange.LowBound);
						byte[] array5 = ExchangeIdHelpers.To26ByteArray(replidFromGuid, guidGlobCountSet.Guid, globCountRange.HighBound);
						list.Add(new KeyRange(new StartStopKey(true, new object[]
						{
							obj,
							array,
							obj2,
							array4
						}), new StartStopKey(true, new object[]
						{
							obj,
							array,
							obj2,
							array5
						})));
					}
				}
			}
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
			TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, new Column[]
			{
				messageTable.MessageId
			}, null, null, null, 0, 0, list, false, false, true);
			using (Reader reader = tableOperator.ExecuteReader(true))
			{
				while (reader.Read())
				{
					ExchangeId id = ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, reader.GetBinary(messageTable.MessageId));
					idSet.Remove(id);
				}
			}
			IdSet idSet2 = IdSet.Union(this.GetMidsetDeleted(context), idSet);
			this.SetMidsetDeleted(context, idSet2, true);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00040CA8 File Offset: 0x0003EEA8
		internal void SetMidsetDeleted(Context context, IdSet idSet, bool skipValidation)
		{
			if (!skipValidation)
			{
				MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					base.Mailbox.MailboxPartitionNumber,
					this.GetId(context).To26ByteArray()
				});
				TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, new Column[]
				{
					messageTable.MessageId
				}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true);
				using (Reader reader = tableOperator.ExecuteReader(true))
				{
					while (reader.Read())
					{
						ExchangeId exchangeId = ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, reader.GetBinary(messageTable.MessageId));
						if (idSet.Contains(exchangeId))
						{
							if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.FolderTracer.TraceError<ExchangeId>(0L, "Attempt to set corrupted MidsetDeleted. Existing MID {0}", exchangeId);
							}
							throw new StoreException((LID)48800U, ErrorCodeValue.CorruptMidsetDeleted, "Attempt to set corrupted MidsetDeleted");
						}
					}
				}
			}
			byte[] value = idSet.Serialize();
			base.SetColumn(context, this.folderTable.MidsetDeleted, value);
			if (AddMidsetDeletedDelta.IsReady(context, context.Database))
			{
				base.SetColumn(context, this.folderTable.MidsetDeletedDelta, null);
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00040E10 File Offset: 0x0003F010
		internal void AddIdToMidsetDeleted(Context context, ExchangeId id)
		{
			if (!AddMidsetDeletedDelta.IsReady(context, context.Database))
			{
				IdSet midsetDeleted = this.GetMidsetDeleted(context);
				midsetDeleted.Insert(id);
				this.SetMidsetDeleted(context, midsetDeleted, true);
				return;
			}
			byte[] array = (byte[])base.GetColumnValue(context, this.folderTable.MidsetDeletedDelta);
			IdSet idSet;
			if (array != null)
			{
				idSet = IdSet.Parse(context, array);
			}
			else
			{
				idSet = new IdSet();
			}
			idSet.Insert(id);
			array = idSet.Serialize();
			if (idSet.CountIds > 100UL || (long)array.Length > 200L)
			{
				byte[] array2 = (byte[])base.GetColumnValue(context, this.folderTable.MidsetDeleted);
				IdSet idSet2;
				if (array2 != null)
				{
					idSet2 = IdSet.Parse(context, array2);
					idSet2.Insert(idSet);
				}
				else
				{
					idSet2 = idSet;
				}
				this.SetMidsetDeleted(context, idSet2, true);
				return;
			}
			base.SetColumn(context, this.folderTable.MidsetDeletedDelta, array);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00040EE8 File Offset: 0x0003F0E8
		internal IdSet GetCnsetSeen(Context context)
		{
			if (!this.IsSearchFolder(context))
			{
				byte[] array = (byte[])this.GetPropertyValue(context, PropTag.Folder.CnsetSeen);
				if (array != null)
				{
					return IdSet.Parse(context, array);
				}
			}
			return new IdSet();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00040F20 File Offset: 0x0003F120
		internal void SetCnsetSeen(Context context, IdSet cnSet)
		{
			cnSet.IdealPack();
			byte[] value = cnSet.Serialize();
			this.SetProperty(context, PropTag.Folder.CnsetSeen, value);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00040F48 File Offset: 0x0003F148
		internal IdSet GetCnsetIn(Context context)
		{
			byte[] array = (byte[])this.GetPropertyValue(context, PropTag.Folder.CnsetIn);
			if (array != null)
			{
				return IdSet.Parse(context, array);
			}
			return new IdSet();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00040F78 File Offset: 0x0003F178
		internal void SetCnsetIn(Context context, IdSet cnSet)
		{
			byte[] value = cnSet.Serialize();
			this.SetProperty(context, PropTag.Folder.CnsetIn, value);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00040F9C File Offset: 0x0003F19C
		internal IdSet BuildCnsetIn(Context context)
		{
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				base.Mailbox.MailboxPartitionNumber,
				this.GetId(context).To26ByteArray(),
				false
			});
			return this.BuildCnsetIn(context, startStopKey, startStopKey);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00040FF0 File Offset: 0x0003F1F0
		internal IdSet BuildCnsetIn(Context context, StartStopKey startKey, StartStopKey stopKey)
		{
			IdSet idSet = new IdSet();
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
			TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, new Column[]
			{
				messageTable.LcnCurrent
			}, null, null, 0, 0, new KeyRange(startKey, stopKey), false, true);
			using (Reader reader = tableOperator.ExecuteReader(true))
			{
				while (reader.Read())
				{
					byte[] binary = reader.GetBinary(messageTable.LcnCurrent);
					idSet.Insert(binary);
				}
			}
			return idSet;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00041094 File Offset: 0x0003F294
		internal long BuildUnreadCount(Context context)
		{
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				base.Mailbox.MailboxPartitionNumber,
				this.GetId(context).To26ByteArray(),
				false
			});
			return this.BuildUnreadCount(context, startStopKey, startStopKey);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000410E8 File Offset: 0x0003F2E8
		internal long BuildUnreadCount(Context context, StartStopKey startKey, StartStopKey stopKey)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
			SearchCriteria restriction = Factory.CreateSearchCriteriaCompare(messageTable.IsRead, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(false, messageTable.IsRead));
			long result;
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, new Column[]
			{
				messageTable.IsRead
			}, restriction, null, 0, 0, new KeyRange(startKey, stopKey), false, true))
			{
				using (CountOperator countOperator = Factory.CreateCountOperator(context.Culture, context, tableOperator, false))
				{
					result = (long)((int)countOperator.ExecuteScalar());
				}
			}
			return result;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000411B0 File Offset: 0x0003F3B0
		internal void ItemUpdated(Context context, TopMessage message)
		{
			SearchFolder searchFolder = this as SearchFolder;
			if ((searchFolder == null || !message.ParentFolder.IsPerUserReadUnreadTrackingEnabled || searchFolder.GetNullableSearchGuid(context) != null) && message.GetIsRead(context) != message.GetOriginalRead(context))
			{
				long num = message.GetOriginalRead(context) ? 1L : -1L;
				this.UpdateAggregateUnreadCount(context, message.GetIsHidden(context), num);
			}
			if (message.OriginalSize != message.CurrentSize)
			{
				long num = message.CurrentSize - message.OriginalSize;
				this.UpdateAggregateCountAndSize(context, message.GetIsHidden(context), 0, num);
			}
			if (message.OriginalAttachCount == 0 != (message.AttachCount == 0))
			{
				long num = (message.OriginalAttachCount == 0) ? 1L : -1L;
				this.UpdateAggregateHasAttachCount(context, message.GetIsHidden(context), num);
			}
			if (message.OriginalAttachCount != message.AttachCount)
			{
				long num = (long)(message.AttachCount - message.OriginalAttachCount);
				this.UpdateAggregateAttachCount(context, message.GetIsHidden(context), num);
			}
			ExchangeId lcnCurrent = message.GetLcnCurrent(context);
			ExchangeId lcnOriginal = message.GetLcnOriginal(context);
			if (!this.IsSearchFolder(context))
			{
				IdSet cnsetSeen = this.GetCnsetSeen(context);
				bool flag = cnsetSeen.Insert(lcnCurrent);
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(InTransitInfo.IsForPublicFolderMove(InTransitInfo.GetInTransitStatus(base.Mailbox.SharedState)) || base.Mailbox.GetPreservingMailboxSignature(context) || (base.Mailbox.GetCreatedByMove(context) && base.Mailbox.SharedState.UnifiedState != null) || lcnCurrent.Guid != base.Mailbox.GetLocalIdGuid(context) || (lcnCurrent == lcnOriginal && !flag) || this.cnsetRecentlyAllocated.Contains(lcnCurrent), "New seen CN must be recently allocated!");
				if (flag)
				{
					this.SetCnsetSeen(context, cnsetSeen);
				}
			}
			if (!message.GetIsHidden(context) && this.IsPerUserReadUnreadTrackingEnabled && lcnCurrent != lcnOriginal)
			{
				IdSet cnsetIn = this.GetCnsetIn(context);
				bool flag2 = cnsetIn.Remove(lcnOriginal);
				if (cnsetIn.Insert(lcnCurrent))
				{
					flag2 = true;
				}
				if (flag2)
				{
					this.SetCnsetIn(context, cnsetIn);
				}
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000413A8 File Offset: 0x0003F5A8
		internal void ItemDeleted(Context context, TopMessage message)
		{
			if (!message.GetIsHidden(context) && this.IsPerUserReadUnreadTrackingEnabled)
			{
				ExchangeId lcnOriginal = message.GetLcnOriginal(context);
				IdSet cnsetIn = this.GetCnsetIn(context);
				if (cnsetIn.Remove(lcnOriginal))
				{
					this.SetCnsetIn(context, cnsetIn);
				}
			}
			this.UpdateAggregateCountAndSize(context, message.GetIsHidden(context), -1, -message.OriginalSize);
			if (!message.GetOriginalRead(context))
			{
				this.UpdateAggregateUnreadCount(context, message.GetIsHidden(context), -1L);
			}
			if (message.OriginalAttachCount != 0)
			{
				this.UpdateAggregateHasAttachCount(context, message.GetIsHidden(context), -1L);
				this.UpdateAggregateAttachCount(context, message.GetIsHidden(context), (long)(-(long)message.OriginalAttachCount));
			}
			this.UpdateAggregateColumn(context, this.folderTable.TotalDeletedCount, 1L);
			if (!(this is SearchFolder) && this.HasLocalReplica(context))
			{
				this.AddIdToMidsetDeleted(context, message.OriginalMessageID);
			}
			if (!this.IsSearchFolder(context) && !message.DeleteChangeNumber.IsNullOrZero)
			{
				ExchangeId deleteChangeNumber = message.DeleteChangeNumber;
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(InTransitInfo.IsForPublicFolderMove(InTransitInfo.GetInTransitStatus(base.Mailbox.SharedState)) || base.Mailbox.GetPreservingMailboxSignature(context) || (base.Mailbox.GetCreatedByMove(context) && base.Mailbox.SharedState.UnifiedState != null) || deleteChangeNumber.Guid != base.Mailbox.GetLocalIdGuid(context) || this.cnsetRecentlyAllocated.Contains(deleteChangeNumber), "New seen CN must be recently allocated!");
				IdSet cnsetSeen = this.GetCnsetSeen(context);
				bool flag = cnsetSeen.Insert(deleteChangeNumber);
				if (flag)
				{
					this.SetCnsetSeen(context, cnsetSeen);
				}
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00041534 File Offset: 0x0003F734
		internal void ItemInserted(Context context, TopMessage message)
		{
			this.UpdateAggregateCountAndSize(context, message.GetIsHidden(context), 1, message.CurrentSize);
			if (!message.GetIsRead(context))
			{
				this.UpdateAggregateUnreadCount(context, message.GetIsHidden(context), 1L);
			}
			if (message.AttachCount != 0)
			{
				this.UpdateAggregateHasAttachCount(context, message.GetIsHidden(context), 1L);
				this.UpdateAggregateAttachCount(context, message.GetIsHidden(context), (long)message.AttachCount);
			}
			ExchangeId lcnCurrent = message.GetLcnCurrent(context);
			if (!this.IsSearchFolder(context))
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(InTransitInfo.IsForPublicFolderMove(InTransitInfo.GetInTransitStatus(base.Mailbox.SharedState)) || base.Mailbox.GetPreservingMailboxSignature(context) || (base.Mailbox.GetCreatedByMove(context) && base.Mailbox.SharedState.UnifiedState != null) || lcnCurrent.Guid != base.Mailbox.GetLocalIdGuid(context) || this.cnsetRecentlyAllocated.Contains(lcnCurrent), "New seen CN must be recently allocated!");
				IdSet cnsetSeen = this.GetCnsetSeen(context);
				bool flag = cnsetSeen.Insert(lcnCurrent);
				if (flag)
				{
					this.SetCnsetSeen(context, cnsetSeen);
				}
			}
			if (!message.GetIsHidden(context) && this.IsPerUserReadUnreadTrackingEnabled)
			{
				IdSet cnsetIn = this.GetCnsetIn(context);
				if (cnsetIn.Insert(lcnCurrent))
				{
					this.SetCnsetIn(context, cnsetIn);
				}
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0004166D File Offset: 0x0003F86D
		internal void SetConversationCount(Context context, long value)
		{
			if (value < 0L)
			{
				value = 0L;
			}
			base.SetColumn(context, this.folderTable.ConversationCount, value);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00041690 File Offset: 0x0003F890
		internal void SetFolderCount(Context context, long value)
		{
			base.SetColumn(context, this.folderTable.FolderCount, value);
			int? num = (int?)this.GetPropertyValue(context, PropTag.Folder.HierarchyChangeNumber);
			this.SetProperty(context, PropTag.Folder.HierarchyChangeNumber, num.GetValueOrDefault() + 1);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000416E2 File Offset: 0x0003F8E2
		public ExchangeId GetLcnCurrent(Context context)
		{
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, (byte[])base.GetColumnValue(context, this.folderTable.LcnCurrent));
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0004170C File Offset: 0x0003F90C
		public ExchangeId GetId(Context context)
		{
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, (byte[])base.GetColumnValue(context, this.folderTable.FolderId));
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00041790 File Offset: 0x0003F990
		public void PromoteProperties(Context context, bool hiddenItemView, SearchCriteria criteria)
		{
			if (criteria != null)
			{
				HashSet<ushort> promotedPropertyIds = this.GetPromotedUniquePropertyIdsForMessages(context, hiddenItemView);
				HashSet<ushort> defaultPromotedPropertyIds = base.Mailbox.GetDefaultPromotedMessagePropertyIds(context);
				List<StorePropTag> propertiesToPromote = null;
				criteria.EnumerateColumns(delegate(Column column, object state)
				{
					ExtendedPropertyColumn extendedPropertyColumn = column as ExtendedPropertyColumn;
					if (extendedPropertyColumn != null)
					{
						PropertyPromotionHelper.CollectPropertiesToPromote(context, extendedPropertyColumn.StorePropTag, promotedPropertyIds, defaultPromotedPropertyIds, ref propertiesToPromote);
					}
				}, null, true);
				if (propertiesToPromote != null)
				{
					this.BootstrapPropertyPromotion(context, hiddenItemView, propertiesToPromote, true);
				}
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0004181C File Offset: 0x0003FA1C
		public void PromoteProperties(Context context, bool hiddenItemView, IList<Column> columns)
		{
			if (columns != null && columns.Count != 0)
			{
				HashSet<ushort> promotedUniquePropertyIdsForMessages = this.GetPromotedUniquePropertyIdsForMessages(context, hiddenItemView);
				HashSet<ushort> defaultPromotedMessagePropertyIds = base.Mailbox.GetDefaultPromotedMessagePropertyIds(context);
				List<StorePropTag> list = null;
				for (int i = 0; i < columns.Count; i++)
				{
					ExtendedPropertyColumn extendedPropertyColumn = columns[i] as ExtendedPropertyColumn;
					if (extendedPropertyColumn != null)
					{
						PropertyPromotionHelper.CollectPropertiesToPromote(context, extendedPropertyColumn.StorePropTag, promotedUniquePropertyIdsForMessages, defaultPromotedMessagePropertyIds, ref list);
					}
				}
				IList<StorePropTag> list2 = list;
				if (list2 == null && Folder.PropertyPromotionState.PropertyPromotionShouldRestart(context, this, hiddenItemView))
				{
					list2 = Array<StorePropTag>.Empty;
				}
				if (list2 != null)
				{
					this.BootstrapPropertyPromotion(context, hiddenItemView, list2, true);
				}
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000418B0 File Offset: 0x0003FAB0
		public void PromoteProperties(Context context, bool hiddenItemView, IList<StorePropTag> properties, bool promotePropertiesForMessages)
		{
			if (properties != null && properties.Count != 0)
			{
				HashSet<ushort> promotedUniquePropertyIdsForMessages = this.GetPromotedUniquePropertyIdsForMessages(context, hiddenItemView);
				HashSet<ushort> defaultPromotedMessagePropertyIds = base.Mailbox.GetDefaultPromotedMessagePropertyIds(context);
				List<StorePropTag> list = null;
				for (int i = 0; i < properties.Count; i++)
				{
					PropertyPromotionHelper.CollectPropertiesToPromote(context, properties[i], promotedUniquePropertyIdsForMessages, defaultPromotedMessagePropertyIds, ref list);
				}
				if (list != null)
				{
					this.BootstrapPropertyPromotion(context, hiddenItemView, list, promotePropertiesForMessages);
				}
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00041938 File Offset: 0x0003FB38
		protected void BootstrapPropertyPromotion(Context context, bool hiddenItemView, IList<StorePropTag> newPropertiesToPromote, bool promotePropertiesForMessages)
		{
			if (base.Mailbox.SharedState.IsMailboxLockedExclusively())
			{
				if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Synchronously starting promotion for folder {0}", this.GetId(context));
				}
				this.DoPromoteProperties(context, hiddenItemView, newPropertiesToPromote, promotePropertiesForMessages);
				return;
			}
			if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Launching promotion bootstrap task for folder {0}", this.GetId(context));
			}
			ExchangeId captureFolderId = this.GetId(context);
			bool captureHiddenItemView = hiddenItemView;
			IList<StorePropTag> captureNewPropertiesToPromote = newPropertiesToPromote;
			bool capturePromotePropertiesForMessages = promotePropertiesForMessages;
			MailboxTaskQueue.LaunchMailboxTask<MailboxTaskContext>(context, MailboxTaskQueue.Priority.High, TaskTypeId.PropertyPromotionBootstrap, base.Mailbox.SharedState, context.SecurityContext.UserSid, context.ClientType, context.Culture, (MailboxTaskContext mailboxTaskContext, Func<bool> shouldTaskContinue) => Folder.BootstrapPromotionTask(mailboxTaskContext, captureFolderId, captureHiddenItemView, captureNewPropertiesToPromote, capturePromotePropertiesForMessages));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00041A30 File Offset: 0x0003FC30
		protected void DoPromoteProperties(Context context, bool hiddenItemView, IList<StorePropTag> newPropertiesToPromote, bool promotePropertiesForMessages)
		{
			long num = hiddenItemView ? this.GetHiddenItemCount(context) : this.GetMessageCount(context);
			if (promotePropertiesForMessages && 0L != num)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PropertyPromotion, new object[]
				{
					Folder.StorePropTagListToString(newPropertiesToPromote, 1000),
					base.Mailbox.MailboxGuid,
					this.GetName(context) + (hiddenItemView ? " <hidden>" : " <normal>"),
					context.ClientType
				});
				if (context.PerfInstance != null)
				{
					context.PerfInstance.PropertyPromotionRate.Increment();
				}
				StorePerClientTypePerformanceCountersInstance perClientPerfInstance = context.Diagnostics.PerClientPerfInstance;
				if (perClientPerfInstance != null)
				{
					perClientPerfInstance.PropertyPromotionRate.Increment();
				}
			}
			HashSet<StorePropTag> promotedPropertiesForMessages = this.GetPromotedPropertiesForMessages(context, hiddenItemView);
			foreach (StorePropTag item in newPropertiesToPromote)
			{
				if (item.IsMultiValueInstance)
				{
					promotedPropertiesForMessages.Add(item.ChangeType(item.PropType & (PropertyType)57343));
				}
				else
				{
					promotedPropertiesForMessages.Add(item);
				}
			}
			this.PutPromotedProperties(context, promotedPropertiesForMessages, hiddenItemView);
			if (promotePropertiesForMessages && 0L != num)
			{
				if (num < (long)ConfigurationSchema.PromotionChunkSize.Value)
				{
					if (Folder.PropertyPromotionState.StartActivePromotion())
					{
						try
						{
							if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Doing message promotion for folder {0} synchronously", this.GetId(context));
							}
							Folder.DoPromotePropertiesSynchronously(context, this, hiddenItemView);
							return;
						}
						finally
						{
							Folder.PropertyPromotionState.EndActivePromotion();
						}
					}
					if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Skipping promotion for folder {0} - max promotions are currently active.", this.GetId(context));
					}
					Folder.PropertyPromotionState.MarkPromotionStarted(context, this, hiddenItemView);
					return;
				}
				if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Promotion for folder {0} must be done asynchronously", this.GetId(context));
				}
				Folder.PropertyPromotionState.MarkPromotionStarted(context, this, hiddenItemView);
				if (Folder.PropertyPromotionState.CanStartActivePromotion())
				{
					if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Starting promotion taks for folder {0}", this.GetId(context));
					}
					DateTime promotionTimeStamp = base.Mailbox.UtcNow;
					Folder.PropertyPromotionState.MarkPromotionInProgress(context, this, hiddenItemView, promotionTimeStamp);
					ExchangeId folderId = this.GetId(context);
					bool promoteHidden = hiddenItemView;
					MailboxTaskQueue.LaunchMailboxTask<MailboxTaskContext>(context, MailboxTaskQueue.Priority.Low, TaskTypeId.PropertyPromotion, base.Mailbox.SharedState, context.SecurityContext.UserSid, context.ClientType, context.Culture, (MailboxTaskContext mailboxTaskContext, Func<bool> shouldTaskContinue) => Folder.DoPromotePropertiesTask(mailboxTaskContext, folderId, promoteHidden, promotionTimeStamp, shouldTaskContinue));
					return;
				}
				if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Skipping promotion for folder {0} - max promotions are currently active.", this.GetId(context));
				}
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00041D04 File Offset: 0x0003FF04
		public bool IsDumpsterMarkedFolder(Context context)
		{
			return PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Folder.FolderAdminFlags, 64, 64);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00041D18 File Offset: 0x0003FF18
		public void SetDumpsterMarkedFolder(Context context, bool dumpster)
		{
			if (this.IsDumpsterMarkedFolder(context) != dumpster)
			{
				base.Mailbox.UpdateMessagesAggregateCountAndSize(context, false, !dumpster, (int)(-(int)this.GetMessageCount(context)), -this.GetMessageSize(context));
				base.Mailbox.UpdateMessagesAggregateCountAndSize(context, true, !dumpster, (int)(-(int)this.GetHiddenItemCount(context)), -this.GetHiddenItemSize(context));
				PropertyBagHelpers.SetPropertyFlags(context, this, PropTag.Folder.FolderAdminFlags, dumpster, 64);
				base.Mailbox.UpdateMessagesAggregateCountAndSize(context, false, dumpster, (int)this.GetMessageCount(context), this.GetMessageSize(context));
				base.Mailbox.UpdateMessagesAggregateCountAndSize(context, true, dumpster, (int)this.GetHiddenItemCount(context), this.GetHiddenItemSize(context));
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00041DC5 File Offset: 0x0003FFC5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Folder>(this);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00041DD0 File Offset: 0x0003FFD0
		private long UpdateAggregateColumn(Context context, PhysicalColumn column, long change)
		{
			long num = (long)base.GetColumnValue(context, column);
			long num2 = num + change;
			if (num2 < 0L)
			{
				num2 = 0L;
			}
			base.SetColumn(context, column, num2);
			return num2;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00041E08 File Offset: 0x00040008
		private void UpdateAggregateUnreadCount(Context context, bool hidden, long countChange)
		{
			if (Folder.updateAggregateUnreadCountTestHook.Value != null)
			{
				Folder.updateAggregateUnreadCountTestHook.Value(this, countChange);
			}
			long num;
			if (this.IsPerUserReadUnreadTrackingEnabled)
			{
				if (!hidden)
				{
					num = this.GetUnreadMessageCount(context, 0L);
				}
				else
				{
					num = 0L;
				}
			}
			else
			{
				PhysicalColumn unreadCount = this.GetAggregateColumns(hidden).UnreadCount;
				num = this.UpdateAggregateColumn(context, unreadCount, countChange);
			}
			if (!hidden)
			{
				this.unreadNormalMessageCountForEvent = (int)num;
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00041E73 File Offset: 0x00040073
		internal void UpdateAggregateCountAndSize_ForTest(Context context, bool hidden, int countChange, long sizeChange)
		{
			this.UpdateAggregateCountAndSize(context, hidden, countChange, sizeChange);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00041E80 File Offset: 0x00040080
		private void UpdateAggregateCountAndSize(Context context, bool hidden, int countChange, long sizeChange)
		{
			long num = this.UpdateAggregateColumn(context, this.GetAggregateColumns(hidden).Count, (long)countChange);
			if (!hidden && countChange != 0)
			{
				this.totalNormalMessageCountForEvent = (int)num;
			}
			this.UpdateAggregateColumn(context, this.GetAggregateColumns(hidden).Size, sizeChange);
			if (this is SearchFolder)
			{
				return;
			}
			if (this.GetId(context) == ConversationItem.GetConversationFolderId(context, base.Mailbox))
			{
				return;
			}
			base.Mailbox.UpdateMessagesAggregateCountAndSize(context, hidden, this.IsDumpsterMarkedFolder(context), countChange, sizeChange);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00041F08 File Offset: 0x00040108
		public void InitializeSearchFolderAggregateCounts(Context context)
		{
			base.SetColumn(context, this.GetAggregateColumns(false).Count, 0L);
			base.SetColumn(context, this.GetAggregateColumns(false).UnreadCount, 0L);
			base.SetColumn(context, this.GetAggregateColumns(false).Size, 0L);
			base.SetColumn(context, this.GetAggregateColumns(true).Count, 0L);
			base.SetColumn(context, this.GetAggregateColumns(true).UnreadCount, 0L);
			base.SetColumn(context, this.GetAggregateColumns(true).Size, 0L);
			this.SetProperty(context, PropTag.Folder.SearchFolderMsgCount, 0);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00041FD8 File Offset: 0x000401D8
		public void UpdateSearchFolderAggregateCountsForLinkedMessages(Context context, bool isHidden, int numMessagesLinked, int numUnreadMessagesLinked, long sizeMessagesLinked)
		{
			if (numMessagesLinked > 0)
			{
				long num = this.UpdateAggregateColumn(context, this.GetAggregateColumns(isHidden).Count, (long)numMessagesLinked);
				if (!isHidden)
				{
					this.totalNormalMessageCountForEvent = (int)num;
				}
				this.UpdateAggregateColumn(context, this.GetAggregateColumns(isHidden).Size, sizeMessagesLinked);
				if (numUnreadMessagesLinked > 0)
				{
					if (Folder.updateAggregateUnreadCountTestHook.Value != null)
					{
						Folder.updateAggregateUnreadCountTestHook.Value(this, (long)numUnreadMessagesLinked);
					}
					num = this.UpdateAggregateColumn(context, this.GetAggregateColumns(isHidden).UnreadCount, (long)numUnreadMessagesLinked);
					if (!isHidden)
					{
						this.unreadNormalMessageCountForEvent = (int)num;
					}
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00042072 File Offset: 0x00040272
		private Folder.AggregateColumns GetAggregateColumns(bool hidden)
		{
			return this.aggregates[hidden ? 1 : 0];
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0004208C File Offset: 0x0004028C
		private void UpdateAggregateAttachCount(Context context, bool hidden, long countChange)
		{
			PhysicalColumn attachCount = this.GetAggregateColumns(hidden).AttachCount;
			this.UpdateAggregateColumn(context, attachCount, countChange);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000420B4 File Offset: 0x000402B4
		private void UpdateAggregateHasAttachCount(Context context, bool hidden, long countChange)
		{
			PhysicalColumn hasAttachCount = this.GetAggregateColumns(hidden).HasAttachCount;
			this.UpdateAggregateColumn(context, hasAttachCount, countChange);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000420DB File Offset: 0x000402DB
		protected override ObjectType GetObjectType()
		{
			return ObjectType.Folder;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000420E0 File Offset: 0x000402E0
		internal bool RequiresPerFolderDocumentId(Context context)
		{
			string containerClass = this.GetContainerClass(context);
			return MessageClassHelper.MatchingMessageClass(containerClass, "IPF.Appointment") || MessageClassHelper.MatchingMessageClass(containerClass, "IPF.Contact");
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00042110 File Offset: 0x00040310
		internal int GetNextMessageDocumentId(Context context)
		{
			int? num = (int?)base.GetColumnValue(context, this.folderTable.ReservedDocumentIdCurrent);
			int? num2 = (int?)base.GetColumnValue(context, this.folderTable.ReservedDocumentIdMax);
			if (num == null || num2 == null || num.Value >= num2.Value)
			{
				int num3 = 100;
				if (this.RequiresPerFolderDocumentId(context))
				{
					num3 = 1000;
				}
				num = new int?(base.Mailbox.ReserveMessageDocumentIdRange(context, num3));
				base.SetColumn(context, this.folderTable.ReservedDocumentIdMax, num.Value + num3);
			}
			base.SetColumn(context, this.folderTable.ReservedDocumentIdCurrent, num.Value + 1);
			return num.Value;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000421DC File Offset: 0x000403DC
		internal int GetNextArticleNumber(Context context)
		{
			int num = (int)base.GetColumnValue(context, this.folderTable.NextArticleNumber);
			base.SetColumn(context, this.folderTable.NextArticleNumber, num + 1);
			return num;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0004221C File Offset: 0x0004041C
		internal ExchangeId GetNextMessageId(Context context)
		{
			ulong itemNbr = this.AllocateMessageIdCounter(context);
			return ExchangeId.Create(context, base.Mailbox.ReplidGuidMap, base.Mailbox.GetLocalIdGuid(context), itemNbr);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00042250 File Offset: 0x00040450
		internal ExchangeId GetNextMessageCn(Context context)
		{
			ulong num = this.AllocateMessageCnCounter(context);
			if (!base.Mailbox.GetPreservingMailboxSignature(context))
			{
				this.cnsetRecentlyAllocated.Insert(base.Mailbox.GetLocalIdGuid(context), num);
			}
			return ExchangeId.Create(context, base.Mailbox.ReplidGuidMap, base.Mailbox.GetLocalIdGuid(context), num);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000422AA File Offset: 0x000404AA
		internal DateTime? GetPromotionTimeStampForTest(Context context, bool hiddenItems)
		{
			return Folder.PropertyPromotionState.GetPromotionTimeStampForTest(context, this, hiddenItems);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000422B4 File Offset: 0x000404B4
		private ulong AllocateMessageIdCounter(Context context)
		{
			return this.AllocateGlobalCounter(context, this.folderTable.ReservedMessageIdGlobCntCurrent, this.folderTable.ReservedMessageIdGlobCntMax, base.Mailbox.SharedState.GlobalIdLowWatermark, 1000U, false);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000422E9 File Offset: 0x000404E9
		private ulong AllocateMessageCnCounter(Context context)
		{
			return this.AllocateGlobalCounter(context, this.folderTable.ReservedMessageCnGlobCntCurrent, this.folderTable.ReservedMessageCnGlobCntMax, base.Mailbox.SharedState.GlobalCnLowWatermark, 1000U, true);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00042320 File Offset: 0x00040520
		private ulong AllocateGlobalCounter(Context context, PhysicalColumn currentColumn, PhysicalColumn maxColumn, ulong lowWaterMark, uint reservedRangeSize, bool forCns)
		{
			ulong num = (ulong)((long)base.GetColumnValue(context, currentColumn));
			ulong num2 = (ulong)((long)base.GetColumnValue(context, maxColumn));
			if (num2 <= lowWaterMark)
			{
				num = num2;
			}
			if (num == num2)
			{
				num = (forCns ? base.Mailbox.GetNextCnCounterAndReserveRange(context, reservedRangeSize) : base.Mailbox.GetNextIdCounterAndReserveRange(context, reservedRangeSize));
				num2 = num + (ulong)reservedRangeSize;
				base.SetColumn(context, maxColumn, (long)num2);
			}
			base.SetColumn(context, currentColumn, (long)(num + 1UL));
			return num;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0004239D File Offset: 0x0004059D
		protected override void OnPropertyChanged(StorePropTag propTag, long deltaSize)
		{
			if (!base.NoReplicateOperationInProgress && this.NeedBumpChangeNumber(propTag))
			{
				this.trackFolderChange = true;
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000423B7 File Offset: 0x000405B7
		protected override void OnColumnChanged(Column column, long deltaSize)
		{
			if (!base.NoReplicateOperationInProgress && this.NeedBumpChangeNumber(column))
			{
				this.trackFolderChange = true;
			}
			base.OnColumnChanged(column, deltaSize);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000423D9 File Offset: 0x000405D9
		protected virtual bool NeedBumpChangeNumber(StorePropTag propTag)
		{
			return !propTag.IsCategory(16);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000423EC File Offset: 0x000405EC
		protected virtual bool NeedBumpChangeNumber(Column column)
		{
			return !(column == this.folderTable.FolderCount) && !(column == this.folderTable.TotalDeletedCount) && !(column == this.folderTable.MessageCount) && !(column == this.folderTable.UnreadMessageCount) && !(column == this.folderTable.MessageSize) && !(column == this.folderTable.MessageAttachCount) && !(column == this.folderTable.MessageHasAttachCount) && !(column == this.folderTable.HiddenItemCount) && !(column == this.folderTable.UnreadHiddenItemCount) && !(column == this.folderTable.HiddenItemSize) && !(column == this.folderTable.HiddenItemHasAttachCount) && !(column == this.folderTable.HiddenItemAttachCount) && !(column == this.folderTable.ConversationCount) && !(column == this.folderTable.ReservedMessageIdGlobCntCurrent) && !(column == this.folderTable.ReservedMessageIdGlobCntMax) && !(column == this.folderTable.ReservedMessageCnGlobCntCurrent) && !(column == this.folderTable.ReservedMessageCnGlobCntMax) && !(column == this.folderTable.ReservedDocumentIdCurrent) && !(column == this.folderTable.ReservedDocumentIdMax) && !(column == this.folderTable.NextArticleNumber) && !(column == this.folderTable.MidsetDeleted) && !(column == this.folderTable.LastModificationTime) && !(column == this.folderTable.LastModifierSid) && !(column == this.folderTable.LcnCurrent) && !(column == this.folderTable.VersionHistory) && !(column == this.folderTable.LocalCommitTimeMax) && !(column == this.folderTable.HiddenItemPromotedColumns) && !(column == this.folderTable.NormalItemPromotedColumns) && !(column == this.folderTable.PromotionTimestamp) && !(column == this.folderTable.PromotionUseHistory) && !(column == this.folderTable.NonRecursiveSearchBacklinks) && !(column == this.folderTable.RecursiveSearchBacklinks);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000426A7 File Offset: 0x000408A7
		public override void DiscardPrivateCache(Context context)
		{
			this.totalNormalMessageCountForEvent = -1;
			this.unreadNormalMessageCountForEvent = -1;
			base.DiscardPrivateCache(context);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000426BE File Offset: 0x000408BE
		protected override void OnDirty(Context context)
		{
			if (!context.IsStateObjectRegistered(this))
			{
				context.RegisterStateObject(this);
			}
			base.OnDirty(context);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000426D8 File Offset: 0x000408D8
		internal QuotaInfo GetQuotaInfo(Context context)
		{
			if (base.Mailbox.IsPublicFolderMailbox)
			{
				UnlimitedBytes folderQuotaProperty = this.GetFolderQuotaProperty(context, 26401);
				UnlimitedBytes folderQuotaProperty2 = this.GetFolderQuotaProperty(context, 26491);
				return new QuotaInfo(folderQuotaProperty2, folderQuotaProperty, UnlimitedBytes.UnlimitedValue, UnlimitedBytes.UnlimitedValue, UnlimitedBytes.UnlimitedValue);
			}
			return QuotaInfo.Unlimited;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00042728 File Offset: 0x00040928
		internal void SetFolderQuotaProperty(Context context, StorePropTag propTag, UnlimitedBytes value)
		{
			int num = value.IsUnlimited ? -1 : ((int)value.KB);
			this.SetProperty(context, propTag, num);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00042758 File Offset: 0x00040958
		internal UnlimitedBytes GetFolderQuotaProperty(Context context, ushort propId)
		{
			StorePropTag storePropTag;
			object obj;
			if (this.TryGetProperty(context, propId, out storePropTag, out obj) && obj != null)
			{
				if ((int)obj == -1)
				{
					return UnlimitedBytes.UnlimitedValue;
				}
				return UnlimitedBytes.FromKB((long)((int)obj));
			}
			else
			{
				switch (propId)
				{
				case 26401:
					return base.Mailbox.MailboxInfo.OrgWidePublicFolderProhibitPostQuota;
				case 26402:
					return base.Mailbox.MailboxInfo.OrgWidePublicFolderMaxItemSize;
				default:
					if (propId == 26491)
					{
						return base.Mailbox.MailboxInfo.OrgWidePublicFolderWarningQuota;
					}
					return UnlimitedBytes.UnlimitedValue;
				}
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000427E8 File Offset: 0x000409E8
		public void OnBeforeCommit(Context context)
		{
			bool isDisposed = base.IsDisposed;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000427F1 File Offset: 0x000409F1
		public void OnCommit(Context context)
		{
			if (!base.IsDisposed && !this.cnsetRecentlyAllocated.IsEmpty)
			{
				this.cnsetRecentlyAllocated = new IdSet();
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00042813 File Offset: 0x00040A13
		public void OnAbort(Context context)
		{
			if (!base.IsDisposed)
			{
				this.DiscardPrivateCache(context);
				this.originalFolderId = ExchangeId.Null;
				this.originalParentFolderId = ExchangeId.Null;
				if (!this.cnsetRecentlyAllocated.IsEmpty)
				{
					this.cnsetRecentlyAllocated = new IdSet();
				}
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00042854 File Offset: 0x00040A54
		protected override IReadOnlyDictionary<Column, Column> GetColumnRenames(Context context)
		{
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(1);
			FolderTable folderTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.FolderTable(context.Database);
			dictionary[folderTable.VirtualUnreadMessageCount] = Factory.CreateFunctionColumn("PerUserUnreadMessageCount", typeof(long), PropertyTypeHelper.SizeFromPropType(PropertyType.Int64), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Int64), folderTable.Table, new Func<object[], object>(this.GetFolderUnreadCountColumnFunction), "ComputeGetUnreadMessageCount", new Column[]
			{
				folderTable.UnreadMessageCount
			});
			return dictionary;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000428CB File Offset: 0x00040ACB
		private object GetFolderUnreadCountColumnFunction(object[] columnValues)
		{
			return this.GetUnreadMessageCount(base.Mailbox.CurrentOperationContext, (long)columnValues[0]);
		}

		// Token: 0x04000384 RID: 900
		public const int LengthOfSerializedGuid = 16;

		// Token: 0x04000385 RID: 901
		public const int LengthOfStringizedGuid = 37;

		// Token: 0x04000386 RID: 902
		internal const int MaxToLog = 1000;

		// Token: 0x04000387 RID: 903
		internal const uint ReservedIdRangeSize = 1000U;

		// Token: 0x04000388 RID: 904
		internal const uint ReservedCnRangeSize = 1000U;

		// Token: 0x04000389 RID: 905
		internal const uint MaxMidsetDeletedDeltaIdCounts = 100U;

		// Token: 0x0400038A RID: 906
		internal const uint MaxMidsetDeletedDeltaBytes = 200U;

		// Token: 0x0400038B RID: 907
		private static int activePropertyPromotionsCount;

		// Token: 0x0400038C RID: 908
		internal static Hookable<Action<Folder.PromotionExecutionType>> PromotionTaskTestCallback = Hookable<Action<Folder.PromotionExecutionType>>.Create(false, null);

		// Token: 0x0400038D RID: 909
		private static Hookable<Action> promotionTaskContentionCallback = Hookable<Action>.Create(false, null);

		// Token: 0x0400038E RID: 910
		private static Hookable<Action<Folder, long>> updateAggregateUnreadCountTestHook = Hookable<Action<Folder, long>>.Create(false, null);

		// Token: 0x0400038F RID: 911
		private static int normalItemPromotedPropertiesDataSlot = -1;

		// Token: 0x04000390 RID: 912
		private static int hiddenItemPromotedPropertiesDataSlot = -1;

		// Token: 0x04000391 RID: 913
		private static int normalItemPromotedPropertyIdsCacheDataSlot = -1;

		// Token: 0x04000392 RID: 914
		private static int hiddenItemPromotedPropertyIdsCacheDataSlot = -1;

		// Token: 0x04000393 RID: 915
		private static int aclTableVersionCookieDataSlot = -1;

		// Token: 0x04000394 RID: 916
		private static int propertyPromotionStateSlot = -1;

		// Token: 0x04000395 RID: 917
		private static int isPerUserReadUnreadTrackingEnabledSlot = -1;

		// Token: 0x04000396 RID: 918
		private static int doNotDeleteReferencesDataSlot = -1;

		// Token: 0x04000397 RID: 919
		private Folder.AggregateColumns[] aggregates;

		// Token: 0x04000398 RID: 920
		private int totalNormalMessageCountForEvent = -1;

		// Token: 0x04000399 RID: 921
		private int unreadNormalMessageCountForEvent = -1;

		// Token: 0x0400039A RID: 922
		private bool trackFolderChange;

		// Token: 0x0400039B RID: 923
		private FolderTable folderTable;

		// Token: 0x0400039C RID: 924
		private ObjectPropertySchema propertySchema;

		// Token: 0x0400039D RID: 925
		private ExchangeId originalParentFolderId;

		// Token: 0x0400039E RID: 926
		private ExchangeId originalFolderId;

		// Token: 0x0400039F RID: 927
		private IdSet cnsetRecentlyAllocated = new IdSet();

		// Token: 0x02000047 RID: 71
		internal enum PromotionExecutionType
		{
			// Token: 0x040003A5 RID: 933
			None,
			// Token: 0x040003A6 RID: 934
			Skip,
			// Token: 0x040003A7 RID: 935
			Mid,
			// Token: 0x040003A8 RID: 936
			Last
		}

		// Token: 0x02000048 RID: 72
		private struct AggregateColumns
		{
			// Token: 0x06000735 RID: 1845 RVA: 0x0004294D File Offset: 0x00040B4D
			internal AggregateColumns(PhysicalColumn columnUnreadCount, PhysicalColumn columnCount, PhysicalColumn columnSize, PhysicalColumn columnHasAttachCount, PhysicalColumn columnAttachCount)
			{
				this.columnCount = columnCount;
				this.columnSize = columnSize;
				this.columnUnreadCount = columnUnreadCount;
				this.columnHasAttachCount = columnHasAttachCount;
				this.columnAttachCount = columnAttachCount;
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000736 RID: 1846 RVA: 0x00042974 File Offset: 0x00040B74
			public PhysicalColumn UnreadCount
			{
				get
				{
					return this.columnUnreadCount;
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000737 RID: 1847 RVA: 0x0004297C File Offset: 0x00040B7C
			public PhysicalColumn Count
			{
				get
				{
					return this.columnCount;
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000738 RID: 1848 RVA: 0x00042984 File Offset: 0x00040B84
			public PhysicalColumn Size
			{
				get
				{
					return this.columnSize;
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000739 RID: 1849 RVA: 0x0004298C File Offset: 0x00040B8C
			public PhysicalColumn HasAttachCount
			{
				get
				{
					return this.columnHasAttachCount;
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x0600073A RID: 1850 RVA: 0x00042994 File Offset: 0x00040B94
			public PhysicalColumn AttachCount
			{
				get
				{
					return this.columnAttachCount;
				}
			}

			// Token: 0x040003A9 RID: 937
			private PhysicalColumn columnUnreadCount;

			// Token: 0x040003AA RID: 938
			private PhysicalColumn columnCount;

			// Token: 0x040003AB RID: 939
			private PhysicalColumn columnSize;

			// Token: 0x040003AC RID: 940
			private PhysicalColumn columnHasAttachCount;

			// Token: 0x040003AD RID: 941
			private PhysicalColumn columnAttachCount;
		}

		// Token: 0x02000049 RID: 73
		private class PropertyPromotionFolderState
		{
			// Token: 0x17000197 RID: 407
			// (get) Token: 0x0600073B RID: 1851 RVA: 0x0004299C File Offset: 0x00040B9C
			// (set) Token: 0x0600073C RID: 1852 RVA: 0x000429A4 File Offset: 0x00040BA4
			internal DateTime? NormalItemPromotionTimeStamp
			{
				get
				{
					return this.normalItemPromotionTimeStamp;
				}
				set
				{
					this.normalItemPromotionTimeStamp = value;
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x0600073D RID: 1853 RVA: 0x000429AD File Offset: 0x00040BAD
			// (set) Token: 0x0600073E RID: 1854 RVA: 0x000429B5 File Offset: 0x00040BB5
			internal DateTime? HiddenItemPromotionTimeStamp
			{
				get
				{
					return this.hiddenItemPromotionTimeStamp;
				}
				set
				{
					this.hiddenItemPromotionTimeStamp = value;
				}
			}

			// Token: 0x040003AE RID: 942
			private DateTime? normalItemPromotionTimeStamp;

			// Token: 0x040003AF RID: 943
			private DateTime? hiddenItemPromotionTimeStamp;
		}

		// Token: 0x0200004A RID: 74
		private class PropertyPromotionState : Dictionary<ExchangeId, Folder.PropertyPromotionFolderState>, IComponentData
		{
			// Token: 0x06000740 RID: 1856 RVA: 0x000429C6 File Offset: 0x00040BC6
			internal static bool CanStartActivePromotion()
			{
				return Folder.activePropertyPromotionsCount < ConfigurationSchema.MaximumActivePropertyPromotions.Value;
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x000429DC File Offset: 0x00040BDC
			internal static bool StartActivePromotion()
			{
				int value = ConfigurationSchema.MaximumActivePropertyPromotions.Value;
				for (;;)
				{
					int activePropertyPromotionsCount = Folder.activePropertyPromotionsCount;
					if (activePropertyPromotionsCount >= value)
					{
						break;
					}
					int num = Interlocked.CompareExchange(ref Folder.activePropertyPromotionsCount, activePropertyPromotionsCount + 1, activePropertyPromotionsCount);
					if (num == activePropertyPromotionsCount)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x00042A14 File Offset: 0x00040C14
			internal static void EndActivePromotion()
			{
				Interlocked.Decrement(ref Folder.activePropertyPromotionsCount);
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x00042A21 File Offset: 0x00040C21
			internal static void MarkPromotionStarted(Context context, Folder folder, bool hiddenItems)
			{
				if (hiddenItems)
				{
					folder.SetProperty(context, PropTag.Folder.PropertyPromotionInProgressHiddenItems, true);
					return;
				}
				folder.SetProperty(context, PropTag.Folder.PropertyPromotionInProgressNormalItems, true);
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x00042A50 File Offset: 0x00040C50
			internal static bool IsAnyPromotionInProgress(Context context, Mailbox mailbox)
			{
				Folder.PropertyPromotionState propertyPromotionState = Folder.PropertyPromotionState.GetPropertyPromotionState(context, mailbox.SharedState, false);
				return propertyPromotionState != null && propertyPromotionState.Count != 0;
			}

			// Token: 0x06000745 RID: 1861 RVA: 0x00042A7C File Offset: 0x00040C7C
			internal static void MarkPromotionInProgress(Context context, Folder folder, bool hiddenItems, DateTime promotionTimeStamp)
			{
				Folder.PropertyPromotionState propertyPromotionState = Folder.PropertyPromotionState.GetPropertyPromotionState(context, folder.Mailbox.SharedState, true);
				ExchangeId id = folder.GetId(context);
				Folder.PropertyPromotionFolderState propertyPromotionFolderState;
				if (!propertyPromotionState.TryGetValue(id, out propertyPromotionFolderState))
				{
					propertyPromotionFolderState = new Folder.PropertyPromotionFolderState();
					propertyPromotionState[id] = propertyPromotionFolderState;
				}
				if (hiddenItems)
				{
					propertyPromotionFolderState.HiddenItemPromotionTimeStamp = new DateTime?(promotionTimeStamp);
					return;
				}
				propertyPromotionFolderState.NormalItemPromotionTimeStamp = new DateTime?(promotionTimeStamp);
			}

			// Token: 0x06000746 RID: 1862 RVA: 0x00042AD9 File Offset: 0x00040CD9
			internal static void MarkPromotionCompleted(Context context, Folder folder, bool hiddenItems)
			{
				if (hiddenItems)
				{
					folder.SetProperty(context, PropTag.Folder.PropertyPromotionInProgressHiddenItems, null);
					return;
				}
				folder.SetProperty(context, PropTag.Folder.PropertyPromotionInProgressNormalItems, null);
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x00042AFC File Offset: 0x00040CFC
			internal static void ResetPromotionInProgress(Context context, MailboxState mailboxState, ExchangeId folderId, bool hiddenItems, DateTime promotionTimeStamp)
			{
				Folder.PropertyPromotionState propertyPromotionState = Folder.PropertyPromotionState.GetPropertyPromotionState(context, mailboxState, false);
				if (propertyPromotionState == null)
				{
					return;
				}
				Folder.PropertyPromotionFolderState propertyPromotionFolderState;
				if (!propertyPromotionState.TryGetValue(folderId, out propertyPromotionFolderState))
				{
					return;
				}
				if (hiddenItems && propertyPromotionFolderState.HiddenItemPromotionTimeStamp != null && propertyPromotionFolderState.HiddenItemPromotionTimeStamp.Value == promotionTimeStamp)
				{
					propertyPromotionFolderState.HiddenItemPromotionTimeStamp = null;
				}
				else if (!hiddenItems && propertyPromotionFolderState.NormalItemPromotionTimeStamp != null && propertyPromotionFolderState.NormalItemPromotionTimeStamp.Value == promotionTimeStamp)
				{
					propertyPromotionFolderState.NormalItemPromotionTimeStamp = null;
				}
				if (propertyPromotionFolderState.HiddenItemPromotionTimeStamp == null && propertyPromotionFolderState.NormalItemPromotionTimeStamp == null)
				{
					propertyPromotionState.Remove(folderId);
				}
				if (propertyPromotionState.Count == 0)
				{
					mailboxState.SetComponentData(Folder.propertyPromotionStateSlot, null);
				}
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x00042BD8 File Offset: 0x00040DD8
			internal static bool PropertyPromotionShouldRestart(Context context, Folder folder, bool hiddenItems)
			{
				if (folder.GetPropertyValue(context, hiddenItems ? PropTag.Folder.PropertyPromotionInProgressHiddenItems : PropTag.Folder.PropertyPromotionInProgressNormalItems) == null)
				{
					return false;
				}
				Folder.PropertyPromotionState propertyPromotionState = Folder.PropertyPromotionState.GetPropertyPromotionState(context, folder.Mailbox.SharedState, false);
				Folder.PropertyPromotionFolderState propertyPromotionFolderState;
				return propertyPromotionState == null || !propertyPromotionState.TryGetValue(folder.GetId(context), out propertyPromotionFolderState) || (hiddenItems && propertyPromotionFolderState.HiddenItemPromotionTimeStamp == null) || (!hiddenItems && propertyPromotionFolderState.NormalItemPromotionTimeStamp == null);
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x00042C58 File Offset: 0x00040E58
			internal static bool HasPromotionChanged(Context context, MailboxState mailboxState, ExchangeId folderId, bool hiddenItems, DateTime promotionTimeStamp)
			{
				Folder.PropertyPromotionState propertyPromotionState = Folder.PropertyPromotionState.GetPropertyPromotionState(context, mailboxState, false);
				if (propertyPromotionState == null)
				{
					return true;
				}
				Folder.PropertyPromotionFolderState propertyPromotionFolderState;
				if (!propertyPromotionState.TryGetValue(folderId, out propertyPromotionFolderState))
				{
					return true;
				}
				if (hiddenItems && propertyPromotionFolderState.HiddenItemPromotionTimeStamp != null)
				{
					return promotionTimeStamp != propertyPromotionFolderState.HiddenItemPromotionTimeStamp.Value;
				}
				return hiddenItems || propertyPromotionFolderState.NormalItemPromotionTimeStamp == null || promotionTimeStamp != propertyPromotionFolderState.NormalItemPromotionTimeStamp.Value;
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x00042CD8 File Offset: 0x00040ED8
			internal static DateTime? GetPromotionTimeStampForTest(Context context, Folder folder, bool hiddenItems)
			{
				Folder.PropertyPromotionState propertyPromotionState = Folder.PropertyPromotionState.GetPropertyPromotionState(context, folder.Mailbox.SharedState, false);
				if (propertyPromotionState == null)
				{
					return null;
				}
				Folder.PropertyPromotionFolderState propertyPromotionFolderState;
				if (!propertyPromotionState.TryGetValue(folder.GetId(context), out propertyPromotionFolderState))
				{
					return null;
				}
				if (!hiddenItems)
				{
					return propertyPromotionFolderState.NormalItemPromotionTimeStamp;
				}
				return propertyPromotionFolderState.HiddenItemPromotionTimeStamp;
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x00042D30 File Offset: 0x00040F30
			private static Folder.PropertyPromotionState GetPropertyPromotionState(Context context, MailboxState mailboxState, bool allowCreation)
			{
				Folder.PropertyPromotionState propertyPromotionState = (Folder.PropertyPromotionState)mailboxState.GetComponentData(Folder.propertyPromotionStateSlot);
				if (propertyPromotionState == null && allowCreation)
				{
					propertyPromotionState = new Folder.PropertyPromotionState();
					mailboxState.SetComponentData(Folder.propertyPromotionStateSlot, propertyPromotionState);
				}
				return propertyPromotionState;
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x00042D67 File Offset: 0x00040F67
			bool IComponentData.DoCleanup(Context context)
			{
				return base.Count == 0;
			}
		}

		// Token: 0x0200004B RID: 75
		private class ACLTableVersionCookie : IComponentData
		{
			// Token: 0x0600074E RID: 1870 RVA: 0x00042D7A File Offset: 0x00040F7A
			bool IComponentData.DoCleanup(Context context)
			{
				return false;
			}
		}

		// Token: 0x0200004C RID: 76
		public class DoNotDeleteReferenceHolder : HashSet<object>, IComponentData
		{
			// Token: 0x06000750 RID: 1872 RVA: 0x00042D85 File Offset: 0x00040F85
			bool IComponentData.DoCleanup(Context context)
			{
				return base.Count == 0;
			}
		}
	}
}
