using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005C0 RID: 1472
	[ParseChildren(true, "Text")]
	[ToolboxData("<{0}:PickerControl runat=server></{0}:PickerControl>")]
	[DefaultProperty("Text")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ValidationProperty("Text")]
	[SupportsEventValidation]
	public abstract class PickerControl : PickerControlBase
	{
		// Token: 0x170025FB RID: 9723
		// (get) Token: 0x060042F1 RID: 17137 RVA: 0x000CB486 File Offset: 0x000C9686
		// (set) Token: 0x060042F2 RID: 17138 RVA: 0x000CB493 File Offset: 0x000C9693
		[Bindable(BindableSupport.Yes)]
		[Localizable(true)]
		[DefaultValue(null)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Text
		{
			get
			{
				return this.inlineSelector.Text;
			}
			set
			{
				this.inlineSelector.Text = value;
			}
		}

		// Token: 0x170025FC RID: 9724
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x000CB4A1 File Offset: 0x000C96A1
		// (set) Token: 0x060042F4 RID: 17140 RVA: 0x000CB4AE File Offset: 0x000C96AE
		[Browsable(false)]
		[Localizable(true)]
		public string BrowseButtonText
		{
			get
			{
				return this.inlineSelector.BrowseButtonText;
			}
			set
			{
				this.inlineSelector.BrowseButtonText = value;
			}
		}

		// Token: 0x170025FD RID: 9725
		// (get) Token: 0x060042F5 RID: 17141 RVA: 0x000CB4BC File Offset: 0x000C96BC
		// (set) Token: 0x060042F6 RID: 17142 RVA: 0x000CB4C9 File Offset: 0x000C96C9
		[DefaultValue(true)]
		public override bool Enabled
		{
			get
			{
				return this.inlineSelector.Enabled;
			}
			set
			{
				this.inlineSelector.Enabled = value;
			}
		}

		// Token: 0x170025FE RID: 9726
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x000CB4D7 File Offset: 0x000C96D7
		// (set) Token: 0x060042F8 RID: 17144 RVA: 0x000CB4E4 File Offset: 0x000C96E4
		[DefaultValue("false")]
		public bool HideClearButton
		{
			get
			{
				return this.inlineSelector.HideClearButton;
			}
			set
			{
				this.inlineSelector.HideClearButton = value;
			}
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x000CB4F2 File Offset: 0x000C96F2
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Controls.Add(this.inlineSelector);
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000CB50B File Offset: 0x000C970B
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("InlineSelector", this.inlineSelector.ClientID, this);
		}

		// Token: 0x04002D8D RID: 11661
		private InlineSelector inlineSelector = new InlineSelector();
	}
}
