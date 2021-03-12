using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000410 RID: 1040
	public class SecondaryNavigationFilter
	{
		// Token: 0x06002588 RID: 9608 RVA: 0x000D91A7 File Offset: 0x000D73A7
		public SecondaryNavigationFilter(string elementId, string title, string onClickHandler)
		{
			this.elementId = elementId;
			this.title = title;
			this.onClickHandler = onClickHandler;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000D91D0 File Offset: 0x000D73D0
		public void AddFilter(string displayString, int value, bool isSelected)
		{
			SecondaryNavigationFilter.FilterInfo item;
			item.DisplayString = displayString;
			item.Value = value;
			item.IsSelected = isSelected;
			this.filters.Add(item);
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x000D9201 File Offset: 0x000D7401
		private string RadioButtonName
		{
			get
			{
				return "_" + this.elementId;
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000D9214 File Offset: 0x000D7414
		public void Render(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<div id=\"");
			output.Write(this.elementId);
			output.Write("\" _onclick=\"");
			Utilities.HtmlEncode(this.onClickHandler, output);
			output.Write("\" fSNFlt=\"1\" class=\"secNvFltRg\">");
			output.Write("<div class=\"secNvFltTtl\">");
			Utilities.HtmlEncode(this.title, output);
			output.Write("</div>");
			for (int i = 0; i < this.filters.Count; i++)
			{
				this.RenderFilter(output, this.filters[i]);
			}
			output.Write("</div>");
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000D92C0 File Offset: 0x000D74C0
		private void RenderFilter(TextWriter output, SecondaryNavigationFilter.FilterInfo filter)
		{
			output.Write("<div id=\"divSecNvFltItm\" _iF=\"");
			output.Write(filter.Value);
			output.Write("\"><div class=\"secNvFltRdDiv\"><input id=\"inpRdo\" class=\"secNvFltRd\" type=radio name=");
			output.Write(this.RadioButtonName);
			if (filter.IsSelected)
			{
				output.Write(" checked");
			}
			output.Write("></div><div class=\"secNvFltTxt\">");
			Utilities.HtmlEncode(filter.DisplayString, output);
			output.Write("</div></div>");
		}

		// Token: 0x040019F3 RID: 6643
		private string elementId;

		// Token: 0x040019F4 RID: 6644
		private string title;

		// Token: 0x040019F5 RID: 6645
		private string onClickHandler;

		// Token: 0x040019F6 RID: 6646
		private List<SecondaryNavigationFilter.FilterInfo> filters = new List<SecondaryNavigationFilter.FilterInfo>();

		// Token: 0x02000411 RID: 1041
		private struct FilterInfo
		{
			// Token: 0x040019F7 RID: 6647
			public string DisplayString;

			// Token: 0x040019F8 RID: 6648
			public int Value;

			// Token: 0x040019F9 RID: 6649
			public bool IsSelected;
		}
	}
}
