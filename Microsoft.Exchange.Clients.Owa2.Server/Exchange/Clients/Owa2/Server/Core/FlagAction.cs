using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000D4 RID: 212
	public enum FlagAction
	{
		// Token: 0x040004D6 RID: 1238
		None,
		// Token: 0x040004D7 RID: 1239
		Default,
		// Token: 0x040004D8 RID: 1240
		Today,
		// Token: 0x040004D9 RID: 1241
		Tomorrow,
		// Token: 0x040004DA RID: 1242
		ThisWeek,
		// Token: 0x040004DB RID: 1243
		NextWeek,
		// Token: 0x040004DC RID: 1244
		NoDate,
		// Token: 0x040004DD RID: 1245
		SpecificDate,
		// Token: 0x040004DE RID: 1246
		DateAndReminder,
		// Token: 0x040004DF RID: 1247
		MarkComplete,
		// Token: 0x040004E0 RID: 1248
		ClearFlag
	}
}
