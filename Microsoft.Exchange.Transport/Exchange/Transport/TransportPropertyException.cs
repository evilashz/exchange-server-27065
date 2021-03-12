using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200044C RID: 1100
	internal class TransportPropertyException : ApplicationException
	{
		// Token: 0x060032B4 RID: 12980 RVA: 0x000C7805 File Offset: 0x000C5A05
		public TransportPropertyException(string message) : base(message)
		{
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000C780E File Offset: 0x000C5A0E
		public TransportPropertyException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
