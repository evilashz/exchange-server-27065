using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000C4 RID: 196
	internal abstract class DataStream : Stream
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x0001C070 File Offset: 0x0001A270
		internal DataStream(DataColumn column, DataRow row, int sequence)
		{
			if (!column.MultiValued && sequence != 1)
			{
				throw new ArgumentException("Column is not multi-valued and sequence != 1 was suplied", "column");
			}
			this.Column = column;
			this.row = row;
			this.sequence = sequence;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001C0A9 File Offset: 0x0001A2A9
		internal DataStream(DataStream rhs) : this(rhs.Column, rhs.DataRow, rhs.Sequence)
		{
			this.length = rhs.Length;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		internal static int JetChunkSize
		{
			get
			{
				if (DataStream.jetChunkSize == 0)
				{
					DataSource.InitGlobal();
					int num = 0;
					string text = null;
					Api.JetGetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, (JET_param)163, ref num, out text, 0);
					DataStream.jetChunkSize = num;
				}
				return DataStream.jetChunkSize;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001C114 File Offset: 0x0001A314
		internal static int TransportChunkSize
		{
			get
			{
				if (DataStream.transportChunkSize == 0)
				{
					DataSource.InitGlobal();
					int num = DataStream.JetChunkSize;
					DataStream.transportChunkSize = Math.Max(1, 65536 / num) * num;
				}
				return DataStream.transportChunkSize;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001C14C File Offset: 0x0001A34C
		internal static int BufferedStreamSize
		{
			get
			{
				if (DataStream.bufferedStreamSize == 0)
				{
					DataSource.InitGlobal();
					TransportAppConfig.JetDatabaseConfig jetDatabase = Components.TransportAppConfig.JetDatabase;
					DataStream.bufferedStreamSize = (int)((double)jetDatabase.BufferedStreamSize);
				}
				return DataStream.bufferedStreamSize;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001C186 File Offset: 0x0001A386
		internal DataRow DataRow
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001C18E File Offset: 0x0001A38E
		internal int Sequence
		{
			get
			{
				return this.sequence;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001C196 File Offset: 0x0001A396
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001C19E File Offset: 0x0001A39E
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001C1A6 File Offset: 0x0001A3A6
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (value < 0L || value > this.length)
				{
					throw new DataStream.PositionException(this, value);
				}
				this.position = value;
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.Position = offset;
				break;
			case SeekOrigin.Current:
				this.Position += offset;
				break;
			case SeekOrigin.End:
				this.Position = this.Length + offset;
				break;
			}
			return this.position;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001C218 File Offset: 0x0001A418
		protected void SetReadPosition(long newPosition)
		{
			this.position = newPosition;
			this.length = Math.Max(this.length, newPosition);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001C234 File Offset: 0x0001A434
		protected int InternalRead(byte[] buffer, int offset, int count, DataTableCursor cursor)
		{
			JET_RETINFO retinfo = new JET_RETINFO
			{
				ibLongValue = (int)this.position,
				itagSequence = this.Sequence
			};
			int num = 0;
			try
			{
				Api.JetRetrieveColumn(cursor.Session, cursor.TableId, this.Column.ColumnId, buffer, count, offset, out num, RetrieveColumnGrbit.RetrieveNull, retinfo);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, cursor.Connection.Source))
				{
					throw;
				}
			}
			this.DataRow.PerfCounters.StreamReads.Increment();
			if (num == 0)
			{
				return 0;
			}
			int num2 = Math.Min(count, num);
			this.DataRow.PerfCounters.StreamBytesRead.IncrementBy((long)num2);
			this.SetReadPosition(this.Position + (long)num2);
			return num2;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001C308 File Offset: 0x0001A508
		protected override void Dispose(bool disposing)
		{
			this.row = null;
			base.Dispose(disposing);
		}

		// Token: 0x04000357 RID: 855
		public readonly DataColumn Column;

		// Token: 0x04000358 RID: 856
		private static int jetChunkSize;

		// Token: 0x04000359 RID: 857
		private static int transportChunkSize;

		// Token: 0x0400035A RID: 858
		private static int bufferedStreamSize;

		// Token: 0x0400035B RID: 859
		protected DataRow row;

		// Token: 0x0400035C RID: 860
		protected int sequence;

		// Token: 0x0400035D RID: 861
		protected long length;

		// Token: 0x0400035E RID: 862
		protected long position;

		// Token: 0x020000C5 RID: 197
		internal class PositionException : InvalidOperationException
		{
			// Token: 0x060006F0 RID: 1776 RVA: 0x0001C318 File Offset: 0x0001A518
			public PositionException(object stream, long seekValue)
			{
				this.stream = stream;
				this.seekValue = seekValue;
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x0001C32E File Offset: 0x0001A52E
			protected PositionException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				if (info != null)
				{
					this.seekValue = (long)info.GetValue("Position", typeof(long));
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001C35B File Offset: 0x0001A55B
			public object Stream
			{
				get
				{
					return this.stream;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001C363 File Offset: 0x0001A563
			public long Position
			{
				get
				{
					return this.seekValue;
				}
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x0001C36B File Offset: 0x0001A56B
			[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				base.GetObjectData(info, context);
				info.AddValue("Position", this.Position, typeof(long));
			}

			// Token: 0x0400035F RID: 863
			private object stream;

			// Token: 0x04000360 RID: 864
			private long seekValue;
		}
	}
}
