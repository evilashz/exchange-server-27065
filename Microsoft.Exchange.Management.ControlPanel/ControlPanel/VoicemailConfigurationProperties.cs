using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000D0 RID: 208
	[ClientScriptResource("VoicemailConfigurationProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Voicemail.js")]
	public sealed class VoicemailConfigurationProperties : VoicemailPropertiesBase
	{
		// Token: 0x06001D5A RID: 7514 RVA: 0x0005A13C File Offset: 0x0005833C
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "VoicemailConfigurationProperties";
			SmsServiceProviders instance = SmsServiceProviders.Instance;
			scriptDescriptor.AddProperty("OperatorIds", instance.VoiceMailCarrierDictionary.Keys);
			scriptDescriptor.AddProperty("VoiceMailCarrierDictionary", instance.VoiceMailCarrierDictionary);
			return scriptDescriptor;
		}
	}
}
