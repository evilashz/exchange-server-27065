using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200015F RID: 351
	internal static class TestSupport
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
		public static void SetZerobox()
		{
			TestSupport.s_zerobox = true;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003D4EC File Offset: 0x0003B6EC
		public static string UseLocalMachineNameOnZerobox(string serverName)
		{
			if (!TestSupport.s_zerobox)
			{
				return serverName;
			}
			return Environment.MachineName;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003D4FC File Offset: 0x0003B6FC
		public static bool IsCatalogSeedDisabled()
		{
			return TestSupport.s_zerobox;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003D503 File Offset: 0x0003B703
		public static bool IsZerobox()
		{
			return TestSupport.s_zerobox;
		}

		// Token: 0x040005D3 RID: 1491
		private static bool s_zerobox;
	}
}
