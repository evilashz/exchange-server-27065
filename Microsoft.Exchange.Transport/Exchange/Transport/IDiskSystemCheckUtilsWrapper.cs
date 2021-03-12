using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000019 RID: 25
	internal interface IDiskSystemCheckUtilsWrapper
	{
		// Token: 0x0600008D RID: 141
		bool IsFilePathOnLockedVolume(string path, out Exception ex);
	}
}
