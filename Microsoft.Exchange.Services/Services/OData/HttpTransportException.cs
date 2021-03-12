using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E0E RID: 3598
	internal class HttpTransportException : Exception
	{
		// Token: 0x06005D41 RID: 23873 RVA: 0x00122E6B File Offset: 0x0012106B
		public HttpTransportException(Exception innerException) : base(innerException.Message, innerException)
		{
		}
	}
}
