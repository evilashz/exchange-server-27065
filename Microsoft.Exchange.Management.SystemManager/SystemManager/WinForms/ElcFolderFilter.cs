using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000232 RID: 562
	public enum ElcFolderFilter
	{
		// Token: 0x040009CC RID: 2508
		[LocDescription(Strings.IDs.ELCFolderTypeAll)]
		All,
		// Token: 0x040009CD RID: 2509
		[LocDescription(Strings.IDs.ELCFolderTypeDefault)]
		Default,
		// Token: 0x040009CE RID: 2510
		[LocDescription(Strings.IDs.ELCFolderTypeOrganizational)]
		Organizational
	}
}
