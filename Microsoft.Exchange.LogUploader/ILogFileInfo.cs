using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000012 RID: 18
	internal interface ILogFileInfo
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000DC RID: 220
		DateTime LastWriteTimeUtc { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000DD RID: 221
		DateTime CreationTimeUtc { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000DE RID: 222
		// (set) Token: 0x060000DF RID: 223
		ProcessingStatus Status { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000E0 RID: 224
		string FileName { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000E1 RID: 225
		string FullFileName { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000E2 RID: 226
		// (set) Token: 0x060000E3 RID: 227
		bool IsActive { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000E4 RID: 228
		long Size { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000E5 RID: 229
		IWatermarkFile WatermarkFileObj { get; }
	}
}
