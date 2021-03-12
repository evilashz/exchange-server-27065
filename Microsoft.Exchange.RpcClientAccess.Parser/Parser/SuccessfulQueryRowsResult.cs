using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200031C RID: 796
	internal sealed class SuccessfulQueryRowsResult : RopResult
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00032F5E File Offset: 0x0003115E
		internal PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00032F66 File Offset: 0x00031166
		internal PropertyValue[][] Rows
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00032F6E File Offset: 0x0003116E
		internal BookmarkOrigin BookmarkOrigin
		{
			get
			{
				return this.bookmarkOrigin;
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00032F76 File Offset: 0x00031176
		internal SuccessfulQueryRowsResult(BookmarkOrigin bookmarkOrigin, RowCollector rowCollector) : base(RopId.QueryRows, ErrorCode.None, null)
		{
			this.bookmarkOrigin = bookmarkOrigin;
			this.columns = rowCollector.Columns;
			this.propertyRows = rowCollector.Rows;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00032FA1 File Offset: 0x000311A1
		internal SuccessfulQueryRowsResult(Reader reader, PropertyTag[] columns, Encoding string8Encoding) : base(reader)
		{
			this.columns = columns;
			this.bookmarkOrigin = (BookmarkOrigin)reader.ReadByte();
			this.propertyRows = reader.ReadSizeAndPropertyRowList(columns, string8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00032FCC File Offset: 0x000311CC
		public override string ToString()
		{
			return string.Format("SuccessfulQueryRowsResult: [BookmarkOrigin: {0}] [Rows: {1}]", this.bookmarkOrigin, this.propertyRows.Count);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00032FF3 File Offset: 0x000311F3
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.bookmarkOrigin);
			writer.WriteSizeAndPropertyRowList(this.propertyRows, base.String8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0003301C File Offset: 0x0003121C
		internal PropertyValue[][] GetValues()
		{
			PropertyValue[][] array = new PropertyValue[this.propertyRows.Count][];
			for (int i = 0; i < this.propertyRows.Count; i++)
			{
				array[i] = this.propertyRows[i].PropertyValues;
			}
			return array;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00033068 File Offset: 0x00031268
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Bookmark=").Append(this.bookmarkOrigin);
			stringBuilder.Append(" Rows=").Append((this.propertyRows != null) ? this.propertyRows.Count : 0);
		}

		// Token: 0x04000A0F RID: 2575
		internal const int BookmarkOriginSize = 1;

		// Token: 0x04000A10 RID: 2576
		private readonly BookmarkOrigin bookmarkOrigin;

		// Token: 0x04000A11 RID: 2577
		private readonly PropertyTag[] columns;

		// Token: 0x04000A12 RID: 2578
		private readonly List<PropertyRow> propertyRows;
	}
}
