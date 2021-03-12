using System;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiTableWrapper : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000772C File Offset: 0x0000592C
		internal MapiMessageStoreSession MapiSession
		{
			get
			{
				return this.mapiSession;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007734 File Offset: 0x00005934
		internal MapiTableWrapper(MapiTable mapiTable, MapiMessageStoreSession mapiSession, MapiObjectId mapiObjectId)
		{
			this.mapiTable = mapiTable;
			this.mapiSession = mapiSession;
			this.mapiObjectId = mapiObjectId;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000778C File Offset: 0x0000598C
		public int SeekRow(BookMark bookmark, int crowsSeek)
		{
			int result = 0;
			this.MapiSession.InvokeWithWrappedException(delegate()
			{
				result = this.mapiTable.SeekRow(bookmark, crowsSeek);
			}, Strings.ErrorMapiTableSeekRow(this.mapiObjectId.ToString(), this.MapiSession.ServerName), this.mapiObjectId);
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000781C File Offset: 0x00005A1C
		public void SetColumns(params PropTag[] propTags)
		{
			this.MapiSession.InvokeWithWrappedException(delegate()
			{
				this.mapiTable.SetColumns(propTags);
			}, Strings.ErrorMapiTableSetColumn(this.mapiObjectId.ToString(), this.MapiSession.ServerName), this.mapiObjectId);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000789C File Offset: 0x00005A9C
		public PropValue[][] QueryRows(int crows)
		{
			PropValue[][] results = null;
			this.MapiSession.InvokeWithWrappedException(delegate()
			{
				results = this.mapiTable.QueryRows(crows);
			}, Strings.ErrorMapiTableQueryRows(this.mapiObjectId.ToString(), this.MapiSession.ServerName), this.mapiObjectId);
			return results;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007902 File Offset: 0x00005B02
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MapiTableWrapper>(this);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000790A File Offset: 0x00005B0A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000791F File Offset: 0x00005B1F
		public void Dispose()
		{
			if (this.mapiTable != null)
			{
				this.mapiTable.Dispose();
				this.mapiTable = null;
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

		// Token: 0x040000F6 RID: 246
		private MapiTable mapiTable;

		// Token: 0x040000F7 RID: 247
		private MapiMessageStoreSession mapiSession;

		// Token: 0x040000F8 RID: 248
		private MapiObjectId mapiObjectId;

		// Token: 0x040000F9 RID: 249
		private DisposeTracker disposeTracker;
	}
}
