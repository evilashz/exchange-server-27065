using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	internal class TrackingLocalizedException : LocalizedException
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x000196D6 File Offset: 0x000178D6
		internal TrackingLocalizedException(LocalizedString message) : base(message)
		{
		}
	}
}
