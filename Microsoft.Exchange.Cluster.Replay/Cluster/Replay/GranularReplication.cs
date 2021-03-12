using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000339 RID: 825
	internal static class GranularReplication
	{
		// Token: 0x0600217E RID: 8574 RVA: 0x0009B8A2 File Offset: 0x00099AA2
		public static string FormPartialLogFileName(string prefix, long generation)
		{
			return EseHelper.MakeLogfileName(prefix, ".jsl", generation);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0009B8B0 File Offset: 0x00099AB0
		public static string FormShortAcllLogFileName(string prefix, long generation)
		{
			return EseHelper.MakeLogfileName(prefix, ".acll", generation);
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0009B8BE File Offset: 0x00099ABE
		public static bool IsEnabled()
		{
			return !RegistryParameters.DisableGranularReplication;
		}

		// Token: 0x04000DBC RID: 3516
		public const string JetShadowLogFileExtension = "jsl";

		// Token: 0x04000DBD RID: 3517
		public const string JetShadowLogFileExtensionWithDot = ".jsl";

		// Token: 0x04000DBE RID: 3518
		public const string AcllBlockModeExtensionWithDot = ".acll";
	}
}
