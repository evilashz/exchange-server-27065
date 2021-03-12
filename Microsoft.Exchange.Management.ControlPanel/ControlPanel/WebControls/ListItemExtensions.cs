using System;
using System.Security.Principal;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005FE RID: 1534
	public static class ListItemExtensions
	{
		// Token: 0x060044C5 RID: 17605 RVA: 0x000CFA44 File Offset: 0x000CDC44
		public static bool IsAccessibleToUser(this ListItem listItem, IPrincipal user)
		{
			string text = listItem.Attributes["Roles"];
			return string.IsNullOrEmpty(text) || LoginUtil.IsInRoles(user, text.Split(new char[]
			{
				','
			}));
		}

		// Token: 0x04002E10 RID: 11792
		private const string Roles = "Roles";
	}
}
