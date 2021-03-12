using System;

namespace Microsoft.Exchange.Servicelets.RPCHTTP
{
	// Token: 0x0200000E RID: 14
	internal sealed class WebSitesNotConfiguredException : Exception
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000044F8 File Offset: 0x000026F8
		public WebSitesNotConfiguredException() : base("No IIS web sites were configured on the server.")
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004505 File Offset: 0x00002705
		public WebSitesNotConfiguredException(Exception innerException) : base("No IIS web sites were configured on the server.", innerException)
		{
		}

		// Token: 0x0400004F RID: 79
		private const string ErrorMessage = "No IIS web sites were configured on the server.";
	}
}
