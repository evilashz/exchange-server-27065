using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001A7 RID: 423
	[Serializable]
	public sealed class OwaLockTimeoutException : OwaTransientException
	{
		// Token: 0x06000EEC RID: 3820 RVA: 0x0005E5E8 File Offset: 0x0005C7E8
		public OwaLockTimeoutException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}
	}
}
