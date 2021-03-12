using System;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiModifyTableWrapper : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00007548 File Offset: 0x00005748
		internal MapiMessageStoreSession MapiSession
		{
			get
			{
				return this.mapiSession;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007550 File Offset: 0x00005750
		internal MapiModifyTableWrapper(MapiModifyTable mapiModifyTable, MapiMessageStoreSession mapiSession, MapiStore mapiStore, MapiObjectId mapiObjectId)
		{
			this.mapiModifyTable = mapiModifyTable;
			this.mapiSession = mapiSession;
			this.mapiStore = mapiStore;
			this.mapiObjectId = mapiObjectId;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007581 File Offset: 0x00005781
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MapiModifyTableWrapper>(this);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007589 File Offset: 0x00005789
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000075C0 File Offset: 0x000057C0
		public MapiTableWrapper GetTable(GetTableFlags flags)
		{
			MapiTable mapiTable = null;
			this.MapiSession.InvokeWithWrappedException(delegate()
			{
				mapiTable = this.mapiModifyTable.GetTable(GetTableFlags.None);
			}, Strings.ErrorGetMapiTableWithIdentityAndServer(this.mapiObjectId.ToString(), this.MapiSession.ServerName), this.mapiObjectId);
			return new MapiTableWrapper(mapiTable, this.MapiSession, this.mapiObjectId);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007658 File Offset: 0x00005858
		public void ModifyTable(ModifyTableFlags flags, RowEntry[] rowList)
		{
			this.MapiSession.InvokeWithWrappedException(delegate()
			{
				this.mapiModifyTable.ModifyTable(flags, rowList);
			}, Strings.ErrorModifyMapiTableWithIdentityAndServer(this.mapiObjectId.ToString(), this.MapiSession.ServerName), this.mapiObjectId);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000076B8 File Offset: 0x000058B8
		public void Dispose()
		{
			if (this.mapiModifyTable != null)
			{
				this.mapiModifyTable.Dispose();
				this.mapiModifyTable = null;
			}
			if (this.mapiStore != null)
			{
				this.mapiStore.Dispose();
				this.mapiStore = null;
			}
			if (this.mapiSession != null)
			{
				this.mapiSession = null;
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
		}

		// Token: 0x040000F0 RID: 240
		private MapiModifyTable mapiModifyTable;

		// Token: 0x040000F1 RID: 241
		private MapiMessageStoreSession mapiSession;

		// Token: 0x040000F2 RID: 242
		private MapiStore mapiStore;

		// Token: 0x040000F3 RID: 243
		private MapiObjectId mapiObjectId;

		// Token: 0x040000F4 RID: 244
		private DisposeTracker disposeTracker;

		// Token: 0x040000F5 RID: 245
		public static readonly Guid IExchangeModifyTable = new Guid("2d734cb0-53fd-101b-b19d-08002b3056e3");
	}
}
