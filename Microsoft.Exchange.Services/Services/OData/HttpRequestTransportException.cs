using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E0F RID: 3599
	internal class HttpRequestTransportException : HttpTransportException
	{
		// Token: 0x06005D42 RID: 23874 RVA: 0x00122E7A File Offset: 0x0012107A
		public HttpRequestTransportException(Exception innerException) : base(innerException)
		{
		}
	}
}
