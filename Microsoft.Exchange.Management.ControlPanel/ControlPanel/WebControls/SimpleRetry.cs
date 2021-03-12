using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200065B RID: 1627
	[ParseChildren(true)]
	[ClientScriptResource("SimpleRetry", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:SimpleRetry runat=server></{0}:SimpleRetry>")]
	public class SimpleRetry : WebServiceExceptionHandler
	{
		// Token: 0x060046C7 RID: 18119 RVA: 0x000D61BD File Offset: 0x000D43BD
		public SimpleRetry()
		{
			base.ExceptionHandlerType = ExceptionHandlerType.SimpleRetry;
		}

		// Token: 0x17002740 RID: 10048
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x000D61CC File Offset: 0x000D43CC
		// (set) Token: 0x060046C9 RID: 18121 RVA: 0x000D61D4 File Offset: 0x000D43D4
		public virtual string Description { get; set; }

		// Token: 0x17002741 RID: 10049
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x000D61DD File Offset: 0x000D43DD
		// (set) Token: 0x060046CB RID: 18123 RVA: 0x000D61E5 File Offset: 0x000D43E5
		[DefaultValue("ClientStrings.IUnderstandAction")]
		public string AcceptanceText { get; set; }

		// Token: 0x17002742 RID: 10050
		// (get) Token: 0x060046CC RID: 18124 RVA: 0x000D61EE File Offset: 0x000D43EE
		// (set) Token: 0x060046CD RID: 18125 RVA: 0x000D61F6 File Offset: 0x000D43F6
		public string PropertiesToAdd { get; set; }

		// Token: 0x060046CE RID: 18126 RVA: 0x000D6200 File Offset: 0x000D4400
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (!this.Description.IsNullOrBlank())
			{
				descriptor.AddProperty("Description", this.Description);
			}
			if (!this.PropertiesToAdd.IsNullOrBlank())
			{
				descriptor.AddProperty("PropertiesToAdd", this.PropertiesToAdd);
			}
			if (!this.AcceptanceText.IsNullOrBlank())
			{
				descriptor.AddProperty("AcceptanceText", this.AcceptanceText);
			}
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x000D6270 File Offset: 0x000D4470
		public override bool ApplyRbacRolesAndAddControls(WebControl parentControl, IPrincipal currentUser)
		{
			bool flag = true;
			if (base.HasAttributes)
			{
				string text = base.Attributes["SetRoles"];
				if (!string.IsNullOrEmpty(text) && !LoginUtil.IsInRoles(currentUser, text.Split(new char[]
				{
					','
				})))
				{
					flag = false;
				}
			}
			if (flag)
			{
				parentControl.Controls.Add(this);
			}
			return flag;
		}
	}
}
