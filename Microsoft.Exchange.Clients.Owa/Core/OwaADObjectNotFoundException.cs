using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	public class OwaADObjectNotFoundException : OwaPermanentException
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x0005E8F4 File Offset: 0x0005CAF4
		public OwaADObjectNotFoundException() : base(null)
		{
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0005E8FD File Offset: 0x0005CAFD
		public OwaADObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0005E907 File Offset: 0x0005CB07
		public OwaADObjectNotFoundException(string message) : base(message)
		{
		}
	}
}
