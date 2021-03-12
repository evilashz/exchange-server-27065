using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000011 RID: 17
	public class ConstantColumn : Column
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000C63A File Offset: 0x0000A83A
		protected ConstantColumn(string name, Type type, Visibility visibility, int size, int maxLength, object value) : base(name, type, value == null, visibility, maxLength, size, null)
		{
			this.value = value;
			base.CacheHashCode();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000C65D File Offset: 0x0000A85D
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000C665 File Offset: 0x0000A865
		public override void EnumerateColumns(Action<Column, object> callback, object state)
		{
			callback(this, state);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000C670 File Offset: 0x0000A870
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			if (this.Name != null)
			{
				sb.Append(this.Name);
				sb.Append(":");
			}
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
			{
				sb.Append("CONST");
			}
			sb.Append("[");
			if ((formatOptions & StringFormatOptions.SkipParametersData) == StringFormatOptions.None)
			{
				sb.AppendAsString(this.Value);
			}
			else
			{
				sb.Append((this.Value != null) ? "X" : "Null");
			}
			sb.Append("]");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		protected internal override bool ActualColumnEquals(Column other)
		{
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			ConstantColumn constantColumn = other as ConstantColumn;
			return constantColumn != null && object.ReferenceEquals(base.Type, constantColumn.Type) && ValueHelper.ValuesEqual(this.Value, constantColumn.Value);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000C743 File Offset: 0x0000A943
		protected override int CalculateHashCode()
		{
			if (this.Value != null)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000C75C File Offset: 0x0000A95C
		protected override int GetSize(ITWIR context)
		{
			return SizeOfColumn.GetColumnSize(this, this.Value).GetValueOrDefault();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000C77D File Offset: 0x0000A97D
		protected override object GetValue(ITWIR context)
		{
			return this.value;
		}

		// Token: 0x04000072 RID: 114
		private readonly object value;
	}
}
