using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000005 RID: 5
	internal class ProxyQueryFailureException : AvailabilityException
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002437 File Offset: 0x00000637
		public ProxyQueryFailureException(string serverName, LocalizedString message, ErrorConstants errorCode, ResponseMessage responseMessage, string responseSource) : base(serverName, errorCode, message)
		{
			this.ResponseMessage = responseMessage;
			this.ResponseSource = responseSource;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002452 File Offset: 0x00000652
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000245A File Offset: 0x0000065A
		public ResponseMessage ResponseMessage { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002463 File Offset: 0x00000663
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000246B File Offset: 0x0000066B
		public string ResponseSource { get; private set; }
	}
}
