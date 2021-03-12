using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200000F RID: 15
	public abstract class FunctionColumn : Column
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000C04C File Offset: 0x0000A24C
		protected FunctionColumn(string name, Type type, int size, int maxLength, Table table, Func<object[], object> function, string functionName, params Column[] argumentColumns) : base(name, type, true, VisibilityHelper.Select(from col in argumentColumns
		select col.Visibility), maxLength, size, table)
		{
			this.function = function;
			this.functionName = functionName;
			this.argumentColumns = argumentColumns;
			base.CacheHashCode();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000C0AE File Offset: 0x0000A2AE
		public Func<object[], object> Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000C0B6 File Offset: 0x0000A2B6
		public string FunctionName
		{
			get
			{
				return this.functionName;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000C0BE File Offset: 0x0000A2BE
		public override Column[] ArgumentColumns
		{
			get
			{
				return this.argumentColumns;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		public static FunctionColumn CreateEscrowUpdateFunctionColumn(Table table, Column column, int deltaValue)
		{
			return Factory.CreateFunctionColumn("EscrowUpdateFunctionColumn", typeof(int), PropertyTypeHelper.SizeFromPropType(PropertyType.Int32), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Int32), table, delegate(object[] columnValues)
			{
				int num = (int)columnValues[0];
				int val = num + deltaValue;
				return Math.Max(0, val);
			}, "EscrowUpdateFunction", new Column[]
			{
				column
			});
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000C154 File Offset: 0x0000A354
		public override void EnumerateColumns(Action<Column, object> callback, object state)
		{
			callback(this, state);
			if (this.argumentColumns != null)
			{
				foreach (Column column in this.argumentColumns)
				{
					column.EnumerateColumns(callback, state);
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000C194 File Offset: 0x0000A394
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append(this.Name);
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
			{
				sb.Append(":FNC[");
				sb.Append(this.functionName);
				sb.Append("(");
				if (this.argumentColumns != null)
				{
					for (int i = 0; i < this.argumentColumns.Length; i++)
					{
						if (i != 0)
						{
							sb.Append(", ");
						}
						this.argumentColumns[i].AppendToString(sb, formatOptions);
					}
				}
				sb.Append(")]");
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000C220 File Offset: 0x0000A420
		protected internal override bool ActualColumnEquals(Column other)
		{
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			FunctionColumn functionColumn = other as FunctionColumn;
			if (functionColumn != null && this.Name == functionColumn.Name && this.Function == functionColumn.Function && this.FunctionName == functionColumn.FunctionName && (object.ReferenceEquals(this.ArgumentColumns, functionColumn.ArgumentColumns) || (this.ArgumentColumns != null && functionColumn.ArgumentColumns != null && this.ArgumentColumns.Length == functionColumn.ArgumentColumns.Length)))
			{
				if (!object.ReferenceEquals(this.ArgumentColumns, functionColumn.ArgumentColumns))
				{
					for (int i = 0; i < this.ArgumentColumns.Length; i++)
					{
						if (!this.ArgumentColumns[i].Equals(functionColumn.ArgumentColumns[i]))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		protected override int CalculateHashCode()
		{
			int num = this.Name.GetHashCode() ^ this.Function.GetHashCode();
			if (this.ArgumentColumns != null)
			{
				foreach (Column column in this.ArgumentColumns)
				{
					num ^= column.GetHashCode();
				}
			}
			if (this.FunctionName != null)
			{
				num ^= this.FunctionName.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000C364 File Offset: 0x0000A564
		protected override int GetSize(ITWIR context)
		{
			object value = this.GetValue(context);
			return SizeOfColumn.GetColumnSize(this, value).GetValueOrDefault();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000C38C File Offset: 0x0000A58C
		protected override object GetValue(ITWIR context)
		{
			object[] array = null;
			if (this.ArgumentColumns != null)
			{
				array = new object[this.ArgumentColumns.Length];
				for (int i = 0; i < this.ArgumentColumns.Length; i++)
				{
					array[i] = this.argumentColumns[i].Evaluate(context);
					if (array[i] is LargeValue && context is IColumnStreamAccess && this.argumentColumns[i] is PhysicalColumn)
					{
						array[i] = this.GetStreamableColumnValue((IColumnStreamAccess)context, (PhysicalColumn)this.argumentColumns[i]);
					}
				}
			}
			return this.function(array);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000C420 File Offset: 0x0000A620
		private byte[] GetStreamableColumnValue(IColumnStreamAccess streamAccess, PhysicalColumn column)
		{
			byte[] result;
			using (PhysicalColumnStream physicalColumnStream = new PhysicalColumnStream(streamAccess, column, true))
			{
				byte[] array = new byte[physicalColumnStream.Length];
				physicalColumnStream.Read(array, 0, array.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x0400006B RID: 107
		private readonly Func<object[], object> function;

		// Token: 0x0400006C RID: 108
		private readonly string functionName;

		// Token: 0x0400006D RID: 109
		private readonly Column[] argumentColumns;
	}
}
