using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200023C RID: 572
	internal sealed class SuccessfulExpandRowResult : RopResult
	{
		// Token: 0x06000C7C RID: 3196 RVA: 0x00027692 File Offset: 0x00025892
		internal SuccessfulExpandRowResult(int expandedRowCount, RowCollector rowCollector) : base(RopId.ExpandRow, ErrorCode.None, null)
		{
			this.expandedRowCount = expandedRowCount;
			this.columns = rowCollector.Columns;
			this.propertyRows = rowCollector.Rows;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000276BD File Offset: 0x000258BD
		internal SuccessfulExpandRowResult(Reader reader, PropertyTag[] columns, Encoding string8Encoding) : base(reader)
		{
			this.expandedRowCount = reader.ReadInt32();
			this.columns = columns;
			this.propertyRows = reader.ReadSizeAndPropertyRowList(columns, string8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x000276E8 File Offset: 0x000258E8
		internal PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x000276F0 File Offset: 0x000258F0
		internal List<PropertyRow> PropertyRows
		{
			get
			{
				return this.propertyRows;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000276F8 File Offset: 0x000258F8
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.expandedRowCount);
			writer.WriteSizeAndPropertyRowList(this.propertyRows, base.String8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x040006D9 RID: 1753
		private readonly int expandedRowCount;

		// Token: 0x040006DA RID: 1754
		private readonly PropertyTag[] columns;

		// Token: 0x040006DB RID: 1755
		private List<PropertyRow> propertyRows;
	}
}
