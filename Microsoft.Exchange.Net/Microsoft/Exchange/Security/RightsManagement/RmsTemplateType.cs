using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009BD RID: 2493
	public enum RmsTemplateType
	{
		// Token: 0x04002E95 RID: 11925
		[LocDescription(DrmStrings.IDs.TemplateTypeArchived)]
		Archived,
		// Token: 0x04002E96 RID: 11926
		[LocDescription(DrmStrings.IDs.TemplateTypeDistributed)]
		Distributed,
		// Token: 0x04002E97 RID: 11927
		[LocDescription(DrmStrings.IDs.TemplateTypeAll)]
		All = 100
	}
}
