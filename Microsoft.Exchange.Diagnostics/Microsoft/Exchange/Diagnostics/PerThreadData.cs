using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A7 RID: 167
	internal sealed class PerThreadData
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		internal static PerThreadData CurrentThreadData
		{
			get
			{
				PerThreadData perThreadData = PerThreadData.currentThread;
				if (perThreadData == null)
				{
					perThreadData = new PerThreadData();
					PerThreadData.currentThread = perThreadData;
				}
				return perThreadData;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000EFF3 File Offset: 0x0000D1F3
		internal ThreadTraceSettings ThreadTraceSettings
		{
			get
			{
				return this.threadTraceSettings;
			}
		}

		// Token: 0x0400033C RID: 828
		[ThreadStatic]
		private static PerThreadData currentThread;

		// Token: 0x0400033D RID: 829
		private ThreadTraceSettings threadTraceSettings = new ThreadTraceSettings();
	}
}
