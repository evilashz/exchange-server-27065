using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200009C RID: 156
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class HierarchyView : FolderBasedView
	{
		// Token: 0x0600064E RID: 1614 RVA: 0x0002A4E4 File Offset: 0x000286E4
		internal HierarchyView(Logon logon, ReferenceCount<CoreFolder> coreFolderReference, TableFlags tableFlags, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandle) : base(logon, coreFolderReference, tableFlags, View.Capabilities.CanRestrict, ViewType.FolderView, notificationHandler, returnNotificationHandle, null)
		{
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0002A501 File Offset: 0x00028701
		protected override PropertyConverter PropertyConverter
		{
			get
			{
				return PropertyConverter.HierarchyView;
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0002A508 File Offset: 0x00028708
		internal override byte[] CreateBookmark()
		{
			return base.GetViewCache().CreateBookmark();
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0002A515 File Offset: 0x00028715
		internal override void FreeBookmark(byte[] bookmark)
		{
			base.GetViewCache().FreeBookmark(bookmark);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0002A523 File Offset: 0x00028723
		internal override int SeekRowBookmark(byte[] bookmark, int rowCount, bool wantMoveCount, out bool soughtLess, out bool positionChanged)
		{
			return base.InternalSeekRowBookmark(bookmark, rowCount, wantMoveCount, out soughtLess, out positionChanged);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0002A534 File Offset: 0x00028734
		protected override IQueryResult CreateQueryResult(NativeStorePropertyDefinition[] propertyDefinitions)
		{
			FolderQueryFlags folderQueryFlags = FolderQueryFlags.None;
			if ((byte)(base.TableFlags & TableFlags.SoftDeletes) == 32)
			{
				folderQueryFlags |= FolderQueryFlags.SoftDeleted;
			}
			if ((byte)(base.TableFlags & TableFlags.Depth) == 4)
			{
				folderQueryFlags |= FolderQueryFlags.DeepTraversal;
			}
			if ((byte)(base.TableFlags & TableFlags.SuppressNotifications) == 128)
			{
				folderQueryFlags |= FolderQueryFlags.SuppressNotificationsOnMyActions;
			}
			byte b = (byte)(base.TableFlags & TableFlags.DeferredErrors);
			return base.CoreFolder.QueryExecutor.FolderQuery(folderQueryFlags, base.Filter, base.SortBys, propertyDefinitions);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0002A5A8 File Offset: 0x000287A8
		protected override void CheckPropertiesAllowed(PropertyTag[] propertyTags)
		{
			foreach (PropertyTag propertyTag in propertyTags)
			{
				if (Array.IndexOf<PropertyTag>(ViewClientProperties.HierarchyViewClientProperties.DisallowList, propertyTag) >= 0)
				{
					throw new RopExecutionException(string.Format("This client side property is not supported on HierarchyView. Property = {0}.", propertyTag), (ErrorCode)2147746050U);
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0002A5FB File Offset: 0x000287FB
		protected override ClientSideProperties ClientSideProperties
		{
			get
			{
				if ((byte)(base.TableFlags & TableFlags.Depth) != 0)
				{
					return ClientSideProperties.DeepHierarchyViewInstance;
				}
				return ClientSideProperties.HierarchyViewInstance;
			}
		}
	}
}
