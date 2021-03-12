using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	public class OwaParsingErrorException : OwaPermanentException
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x0005E94D File Offset: 0x0005CB4D
		public OwaParsingErrorException() : base(null)
		{
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0005E956 File Offset: 0x0005CB56
		public OwaParsingErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0005E960 File Offset: 0x0005CB60
		public OwaParsingErrorException(string message) : base(message)
		{
		}
	}
}
