using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A3 RID: 1187
	[ClientScriptResource("AddUMExtensionProperties", "Microsoft.Exchange.Management.ControlPanel.Client.UnifiedMessaging.js")]
	public sealed class AddUMExtensionProperties : Properties
	{
		// Token: 0x06003AF8 RID: 15096 RVA: 0x000B2714 File Offset: 0x000B0914
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (string.IsNullOrEmpty(this.Page.Request["dialPlanId"]))
			{
				throw new BadQueryParameterException("dialPlanId");
			}
			EcpSingleSelect ecpSingleSelect = (EcpSingleSelect)base.ContentContainer.FindControl("pickerUMDialPlan");
			ecpSingleSelect.PickerFormUrl = EcpUrl.AppendQueryParameter(ecpSingleSelect.PickerFormUrl, "dialPlanId", this.Page.Request["dialPlanId"]);
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x000B2790 File Offset: 0x000B0990
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "AddUMExtensionProperties";
			scriptDescriptor.AddElementProperty("Extension", base.ContentContainer.FindControl("txtExtension").ClientID);
			scriptDescriptor.AddElementProperty("PhoneContext", base.ContentContainer.FindControl("txtPhoneContext").ClientID);
			scriptDescriptor.AddElementProperty("DialPlanName", base.ContentContainer.FindControl("txtDialPlanName").ClientID);
			scriptDescriptor.AddElementProperty("ExtensionLabel", base.ContentContainer.FindControl("txtExtension_label").ClientID);
			scriptDescriptor.AddComponentProperty("DialPlanSelector", base.ContentContainer.FindControl("pickerUMDialPlan").ClientID);
			return scriptDescriptor;
		}
	}
}
