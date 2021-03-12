using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000418 RID: 1048
	internal class SingleLineItemContentsForVirtualList : ItemList2
	{
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x000DA49E File Offset: 0x000D869E
		protected virtual bool IsRenderColumnShadow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000DA4A1 File Offset: 0x000D86A1
		public SingleLineItemContentsForVirtualList(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext) : this(viewDescriptor, sortedColumn, sortOrder, userContext, SearchScope.SelectedFolder)
		{
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000DA4AF File Offset: 0x000D86AF
		public SingleLineItemContentsForVirtualList(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext, SearchScope folderScope) : base(viewDescriptor, sortedColumn, sortOrder, userContext, folderScope)
		{
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000DA4C0 File Offset: 0x000D86C0
		protected override void ValidatedRender(TextWriter writer, int startRange, int endRange)
		{
			writer.Write("<div class=\"baseIL\" id=\"");
			writer.Write("divVLVIL");
			writer.Write("\">");
			this.DataSource.MoveToItem(startRange);
			while (this.DataSource.CurrentItem <= endRange)
			{
				writer.Write("<div class=\"listItemRow");
				this.RenderItemRowStyle(writer);
				writer.Write("\"");
				base.RenderItemTooltip(writer);
				writer.Write(" id=\"");
				writer.Write(base.IsForVirtualListView ? "vr" : "us");
				writer.Write("\">");
				writer.Write("<div class=\"cData\"");
				ItemList2.RenderRowId(writer, this.DataSource.GetItemId());
				this.RenderItemMetaDataExpandos(writer);
				writer.Write("></div>");
				for (int i = 0; i < this.ViewDescriptor.ColumnCount; i++)
				{
					ColumnId column = this.ViewDescriptor.GetColumn(i);
					writer.Write("<div class=\"listColumn ");
					writer.Write(this.ColumnClassPrefix);
					writer.Write(column.ToString());
					writer.Write("\"");
					this.RenderTableCellAttributes(writer, column);
					if (column == ColumnId.FlagDueDate || column == ColumnId.ContactFlagDueDate || column == ColumnId.TaskFlag)
					{
						writer.Write(" id=");
						writer.Write("divFlg");
					}
					if (column == ColumnId.Categories || column == ColumnId.ContactCategories)
					{
						writer.Write(" id=");
						writer.Write("divCat");
					}
					writer.Write(">");
					base.RenderColumn(writer, column, false);
					writer.Write("</div>");
				}
				base.RenderSelectionImage(writer);
				base.RenderRowDivider(writer);
				writer.Write("</div>");
				this.DataSource.MoveNext();
			}
			writer.Write("</div>");
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000DA68B File Offset: 0x000D888B
		protected virtual void RenderItemRowStyle(TextWriter writer)
		{
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000DA68D File Offset: 0x000D888D
		protected virtual void RenderTableCellAttributes(TextWriter writer, ColumnId columnId)
		{
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x000DA68F File Offset: 0x000D888F
		protected virtual string ColumnClassPrefix
		{
			get
			{
				return string.Empty;
			}
		}
	}
}
