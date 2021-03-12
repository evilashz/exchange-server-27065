using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000007 RID: 7
	public abstract class Column : IColumn, IEquatable<Column>
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000B390 File Offset: 0x00009590
		protected Column(string name, Type type, bool nullable, bool schemaExtension, Visibility visibility, int maxLength, int size, Table table)
		{
			if (schemaExtension)
			{
				this.minVersion = int.MaxValue;
			}
			else
			{
				this.minVersion = 0;
			}
			this.maxVersion = int.MaxValue;
			this.table = table;
			this.name = name;
			this.type = type;
			this.extendedTypeCode = ValueTypeHelper.GetExtendedTypeCode(type);
			this.nullable = nullable;
			this.visibility = visibility;
			this.maxLength = maxLength;
			this.size = size;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000B408 File Offset: 0x00009608
		protected Column(string name, Type type, bool nullable, Visibility visibility, int maxLength, int size, Table table) : this(name, type, nullable, false, visibility, maxLength, size, table)
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000B427 File Offset: 0x00009627
		public virtual bool IsNullable
		{
			get
			{
				return this.nullable;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000B42F File Offset: 0x0000962F
		public bool SchemaExtension
		{
			get
			{
				return this.minVersion != 0;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000B43D File Offset: 0x0000963D
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000B445 File Offset: 0x00009645
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000B44D File Offset: 0x0000964D
		public Table Table
		{
			get
			{
				return this.table;
			}
			internal set
			{
				this.table = value;
				this.CacheHashCode();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000B45C File Offset: 0x0000965C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000B464 File Offset: 0x00009664
		public int MinVersion
		{
			get
			{
				return this.minVersion;
			}
			set
			{
				this.minVersion = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000B46D File Offset: 0x0000966D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000B475 File Offset: 0x00009675
		public int MaxVersion
		{
			get
			{
				return this.maxVersion;
			}
			set
			{
				this.maxVersion = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000B47E File Offset: 0x0000967E
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000B486 File Offset: 0x00009686
		public ExtendedTypeCode ExtendedTypeCode
		{
			get
			{
				return this.extendedTypeCode;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000B48E File Offset: 0x0000968E
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000B496 File Offset: 0x00009696
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000B49E File Offset: 0x0000969E
		public Visibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000B4A6 File Offset: 0x000096A6
		public virtual Column ActualColumn
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000B4A9 File Offset: 0x000096A9
		public virtual Column ColumnForEquality
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000B4AC File Offset: 0x000096AC
		public virtual Column[] ArgumentColumns
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000B4AF File Offset: 0x000096AF
		public static bool operator ==(Column col1, Column col2)
		{
			return object.ReferenceEquals(col1, col2) || (col1 != null && col2 != null && col1.Equals(col2));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000B4CB File Offset: 0x000096CB
		public static bool operator !=(Column col1, Column col2)
		{
			return !(col1 == col2);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000B4D7 File Offset: 0x000096D7
		public object Evaluate(ITWIR twir)
		{
			return twir.GetColumnValue(this);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000B4E0 File Offset: 0x000096E0
		public virtual void EnumerateColumns(Action<Column, object> callback, object state)
		{
			callback(this, state);
		}

		// Token: 0x06000041 RID: 65
		public abstract void AppendToString(StringBuilder sb, StringFormatOptions formatOptions);

		// Token: 0x06000042 RID: 66 RVA: 0x0000B4EC File Offset: 0x000096EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToString(stringBuilder, StringFormatOptions.IncludeDetails);
			return stringBuilder.ToString();
		}

		// Token: 0x06000043 RID: 67
		protected internal abstract bool ActualColumnEquals(Column other);

		// Token: 0x06000044 RID: 68 RVA: 0x0000B50D File Offset: 0x0000970D
		public bool Equals(Column other)
		{
			return other != null && this.ActualColumnEquals(other.ColumnForEquality);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000B520 File Offset: 0x00009720
		public override bool Equals(object other)
		{
			if (other != null)
			{
				Column column = other as Column;
				if (column != null)
				{
					return this.Equals(column);
				}
			}
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000B543 File Offset: 0x00009743
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000B54B File Offset: 0x0000974B
		int IColumn.GetSize(ITWIR context)
		{
			if (!this.IsNullable && this.Size > 0)
			{
				return this.Size;
			}
			return this.GetSize(context);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000B56C File Offset: 0x0000976C
		object IColumn.GetValue(ITWIR context)
		{
			return this.GetValue(context);
		}

		// Token: 0x06000049 RID: 73
		protected abstract int GetSize(ITWIR context);

		// Token: 0x0600004A RID: 74
		protected abstract object GetValue(ITWIR context);

		// Token: 0x0600004B RID: 75 RVA: 0x0000B575 File Offset: 0x00009775
		protected void CacheHashCode()
		{
			this.hashCode = this.CalculateHashCode();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000B583 File Offset: 0x00009783
		protected virtual int CalculateHashCode()
		{
			return this.Name.GetHashCode() ^ this.Type.GetHashCode() ^ ((this.Table == null) ? 0 : this.Table.GetHashCode());
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000B5B3 File Offset: 0x000097B3
		public virtual void GetNameOrIdForSerialization(out string columnName, out uint columnId)
		{
			columnName = this.Name;
			columnId = 0U;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public bool TryGetColumnMaxSize(out int columnMaxSize)
		{
			columnMaxSize = Math.Max(this.MaxLength, this.Size);
			return true;
		}

		// Token: 0x04000048 RID: 72
		private readonly bool nullable;

		// Token: 0x04000049 RID: 73
		private readonly ExtendedTypeCode extendedTypeCode;

		// Token: 0x0400004A RID: 74
		private readonly int maxLength;

		// Token: 0x0400004B RID: 75
		private readonly Type type;

		// Token: 0x0400004C RID: 76
		protected string name;

		// Token: 0x0400004D RID: 77
		private readonly int size;

		// Token: 0x0400004E RID: 78
		private readonly Visibility visibility;

		// Token: 0x0400004F RID: 79
		private int minVersion;

		// Token: 0x04000050 RID: 80
		private int maxVersion;

		// Token: 0x04000051 RID: 81
		private Table table;

		// Token: 0x04000052 RID: 82
		private int hashCode;
	}
}
