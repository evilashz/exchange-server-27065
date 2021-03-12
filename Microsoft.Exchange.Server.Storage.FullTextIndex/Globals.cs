using System;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x02000013 RID: 19
	public static class Globals
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00005505 File Offset: 0x00003705
		public static void Initialize()
		{
			FullTextIndexSchema.Initialize();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000550C File Offset: 0x0000370C
		public static void Terminate()
		{
			FullTextIndexSchema.Terminate();
		}
	}
}
