using System;

namespace Microsoft.Exchange.Security.Dkm.Proxy
{
	// Token: 0x02000007 RID: 7
	internal class ObjectAlreadyExistsException : Exception
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public ObjectAlreadyExistsException()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CAC File Offset: 0x00000EAC
		public ObjectAlreadyExistsException(string message) : base(message)
		{
		}
	}
}
