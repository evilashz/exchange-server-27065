using System;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200000E RID: 14
	public abstract class ConversionColumn : Column
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000BE38 File Offset: 0x0000A038
		protected ConversionColumn(string name, Type type, int size, int maxLength, Table table, Func<object, object> conversionFunction, string functionName, Column argumentColumn) : base(name, type, argumentColumn.IsNullable, argumentColumn.Visibility, maxLength, size, table)
		{
			this.conversionFunction = conversionFunction;
			this.functionName = functionName;
			this.argumentColumns[0] = argumentColumn;
			base.CacheHashCode();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000BE8C File Offset: 0x0000A08C
		public override bool IsNullable
		{
			get
			{
				return this.ArgumentColumn.IsNullable;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000BE99 File Offset: 0x0000A099
		public Func<object, object> ConversionFunction
		{
			get
			{
				return this.conversionFunction;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000BEA1 File Offset: 0x0000A0A1
		public string FunctionName
		{
			get
			{
				return this.functionName;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000BEA9 File Offset: 0x0000A0A9
		public Column ArgumentColumn
		{
			get
			{
				return this.argumentColumns[0];
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000BEB3 File Offset: 0x0000A0B3
		public override Column[] ArgumentColumns
		{
			get
			{
				return this.argumentColumns;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000BEBB File Offset: 0x0000A0BB
		public override void EnumerateColumns(Action<Column, object> callback, object state)
		{
			callback(this, state);
			this.ArgumentColumn.EnumerateColumns(callback, state);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append(this.Name);
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
			{
				sb.Append(":CVT[");
				sb.Append(this.functionName);
				sb.Append("(");
				this.ArgumentColumn.AppendToString(sb, formatOptions);
				sb.Append(")]");
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000BF34 File Offset: 0x0000A134
		protected internal override bool ActualColumnEquals(Column other)
		{
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			ConversionColumn conversionColumn = other as ConversionColumn;
			return conversionColumn != null && this.ConversionFunction == conversionColumn.ConversionFunction && this.FunctionName == conversionColumn.FunctionName && this.ArgumentColumn == conversionColumn.ArgumentColumn && this.Name == conversionColumn.Name;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		protected override int CalculateHashCode()
		{
			int num = this.ConversionFunction.GetHashCode() ^ this.ArgumentColumn.GetHashCode() ^ this.Name.GetHashCode();
			if (this.FunctionName != null)
			{
				num ^= this.FunctionName.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		protected override int GetSize(ITWIR context)
		{
			object value = this.GetValue(context);
			return SizeOfColumn.GetColumnSize(this, value).GetValueOrDefault();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000C018 File Offset: 0x0000A218
		protected override object GetValue(ITWIR context)
		{
			object obj = this.ArgumentColumn.Evaluate(context);
			if (obj == null)
			{
				return null;
			}
			return this.conversionFunction(obj);
		}

		// Token: 0x04000068 RID: 104
		private readonly Func<object, object> conversionFunction;

		// Token: 0x04000069 RID: 105
		private readonly string functionName;

		// Token: 0x0400006A RID: 106
		private readonly Column[] argumentColumns = new Column[1];
	}
}
