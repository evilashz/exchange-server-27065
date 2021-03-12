using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public class OwaADObjectNotFoundException : OwaPermanentException
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x00022972 File Offset: 0x00020B72
		public OwaADObjectNotFoundException() : base(null)
		{
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002297B File Offset: 0x00020B7B
		public OwaADObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00022985 File Offset: 0x00020B85
		public OwaADObjectNotFoundException(string message) : base(message)
		{
		}
	}
}
