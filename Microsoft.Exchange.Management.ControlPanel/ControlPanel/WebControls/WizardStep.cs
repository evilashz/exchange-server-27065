using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020001E8 RID: 488
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("WizardStep", "Microsoft.Exchange.Management.ControlPanel.Client.Wizard.js")]
	public class WizardStep : WizardStepBase
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x0007483D File Offset: 0x00072A3D
		public WizardStep()
		{
			base.ClientClassName = "WizardStep";
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00074850 File Offset: 0x00072A50
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<WizardStep>(this);
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0007486C File Offset: 0x00072A6C
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.AddProperty("StepID", this.ID);
			scriptDescriptor.AddProperty("NextStepID", base.NextStepID);
			return scriptDescriptor;
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000748A4 File Offset: 0x00072AA4
		protected Properties FindPropertiesParent()
		{
			Properties properties = null;
			Control parent = this.Parent;
			while (parent != null && parent != this.Page)
			{
				properties = (parent as Properties);
				if (properties != null)
				{
					break;
				}
				parent = parent.Parent;
			}
			return properties;
		}
	}
}
