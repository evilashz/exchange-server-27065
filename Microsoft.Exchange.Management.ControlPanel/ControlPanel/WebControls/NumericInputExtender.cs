using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000624 RID: 1572
	[TargetControlType(typeof(TextBox))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("NumericInputBehavior", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class NumericInputExtender : ExtenderControlBase
	{
	}
}
