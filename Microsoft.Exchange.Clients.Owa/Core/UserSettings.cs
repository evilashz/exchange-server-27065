using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000276 RID: 630
	[Flags]
	public enum UserSettings : uint
	{
		// Token: 0x040010C8 RID: 4296
		Mail = 1U,
		// Token: 0x040010C9 RID: 4297
		Spelling = 2U,
		// Token: 0x040010CA RID: 4298
		Calendar = 4U,
		// Token: 0x040010CB RID: 4299
		General = 8U,
		// Token: 0x040010CC RID: 4300
		Regional = 16U,
		// Token: 0x040010CD RID: 4301
		Language = 32U
	}
}
