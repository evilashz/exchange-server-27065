using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200009B RID: 155
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContentsView : FolderBasedView
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x0002A220 File Offset: 0x00028420
		internal ContentsView(Logon logon, ReferenceCount<CoreFolder> coreFolderReference, TableFlags tableFlags, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandle) : base(logon, coreFolderReference, tableFlags, View.Capabilities.All, ViewType.MessageView, notificationHandler, returnNotificationHandle, ModernCalendarItemFilteringHelper.GetDefaultContentsViewFilter(coreFolderReference.ReferencedObject, logon))
		{
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0002A249 File Offset: 0x00028449
		protected override PropertyConverter PropertyConverter
		{
			get
			{
				return PropertyConverter.Message;
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0002A250 File Offset: 0x00028450
		internal override byte[] CreateBookmark()
		{
			return base.GetViewCache().CreateBookmark();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002A25D File Offset: 0x0002845D
		internal override void FreeBookmark(byte[] bookmark)
		{
			base.GetViewCache().FreeBookmark(bookmark);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002A26B File Offset: 0x0002846B
		internal override int SeekRowBookmark(byte[] bookmark, int rowCount, bool wantMoveCount, out bool soughtLess, out bool positionChanged)
		{
			return base.InternalSeekRowBookmark(bookmark, rowCount, wantMoveCount, out soughtLess, out positionChanged);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002A27C File Offset: 0x0002847C
		internal byte[] GetCollapseState(long rowId, uint rowInstanceNumber)
		{
			return base.GetViewCache().GetCollapseState(ServerIdConverter.MakeInstanceKey(this.ContainerFolderId.Value, rowId, (int)rowInstanceNumber));
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002A2AE File Offset: 0x000284AE
		internal byte[] SetCollapseState(byte[] collapseState)
		{
			return base.GetViewCache().SetCollapseState(collapseState);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002A2BC File Offset: 0x000284BC
		protected override IQueryResult CreateQueryResult(NativeStorePropertyDefinition[] propertyDefinitions)
		{
			NativeStorePropertyDefinition[] array = propertyDefinitions;
			SortBy[] array2 = base.SortBys;
			bool flag = false;
			PrivateLogon privateLogon = base.LogonObject as PrivateLogon;
			if (privateLogon != null)
			{
				flag = base.ContainerStoreObjectId.Equals(privateLogon.MailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions));
			}
			else
			{
				PublicLogon publicLogon = base.LogonObject as PublicLogon;
				if (publicLogon != null)
				{
					flag = PublicFolderCOWSession.IsDumpsterFolder(base.CoreFolder);
				}
			}
			if (flag && (byte)(base.TableFlags & (TableFlags.Associated | TableFlags.SoftDeletes)) == 0)
			{
				for (int i = 0; i < propertyDefinitions.Length; i++)
				{
					if (propertyDefinitions[i] == CoreObjectSchema.DeletedOnTime)
					{
						if (array == propertyDefinitions)
						{
							array = (NativeStorePropertyDefinition[])propertyDefinitions.Clone();
						}
						array[i] = (NativeStorePropertyDefinition)CoreObjectSchema.LastModifiedTime;
					}
				}
				if (base.SortBys != null)
				{
					for (int j = 0; j < array2.Length; j++)
					{
						if (array2[j].ColumnDefinition == CoreObjectSchema.DeletedOnTime)
						{
							if (array2 == base.SortBys)
							{
								array2 = (SortBy[])base.SortBys.Clone();
							}
							array2[j] = new SortBy(CoreObjectSchema.LastModifiedTime, base.SortBys[j].SortOrder);
						}
					}
				}
			}
			ItemQueryType itemQueryType = ItemQueryType.None;
			if ((byte)(base.TableFlags & TableFlags.Associated) == 2)
			{
				itemQueryType |= ItemQueryType.Associated;
			}
			if ((byte)(base.TableFlags & TableFlags.SoftDeletes) == 32)
			{
				itemQueryType |= ItemQueryType.SoftDeleted;
			}
			byte b = (byte)(base.TableFlags & TableFlags.DeferredErrors);
			if ((byte)(base.TableFlags & TableFlags.SuppressNotifications) == 128)
			{
				itemQueryType |= ItemQueryType.ConversationViewMembers;
			}
			if ((byte)(base.TableFlags & TableFlags.Depth) == 4)
			{
				itemQueryType |= ItemQueryType.ConversationView;
			}
			if ((byte)(base.TableFlags & TableFlags.RetrieveFromIndex) == 1)
			{
				itemQueryType |= ItemQueryType.RetrieveFromIndex;
			}
			if (base.GroupBys != null)
			{
				return base.CoreFolder.QueryExecutor.GroupedItemQuery(base.Filter, itemQueryType, base.GroupBys, base.ExpandedCount, array2, array);
			}
			return base.CoreFolder.QueryExecutor.ItemQuery(itemQueryType, base.Filter, array2, array);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0002A488 File Offset: 0x00028688
		protected override void CheckPropertiesAllowed(PropertyTag[] propertyTags)
		{
			foreach (PropertyTag propertyTag in propertyTags)
			{
				if (Array.IndexOf<PropertyTag>(ViewClientProperties.ContentsViewClientProperties.DisallowList, propertyTag) >= 0)
				{
					throw new RopExecutionException(string.Format("This client side property is not supported on ContentsView. Property = {0}.", propertyTag), (ErrorCode)2147746050U);
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0002A4DB File Offset: 0x000286DB
		protected override ClientSideProperties ClientSideProperties
		{
			get
			{
				return ClientSideProperties.ContentViewInstance;
			}
		}
	}
}
