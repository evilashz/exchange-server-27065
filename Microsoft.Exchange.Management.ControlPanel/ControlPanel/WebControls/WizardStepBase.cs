using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020001E7 RID: 487
	public class WizardStepBase : Panel, IScriptControl
	{
		// Token: 0x060025E4 RID: 9700 RVA: 0x00074733 File Offset: 0x00072933
		public WizardStepBase()
		{
			this.FvaEnabled = true;
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x00074742 File Offset: 0x00072942
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.Style[HtmlTextWriterStyle.Display] = "none";
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0007475D File Offset: 0x0007295D
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x00074777 File Offset: 0x00072977
		// (set) Token: 0x060025E8 RID: 9704 RVA: 0x0007477F File Offset: 0x0007297F
		public string ClientClassName { get; set; }

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x00074788 File Offset: 0x00072988
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x00074790 File Offset: 0x00072990
		public string NextStepID { get; set; }

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x00074799 File Offset: 0x00072999
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x000747A1 File Offset: 0x000729A1
		[DefaultValue(true)]
		public bool FvaEnabled { get; set; }

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000747AA File Offset: 0x000729AA
		// (set) Token: 0x060025EE RID: 9710 RVA: 0x000747B2 File Offset: 0x000729B2
		public string Title { get; set; }

		// Token: 0x060025EF RID: 9711 RVA: 0x000747BC File Offset: 0x000729BC
		public virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return new ScriptDescriptor[]
			{
				this.GetScriptDescriptor()
			};
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000747DC File Offset: 0x000729DC
		protected virtual ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor(this.ClientClassName, this.ClientID);
			scriptControlDescriptor.AddProperty("FvaEnabled", this.FvaEnabled, true);
			scriptControlDescriptor.AddProperty("Title", this.Title, true);
			scriptControlDescriptor.AddComponentProperty("Form", "aspnetForm");
			return scriptControlDescriptor;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x00074830 File Offset: 0x00072A30
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}
	}
}
