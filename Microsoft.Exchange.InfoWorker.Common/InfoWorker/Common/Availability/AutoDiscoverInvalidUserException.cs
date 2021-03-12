using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000092 RID: 146
	internal class AutoDiscoverInvalidUserException : AutoDiscoverFailedException
	{
		// Token: 0x0600035A RID: 858 RVA: 0x0000E90D File Offset: 0x0000CB0D
		public AutoDiscoverInvalidUserException(LocalizedString message) : base(message)
		{
		}
	}
}
