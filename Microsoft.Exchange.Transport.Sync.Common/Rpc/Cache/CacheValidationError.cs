using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache
{
	// Token: 0x02000096 RID: 150
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CacheValidationError : ValidationError
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x00015F63 File Offset: 0x00014163
		public CacheValidationError(LocalizedString description) : base(description)
		{
		}
	}
}
