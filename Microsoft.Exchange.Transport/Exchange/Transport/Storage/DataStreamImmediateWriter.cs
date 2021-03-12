using System;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000C9 RID: 201
	internal class DataStreamImmediateWriter : DataStreamImmediate
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x0001C634 File Offset: 0x0001A834
		internal DataStreamImmediateWriter(DataColumn column, DataTableCursor cursor, DataRow dataRow, bool update, Func<bool> checkpointCallback, int sequence) : base(column, cursor, dataRow, sequence)
		{
			this.checkpointCallback = checkpointCallback;
			if (update)
			{
				try
				{
					this.length = (long)(Api.RetrieveColumnSize(cursor.Session, cursor.TableId, column.ColumnId, base.Sequence, RetrieveColumnGrbit.None) ?? 0);
					return;
				}
				catch (EsentErrorException ex)
				{
					this.inError = true;
					if (!DataSource.HandleIsamException(ex, cursor.Connection.Source))
					{
						throw;
					}
					return;
				}
			}
			this.length = -1L;
			this.SetColumnLength(0L);
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001C6D7 File Offset: 0x0001A8D7
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001C6DA File Offset: 0x0001A8DA
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001C6DD File Offset: 0x0001A8DD
		public override bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001C6E7 File Offset: 0x0001A8E7
		public override void Flush()
		{
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001C6E9 File Offset: 0x0001A8E9
		public override void SetLength(long value)
		{
			this.SetColumnLength(value);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001C6F4 File Offset: 0x0001A8F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (offset == 0 && buffer.Length == count)
			{
				this.Write(buffer);
				return;
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = buffer[offset + i];
			}
			this.Write(array);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001C738 File Offset: 0x0001A938
		private void Write(byte[] buffer)
		{
			this.Write(this.Position, buffer);
			this.position += (long)buffer.Length;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001C758 File Offset: 0x0001A958
		private void Write(long position, byte[] data)
		{
			this.ThrowIfDataRowNotInUpdate();
			if (this.inError)
			{
				ExTraceGlobals.ExpoTracer.TraceError(0L, "Attempt to write to the database stream after it encountered an exception was skipped.");
				return;
			}
			JET_SETINFO jet_SETINFO = new JET_SETINFO();
			jet_SETINFO.ibLongValue = (int)position;
			jet_SETINFO.itagSequence = base.Sequence;
			SetColumnGrbit setColumnGrbit = (position >= this.length) ? SetColumnGrbit.AppendLV : SetColumnGrbit.None;
			if (this.Column.IntrinsicLV)
			{
				setColumnGrbit |= SetColumnGrbit.IntrinsicLV;
			}
			try
			{
				if (Api.JetSetColumn(this.cursor.Session, this.cursor.TableId, this.Column.ColumnId, data, data.Length, setColumnGrbit, jet_SETINFO) != JET_wrn.Success)
				{
					this.inError = true;
					throw new InvalidOperationException(Strings.StreamStateInvalid);
				}
			}
			catch (EsentErrorException ex)
			{
				this.inError = true;
				if (!DataSource.HandleIsamException(ex, this.cursor.Connection.Source))
				{
					throw;
				}
			}
			this.length = Math.Max(this.length, position + (long)data.Length);
			base.DataRow.PerfCounters.StreamWrites.Increment();
			if (data != null)
			{
				base.DataRow.PerfCounters.StreamBytesWritten.IncrementBy((long)data.Length);
				this.bytesWritten += (long)data.Length;
			}
			if (this.checkpointCallback != null && base.DataRow.Updating && this.bytesWritten > 131072L)
			{
				this.checkpointCallback();
				this.bytesWritten = 0L;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
		private void ThrowIfDataRowNotInUpdate()
		{
			if (!this.row.Updating)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001C8E8 File Offset: 0x0001AAE8
		private void SetColumnLength(long value)
		{
			this.ThrowIfDataRowNotInUpdate();
			if (this.length == value)
			{
				return;
			}
			if (this.inError)
			{
				ExTraceGlobals.ExpoTracer.TraceError(0L, "Attempt to modify the length of the database stream after it encountered an exception was skipped.");
				return;
			}
			try
			{
				JET_SETINFO setinfo = new JET_SETINFO
				{
					itagSequence = base.Sequence
				};
				if (value == 0L)
				{
					Api.JetSetColumn(this.cursor.Session, base.Cursor.TableId, this.Column.ColumnId, null, 0, SetColumnGrbit.ZeroLength, setinfo);
				}
				else
				{
					byte[] bytes = BitConverter.GetBytes(value);
					Api.JetSetColumn(this.cursor.Session, base.Cursor.TableId, this.Column.ColumnId, bytes, bytes.Length, SetColumnGrbit.SizeLV, setinfo);
				}
			}
			catch (EsentErrorException ex)
			{
				this.inError = true;
				if (!DataSource.HandleIsamException(ex, this.cursor.Connection.Source))
				{
					throw;
				}
			}
			this.length = value;
			base.DataRow.PerfCounters.StreamSetLen.Increment();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04000362 RID: 866
		private const int CheckPointChunkSize = 131072;

		// Token: 0x04000363 RID: 867
		private Func<bool> checkpointCallback;

		// Token: 0x04000364 RID: 868
		private long bytesWritten;

		// Token: 0x04000365 RID: 869
		private bool inError;
	}
}
