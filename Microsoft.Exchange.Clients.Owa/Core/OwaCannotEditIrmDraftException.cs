using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001CA RID: 458
	[Serializable]
	public sealed class OwaCannotEditIrmDraftException : OwaPermanentException
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x0005E92B File Offset: 0x0005CB2B
		internal OwaCannotEditIrmDraftException(string message) : base(message)
		{
		}
	}
}
