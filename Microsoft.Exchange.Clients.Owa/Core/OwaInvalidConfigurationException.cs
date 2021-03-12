using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	public sealed class OwaInvalidConfigurationException : OwaPermanentException
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0005E6DA File Offset: 0x0005C8DA
		internal OwaInvalidConfigurationException(string message) : base(message)
		{
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0005E6E3 File Offset: 0x0005C8E3
		internal OwaInvalidConfigurationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
