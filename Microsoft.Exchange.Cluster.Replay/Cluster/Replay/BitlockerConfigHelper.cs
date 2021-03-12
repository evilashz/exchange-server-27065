using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200029B RID: 667
	internal static class BitlockerConfigHelper
	{
		// Token: 0x06001A0B RID: 6667 RVA: 0x0006C9CB File Offset: 0x0006ABCB
		public static bool IsBitlockerWin8UsedOnlyEncryptionFeatureEnabled()
		{
			return !RegistryParameters.BitlockerWin8UsedOnlyDisabled;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0006C9D5 File Offset: 0x0006ABD5
		public static bool IsBitlockerEmptyWin8VolumesUsedOnlyEncryptionFeatureEnabled()
		{
			return !RegistryParameters.BitlockerWin8EmptyUsedOnlyDisabled;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0006C9DF File Offset: 0x0006ABDF
		public static bool IsBitlockerEmptyWin7VolumesFullVolumeEncryptionFeatureEnabled()
		{
			return !RegistryParameters.BitlockerWin7EmptyFullVolumeDisabled;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0006C9E9 File Offset: 0x0006ABE9
		public static bool IsBitlockerManagerEnabled()
		{
			return !RegistryParameters.BitlockerFeatureDisabled;
		}
	}
}
