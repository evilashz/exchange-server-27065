using System;
using System.Collections.Generic;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A6 RID: 166
	[ClientScriptResource("RegionalProperties", "Microsoft.Exchange.Management.ControlPanel.Client.RegionalProperties.js")]
	public sealed class RegionalProperties : Properties, IScriptControl
	{
		// Token: 0x06001C11 RID: 7185 RVA: 0x00057D88 File Offset: 0x00055F88
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptDescriptor = this.GetScriptDescriptor();
			scriptDescriptor.Type = "RegionalProperties";
			scriptDescriptor.AddProperty("LanguageDateSets", RegionalSettingsSlab.LanguageDateSets);
			scriptDescriptor.AddProperty("LanguageTimeSets", RegionalSettingsSlab.LanguageTimeSets);
			return new ScriptControlDescriptor[]
			{
				scriptDescriptor
			};
		}
	}
}
