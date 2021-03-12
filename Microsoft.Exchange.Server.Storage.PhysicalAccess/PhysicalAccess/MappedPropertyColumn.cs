using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200000D RID: 13
	public class MappedPropertyColumn : ExtendedPropertyColumn
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000BCCC File Offset: 0x00009ECC
		protected MappedPropertyColumn(Column actualColumn, StorePropTag propTag) : base(actualColumn.Name, actualColumn.Type, actualColumn.IsNullable, MappedPropertyColumn.GetVisibility(propTag, actualColumn), actualColumn.Size, actualColumn.MaxLength, actualColumn.Table, propTag)
		{
			this.actualColumn = actualColumn;
			base.CacheHashCode();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000BD18 File Offset: 0x00009F18
		public override Column ActualColumn
		{
			get
			{
				return this.actualColumn;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000BD20 File Offset: 0x00009F20
		public override Column ColumnForEquality
		{
			get
			{
				Column column = this.ActualColumn;
				while (column is MappedPropertyColumn)
				{
					column = column.ActualColumn;
				}
				return column;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000BD46 File Offset: 0x00009F46
		public override void EnumerateColumns(Action<Column, object> callback, object state)
		{
			callback(this, state);
			this.actualColumn.EnumerateColumns(callback, state);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000BD60 File Offset: 0x00009F60
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append(this.Name);
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
			{
				sb.Append(":MAP[");
				base.StorePropTag.AppendToString(sb, true);
				sb.Append("->");
				this.actualColumn.AppendToString(sb, formatOptions);
				sb.Append("]");
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000BDC1 File Offset: 0x00009FC1
		protected internal override bool ActualColumnEquals(Column other)
		{
			return this.actualColumn.ActualColumnEquals(other);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000BDCF File Offset: 0x00009FCF
		protected override int CalculateHashCode()
		{
			return this.actualColumn.GetHashCode();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000BDDC File Offset: 0x00009FDC
		protected override int GetSize(ITWIR context)
		{
			return ((IColumn)this.actualColumn).GetSize(context);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000BDEA File Offset: 0x00009FEA
		protected override object GetValue(ITWIR context)
		{
			return ((IColumn)this.actualColumn).GetValue(context);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		private static Visibility GetVisibility(StorePropTag propTag, Column column)
		{
			Visibility v = (propTag.PropInfo != null) ? propTag.PropInfo.Visibility : Visibility.Public;
			Visibility v2 = (column != null) ? column.Visibility : Visibility.Public;
			return VisibilityHelper.Select(v, v2);
		}

		// Token: 0x04000067 RID: 103
		private Column actualColumn;
	}
}
