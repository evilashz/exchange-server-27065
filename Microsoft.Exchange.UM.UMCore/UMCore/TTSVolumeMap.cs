using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001FF RID: 511
	internal abstract class TTSVolumeMap
	{
		// Token: 0x06000EFA RID: 3834 RVA: 0x00043F40 File Offset: 0x00042140
		internal static int GetVolume(CultureInfo culture)
		{
			return LocConfig.Instance[culture].General.TTSVolume;
		}
	}
}
