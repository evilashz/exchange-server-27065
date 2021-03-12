using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005E7 RID: 1511
	[ControlValueProperty("Value")]
	[ClientScriptResource("FormletControlBase", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ToolboxData("<{0}:FormletControlBase runat=server></{0}:FormletControlBase>")]
	public abstract class FormletControlBase<P, E> : ScriptControlBase, INamingContainer where P : FormletParameter where E : Control, new()
	{
		// Token: 0x1700263F RID: 9791
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x000CCD92 File Offset: 0x000CAF92
		// (set) Token: 0x060043BF RID: 17343 RVA: 0x000CCD9A File Offset: 0x000CAF9A
		protected P Parameter { get; set; }

		// Token: 0x17002640 RID: 9792
		// (get) Token: 0x060043C0 RID: 17344 RVA: 0x000CCDA3 File Offset: 0x000CAFA3
		// (set) Token: 0x060043C1 RID: 17345 RVA: 0x000CCDAB File Offset: 0x000CAFAB
		protected E Editor { get; set; }

		// Token: 0x060043C2 RID: 17346 RVA: 0x000CCDB4 File Offset: 0x000CAFB4
		public FormletControlBase() : base(HtmlTextWriterTag.Div)
		{
			if (string.IsNullOrEmpty(this.NoSelectionText))
			{
				this.NoSelectionText = ClientStrings.SelectOneLink;
			}
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000CCDD8 File Offset: 0x000CAFD8
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (string.IsNullOrEmpty(this.ID))
			{
				this.ID = typeof(E).ToString();
			}
			Control control = this.Page.FindControl(this.ID);
			if (control != null)
			{
				this.Editor = (control as E);
			}
			if (this.Editor == null)
			{
				this.Editor = Activator.CreateInstance<E>();
				this.Controls.Add(this.Editor);
			}
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000CCE61 File Offset: 0x000CB061
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddScriptProperty("EditorParameter", this.Parameter.ToJsonString(null));
		}

		// Token: 0x17002641 RID: 9793
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x000CCE86 File Offset: 0x000CB086
		// (set) Token: 0x060043C6 RID: 17350 RVA: 0x000CCE8E File Offset: 0x000CB08E
		public string FormletDialogTitle { get; set; }

		// Token: 0x17002642 RID: 9794
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x000CCE97 File Offset: 0x000CB097
		// (set) Token: 0x060043C8 RID: 17352 RVA: 0x000CCE9F File Offset: 0x000CB09F
		public string NoSelectionText { get; set; }
	}
}
