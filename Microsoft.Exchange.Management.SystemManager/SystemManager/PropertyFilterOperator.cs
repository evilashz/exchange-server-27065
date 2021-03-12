using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200003A RID: 58
	public enum PropertyFilterOperator
	{
		// Token: 0x0400008B RID: 139
		[LocDescription(Strings.IDs.Equal)]
		Equal,
		// Token: 0x0400008C RID: 140
		[LocDescription(Strings.IDs.NotEqual)]
		NotEqual,
		// Token: 0x0400008D RID: 141
		[LocDescription(Strings.IDs.LessThan)]
		LessThan,
		// Token: 0x0400008E RID: 142
		[LocDescription(Strings.IDs.LessThanOrEqual)]
		LessThanOrEqual,
		// Token: 0x0400008F RID: 143
		[LocDescription(Strings.IDs.GreaterThan)]
		GreaterThan,
		// Token: 0x04000090 RID: 144
		[LocDescription(Strings.IDs.GreaterThanOrEqual)]
		GreaterThanOrEqual,
		// Token: 0x04000091 RID: 145
		[LocDescription(Strings.IDs.StartsWith)]
		StartsWith,
		// Token: 0x04000092 RID: 146
		[LocDescription(Strings.IDs.EndsWith)]
		EndsWith,
		// Token: 0x04000093 RID: 147
		[LocDescription(Strings.IDs.Contains)]
		Contains,
		// Token: 0x04000094 RID: 148
		[LocDescription(Strings.IDs.NotContains)]
		NotContains,
		// Token: 0x04000095 RID: 149
		[LocDescription(Strings.IDs.Present)]
		Present,
		// Token: 0x04000096 RID: 150
		[LocDescription(Strings.IDs.NotPresent)]
		NotPresent
	}
}
