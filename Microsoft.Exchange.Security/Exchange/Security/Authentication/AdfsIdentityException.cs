using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200001A RID: 26
	public class AdfsIdentityException : LocalizedException
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00007627 File Offset: 0x00005827
		public AdfsIdentityException(string message) : base(new LocalizedString(message))
		{
		}
	}
}
