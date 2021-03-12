using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B5D RID: 2909
	internal abstract class WSTrustException : LocalizedException
	{
		// Token: 0x06003E65 RID: 15973 RVA: 0x000A2E94 File Offset: 0x000A1094
		public WSTrustException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x000A2E9D File Offset: 0x000A109D
		public WSTrustException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}
	}
}
