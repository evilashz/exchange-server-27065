using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000041 RID: 65
	internal abstract class AIException : LocalizedException
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		protected AIException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}
	}
}
