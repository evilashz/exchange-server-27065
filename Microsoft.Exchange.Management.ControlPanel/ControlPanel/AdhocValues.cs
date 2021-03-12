using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200056A RID: 1386
	[ClientScriptResource("AdhocValues", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class AdhocValues : ScriptControlBase
	{
		// Token: 0x06004079 RID: 16505 RVA: 0x000C4C06 File Offset: 0x000C2E06
		public AdhocValues()
		{
			this.Values = new List<ValuePair>();
			this.Name = "AdhocValues";
		}

		// Token: 0x170024FC RID: 9468
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x000C4C24 File Offset: 0x000C2E24
		// (set) Token: 0x0600407B RID: 16507 RVA: 0x000C4C2C File Offset: 0x000C2E2C
		[DefaultValue("AdhocValues")]
		public string Name { get; set; }

		// Token: 0x170024FD RID: 9469
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000C4C35 File Offset: 0x000C2E35
		// (set) Token: 0x0600407D RID: 16509 RVA: 0x000C4C3D File Offset: 0x000C2E3D
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		public List<ValuePair> Values { get; private set; }

		// Token: 0x0600407E RID: 16510 RVA: 0x000C4C46 File Offset: 0x000C2E46
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CssClass = "hidden";
			base.Attributes["data-control"] = "AdhocValues";
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000C4C70 File Offset: 0x000C2E70
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			foreach (ValuePair child in this.Values)
			{
				this.Controls.Add(child);
			}
		}

		// Token: 0x170024FE RID: 9470
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x000C4CD0 File Offset: 0x000C2ED0
		protected override string TagName
		{
			get
			{
				return "var";
			}
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000C4CD8 File Offset: 0x000C2ED8
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			this.DataBind();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (ValuePair valuePair in this.Values)
			{
				dictionary.Add(valuePair.Name, valuePair.Value);
			}
			descriptor.AddScriptProperty("Values", dictionary.ToJsonString(null));
			descriptor.AddProperty("Name", this.Name);
		}
	}
}
