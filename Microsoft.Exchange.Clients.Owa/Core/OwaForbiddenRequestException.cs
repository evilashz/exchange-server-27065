using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	public sealed class OwaForbiddenRequestException : OwaPermanentException
	{
		// Token: 0x06000F07 RID: 3847 RVA: 0x0005E73E File Offset: 0x0005C93E
		public OwaForbiddenRequestException(string message) : base(message)
		{
		}
	}
}
