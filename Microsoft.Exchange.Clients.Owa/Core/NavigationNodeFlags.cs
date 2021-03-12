using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000166 RID: 358
	[Flags]
	internal enum NavigationNodeFlags
	{
		// Token: 0x040008C0 RID: 2240
		None = 0,
		// Token: 0x040008C1 RID: 2241
		PublicFolder = 1,
		// Token: 0x040008C2 RID: 2242
		PublicFolderFavorite = 2,
		// Token: 0x040008C3 RID: 2243
		ImapFolder = 4,
		// Token: 0x040008C4 RID: 2244
		DavFolder = 8,
		// Token: 0x040008C5 RID: 2245
		SharepointFolder = 16,
		// Token: 0x040008C6 RID: 2246
		RootFolder = 32,
		// Token: 0x040008C7 RID: 2247
		FATFolder = 64,
		// Token: 0x040008C8 RID: 2248
		WebFolder = 128,
		// Token: 0x040008C9 RID: 2249
		SharedOut = 256,
		// Token: 0x040008CA RID: 2250
		SharedIn = 512,
		// Token: 0x040008CB RID: 2251
		PersonFolder = 1024,
		// Token: 0x040008CC RID: 2252
		IcalFolder = 2048,
		// Token: 0x040008CD RID: 2253
		CalendarOverlaid = 4096,
		// Token: 0x040008CE RID: 2254
		OneOffName = 8192,
		// Token: 0x040008CF RID: 2255
		TodoFolder = 16384,
		// Token: 0x040008D0 RID: 2256
		IpfNote = 32768,
		// Token: 0x040008D1 RID: 2257
		IpfDocument = 65536,
		// Token: 0x040008D2 RID: 2258
		IsDefaultStore = 1048576
	}
}
