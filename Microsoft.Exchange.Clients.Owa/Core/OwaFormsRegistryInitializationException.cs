using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	public sealed class OwaFormsRegistryInitializationException : OwaPermanentException
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0005E3FF File Offset: 0x0005C5FF
		public OwaFormsRegistryInitializationException(string message, Exception innerException) : base(message, innerException, null)
		{
		}
	}
}
