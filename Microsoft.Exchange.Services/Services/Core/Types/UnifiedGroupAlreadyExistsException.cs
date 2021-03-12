using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008AB RID: 2219
	internal sealed class UnifiedGroupAlreadyExistsException : LocalizedException
	{
		// Token: 0x06003F16 RID: 16150 RVA: 0x000DAA6C File Offset: 0x000D8C6C
		public UnifiedGroupAlreadyExistsException() : base(CoreResources.GetLocalizedString((CoreResources.IDs)2930851601U))
		{
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000DAA7E File Offset: 0x000D8C7E
		public UnifiedGroupAlreadyExistsException(Exception innerException) : base(CoreResources.GetLocalizedString((CoreResources.IDs)2930851601U), innerException)
		{
		}
	}
}
