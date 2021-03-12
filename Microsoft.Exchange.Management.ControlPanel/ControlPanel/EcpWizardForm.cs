using System;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200022D RID: 557
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("EcpWizardForm", "Microsoft.Exchange.Management.ControlPanel.Client.Wizard.js")]
	public class EcpWizardForm : WizardFormBase
	{
		// Token: 0x060027BD RID: 10173 RVA: 0x0007CEBC File Offset: 0x0007B0BC
		protected override void OnPreRender(EventArgs e)
		{
			base.NextButton.Attributes.Add("data-visible", "{CanNext, Mode=OneWay}");
			base.BackButton.Attributes.Add("data-visible", "{CanBack, Mode=OneWay}");
			base.CommitButton.Attributes.Add("data-visible", "{IsCommitStep, Mode=OneWay}");
			base.OnPreRender(e);
		}
	}
}
