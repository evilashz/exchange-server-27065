using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000D7 RID: 215
	[Flags]
	public enum FormatBarButtonGroups
	{
		// Token: 0x040004ED RID: 1261
		None = 0,
		// Token: 0x040004EE RID: 1262
		BoldItalicUnderline = 1,
		// Token: 0x040004EF RID: 1263
		Justification = 2,
		// Token: 0x040004F0 RID: 1264
		Lists = 4,
		// Token: 0x040004F1 RID: 1265
		Indenting = 8,
		// Token: 0x040004F2 RID: 1266
		Direction = 16,
		// Token: 0x040004F3 RID: 1267
		ForegroundColor = 32,
		// Token: 0x040004F4 RID: 1268
		BackgroundColor = 64,
		// Token: 0x040004F5 RID: 1269
		RemoveFormatting = 128,
		// Token: 0x040004F6 RID: 1270
		HorizontalRule = 256,
		// Token: 0x040004F7 RID: 1271
		UndoRedo = 512,
		// Token: 0x040004F8 RID: 1272
		Hyperlinking = 1024,
		// Token: 0x040004F9 RID: 1273
		SuperSubScript = 2048,
		// Token: 0x040004FA RID: 1274
		Strikethrough = 4096,
		// Token: 0x040004FB RID: 1275
		Customize = 8192,
		// Token: 0x040004FC RID: 1276
		All = 16383
	}
}
