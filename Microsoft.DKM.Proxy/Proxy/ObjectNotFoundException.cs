using System;

namespace Microsoft.Exchange.Security.Dkm.Proxy
{
	// Token: 0x02000008 RID: 8
	internal class ObjectNotFoundException : Exception
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002CB5 File Offset: 0x00000EB5
		public ObjectNotFoundException()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002CBD File Offset: 0x00000EBD
		public ObjectNotFoundException(string message) : base(message)
		{
		}
	}
}
