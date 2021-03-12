using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	internal class NoAvailableDownLevelBackEndException : ServerNotFoundException
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000BE47 File Offset: 0x0000A047
		public NoAvailableDownLevelBackEndException(string message) : base(message)
		{
		}
	}
}
