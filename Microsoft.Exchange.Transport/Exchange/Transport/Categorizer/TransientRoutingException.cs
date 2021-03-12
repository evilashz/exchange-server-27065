using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000276 RID: 630
	[Serializable]
	internal class TransientRoutingException : TransientException
	{
		// Token: 0x06001B45 RID: 6981 RVA: 0x0006FD42 File Offset: 0x0006DF42
		public TransientRoutingException(LocalizedString localizedString) : base(localizedString)
		{
		}
	}
}
