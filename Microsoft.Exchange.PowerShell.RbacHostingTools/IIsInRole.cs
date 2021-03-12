using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools
{
	// Token: 0x02000003 RID: 3
	internal interface IIsInRole
	{
		// Token: 0x0600000D RID: 13
		bool IsInRole(string role);

		// Token: 0x0600000E RID: 14
		bool IsInRole(string role, ADRawEntry adRawEntry);
	}
}
