using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x0200002F RID: 47
	public class HybridConfigurationDetectionException : LocalizedException
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00004D34 File Offset: 0x00002F34
		public HybridConfigurationDetectionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004D3D File Offset: 0x00002F3D
		public HybridConfigurationDetectionException(LocalizedString message, Exception exception) : base(message, exception)
		{
		}
	}
}
