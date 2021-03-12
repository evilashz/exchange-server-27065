using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x0200009B RID: 155
	internal class BlobCollection
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x00016A20 File Offset: 0x00014C20
		public BlobCollection(DataColumn column, DataRow row)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			if (row == null)
			{
				throw new ArgumentNullException("row");
			}
			if (!column.MultiValued)
			{
				throw new ArgumentException("Column is not MultiValued.", "column");
			}
			if (column.JetColType != JET_coltyp.LongBinary)
			{
				throw new ArgumentException("Column is not of type LongBinary.", "column");
			}
			this.column = column;
			this.row = row;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00016A90 File Offset: 0x00014C90
		public Stream OpenWriter(byte blobKey, DataTableCursor cursor, bool update, bool cached = false, Func<bool> checkpointCallback = null)
		{
			Stream result;
			lock (this)
			{
				int sequence = this.GetSequence(blobKey, true, cursor);
				Stream stream = cached ? this.column.OpenCachingWriter(cursor, this.row, update, checkpointCallback, sequence) : this.column.OpenImmediateWriter(cursor, this.row, update, sequence);
				result = stream;
			}
			return result;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00016B08 File Offset: 0x00014D08
		public Stream OpenReader(byte blobKey, DataTableCursor cursor, bool lazy = false)
		{
			Stream result;
			lock (this)
			{
				int sequence = this.GetSequence(blobKey, false, cursor);
				if (sequence > 0)
				{
					result = (lazy ? this.column.OpenLazyReader(cursor, this.row, sequence) : this.column.OpenImmediateReader(cursor, this.row, sequence));
				}
				else
				{
					result = Stream.Null;
				}
			}
			return result;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00016B80 File Offset: 0x00014D80
		private int GetSequence(byte blobKey, bool create, DataTableCursor cursor)
		{
			this.LazyInitMap(cursor);
			int num = this.map.IndexOf(blobKey);
			if (num >= 0 || !create)
			{
				return num + 1;
			}
			this.map.Add(blobKey);
			this.SaveMap(cursor);
			return this.map.Count;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00016BCC File Offset: 0x00014DCC
		private void LazyInitMap(DataTableCursor cursor)
		{
			if (this.map == null)
			{
				this.map = new List<byte>();
				byte[] array = this.column.BytesFromCursor(cursor, true, 1);
				if (array == null || array.Length == 0)
				{
					this.map.Add(byte.MaxValue);
					return;
				}
				this.map.AddRange(array);
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00016C20 File Offset: 0x00014E20
		private void SaveMap(DataTableCursor cursor)
		{
			this.column.SaveToCursor(cursor, this.map.ToArray(), 1, false, -1);
		}

		// Token: 0x040002C7 RID: 711
		private const byte BlobCollectionMapKey = 255;

		// Token: 0x040002C8 RID: 712
		private readonly DataColumn column;

		// Token: 0x040002C9 RID: 713
		private readonly DataRow row;

		// Token: 0x040002CA RID: 714
		private List<byte> map;
	}
}
