using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001AD RID: 429
	[Serializable]
	public sealed class OwaBodyConversionFailedException : OwaPermanentException
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x0005E6ED File Offset: 0x0005C8ED
		public OwaBodyConversionFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
