using System;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000351 RID: 849
	internal class LegacySingleLineItemList : LegacyItemList
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x000B9DFD File Offset: 0x000B7FFD
		protected virtual bool IsRenderColumnShadow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x000B9E00 File Offset: 0x000B8000
		protected virtual string ListViewStyle
		{
			get
			{
				return "slIL";
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000B9E07 File Offset: 0x000B8007
		public LegacySingleLineItemList(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext) : this(viewDescriptor, sortedColumn, sortOrder, userContext, SearchScope.SelectedFolder)
		{
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000B9E15 File Offset: 0x000B8015
		public LegacySingleLineItemList(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext, SearchScope folderScope) : base(viewDescriptor, sortedColumn, sortOrder, userContext, folderScope)
		{
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000B9E24 File Offset: 0x000B8024
		protected override void ValidatedRender(TextWriter writer, int startRange, int endRange)
		{
			writer.Write("<table class=\"");
			writer.Write(this.ListViewStyle);
			writer.Write("\" id=\"");
			writer.Write("tblIL");
			writer.Write("\"");
			if (this.ViewDescriptor.IsFixedWidth)
			{
				writer.Write(" style=\"width:");
				writer.Write(this.ViewDescriptor.Width);
				writer.Write("em\"");
			}
			writer.Write(">");
			writer.Write("<col style=\"width:0px\">");
			for (int i = 0; i < this.ViewDescriptor.ColumnCount; i++)
			{
				ColumnId column = this.ViewDescriptor.GetColumn(i);
				Column column2 = ListViewColumns.GetColumn(column);
				writer.Write("<col style=\"width:");
				writer.Write(this.ViewDescriptor.GetColumnWidth(i).ToString(CultureInfo.InvariantCulture));
				if (this.ViewDescriptor.IsFixedWidth || !column2.IsFixedWidth)
				{
					writer.Write("%");
				}
				else
				{
					writer.Write("px");
				}
				writer.Write(";\"");
				writer.Write(" class=\"");
				switch (column2.HorizontalAlign)
				{
				case HorizontalAlign.Center:
					writer.Write("c");
					break;
				case HorizontalAlign.Right:
					writer.Write("r d");
					break;
				default:
					writer.Write(" d");
					break;
				}
				if (this.IsRenderColumnShadow && base.SortedColumn.Id == column)
				{
					writer.Write(" s");
				}
				writer.Write("\"");
				writer.Write(">");
			}
			this.DataSource.MoveToItem(startRange);
			while (this.DataSource.CurrentItem <= endRange)
			{
				writer.Write("<tr");
				base.RenderItemTooltip(writer);
				this.RenderItemRowStyle(writer);
				writer.Write(" id=\"");
				writer.Write(base.IsForVirtualListView ? "vr" : "us");
				writer.Write("\"><td style=\"width:0px\"");
				this.RenderItemMetaDataExpandos(writer);
				writer.Write("></td>");
				this.RenderInnerRowContainerStart(writer);
				for (int j = 0; j < this.ViewDescriptor.ColumnCount; j++)
				{
					ColumnId column3 = this.ViewDescriptor.GetColumn(j);
					writer.Write("<td nowrap");
					this.RenderTableCellAttributes(writer, column3);
					if (column3 == ColumnId.FlagDueDate || column3 == ColumnId.ContactFlagDueDate || column3 == ColumnId.TaskFlag)
					{
						writer.Write(" id=");
						writer.Write("tdFlg");
					}
					if (column3 == ColumnId.Categories || column3 == ColumnId.ContactCategories)
					{
						writer.Write(" id=");
						writer.Write("tdCat");
					}
					writer.Write(">");
					base.RenderColumn(writer, column3);
					writer.Write("</td>");
				}
				this.RenderInnerRowContainerEnd(writer);
				writer.Write("</tr>");
				this.DataSource.MoveNext();
			}
			writer.Write("</table>");
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000BA11D File Offset: 0x000B831D
		protected virtual void RenderItemRowStyle(TextWriter writer)
		{
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x000BA11F File Offset: 0x000B831F
		protected virtual void RenderInnerRowContainerStart(TextWriter writer)
		{
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000BA121 File Offset: 0x000B8321
		protected virtual void RenderInnerRowContainerEnd(TextWriter writer)
		{
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000BA123 File Offset: 0x000B8323
		protected virtual void RenderTableCellAttributes(TextWriter writer, ColumnId columnId)
		{
		}
	}
}
