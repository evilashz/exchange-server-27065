using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200077F RID: 1919
	internal class SslNegotiationException : HttpWebResponseWrapperException
	{
		// Token: 0x06002606 RID: 9734 RVA: 0x00050288 File Offset: 0x0004E488
		public SslNegotiationException(string message, HttpWebRequestWrapper request, SslError sslError, Exception innerException) : base(message, request, innerException)
		{
			this.hostName = request.RequestUri.Host;
			this.sslError = sslError;
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000502AC File Offset: 0x0004E4AC
		public SslErrorType SslErrorType
		{
			get
			{
				return this.sslError.SslErrorType;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x000502B9 File Offset: 0x0004E4B9
		public override string ExceptionHint
		{
			get
			{
				return "SslNegotiationFailure: " + this.hostName;
			}
		}

		// Token: 0x04002304 RID: 8964
		private readonly string hostName;

		// Token: 0x04002305 RID: 8965
		private readonly SslError sslError;
	}
}
