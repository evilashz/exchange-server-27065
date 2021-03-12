using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000322 RID: 802
	internal class DropDownList : DropDownCombo
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000AF9FD File Offset: 0x000ADBFD
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x000AFA05 File Offset: 0x000ADC05
		protected string SelectedValue
		{
			get
			{
				return this.selectedValue;
			}
			set
			{
				this.selectedValue = value;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000AFA0E File Offset: 0x000ADC0E
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x000AFA16 File Offset: 0x000ADC16
		public string AdditionalListStyles
		{
			get
			{
				return this.additionalListStyles;
			}
			set
			{
				this.additionalListStyles = value;
			}
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000AFA1F File Offset: 0x000ADC1F
		protected DropDownList(string id, bool showOnValueMouseDown, string selectedValue, DropDownListItem[] listItems) : base(id, showOnValueMouseDown)
		{
			this.selectedValue = selectedValue;
			this.listItems = listItems;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000AFA38 File Offset: 0x000ADC38
		public DropDownList(string id, string selectedValue, DropDownListItem[] listItems) : this(id, true, selectedValue, listItems)
		{
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000AFA44 File Offset: 0x000ADC44
		public static void RenderDropDownList(TextWriter writer, string id, string selectedValue, params DropDownListItem[] listItems)
		{
			DropDownList dropDownList = new DropDownList(id, selectedValue, listItems);
			dropDownList.Render(writer);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000AFA64 File Offset: 0x000ADC64
		protected static void RenderListItemHtml(TextWriter writer, object value, SanitizedHtmlString display, string id, bool isBold, bool isRtl)
		{
			writer.Write("<div tabIndex=0 oV=\"");
			Utilities.SanitizeHtmlEncode(value.ToString(), writer);
			if (!string.IsNullOrEmpty(id))
			{
				writer.Write("\" id=\"");
				Utilities.SanitizeHtmlEncode(id, writer);
			}
			writer.Write("\">");
			writer.Write("<span id=\"spanListItm\" class=\"listItm");
			if (isBold)
			{
				writer.Write(" bT");
			}
			writer.Write("\">");
			Utilities.RenderDirectionEnhancedValue(writer, display, isRtl);
			writer.Write("</span>");
			writer.Write("</div>");
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000AFAF1 File Offset: 0x000ADCF1
		protected static void RenderListItem(TextWriter writer, object value, string display, string id, bool isBold, bool isRtl)
		{
			DropDownList.RenderListItemHtml(writer, value, Utilities.SanitizeHtmlEncode(display), id, isBold, isRtl);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000AFB05 File Offset: 0x000ADD05
		protected override void RenderExpandoData(TextWriter writer)
		{
			base.RenderExpandoData(writer);
			if (!string.IsNullOrEmpty(this.selectedValue))
			{
				writer.Write(" oV=\"");
				Utilities.SanitizeHtmlEncode(this.selectedValue, writer);
				writer.Write("\"");
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000AFB40 File Offset: 0x000ADD40
		protected override void RenderDropControl(TextWriter writer)
		{
			writer.Write("<div id=\"divCmbList\" style=\"display:none\"");
			if (this.AdditionalListStyles != null)
			{
				writer.Write(" class=\"");
				writer.Write(this.AdditionalListStyles);
				writer.Write("\"");
			}
			writer.Write(">");
			this.RenderListItems(writer);
			writer.Write("</div>");
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x000AFBA0 File Offset: 0x000ADDA0
		protected override void RenderSelectedValue(TextWriter writer)
		{
			for (int i = 0; i < this.ListItems.Length; i++)
			{
				DropDownListItem dropDownListItem = this.ListItems[i];
				if (dropDownListItem.ItemValue == this.selectedValue)
				{
					Utilities.SanitizeHtmlEncode(dropDownListItem.Display, writer);
					return;
				}
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000AFBEC File Offset: 0x000ADDEC
		protected virtual void RenderListItems(TextWriter writer)
		{
			for (int i = 0; i < this.ListItems.Length; i++)
			{
				DropDownListItem dropDownListItem = this.ListItems[i];
				if (dropDownListItem.IsDisplayTextHtmlEncoded)
				{
					DropDownList.RenderListItemHtml(writer, dropDownListItem.ItemValue, SanitizedHtmlString.GetSanitizedStringWithoutEncoding(dropDownListItem.Display), dropDownListItem.ItemId, dropDownListItem.IsBold, this.sessionContext.IsRtl);
				}
				else
				{
					DropDownList.RenderListItem(writer, dropDownListItem.ItemValue, dropDownListItem.Display, dropDownListItem.ItemId, dropDownListItem.IsBold, this.sessionContext.IsRtl);
				}
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x000AFC76 File Offset: 0x000ADE76
		private DropDownListItem[] ListItems
		{
			get
			{
				if (this.listItems == null)
				{
					this.listItems = this.CreateListItems();
				}
				return this.listItems;
			}
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000AFC92 File Offset: 0x000ADE92
		protected virtual DropDownListItem[] CreateListItems()
		{
			return null;
		}

		// Token: 0x04001675 RID: 5749
		private string selectedValue;

		// Token: 0x04001676 RID: 5750
		protected string additionalListStyles;

		// Token: 0x04001677 RID: 5751
		private DropDownListItem[] listItems;
	}
}
