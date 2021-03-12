using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000777 RID: 1911
	internal class BrokenAffinityException : HttpWebResponseWrapperException
	{
		// Token: 0x060025E8 RID: 9704 RVA: 0x0004FED1 File Offset: 0x0004E0D1
		public BrokenAffinityException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, string hostname, string server1, string server2, Exception innerException) : base(message, request, response, innerException)
		{
			this.hostName = hostname;
			this.server1 = server1;
			this.server2 = server2;
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x0004FEF6 File Offset: 0x0004E0F6
		public override string ExceptionHint
		{
			get
			{
				return string.Format("BrokenAffinityException: {0}, {1}, {2}", this.hostName, this.server1, this.server2);
			}
		}

		// Token: 0x040022F8 RID: 8952
		private readonly string hostName;

		// Token: 0x040022F9 RID: 8953
		private readonly string server1;

		// Token: 0x040022FA RID: 8954
		private readonly string server2;
	}
}
