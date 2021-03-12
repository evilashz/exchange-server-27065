using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200077B RID: 1915
	internal class NameResolutionException : HttpWebResponseWrapperException
	{
		// Token: 0x060025F4 RID: 9716 RVA: 0x00050064 File Offset: 0x0004E264
		public NameResolutionException(string message, HttpWebRequestWrapper request, Exception innerException, string hostName) : base(message, request, innerException)
		{
			this.hostName = hostName;
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x00050077 File Offset: 0x0004E277
		public override string ExceptionHint
		{
			get
			{
				return "NameResolutionFailure: " + this.hostName;
			}
		}

		// Token: 0x040022FE RID: 8958
		private readonly string hostName;
	}
}
