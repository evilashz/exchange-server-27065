using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000012 RID: 18
	public class SizeOfColumn : Column
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000C785 File Offset: 0x0000A985
		protected SizeOfColumn(string name, Column termColumn, bool compressedSize) : base(name, typeof(int), termColumn.IsNullable, Visibility.Public, 0, 4, termColumn.Table)
		{
			this.termColumn = termColumn;
			this.compressedSize = compressedSize;
			base.CacheHashCode();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000C7BB File Offset: 0x0000A9BB
		public Column TermColumn
		{
			get
			{
				return this.termColumn;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000C7C3 File Offset: 0x0000A9C3
		public bool CompressedSize
		{
			get
			{
				return this.compressedSize;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		public static int? GetColumnSize(Column column, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (column.Size != 0)
			{
				return new int?(column.Size);
			}
			if (column.Type == typeof(string))
			{
				return new int?(((string)value).Length * 2);
			}
			if (column.Type == typeof(byte[]))
			{
				return new int?(((byte[])value).Length);
			}
			if (column.Type == typeof(byte[][]))
			{
				int num = 0;
				foreach (byte[] array2 in (byte[][])value)
				{
					if (array2 != null)
					{
						num += array2.Length;
					}
				}
				return new int?(num);
			}
			if (column.Type == typeof(short[]))
			{
				return new int?(((short[])value).Length * 2);
			}
			throw new NotSupportedException(string.Format("Don't know how to retrieve the size of this data type: {0}", value.GetType()));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public override void EnumerateColumns(Action<Column, object> callback, object state)
		{
			callback(this, state);
			this.termColumn.EnumerateColumns(callback, state);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			if (this.Name != null)
			{
				sb.Append(this.Name);
			}
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || this.Name == null)
			{
				if (this.Name != null)
				{
					sb.Append(":");
				}
				if (this.compressedSize)
				{
					sb.Append("COMPRESSED");
				}
				sb.Append("SIZEOF[");
				this.termColumn.AppendToString(sb, formatOptions);
				sb.Append("]");
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000C964 File Offset: 0x0000AB64
		protected internal override bool ActualColumnEquals(Column other)
		{
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			SizeOfColumn sizeOfColumn = other as SizeOfColumn;
			return sizeOfColumn != null && this.Name == sizeOfColumn.Name && this.TermColumn == sizeOfColumn.TermColumn && this.CompressedSize == sizeOfColumn.CompressedSize;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		protected override int CalculateHashCode()
		{
			int num = 305419896;
			if (this.Name != null)
			{
				num ^= this.Name.GetHashCode();
			}
			num ^= this.TermColumn.GetHashCode();
			return num ^ this.CompressedSize.GetHashCode();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000CA09 File Offset: 0x0000AC09
		protected override int GetSize(ITWIR context)
		{
			return 4;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		protected override object GetValue(ITWIR context)
		{
			return context.GetColumnSize(this.TermColumn);
		}

		// Token: 0x04000073 RID: 115
		private readonly Column termColumn;

		// Token: 0x04000074 RID: 116
		private readonly bool compressedSize;
	}
}
