using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000628 RID: 1576
	[ClientScriptResource("OwaPickerUtil", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:OwaPickerUtil runat=server></{0}:OwaPickerUtil>")]
	public class OwaPickerUtil : OwaScriptsUtil
	{
		// Token: 0x060045A7 RID: 17831 RVA: 0x000D2955 File Offset: 0x000D0B55
		public OwaPickerUtil()
		{
			this.ID = "owaUtil";
			base.NameSpace = "OwaPeoplePickerUtil";
			this.CssClass = "OwaUtilDiv";
		}

		// Token: 0x170026D9 RID: 9945
		// (get) Token: 0x060045A8 RID: 17832 RVA: 0x000D2980 File Offset: 0x000D0B80
		public static bool CanUseOwaPicker
		{
			get
			{
				LocalSession localSession = RbacPrincipal.GetCurrent(false) as LocalSession;
				return localSession != null && !EcpUrl.IsEcpStandalone && !localSession.IsCrossSiteMailboxLogon && localSession.IsInRole("LogonUserMailbox+OWA+MailboxFullAccess+!DelegatedAdmin");
			}
		}
	}
}
