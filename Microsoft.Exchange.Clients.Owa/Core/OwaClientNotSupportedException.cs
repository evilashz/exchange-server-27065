using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public sealed class OwaClientNotSupportedException : OwaPermanentException
	{
		// Token: 0x06000EE2 RID: 3810 RVA: 0x0005E46C File Offset: 0x0005C66C
		public OwaClientNotSupportedException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0005E477 File Offset: 0x0005C677
		public OwaClientNotSupportedException(string message) : base(message)
		{
		}
	}
}
