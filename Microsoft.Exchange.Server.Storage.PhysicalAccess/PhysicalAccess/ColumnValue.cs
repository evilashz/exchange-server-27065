using System;
using System.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000016 RID: 22
	public struct ColumnValue
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x0000CA1F File Offset: 0x0000AC1F
		public ColumnValue(Column column, object value)
		{
			this.column = column;
			this.value = value;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000CA2F File Offset: 0x0000AC2F
		public Column Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000CA37 File Offset: 0x0000AC37
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000CA3F File Offset: 0x0000AC3F
		public static bool operator ==(ColumnValue cv1, ColumnValue cv2)
		{
			return cv1.Equals(cv2);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000CA54 File Offset: 0x0000AC54
		public static bool operator !=(ColumnValue cv1, ColumnValue cv2)
		{
			return !(cv1 == cv2);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public override int GetHashCode()
		{
			return this.column.GetHashCode();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public override bool Equals(object other)
		{
			if (other is ColumnValue)
			{
				ColumnValue columnValue = (ColumnValue)other;
				if (object.Equals(columnValue.Column, this.Column))
				{
					return ValueHelper.ValuesEqual(columnValue.Value, this.Value);
				}
			}
			return false;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		[Conditional("DEBUG")]
		private void ValidateColumnValue()
		{
			Type type;
			if (this.value != null)
			{
				type = this.value.GetType();
			}
			else
			{
				type = typeof(DBNull);
			}
			if (this.value != null && (this.column.Type == typeof(string) || this.column.Type.IsArray))
			{
				if (type == typeof(string))
				{
					string text = (string)this.value;
					return;
				}
				if (type.IsArray)
				{
					Array array = (Array)this.value;
				}
			}
		}

		// Token: 0x04000075 RID: 117
		private readonly Column column;

		// Token: 0x04000076 RID: 118
		private readonly object value;
	}
}
