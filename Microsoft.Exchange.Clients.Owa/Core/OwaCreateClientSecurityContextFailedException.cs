using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001D1 RID: 465
	[Serializable]
	public class OwaCreateClientSecurityContextFailedException : OwaTransientException
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x0005E9CB File Offset: 0x0005CBCB
		public OwaCreateClientSecurityContextFailedException(string message) : base(message)
		{
		}
	}
}
