using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200002F RID: 47
	[Flags]
	public enum Regions
	{
		// Token: 0x040000E8 RID: 232
		None = 0,
		// Token: 0x040000E9 RID: 233
		NA01 = 1,
		// Token: 0x040000EA RID: 234
		EMEA01 = 2,
		// Token: 0x040000EB RID: 235
		NASIP01 = 4,
		// Token: 0x040000EC RID: 236
		EMEASIP01 = 8,
		// Token: 0x040000ED RID: 237
		CN01 = 16,
		// Token: 0x040000EE RID: 238
		NASIP02 = 32,
		// Token: 0x040000EF RID: 239
		APAC01 = 64,
		// Token: 0x040000F0 RID: 240
		Reserved5 = 128,
		// Token: 0x040000F1 RID: 241
		Reserved6 = 256,
		// Token: 0x040000F2 RID: 242
		Reserved7 = 512,
		// Token: 0x040000F3 RID: 243
		Reserved8 = 1024,
		// Token: 0x040000F4 RID: 244
		Reserved9 = 2048,
		// Token: 0x040000F5 RID: 245
		Reserved10 = 4096,
		// Token: 0x040000F6 RID: 246
		Reserved11 = 8192,
		// Token: 0x040000F7 RID: 247
		Reserved12 = 16384,
		// Token: 0x040000F8 RID: 248
		Reserved13 = 32768,
		// Token: 0x040000F9 RID: 249
		Reserved14 = 65536,
		// Token: 0x040000FA RID: 250
		Reserved15 = 131072,
		// Token: 0x040000FB RID: 251
		Reserved16 = 262144,
		// Token: 0x040000FC RID: 252
		Reserved17 = 524288,
		// Token: 0x040000FD RID: 253
		Reserved18 = 1048576,
		// Token: 0x040000FE RID: 254
		Reserved19 = 2097152,
		// Token: 0x040000FF RID: 255
		Reserved20 = 4194304,
		// Token: 0x04000100 RID: 256
		Reserved21 = 8388608
	}
}
