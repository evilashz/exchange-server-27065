using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002FB RID: 763
	public class Update : EsentResource
	{
		// Token: 0x06000E12 RID: 3602 RVA: 0x0001C46C File Offset: 0x0001A66C
		public Update(JET_SESID sesid, JET_TABLEID tableid, JET_prep prep)
		{
			if (JET_prep.Cancel == prep)
			{
				throw new ArgumentException("Cannot create an Update for JET_prep.Cancel", "prep");
			}
			this.sesid = sesid;
			this.tableid = tableid;
			this.prep = prep;
			Api.JetPrepareUpdate(this.sesid, this.tableid, this.prep);
			base.ResourceWasAllocated();
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Update ({0})", new object[]
			{
				this.prep
			});
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0001C4FA File Offset: 0x0001A6FA
		public void Save(byte[] bookmark, int bookmarkSize, out int actualBookmarkSize)
		{
			base.CheckObjectIsNotDisposed();
			if (!base.HasResource)
			{
				throw new InvalidOperationException("Not in an update");
			}
			Api.JetUpdate(this.sesid, this.tableid, bookmark, bookmarkSize, out actualBookmarkSize);
			base.ResourceWasReleased();
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0001C530 File Offset: 0x0001A730
		public void Save()
		{
			int num;
			this.Save(null, 0, out num);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0001C548 File Offset: 0x0001A748
		public void SaveAndGotoBookmark()
		{
			byte[] array = null;
			try
			{
				array = Caches.BookmarkCache.Allocate();
				int bookmarkSize;
				this.Save(array, array.Length, out bookmarkSize);
				Api.JetGotoBookmark(this.sesid, this.tableid, array, bookmarkSize);
			}
			finally
			{
				if (array != null)
				{
					Caches.BookmarkCache.Free(ref array);
				}
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		public void Cancel()
		{
			base.CheckObjectIsNotDisposed();
			if (!base.HasResource)
			{
				throw new InvalidOperationException("Not in an update");
			}
			Api.JetPrepareUpdate(this.sesid, this.tableid, JET_prep.Cancel);
			base.ResourceWasReleased();
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0001C5D7 File Offset: 0x0001A7D7
		protected override void ReleaseResource()
		{
			this.Cancel();
		}

		// Token: 0x04000946 RID: 2374
		private readonly JET_SESID sesid;

		// Token: 0x04000947 RID: 2375
		private readonly JET_TABLEID tableid;

		// Token: 0x04000948 RID: 2376
		private readonly JET_prep prep;
	}
}
