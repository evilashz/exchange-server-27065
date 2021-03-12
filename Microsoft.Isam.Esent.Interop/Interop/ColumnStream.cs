using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200009E RID: 158
	public class ColumnStream : Stream
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x000109CD File Offset: 0x0000EBCD
		public ColumnStream(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			this.sesid = sesid;
			this.tableid = tableid;
			this.columnid = columnid;
			this.Itag = 1;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000109F1 File Offset: 0x0000EBF1
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x000109F9 File Offset: 0x0000EBF9
		public int Itag { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00010A02 File Offset: 0x0000EC02
		public override bool CanRead
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00010A05 File Offset: 0x0000EC05
		public override bool CanWrite
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00010A08 File Offset: 0x0000EC08
		public override bool CanSeek
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00010A0B File Offset: 0x0000EC0B
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x00010A14 File Offset: 0x0000EC14
		public override long Position
		{
			[DebuggerStepThrough]
			get
			{
				return (long)this.ibLongValue;
			}
			set
			{
				if (value < 0L || value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", value, "A long-value offset has to be between 0 and 0x7fffffff bytes");
				}
				this.ibLongValue = checked((int)value);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00010A44 File Offset: 0x0000EC44
		public override long Length
		{
			get
			{
				JET_RETINFO retinfo = new JET_RETINFO
				{
					itagSequence = this.Itag,
					ibLongValue = 0
				};
				int num;
				Api.JetRetrieveColumn(this.sesid, this.tableid, this.columnid, null, 0, out num, ColumnStream.RetrieveGrbit, retinfo);
				return (long)num;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00010A90 File Offset: 0x0000EC90
		private static RetrieveColumnGrbit RetrieveGrbit
		{
			[DebuggerStepThrough]
			get
			{
				return RetrieveColumnGrbit.RetrieveCopy;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00010A94 File Offset: 0x0000EC94
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ColumnStream(0x{0:x}:{1})", new object[]
			{
				this.columnid.Value,
				this.Itag
			});
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00010AD9 File Offset: 0x0000ECD9
		public override void Flush()
		{
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00010ADC File Offset: 0x0000ECDC
		public override void Write(byte[] buffer, int offset, int count)
		{
			ColumnStream.CheckBufferArguments(buffer, offset, count);
			checked
			{
				int num = (int)this.Length;
				int num2 = this.ibLongValue + count;
				JET_SETINFO setinfo;
				if (this.ibLongValue > num)
				{
					setinfo = new JET_SETINFO
					{
						itagSequence = this.Itag
					};
					Api.JetSetColumn(this.sesid, this.tableid, this.columnid, null, this.ibLongValue, SetColumnGrbit.SizeLV, setinfo);
					num = this.ibLongValue;
				}
				SetColumnGrbit grbit;
				if (this.ibLongValue == num)
				{
					grbit = SetColumnGrbit.AppendLV;
				}
				else if (num2 >= num)
				{
					grbit = (SetColumnGrbit.OverwriteLV | SetColumnGrbit.SizeLV);
				}
				else
				{
					grbit = SetColumnGrbit.OverwriteLV;
				}
				setinfo = new JET_SETINFO
				{
					itagSequence = this.Itag,
					ibLongValue = this.ibLongValue
				};
				Api.JetSetColumn(this.sesid, this.tableid, this.columnid, buffer, count, offset, grbit, setinfo);
				this.ibLongValue += count;
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		public override int Read(byte[] buffer, int offset, int count)
		{
			ColumnStream.CheckBufferArguments(buffer, offset, count);
			if ((long)this.ibLongValue >= this.Length)
			{
				return 0;
			}
			JET_RETINFO retinfo = new JET_RETINFO
			{
				itagSequence = this.Itag,
				ibLongValue = this.ibLongValue
			};
			int val;
			Api.JetRetrieveColumn(this.sesid, this.tableid, this.columnid, buffer, count, offset, out val, ColumnStream.RetrieveGrbit, retinfo);
			int num = Math.Min(val, count);
			checked
			{
				this.ibLongValue += num;
				return num;
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00010C34 File Offset: 0x0000EE34
		public override void SetLength(long value)
		{
			if (value > 2147483647L || value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", value, "A LongValueStream cannot be longer than 0x7FFFFFF or less than 0 bytes");
			}
			if (value < this.Length && value > 0L)
			{
				byte[] array = new byte[value];
				JET_RETINFO retinfo = new JET_RETINFO
				{
					itagSequence = this.Itag,
					ibLongValue = 0
				};
				int num;
				Api.JetRetrieveColumn(this.sesid, this.tableid, this.columnid, array, array.Length, out num, ColumnStream.RetrieveGrbit, retinfo);
				JET_SETINFO setinfo = new JET_SETINFO
				{
					itagSequence = this.Itag
				};
				Api.JetSetColumn(this.sesid, this.tableid, this.columnid, array, array.Length, SetColumnGrbit.None, setinfo);
			}
			else
			{
				JET_SETINFO setinfo2 = new JET_SETINFO
				{
					itagSequence = this.Itag
				};
				SetColumnGrbit grbit = (0L == value) ? SetColumnGrbit.ZeroLength : SetColumnGrbit.SizeLV;
				Api.JetSetColumn(this.sesid, this.tableid, this.columnid, null, checked((int)value), grbit, setinfo2);
			}
			if ((long)this.ibLongValue > value)
			{
				this.ibLongValue = checked((int)value);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00010D50 File Offset: 0x0000EF50
		public override long Seek(long offset, SeekOrigin origin)
		{
			checked
			{
				long num;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = offset;
					break;
				case SeekOrigin.Current:
					num = unchecked((long)this.ibLongValue) + offset;
					break;
				case SeekOrigin.End:
					num = this.Length + offset;
					break;
				default:
					throw new ArgumentOutOfRangeException("origin", origin, "Unknown origin");
				}
				if (num < 0L || num > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("offset", offset, "invalid offset/origin combination");
				}
				this.ibLongValue = (int)num;
			}
			return (long)this.ibLongValue;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		private static void CheckBufferArguments(ICollection<byte> buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "cannot be negative");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "cannot be negative");
			}
			if (checked(buffer.Count - offset) < count)
			{
				throw new ArgumentOutOfRangeException("count", count, "cannot be larger than the size of the buffer");
			}
		}

		// Token: 0x04000326 RID: 806
		private const int MaxLongValueSize = 2147483647;

		// Token: 0x04000327 RID: 807
		private readonly JET_SESID sesid;

		// Token: 0x04000328 RID: 808
		private readonly JET_TABLEID tableid;

		// Token: 0x04000329 RID: 809
		private readonly JET_COLUMNID columnid;

		// Token: 0x0400032A RID: 810
		private int ibLongValue;
	}
}
