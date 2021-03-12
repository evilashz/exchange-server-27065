using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200009A RID: 154
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class FolderBasedView : View
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x0002A0D2 File Offset: 0x000282D2
		protected FolderBasedView(Logon logon, ReferenceCount<CoreFolder> coreFolderReference, TableFlags tableFlags, View.Capabilities capabilities, ViewType viewType, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandle, QueryFilter defaultQueryFilter = null) : base(logon, tableFlags, capabilities, viewType, notificationHandler, returnNotificationHandle, defaultQueryFilter)
		{
			this.coreFolderReference = coreFolderReference;
			this.coreFolderReference.AddRef();
			this.coreFolderId = coreFolderReference.ReferencedObject.Id.ObjectId;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0002A10F File Offset: 0x0002830F
		protected CoreFolder CoreFolder
		{
			get
			{
				return this.coreFolderReference.ReferencedObject;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0002A11C File Offset: 0x0002831C
		protected override ICoreObject CoreObject
		{
			get
			{
				return this.coreFolderReference.ReferencedObject;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0002A12C File Offset: 0x0002832C
		protected override IViewDataSource CreateDataSource()
		{
			IViewDataSource result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IQueryResult queryResult = this.CreateQueryResult(this.ColumnPropertyDefinitions);
				disposeGuard.Add<IQueryResult>(queryResult);
				bool useUnicodeForRestrictions = (byte)(base.TableFlags & TableFlags.MapiUnicode) != 0;
				QueryResultViewDataSource queryResultViewDataSource = new QueryResultViewDataSource(this.coreFolderReference.ReferencedObject.Session, base.ServerColumns, queryResult, useUnicodeForRestrictions);
				disposeGuard.Success();
				result = queryResultViewDataSource;
			}
			return result;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0002A1B4 File Offset: 0x000283B4
		protected override NativeStorePropertyDefinition[] ColumnPropertyDefinitions
		{
			get
			{
				return base.GetColumnPropertyDefinitions(this.coreFolderReference.ReferencedObject.Session, this.coreFolderReference.ReferencedObject.PropertyBag);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0002A1DC File Offset: 0x000283DC
		protected StoreObjectId ContainerStoreObjectId
		{
			get
			{
				return this.coreFolderId;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0002A1E4 File Offset: 0x000283E4
		protected override StoreId? ContainerFolderId
		{
			get
			{
				return new StoreId?(new StoreId(base.LogonObject.Session.IdConverter.GetFidFromId(this.coreFolderId)));
			}
		}

		// Token: 0x06000642 RID: 1602
		protected abstract IQueryResult CreateQueryResult(NativeStorePropertyDefinition[] propertyDefinitions);

		// Token: 0x06000643 RID: 1603 RVA: 0x0002A20B File Offset: 0x0002840B
		protected override void InternalDispose()
		{
			base.InternalDispose();
			this.coreFolderReference.Release();
		}

		// Token: 0x0400029E RID: 670
		private readonly ReferenceCount<CoreFolder> coreFolderReference;

		// Token: 0x0400029F RID: 671
		private readonly StoreObjectId coreFolderId;
	}
}
