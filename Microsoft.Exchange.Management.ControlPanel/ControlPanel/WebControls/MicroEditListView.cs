using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000611 RID: 1553
	[ClientScriptResource("MicroEditListView", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class MicroEditListView : ListView
	{
		// Token: 0x06004524 RID: 17700 RVA: 0x000D118C File Offset: 0x000CF38C
		protected override void OnLoad(EventArgs e)
		{
			string text = null;
			if (this.MicroEditPanel != null)
			{
				text = this.MicroEditPanel.Roles;
			}
			bool flag = string.IsNullOrEmpty(text) || LoginUtil.IsInRoles(this.Context.User, text.Split(new char[]
			{
				','
			}));
			List<ColumnHeader> list = base.Columns.FindAll((ColumnHeader x) => x is MicroEditColumnHeader);
			if (this.MicroEditPanel == null || !flag)
			{
				list.ForEach(delegate(ColumnHeader x)
				{
					base.Columns.Remove(x);
				});
			}
			else if (list.Count<ColumnHeader>() == 0)
			{
				base.Columns.Add(new MicroEditColumnHeader());
			}
			if (this.MicroEditPanel != null && this.MicroEditPanel.ServiceUrl == null)
			{
				this.MicroEditPanel.ServiceUrl = base.ServiceUrl;
			}
			base.OnLoad(e);
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x000D1275 File Offset: 0x000CF475
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.MicroEditPanel != null)
			{
				descriptor.AddComponentProperty("MicroEditPanel", this.MicroEditPanel.ClientID);
				descriptor.AddProperty("MicroEditColumnTitle", this.MicroEditPanel.Title);
			}
		}

		// Token: 0x170026AE RID: 9902
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x000D12B2 File Offset: 0x000CF4B2
		// (set) Token: 0x06004527 RID: 17703 RVA: 0x000D12BA File Offset: 0x000CF4BA
		public MicroEditPanel MicroEditPanel { get; set; }

		// Token: 0x06004528 RID: 17704 RVA: 0x000D12C3 File Offset: 0x000CF4C3
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.MicroEditPanel != null)
			{
				this.Controls.Add(this.MicroEditPanel);
			}
		}
	}
}
