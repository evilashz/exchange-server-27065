using System;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005BF RID: 1471
	[ClientScriptResource("PickerControlBase", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public abstract class PickerControlBase : ScriptControlBase, INamingContainer
	{
		// Token: 0x060042E1 RID: 17121 RVA: 0x000CB30C File Offset: 0x000C950C
		public PickerControlBase() : this(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x000CB316 File Offset: 0x000C9516
		protected PickerControlBase(HtmlTextWriterTag tag) : base(tag)
		{
			this.DisplayProperty = "DisplayName";
			this.ValueProperty = "Identity";
		}

		// Token: 0x170025F5 RID: 9717
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x000CB335 File Offset: 0x000C9535
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x000CB33D File Offset: 0x000C953D
		public string PickerFormUrl
		{
			get
			{
				return this.pickerFormUrl;
			}
			set
			{
				this.pickerFormUrl = value;
			}
		}

		// Token: 0x170025F6 RID: 9718
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x000CB346 File Offset: 0x000C9546
		// (set) Token: 0x060042E6 RID: 17126 RVA: 0x000CB34E File Offset: 0x000C954E
		[DefaultValue("DisplayName")]
		public string DisplayProperty { get; set; }

		// Token: 0x170025F7 RID: 9719
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000CB357 File Offset: 0x000C9557
		// (set) Token: 0x060042E8 RID: 17128 RVA: 0x000CB35F File Offset: 0x000C955F
		[DefaultValue("Identity")]
		public string ValueProperty { get; set; }

		// Token: 0x170025F8 RID: 9720
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x000CB368 File Offset: 0x000C9568
		// (set) Token: 0x060042EA RID: 17130 RVA: 0x000CB370 File Offset: 0x000C9570
		public int? DialogHeight { get; set; }

		// Token: 0x170025F9 RID: 9721
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x000CB379 File Offset: 0x000C9579
		// (set) Token: 0x060042EC RID: 17132 RVA: 0x000CB381 File Offset: 0x000C9581
		public int? DialogWidth { get; set; }

		// Token: 0x170025FA RID: 9722
		// (get) Token: 0x060042ED RID: 17133 RVA: 0x000CB38A File Offset: 0x000C958A
		// (set) Token: 0x060042EE RID: 17134 RVA: 0x000CB392 File Offset: 0x000C9592
		[Description("Picker Content to build query parameters")]
		[TemplateInstance(TemplateInstance.Single)]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(PropertiesContentPanel))]
		public virtual ITemplate Content { get; set; }

		// Token: 0x060042EF RID: 17135 RVA: 0x000CB39C File Offset: 0x000C959C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.Content != null)
			{
				PropertiesContentPanel propertiesContentPanel = new PropertiesContentPanel();
				propertiesContentPanel.ID = "contentContainer";
				this.Controls.Add(propertiesContentPanel);
				this.Content.InstantiateIn(propertiesContentPanel);
			}
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000CB3E0 File Offset: 0x000C95E0
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (!string.IsNullOrEmpty(this.PickerFormUrl))
			{
				descriptor.AddProperty("PickerFormUrl", base.ResolveClientUrl(this.PickerFormUrl));
			}
			descriptor.AddProperty("DisplayProperty", this.DisplayProperty);
			descriptor.AddProperty("ValueProperty", this.ValueProperty);
			if (this.DialogHeight != null)
			{
				descriptor.AddProperty("DialogHeight", this.DialogHeight);
			}
			if (this.DialogWidth != null)
			{
				descriptor.AddProperty("DialogWidth", this.DialogWidth);
			}
		}

		// Token: 0x04002D87 RID: 11655
		private string pickerFormUrl;
	}
}
