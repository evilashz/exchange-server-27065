using System;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007A2 RID: 1954
	internal class EcpErrorResponseException : HttpWebResponseWrapperException
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x0005395F File Offset: 0x00051B5F
		// (set) Token: 0x06002772 RID: 10098 RVA: 0x00053967 File Offset: 0x00051B67
		public EcpErrorResponse EcpErrorResponse { get; private set; }

		// Token: 0x06002773 RID: 10099 RVA: 0x00053970 File Offset: 0x00051B70
		public EcpErrorResponseException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, EcpErrorResponse ecpErrorResponse) : base(message, request, response)
		{
			this.EcpErrorResponse = ecpErrorResponse;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00053984 File Offset: 0x00051B84
		public override string ToString()
		{
			string text = base.ToString();
			text = text + Environment.NewLine + Environment.NewLine;
			if (this.EcpErrorResponse != null)
			{
				text = text + this.EcpErrorResponse.ToString() + Environment.NewLine;
			}
			return text;
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000539C9 File Offset: 0x00051BC9
		public override string ExceptionHint
		{
			get
			{
				return "EcpErrorResponse: " + this.EcpErrorResponse.ExceptionType;
			}
		}
	}
}
