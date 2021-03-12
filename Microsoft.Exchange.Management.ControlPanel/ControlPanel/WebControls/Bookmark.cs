using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200056D RID: 1389
	public class Bookmark : WebControl
	{
		// Token: 0x0600409D RID: 16541 RVA: 0x000C545C File Offset: 0x000C365C
		public Bookmark() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "bookmark";
			base.Attributes.Add("data-control", "Bookmark");
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x000C54C0 File Offset: 0x000C36C0
		public void AddEntry(string anchor, string title, string workflowName, string visibilityBinding)
		{
			if (string.IsNullOrEmpty(anchor))
			{
				throw new ArgumentNullException("anchor");
			}
			if (string.IsNullOrEmpty(title))
			{
				ArgumentNullException ex = new ArgumentNullException("title");
				ex.Data["SectionID"] = anchor;
				throw ex;
			}
			this.anchors.Add(anchor);
			this.titles.Add(HttpUtility.HtmlEncode(title));
			this.workflows.Add(workflowName);
			if (!string.IsNullOrEmpty(visibilityBinding))
			{
				this.sectionVisibilityBinding.Add(anchor, visibilityBinding);
			}
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x000C5548 File Offset: 0x000C3748
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			int num = 0;
			foreach (string text in this.anchors)
			{
				string text2 = this.titles[num];
				string text3 = this.workflows[num];
				string text4 = this.sectionVisibilityBinding.ContainsKey(text) ? string.Format("data-control=\"Panel\" data-visible=\"{0}\"", this.sectionVisibilityBinding[text]) : string.Empty;
				writer.Write("<span {4}><a name=\"{0}\" id=\"bookmarklink_{1}\" role=\"tab\" ecp_index=\"{1}\" workflow=\"{2}\">{3}</a><div class=\"bmSplit\" ></div></span>", new object[]
				{
					text,
					num,
					text3,
					text2,
					text4
				});
				num++;
			}
			writer.Write("<div id=\"ptr\" class=\"ptr CommonSprite ArrowExpand\" style=\"display:none\" ></div>");
			this.RenderEndTag(writer);
		}

		// Token: 0x04002AF5 RID: 10997
		private List<string> anchors = new List<string>();

		// Token: 0x04002AF6 RID: 10998
		private List<string> titles = new List<string>();

		// Token: 0x04002AF7 RID: 10999
		private List<string> workflows = new List<string>();

		// Token: 0x04002AF8 RID: 11000
		private Dictionary<string, string> sectionVisibilityBinding = new Dictionary<string, string>();
	}
}
