using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000083 RID: 131
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiContainer : MapiProp
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		internal MapiContainer(IExMapiContainer iMAPIContainer, object externalIMAPIContainer, MapiStore mapiStore, Guid[] interfaceIds) : base(iMAPIContainer, externalIMAPIContainer, mapiStore, interfaceIds)
		{
			this.iMAPIContainer = iMAPIContainer;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		protected override void MapiInternalDispose()
		{
			this.iMAPIContainer = null;
			base.MapiInternalDispose();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000EFD7 File Offset: 0x0000D1D7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiContainer>(this);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		internal static object Wrap(IExInterface iObj, int objType, MapiStore mapiStore)
		{
			switch (objType)
			{
			case 3:
				return new MapiFolder(iObj.ToInterface<IExMapiFolder>(), null, mapiStore);
			case 5:
				return new MapiMessage(iObj.ToInterface<IExMapiMessage>(), null, mapiStore);
			}
			throw MapiExceptionHelper.ArgumentException("objType", string.Format("Unable to wrap unknown object type {0}; object must be MapiFolder or MapiMessage", objType));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000F03C File Offset: 0x0000D23C
		public MapiTable GetContentsTable(ContentsTableFlags flags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			MapiTable result;
			try
			{
				MapiTable mapiTable = null;
				IExMapiTable exMapiTable = null;
				try
				{
					int contentsTable = this.iMAPIContainer.GetContentsTable((int)flags, out exMapiTable);
					if (contentsTable != 0)
					{
						base.ThrowIfError("Unable to get contents table.", contentsTable);
					}
					mapiTable = new MapiTable(exMapiTable, base.MapiStore);
					exMapiTable = null;
				}
				finally
				{
					exMapiTable.DisposeIfValid();
				}
				result = mapiTable;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public MapiTable GetContentsTable()
		{
			return this.GetContentsTable(ContentsTableFlags.DeferredErrors | ContentsTableFlags.Unicode);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000F0CD File Offset: 0x0000D2CD
		public MapiTable GetAssociatedContentsTable()
		{
			return this.GetContentsTable(ContentsTableFlags.Associated | ContentsTableFlags.DeferredErrors | ContentsTableFlags.Unicode);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public MapiTable GetHierarchyTable(HierarchyTableFlags flags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			MapiTable result;
			try
			{
				MapiTable mapiTable = null;
				IExMapiTable exMapiTable = null;
				try
				{
					int hierarchyTable = this.iMAPIContainer.GetHierarchyTable((int)flags, out exMapiTable);
					if (hierarchyTable != 0)
					{
						base.ThrowIfError("Unable to get hierarchy table.", hierarchyTable);
					}
					mapiTable = new MapiTable(exMapiTable, base.MapiStore);
					exMapiTable = null;
				}
				finally
				{
					exMapiTable.DisposeIfValid();
				}
				result = mapiTable;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000F160 File Offset: 0x0000D360
		public MapiTable GetHierarchyTable()
		{
			return this.GetHierarchyTable(HierarchyTableFlags.DeferredErrors | HierarchyTableFlags.Unicode);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000F170 File Offset: 0x0000D370
		public void SetSearchCriteria(Restriction restriction, byte[][] entryIds, SearchCriteriaFlags flags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(15))
				{
					ComponentTrace<MapiNetTags>.Trace<SearchCriteriaFlags, string>(12466, 15, (long)this.GetHashCode(), "MapiFolder.SetSearchCriteria params: flags={0}, entryIds={1}", flags, TraceUtils.DumpArray(entryIds));
				}
				int num = this.iMAPIContainer.SetSearchCriteria(restriction, entryIds, (int)flags);
				if (num != 0)
				{
					base.ThrowIfError("Unable to SetSearchCriteria.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		public void GetSearchCriteria(out Restriction restriction, out byte[][] entryIds, out SearchState state)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				int num = 0;
				state = SearchState.None;
				int searchCriteria = this.iMAPIContainer.GetSearchCriteria(int.MinValue, out restriction, out entryIds, out num);
				if (searchCriteria != 0)
				{
					base.ThrowIfError("Unable to GetSearchCriteria.", searchCriteria);
				}
				state = (SearchState)num;
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000F254 File Offset: 0x0000D454
		public object OpenEntry(byte[] entryId)
		{
			return this.OpenEntry(entryId, OpenEntryFlags.BestAccess | OpenEntryFlags.DeferredErrors);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000F260 File Offset: 0x0000D460
		public object OpenEntry(byte[] entryId, OpenEntryFlags flags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			object result;
			try
			{
				object obj = null;
				int objType = 0;
				IExInterface exInterface = null;
				try
				{
					exInterface = this.InternalOpenEntry(entryId, flags, out objType);
					if (exInterface != null)
					{
						obj = MapiContainer.Wrap(exInterface, objType, base.MapiStore);
						exInterface = null;
					}
				}
				finally
				{
					exInterface.DisposeIfValid();
				}
				result = obj;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		protected IExInterface InternalOpenEntry(byte[] entryId, OpenEntryFlags flags, out int objType)
		{
			base.BlockExternalObjectCheck();
			IExInterface exInterface = null;
			bool flag = false;
			int ulFlags = (int)(flags & ~OpenEntryFlags.DontThrowIfEntryIsMissing);
			try
			{
				int num = this.iMAPIContainer.OpenEntry(entryId, Guid.Empty, ulFlags, out objType, out exInterface);
				if (num == -2147221233 && (flags & OpenEntryFlags.DontThrowIfEntryIsMissing) != OpenEntryFlags.None)
				{
					return null;
				}
				if (num != 0)
				{
					base.ThrowIfError("Unable to open entry.", num);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					exInterface.DisposeIfValid();
					exInterface = null;
				}
			}
			return exInterface;
		}

		// Token: 0x040004F5 RID: 1269
		protected IExMapiContainer iMAPIContainer;
	}
}
