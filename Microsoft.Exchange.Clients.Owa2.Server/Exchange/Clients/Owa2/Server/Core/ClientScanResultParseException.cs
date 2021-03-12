using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000232 RID: 562
	[Serializable]
	internal class ClientScanResultParseException : OwaPermanentException
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x0004CC36 File Offset: 0x0004AE36
		public ClientScanResultParseException(string errorMessage) : this(errorMessage, null)
		{
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0004CC40 File Offset: 0x0004AE40
		public ClientScanResultParseException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
		{
		}
	}
}
