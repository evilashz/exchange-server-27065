using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200066D RID: 1645
	[ToolboxData("<{0}:TextArea runat=server></{0}:TextArea>")]
	public class TextArea : TextBox
	{
		// Token: 0x06004754 RID: 18260 RVA: 0x000D8568 File Offset: 0x000D6768
		protected override void OnPreRender(EventArgs e)
		{
			if (this.MaxLength > 0)
			{
				if (Util.IsIE() || Util.IsSafari())
				{
					base.Attributes.Add("onkeydown", "return TextAreaUtil.TextAreaOnKeyDownEvent(this);");
					base.Attributes.Add("onpaste", "TextAreaUtil.TextAreaOnPasteEvent(this);");
				}
				else if (Util.IsFirefox())
				{
					base.Attributes.Add("oninput", "TextAreaUtil.TextAreaOnInputEvent(this);");
				}
				else
				{
					base.Attributes.Add("onkeydown", "return TextAreaUtil.TextAreaOnKeyDownEvent(this);");
					base.Attributes.Add("onpaste", "TextAreaUtil.TextAreaOnPasteEvent(this);");
					base.Attributes.Add("oninput", "TextAreaUtil.TextAreaOnInputEvent(this);");
				}
				base.Attributes.Add("onmouseover", "TextAreaUtil.TextAreaOnMouseOverEvent(this);");
				base.Attributes.Add("maxLength", this.MaxLength.ToString());
			}
			base.OnPreRender(e);
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x000D8652 File Offset: 0x000D6852
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			if (this.TextMode == TextBoxMode.MultiLine)
			{
				HttpUtility.HtmlEncode(this.Text, writer);
			}
			this.RenderEndTag(writer);
		}

		// Token: 0x17002767 RID: 10087
		// (get) Token: 0x06004756 RID: 18262 RVA: 0x000D8677 File Offset: 0x000D6877
		// (set) Token: 0x06004757 RID: 18263 RVA: 0x000D867A File Offset: 0x000D687A
		public override TextBoxMode TextMode
		{
			get
			{
				return TextBoxMode.MultiLine;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002768 RID: 10088
		// (get) Token: 0x06004758 RID: 18264 RVA: 0x000D8681 File Offset: 0x000D6881
		// (set) Token: 0x06004759 RID: 18265 RVA: 0x000D8689 File Offset: 0x000D6889
		public override int Rows
		{
			get
			{
				return base.Rows;
			}
			set
			{
				if (value < 5)
				{
					throw new ArgumentException("You may get the bug(Exchange14:$93146) again if you specify Rows < 5 in TextArea.", "TextArea.Rows");
				}
				base.Rows = value;
			}
		}
	}
}
