using System;
using Microsoft.Exchange.Common.Bitlocker.Utilities;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200001A RID: 26
	internal sealed class DiskSystemCheckUtilsWrapper : IDiskSystemCheckUtilsWrapper
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000036DA File Offset: 0x000018DA
		public bool IsFilePathOnLockedVolume(string path, out Exception ex)
		{
			return BitlockerUtil.IsFilePathOnLockedVolume(path, out ex);
		}
	}
}
