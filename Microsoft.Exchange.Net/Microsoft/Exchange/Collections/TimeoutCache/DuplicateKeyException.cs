using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x020006A1 RID: 1697
	internal class DuplicateKeyException : ArgumentException
	{
		// Token: 0x06001F37 RID: 7991 RVA: 0x0003AEA8 File Offset: 0x000390A8
		public DuplicateKeyException() : base("Cannot add a duplicate key.  Use Insert instead")
		{
		}
	}
}
