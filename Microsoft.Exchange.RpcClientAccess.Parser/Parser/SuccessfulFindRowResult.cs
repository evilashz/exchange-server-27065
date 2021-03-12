using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002BA RID: 698
	internal sealed class SuccessfulFindRowResult : RopResult
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		internal PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
		internal bool PositionChanged
		{
			get
			{
				return this.positionChanged;
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0002D4F8 File Offset: 0x0002B6F8
		internal SuccessfulFindRowResult(bool positionChanged, RowCollector rowCollector) : base(RopId.FindRow, ErrorCode.None, null)
		{
			this.positionChanged = positionChanged;
			if (rowCollector.RowCount > 1)
			{
				throw new ArgumentException("RowCollector can only have one row for SuccessfulFindRowResult");
			}
			this.columns = rowCollector.Columns;
			this.propertyRows = rowCollector.Rows;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0002D538 File Offset: 0x0002B738
		internal SuccessfulFindRowResult(Reader reader, PropertyTag[] columns, Encoding string8Encoding) : base(reader)
		{
			this.columns = columns;
			this.positionChanged = reader.ReadBool();
			byte b = reader.ReadBool() ? 1 : 0;
			this.propertyRows = new List<PropertyRow>((int)b);
			for (int i = 0; i < (int)b; i++)
			{
				PropertyRow item = PropertyRow.Parse(reader, columns, WireFormatStyle.Rop);
				item.ResolveString8Values(string8Encoding);
				this.propertyRows.Add(item);
			}
			base.String8Encoding = string8Encoding;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0002D5A8 File Offset: 0x0002B7A8
		public override string ToString()
		{
			return string.Format("SuccessfulFindRowResult: [PositionChanged: {0}]", this.positionChanged);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.positionChanged, 1);
			writer.WriteBool(this.propertyRows.Count > 0, 1);
			foreach (PropertyRow propertyRow in this.propertyRows)
			{
				propertyRow.Serialize(writer, base.String8Encoding, WireFormatStyle.Rop);
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0002D644 File Offset: 0x0002B844
		internal PropertyValue[][] GetValues()
		{
			PropertyValue[][] array = new PropertyValue[this.propertyRows.Count][];
			for (int i = 0; i < this.propertyRows.Count; i++)
			{
				array[i] = this.propertyRows[i].PropertyValues;
			}
			return array;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0002D690 File Offset: 0x0002B890
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" PositionChanged=").Append(this.positionChanged);
			stringBuilder.Append(" RowCount=").Append((this.propertyRows != null) ? this.propertyRows.Count : 0);
		}

		// Token: 0x040007F4 RID: 2036
		internal const int PositionChangedSize = 1;

		// Token: 0x040007F5 RID: 2037
		internal const int RowReturnedSize = 1;

		// Token: 0x040007F6 RID: 2038
		private readonly bool positionChanged;

		// Token: 0x040007F7 RID: 2039
		private readonly PropertyTag[] columns;

		// Token: 0x040007F8 RID: 2040
		private readonly List<PropertyRow> propertyRows;
	}
}
